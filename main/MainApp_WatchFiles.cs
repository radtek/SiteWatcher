using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SiteWatcher.Core;
using SiteWatcher.Core.WatcherFile;
using SiteWatcher.Ctr;
using SiteWatcher.Entity;
using XS.Core;

namespace SiteWatcher
{
    public partial class MainApp 
    {
        #region 检视
        //FileSystemWatcher _watchFolder = new FileSystemWatcher();
        private delegate void DelegateAddWatchFileToList(ListViewItem lvi);//用来在扫描过程中回调
        private DelegateAddWatchFileToList dlgAddWatchFileToList;
        private MyFileSystemWather myWather;
        private string[] WatherFileType;

        private void AddWatchFileToList(ListViewItem lvi)
        {
            lstWatchList.Items.Add(lvi);
        }
        private void btnToStartWatch_Click(object sender, EventArgs e)
        {
            if (btnToStartWatch.Text.Equals("开始监视"))
            {
                dlgAddWatchFileToList = AddWatchFileToList;
                string wft = txtWatchType.Text;

                Core.Configs.ConfigsControl.Instance.WatchType = wft;
                Core.Configs.ConfigsControl.SaveConfig();

                if (!string.IsNullOrEmpty(wft))
                {
                    WatherFileType = wft.Split(',');

                }
                else
                {
                    MessageBox.Show("请设置要监视的文件！");
                    return;
                }

                myWather = new MyFileSystemWather(txtWatchPath.Text, "", true);
                myWather.OnChanged += new FileSystemEventHandler(Watch_AMD);
                myWather.OnCreated += new FileSystemEventHandler(Watch_AMD);
                myWather.OnDeleted += new FileSystemEventHandler(Watch_AMD);
                myWather.OnRenamed += new RenamedEventHandler(Watch_Renamed);



                myWather.Start();

                btnToStartWatch.Text = "停止监视";
                btnToStartWatch.ForeColor = Color.Red;
                
                //startActivityMonitoring();
            }
            else
            {
                btnToStartWatch.Text = "开始监视";
                btnToStartWatch.ForeColor = Color.Black;

                myWather.Stop();
            }
        }

        private void Watch_FindMuma(string FullPath, ListViewItem lvi, string tips)
        {
            
            int iMuma = IsMuma(FullPath, 5000);
            if (iMuma > 0)
            {
                string tipsMsg = string.Empty;
                if (iMuma == 1)
                {
                    tipsMsg = tips + "-可疑,代码含有文件读写操作";
                    lvi.SubItems.Add(tipsMsg);
                }
                else if (iMuma == 2)
                {
                    tipsMsg = tips + "-可疑,代码含有iframe";
                    lvi.SubItems.Add(tipsMsg);
                }
                
                BLL.EmailQueue.Instance.AddEmailToDB(tipsMsg, string.Format("{0},发生时间为:{1},服务器IP:{2}", tipsMsg, DateTime.Now, Utils.GetIPAddress()));
                lvi.ForeColor = Color.Red;
                lvi.Tag = 1;
            }
            else
            {
                //BLL.EmailQueue.Instance.AddEmailToDB(tips, string.Format("{0},发生时间为:{1},服务器IP:{2}", tips, DateTime.Now, Utils.GetIPAddress()));
                lvi.SubItems.Add(tips);
            }
        }
        private static  object _objlock = new object();
        private void Watch_AMD(object sender, System.IO.FileSystemEventArgs e)
        {
            lock (_objlock)
            {
                if (IsHaveType(e.FullPath))
                {
                    ListViewItem lvi = new ListViewItem();
                    string tipsMsg = "";
                    switch (e.ChangeType)
                    {
                        case WatcherChangeTypes.Changed:

                            lvi.SubItems[0].Text = e.FullPath;
                            Watch_FindMuma(e.FullPath, lvi, String.Format("修改,修改时间:{0}",DateTime.Now));
                            AddItemToWatchList(lvi);
                            tipsMsg = "检视文件时发现有人修改了文件，请管理员检查情况！";

                            BLL.EmailQueue.Instance.AddEmailToDB(tipsMsg, string.Format("{0},发生时间为:{1},服务器IP:{2},文件路径:{3}", tipsMsg, DateTime.Now, Utils.GetIPAddress(), Path.GetFileName(e.FullPath)));
                            List<Entity.ReCreatFile> lst = BLL.ReCreatFile.Instance.GetList().Where(rd => rd.FilePath.ToLower().Equals(e.FullPath.ToLower())).ToList();
                            if (lst.Count > 0)
                            {
                                foreach (var model in lst)
                                {
                                    if (model.IsStart)
                                    {
                                        try
                                        {
                                            string sNewsContent = XS.Core.FSO.FObject.ReadFile(e.FullPath);
                                            if (!XsUtils.MD5(sNewsContent).Equals(XsUtils.MD5(model.ContentTem)))
                                            {
                                                if (model.FileType == 0)
                                                {
                                                    XS.Core.FSO.FObject.WriteFileUtf8(model.FilePath, model.ContentTem);
                                                }
                                                else
                                                {
                                                    XS.Core.FSO.FObject.WriteFile(model.FilePath, model.ContentTem);
                                                }
                                                BLL.EmailQueue.Instance.AddEmailToDB("已经恢复文件",
                                                    string.Format("{0},发生时间为:{1},服务器IP:{2},文件路径:{3}", "已经恢复文件了被修改的文件", DateTime.Now,
                                                        Utils.GetIPAddress(), Path.GetFileName(e.FullPath)));
                                            }
                                            else
                                            {
                                                BLL.EmailQueue.Instance.AddEmailToDB("修改后的文件与预设模板一至",
                                                    string.Format("{0},发生时间为:{1},服务器IP:{2},文件路径:{3}", tipsMsg, DateTime.Now,
                                                        Utils.GetIPAddress(), Path.GetFileName(e.FullPath)));
                                            }

                                            break;

                                        }
                                        catch (Exception exception)
                                        {
                                            Log.ErrorLog.InfoFormat("试图恢复被修改文件发生错误:{0}", exception.Message);
                                            //Console.WriteLine(exception);
                                            //throw;
                                        }
                                    }
                                }
                            }

                            break;
                        case WatcherChangeTypes.Created:

                            lvi.SubItems[0].Text = e.FullPath;
                            Watch_FindMuma(e.FullPath, lvi, String.Format("创建,创建时间:{0}", DateTime.Now));
                            AddItemToWatchList(lvi);
                            tipsMsg = "检视文件时发现有人创建了文件，请管理员检查情况！";
                            BLL.EmailQueue.Instance.AddEmailToDB(tipsMsg, string.Format("{0},发生时间为:{1},服务器IP:{2}", tipsMsg, DateTime.Now, Utils.GetIPAddress()));


                            break;
                        case WatcherChangeTypes.Deleted:
                            lvi.SubItems[0].Text = e.FullPath;

                            Watch_FindMuma(e.FullPath, lvi, String.Format("删除,删除时间:{0}", DateTime.Now));
                            AddItemToWatchList(lvi);
                            tipsMsg = "检视文件时发现有人删除了文件，请管理员检查情况！";
                            BLL.EmailQueue.Instance.AddEmailToDB(tipsMsg, string.Format("{0},发生时间为:{1},服务器IP:{2}", tipsMsg, DateTime.Now, Utils.GetIPAddress()));


                            break;
                        default: // Another action
                            break;
                    }
                }
                
            }
            
        }
        public void Watch_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            lock (_objlock)
            {
                if (IsHaveType(e.FullPath))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = e.FullPath;
                    Watch_FindMuma(e.FullPath, lvi, String.Format("重命名,重命名时间:{0}", DateTime.Now));
                    lvi.SubItems.Add(String.Format("新名称:{0},原来名称:{1}", e.Name, e.OldName));
                    AddItemToWatchList(lvi);
                    string tipsMsg = "检视文件时发现有人重命名了文件，请管理员检查情况！";
                    BLL.EmailQueue.Instance.AddEmailToDB(tipsMsg, string.Format("{0},发生时间为:{1},服务器IP:{2}", tipsMsg, DateTime.Now, Utils.GetIPAddress()));

                }
            }
            
           
        }
        private bool IsHaveType(string FullPath)
        { 

            foreach (string s in WatherFileType)
            {
                if (FullPath.EndsWith(s))
                { 
                    return true; 
                }
            }
            return false;
        }
        private void AddItemToWatchList(ListViewItem lvi)
        {
            lstWatchList.Invoke(dlgAddWatchFileToList, lvi);
        }
        /// <summary>
        /// 添加恢复文件,当指定文件被修改后，快速恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddReCreatFile_Click(object sender, EventArgs e)
        {
            ReCreactFileList rcf = new ReCreactFileList();
            rcf.ShowDialog();
        }

        private void btnClearWatchList_Click(object sender, EventArgs e)
        {
            lstWatchList.Items.Clear();
        }

        #endregion

    }
}

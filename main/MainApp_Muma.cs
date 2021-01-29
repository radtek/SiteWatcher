using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SiteWatcher.Core; 
using SiteWatcher.Ctr;
using SiteWatcher.Entity;
using XS.Core;
using XS.Core.FSO;

namespace SiteWatcher
{
    public partial class MainApp 
    {
        #region 扫描木马


        Thread thMumaFind = null;
        private delegate void DelegateShowInfo(string info);//用来在扫描过程中回调
        private delegate void DelegateBtnEnable(bool isenable, int itype);//用来在扫描过程中回调
        private delegate void DelegateListViewItemUpdate(ListViewItem lvi, Color cl, string Text);//查找木马后回调更新

        private DelegateShowInfo dlgShowScanInfo;
        private DelegateShowInfo dlgAddScanFileToList;
        private DelegateBtnEnable dlgIsEnableBtn;
        private DelegateListViewItemUpdate dlgListViewItemUpdate;

        #region 回调

        private void ListViewItemUpdate(ListViewItem lvi, Color cl, string Text)
        {
            lvi.SubItems[1].Text = Text;
            lvi.ForeColor = cl;
        }

        private void ShowScanInfo(string filepath)
        {
            lbFindingInfo.Text = filepath;
        }
        private void AddScanFileToList(string filepath)
        {
            ListViewItem lvi = new ListViewItem();

            lvi.SubItems[0].Text = filepath;
            lvi.SubItems.Add("等待处理");
            lstbFileList.Items.Add(lvi);

            Amib.Threading.IWorkItemResult wir = ThreadPoolManager.Instance.QueueWorkItem(new Amib.Threading.WorkItemCallback(FindMuma), lvi);

        }
        private void IsEnableBtn(bool isenable, int itype)
        {
            switch (itype)
            {
                case 0:
                    btnPause.Enabled = isenable;

                    break;
                case 1:
                    btnStop.Enabled = isenable;
                    break;
            }
        }
        #endregion

        private string[] aScanFileType;
        private string[] aNoScanFolder;
        private void tlbtnFindMuma_Click(object sender, EventArgs e)
        {
            lstbFileList.Items.Clear();
            dlgShowScanInfo = ShowScanInfo;
            dlgAddScanFileToList = AddScanFileToList;
            dlgIsEnableBtn = IsEnableBtn;
            dlgListViewItemUpdate = ListViewItemUpdate;

            string sScanFileType = tooltxtScanFileType.Text.Trim();
            if (!string.IsNullOrEmpty(sScanFileType))
            {
                aScanFileType = sScanFileType.Split(',');
            }
            else
            {
                MessageBox.Show("请设置扫描的文件类型！如 .aspx,.html");
            }

            string sNoscanfoldr = Core.Configs.ConfigsControl.Instance.NoScanFolder;
            if (!string.IsNullOrEmpty(sNoscanfoldr))
            {
                aNoScanFolder = sNoscanfoldr.Split(',');
            }


            if (thMumaFind == null)
                thMumaFind = new Thread(new ThreadStart(startsearch));

            if (thMumaFind.ThreadState == ThreadState.Stopped)
            {
                thMumaFind = null;
                thMumaFind = new Thread(new ThreadStart(startsearch));
            }

            if (!thMumaFind.IsAlive)
            {
                thMumaFind.Start();

                btnPause.Enabled = true;
                btnStop.Enabled = true;
            }




        }

        private int IsMuma(string FullPath, int sleep)
        {
            if (sleep > 0)
            {
                Thread.Sleep(sleep);
            }

            try
            {
                if (FObject.IsExist(FullPath, FsoMethod.File))
                {
                    string sContent = FObject.ReadFile(FullPath);

                    if (sContent.IndexOf("System.IO") > -1 || sContent.IndexOf("FileSystemObject") > -1)
                    {
                        return 1;
                    }
                    else if (sContent.IndexOf("<iframe") > -1)
                    {
                        return 2;
                    }
                }
            }
            catch (Exception e)
            {
                Utils.WriteLog(string.Format("文件读取发生错误:{0},错误信息:{1}", FullPath, e.Message));
                //throw;
            }

            return 0;
        }
        private object FindMuma(object lvi)
        {
            ListViewItem li = lvi as ListViewItem;

            string filepath = li.Text;

            if (!string.IsNullOrEmpty(filepath))
            {
                int iMuma = IsMuma(filepath, 100);
                if (iMuma > 0)
                {
                    //li.SubItems.Add("可疑文件");
                    //li.SubItems[1].Text = "可疑文件";
                    //li.ForeColor = Color.Red;
                    //dlgListViewItemUpdate.Invoke(li,Color.Red, "可疑文件");
                    //lstbFileList.Items.Add(li);
                    if (iMuma == 1)
                    {
                        lstbFileList.Invoke(dlgListViewItemUpdate, li, Color.Red, "可疑，代码含有文件读写操作");
                    }
                    else if (iMuma == 2)
                    {
                        lstbFileList.Invoke(dlgListViewItemUpdate, li, Color.Red, "可疑，代码含有iframe");
                    }
                    MumaCount++;
                }
                else
                {

                    lstbFileList.Invoke(dlgListViewItemUpdate, li, Color.DarkGray, "正常");
                }

            }


            return 1;
        }


        private int MumaCount = 0;
        private void startsearch()
        {


            string FilePath = txtScanFilePath.Text.Trim();
            ScanFiles(FilePath);
            //lbFindingInfo.Text = string.Format("扫描完毕！共扫描文件{0}个", lstbFileList.Items.Count);
            lbFindingInfo.Invoke(dlgShowScanInfo, string.Format("扫描完毕！共扫描文件{0}个", lstbFileList.Items.Count));
            btnPause.Invoke(dlgIsEnableBtn, false, 0);
            btnStop.Invoke(dlgIsEnableBtn, false, 1);
            //var remails = Core.Configs.ConfigsControl.Instance.GetReciveEmails();
            if(MumaCount>0)
                BLL.EmailQueue.Instance.AddEmailToDB(string.Format("{0}木马扫描报告", DateTime.Now), string.Format("扫描完毕！共扫描文件{0}个,发现可疑文件{1}个，报告于：{2}", lstbFileList.Items.Count, MumaCount, DateTime.Now));

            //foreach (var e in remails)
            //{ 
            //    BLL.EmailQueue.Instance.AddEmailToDB(string.Format("{0}木马扫描报告", DateTime.Now), string.Format("扫描完毕！共扫描文件{0}个,发现可疑文件{1}个，报告于：{2}", lstbFileList.Items.Count, MumaCount, DateTime.Now));

            //}

        }
        private bool IsNoScanFolder(string path)
        {
            if (!Equals(aNoScanFolder, null))
            {
                foreach (string s in aNoScanFolder)
                {
                    if (path.EndsWith(s))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private void ScanFiles(string filepath)
        {
            if (filepath.Trim().Length > 0)
            {

                string[] filecollect = null;
                try
                {
                    lbFindingInfo.Invoke(dlgShowScanInfo, string.Concat("正在计算列表:", filepath));
                    //lab_showfile.Invoke(dlg, string.Concat("正在计算列表:", filepath));
                    filecollect = Directory.GetFileSystemEntries(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出错了！" + ex.Message);
                    ex.ToString();
                }

                if (!Equals(filecollect, null))
                {
                    foreach (string file in filecollect)
                    {

                        lbFindingInfo.Invoke(dlgShowScanInfo, file);
                        //lab_showfile.Invoke(dlg, file);

                        if (Directory.Exists(file))
                        {
                            if (!IsNoScanFolder(file))
                                ScanFiles(file);

                        }
                        else
                        {
                            foreach (string file_extend in aScanFileType)
                            {
                                if (file.EndsWith(file_extend))
                                {
                                    lstbFileList.Invoke(dlgAddScanFileToList, file);
                                }
                            }

                        }
                    }
                }


            }
        }





        private void btnPause_Click(object sender, EventArgs e)
        {
            if (thMumaFind == null) return;
            Button btn = sender as Button;
            if (btn == null) return;

            if (thMumaFind.ThreadState == ThreadState.Running)
            {
                thMumaFind.Suspend();
                btn.Text = " 继 续 ";
            }
            else if (thMumaFind.ThreadState == ThreadState.Suspended)
            {
                thMumaFind.Resume();
                btn.Text = " 暂 停 ";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {


            if (thMumaFind != null)
            {
                try
                {
                    if (thMumaFind.ThreadState == ThreadState.Suspended)
                    {
                        thMumaFind.Resume();
                        while (thMumaFind.ThreadState != ThreadState.Running)
                        {

                        }

                    }
                    thMumaFind.Abort();
                    thMumaFind.Join();
                    thMumaFind = null;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }

        private void toolbtnOpenFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();//c#实现的类，代码关键
            fld.ShowDialog();
            txtScanFilePath.Text = fld.SelectedPath.Trim();
        }
        #endregion

    }
}

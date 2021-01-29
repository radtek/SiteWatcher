using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SiteWatcher.Core;
using XS.Core.FSO;

namespace SiteWatcher
{
    public partial class FileSearch : Form
    {
        public FileSearch()
        {
            InitializeComponent();

            lstFiles.View = View.Details;//只有设置为这个HeaderStyle才有用
            lstFiles.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstFiles.GridLines = true;//显示网格线
            lstFiles.FullRowSelect = true;//选择时选择是行，而不是某列 
            lstFiles.Columns.Add("文件名称", 500, HorizontalAlignment.Left);//文本左
            lstFiles.Columns.Add("替换", 80, HorizontalAlignment.Center);//文本距中

        }

        private string FindKey = string.Empty;
        private string ReplaceKey = string.Empty;
        private void btnToSetting_Click(object sender, EventArgs e)
        {
            FileSearchSetting frm = new FileSearchSetting(this);
            frm.ShowDialog();

        }
        private string[] aScanFileType;
        Thread search_thread = null;
        private delegate void DelegateShowInfo(string info);//用来在扫描过程中回调
        private delegate void DelegateBtnEnable(bool isenable, int itype);//用来在扫描过程中回调
        private delegate void DelegateListViewItemUpdate(ListViewItem lvi, Color cl, string Text);//查找木马后回调更新


        private DelegateShowInfo dlgShowScanInfo;
        private DelegateShowInfo dlgAddScanFileToList;
        private DelegateBtnEnable dlgIsEnableBtn;
        private DelegateListViewItemUpdate dlgListViewItemUpdate;
        public void ToSearch(string keyword, string replace)
        {
            FindKey = keyword;
            ReplaceKey = replace;
            string sScanFileType = txtFileType.Text.Trim();
            if (!string.IsNullOrEmpty(sScanFileType))
            {
                aScanFileType = sScanFileType.Split(',');
            }
            else
            {
                MessageBox.Show("请设置扫描的文件类型！如 .aspx,.html");
            }

            lstFiles.Items.Clear();
            dlgShowScanInfo = ShowScanInfo;
            dlgAddScanFileToList = AddScanFileToList;
            dlgIsEnableBtn = IsEnableBtn;
            dlgListViewItemUpdate = ListViewItemUpdate;


            if (search_thread == null)
                search_thread = new Thread(new ThreadStart(startsearch));

            if (search_thread.ThreadState == ThreadState.Stopped)
            {
                search_thread = null;
                search_thread = new Thread(new ThreadStart(startsearch));
            }

            if (!search_thread.IsAlive)
            {
                search_thread.Start();

                btnPause.Enabled = true;
                btnStop.Enabled = true;
            }
        }
        private void AddScanFileToList(string filepath)
        {
            string sContent =  FObject.ReadFile(filepath);
            if (sContent.IndexOf(FindKey) > -1)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = filepath;
                lvi.SubItems.Add("未替换");
                lstFiles.Items.Add(lvi);

                //Amib.Threading.IWorkItemResult wir = ThreadPoolManager.Instance.QueueWorkItem(new Amib.Threading.WorkItemCallback(ToReplace), lvi);
            }
         

        }
        private void ListViewItemUpdate(ListViewItem lvi, Color cl, string Text)
        {
            lvi.SubItems[1].Text = Text;
            lvi.ForeColor = cl;
        }
        private object ToReplace(object lvi)
        {
            ListViewItem li = lvi as ListViewItem;

            string filepath = li.Text;

            if (!string.IsNullOrEmpty(filepath))
            {
                lstFiles.Invoke(dlgListViewItemUpdate, li, Color.Red, "可疑，代码含有文件读写操作");

            }


            return 1;
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
        private void ShowScanInfo(string filepath)
        {
            lbFindingInfo.Text = filepath;
        }
        private void btnSelPathForWatch_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();//c#实现的类，代码关键
            fld.ShowDialog();
            txtFilesPath.Text = fld.SelectedPath.Trim();
        }

        private void startsearch()
        {
            string FilePath = txtFilesPath.Text.Trim();
            ScanFiles(FilePath);
            lbFindingInfo.Invoke(dlgShowScanInfo, string.Format("扫描完毕！共扫描文件{0}个", lstFiles.Items.Count));
            btnPause.Invoke(dlgIsEnableBtn, false, 0);
            btnStop.Invoke(dlgIsEnableBtn, false, 1);
            
        }

        private void ScanFiles(string filepath)
        {
            if (filepath.Trim().Length > 0)
            {

                string[] filecollect = null;
                try
                {
                    lbFindingInfo.Invoke(dlgShowScanInfo, string.Concat("正在计算列表:", filepath));
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
                        
                        if (Directory.Exists(file))
                        {
                            ScanFiles(file);
                        }
                        else
                        {
                            foreach (string file_extend in aScanFileType)
                            {
                                if (file.EndsWith(file_extend))
                                {
                                    lstFiles.Invoke(dlgAddScanFileToList, file);
                                }
                            }

                        }
                    }
                }


            }
        }

        private void lstFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string surl =  lstFiles.SelectedItems[0].Text;

            //MessageBox.Show(surl);

            System.Diagnostics.Process.Start("Explorer", "/select,"+surl);

        }

        private void btnPause_Click(object sender, EventArgs e)
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SiteWatcher.Core;
using SiteWatcher.Core.Plugins; 
using SiteWatcher.Entity;
using SiteWatcher.Properties;
using XS.Core;

namespace SiteWatcher
{
    public partial class MainApp : Form
    {
        private DateTimePicker dtpStart = new DateTimePicker();
        private DateTimePicker dtpEnd = new DateTimePicker();
        private string GroupOrderBy = "";
        private void ToSendReport(object sender, EventArgs e)
        {
            BLL.EmailQueue.Instance.SendEmail();
        }
        public MainApp()
        {
            InitializeComponent();
            //this.TopMost = true;

            this.StartPosition = FormStartPosition.CenterScreen;
            Splash.Status = "状态:载入初始化数据";

            this.FormClosing += MainApp_FormClosing;

            ThreadPoolManager.Init(1);

            timerEmailSend.Interval = 5000;//10秒检测一次
            timerEmailSend.Tick += ToSendReport;
            timerEmailSend.Start();

            #region 初始


            dtpStart.Width = 110;
            dtpEnd.Width = 110;



            lstbFileList.View = View.Details;//只有设置为这个HeaderStyle才有用
            lstbFileList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstbFileList.GridLines = true;//显示网格线
            lstbFileList.FullRowSelect = true;//选择时选择是行，而不是某列
            lstbFileList.Columns.Add("文件目录", 600, HorizontalAlignment.Center);
            lstbFileList.Columns.Add("状态", 100, HorizontalAlignment.Center);

            lstWatchList.View = View.Details;//只有设置为这个HeaderStyle才有用
            lstWatchList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstWatchList.GridLines = true;//显示网格线
            lstWatchList.FullRowSelect = true;//选择时选择是行，而不是某列
            lstWatchList.Columns.Add("文件路径", 600, HorizontalAlignment.Center);
            lstWatchList.Columns.Add("操作", 100, HorizontalAlignment.Center);
            lstWatchList.Columns.Add("备注", 100, HorizontalAlignment.Center);

            lvErrSiteList.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvErrSiteList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvErrSiteList.GridLines = true;//显示网格线
            lvErrSiteList.FullRowSelect = true;//选择时选择是行，而不是某列
            lvErrSiteList.Columns.Add("网站名称", 100, HorizontalAlignment.Center);
            lvErrSiteList.Columns.Add("应用池", 100, HorizontalAlignment.Center);
            lvErrSiteList.Columns.Add("检测网址", 250, HorizontalAlignment.Left);
            lvErrSiteList.Columns.Add("间隔", 50, HorizontalAlignment.Center);
            lvErrSiteList.Columns.Add("错误数", 50, HorizontalAlignment.Center);
            lvErrSiteList.Columns.Add("报告数", 50, HorizontalAlignment.Center);
            lvErrSiteList.Columns.Add("最后错误时间", 100, HorizontalAlignment.Center);
            lvErrSiteList.Columns.Add("ID", 0);


            //winform中，listview是没有办法设置行高的，没行之间排得密密麻麻的，很不好！

            //可以加入一个imagelist来 撑大 行，实现行高的设置！

            //   设置行高   20   
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 20);//分别是宽和高 

            lv404links.View = View.Details;//只有设置为这个HeaderStyle才有用
            lv404links.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lv404links.GridLines = true;//显示网格线
            lv404links.FullRowSelect = true;//选择时选择是行，而不是某列
            lv404links.Columns.Add("检测目标地址", 350, HorizontalAlignment.Left);
            lv404links.Columns.Add("时间间隔", 80, HorizontalAlignment.Center);
            lv404links.Columns.Add("死链数", 80, HorizontalAlignment.Center);
            lv404links.Columns.Add("状态", 120, HorizontalAlignment.Center);
            lv404links.Columns.Add("最后汇报时间", 120, HorizontalAlignment.Center);
            lv404links.Columns.Add("最后汇报结果", 500, HorizontalAlignment.Center);
            lv404links.Columns.Add("ID", 0);
            lv404links.Columns.Add("页面名称", 80);
            lv404links.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大


            lvBadWords.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvBadWords.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvBadWords.GridLines = true;//显示网格线
            lvBadWords.FullRowSelect = true;//选择时选择是行，而不是某列
            lvBadWords.Columns.Add("检测目标地址", 350, HorizontalAlignment.Left);
            lvBadWords.Columns.Add("内容获取方式", 100, HorizontalAlignment.Center);
            lvBadWords.Columns.Add("状态", 120, HorizontalAlignment.Center);
            lvBadWords.Columns.Add("最后汇报时间", 120, HorizontalAlignment.Center);
            lvBadWords.Columns.Add("最后汇报结果", 500, HorizontalAlignment.Center);
            lvBadWords.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大

            lvPageUpdate.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvPageUpdate.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvPageUpdate.GridLines = true;//显示网格线
            lvPageUpdate.FullRowSelect = true;//选择时选择是行，而不是某列
            lvPageUpdate.Columns.Add("检测目标地址", 350, HorizontalAlignment.Left);
            lvPageUpdate.Columns.Add("时间间隔", 180, HorizontalAlignment.Center);
            lvPageUpdate.Columns.Add("状态", 120, HorizontalAlignment.Center);
            lvPageUpdate.Columns.Add("最后汇报时间", 120, HorizontalAlignment.Center);
            lvPageUpdate.Columns.Add("最后汇报结果", 500, HorizontalAlignment.Center);
            lvPageUpdate.Columns.Add("最近一次md5", 120, HorizontalAlignment.Center);
            lvPageUpdate.Columns.Add("ID", 0);

            lvPageUpdate.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大


            lvWebLinkUrl.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvWebLinkUrl.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvWebLinkUrl.GridLines = true;//显示网格线
            lvWebLinkUrl.FullRowSelect = true;//选择时选择是行，而不是某列
            lvWebLinkUrl.Columns.Add("检测目标地址", 350, HorizontalAlignment.Left);
            lvWebLinkUrl.Columns.Add("时间间隔", 180, HorizontalAlignment.Center);
            lvWebLinkUrl.Columns.Add("状态", 120, HorizontalAlignment.Center);
            lvWebLinkUrl.Columns.Add("友链数量", 100, HorizontalAlignment.Center);
            lvWebLinkUrl.Columns.Add("最后汇报时间", 120, HorizontalAlignment.Center);
            lvWebLinkUrl.Columns.Add("ID", 0);
            lvWebLinkUrl.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大

            //WindowState = FormWindowState.Maximized;


            //MyHttpListener _MyHttpListener = new MyHttpListener("http://+:8089/");
            //_MyHttpListener.RequestEvent += OnHttpRequest;
            //_MyHttpListener.Start();
            //dglUpdateHttpWatch = OnUpdateHttpWatch;




            #endregion


            Splash.Status = "状态:启用监听事件";

            //监视cpu与内存
            GetCpuInfo();
            siMemoryInfo = new SystemInfo();
            timerMemory.Interval = 3000;
            timerMemory.Tick += new EventHandler(OntimerMemory);
            //timerMemory.Tick += new EventHandler(NetInfo);
            timerMemory.Start();
            Splash.Status = "状态:获取网卡信息";
            cbNetCar.DataSource = GetNicList();
            if (!string.IsNullOrEmpty(Core.Configs.ConfigsControl.Instance.SelNetCar))
            {
                cbNetCar.Text = Core.Configs.ConfigsControl.Instance.SelNetCar;
                //StarShowNetInfo();
            }

            Splash.Status = "状态:加载站点信息";
            drpTimeSpan.Text = "检测时间间隔(分钟)";
            BindErrSites();
            LoadPlugins();

            Splash.Close();
            this.Show();
            this.Activate();

            urlNoChecks = BLL.BadLinkNoCheck.Instance.GetList();

            //Log.ErrorLog.Error("fsdfsdfsd{0}");

        }

        public void LoadPlugins()
        {

            string PluginPathName = string.Concat(Application.StartupPath, "\\Plugins.MobileSend.dll");
            if (!File.Exists(PluginPathName)) return;
            Assembly asm = null;
            Type[] types = null;
            try
            {
                //asm = Assembly.LoadFile(assembly);
                // 使用此方法可让DLL不会被锁住，加载完后还可以被删除
                asm = Assembly.Load(RetrievePluginAssembly(PluginPathName));

                types = asm.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    // 避免加载到抽象类，它们不能被实例化
                    if (types[i].IsAbstract)
                        continue;
                    Type[] interfaces;
                    interfaces = types[i].GetInterfaces();
                    foreach (Type iface in interfaces)
                    {
                        //这里也可以添加其他插件的 proivder，加多几个 if 判断即可

                        if (iface == typeof(Core.Plugins.IMobileSend))
                        {

                            Utils.MobileSend = CreateInstance<Core.Plugins.IMobileSend>(asm, types[i]);

                            //if (Utils.MobileSend != null)
                            //{
                            //    MessageBox.Show("加载成功！");

                            //    Utils.MobileSend.SendMsg("我是好来们,谢谢","15811071080");
                            //}
                        }
                    }
                }
            }
            catch
            {
            }

        }

        /// <summary>
        /// 创建一个提供者的实例
        /// </summary>
        /// <typeparam name="T">提供者接口类型</typeparam>
        /// <param name="asm">包含指定类型的程序集</param>
        /// <param name="type">要创建的实例类型</param>
        /// <returns>创建的实例，或者 <c>null</c>.</returns>
        private static T CreateInstance<T>(Assembly asm, Type type) where T : class, IProvider
        {
            T instance;
            try
            {
                instance = asm.CreateInstance(type.ToString()) as T;
                return instance;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 读取一个程序集的二进数据
        /// </summary>
        /// <param name="filename">程序集名称 如 DownloadCounterPlugin.dll</param>
        public byte[] RetrievePluginAssembly(string filename)
        {

            lock (this)
            {
                try
                {
                    return File.ReadAllBytes(filename);
                }
                catch (IOException)
                {
                    return null;
                }
            }
        }

        void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭窗体前清空计时器
            if (AppStartInit.BadLinkTimers.Count > 0)
            {
                foreach (WatchLinkTimer lt in AppStartInit.BadLinkTimers.Values)
                {
                    lt.Stop();
                }
                AppStartInit.BadLinkTimers.Clear();
            }
            if (AppStartInit.UpdateLinkTimers.Count > 0)
            {
                foreach (WatchLinkTimer lt in AppStartInit.UpdateLinkTimers.Values)
                {
                    lt.Stop();
                }
                AppStartInit.UpdateLinkTimers.Clear();
            }
            if (AppStartInit.ErrorSiteTimers.Count > 0)
            {
                foreach (WatchLinkTimer lt in AppStartInit.ErrorSiteTimers.Values)
                {
                    lt.Stop();
                }
                AppStartInit.ErrorSiteTimers.Clear();
            }
            Application.ExitThread();
        }

        #region 最小化到托盘程序
        void MainApp_SizeChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //this.ShowInTaskbar = false;
                this.notifyIcon.Visible = true;
            }

        }
        void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Show();
                    this.Activate();
                    this.WindowState = FormWindowState.Normal;
                    this.notifyIcon.Visible = false;
                    this.ShowInTaskbar = true;
                }
            }
        }

        private void tsbtnExit_ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        

        private void btnMumaConfig_Click(object sender, EventArgs e)
        {
            MumaConfig frm = new MumaConfig();
            frm.ShowDialog();
        }

        private void windows安全设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowsSafe ws = new WindowsSafe();
            ws.ShowDialog();
        }

        private void MessageSendToolStripMenuItem_CLick(object sender, System.EventArgs e)
        {
            MobileSetting frm = new MobileSetting();
            frm.Show();
        }

        private void 发送邮箱设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmailSetting frm = new EmailSetting();
            frm.Show();
        }

        private void btnSetBadWord_Click(object sender, EventArgs e)
        {
            BadKeyWord frm = new BadKeyWord();
            frm.Show();
        }


        #region 切换标签
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab.Text.Equals("木马查杀"))
            {
                txtScanFilePath.Text = Core.Configs.ConfigsControl.Instance.SitePath;
                ThreadPoolManager.Init(1);
            }
            else if (tabMain.SelectedTab.Text.Equals("实时文件监视"))
            {

                txtWatchPath.Text = Core.Configs.ConfigsControl.Instance.SitePath;
                txtWatchType.Text = Core.Configs.ConfigsControl.Instance.WatchType;
            }
            else if (tabMain.SelectedTab.Text.Equals("页面内容更新监控"))
            {
                if (lvPageUpdate.Items.Count == 0)
                {
                    BindUpdateLink();
                }

            }
            else if (tabMain.SelectedTab.Text.Equals("死链监控"))
            {
                if (lv404links.Items.Count == 0)
                {
                    BindBadLink();
                }
            }
            else if (tabMain.SelectedTab.Text.Equals("友情链接检控"))
            {
                if (lv404links.Items.Count == 0)
                {
                    BindWebLink();
                }
            }



        }
        #endregion


        private void lv404links_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lv404links.SelectedItems.Count > 0)
                {

                    Point p = new Point();
                    p.X = e.Location.X;
                    p.Y = e.Location.Y + 30;

                    this.cmuBadlink.Show(this, p);
                }

            }
        }
        private void lvPageUpdate_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lvPageUpdate.SelectedItems.Count > 0)
                {

                    Point p = new Point();
                    p.X = e.Location.X;
                    p.Y = e.Location.Y + 110;

                    this.cmuPageContent.Show(this, p);
                }

            }
        }

        private void lvErrSiteList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lvErrSiteList.SelectedItems.Count > 0)
                {

                    Point p = new Point();
                    p.X = e.Location.X;
                    p.Y = e.Location.Y + 110;

                    this.cmuWatchSite.Show(this, p);
                }

            }
        }
        private void 删除地址_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除地址吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem lvi in lv404links.SelectedItems)
                {
                    string sid = lvi.SubItems[6].Text;

                    if (!string.IsNullOrEmpty(sid))
                        BLL.BadLink.Instance.Delete(new Guid(sid));
                }

                BindBadLink();
            }
        }
        private void 网站_删除网站_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除该网站吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem lvi in lvErrSiteList.SelectedItems)
                {
                    string sid = lvi.SubItems[7].Text;

                    if (!string.IsNullOrEmpty(sid))
                    {
                        BLL.SiteErr.Instance.Delete(new Guid(sid));
                    }
                }
                BindErrSites();
            }
        }
        private void 页面_删除网址_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除该网址吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem lvi in lvPageUpdate.SelectedItems)
                {
                    string sid = lvi.SubItems[6].Text;

                    if (!string.IsNullOrEmpty(sid))
                    {
                        BLL.UpdateLink.Instance.Delete(new Guid(sid));
                    }
                }
                BindUpdateLink();
            }
        }

        private void 死链_查看最新报告_Click(object sender, EventArgs e)
        {
            if (lv404links.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lv404links.SelectedItems[0];
                BadLinksReport frm = new BadLinksReport(lvi.SubItems[6].Text);
                frm.Show();
            }

        }

        private void 死链_打开页面_Click(object sender, EventArgs e)
        {

            //string url =  lv404links.SelectedItems[0].Text;
            string target = lv404links.SelectedItems[0].Text;
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
        /// <summary>
        /// 打开帮助网站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void indexToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "http://www.ebsite.net");
        }
        /// <summary>
        /// 打开关于窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            About aboutFrm = new About();
            aboutFrm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            EmailHistory frm = new EmailHistory();
            frm.Owner = this;
            frm.Show();
        }

        private void 网站设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiteSetting frm = new SiteSetting();
            frm.Show();
        }

        private void 文件查找替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSearch frm = new FileSearch();
            frm.Show();
        }

        private void btnSelPathForWatch_Click(object sender, EventArgs e)
        {

        }

        private void 邮件报告ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MobileMsgHistory frm = new MobileMsgHistory();
            frm.Show();
        }


        //private void LoadSaveUrlErrSetting()
        //{
        //    txtErrUrl1.Text = Configs.ConfigsControl.Instance.ErrUrl1;
        //    txtErrUrl2.Text = Configs.ConfigsControl.Instance.ErrUrl2;
        //    txtErrUrl3.Text = Configs.ConfigsControl.Instance.ErrUrl3;
        //    cbErrState.Text = Configs.ConfigsControl.Instance.ErrUrlState;
        //    cbErrUrlSolve.Text = Configs.ConfigsControl.Instance.ErrUrlSolve;
        //}

        //private void btnSaveUrlErrSetting_Click(object sender, EventArgs e)
        //{
        //    Configs.ConfigsControl.Instance.ErrUrl1 = txtErrUrl1.Text.Trim();
        //    Configs.ConfigsControl.Instance.ErrUrl2 = txtErrUrl2.Text.Trim();
        //    Configs.ConfigsControl.Instance.ErrUrl3 = txtErrUrl3.Text.Trim();
        //    Configs.ConfigsControl.Instance.ErrUrlState = cbErrState.Text.Trim();
        //    Configs.ConfigsControl.Instance.ErrUrlSolve = cbErrUrlSolve.Text.Trim();

        //    Configs.ConfigsControl.SaveConfig();

        //    MessageBox.Show("保存设置成功！");


        //}




        private void MainApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (threadCpuInfo != null)
                threadCpuInfo.Abort();
            if (threadCpuInfo != null)
                threadCpuInfo.Abort();
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void btnReportSettingCPU_Click(object sender, EventArgs e)
        {
            ReportSettingCPU fm = new ReportSettingCPU();
            fm.Show();
        }

        //private void btnAddBadTime_Click(object sender, EventArgs e)
        //{

        //}

        //private void lv404links_ItemChecked(object sender, ItemCheckedEventArgs e)
        //{
        //    ListViewItem lvi = lv404links.SelectedItems[0];

        //    string sUrl = lvi.SubItems[0].Text;

        //    MessageBox.Show(sUrl);
        //}

        private void 立即检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lv404links.SelectedItems[0];

            string sUrl = lvi.SubItems[0].Text;

            WatchTimerEventArgs Args = new WatchTimerEventArgs();
            Args.Url = sUrl;
            Args.UpdateTime = DateTime.Now;
            //Args.ReportMsg = "汇报情况";
            if (!Equals(thThreadCheckLink, null))
            {
                thThreadCheckLink.Abort();

            }

            CurrentBadLink = "";

            foreach (ListViewItem lviSub in lv404links.Items)
            {
                if (lviSub.SubItems[0].Text.StartsWith("--"))
                {
                    lviSub.Remove();
                }
            }

            foreach (ListViewItem lviItem in lv404links.Items)
            {
                if (!lviItem.SubItems[0].Text.Equals(sUrl))
                {
                    lviItem.SubItems[3].Text = "排队中";
                    lviItem.BackColor = Color.Yellow;

                }
            }

            UpdateBadLinkLvItem(null, Args);
        }

        
    } }


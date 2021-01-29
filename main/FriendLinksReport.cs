using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SiteWatcher.Entity;

namespace SiteWatcher
{
    public partial class FriendLinksReport : Form
    {
        private Entity.FriendLink model;
        public FriendLinksReport(string datakey)
        {
            InitializeComponent();
            
            //   设置行高   20   
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 20);//分别是宽和高
            
            lvReport.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvReport.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvReport.GridLines = true;//显示网格线
            lvReport.FullRowSelect = true;//选择时选择是行，而不是某列
            lvReport.Columns.Add("页面地址", 200, HorizontalAlignment.Center);
            lvReport.Columns.Add("是否死链", 80, HorizontalAlignment.Center);
            lvReport.Columns.Add("初始百度权重", 100, HorizontalAlignment.Center);
            lvReport.Columns.Add("初始移动权重", 100, HorizontalAlignment.Center);
            //lvReport.Columns.Add("上月BR", 50, HorizontalAlignment.Center);
            //lvReport.Columns.Add("上月PR", 50, HorizontalAlignment.Center);
            lvReport.Columns.Add("当前百度权重", 100, HorizontalAlignment.Center);
            lvReport.Columns.Add("当前移动权重", 100, HorizontalAlignment.Center);

            lvReport.Columns.Add("友链数量", 80, HorizontalAlignment.Center);
            lvReport.Columns.Add("是否有反链", 80, HorizontalAlignment.Center);
            lvReport.Columns.Add("反链文本", 80, HorizontalAlignment.Center);
            lvReport.Columns.Add("是否NoFllow", 80, HorizontalAlignment.Center);
            lvReport.Columns.Add("汇报情况", 250, HorizontalAlignment.Center);
            lvReport.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大

            model = BLL.FriendLink.Instance.GetEntity(new Guid(datakey));//BLL.BadLink.Instance.GetOne(datakey);
            txtInfo.Text = string.Format("检测地址:{0},{1},最后检测时间{2}", model.PUrl, model.IsHaveBad?"发现异常":"正常", model.LastTestDate);

            txtFriendLinkNoCheck.Text = model.NoToCheckUrl;

            BindData();
        }

        private void BindData()
        {
            if (!Equals(model, null))
            {
                List<FriendLinkReport> lst =  model.Reports;
                lvReport.Items.Clear();

                foreach (FriendLinkReport report in lst)
                {

                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = report.PUrl;

                    if (!report.IsNoToCheckUrl)
                    {
                        lvi.SubItems.Add(report.IsOK ? "否" : "是");
                        lvi.SubItems.Add(report.AddBR.ToString());
                        lvi.SubItems.Add(report.AddBR.ToString());
                        lvi.SubItems.Add(report.CurrentBR.ToString());
                        lvi.SubItems.Add(report.CurrentPR.ToString());

                        lvi.SubItems.Add(report.OutLinkCount.ToString());
                        lvi.SubItems.Add(report.IsLinkBack ? "" : "无反链");
                        lvi.SubItems.Add(report.LinkBackText);
                        lvi.SubItems.Add(report.IsNofollow ? "是" : "");

                        lvi.SubItems.Add(report.Info);
                        if (BLL.FriendLink.Instance.IsBadLink(report))
                        {
                            lvi.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        lvi.SubItems.Add("---");
                        lvi.SubItems.Add("---");
                        lvi.SubItems.Add("---");
                        lvi.SubItems.Add("---");
                        lvi.SubItems.Add("---");

                        lvi.SubItems.Add("---");
                        lvi.SubItems.Add("---");
                        lvi.SubItems.Add("---");
                        lvi.SubItems.Add("---");

                        lvi.SubItems.Add("忽略不检测");
                        lvi.BackColor = Color.DarkGray;
                    }
                    
                    lvReport.Items.Add(lvi);
                }
            }
        }

        private void lvReport_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lvReport.SelectedItems.Count > 0)
                {

                    Point p = new Point();
                    p.X = e.Location.X;
                    p.Y = e.Location.Y + 30;

                    this.cmnMenu.Show(this, p);
                }

            }
        }

        private void 复制网址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvReport.SelectedItems[0];

            string sUrl = lvi.SubItems[0].Text.Replace("--","");
            

            Thread th = new Thread(() =>
            {

                Clipboard.SetText(sUrl);

            });
            th.ApartmentState = ApartmentState.STA;
            th.Start();


        }

        private void 打开网址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvReport.SelectedItems[0];

            string sUrl = lvi.SubItems[0].Text.Replace("--", "");
            Process.Start(sUrl);
        }

        private void btnFriendLinkNoCheck_Click(object sender, EventArgs e)
        {
            model.NoToCheckUrl =  txtFriendLinkNoCheck.Text.Trim();
            model.Reports.Clear();
            BLL.FriendLink.Instance.UpdateOne(model);

            MessageBox.Show("保存成功！");
        }
    }
}

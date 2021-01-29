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
    public partial class BadLinksReport : Form
    {
        private Entity.BadLink model;
        public BadLinksReport(string datakey)
        {
            InitializeComponent();


            lvReport.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvReport.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvReport.GridLines = true;//显示网格线
            lvReport.FullRowSelect = true;//选择时选择是行，而不是某列
            lvReport.Columns.Add("页面地址", 350, HorizontalAlignment.Center);
            lvReport.Columns.Add("是否死链接", 100, HorizontalAlignment.Center);
            lvReport.Columns.Add("报告结果", 300, HorizontalAlignment.Left);

            model = BLL.BadLink.Instance.GetEntity(new Guid(datakey));//BLL.BadLink.Instance.GetOne(datakey);
            txtInfo.Text = string.Format("检测地址:{0},找到死链接{1}个,最后检测时间{2}", model.PUrl, model.BadLinkCount, model.LastTestDate);
            BindData();
        }

        private void BindData()
        {
            if (!Equals(model, null))
            {
                List<BadLinkReport> lst =  model.Reports;
                lvReport.Items.Clear();

                foreach (BadLinkReport report in lst)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = report.PUrl;
                    lvi.SubItems.Add(report.IsOK ? "否" : "是");
                    lvi.SubItems.Add(report.Info);

                    if (!report.IsOK)
                    {
                        lvi.BackColor = Color.Red;
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using SiteWatcher.Entity;

namespace SiteWatcher
{
    public partial class ReCreactFileList : Form
    {
        public ReCreactFileList()
        {
            InitializeComponent();

            lvReCreactFile.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvReCreactFile.GridLines = true;//显示网格线
            lvReCreactFile.FullRowSelect = true;//选择时选择是行，而不是某列 
            lvReCreactFile.Columns.Add("添加时间", 100, HorizontalAlignment.Center);
            lvReCreactFile.Columns.Add("路径", 500, HorizontalAlignment.Center);
            cbBox.Text = "UTF-8";
            BindData();
            
        }

        public Guid ModifyId = Guid.Empty;
        private void btcSave_Click(object sender, EventArgs e)
        {
            Entity.ReCreatFile model = new Entity.ReCreatFile();
            model.FilePath = txtPath.Text.Trim();
            model.ContentTem = txtContent.Text;
            model.IsStart = cbIsStart.Checked;
            model.FileType = cbBox.Text.Equals("UTF-8") ? 0 : 1;
            model.AdDateTime = DateTime.Now;
            if (ModifyId == Guid.Empty)
            {
                BLL.ReCreatFile.Instance.Add(model);
            }
            else
            {
                model.Id = ModifyId;
                BLL.ReCreatFile.Instance.UpdateOne(model);
            }

            BindData();


        }

        private void BindData()
        {
            lvReCreactFile.Items.Clear();
            List<Entity.ReCreatFile> lst = BLL.ReCreatFile.Instance.GetList();
            foreach (ReCreatFile queue in lst)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = queue.AdDateTime.ToString(); 
                lvi.SubItems.Add(queue.FilePath);
                lvi.SubItems.Add(queue.Id.ToString());
                lvReCreactFile.Items.Add(lvi);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
              ModifyId = Guid.Empty;
            btcSave.Text = "添加";
            txtContent.Text = "";
            txtPath.Text = "";
        }

        private void lvReCreactFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem currentRow = lvReCreactFile.SelectedItems[0];
            ModifyId = Guid.Parse(currentRow.SubItems[2].Text);

            var model = BLL.ReCreatFile.Instance.GetEntity(ModifyId);

            txtPath.Text = model.FilePath;
            txtContent.Text = model.ContentTem;
            cbIsStart.Checked = model.IsStart;

            cbBox.Text = model.FileType == 0 ? "UTF-8" : "gb2312";
            btcSave.Text = "修改";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ListViewItem currentRow = lvReCreactFile.SelectedItems[0];
            var gId = Guid.Parse(currentRow.SubItems[2].Text);
            BLL.ReCreatFile.Instance.Delete(gId);
            BindData();
        }

        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }
    }
}

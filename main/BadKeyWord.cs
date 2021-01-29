using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiteWatcher.BLL;

namespace SiteWatcher
{
    public partial class BadKeyWord : Form
    {
        public BadKeyWord()
        {
            InitializeComponent();

            lvBadWords.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvBadWords.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvBadWords.GridLines = true;//显示网格线
            lvBadWords.FullRowSelect = true;//选择时选择是行，而不是某列
            lvBadWords.Columns.Add("id", 0);
            lvBadWords.Columns.Add("关键词", 120, HorizontalAlignment.Left);
            lvBadWords.Columns.Add("词频", 100, HorizontalAlignment.Center);
            lvBadWords.Columns.Add("添加时间", 120, HorizontalAlignment.Center);
            BindBadWord();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Entity.IllegalWord md = new Entity.IllegalWord();

            md.WordName = txtBadWord.Text;
            md.WordNum = int.Parse(txtShowNum.Text);
            md.AddDateTime = DateTime.Now;
            IllegalWord.Instance.Add(md);
            //IllegalWord.Instance.InsertOne(md);

            BindBadWord();
        }

        private void BindBadWord()
        {
           List<Entity.IllegalWord> lst =  IllegalWord.Instance.GetList();
            lvBadWords.Items.Clear();
            foreach (Entity.IllegalWord word in lst)
            {
               
                    ListViewItem lvi = new ListViewItem();
                    
                    lvi.SubItems[0].Text = word.Id.ToString();
                    lvi.SubItems.Add(word.WordName);
                    lvi.SubItems.Add(word.WordNum.ToString());//更新第一个列的值，如果您使用惯了asp.net,可能觉得这样更新有点不可理解
                    lvi.SubItems.Add(word.AddDateTime.ToString());
                    lvBadWords.Items.Add(lvi);
               
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in lvBadWords.SelectedItems)
            {
                string _id = selectedItem.Text;
                IllegalWord.Instance.Delete(new Guid(_id));
            }
            BindBadWord();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SiteWatcher
{
    public partial class BadLinkNoCheck : Form
    {
        public BadLinkNoCheck()
        {
            InitializeComponent();
            lbList.DisplayMember = "Url";
            lbList.ValueMember = "Id";
            BindData();
        }

        private void BindData()
        {
            lbList.Items.Clear();
            List<Entity.BadLinkNoCheck> lst =  BLL.BadLinkNoCheck.Instance.GetList();
            foreach (var url in lst)
            {
                lbList.Items.Add(url);
            }

            //lbList.DataSource = lst;


        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sUrl = txtUrl.Text.Trim();
            if (!string.IsNullOrEmpty(sUrl))
            {
                BLL.BadLinkNoCheck.Instance.Add(new Entity.BadLinkNoCheck() { Url = sUrl });
                BindData();
            }
               
            else
            {
                MessageBox.Show("地址不能为空！");
            }

        }

        private void btnDelSels_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedItems.Count > 0)
            {
                foreach (var item in lbList.SelectedItems)
                {
                    var obj = item as Entity.BadLinkNoCheck;

                    BLL.BadLinkNoCheck.Instance.Delete(obj.Id);

                }
                BindData();
            }
            
        }
    }
}

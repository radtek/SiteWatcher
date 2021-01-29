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
    public partial class FileSearchSetting : Form
    {
        private FileSearch frmParent;
        public FileSearchSetting(FileSearch _frmParent)
        {
            InitializeComponent();
            frmParent = _frmParent;
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKeyWord.Text.Trim()))
            {
                MessageBox.Show("查找的关键字不能为空！");
                return;
            }

            frmParent.ToSearch(txtKeyWord.Text.Trim(),txtReplace.Text);

            this.Close();
        }

         
    }
}

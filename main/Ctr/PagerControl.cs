using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SiteWatcher.Ctr
{

    public partial class PagerControl : UserControl
    {
        #region 构造函数

        public PagerControl()
        {
            InitializeComponent();
        }

        public PagerControl(int currentPage, int recordsPerPage, int totalCount)
        {
            InitializeComponent();
            this.totalCount = totalCount;
            this.recordsPerPage = recordsPerPage;
            this.currentPage = currentPage;
            DrawControl();
        }

        public PagerControl(int currentPage, int recordsPerPage, int totalCount, string jumpText)
        {
            InitializeComponent();
            this.totalCount = totalCount;
            this.recordsPerPage = recordsPerPage;
            this.currentPage = currentPage;
            this.JumpText = jumpText;
            DrawControl();
        }

        #endregion

        #region 分页字段和属性

        private int currentPage = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public virtual int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        private int recordsPerPage = 15;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public virtual int RecordsPerPage
        {
            get { return recordsPerPage; }
            set { recordsPerPage = value; }
        }

        private int totalCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public virtual int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }

        private int pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (recordsPerPage != 0)
                {
                    pageCount = GetPageCount();
                }
                return pageCount;
            }
        }

        private string jumpText;
        /// <summary>
        /// 跳转按钮文本
        /// </summary>
        public string JumpText
        {
            get
            {
                if (string.IsNullOrEmpty(jumpText))
                {
                    jumpText = "Go";
                }
                return jumpText;
            }
            set { jumpText = value; }
        }


        #endregion

        #region 页码变化触发事件

        public event EventHandler currentPageChanged;

        #endregion

        #region 分页及相关事件功能实现

        private void SetFormCtrEnabled()
        {
            this.lnkFirst.Enabled = true;
            this.lnkPrev.Enabled = true;
            this.lnkNext.Enabled = true;
            this.lnkLast.Enabled = true;
            this.btnGo.Enabled = true;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <returns></returns>
        private int GetPageCount()
        {
            if (RecordsPerPage == 0)
            {
                return 0;
                //throw new DivideByZeroException("每页记录数为0");
            }
            int pageCount = TotalCount / RecordsPerPage;
            if (TotalCount % RecordsPerPage == 0)
            {
                pageCount = TotalCount / RecordsPerPage;
            }
            else
            {
                pageCount = TotalCount / RecordsPerPage + 1;
            }
            return pageCount;
        }
        public void DrawControl()
        {
            DrawControl(true);
        }

        /// <summary>
        /// 页面控件呈现
        /// </summary>
        public void DrawControl(bool isChange)
        {
            this.btnGo.Text = this.JumpText;
            //lblCurrentPage.Text = string.Format("{0}/{1}  共 {2} 条记录，每页 {3} 条", CurrentPage.ToString(),
            //    this.PageCount.ToString(), TotalCount.ToString(), RecordsPerPage.ToString());
            this.lblCurrentPage.Text = CurrentPage.ToString();
            this.lblPageCount.Text = PageCount.ToString();
            this.lblTotalCount.Text = TotalCount.ToString();
            this.lblRecPerPg.Text = RecordsPerPage.ToString();

            if (isChange&&currentPageChanged != null)
            {
                currentPageChanged(this, null);//当前分页数字改变时，触发委托事件
            }
            SetFormCtrEnabled();
            if (PageCount == 1)//有且仅有一页
            {
                this.lnkFirst.Enabled = false;
                this.lnkPrev.Enabled = false;
                this.lnkNext.Enabled = false;
                this.lnkLast.Enabled = false;
                this.btnGo.Enabled = false;
            }
            else if (CurrentPage == 1)//第一页
            {
                this.lnkFirst.Enabled = false;
                this.lnkPrev.Enabled = false;
                //this.lnkFirst.ForeColor = Color.Gray;
                //this.lnkPrev.ForeColor = Color.Gray;
            }
            else if (CurrentPage == this.PageCount)//最后一页
            {
                this.lnkNext.Enabled = false;
                this.lnkLast.Enabled = false;
            }
        }

        #endregion

        #region 相关控件事件

        private void lnkFirst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentPage = 1;
            DrawControl();
        }

        private void lnkPrev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentPage = Math.Max(1, CurrentPage - 1);
            DrawControl();
        }

        private void lnkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentPage = Math.Min(PageCount, CurrentPage + 1);
            DrawControl();
        }

        private void lnkLast_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CurrentPage = PageCount;
            DrawControl();
        }

        /// <summary>
        /// enter键功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strNum = this.txtPageNum.Text.Trim();
            if (e.KeyChar == (char)Keys.Enter && IsPositiveNumber(strNum))
            {
                CurrentPage = int.Parse(strNum);
                DrawControl();
            }
        }

        /// <summary>
        /// 页数限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            string strNum = this.txtPageNum.Text.Trim();
            if (strNum.Length > 0 && IsPositiveNumber(strNum) && int.Parse(strNum) > PageCount)
            {
                txtPageNum.Text = PageCount.ToString();
            }
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            string strNum = this.txtPageNum.Text.Trim();
            if (IsPositiveNumber(strNum) == false)
            {
                return;
            }
            CurrentPage = int.Parse(strNum);
            DrawControl();
        }

        #endregion
        public static Regex regPosNum = new Regex(@"^[0-9]*[1-9][0-9]*$");

        /// <summary>
        /// 是否是正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPositiveNumber(string input)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(input) == false)
            {
                flag = regPosNum.IsMatch(input.Trim());
            }
            return flag;
        }

    }
}
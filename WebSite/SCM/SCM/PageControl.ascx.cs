using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SCM.Web
{
    public partial class PageControl : System.Web.UI.UserControl
    {        
        //设置条数数
        private int pageCount = 1;
        public int PageCount
        {
            get
            {   //先从ViewState中读取总页数，如果有总会页数的值，则将值返回，否则认为是一页
                if (ViewState["PageCount"] != null)
                {
                    return Convert.ToInt32(ViewState["PageCount"]);
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (pageCount != value)
                {
                    pageCount = value;
                    ViewState["PageCount"] = pageCount;
                    ChangePage(currentPage);
                }
            }
        }
        //当前页数
        private int currentPage = 1;
        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] != null)
                {
                    return Convert.ToInt32(ViewState["CurrentPage"]);
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    //将当前页数保存在 ViewState["CurrentPage"]
                    ViewState["CurrentPage"] = currentPage;
                    //设置按钮启用
                    ChangePage(currentPage);
                }
            }
        }
        //总记录数
        private int recorderCount = 1;
        public int RecorderCount
        {
            get { return recorderCount; }
            set
            {
                recorderCount = value;
                PageCount = (recorderCount + PageSize - 1) / PageSize;
            }
        }
        //每页数量
        private int pageSize = 1;
        public int PageSize
        {
            get
            {
                if (ViewState["PageSize"] != null)
                {
                    return Convert.ToInt32(ViewState["PageSize"]);
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                if (pageSize != value)
                {
                    pageSize = value;
                    //将总页数保存在ViewState["PageSize"] 中
                    ViewState["PageSize"] = pageSize;
                    ChangePage(currentPage);
                }
            }
        }

        /// <summary>
        /// 添加图层之前
        /// </summary>
        //用委托添加添加ChangePage委托
        public delegate void PageChangedEventHandler(object sender, int e);
        //用委托改ChangePage添加委托
        public event PageChangedEventHandler PageChanged;
        protected virtual void OnPageChanged(int e)
        {
            //如果存在事件响应，则将当前点击的事件和页数传递
            if (PageChanged != null)
            {
                PageChanged(this, e);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //ViewState["CurrentPage"] = 1;
                //ViewState["PageCount"] = 1;
            }
            else
            {
                currentPage = Convert.ToInt32(ViewState["CurrentPage"]);
                pageCount = Convert.ToInt32(ViewState["PageCount"]);
                pageSize = Convert.ToInt32(ViewState["PageSize"]);
            }
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            int iTmpCurrent = 1;
            LinkButton myLinkButton = (LinkButton)sender;
            if (myLinkButton.CommandName == "First")
            {
                iTmpCurrent = 1;
            }
            else if (myLinkButton.CommandName == "Previous")
            {
                iTmpCurrent = currentPage - 1;
            }
            else if (myLinkButton.CommandName == "Next")
            {
                iTmpCurrent = currentPage + 1;
            }
            else if (myLinkButton.CommandName == "Last")
            {
                iTmpCurrent = pageCount;
            }
            else if (myLinkButton.CommandName == "Goto")
            {
                int iGoto = 1;
                if (int.TryParse(this.txtCurrentPage.Text, out iGoto))
                {
                    if (iGoto <= 1)
                    {
                        iGoto = 1;
                    }
                    if (iGoto > pageCount)
                    {
                        iGoto = pageCount;
                    }
                    iTmpCurrent = iGoto;
                }
                else
                {
                    iTmpCurrent = currentPage;
                }
            }
            //iTmpCurrent要跳转的页数
            ChangePage(iTmpCurrent);
            //currentPage当前页数，点击事件触发
            OnPageChanged(currentPage);
        }

        //改变页数方法
        private void ChangePage(int page)
        {
            currentPage = page;
            //加载热点商品推荐
            this.btnGoto.Enabled = true;
            if (page <= 1)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = true;
                this.btnLast.Enabled = true;
                currentPage = 1;
            }
            else if (page >= pageCount)
            {
                this.btnFirst.Enabled = true;
                this.btnPrev.Enabled = true;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                currentPage = pageCount;
            }
            else
            {
                this.btnFirst.Enabled = true;
                this.btnPrev.Enabled = true;
                this.btnNext.Enabled = true;
                this.btnLast.Enabled = true;
            }
            if (pageCount <= 1)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                this.btnGoto.Enabled = false;
            }
            //currentPage.ToString()当前页数。pageCount.ToString();总页数
            this.lblCurrentPage.Text = currentPage.ToString();
            this.lblTotalPage.Text   = pageCount.ToString();
            this.txtCurrentPage.Text = currentPage.ToString();
            //将当前的页数保存在ViewState中
            ViewState["CurrentPage"] = currentPage;
        }
    }//end class
}

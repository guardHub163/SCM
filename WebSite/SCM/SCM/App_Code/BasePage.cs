using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SCM.Model;
using System.Collections;
using log4net;

namespace SCM.Web
{
    /// <summary>
    ///BasePage 的摘要说明
    /// </summary>    
    abstract public class BasePage : System.Web.UI.Page
    {
        protected int PageSize = 16;
        protected BaseUserTable _userTable = null;
        protected ILog _log = null;

        protected BaseUserTable UserTable
        {
            get
            {
                _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];

                if (this._userTable == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "parent.document.location.href='../../TimeOut.aspx?Flag=Parent';", true);
                }
                return _userTable;
            }
            set { _userTable = value; }
        }

        /// <summary>
        /// OnInit
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new System.EventHandler(BasePage_Load);
            this.Error += new System.EventHandler(BasePage_Error);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        private void BasePage_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];

                    if (this._userTable == null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "parent.document.location.href='../../TimeOut.aspx?Flag=Parent';", true);
                        return;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 错误处理 
        /// </summary>
        protected void BasePage_Error(object sender, System.EventArgs e)
        {
           
            Exception currentError = HttpContext.Current.Server.GetLastError();
            try
            {
                _log.Error(DateTime.Now.ToString()+": "+currentError.ToString());
            }
            catch { };
            Response.Redirect("~/Error.aspx?ErrorMessage=" + currentError.Message.ToString(), true);
            Server.ClearError();
        }

        /// <summary>
        /// 访问权限验证
        /// </summary>
        protected void ValidateRole(string str)
        {
            string url = "";
            try
            {
                Hashtable ht = (Hashtable)HttpContext.Current.Session["UserPage"];
                if (ht == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "parent.document.location.href='../../TimeOut.aspx?Flag=Parent';", true);
                }
                else
                {
                    if (ht[str] == null)
                    {
                        try
                        {
                            _log.Info(DateTime.Now.ToString()+": "+_userTable.USER_ID+"|"+_userTable.TRUE_NAME+" 权限不足!");
                        }
                        catch { };
                        url = "~/NoRole.aspx?Flag=Parent";
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    _log.Error(DateTime.Now.ToString() + ": " + ex.ToString());
                }
                catch { };
                url = "~/Error.aspx?Flag=Parent&ErrorMessage=" + ex.Message.ToString();
            }
            if (url != "")
            {
                Response.Redirect(url, true);
            }
        }

        /// <summary>
        /// Session验证
        /// </summary>
        protected bool ValidateSession()
        {
            try
            {
                _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
                if (this._userTable == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "parent.document.location.href='../../TimeOut.aspx?Flag=Parent';", true);
                    return false;
                }
            }
            catch { }
            return true;
        }

        /// <summary>
        /// 页面点击事件
        /// </summary>
        protected void processClick(object sender, EventArgs e)
        {
            try
            {
                _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
                if (this._userTable == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "parent.document.location.href='../../TimeOut.aspx?Flag=Parent';", true);
                    return;
                }
            }
            catch { }

            string btnId = "";
            if (sender.GetType().Name == "Button")
            {
                Button button = (Button)sender;
                btnId = button.ID;
            }
            else if (sender.GetType().Name == "LinkButton")
            {
                LinkButton linkButton = (LinkButton)sender;
                btnId = linkButton.ID;
            }
            else if (sender.GetType().Name == "Image")
            {
                Image image = (Image)sender;
                btnId = image.ID;
            }
            processBtnClick(btnId, sender, e);
        }

        abstract protected bool processBtnClick(string btnId, object sender, EventArgs e);
    }//end class
}

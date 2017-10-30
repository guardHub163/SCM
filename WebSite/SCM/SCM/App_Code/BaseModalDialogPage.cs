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
    ///BP 的摘要说明
    /// </summary>
    abstract public class BaseModalDialogPage : System.Web.UI.Page
    {
        protected int PageSize = 16;
        private BaseUserTable _userTable = null;
        protected ILog _log = null;

        protected BaseUserTable UserTable
        {
            get
            {
                try
                {
                    _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
                }
                catch (Exception ex)
                { 
                    try
                    {
                        _log.Error("BaseModalDialogPage", ex);
                    }
                    catch { }
                    _userTable = null;
                }

                if (this._userTable == null)
                {
                    Response.Redirect("~/TimeOut.aspx?Flag=Child", true);
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
            this.Error += new System.EventHandler(BasePage_Error);
        }

        /// <summary>
        /// 错误处理 
        /// </summary>
        protected void BasePage_Error(object sender, System.EventArgs e)
        {
            Exception currentError = HttpContext.Current.Server.GetLastError();
            try
            {
                _log.Error(DateTime.Now.ToString() + ": " + currentError.ToString());
            }
            catch { };
            Response.Redirect("~/Error.aspx?Flag=Child&ErrorMessage=" + currentError.Message.ToString());
            Server.ClearError();
        }

        /// <summary>
        /// 访问权限验证
        /// </summary>
        /// <param name="str"></param>
        protected void ValidateRole(string str)
        {
            string url = "";
            try
            {
                Hashtable ht = (Hashtable)HttpContext.Current.Session["UserPage"];
                if (ht == null)
                {
                    Response.Redirect("~/TimeOut.aspx?Flag=Child", true);
                }
                else
                {
                    if (ht[str] == null)
                    {
                        try
                        {
                            _log.Info(DateTime.Now.ToString() + ": " + _userTable.USER_ID + "|" + _userTable.TRUE_NAME + " 权限不足!");
                        }
                        catch { };
                        url = "~/NoRole.aspx?Flag=Child";
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
                url = "~/Error.aspx?Flag=Child&ErrorMessage=" + ex.Message.ToString();
            }
            if (url != "")
            {
                Response.Redirect(url, true);
            }
        }

        /// <summary>
        /// 页面点击事件
        /// </summary>
        protected void processClick(object sender, EventArgs e)
        {
            //CHECK SESSION
            BaseUserTable user= this.UserTable;

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

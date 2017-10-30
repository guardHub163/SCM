
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
using SCM.Bll;
using SCM.Common;
using log4net;
using System.Reflection;
using SCM.Model;
namespace SCM.Web
{
    public partial class Login : System.Web.UI.Page
    {
        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        BUser bUser = new BUser();
        protected void Page_Load(object sender, EventArgs e)
        {             
            //ILog log = log4net.LogManager.GetLogger("logger");  
            log.Info("警告：Info");
            log.Debug("警告：Debug");
            log.Warn("警告：Warn");                                                                                 
            log.Error("警告：Error");
            log.Fatal("警告：Fatal");
 
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.btnLogin_Click);
            this.btnLogin.Attributes.Add("onMouseOver", "this.src='Images/loginOver.png'");
            this.btnLogin.Attributes.Add("onMouseOut", "this.src='Images/login.png'");
        }
        #endregion

        private void btnLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
            {
                int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                if (PassErroeCount > 3)
                {
                    txtUsername.Disabled = true;
                    txtPass.Disabled = true;
                    btnLogin.Enabled = false;
                    MessageBox.Show(this, "对不起，你错误登录了三次，系统登录锁定！");
                    return;
                }

            }
            string userName = SCM.Common.PageValidate.InputText(txtUsername.Value.Trim(), 30);
            string Password = SCM.Common.PageValidate.InputText(txtPass.Value.Trim(), 30);

            BaseUserTable currentUser = bUser.ValidateLogin(userName, Password);
            if (currentUser == null)
            {
                MessageBox.Show(this, "登陆失败： " + userName);
                if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
                {
                    int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                    Session["PassErrorCountAdmin"] = PassErroeCount + 1;
                }
                else
                {
                    Session["PassErrorCountAdmin"] = 1;
                }
            }
            else
            {

                FormsAuthentication.SetAuthCookie(userName, false);
                //日志
                //UserLog.AddLog(currentUser.UserName, currentUser.UserType, Request.UserHostAddress, Request.Url.AbsoluteUri, "登录成功");

                Session["UserInfo"] = currentUser;
                Session["Style"] = currentUser.STYLE;
                try
                {
                    DataSet ds = bUser.GetUserPageRole(currentUser.USER_TYPE);
                    Hashtable ht = new Hashtable();
                    foreach (DataRow row in ds.Tables[0].Rows) 
                    {
                        ht.Add(row["NAME_SPACE"],row);
                    }
                    Session["UserPage"] = ht;
                }
                catch(Exception ex) { }

                if (Session["returnPage"] != null)
                {
                    string returnpage = Session["returnPage"].ToString();
                    Session["returnPage"] = null;
                    Response.Redirect(returnpage);
                }
                else
                {
                    if (this.chkPos.Checked)
                    {
                        Response.Redirect("PosIndex.aspx");
                    }
                    else 
                    {
                        Response.Redirect("Index.aspx");
                    }
                }
            }
        }

        
}//end class
}

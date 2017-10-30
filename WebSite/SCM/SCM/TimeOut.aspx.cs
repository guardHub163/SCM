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
    public partial class TimeOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                try
                {
                    if ("Parent".Equals(Request.QueryString["Flag"]))
                    {
                        btnReLogin.Visible = true;
                        btnReLogin.Enabled = true;
                    }
                }
                catch { }
            }
            btnClose.Attributes.Add("onclick", "window.opener=null;window.close();");
        }

        protected void process_login(object sender, EventArgs e) 
        {
            Response.Redirect("~/Login.aspx");
        }
   
}//end class
}

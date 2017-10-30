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
    public partial class LeadingErrorInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string message = HttpContext.Current.Session["ERROR_INFO"].ToString();
                string info="";
                string[] Arry = message.Split('。');
                for (int i = 0; i < Arry.Length; i++) 
                {
                   info+= ""+Arry[i] + "</br>";
                }
                this.error.InnerHtml = info;
                HttpContext.Current.Session.Remove("ERROR_INFO");
            }
        }
    }
}

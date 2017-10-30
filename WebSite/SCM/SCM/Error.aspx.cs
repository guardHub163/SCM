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
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if ("Child".Equals(Request.QueryString["Flag"]))
                    {
                        btnClose.Visible = true;
                        btnClose.Enabled = true;
                    }
                }
                catch { }

                try
                {
                    errorMessage.Text = Request.QueryString["ErrorMessage"];
                }
                catch { }
            }
            btnClose.Attributes.Add("onclick", "window.opener=null;window.close();");
        }
    }//end class
}

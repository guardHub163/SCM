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
using SCM.Model;
using System.Reflection;
using log4net;

namespace SCM.Web.Color
{
    public partial class Show : BaseModalDialogPage
    {
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    string CODE = Request.Params["code"];
                    Showinfo(CODE);
                }
            }
            //HttpContext.Current.Session["UserInfo"]=null;
        }

        private void Showinfo(string CODE)
        {
            BColor bll = new BColor();
            BaseColorTable colortable = bll.GetModel(CODE);
            this.lblCode.Text = colortable.CODE;
            this.lblName.Text = colortable.NAME;
            this.lblAttribute1.Text = colortable.ATTRIBUTE1;
            this.lblAttribute2.Text = colortable.ATTRIBUTE2;
            this.lblAttribute3.Text = colortable.ATTRIBUTE3;
            this.lblCreate_user.Text = colortable.Create_name;
            this.lblCreate_date_time.Text = colortable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = colortable.Update_name;
            this.lblLast_update_time.Text = colortable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}

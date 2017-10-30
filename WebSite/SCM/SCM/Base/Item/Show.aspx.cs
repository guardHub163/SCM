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
using log4net;
using System.Reflection;

namespace SCM.Web.Item
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
        }

        private void Showinfo(string CODE)
        {
            BItem bll = new BItem();
            BaseItemTable Ptable = bll.GetModel(CODE);
            this.lblCode.Text = Ptable.CODE;
            this.lblName.Text = Ptable.NAME;
            this.lblSpec.Text = Ptable.SPEC;
            this.lblUnit_name.Text = Ptable.Unit_name;
            this.lblAttribute1.Text = Ptable.ATTRIBUTE1;
            this.lblAttribute2.Text = Ptable.ATTRIBUTE2;
            this.lblAttribute3.Text = Ptable.ATTRIBUTE3;
            this.lblAttribute4.Text = Ptable.ATTRIBUTE4;
            this.lblAttribute5.Text = Ptable.ATTRIBUTE5;
            this.lblCreate_user.Text = Ptable.Creat_name;
            this.lblLast_update_user.Text = Ptable.Update_name;
            this.lblCreate_date_time.Text = Ptable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_time.Text = Ptable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");

        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}

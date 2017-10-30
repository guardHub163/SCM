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

namespace SCM.Web.Unit
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
            BUnit bll = new BUnit();
            BaseUnitTable unitTable = bll.GetModel(CODE);
            this.lblCode.Text = unitTable.CODE;
            this.lblName.Text = unitTable.NAME;
            this.lblAttribute1.Text = unitTable.ATTRIBUTE1;
            this.lblAttribute2.Text = unitTable.ATTRIBUTE2;
            this.lblAttribute3.Text = unitTable.ATTRIBUTE3;
            this.lblCreate_date_time.Text = unitTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblCreate_user.Text = unitTable.Create_name;
            this.lblLast_update_time.Text = unitTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = unitTable.Update_name;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}

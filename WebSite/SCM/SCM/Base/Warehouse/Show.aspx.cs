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

namespace SCM.Web.Warehouse
{
    public partial class Show : BaseModalDialogPage
    {
        string str = "";
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            this.selType.Disabled = true;
            if (!Page.IsPostBack)
            {
                if (Request.Params["code"] != null && Request.Params["code"].Trim() != "")
                {
                    str = Request.Params["code"];
                    string CODE = str;
                    Showinfo(CODE);
                }
            }
        }
        private void Showinfo(string CODE)
        {
            BWarehouse bll = new BWarehouse();
            BaseWarehouseTable houseTable = bll.GetModel(CODE);
            this.lblCode.Text = houseTable.CODE;
            this.lblName.Text = houseTable.NAME;
            // this.lblDerartment_code.Text = houseTable.DEPARTMENT_CODE;
            this.lblDerartment_code.Text = houseTable.DEPARTMENT_NAME;
            //this.lblCreate_user.Text = houseTable.CREATE_USER;
            this.lblCreate_user.Text = houseTable.CREATE_USER_NAME;
            this.lblLast_update_user.Text = houseTable.LAST_UPDATE_USER_NAME;
            this.lblCreate_date_time.Text = houseTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            //this.lblLast_update_user.Text = houseTable.LAST_UPDATE_USER;
            this.lblLast_update_time.Text = houseTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
            this.selType.Value = houseTable.TYPE.ToString();
            this.lblAttribute1.Text = houseTable.ATTRIBUTE1;
            this.lblAttribute2.Text = houseTable.ATTRIBUTE2;
            this.lblAttribute3.Text = houseTable.ATTRIBUTE3;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}
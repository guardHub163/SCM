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

namespace SCM.Web.VipCustomer
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
            BVipCustomer bll = new BVipCustomer();
            BaseVipCustomerTable VipTable = bll.GetModel(CODE);
            this.lblCode.Text = VipTable.CODE;
            this.lblName.Text = VipTable.NAME;
            this.LblLevel.Text = VipTable.VIP_LEVEL.ToString();
            this.lblAdress.Text = VipTable.ADDRESS;
            this.LblQQ.Text = VipTable.QQ;
            this.LblWw.Text = VipTable.WW;
            this.LblEmail.Text = VipTable.EMAIL;
            this.lblDepartment.Text = VipTable.Department;
            this.LblSalesTime.Text = VipTable.LAST_SALES_DATE.ToString("yyyy/MM/dd");
            this.LblBirth.Text = VipTable.BIRTH_DATE.ToString("yyyy/MM/dd");
            this.LblDiscount.Text = VipTable.DISCOUNT_RATE.ToString();
            this.LblPoints.Text = VipTable.POINTS.ToString();
            this.lblAttribute1.Text = VipTable.ATTRIBUTE1;
            this.lblAttribute2.Text = VipTable.ATTRIBUTE2;
            this.lblAttribute3.Text = VipTable.ATTRIBUTE3;
            this.lblCreate_date_time.Text = VipTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblCreate_user.Text = VipTable.Creat_name;
            this.lblLast_update_time.Text = VipTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = VipTable.Update_name;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}
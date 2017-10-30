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
using SCM.Model.Base;
using log4net;
using System.Reflection;

namespace SCM.Web.Supplier
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
            BSupplier bll = new BSupplier();
            BaseSupplierTable supplierTable = bll.GetModel(CODE);
            this.lblCode.Text = supplierTable.CODE;
            this.lblName.Text = supplierTable.NAME;
            this.lblWarehouse_name.Text = supplierTable.Warehouse_name;
            this.lblName_short.Text = supplierTable.NAME_SHORT;
            this.lblAddress.Text = supplierTable.ADDRESS;
            this.lblPost_code.Text = supplierTable.POST_CODE;
            this.lblTel.Text = supplierTable.TEL;
            //this.lblType.Text =Convert.ToString( supplierTable.TYPE);
            this.lblType.Text = supplierTable.Typenama;
            this.lblFax.Text = supplierTable.FAX;
            this.lblContact.Text = supplierTable.CONTACT;
            this.lblEmail.Text = supplierTable.EMAIL;
            this.lblCreate_user.Text = supplierTable.Creat_name;
            this.lblCreate_date_time.Text = supplierTable.CREATE_DATE_TIME.ToString("yyyy/MM/dd");
            this.lblLast_update_user.Text = supplierTable.Last_creat_name;
            this.lblLast_update_time.Text = supplierTable.LAST_UPDATE_TIME.ToString("yyyy/MM/dd");
            this.lblAttribute1.Text = supplierTable.ATTRIBUTE1;
            this.lblAttribute2.Text = supplierTable.ATTRIBUTE2;
            this.lblAttribute3.Text = supplierTable.ATTRIBUTE3;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return true;
        }
    }
}

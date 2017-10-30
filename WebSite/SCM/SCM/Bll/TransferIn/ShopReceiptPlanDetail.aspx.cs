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
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.TransferIn
{
    public partial class ShopReceiptPlanDetail : BaseModalDialogPage
    {
        BTransferPlan bll = new BTransferPlan();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
              base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                if (Request.Params["SN"] != null && Request.Params["SN"].Trim() != "")
                {
                    decimal SlipNumber = Convert.ToDecimal(Request.Params["SN"]);
                    showInfo(SlipNumber);
                }
            }
        }

        private void showInfo(decimal SlipNumber)
        {
            BllTransferInPlanTable btable = bll.GetModel(SlipNumber);
            this.txtSlipNumber.Text = btable.SLIP_NUMBER.ToString();
            this.lblPurchaseSlipNumber.Text = btable.SHIPMENT_SLIP_NUMBER;
            this.lblToWarehouseName.Text = btable.SHOP_NAME;
            this.lblFromWarehouseName.Text = btable.WAREHOUSE_NAME;
            this.lblDepartureDate.Text = btable.DEPARTUAL_DATE.ToString("yyyy/MM/dd");
            this.lblArrivalDate.Text = btable.ARRIVAL_DATE.ToString("yyyy/MM/dd");
            this.lblProductCode.Text = btable.PRODUCT_CODE;
            this.lblProductName.Text = btable.PRODUCT_NAME;
            this.lblUnitName.Text = btable.UNIT_NAME;
            this.txtOldQuantity.Text = btable.QUANTITY.ToString();
            this.txtQuantity.Text = btable.QUANTITY.ToString();
            this.lblFromWarehousecode.Text = btable.FROM_WAREHOUSE_CODE;
            this.lblToWarehousecode.Text = btable.TO_WAREHOUSE_CODE;
            this.lblUnitcode.Text = btable.UNIT_CODE;

        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave":
                    Save();
                    break;
            }
            return true;
        }

        private void Save()
        {
            string message = "";
            if (this.txtQuantity.Text.Trim().Length == 0)
            {
                message += "交货数量不能为空！";
            }
            //if (Convert.ToDecimal(this.txtOldQuantity.Text) < Convert.ToDecimal(this.txtQuantity.Text))
            //{
            //    message += "实际到货数量不能大于预定数量！";
            //}
            BllTransferInPlanTable btable = new BllTransferInPlanTable();
            btable.SLIP_NUMBER = Convert.ToDecimal(this.txtSlipNumber.Text.Trim());
            btable.ARRIVAL_DATE = Convert.ToDateTime(this.lblArrivalDate.Text.Trim());
            btable.FROM_WAREHOUSE_CODE = this.lblFromWarehousecode.Text.Trim();
            btable.TO_WAREHOUSE_CODE = this.lblToWarehousecode.Text.Trim();
            btable.PRODUCT_CODE = this.lblProductCode.Text.Trim();
            btable.UNIT_CODE = this.lblUnitcode.Text.Trim();
            btable.TRANSFERQUANTITY = Convert.ToDecimal(this.txtQuantity.Text.Trim());
            btable.QUANTITY = Convert.ToDecimal(this.txtOldQuantity.Text.Trim());
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                btable.CREATE_USER = userTable.USER_ID;
                btable.LAST_UPDATE_USER = userTable.USER_ID;
            }
            catch { }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }
            if (bll.GetTransferInfo(btable) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"入库成功！\");processCloseAndRefreshParent();", true);
            }
        }
    }
}
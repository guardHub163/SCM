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
using System.Collections.Generic;
using SCM.Common;
using System.Reflection;
using log4net;
namespace SCM.Web.TransferIn
{
    public partial class DeliveryAnswerModify : BaseModalDialogPage
    {
        BReceivingPlan bll = new BReceivingPlan();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                object slipNumber = Request.Params["SN"];
                if (slipNumber != null && slipNumber.ToString() != "")
                {
                    BllReceivingPlanTable receivingPlanTable = bll.getSearchViewMode(Convert.ToDecimal(slipNumber));
                    if (receivingPlanTable != null)
                    {
                        txtSlipNumber.Text = slipNumber.ToString();
                        lblPurchaseSlipNumber.Text = receivingPlanTable.PURCHASE_SLIP_NUMBER;
                        lblInputType.Text = receivingPlanTable.INPUT_TYPE_NAME;
                        lblWarehouseName.Text = receivingPlanTable.WAREHOUSE_NAME;
                        lblDepartureDate.Text = receivingPlanTable.DEPARTUAL_DATE.ToString("yyyy/MM/dd");
                        txtStockFromDate.Text = receivingPlanTable.ARRIVAL_DATE.ToString("yyyy/MM/dd");
                        lblProductName.Text = receivingPlanTable.PRODUCT_NAME;
                        lblUnitName.Text = receivingPlanTable.UNIT_NAME;
                        txtOldQuantity.Text = receivingPlanTable.QUANTITY.ToString();
                        txtQuantity.Text = receivingPlanTable.QUANTITY.ToString();
                        this.txtNewArrivalDate.Enabled = false;
                    }
                    else
                    {
                        throw new Exception("订单明细不存在，该明细己删除或入库！");
                    }
                }
                else
                {
                    throw new Exception("未知异常！");
                }
            }

        }

        protected void Rdo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo1.Checked)
            {
                this.txtNewArrivalDate.Text = "";
                this.txtNewArrivalDate.Enabled = false;
                this.txtNewQuantity.Text = "";
            }
            else
            {
                this.txtNewArrivalDate.Enabled = true;
                this.txtNewArrivalDate.Text = this.txtStockFromDate.Text;
                try
                {
                    this.txtNewQuantity.Text = (Convert.ToDecimal(this.txtOldQuantity.Text) - Convert.ToDecimal(this.txtQuantity.Text)).ToString();
                }
                catch { }
            }
        }

        protected void Quantity_Changed(object sender, EventArgs e)
        {
            string message = "";
            try
            {
                decimal quantity = Convert.ToDecimal(this.txtQuantity.Text);
                decimal oldQuantity = Convert.ToDecimal(this.txtOldQuantity.Text);
                if (quantity > oldQuantity)
                {
                    message += "入库数量不能大于预定数量!\\n";
                }
                else if (quantity <= 0)
                {
                    message += "入库数量不能为负数或零!\\n";
                }
                else if (quantity < oldQuantity)
                {
                    this.rdo2.Checked = true;
                    this.txtNewArrivalDate.Enabled = true;
                    this.txtNewArrivalDate.Text = this.txtStockFromDate.Text;
                    this.txtNewQuantity.Text = (oldQuantity - quantity).ToString();
                }
                else
                {
                    this.rdo1.Checked = true;
                    this.txtNewArrivalDate.Text = "";
                    this.txtNewArrivalDate.Enabled = false;
                    this.txtNewQuantity.Text = "";
                }
            }
            catch (FormatException ex)
            {
                message += "交货数量输入格式错误\\n";
            }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");document.getElementById('" + txtQuantity.ClientID + "').value='" + this.txtOldQuantity.Text + "';", true);
            }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave":
                    Save(sender, e);
                    break;
            }
            return true;
        }

        private void Save(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }
            List<BllReceivingPlanTable> list = new List<BllReceivingPlanTable>();

            BllReceivingPlanTable rp = new BllReceivingPlanTable();
            rp.SLIP_NUMBER = Convert.ToDecimal(this.txtSlipNumber.Text);
            rp.ARRIVAL_DATE = Convert.ToDateTime(txtStockFromDate.Text);
            rp.QUANTITY = Convert.ToDecimal(txtQuantity.Text);
            rp.LAST_UPDATE_USER = UserTable.USER_ID;
            list.Add(rp);

            if (this.rdo2.Checked)
            {
                rp = new BllReceivingPlanTable();
                rp.ARRIVAL_DATE = Convert.ToDateTime(txtNewArrivalDate.Text);
                rp.QUANTITY = Convert.ToDecimal(txtNewQuantity.Text);
                rp.CREATE_USER = UserTable.USER_ID;
                rp.LAST_UPDATE_USER = rp.CREATE_USER;
                list.Add(rp);
            }

            if (bll.Insert(list))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"修改成功！\");processCloseAndRefreshParent();", true);
            }
        }

        private bool CheckInput()
        {
            string message = "";
            if (this.txtStockFromDate.Text.Trim() == "")
            {
                message += "交货预定日不能为空!\\n";
            }
            if (this.txtQuantity.Text.Trim() == "")
            {
                message += "交货数量不能为空!\\n";
            }
            else
            {
                try
                {
                    decimal quantity = Convert.ToDecimal(this.txtQuantity.Text.Trim());
                    decimal oldQuantity = Convert.ToDecimal(this.txtOldQuantity.Text.Trim());
                    if (quantity <= 0)
                    {
                        message += "交货数量必须大于零!\\n";
                    }
                    else if (quantity > oldQuantity)
                    {
                        message += "交货数量不能大于采购数量！\\n";
                    }

                }
                catch 
                {
                    message += "交货数量格式输入错误!\\n";
                }
            }
            if (this.rdo2.Checked)
            {
                if (this.txtNewArrivalDate.Text.Trim() == "")
                {
                    message += "二期交货预定日不能为空!\\n";
                }

                
                if (Convert.ToDecimal(this.txtNewQuantity.Text.Trim()) <= 0)
                {
                    message += "二期交货数量必须大于零!\\n";
                }
            }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return false;
            }
            return true;
        }

        protected void StockFromDate_Changed(object sender, EventArgs e)
        {
            if (txtStockFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtStockFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtStockFromDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtStockFromDate.Text = Convert.ToDateTime(txtStockFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtStockFromDate.Text.Trim() != "" && this.lblDepartureDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtStockFromDate.Text) < Convert.ToDateTime(lblDepartureDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"采购日期不能大于交货预定日!\");document.getElementById('" + txtStockFromDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
        protected void NewArrivalDate_Changed(object sender, EventArgs e)
        {
            if (txtNewArrivalDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtNewArrivalDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtNewArrivalDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtNewArrivalDate.Text = Convert.ToDateTime(txtNewArrivalDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
    }
}

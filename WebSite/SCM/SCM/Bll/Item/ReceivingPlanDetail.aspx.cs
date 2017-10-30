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
using SCM.Model;
using SCM.Bll;
using System.Collections.Generic;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.Item
{
    public partial class ReceivingPlanDetail : BaseModalDialogPage
    {
        BReceivingPlan bll = new BReceivingPlan();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {

                this.txtNewArrivalDate.Enabled = false;
                this.lblArrivalDate.Attributes.Add("onClick", "WdatePicker();");
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
                        lblArrivalDate.Text = receivingPlanTable.ARRIVAL_DATE.ToString("yyyy/MM/dd");
                        lblProductCode.Text = receivingPlanTable.PRODUCT_CODE;
                        lblProductName.Text = receivingPlanTable.PRODUCT_NAME;
                        lblUnitName.Text = receivingPlanTable.UNIT_NAME;
                        txtOldQuantity.Text = receivingPlanTable.QUANTITY.ToString();
                        txtQuantity.Text = receivingPlanTable.QUANTITY.ToString();
                        txtReQuantity.Text = "0";
                    }
                    else
                    {
                        throw new Exception("订单明细不存在，该明细己删除或入库!");
                    }
                }
                else
                {
                    throw new Exception("未知异常!");
                }

                ddlReason.DataSource = bCommon.GetNames("RECEIPT_RETURN").Tables[0];
                ddlReason.DataTextField = "NAME"; //dropdownlist的Text的字段 
                ddlReason.DataValueField = "CODE";//dropdownlist的Value的字段 
                ddlReason.DataBind();
            }
        }

        //返品处理单选按钮事件
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
                this.txtNewArrivalDate.Text = this.lblArrivalDate.Text;
                try
                {
                    this.txtNewQuantity.Text = this.txtReQuantity.Text;
                }
                catch { }
            }
        }

        //入库数量CHANGED事件
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
                    this.txtReQuantity.Text = (oldQuantity - quantity).ToString();
                    if (this.rdo2.Checked)
                    {
                        this.txtNewQuantity.Text = this.txtReQuantity.Text;
                    }
                    this.rdo1.Enabled = true;
                    this.rdo2.Enabled = true;
                    this.btnNew.Enabled = true;
                    this.ddlReason.Enabled = true;
                    this.txtQ.Enabled = true;
                }
                else
                {
                    this.txtReQuantity.Text = "";
                    this.rdo1.Checked = true;
                    this.rdo1.Enabled = false;
                    this.rdo2.Enabled = false;
                    this.txtNewArrivalDate.Text = "";
                    this.txtNewArrivalDate.Enabled = false;
                    this.txtNewQuantity.Text = "";
                    this.btnNew.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.ddlReason.Enabled = false;
                    this.txtQ.Enabled = false;
                    this.lbReason.Items.Clear();
                }
            }
            catch (FormatException ex)
            {
                message += "入库数量输入格式错误!\\n";                
            }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");document.getElementById('" + txtQuantity.ClientID + "').value='" + this.txtOldQuantity.Text + "';", true);
            }
        }

        //页面点击事件
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave":
                    Save();
                    break;
                case "btnNew":
                    AddReason();
                    break;
                case "btnDelete":
                    DeleteReason();
                    break;
            }
            return true;
        }

        //返品原因追加
        private void AddReason()
        {
            string message = "";
            if (this.txtQ.Text == "")
            {
                message += "返品原因数量不能为空!\\n";
            }
            else
            {
                try
                {
                    decimal quantity = Convert.ToDecimal(this.txtQ.Text);
                    if (quantity <= 0)
                    {
                        message += "返品原因数量不能小于等于零!\\n";
                    }
                }
                catch (FormatException)
                {
                    message += "返品原因数量输入格式错误!\\n";
                }
            }
            ListItem ddl = this.ddlReason.SelectedItem;
            foreach (ListItem lb in this.lbReason.Items)
            {
                if (ddl.Value == lb.Value)
                {
                    message += "返品原因己经存在!\\n";
                }
            }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return;
            }

            this.lbReason.Items.Add(new ListItem(this.txtQ.Text + "|" + ddl.Text, ddl.Value));
            this.btnDelete.Enabled = true;
            this.txtQ.Text = "";
        }

        //返品原因删除
        private void DeleteReason()
        {
            if (this.lbReason.SelectedItem == null)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"没有选中任何项!\");", true);
            }
            this.lbReason.Items.Remove(this.lbReason.SelectedItem);
            if (this.lbReason.Items.Count == 0)
            {
                this.btnDelete.Enabled = false;
            }
        }

        //入库保存
        private void Save()
        {
            //返品数据
            List<BllReceiptReturnTable> returnlist = new List<BllReceiptReturnTable>();
            if (!CheckInput(returnlist))
            {
                return;
            }

            //入库数据
            BllReceiptLineTable rlTable = new BllReceiptLineTable();
            rlTable.RECEIVING_PLAN_SLIP_NUMBER = Convert.ToDecimal(this.txtSlipNumber.Text);
            rlTable.QUANTITY = Convert.ToDecimal(txtQuantity.Text);

            //入库预定数据
            BllReceivingPlanTable rpTable = null;
            if (this.rdo2.Checked)
            {
                rpTable = new BllReceivingPlanTable();
                rpTable.ARRIVAL_DATE = Convert.ToDateTime(txtNewArrivalDate.Text);
                rpTable.QUANTITY = Convert.ToDecimal(txtNewQuantity.Text);
            }

            if (bll.Insert(rlTable, rpTable, returnlist, UserTable.USER_ID))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"入库处理成功!\");processCloseAndRefreshParent();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"入库处理失败!\");", true);
            }
        }

        //页面输入项检查
        private bool CheckInput(List<BllReceiptReturnTable> list)
        {
            string message = "";
            try
            {
                decimal oldQuantity = Convert.ToDecimal(this.txtOldQuantity.Text.Trim());
                decimal quantity = Convert.ToDecimal(this.txtQuantity.Text.Trim());
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
                    BllReceiptReturnTable rrTable = null;
                    decimal totalReturnQuantity = 0;
                    if (this.lbReason.Items.Count > 0)
                    {
                        string[] str = null;
                        foreach (ListItem li in this.lbReason.Items)
                        {
                            rrTable = new BllReceiptReturnTable();
                            rrTable.RETURN_REASON = li.Value;
                            str = li.Text.Split('|');
                            rrTable.QUANTITY = Convert.ToDecimal(str[0]);
                            list.Add(rrTable);
                            totalReturnQuantity += rrTable.QUANTITY;
                        }
                    }

                    if (totalReturnQuantity > Convert.ToDecimal(this.txtReQuantity.Text.Trim()))
                    {
                        message += "返品原因数量大于总的返品数量!\\n";
                    }

                    if (this.rdo2.Checked)
                    {
                        if (this.txtNewArrivalDate.Text.Trim() == "")
                        {
                            message += "二期交货预定日期不能为空!\\n";
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                message += "入库数量输入格式错误!\\n";
            }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return false;
            }
            return true;
        }


        protected void NewArrivalDate_Changed(object sender, EventArgs e)
        {
            if (txtNewArrivalDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtNewArrivalDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtNewArrivalDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtNewArrivalDate.Text = Convert.ToDateTime(txtNewArrivalDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
}//end class
}

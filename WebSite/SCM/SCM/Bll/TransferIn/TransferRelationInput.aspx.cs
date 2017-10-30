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
    public partial class TransferRelationInput : BasePage
    {
        BTransferRelation bll = new BTransferRelation();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int pageSize = 12;
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            DataTable dt = null;
            this.txtDeparturlDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            if (!Page.IsPostBack)
            {
                dt = InitDataTable();
                gridView.DataSource = dt;
                gridView.DataBind();
                ViewState["SHIPMENT_DATATABLE"] = dt;
                this.txtDeparturlDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            btnProduct.Attributes.Add("onclick", "processMasterClickByServer('product','" + txtProductCode.ClientID + "','" + lblProductName.ClientID + "');");
            
        }

        #region change事件
        protected void From_Warehouse_Change(object sender, EventArgs e)
        {
            if (txtFromWarehouseCode.Text.Trim() == "")
            {
                this.txtFromWarehouseCode.Text = "";
                this.lblFromWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtFromWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtFromWarehouseCode.Text = table.Code;
                this.lblFromWarehouseName.Text = table.Name;
            }
            else
            {
                this.lblFromWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库仓库不存在!\");document.getElementById('" + txtFromWarehouseCode.ClientID + "').value='';", true);
            }
        }
        protected void DeparturlDate_Changed(object sender, EventArgs e)
        {
            if (txtDeparturlDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtDeparturlDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtDeparturlDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtDeparturlDate.Text = Convert.ToDateTime(txtDeparturlDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void To_Warehouse_Change(object sender, EventArgs e)
        {
            if (txtToWarehouseCode.Text.Trim() == "")
            {
                this.txtToWarehouseCode.Text = "";
                this.lblToWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtToWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtToWarehouseCode.Text = table.Code;
                this.lblToWarehouseName.Text = table.Name;
            }
            else
            {
                this.lblToWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库仓库不存在!\");document.getElementById('" + txtToWarehouseCode.ClientID + "').value='';", true);
            }
        }
        protected void Product_Changed(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() == "")
            {
                this.lblProductName.Text = "";
                this.lblStyle.Text = "";
                this.lblColor.Text = "";
                this.lblSize.Text = "";
                this.lblUnit.Text = "";
                this.txtUnitCode.Text = "";
                return;
            }

            BaseProductTable productTable = new BProduct().GetModel(txtProductCode.Text.Trim());
            if (productTable != null)
            {
                this.txtProductCode.Text = productTable.CODE;
                this.lblProductName.Text = productTable.NAME;
                this.lblStyle.Text = productTable.STYLE_NAME;
                this.lblColor.Text = productTable.COLOR_NAME;
                this.lblSize.Text = productTable.SIZE_NAME;
                this.lblUnit.Text = productTable.UNIT_NAME;
                this.txtUnitCode.Text = productTable.UNIT_CODE;
            }
            else
            {
                this.lblProductName.Text = "";
                this.lblStyle.Text = "";
                this.lblColor.Text = "";
                this.lblSize.Text = "";
                this.lblUnit.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品不存在!\");document.getElementById('" + txtProductCode.ClientID + "').value='';", true);
            }
        }
        #endregion

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LINE_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_CODE", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SIZE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("UNIT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("UNIT_CODE", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.Decimal"));
            dt.Columns.Add("LINE_ATTRIBUTE1", Type.GetType("System.String"));
            dt.Columns.Add("LINE_ATTRIBUTE2", Type.GetType("System.String"));
            dt.Columns.Add("LINE_ATTRIBUTE3", Type.GetType("System.String"));

            for (int i = 1; i <= pageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            return dt;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnAdd":
                    AddLine();
                    break;
                case "btnClear":
                    ClearLine();
                    break;
                case "btnModify":
                    ModifyShow(sender, e);
                    break;
                case "btnDelete":
                    DeleteLine(sender, e);
                    break;
                case "btnSave":
                    Save(sender, e);
                    break;
                case "btnProduct":
                    Product_Changed(sender, e);
                    break;
            }
            return true;
        }

        private void Save(object sender, EventArgs e)
        {
            if (!CheckHearderInput())
            {
                return;
            }
            BllShipmentTable shipmentTable = new BllShipmentTable();
            shipmentTable.SHIPMENT_TYPE = CConstant.SHIPMENT_TYPE_SHFIT;
            shipmentTable.DEPARTUAL_DATE = Convert.ToDateTime(txtDeparturlDate.Text);
            shipmentTable.ARRIVAL_DATE = Convert.ToDateTime(txtDeparturlDate.Text);
            shipmentTable.FROM_WAREHOUSE_CODE = txtFromWarehouseCode.Text.Trim();
            shipmentTable.TO_WAREHOUSE_CODE = txtToWarehouseCode.Text.Trim();
            shipmentTable.STATUS_FLAG = CConstant.NORMAL;
            shipmentTable.ATTRIBUTE1 = txtAttribute1.Text.Trim();
            shipmentTable.ATTRIBUTE2 = txtAttribute2.Text.Trim();
            shipmentTable.ATTRIBUTE3 = txtAttribute3.Text.Trim();
            shipmentTable.CREATE_USER = _userTable.USER_ID;
            shipmentTable.LAST_UPDATE_USER = _userTable.USER_ID;

            DataTable dt = (DataTable)ViewState["SHIPMENT_DATATABLE"];
            BllShipmentLineTable shipmentLineTable = null;
            foreach (DataRow row in dt.Rows)
            {
                if (row["LINE_NUMBER"] == null || row["LINE_NUMBER"].ToString().Trim() == "")
                {
                    break;
                }
                shipmentLineTable = new BllShipmentLineTable();
                shipmentLineTable.SLIP_NUMBER = shipmentTable.SLIP_NUMBER;
                shipmentLineTable.LINE_NUMBER = Convert.ToInt32(row["LINE_NUMBER"]);
                shipmentLineTable.SHIPMENT_PLAN_SLIP_NUMBER = 0;
                shipmentLineTable.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                shipmentLineTable.UNIT_CODE = Convert.ToString(row["UNIT_CODE"]);
                shipmentLineTable.QUANTITY = Convert.ToDecimal(row["QUANTITY"]);
                shipmentLineTable.STATUS_FLAG = CConstant.NORMAL;
                shipmentLineTable.ATTRIBUTE1 = Convert.ToString(row["LINE_ATTRIBUTE1"]);
                shipmentLineTable.ATTRIBUTE2 = Convert.ToString(row["LINE_ATTRIBUTE2"]);
                shipmentLineTable.ATTRIBUTE3 = Convert.ToString(row["LINE_ATTRIBUTE3"]);

                shipmentTable.AddShipmentLine(shipmentLineTable);
            }
            if (shipmentTable.SHIPMENT_LINE.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"请输入订单明细信息！\");", true);
                return;
            }

            //新的订单输入
            if (bll.Insert(shipmentTable) > 0)
            {
                ClearLine();
                dt = InitDataTable();
                gridView.DataSource = dt;
                gridView.DataBind();
                ViewState["SHIPMENT_DATATABLE"] = dt;
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存成功!\");processCloseAndRefreshParent();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存失败!\");", true);
            }

        }

        //订单明细删除
        private void DeleteLine(object sender, EventArgs e)
        {
            if (this.txtLineNumber.Text.Trim() != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单明细修正中，请先登录或清空明细！\");", true);
                return;
            }
            DataTable dt = (DataTable)ViewState["SHIPMENT_DATATABLE"];
            LinkButton btnD = (LinkButton)sender;
            string lineNumber = btnD.CommandArgument;
            DataRow row = dt.Rows[Convert.ToInt32(lineNumber) - 1];
            dt.Rows.Remove(row);
            for (int i = Convert.ToInt32(lineNumber) - 1; i < dt.Rows.Count; i++)
            {
                row = dt.Rows[i];
                if (row["LINE_NUMBER"].ToString() != "")
                {
                    row["LINE_NUMBER"] = (i + 1).ToString();
                }
                else
                {
                    break;
                }
            }
            if (dt.Rows.Count < pageSize)
            {
                for (int i = dt.Rows.Count; i < pageSize; i++)
                {
                    dt.Rows.Add(dt.NewRow());
                }
            }
            gridView.DataSource = dt;
            gridView.DataBind();
            ViewState["SHIPMENT_DATATABLE"] = dt;
        }
        //显示明细
        private void ModifyShow(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["SHIPMENT_DATATABLE"];
            LinkButton btnM = (LinkButton)sender;
            string lineNumber = btnM.CommandArgument;
            DataRow row = dt.Rows[Convert.ToInt32(lineNumber) - 1];
            this.txtLineNumber.Text = Convert.ToString(row["LINE_NUMBER"]);
            this.txtProductCode.Text = Convert.ToString(row["PRODUCT_CODE"]);
            this.lblProductName.Text = Convert.ToString(row["PRODUCT_NAME"]);
            this.lblStyle.Text = Convert.ToString(row["STYLE_NAME"]);
            this.lblColor.Text = Convert.ToString(row["COLOR_NAME"]);
            this.lblSize.Text = Convert.ToString(row["SIZE_NAME"]);
            this.lblUnit.Text = Convert.ToString(row["UNIT_NAME"]);
            this.txtUnitCode.Text = Convert.ToString(row["UNIT_CODE"]);
            this.txtQuantity.Text = Convert.ToString(row["QUANTITY"]);
            this.txtLineAttribute1.Text = Convert.ToString(row["LINE_ATTRIBUTE1"]);
            this.txtLineAttribute2.Text = Convert.ToString(row["LINE_ATTRIBUTE2"]);
            this.txtLineAttribute3.Text = Convert.ToString(row["LINE_ATTRIBUTE3"]);
            this.lblMessage.Text = "明细[ " + lineNumber + " ]修正中．．．．";
        }
        //清空
        private void ClearLine()
        {
            this.txtLineNumber.Text = "";
            this.txtProductCode.Text = "";
            this.lblProductName.Text = "";
            this.lblStyle.Text = "";
            this.lblColor.Text = "";
            this.lblSize.Text = "";
            this.lblUnit.Text = "";
            this.txtQuantity.Text = "";
            this.txtLineAttribute1.Text = "";
            this.txtLineAttribute2.Text = "";
            this.txtLineAttribute3.Text = "";
            this.lblMessage.Text = "";
        }

        //添加明细
        private void AddLine()
        {
            if (!CheckLineInput())
            {
                return;
            }
            DataTable dt = (DataTable)ViewState["SHIPMENT_DATATABLE"];
            DataRow row = null;
            if (txtLineNumber.Text.Trim() != "")
            {
                row = dt.Rows[Convert.ToInt32(txtLineNumber.Text.Trim()) - 1];
                EditDataRow(row);
            }
            else
            {
                int i = 0;
                if (dt.Rows.Count <= pageSize)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        row = dt.Rows[i];
                        if (row["LINE_NUMBER"].ToString() == "")
                        {
                            row["LINE_NUMBER"] = (i + 1).ToString();
                            EditDataRow(row);
                            break;
                        }
                    }
                }
                if (dt.Rows.Count > pageSize || i >= pageSize)
                {
                    row = dt.NewRow();
                    row["LINE_NUMBER"] = (dt.Rows.Count + 1).ToString();
                    EditDataRow(row);
                    dt.Rows.Add(row);
                }
            }
            //重新绑定
            gridView.DataSource = dt;
            gridView.DataBind();
            ViewState["SHIPMENT_DATATABLE"] = dt;
            ClearLine();
        }


        private void EditDataRow(DataRow row)
        {
            row["PRODUCT_CODE"] = this.txtProductCode.Text;
            row["PRODUCT_NAME"] = this.lblProductName.Text;
            row["STYLE_NAME"] = this.lblStyle.Text;
            row["COLOR_NAME"] = this.lblColor.Text;
            row["SIZE_NAME"] = this.lblSize.Text;
            row["UNIT_NAME"] = this.lblUnit.Text;
            row["UNIT_CODE"] = this.txtUnitCode.Text;
            row["QUANTITY"] = Convert.ToDecimal(this.txtQuantity.Text);
            row["LINE_ATTRIBUTE1"] = this.txtLineAttribute1.Text;
            row["LINE_ATTRIBUTE2"] = this.txtLineAttribute2.Text;
            row["LINE_ATTRIBUTE3"] = this.txtLineAttribute3.Text;
        }

        //表头验证
        private bool CheckHearderInput()
        {
            string message = "";
            //出库日期
            if (this.txtDeparturlDate.Text.Trim() == "")
            {
                message += "采购日期不能为空!\\n";
            }

            //出库仓库
            if (this.txtFromWarehouseCode.Text.Trim() == "")
            {
                message += "出库仓库不能为空!\\n";
            }

            //入库仓库
            if (this.txtToWarehouseCode.Text.Trim() == "")
            {
                message += "入库仓库不能为空!\\n";
            }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return false;
            }
            return true;
        }

        //明细验证
        private bool CheckLineInput()
        {
            string message = "";
            //商品CHECK
            if (this.txtProductCode.Text.Trim() == "")
            {
                message += "商品编号不能为空!\\n";
            }
            //数量
            if (this.txtQuantity.Text.Trim() == "")
            {
                message += "数量不能为空!\\n";
            }
            else
            {
                try
                {
                    decimal quantity = decimal.Parse(this.txtQuantity.Text.Trim());
                }
                catch
                {
                    message += "数量输入格式错误!\\n";
                }
            }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return false;
            }
            return true;
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                string ID = btnD.CommandArgument;
                btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");

                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");

                if (e.Row.Cells[0].Text == "&nbsp;" || e.Row.Cells[0].Text == "")
                {
                    btnD.Visible = false;
                    btnM.Visible = false;
                }
            }
        }
    }
}
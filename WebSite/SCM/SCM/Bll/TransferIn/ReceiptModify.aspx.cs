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
    public partial class ReceiptModify : BaseModalDialogPage
    {
        BReceipt bll = new BReceipt();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        int pageSize = 12;
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                Object SN = Request.Params["SN"];
                if (SN != null)
                {
                    this.txtSlipNumber.Text = SN.ToString();
                    this.lblTitle.InnerHtml = "入库&nbsp;>>&nbsp;成品入库&nbsp;>>&nbsp;编辑";
                    try
                    {
                        ds = bll.GetReceiptDetail(SN.ToString().Trim());
                        dt = ds.Tables[0];
                        for (int i = dt.Rows.Count; i < pageSize; i++)
                        {
                            dt.Rows.Add(dt.NewRow());
                        }
                        this.selInputType.Value = Convert.ToString(dt.Rows[0]["INPUT_TYPE"]);
                        this.txtFromDate.Text = Convert.ToDateTime(dt.Rows[0]["ARRIVAL_DATE"]).ToString("yyyy/MM/dd");
                        this.txtToWarehouseCode.Text = Convert.ToString(dt.Rows[0]["TO_WAREHOUSE_CODE"]);
                        this.lblToWarehouseName.Text = Convert.ToString(dt.Rows[0]["TO_WAREHOUSE_NAME"]);
                        this.lblSupplierName.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_NAME"]);
                        this.txtSupplierCode.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                        this.txtAttribute1.Text = Convert.ToString(dt.Rows[0]["ATTRIBUTE1"]);
                        this.txtAttribute2.Text = Convert.ToString(dt.Rows[0]["ATTRIBUTE2"]);
                        this.txtAttribute3.Text = Convert.ToString(dt.Rows[0]["ATTRIBUTE3"]);
                    }
                    catch (Exception ex) { }
                    if (Request.Params["ST"] != null && Request.Params["ST"].ToString() == "1")
                    {
                        this.btnSave.Enabled = false;
                        this.btnSave.Visible = false;
                    }
                    else if (Request.Params["SHOW_FLAG"] != null && Request.Params["SHOW_FLAG"].ToString() == "1")
                    {
                        this.btnSave.Enabled = false;
                        this.btnSave.Visible = false;
                        this.lblTitle.InnerHtml = "入库&nbsp;>>&nbsp;成品入库&nbsp;>>&nbsp;详细";
                    }
                }
                else
                {
                    dt = InitDataTable();
                    this.lblTitle.InnerHtml = "入库&nbsp;>>&nbsp;成品入库&nbsp;>>&nbsp;临时入库";
                }
                gridView.DataSource = dt;
                gridView.DataBind();
                ViewState["RECEIPT_DATATABLE"] = dt;
                btnProduct.Attributes.Add("onclick", "processMasterClickByServer('PRODUCT','" + txtProductCode.ClientID + "','" + lblProductName.ClientID + "');");
                btnCancel.Attributes.Add("onclick", "return processClose('你确定要取消订单的编辑吗?'); ");
                this.Title = this.lblTitle.InnerHtml;
            }
        }

        private DataTable InitDataTable()
        {
            dt = new DataTable();
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


        #region button click event
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
                case "btnCancel":
                    ClearLine();
                    ClearHeader();
                    dt = InitDataTable();
                    gridView.DataSource = dt;
                    gridView.DataBind();
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
            BllReceiptTable receiptTable = new BllReceiptTable();
            receiptTable.SLIP_NUMBER = this.txtSlipNumber.Text.Trim();
            receiptTable.INPUT_TYPE = Convert.ToInt32(this.selInputType.Value);
            receiptTable.RECEIPT_TYPE = CConstant.RECEIPT_TYPE_TEMP;
            receiptTable.ARRIVAL_DATE = Convert.ToDateTime(this.txtFromDate.Text);
            receiptTable.TO_WAREHOUSE_CODE = this.txtToWarehouseCode.Text.Trim();
            receiptTable.SUPPLIER_CODE = this.txtSupplierCode.Text.Trim();
            receiptTable.STATUS_FLAG = CConstant.NORMAL;
            receiptTable.ATTRIBUTE1 = this.txtAttribute1.Text.Trim();
            receiptTable.ATTRIBUTE2 = this.txtAttribute2.Text.Trim();
            receiptTable.ATTRIBUTE3 = this.txtAttribute3.Text.Trim();
            receiptTable.CREATE_USER = UserTable.USER_ID;
            receiptTable.LAST_UPDATE_USER = receiptTable.CREATE_USER;

            dt = (DataTable)ViewState["RECEIPT_DATATABLE"];
            if (dt == null)
            {
                InitDataTable();
            }
            BllReceiptLineTable receiptLineTable = null;
            foreach (DataRow row in dt.Rows)
            {
                if (row["LINE_NUMBER"] == null || row["LINE_NUMBER"].ToString().Trim() == "")
                {
                    break;
                }
                receiptLineTable = new BllReceiptLineTable();
                receiptLineTable.SLIP_NUMBER = receiptTable.SLIP_NUMBER;
                receiptLineTable.LINE_NUMBER = Convert.ToInt32(row["LINE_NUMBER"]);
                receiptLineTable.RECEIVING_PLAN_SLIP_NUMBER = 0;
                receiptLineTable.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                receiptLineTable.UNIT_CODE = Convert.ToString(row["UNIT_CODE"]);
                receiptLineTable.QUANTITY = Convert.ToDecimal(row["QUANTITY"]);
                receiptLineTable.STATUS_FLAG = CConstant.NORMAL;
                receiptLineTable.ATTRIBUTE1 = Convert.ToString(row["LINE_ATTRIBUTE1"]);
                receiptLineTable.ATTRIBUTE2 = Convert.ToString(row["LINE_ATTRIBUTE2"]);
                receiptLineTable.ATTRIBUTE3 = Convert.ToString(row["LINE_ATTRIBUTE3"]);

                receiptTable.AddReceiptLine(receiptLineTable);
            }
            if (receiptTable.ReceiptLine.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"请输入订单明细信息！\");", true);
                return;
            }
            if (txtSlipNumber.Text.Trim() != "")
            {
                //订单修正
                if (bll.Update(receiptTable) > 0)
                {
                    ClearLine();
                    ClearHeader();
                    dt = InitDataTable();
                    gridView.DataSource = dt;
                    gridView.DataBind();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单修正成功!\");processCloseAndRefreshParent();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单修正失败!\");", true);
                }
            }
            else
            {
                //新的订单输入
                if (bll.Insert(receiptTable) > 0)
                {
                    ClearLine();
                    ClearHeader();
                    dt = InitDataTable();
                    gridView.DataSource = dt;
                    gridView.DataBind();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存成功!\");processCloseAndRefreshParent();", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存失败!\");", true);
                }
            }

        }

        private void DeleteLine(object sender, EventArgs e)
        {
            if (this.txtLineNumber.Text.Trim() != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单明细修正中，请先登录或清空明细！\");", true);
                return;
            }
            DataTable dt = (DataTable)ViewState["RECEIPT_DATATABLE"];
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
            ViewState["RECEIPT_DATATABLE"] = dt;
        }

        private void AddLine()
        {
            if (!CheckLineInput())
            {
                return;
            }
            dt = (DataTable)ViewState["RECEIPT_DATATABLE"];
            if (dt == null)
            {
                InitDataTable();
            }
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
            gridView.DataSource = dt;
            gridView.DataBind();
            ViewState["RECEIPT_DATATABLE"] = dt;
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

        #endregion

        #region Changed 事件
        protected void TO_Warehouse_Change(object sender, EventArgs e)
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"入库仓库不存在!\");document.getElementById('" + txtToWarehouseCode.ClientID + "').value='';", true);
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

        #region 清空输入区域
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
        private void ClearHeader()
        {
            this.txtSlipNumber.Text = "";
            this.txtFromDate.Text = "";
            this.txtToWarehouseCode.Text = "";
            this.lblToWarehouseName.Text = "";
            this.txtAttribute1.Text = "";
            this.txtAttribute2.Text = "";
            this.txtAttribute3.Text = "";

        }
        #endregion


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

        #region 明细修正,数据显示
        private void ModifyShow(object sender, EventArgs e)
        {
            dt = (DataTable)ViewState["RECEIPT_DATATABLE"];
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
        #endregion

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

        private bool CheckHearderInput()
        {
            string message = "";
            //入库仓库
            if (this.txtToWarehouseCode.Text.Trim() == "")
            {
                message += "入库仓库不能为空!\\n";
            }

            //入库预定日  >=NOW
            try
            {
                if (txtFromDate.Text.Trim() != "")
                {
                    string dateNow = DateTime.Now.ToString("yyyy/MM/dd");
                    if (DateTime.Parse(dateNow) > DateTime.Parse(txtFromDate.Text))
                    {
                        message += "入库日期不能为过去日期!\\n";
                    }
                }
                else
                {
                    message += "入库日期不能为空!\\n";
                }
            }
            catch { }

            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return false;
            }
            return true;
        }
        protected void Supplier_Change(object sender, EventArgs e)
        {
            if (txtSupplierCode.Text.Trim() == "")
            {
                this.txtSupplierCode.Text = "";
                this.lblSupplierName.Text = "";
                return;
            }

            BaseMaster table = bCommon.GetBaseMaster("BASE_SUPPLIER", txtSupplierCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtSupplierCode.Text = table.Code;
                this.lblSupplierName.Text = table.Name;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"供应商不存在！\");", true);
                this.txtSupplierCode.Text = "";
                this.lblSupplierName.Text = "";
            }
        }
        protected void FromDate_Changed(object sender, EventArgs e)
        {
            if (txtFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtFromDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtFromDate.Text = Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }

    }
}

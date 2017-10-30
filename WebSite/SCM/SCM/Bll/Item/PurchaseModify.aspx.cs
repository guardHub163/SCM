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
using System.Text;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.Item
{
    public partial class PurchaseModify : BaseModalDialogPage
    {
        BPurchase bll = new BPurchase();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        int pageSize = 12;
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region init
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                Object slipNumer = Request.Params["SN"];
                if (slipNumer != null)
                {
                    this.lblTitle.InnerHtml = "采购&nbsp;>>&nbsp;原料采购&nbsp;>>&nbsp;编辑";
                    this.txtSlipNumber.Text = slipNumer.ToString();
                    try
                    {
                        ds = bll.GetPurchaseDetail(slipNumer.ToString().Trim());
                        dt = ds.Tables[0];
                        for (int i = dt.Rows.Count; i < pageSize; i++)
                        {
                            dt.Rows.Add(dt.NewRow());
                        }
                        this.selInputType.Value = Convert.ToString(dt.Rows[0]["INPUT_TYPE"]);
                        this.txtPurchaseDate.Text = Convert.ToDateTime(dt.Rows[0]["PURCHASE_DATE"]).ToString("yyyy/MM/dd");
                        this.txtSupplierCode.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_CODE"]);
                        this.lblSupplierName.Text = Convert.ToString(dt.Rows[0]["SUPPLIER_NAME"]);
                        this.txtWarehouseCode.Text = Convert.ToString(dt.Rows[0]["WAREHOUSE_CODE"]);
                        this.lblWarehouseName.Text = Convert.ToString(dt.Rows[0]["WAREHOUSE_NAME"]);
                        this.txtUser.Text = Convert.ToString(dt.Rows[0]["CREATE_USER"]);
                        this.lblUserName.Text = Convert.ToString(dt.Rows[0]["TRUE_NAME"]);
                        this.txtAttribute1.Text = Convert.ToString(dt.Rows[0]["ATTRIBUTE1"]);
                        this.txtAttribute2.Text = Convert.ToString(dt.Rows[0]["ATTRIBUTE2"]);
                        this.txtAttribute3.Text = Convert.ToString(dt.Rows[0]["ATTRIBUTE3"]);
                    }
                    catch { }
                    if (Request.Params["RS"] != null && Request.Params["RS"].ToString() != "0")
                    {
                        this.btnSave.Enabled = false;
                        this.btnSave.Visible = false;
                    }
                    else if (Request.Params["SHOW_FLAG"] != null && Request.Params["SHOW_FLAG"].ToString() == "1")
                    {
                        this.btnSave.Enabled = false;
                        this.btnSave.Visible = false;
                        this.lblTitle.InnerHtml = "采购&nbsp;>>&nbsp;原料采购&nbsp;>>&nbsp;详细";
                    }
                }
                else
                {
                    try
                    {
                        BaseUserTable userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
                        this.txtUser.Text = userTable.USER_ID;
                        this.lblUserName.Text = userTable.TRUE_NAME;
                    }
                    catch { }
                    dt = InitDataTable();
                    this.lblTitle.InnerHtml = "采购&nbsp;>>&nbsp;原料采购&nbsp;>>&nbsp;新建";
                }
                gridView.DataSource = dt;
                gridView.DataBind();
                ViewState["PURCHASE_DATATABLE"] = dt;
                btnProduct.Attributes.Add("onclick", "return processMasterClickByServer('PRODUCT','" + txtProductCode.ClientID + "','" + lblProductName.ClientID + "');");
                //btnCancel.Attributes.Add("onclick", "return processClose('你确定要取消订单的编辑吗?')");

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
            dt.Columns.Add("PRICE", Type.GetType("System.Decimal"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.Decimal"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("LINE_ATTRIBUTE1", Type.GetType("System.String"));
            dt.Columns.Add("LINE_ATTRIBUTE2", Type.GetType("System.String"));
            dt.Columns.Add("LINE_ATTRIBUTE3", Type.GetType("System.String"));

            for (int i = 1; i <= pageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            return dt;
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
        #endregion

        #region textbox change event
        protected void Warehouse_Change(object sender, EventArgs e)
        {
            if (txtWarehouseCode.Text.Trim() == "")
            {
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtWarehouseCode.Text = table.Code;
                this.lblWarehouseName.Text = table.Name;
            }
            else
            {
                this.lblWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"入库仓库不存在!\");document.getElementById('" + txtWarehouseCode.ClientID + "').value='';", true);
            }
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
                this.lblSupplierName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"供应商不存在!\");document.getElementById('" + txtSupplierCode.ClientID + "').value='';", true);
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
                //case "btnCancel":
                //ClearLine();
                //ClearHeader();
                //dt = InitDataTable();
                //gridView.DataSource = dt;
                //gridView.DataBind();
                //HttpContext.Current.Session.Remove("PURCHASE_DATATABLE");
                //break;
                case "btnProduct":
                    Product_Changed(sender, e);
                    break;
            }
            return true;
        }
        #endregion

        #region 明细修正,数据显示
        private void ModifyShow(object sender, EventArgs e)
        {
            dt = (DataTable)ViewState["PURCHASE_DATATABLE"];
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
            this.txtPrice.Text = Convert.ToString(row["PRICE"]);
            this.txtQuantity.Text = Convert.ToString(row["QUANTITY"]);
            this.txtArrivalDate.Text = Convert.ToString(row["ARRIVAL_DATE"]).Substring(0, 10);
            this.txtLineAttribute1.Text = Convert.ToString(row["LINE_ATTRIBUTE1"]);
            this.txtLineAttribute2.Text = Convert.ToString(row["LINE_ATTRIBUTE2"]);
            this.txtLineAttribute3.Text = Convert.ToString(row["LINE_ATTRIBUTE3"]);
            this.lblMessage.Text = "明细[ " + lineNumber + " ]修正中．．．．";
        }
        #endregion

        #region 订单明细追加,修正
        private void AddLine()
        {
            if (!CheckLineInput())
            {
                return;
            }
            dt = (DataTable)ViewState["PURCHASE_DATATABLE"];
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
            //重新绑定
            gridView.DataSource = dt;
            gridView.DataBind();
            ViewState["PURCHASE_DATATABLE"] = dt;
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
            row["PRICE"] = Convert.ToDecimal(this.txtPrice.Text);
            row["QUANTITY"] = Convert.ToDecimal(this.txtQuantity.Text);
            row["ARRIVAL_DATE"] = this.txtArrivalDate.Text;
            row["LINE_ATTRIBUTE1"] = this.txtLineAttribute1.Text;
            row["LINE_ATTRIBUTE2"] = this.txtLineAttribute2.Text;
            row["LINE_ATTRIBUTE3"] = this.txtLineAttribute3.Text;
        }
        #endregion

        #region  订单明细册除
        private void DeleteLine(object sender, EventArgs e)
        {
            if (this.txtLineNumber.Text.Trim() != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单明细修正中，请先登录或清空明细！\");", true);
                return;
            }
            DataTable dt = (DataTable)ViewState["PURCHASE_DATATABLE"];
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
            ViewState["PURCHASE_DATATABLE"] = dt;
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
            this.txtPrice.Text = "";
            this.txtQuantity.Text = "";
            this.txtArrivalDate.Text = "";
            this.txtLineAttribute1.Text = "";
            this.txtLineAttribute2.Text = "";
            this.txtLineAttribute3.Text = "";
            this.lblMessage.Text = "";
        }
        private void ClearHeader()
        {
            this.txtSlipNumber.Text = "";
            this.txtPurchaseDate.Text = "";
            this.txtWarehouseCode.Text = "";
            this.lblWarehouseName.Text = "";
            this.txtSupplierCode.Text = "";
            this.lblSupplierName.Text = "";
            this.txtAttribute1.Text = "";
            this.txtAttribute2.Text = "";
            this.txtAttribute3.Text = "";

        }
        #endregion

        #region 订单保存
        private void Save(object sender, EventArgs e)
        {
            if (!CheckHearderInput())
            {
                return;
            }
            BllPurchaseTable purchaseTable = new BllPurchaseTable();
            purchaseTable.SLIP_NUMBER = txtSlipNumber.Text.Trim();
            purchaseTable.INPUT_TYPE = int.Parse(selInputType.Value);
            purchaseTable.PURCHASE_DATE = DateTime.Parse(txtPurchaseDate.Text);
            purchaseTable.SUPPLIER_CODE = txtSupplierCode.Text.Trim();
            purchaseTable.WAREHOUSE_CODE = txtWarehouseCode.Text.Trim();
            purchaseTable.STATUS_FLAG = CConstant.INIT;
            purchaseTable.ATTRIBUTE1 = txtAttribute1.Text.Trim();
            purchaseTable.ATTRIBUTE2 = txtAttribute2.Text.Trim();
            purchaseTable.ATTRIBUTE3 = txtAttribute3.Text.Trim();
            purchaseTable.CREATE_USER = txtUser.Text.Trim();
            purchaseTable.LAST_UPDATE_USER = UserTable.USER_ID;

            dt = (DataTable)ViewState["PURCHASE_DATATABLE"];
            if (dt == null)
            {
                InitDataTable();
            }
            BllPurchaseLineTable purchaseLineTable = null;
            foreach (DataRow row in dt.Rows)
            {
                if (row["LINE_NUMBER"] == null || row["LINE_NUMBER"].ToString().Trim() == "")
                {
                    break;
                }
                purchaseLineTable = new BllPurchaseLineTable();
                purchaseLineTable.SLIP_NUMBER = purchaseTable.SLIP_NUMBER;
                purchaseLineTable.LINE_NUMBER = Convert.ToInt32(row["LINE_NUMBER"]);
                purchaseLineTable.ARRIVAL_DATE = Convert.ToDateTime(row["ARRIVAL_DATE"]);
                purchaseLineTable.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                purchaseLineTable.UNIT_CODE = Convert.ToString(row["UNIT_CODE"]);
                purchaseLineTable.PRICE = Convert.ToDecimal(row["PRICE"]);
                purchaseLineTable.QUANTITY = Convert.ToDecimal(row["QUANTITY"]);
                purchaseLineTable.STATUS_FLAG = CConstant.INIT;
                purchaseLineTable.ATTRIBUTE1 = Convert.ToString(row["LINE_ATTRIBUTE1"]);
                purchaseLineTable.ATTRIBUTE2 = Convert.ToString(row["LINE_ATTRIBUTE2"]);
                purchaseLineTable.ATTRIBUTE3 = Convert.ToString(row["LINE_ATTRIBUTE3"]);

                purchaseTable.AddPurchaseLine(purchaseLineTable);
            }
            if (purchaseTable.PURCHASE_LINE.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"请输入订单明细信息！\");", true);
                return;
            }


            if (txtSlipNumber.Text.Trim() != "")
            {
                //订单修正
                if (bll.Update(purchaseTable) > 0)
                {
                    ClearLine();
                    ClearHeader();
                    dt = InitDataTable();
                    gridView.DataSource = dt;
                    gridView.DataBind();
                    //HttpContext.Current.Session.Remove("PURCHASE_DATATABLE");
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单修正成功!\");processCloseAndRefreshParent();", true);
                }
                else
                {
                    lblMessage.Text = "订单修正失败!";
                }

            }
            else
            {
                //新的订单输入
                if (bll.Insert(purchaseTable) > 0)
                {
                    ClearLine();
                    ClearHeader();
                    dt = InitDataTable();
                    gridView.DataSource = dt;
                    gridView.DataBind();
                    //HttpContext.Current.Session.Remove("PURCHASE_DATATABLE");
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存成功!\");processCloseAndRefreshParent();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存失败!\");", true);
                }
            }
        }

        #endregion

        #region 数据验证
        //表头
        private bool CheckHearderInput()
        {
            string message = "";
            //采购日期
            if (this.txtPurchaseDate.Text.Trim() == "")
            {
                message += "采购日期不能为空!\\n";
            }

            //供应商
            if (this.txtSupplierCode.Text.Trim() == "")
            {
                message += "供应商不能为空!\\n";
            }

            //入库仓库
            if (this.txtWarehouseCode.Text.Trim() == "")
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

        //明细
        private bool CheckLineInput()
        {
            string message = "";
            //商品CHECK
            if (this.txtProductCode.Text.Trim() == "")
            {
                message += "商品编号不能为空!\\n";
            }
            //单价
            if (this.txtPrice.Text.Trim() == "")
            {
                message += "单价不能为空!\\n";
            }
            else
            {
                try
                {
                    decimal price = decimal.Parse(this.txtPrice.Text.Trim());
                }
                catch
                {
                    message += "单价输入格式错误!\\n";
                }
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

            //入库预定日  >=NOW           
            try
            {
                if (txtArrivalDate.Text.Trim() != "")
                {
                    string dateNow = DateTime.Now.ToString("yyyy/MM/dd");
                    if (DateTime.Parse(dateNow) > DateTime.Parse(txtArrivalDate.Text))
                    {
                        message += "入库预定日不能为过去日期!\\n";
                    }
                }
                else
                {
                    message += "入库预定日不能为空!\\n";
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
        #endregion
        protected void PurchaseDate_Changed(object sender, EventArgs e)
        {
            if (txtPurchaseDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtPurchaseDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtPurchaseDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtPurchaseDate.Text = Convert.ToDateTime(txtPurchaseDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void ArrivalDate_Changed(object sender, EventArgs e)
        {
            if (txtArrivalDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtArrivalDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtArrivalDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtArrivalDate.Text = Convert.ToDateTime(txtArrivalDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
    }
}


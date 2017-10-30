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
using SCM.SQLServerDAL;
using log4net;
using System.Reflection;

namespace SCM.Web.Purchase
{
    public partial class RequiditionAdd : BaseModalDialogPage
    {
        BPurchaseRequisition bll = new BPurchaseRequisition();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int PageSize = 12;
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                btnProductGroup.Attributes.Add("onclick", "return processMasterClickByServer('PRODUCT_GROUP','" + txtProductGroupCode.ClientID + "','" + lblProductGroupName.ClientID + "');");
                btnClear.Attributes.Add("onclick", "return confirm(\"你确定要清空载入的商品吗?\");");
                ddlPeriod.DataSource = bCommon.GetNames("REQUISITION_PERIOD").Tables[0];
                ddlPeriod.DataValueField = "CODE";//dropdownlist的Value的字段 
                ddlPeriod.DataTextField = "NAME"; //dropdownlist的Text的字段                
                ddlPeriod.DataBind();

                if (Request.QueryString["SN"] != null && Request.QueryString["SN"] != "")
                {
                    Show(Request.QueryString["SN"]);
                    this.lblTitle.InnerHtml = "采购&nbsp;>>&nbsp;补货申请&nbsp;>>&nbsp;编辑";
                }
                else
                {
                    gridView.DataSource = getDataTable();
                    gridView.DataBind();
                    try
                    {
                        BaseMaster model = bCommon.GetCenterWarehouse();
                        this.txtFromWarehouseCode.Text = model.Code;
                        this.lblFromWarehouse.Text = model.Code + " " + model.Name;
                        this.txtDepartualDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                        this.txtArrivalDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                        this.txtUserId.Text = UserTable.USER_ID;
                        this.lblUserName.Text = UserTable.TRUE_NAME;
                        DataSet ds = bll.GetWarehouseName(UserTable.DEPARTMENT_CODE);
                        this.txtToWarehouseCode.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                        this.lblToWarehouseName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();

                    }
                    catch { }
                    this.btnClear.Enabled = false;
                    this.lblTitle.InnerHtml = "采购&nbsp;>>&nbsp;补货申请&nbsp;>>&nbsp;新建";
                }
            }
            this.Title = this.lblTitle.InnerText;
        }

        private void Show(string slipNumber)
        {
            BllPurchaseRequisitionTable prTable = bll.GetModel(slipNumber);
            this.txtSlipNumber.Text = prTable.SLIP_NUMBER;
            this.txtUserId.Text = prTable.CREATE_USER;
            this.lblUserName.Text = prTable.USER_NAME;
            this.txtProductGroupCode.Text = prTable.PRODUCT_GROUP_CODE;
            this.lblProductGroupName.Text = prTable.PRODUCT_GROUP_NAME;
            this.txtToWarehouseCode.Text = prTable.TO_WAREHOUSE_CODE;
            this.lblToWarehouseName.Text = prTable.TO_WAREHOUSE_NAME;
            this.txtDepartualDate.Text = prTable.DEPARTUAL_DATE.ToString("yyyy/MM/dd");
            this.txtArrivalDate.Text = prTable.ARRIVAL_DATE.ToString("yyyy/MM/dd");
            this.txtFromWarehouseCode.Text = prTable.FROM_WAREHOUSE_CODE;
            this.lblFromWarehouse.Text = prTable.FROM_WAREHOUSE_CODE + " " + prTable.FROM_WAREHOUSE_NAME;
            this.ddlPeriod.SelectedValue = prTable.REQUISITION_PERIOD;
            this.lblGroupStock.Text = String.Format("{0:F0}", prTable.GROUP_STOCK);
            this.lblAreaPercentage.Text = prTable.AREA_PERCENTAGE + "%";
            this.lblAreaMaxQuantity.Text = String.Format("{0:F0}", prTable.AREA_MAX_QUANTITY);
            this.lblShopPercentage.Text = prTable.SHOP_PERCENTAGE + "%";
            this.lblShopMaxQuantity.Text = String.Format("{0:F0}", prTable.SHOP_MAX_QUANTITY);

            ds = bll.GetLineList(prTable.SLIP_NUMBER);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            }
            gridView.DataSource = ds;
            gridView.DataBind();

            this.txtProductGroupCode.Enabled = false;
            this.btnProductGroup.Enabled = false;
            this.btnSearch.Enabled = false;
            this.btnClear.Enabled = false;
            this.ddlPeriod.Enabled = false;
        }

        private DataTable getDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCT_CODE", Type.GetType("System.String"));
            dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SIZE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("WAREHOUSE_STOCK", Type.GetType("System.Decimal"));
            dt.Columns.Add("SHOP_STOCK", Type.GetType("System.Decimal"));
            dt.Columns.Add("BEFORE_SALES_QUANTITY", Type.GetType("System.Decimal"));
            dt.Columns.Add("REQUISTION_QUANTITY", Type.GetType("System.Decimal"));
            dt.Columns.Add("BOX_NUMBER", Type.GetType("System.Int32"));
            dt.Columns.Add("UNIT_CODE", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }


        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtBSQ = (TextBox)e.Row.FindControl("txtBeforeSalesQuantity");
                TextBox txtRQ = (TextBox)e.Row.FindControl("txtRequistionQuantity");
                TextBox txtBN = (TextBox)e.Row.FindControl("txtBoxNumber");
                if (e.Row.Cells[0].Text != "&nbsp;" && e.Row.Cells[0].Text != "")
                {
                    txtBSQ.Attributes.Add("onfocus", "this.select();");
                    txtRQ.Attributes.Add("onfocus", "this.select();");
                    txtBN.Attributes.Add("onfocus", "this.select();");
                    txtBSQ.Attributes.Add("onkeyup", "this.value=this.value.replace(/[^\\d]/g,'') ");
                    txtRQ.Attributes.Add("onkeyup", "this.value=this.value.replace(/[^\\d]/g,'') ");
                    txtBN.Attributes.Add("onkeyup", "this.value=this.value.replace(/[^\\d]/g,'') ");
                }
                else
                {
                    txtBSQ.Visible = false;
                    txtRQ.Visible = false;
                    txtBN.Visible = false;
                }
                e.Row.Cells[9].Visible = false;
            }
        }

        protected void Product_Group_Changed(object sender, EventArgs e)
        {
            if (this.txtProductGroupCode.Text.Trim() == "")
            {
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_PRODUCT_GROUP", txtProductGroupCode.Text.Trim(), "");
            if (table != null)
            {

                this.txtProductGroupCode.Text = table.Code;
                this.lblProductGroupName.Text = table.Name;
                ShowReferenceInfo();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品种类不存在！\");document.getElementById('" + txtProductGroupCode.ClientID + "').value='';", true);
                this.lblProductGroupName.Text = "";
            }
        }

        protected void Before_Sales_Quantity_Changed(object sender, EventArgs e)
        {
            TextBox qty = (TextBox)sender;
            if (qty.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"到货前销售量不能为空！\");document.getElementById('" + qty.ClientID + "').value='0';", true);
            }
        }
        protected void Requisition_Quantity_Changed(object sender, EventArgs e)
        {
            TextBox qty = (TextBox)sender;
            if (qty.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"申请数量不能为空！\");document.getElementById('" + qty.ClientID + "').value='0';", true);
            }
        }
        protected void Box_Number_Changed(object sender, EventArgs e)
        {
            TextBox qty = (TextBox)sender;
            if (qty.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"箱号不能为空！\");document.getElementById('" + qty.ClientID + "').value='1';", true);
            }
        }


        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnProductGroup":
                    Product_Group_Changed(sender, e);
                    break;
                case "btnSearch":
                    Search(sender, e);
                    break;
                case "btnClear":
                    txtProductGroupCode.Enabled = true;
                    btnProductGroup.Enabled = true;
                    btnSearch.Enabled = true;
                    btnClear.Enabled = false;
                    gridView.DataSource = getDataTable();
                    gridView.DataBind();
                    break;
                case "btnSave":
                    if (Save(sender, e) > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存成功！\");processCloseAndRefreshParent();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"订单保存失败！\");", true);
                    }
                    break;
                case "btnMonitor":
                    if (Save(sender, e) > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "winOpen('RequisitionMonitor.aspx?','SN=" + txtSlipNumber.Text + "','280','1020');", true);
                    }
                    break;
            }
            return true;
        }

        private int Save(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return 0;
            }


            BllPurchaseRequisitionTable prTable = new BllPurchaseRequisitionTable();
            prTable.SLIP_NUMBER = txtSlipNumber.Text.Trim();
            prTable.DEPARTUAL_DATE = Convert.ToDateTime(txtDepartualDate.Text.Trim());
            prTable.ARRIVAL_DATE = Convert.ToDateTime(txtArrivalDate.Text.Trim());
            prTable.CREATE_USER = txtUserId.Text.Trim();
            prTable.LAST_UPDATE_USER = UserTable.USER_ID;
            prTable.PRODUCT_GROUP_CODE = txtProductGroupCode.Text.Trim();
            prTable.REQUISITION_PERIOD = ddlPeriod.SelectedItem.Value;
            prTable.FROM_WAREHOUSE_CODE = txtFromWarehouseCode.Text.Trim();
            prTable.TO_WAREHOUSE_CODE = txtToWarehouseCode.Text.Trim();
            prTable.GROUP_STOCK = Convert.ToDecimal(lblGroupStock.Text.Trim());
            prTable.AREA_PERCENTAGE = Convert.ToDecimal(lblAreaPercentage.Text.Trim().Replace("%", ""));
            prTable.AREA_MAX_QUANTITY = Convert.ToDecimal(lblAreaMaxQuantity.Text.Trim());
            prTable.SHOP_PERCENTAGE = Convert.ToDecimal(lblShopPercentage.Text.Trim().Replace("%", ""));
            prTable.SHOP_MAX_QUANTITY = Convert.ToDecimal(lblShopMaxQuantity.Text.Trim());
            prTable.STATUS_FLAG = CConstant.INIT;
            BllPurchaseRequisitionLineTable prlTable = null;
            int i = 1;
            foreach (GridViewRow gr in gridView.Rows)
            {
                if (gr.Cells[0].Text != "&nbsp;" && gr.Cells[0].Text != "")
                {
                    TextBox txtRQty = (TextBox)gr.FindControl("txtRequistionQuantity");
                    prlTable = new BllPurchaseRequisitionLineTable();
                    TextBox txtBSQty = (TextBox)gr.FindControl("txtBeforeSalesQuantity");
                    TextBox txtBQty = (TextBox)gr.FindControl("txtBoxNumber");
                    prlTable.SLIP_NUMBER = prTable.SLIP_NUMBER;
                    prlTable.LINE_NUMBER = i++;
                    prlTable.PRODUCT_CODE = gr.Cells[0].Text.Trim();
                    prlTable.WAREHOUSE_STOCK = Convert.ToDecimal(gr.Cells[4].Text.Trim());
                    prlTable.SHOP_STOCK = Convert.ToDecimal(gr.Cells[5].Text);
                    prlTable.BEFORE_SALES_QUANTITY = Convert.ToDecimal(txtBSQty.Text.Trim());
                    prlTable.REQUISTION_QUANTITY = Convert.ToDecimal(txtRQty.Text.Trim());
                    prlTable.BOX_NUMBER = Convert.ToInt32(txtBQty.Text.Trim());
                    prlTable.UNIT_CODE = gr.Cells[9].Text.Trim();
                    prTable.ADD_LINES(prlTable);
                }
            }
            if (prTable.SLIP_NUMBER != "")
            {
                return bll.Update(prTable);
            }
            else
            {
                prTable.SLIP_NUMBER = CommonManage.GetSeq("PR");
                if (bll.Insert(prTable) > 0)
                {
                    this.txtSlipNumber.Text = prTable.SLIP_NUMBER;
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        private bool CheckInput()
        {
            string message = "";
            if (string.IsNullOrEmpty(txtProductGroupCode.Text.Trim()))
            {
                message += "商品种类不能为空!\\n";
            }
            if (string.IsNullOrEmpty(this.txtArrivalDate.Text.Trim()))
            {
                message += "申请到货日期不能为空!\\n";
            }
            if (message != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                return false;
            }
            return true;
        }

        private void Search(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductGroupCode.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品种类不能为空！\");", true);
                return;
            }

            ds = bll.GetStockList(txtFromWarehouseCode.Text.Trim(), txtToWarehouseCode.Text.Trim(), txtProductGroupCode.Text.Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtProductGroupCode.Enabled = false;
                btnProductGroup.Enabled = false;
                this.btnSearch.Enabled = false;
                this.btnClear.Enabled = true;
            }
            else
            {
                txtProductGroupCode.Enabled = true;
                btnProductGroup.Enabled = true;
                this.btnSearch.Enabled = true;
                this.btnClear.Enabled = false;
            }
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        private void ShowReferenceInfo()
        {
            Hashtable ht = new Hashtable();
            ht = bll.GetReferenceInfo(txtFromWarehouseCode.Text.Trim(), txtToWarehouseCode.Text.Trim(), txtProductGroupCode.Text.Trim(), ((BaseUserTable)HttpContext.Current.Session["UserInfo"]).DEPARTMENT_CODE);

            this.lblGroupStock.Text = ht["GroupStock"] != null ? String.Format("{0:F0}", ht["GroupStock"]) : "0";
            this.lblAreaPercentage.Text = (ht["AreaPercentage"] != null ? ht["AreaPercentage"].ToString() : "0") + " %";
            this.lblAreaMaxQuantity.Text = ht["AreaMaxQuantity"] != null ? String.Format("{0:F0}", ht["AreaMaxQuantity"]) : "0";
            this.lblShopPercentage.Text = (ht["ShopPercentage"] != null ? ht["ShopPercentage"].ToString() : "0") + " %";
            this.lblShopMaxQuantity.Text = ht["ShopMaxQuantity"] != null ? String.Format("{0:F0}", ht["ShopMaxQuantity"]) : "0";
        }
    }//end class
}

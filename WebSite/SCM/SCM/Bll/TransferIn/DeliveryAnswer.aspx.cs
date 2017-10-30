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
using System.Text;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.TransferIn
{
    public partial class DeliveryAnswer : BasePage
    {
        BReceivingPlan bll = new BReceivingPlan();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int currentPage = 1;
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region page init
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                this.txtArrivalFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtArrivalToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtStockFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtStockToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);
        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }
        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("PURCHASE_SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("PURCHASE_LINE_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("INPUT_TYPE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("DEPARTUAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("UNIT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_NAME", Type.GetType("System.String"));

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
                //编辑按钮
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnM.Attributes.Add("onclick", "return winOpen('DeliveryAnswerModify.aspx?','SN=" + btnM.CommandArgument + "','380','420');");

                    //光标移动事件
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnM.Visible = false;
                }
                e.Row.Cells[0].Visible = false;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
        #endregion

        #region textbox change event
        //仓库
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

        //供应商
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

        //商品
        protected void Product_Change(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() == "")
            {
                this.txtProductCode.Text = "";
                this.lblProductName.Text = "";
                return;
            }

            BaseMaster table = bCommon.GetBaseMaster("BASE_PRODUCT", txtProductCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtProductCode.Text = table.Code;
                this.lblProductName.Text = table.Name;
            }
            else
            {
                this.lblProductName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品不存在!\");document.getElementById('" + txtProductCode.ClientID + "').value='';", true);
            }
        }
        #endregion

        #region search

        private void Search(object sender, EventArgs e)
        {
            //获得总的记录数
            int recordCount = bll.GetRecordCount(getConduction());
            if (recordCount > 0)
            {
                panelPage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"您查询的信息不存在！\");processCloseAndRefreshParent();", true);
                panelPage.Visible = false;
            }
            //将每页显示的数量保存在用户控件
            this.paging.PageSize = PageSize;
            //将数据总条数保存在用户控件
            this.paging.RecorderCount = recordCount;
            BindData();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetReceivingPlanList(strWhere, "PURCHASE_SLIP_NUMBER", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");
            if (txtSlipNumber.Text.Trim() != "")
            {
                sb.AppendFormat(" AND (PURCHASE_SLIP_NUMBER = '{0}')", txtSlipNumber.Text.Trim());
            }
            if (selInputType.Value != "0")
            {
                sb.AppendFormat(" AND INPUT_TYPE = {0}", selInputType.Value);
            }
            if (txtStockFromDate.Text.Trim() != "" && txtStockToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE BETWEEN '{0}' AND '{1}'", txtStockFromDate.Text.Trim(), Convert.ToDateTime(txtStockToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtStockFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE  >= '{0}' ", txtStockFromDate.Text.Trim());
            }
            else if (txtStockToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE  < '{0}' ", Convert.ToDateTime(txtStockToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }

            if (txtArrivalFromDate.Text.Trim() != "" && txtArrivalToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE BETWEEN '{0}' AND '{1}'", txtArrivalFromDate.Text.Trim(), Convert.ToDateTime(txtArrivalToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtArrivalFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE  >= '{0}' ", txtArrivalFromDate.Text.Trim());
            }
            else if (txtArrivalToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE  < '{0}' ", Convert.ToDateTime(txtArrivalToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            if (txtSupplierCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SUPPLIER_CODE = '{0}'", txtSupplierCode.Text.Trim());
            }

            if (txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND TO_WAREHOUSE_CODE = '{0}'", txtWarehouseCode.Text.Trim());
            }

            if (txtProductCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND PRODUCT_CODE = '{0}'", txtProductCode.Text.Trim());
            }
            return sb.ToString();
        }


        #endregion

        #region button click event
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
                case "btnModify":
                    BindData();
                    break;
            }
            return true;
        }
        #endregion
        #region 时间的changed事件
        protected void StockFromDate_Changed(object sender, EventArgs e)
        {
            if (txtStockFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtStockFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtStockFromDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtStockFromDate.Text = Convert.ToDateTime(txtStockFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void StockToDate_Changed(object sender, EventArgs e)
        {
            if (txtStockToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtStockToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtStockToDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtStockToDate.Text = Convert.ToDateTime(txtStockToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtStockFromDate.Text.Trim() != "" && this.txtStockToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtStockToDate.Text) < Convert.ToDateTime(txtStockFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"起始时间不能大于截止时间!\");document.getElementById('" + txtStockToDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
        protected void ArrivalFromDate_Changed(object sender, EventArgs e)
        {

            if (txtArrivalFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtArrivalFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtArrivalFromDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtArrivalFromDate.Text = Convert.ToDateTime(txtArrivalFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void ArrivalToDate_Changed(object sender, EventArgs e)
        {
            if (txtArrivalToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtArrivalToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtArrivalToDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtArrivalToDate.Text = Convert.ToDateTime(txtArrivalToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtArrivalFromDate.Text.Trim() != "" && this.txtArrivalToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtArrivalToDate.Text) < Convert.ToDateTime(txtArrivalFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"起始时间不能大于截止时间!\");document.getElementById('" + txtArrivalToDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
        #endregion
    }
}
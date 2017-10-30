using System;
using System.Collections;
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
using System.Drawing;
using System.Text;
using SCM.Web;
using SCM.Model;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.TransferIn
{
    public partial class ReceiptReturn : BasePage
    {
        BReceipt bll = new BReceipt();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
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
            dt.Columns.Add("SUPPLIER_NAME", Type.GetType("System.String"));
            dt.Columns.Add("RECIEPT_SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("REASON_NAME", Type.GetType("System.String"));
            dt.Columns.Add("WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("UNIT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE1", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE2", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE3", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
            }
            return true;
        }

        private void Search(object sender, EventArgs e)
        {
            int recordCount = bll.GetReturnCount(getConduction());
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

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" STATUS_FLAG <>" + CConstant.DELETE);
            sb.Append(" AND INPUT_TYPE = " + CConstant.INPUT_TYPE_HEADER);
            if (this.txtProductCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND PRODUCT_CODE = '{0}'", txtProductCode.Text.Trim());
            }
            if (this.txtSlipNumber.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER LIKE '{0}'", txtSlipNumber.Text.Trim());
            }
            if (this.txtSupplierCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SUPPLIER_CODE = '{0}'", txtSupplierCode.Text.Trim());
            }
             if (this.txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND RETURN_WAREHOUSE_CODE = '{0}'", txtWarehouseCode.Text.Trim());
            }
            return sb.ToString();
        }
      
        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetReturnList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
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

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库仓库不存在!\");", true);
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
            }
        }
        protected void Product_Changed(object sender, EventArgs e)
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

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库仓库不存在!\");", true);
                this.txtProductCode.Text = "";
                this.lblProductName.Text = "";
            }
        }
    }
}

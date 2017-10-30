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

namespace SCM.Web.Stock
{
    public partial class StockList : BasePage
    {
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int currentPage = 1;
        BStock bll = new BStock();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {

            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                this.RadioButton1.Checked = true;
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
            dt.Columns.Add("WAREHOUSE_CODE", Type.GetType("System.String"));
            dt.Columns.Add("WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_CODE", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SIZE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.Decimal"));
            dt.Columns.Add("SP_QUANTITY", Type.GetType("System.Decimal"));
            dt.Columns.Add("RP_QUANTITY", Type.GetType("System.Decimal"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
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

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"商品不存在!\");", true);
                this.txtProductCode.Text = "";
                this.lblProductName.Text = "";
            }
        }
        protected void ProductGroupCode_Chanage(object sender, EventArgs e)
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
                this.lblProductGroupName.Text = table.Name;
                this.txtProductGroupCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"种类不存在！\");", true);
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
            }
        }
        protected void ClolorCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtColorCode.Text.Trim() == "")
            {
                this.lblColorName.Text = "";
                this.txtColorCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_COLOR", txtColorCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblColorName.Text = table.Name;
                this.txtColorCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"颜色不存在！\");", true);
                this.lblColorName.Text = "";
                this.txtColorCode.Text = "";
            }
        }
        protected void SysleCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtStyleCode.Text.Trim() == "")
            {
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_STYLE", txtStyleCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblStyleName.Text = table.Name;
                this.txtStyleCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"款式不存在！\");", true);
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
            }
        }
        protected void SizeCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtSizeCode.Text.Trim() == "")
            {
                this.lblSizeName.Text = "";
                this.txtSizeCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_SIZE", txtSizeCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblSizeName.Text = table.Name;
                this.txtSizeCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"尺码不存在！\");", true);
                this.lblSizeName.Text = "";
                this.txtSizeCode.Text = "";
            }
        }


        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                string[] sArray = btnS.CommandArgument.ToString().Split('|');
                string warehouse = Convert.ToString(sArray[0]);
                string product = Convert.ToString(sArray[1]);
                string param = "WAREHOUSE_CODE=" + warehouse;
                param += "&PRODUCT_CODE=" + product;
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnS.Attributes.Add("onclick", "return winOpen('StockDetails.aspx?','" + param + " ','550','1020')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnS.Visible = false;
                }
            }
        }

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
                case "btnExcel":
                    GetExcel();
                    break;
            }
            return true;
        }

        private void GetExcel()
        {
            string strWhere = getConduction();
            DataSet ds = bll.GetStockInfo(getConduction());
            DataTable dt = ds.Tables[0];
            CommonUtil.DataTable2Excel(dt);
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetListByPage(strWhere, "WAREHOUSE_CODE,T.STYLE_CODE", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Search(object sender, EventArgs e)
        {
            DataSet ta = bll.GetRecordCount(getConduction());
            string to = ta.Tables[0].Rows[0]["STOCK"].ToString();
            string[] To = to.Split('.');
            this.txtStockTote.Text = To[0].ToString();
            string ke = ta.Tables[0].Rows[0]["OUTSTOCK"].ToString();
            string[] Ke = ke.Split('.');
            this.txtStouckEnter.Text = Ke[0].ToString();
            string ko = ta.Tables[0].Rows[0]["ENTERSTOCK"].ToString();
            string[] Ko = ko.Split('.');
            this.txtStockOut.Text = Ko[0].ToString();
            int recordCount = Convert.ToInt32(ta.Tables[0].Rows[0]["CODE"].ToString());
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
            sb.Append("1=1");
            if (this.txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND WAREHOUSE_CODE = '{0}'", this.txtWarehouseCode.Text);
            }
            if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
            {
                if (this.txtProductGroupCode.Text.Trim() != "")
                {
                    sb.AppendFormat(" AND PRODUCT_GROUP_CODE = '{0}'", this.txtProductGroupCode.Text.Trim());
                }
                if (this.txtColorCode.Text.Trim() != "")
                {
                    sb.AppendFormat(" AND COLOR_CODE = '{0}'", this.txtColorCode.Text.Trim());
                }
                if (this.txtStyleCode.Text.Trim() != "")
                {
                    sb.AppendFormat(" AND STYLE_CODE = '{0}'", this.txtStyleCode.Text.Trim());
                }
                if (this.txtSizeCode.Text.Trim() != "")
                {
                    sb.AppendFormat(" AND SIZE_CODE = '{0}'", this.txtSizeCode.Text.Trim());
                }
            }
            else
            {
                sb.AppendFormat(" AND PRODUCT_CODE = '{0}'", this.txtProductCode.Text.Trim());
            }

            if (this.RadioButton2.Checked == true)
            {
                sb.AppendFormat(" AND QUANTITY >{0}", 0);
            }
            else if (this.RadioButton3.Checked == true)
            {
                sb.AppendFormat(" AND QUANTITY ={0}", 0);
            }
            else if (this.RadioButton4.Checked == true)
            {
                sb.AppendFormat(" AND QUANTITY <{0}", 0);
            }
            return sb.ToString();
        }

    }
}

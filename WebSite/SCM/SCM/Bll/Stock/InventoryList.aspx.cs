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
using SCM.Common;
using System.Text;
using log4net;

namespace SCM.Web.Stock
{
    public partial class InventoryList : BasePage
    {
        BStock bll = new BStock();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('InventoryStart.aspx?','','150','400');");
                this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
            dt.Columns.Add("WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("INVENTORY_START_DATE", Type.GetType("System.String"));
            dt.Columns.Add("INVENTORY_END_DATE", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_FLAG", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_NAME", Type.GetType("System.String"));
            dt.Columns.Add("GROUP_NAME", Type.GetType("System.String"));
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
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnM.Attributes.Add("onclick", "return winOpen('InventoryModify.aspx?','SN=" + btnM.CommandArgument + "','600','1020');");

                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnD.Visible = false;
                    btnM.Visible = false;
                }
            }
        }

        #region processChange event
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"盘点仓库不存在!\");document.getElementById('" + txtWarehouseCode.ClientID + "').value='';", true);
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
        protected void ToDate_Changed(object sender, EventArgs e)
        {
            if (txtToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtToDate.Text = Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtFromDate.Text.Trim() != "" && this.txtToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(txtFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"起始时间不能大于截止时间!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
        #endregion

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
                case "btnNew":
                    Search(sender, e);
                    break;
                case "btnExcel":
                    GetExcel(sender, e);
                    break;
                case "btnModify":
                    BindData();
                    break;
                case "btnDelete":
                    bll.DeleteInventory(((LinkButton)sender).CommandArgument);
                    Search(sender, e);
                    break;
            }
            return true;
        }

        private void GetExcel(object sender, EventArgs e)
        {
            DataSet ds = bll.GetInventoryScheduleInfo(getConduction());
            DataTable da = ds.Tables[0];
            CommonUtil.DataTable2Excel(da);
        }

        private void Search(object sender, EventArgs e)
        {
            int recordCount = bll.GetInventoryScheduleRecordCount(getConduction());
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
            if (this.txtSlipNumber.Text != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER = '{0}'", this.txtSlipNumber.Text);
            }
            else
            {
                if (this.txtWarehouseCode.Text != "")
                {
                    sb.AppendFormat(" AND WAREHOUSE_CODE = '{0}'", this.txtWarehouseCode.Text);
                }

                if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
                {
                    sb.AppendFormat(" AND CREATE_DATE_TIME BETWEEN '{0}' AND '{1}'", txtFromDate.Text.Trim(), Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
                }
                else if (txtFromDate.Text.Trim() != "")
                {
                    sb.AppendFormat(" AND CREATE_DATE_TIME  >= '{0}' ", txtFromDate.Text.Trim());
                }
                else if (txtToDate.Text.Trim() != "")
                {
                    sb.AppendFormat(" AND CREATE_DATE_TIME  < '{0}' ", Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
                }

                if (rdo2.Checked)
                {
                    sb.AppendFormat(" AND STATUS_FLAG  = {0} ", CConstant.INIT);
                }
                else if (rdo3.Checked)
                {
                    sb.AppendFormat(" AND STATUS_FLAG  = {0} ", CConstant.NORMAL);
                }
            }
            return sb.ToString();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetInventoryScheduleList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

    }//end class
}

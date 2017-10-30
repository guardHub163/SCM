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

namespace SCM.Web.Purchase
{
    public partial class RequidtitonAuditSearch : BasePage
    {
        BPurchaseRequisition bll = new BPurchaseRequisition();
        DataSet ds = new DataSet();
        BCommon bCommon = new BCommon();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                try
                {
                    gridView.DataSource = InitDataTable();
                    gridView.DataBind();
                    this._userTable = (BaseUserTable)Session["UserInfo"];
                    this.txtUserCode.Text = _userTable.USER_ID;
                    this.lblUserName.Text = _userTable.TRUE_NAME;
                    DataSet dt = bll.GetWarehouseName(_userTable.DEPARTMENT_CODE);
                    this.lblWarehouseName.Text = dt.Tables[0].Rows[0]["NAME"].ToString();
                    this.txtWarehouseCode.Text = dt.Tables[0].Rows[0]["CODE"].ToString();
                    this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                }
                catch { }
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
            dt.Columns.Add("FROM_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("TO_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("REQUISITION_NAME", Type.GetType("System.String"));
            dt.Columns.Add("DEPARTUAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("SHOP_MAX_QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("REQUISTION_QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_FLAG", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }

        private void Search(object sender, EventArgs e)
        {
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

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" STATUS_FLAG=0");
            if (this.txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND TO_WAREHOUSE_CODE like '%{0}%'", this.txtWarehouseCode.Text.Trim());
            }
            if (this.txtFromDate.Text.Trim() != "" && this.txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE BETWEEN '{0}' AND '{1}'", txtFromDate.Text.Trim(), Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (this.txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE > '{0}'", this.txtFromDate.Text.Trim());
            }
            else if (this.txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE < '{0}'", Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetListByPage(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                LinkButton btnE = (LinkButton)e.Row.FindControl("btnEnd");
                LinkButton btnO = (LinkButton)e.Row.FindControl("btnMonitor");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnE.Attributes.Add("onclick", "return confirm(\"你确认要审核吗?\")");
                    btnM.Attributes.Add("onclick", "return winOpen('RequiditionAuditing.aspx?','SN=" + btnM.CommandArgument + "','630','1020')");
                    btnO.Attributes.Add("onclick", "return winOpen('RequisitionMonitor.aspx?','SN=" + btnO.CommandArgument + "','280','1020')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnM.Visible = false;
                    btnE.Visible = false;
                    btnO.Visible = false;
                }
            }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":                   //查询
                    Search(sender, e);
                    break;
                case "btnMonitor":
                    BindData();
                    break;
                case "btnModify":                   //编辑
                    BindData();
                    break;
                case "btnEnd":
                    //try
                    //{
                    LinkButton bte = (LinkButton)sender;
                    if (bll.Audit(bte.CommandArgument) > 0)
                    {
                        BindData();
                    }
                    //}
                    //catch { }
                    break;
            }
            return true;
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
        protected void User_Change(object sender, EventArgs e)
        {
            if (this.txtUserCode.Text.Trim() == "")
            {
                this.lblUserName.Text = "";
                this.txtUserCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_USER", this.txtUserCode.Text, "");
            if (table != null)
            {
                this.lblUserName.Text = table.Name;
                this.txtUserCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"人员不存在！\");", true);
                this.lblUserName.Text = "";
                this.txtUserCode.Text = "";
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
    }
}

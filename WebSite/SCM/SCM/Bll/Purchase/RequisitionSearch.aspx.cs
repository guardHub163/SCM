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
    public partial class RequisitionSearch : BasePage
    {
        BPurchaseRequisition bll = new BPurchaseRequisition();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
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
                    this.txtUserId.Text = _userTable.USER_ID;
                    this.lblUserName.Text = _userTable.TRUE_NAME;
                    DataSet dt = bll.GetWarehouseName(_userTable.DEPARTMENT_CODE);
                    this.lblWarehouseName.Text = dt.Tables[0].Rows[0]["NAME"].ToString();
                    this.txtWarehouseCode.Text = dt.Tables[0].Rows[0]["CODE"].ToString();
                    this.txtFromDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
                    this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                }
                catch { }
                btnNew.Attributes.Add("onclick", "return winOpen('RequiditionAdd.aspx?','','630','1020');");
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
            sb.Append(" STATUS_FLAG <>" + CConstant.DELETE);
            if (this.txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND TO_WAREHOUSE_CODE like '%{0}%'", this.txtWarehouseCode.Text.Trim());
            }
            if (this.txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE > '{0}'", this.txtFromDate.Text.Trim());
            }

            if (this.txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE < '{0}'", Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            if (this.rdo1.Checked)
            {
                sb.Append(" AND STATUS_FLAG like '0'");
            }
            if (this.rdo2.Checked)
            {
                sb.Append(" AND STATUS_FLAG like '1'");
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
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnM.Attributes.Add("onclick", "return winOpen('RequiditionAdd.aspx?','SN=" + btnM.CommandArgument + "','630','1020')");
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\");");

                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnM.Visible = false;
                    btnD.Visible = false;
                }
            }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":                   //查询
                    if (CheckInput())
                    {
                        Search(sender, e);
                    }
                    break;
                case "btnNew":                      //新建
                    BindData();
                    break;
                case "btnModify":                   //编辑
                    BindData();
                    break;
                case "btnDelete":
                    //try
                    //{
                    LinkButton btn = (LinkButton)sender;
                    bll.Delete(btn.CommandArgument);
                    BindData();
                    //}
                    //catch { }
                    break;

            }
            return true;
        }

        protected bool CheckInput()
        {
            bool b = true;

            if (!PageValidate.IsDateTimeOrEmpty(txtFromDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtFromDate.ClientID + "').value='';", true);
                b = false;
            }
            else if (!PageValidate.IsDateTimeOrEmpty(txtToDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                b = false;
            }


            return true;

        }



    }//end class
}

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
using SCM.Common;
using SCM.Model;
using System.Reflection;
using log4net;

namespace SCM.Web.VipCustomer
{
    public partial class List : BasePage
    {
        BVipCustomer bll = new BVipCustomer();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','460','420');");
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
            dt.Columns.Add("CODE", Type.GetType("System.String"));
            dt.Columns.Add("NAME", Type.GetType("System.String"));
            dt.Columns.Add("DEPARTEMENT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ADDRESS", Type.GetType("System.String"));
            dt.Columns.Add("QQ", Type.GetType("System.String"));
            dt.Columns.Add("EMAIL", Type.GetType("System.String"));
            dt.Columns.Add("WW", Type.GetType("System.String"));
            dt.Columns.Add("BIRTH_DATE", Type.GetType("System.String"));
            dt.Columns.Add("DISCOUNT_RATE", Type.GetType("System.String"));
            dt.Columns.Add("POINTS", Type.GetType("System.String"));
            dt.Columns.Add("LAST_SALES_DATE", Type.GetType("System.DateTime"));     
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"您查询的信息不存在！\");processCloseAndRefreshParent();", true);
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
            ds = bll.GetList(strWhere, "LAST_SALES_DATE desc", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
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
            sb.Append("STATUS_FLAG <>" + CConstant.DELETE);
            if (this.txtCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CODE like '%{0}%'", this.txtCode.Text.Trim());
            }
            if (this.txtName.Text.Trim() != "")
            {
                sb.AppendFormat(" AND NAME like '%{0}%'", this.txtName.Text.Trim());
            }
            if (this.txtDepartmentCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTMENT_CODE like '%{0}%'", this.txtDepartmentCode.Text.Trim());
            }
            return sb.ToString();
        }

        protected void Department_Change(object sender, EventArgs e)
        {
            if (this.txtDepartmentCode.Text.Trim() == "")
            {
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT", this.txtDepartmentCode.Text, "");
            if (table != null)
            {
                this.lblDepartmentName.Text = table.Name;
                this.txtDepartmentCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "click", "alert(\"部门不存在！\");", true);
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                LinkButton btnSa = (LinkButton)e.Row.FindControl("btnSales");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnS.Attributes.Add("onclick", "return winOpen('Show.aspx?','code=" + btnS.CommandArgument + "','570','420')");
                    // btnSa.Attributes.Add("onclick", "document.location.href='/SCM/Base/VipCustomer/LaseCustomeLog.aspx?id=" + param + "';return false;");
                    btnSa.Attributes.Add("onclick", "return winOpen('LaseCustomeLog.aspx?','code=" + btnSa.CommandArgument + "','450','1020')");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','code=" + btnM.CommandArgument + "','460','420')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnD.Visible = false;
                    btnS.Visible = false;
                    btnM.Visible = false;
                    btnSa.Visible = false;
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
                case "btnNew":
                    Search(sender, e);
                    break;
                case "btnModify":
                    BindData();
                    break;
                case "btnDelete":
                    try
                    {
                        LinkButton btn = (LinkButton)sender;
                        bll.Delete(btn.CommandArgument);
                        BindData();
                    }
                    catch { }
                    break;
            }
            return true;
        }

    }
}
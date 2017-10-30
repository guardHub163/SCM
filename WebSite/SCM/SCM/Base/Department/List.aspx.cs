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
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.Department
{
    public partial class List : BasePage
    {
        BDepartment bll = new BDepartment();
        DataSet ds = new DataSet();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','230','420');");
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
            dt.Columns.Add("PARENT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE1", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE2", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE3", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
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
            if (this.txtDepartCode.Text != "")
            {
                sb.AppendFormat(" AND CODE like '%{0}%'", this.txtDepartCode.Text);
            }
            if (this.txtDepartName.Text != "")
            {
                sb.AppendFormat(" AND NAME like '%{0}%'", this.txtDepartName.Text);
            }
            return sb.ToString();

        }

        public void BindData()
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

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    btnSearch_Click(sender, e);
                    break;
                case "btnNew":
                    btnSearch_Click(sender, e);
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

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnS.Attributes.Add("onclick", "return winOpen('Show.aspx?','code=" + btnS.CommandArgument + "','290','420')");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','code=" + btnM.CommandArgument + "','230','420')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnD.Visible = false;
                    btnS.Visible = false;
                    btnM.Visible = false;
                }
            }
        }

    }
}
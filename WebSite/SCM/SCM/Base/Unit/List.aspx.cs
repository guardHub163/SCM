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
using System.Reflection;
using log4net;

namespace SCM.Web.Unit
{
    public partial class List : BasePage
    {
        BUnit bll = new BUnit();
        DataSet ds = new DataSet();
        int currentPage = 1;
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','200','420');");
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
            sb.Append("STATUS_FLAG <>" + CConstant.DELETE);
            if (this.txtUnitName.Text.Trim() != "")
            {
                sb.AppendFormat(" AND NAME like '%{0}%'", this.txtUnitName.Text.Trim());
            }
            if (this.txtCode.Text.Trim() != "") 
            {
                sb.AppendFormat(" AND CODE like '%{0}%'", this.txtCode.Text.Trim());
            }
            return sb.ToString();
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
                    btnS.Attributes.Add("onclick", "return winOpen('Show.aspx?','code=" + btnS.CommandArgument + "','310','420')");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','code=" + btnM.CommandArgument + "','200','420')");
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
    }
}
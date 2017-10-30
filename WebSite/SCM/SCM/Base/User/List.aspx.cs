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

namespace SCM.Web.User
{
    public partial class List : BasePage
    {
        BCommon bCommon = new BCommon();
        BUser bll = new BUser();
        DataSet ds = new DataSet();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                selUserType.DataSource = bCommon.GetNames("USER_TYPE").Tables[0];
                selUserType.DataTextField = "NAME"; //dropdownlist的Text的字段 
                selUserType.DataValueField = "CODE";//dropdownlist的Value的字段 
                selUserType.DataBind();
                selUserType.Items.Insert(0, new ListItem("全部", ""));

                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','380','420');");
                btnUpdate.Attributes.Add("onclick", "return winOpen('UpdatePassWord.aspx?','','150','410');");
            }
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);
            //将每页显示的数量保存在用户控件
            this.paging.PageSize = PageSize;
        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("USER_ID", Type.GetType("System.String"));
            dt.Columns.Add("TRUE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SEX", Type.GetType("System.String"));
            dt.Columns.Add("PHONE", Type.GetType("System.String"));
            dt.Columns.Add("EMAIL", Type.GetType("System.String"));
            dt.Columns.Add("DEPARTMENT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("USER_TYPE_NAME", Type.GetType("System.String"));

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
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");

                    btnS.Attributes.Add("onclick", "return winOpen('Show.aspx?','id=" + btnS.CommandArgument + "','510','420')");

                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','id=" + btnM.CommandArgument + "','510','420')");

                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor = mouseOverBackgroundColor;");
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

        #region 查询
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
            //将数据总条数保存在用户控件
            this.paging.RecorderCount = recordCount;            
            BindData();
        }
       
        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" STATUS_FLAG <> " + CConstant.DELETE);
            sb.Append(" AND USER_TYPE <> 'Z' ");
            if (txtUserID.Text.Trim() != "")
            {
                sb.AppendFormat(" AND USER_ID like '%{0}%'", txtUserID.Text.Trim());
            }
            if (this.txtTrueName.Text.Trim() != "")
            {
                sb.AppendFormat(" AND TRUE_NAME like '%{0}%'", txtTrueName.Text.Trim());
            }
            if (this.txtDepartmentCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTMENT_CODE = '{0}'", txtDepartmentCode.Text.Trim());
            }
            if (this.selUserType.SelectedItem.Value.Trim() != "")
            {
                sb.AppendFormat(" AND USER_TYPE = '{0}'", this.selUserType.SelectedItem.Value.Trim());
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
                case "btnModify":
                    BindData();
                    break;
                case "btnDelete":
                    try
                    {
                        LinkButton btn = (LinkButton)sender;
                        bll.Delete(int.Parse(btn.CommandArgument));
                        BindData();
                    }
                    catch { }
                    break;
            }
            return true;
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"部门不存在！\");", true);
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
            }
        }
    }//end class
}

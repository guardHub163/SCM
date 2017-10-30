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

namespace SCM.Web.News
{
    public partial class List : BasePage
    {
        BNews bll = new BNews();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {

                selNewsType.DataSource = bCommon.GetNames("NEW_TYPE").Tables[0];
                selNewsType.DataTextField = "NAME"; //dropdownlist的Text的字段 
                selNewsType.DataValueField = "CODE";//dropdownlist的Value的字段 
                selNewsType.DataBind();

                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                btnNew.Attributes.Add("onclick", "return winOpen('Add.aspx?','','330','420');");
                this.txtPublishFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtPublishToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
            dt.Columns.Add("ID", Type.GetType("System.Decimal"));
            dt.Columns.Add("NEWS_TITLE", Type.GetType("System.String"));
            dt.Columns.Add("CREATE_USER", Type.GetType("System.String"));
            dt.Columns.Add("TYPE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("PUBLISH_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("NEWS_CONTENT", Type.GetType("System.String"));
            dt.Columns.Add("CREATE_NAME", Type.GetType("System.String"));
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
                        bll.Delete(Convert.ToDecimal(btn.CommandArgument));
                        BindData();
                    }
                    catch { }
                    break;
            }
            return true;
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetNewsListByPage(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        private void Search(object sender, EventArgs e)
        {
            int recordCount = bll.GetNewsCount(getConduction());
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
            sb.Append(" AND PARENT_ID=" + 0);
            if (this.txtUserCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_USER ='{0}'", txtUserCode.Text.Trim());
            }
            if (this.txtTitle.Text.Trim() != "")
            {
                sb.AppendFormat(" AND NEWS_TITLE LIKE '%{0}%'", txtTitle.Text.Trim());
            }
            if (this.selNewsType.SelectedItem.Value.Trim() != "")
            {
                sb.AppendFormat(" AND NEWS_TYPE ='{0}'", this.selNewsType.SelectedItem.Value.Trim());
            }
            if (txtPublishFromDate.Text.Trim() != "" && txtPublishToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND PUBLISH_DATE BETWEEN '{0}' AND '{1}'", txtPublishFromDate.Text.Trim(), Convert.ToDateTime(txtPublishToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtPublishFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND PUBLISH_DATE  >= '{0}' ", txtPublishFromDate.Text.Trim());
            }
            else if (txtPublishToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND PUBLISH_DATE  <= '{0}' ", Convert.ToDateTime(txtPublishToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();
        }



        protected void PublishFromDate_Changed(object sender, EventArgs e)
        {
            if (txtPublishFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtPublishFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtPublishFromDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtPublishFromDate.Text = Convert.ToDateTime(txtPublishFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void PublishToDate_Changed(object sender, EventArgs e)
        {
            if (txtPublishToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtPublishToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtPublishToDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtPublishToDate.Text = Convert.ToDateTime(txtPublishToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtPublishFromDate.Text.Trim() != "" && this.txtPublishToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtPublishToDate.Text) < Convert.ToDateTime(txtPublishFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"起始时间不能大于截止时间!\");document.getElementById('" + txtPublishToDate.ClientID + "').value='';", true);
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
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                string[] sArray = btnM.CommandArgument.ToString().Split('|');
                string Id = Convert.ToString(sArray[0]);
                string Name = Convert.ToString(sArray[1]);
                _userTable = (BaseUserTable)HttpContext.Current.Session["UserInfo"];
                if (Name != _userTable.USER_ID)
                {
                    btnM.Enabled = false;
                    btnD.Enabled = false;
                }
                string param = "ID=" + Id;
                param += "&CREATE_USER=" + Name;
                if (e.Row.Cells[6].Text.Length > 15)
                {
                    e.Row.Cells[6].Text = e.Row.Cells[6].Text.Substring(0, 15) + "...";
                }
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnS.Attributes.Add("onclick", "document.location.href='ShowInfo.aspx?id=" + btnS.CommandArgument + "';return false;");
                    btnM.Attributes.Add("onclick", "return winOpen('Modify.aspx?','" + param + "','330','420')");
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
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
    }
}
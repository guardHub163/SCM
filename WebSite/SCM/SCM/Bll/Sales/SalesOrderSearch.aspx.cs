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

namespace SCM.Web.Sales
{
    public partial class SalesOrderSearch : BasePage
    {
        BSalesOrder bll = new BSalesOrder();
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
                this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this._userTable = (BaseUserTable)Session["UserInfo"];
                if (_userTable.ROLES_ID <= 5)
                {
                    this.btnDerive.Visible = false;
                }
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
            dt.Columns.Add("DEPARTMENT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_CODE", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("UNIT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SALES_EMPLOYEE", Type.GetType("System.String"));
            dt.Columns.Add("CUSTOMER_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ORI_PRICE", Type.GetType("System.String"));
            dt.Columns.Add("DISCOUNT_RATE", Type.GetType("System.String"));
            dt.Columns.Add("PRICE", Type.GetType("System.DateTime"));
            dt.Columns.Add("AMOUNT", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("POINTS", Type.GetType("System.String"));
            dt.Columns.Add("USED_POINTS", Type.GetType("System.String"));
            dt.Columns.Add("CREATE_DATE_TIME", Type.GetType("System.DateTime"));
            dt.Columns.Add("MEMO", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
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

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {

            switch (btnId)
            {
                case "btnSearch":
                    if (CheckInput())
                    {
                        Search(sender, e);
                    }
                    break;
                case "btnDerive":
                    Derive(sender, e);
                    break;
            }
            return true;
        }

        private void Derive(object sender, EventArgs e)
        {
            string strWhere = getConduction();
            ds = bll.GetAllOrderInfo(strWhere);
            DataTable dt = ds.Tables[0];
            CommonUtil.DataTable2Excel(dt);
        }

        private void Search(object sender, EventArgs e)
        {
            int recordCount = bll.GetSalesOrderCount(getConduction());
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
            sb.Append(" 1=1");
            if (this.txtCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER LIKE '%{0}%'", txtCode.Text.Trim());
            }
            if (this.lblUserName.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SALES_EMPLOYEE LIKE '%{0}%'", txtUserCode.Text.Trim());
            }
            if (this.txtDepartmentCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTMENT_CODE='{0}'", txtDepartmentCode.Text.Trim());
            }
            if (this.txtProductCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND PRODUCT_CODE='{0}'", txtProductCode.Text.Trim());
            }
            if (txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME  >= '{0}' ", txtFromDate.Text.Trim());
            }

            if (txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME  < '{0}' ", CConvert.ToDateTime(txtToDate.Text.Trim()).AddDays(1));
            }
            return sb.ToString();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetSalesOrderList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        private bool CheckInput()
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

            return b;

        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                if (btnS.Text.Trim() != "&nbsp;" && btnS.Text.Trim() != "")
                {
                    btnS.Attributes.Add("onclick", "return winOpen('SalesOrderDetail.aspx?','code=" + btnS.CommandArgument + "','410','1020')");
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnS.Visible = false;
                }
            }
        }
    }
}

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
using System.Reflection;
using log4net;

namespace SCM.Web.Cash
{
    public partial class CashSearch : BasePage
    {
        BCash bll = new BCash();
        DataSet ds = new DataSet();
        BCommon bCommon = new BCommon();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                BankType.DataSource = bCommon.GetNames("BANK").Tables[0];
                BankType.DataTextField = "NAME"; //dropdownlist的Text的字段 
                BankType.DataValueField = "CODE";//dropdownlist的Value的字段 
                BankType.DataBind();
                BankType.Items.Insert(0, new ListItem("全部", ""));

                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BANK_NAME", Type.GetType("System.String"));
            dt.Columns.Add("CASH_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("LAST_UPDATE_TIME", Type.GetType("System.DateTime"));
            dt.Columns.Add("BANK_SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("PROFIT_CASH", Type.GetType("System.Decimal"));
            dt.Columns.Add("LAST_CASH", Type.GetType("System.Decimal"));
            dt.Columns.Add("TAKE_CASH", Type.GetType("System.Decimal"));
            dt.Columns.Add("BALANCE_CASH", Type.GetType("System.Decimal"));
            dt.Columns.Add("SALES_SLIP_NUMBER", Type.GetType("System.Decimal"));
            dt.Columns.Add("MEMO", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetCashInfo(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
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
                    if (CheckInput())
                    {
                        Search(sender, e);
                    }
                    break;
                case "btnExport":
                    Search1(sender, e);
                    break;
            }
            return true;
        }

        private void Search1(object sender, EventArgs e)
        {
            BStaDepGrpSales st = new BStaDepGrpSales();
            BStaDepGrpSizeSales ss = new BStaDepGrpSizeSales();
            int i = st.InsertInfoOne();
            int y = st.InsertInfoTwo();
            int z = st.InsertInfoThree();
            int a = ss.InsertOneDepGrpSize();
            int b = ss.InsertThreeDepGrpSize();
            int c = ss.InsertTwoDepGrpSize();
        }

        private void Search(object sender, EventArgs e)
        {
            int recordCount = bll.GetCashCount(getConduction());
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
            if (txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND LAST_UPDATE_TIME  >= '{0}' ", txtFromDate.Text.Trim());
            }

            if (txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND LAST_UPDATE_TIME  < '{0}' ", Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1));
            }

            if (this.BankType.SelectedItem.Value.Trim() != "")
            {
                sb.AppendFormat(" AND BANK_NAME='{0}'", BankType.SelectedItem.Text.Trim());
            }

            if (this.txtDepartmentCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER LIKE '{0}%'", this.txtDepartmentCode.Text.Trim());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool b = true;
            if (!PageValidate.IsDateTimeOrEmpty(txtFromDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtFromDate.ClientID + "').value='';", true);
                b = false;

            }else if (!PageValidate.IsDateTimeOrEmpty(txtToDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                b = false;

            }

            return b;
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
    }
}

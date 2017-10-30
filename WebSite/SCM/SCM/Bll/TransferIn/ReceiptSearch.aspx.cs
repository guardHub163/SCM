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
using System.Text;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.TransferIn
{
    public partial class ReceiptSearch : BasePage
    {
        BReceipt bll = new BReceipt();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int currentPage = 1;
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
            }
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);

        }

        protected void PageChanged(object sender, int e)
        {
            if (base.ValidateSession())
            {
                BindData();
            }
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("INPUT_TYPE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SUPPLIER_NAME", Type.GetType("System.String"));
            dt.Columns.Add("WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("ARRIVAL_STATUS_NAME", Type.GetType("System.String"));
            dt.Columns.Add("RECEIPT_TYPE", Type.GetType("System.String"));

            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            return dt;

        }

        #region search
        private void Search(object sender, EventArgs e)
        {
            //获得总的记录数
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
            sb.AppendFormat(" STATUS_FLAG <> {0}", CConstant.DELETE);
            if (txtSlipNumber.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER = '{0}'", txtSlipNumber.Text.Trim());
            }
            if (selInputType.Value != "0")
            {
                sb.AppendFormat(" AND INPUT_TYPE = {0}", selInputType.Value);
            }
            if (txtSupplierCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SUPPLIER_CODE = '{0}'", txtSupplierCode.Text.Trim());
            }

            if (txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND TO_WAREHOUSE_CODE = '{0}'", txtWarehouseCode.Text.Trim());
            }

            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE BETWEEN '{0}' AND '{1}'", txtFromDate.Text.Trim(), Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE  >= '{0}' ", txtFromDate.Text.Trim());
            }
            else if (txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE  < '{0}' ", Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();

        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetReceiptList(strWhere, "SLIP_NUMBER", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }


        #endregion

        protected void Supplier_Change(object sender, EventArgs e)
        {
            if (txtSupplierCode.Text.Trim() == "")
            {
                this.txtSupplierCode.Text = "";
                this.lblSupplierName.Text = "";
                return;
            }

            BaseMaster table = bCommon.GetBaseMaster("BASE_SUPPLIER", txtSupplierCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtSupplierCode.Text = table.Code;
                this.lblSupplierName.Text = table.Name;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"供应商不存在！\");", true);
                this.txtSupplierCode.Text = "";
                this.lblSupplierName.Text = "";
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"仓库不存在！\");", true);
                this.txtWarehouseCode.Text = "";
                this.lblWarehouseName.Text = "";
            }
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                if (btnS.Text.Trim() != "&nbsp;" && btnS.Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    string[] argument = btnM.CommandArgument.Split('|');
                    string param = "SN=" + argument[0];
                    if (argument.Length > 1)
                    {
                        param += "&RT=" + argument[1];
                    }
                    btnM.Attributes.Add("onclick", "return winOpen('ReceiptModify.aspx?','" + param + "','630','1020');");
                    btnS.Attributes.Add("onclick", "return winOpen('ReceiptModify.aspx?','SN=" + btnD.CommandArgument + "&SHOW_FLAG=1" + "','630','1020');");


                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");

                    if (_userTable.USER_TYPE.Equals(CConstant.USER_TYPE_D))
                    {
                        btnD.Enabled = false;
                        //btnD.Visible = false;
                        btnM.Enabled = false;
                        //btnM.Visible = false;
                    }
                }
                else
                {
                    btnD.Visible = false;
                    btnM.Visible = false;
                }
            }
        }

        #region button click event
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":                   //查询
                    Search(sender, e);
                    break;
                case "btnNew":                      //新建
                    Search(sender, e);
                    break;
                case "btnModify":                   //编辑
                    BindData();
                    break;
                case "btnDelete":                   //取消
                    bll.Delete(((LinkButton)sender).CommandArgument, _userTable.USER_ID);
                    BindData();
                    break;
            }
            return true;
        }
        #endregion
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
    }
}

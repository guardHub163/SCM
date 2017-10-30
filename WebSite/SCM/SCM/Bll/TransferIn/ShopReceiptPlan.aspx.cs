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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using log4net;
using System.Reflection;

namespace SCM.Web.TransferIn
{
    public partial class ShopReceiptPlan : BasePage
    {
        BTransferPlan bll = new BTransferPlan();
        BPurchaseRequisition bpll = new BPurchaseRequisition();
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
                this._userTable = (BaseUserTable)Session["UserInfo"];
                DataSet dt = bpll.GetWarehouseName(_userTable.DEPARTMENT_CODE);
                if (dt.Tables[0].Rows.Count != 0)
                {
                    this.lblWarehouseName.Text = dt.Tables[0].Rows[0]["NAME"].ToString();
                    this.txtWarehouseCode.Text = dt.Tables[0].Rows[0]["CODE"].ToString();
                }
                this.txtReFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtReToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
            dt.Columns.Add("SHIPMENT_SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SHOP_NAME", Type.GetType("System.String"));
            dt.Columns.Add("DEPARTUAL_DATE", Type.GetType("System.DateTime"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_CODE", Type.GetType("System.String"));
            dt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("UNIT_NAME", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_NAME", Type.GetType("System.String"));
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
                case "btnAllEnter":
                    LinkButton btA = (LinkButton)sender;
                    decimal slipNumber = Convert.ToDecimal(btA.CommandArgument);
                    if (slipNumber == CConvert.ToDecimal(ViewState["SN"]))
                    {
                        BindData();
                        break;
                    }
                    else
                    {
                        ViewState["SN"] = slipNumber;
                    }
                    BllTransferInPlanTable btable = bll.GetModel(slipNumber);
                    if (btable.STATUS_FLAG == CConstant.NORMAL)
                    {
                        BindData();
                        break ;
                    }
                    btable.TRANSFERQUANTITY = btable.QUANTITY;
                    if (bll.GetTransferInfo(btable) > 0)
                    {
                        BindData();
                    }
                    break;
                case "btnHaifEnter":
                    BindData();
                    break;
            }
            return true;
        }

        private void Search(object sender, EventArgs e)
        {
            //获得总的记录数
            int recordCount = bll.GetTransferPlanCount(getConduction());
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
            sb.Append("1=1");
            if (this.txtSlipNumber.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER Like %{0}%", txtSlipNumber.Text.Trim());
            }
            if (this.txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND TO_WAREHOUSE_CODE = '{0}'", txtWarehouseCode.Text.Trim());
            }
            if (txtReToDate.Text.Trim() != "" && txtReFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE BETWEEN '{0}' AND '{1}'", txtReFromDate.Text.Trim(), Convert.ToDateTime(txtReToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtReFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE  >= '{0}' ", txtReFromDate.Text.Trim());
            }
            else if (txtReToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE  < '{0}' ", Convert.ToDateTime(txtReToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetTransferPlanList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
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
                //入库按钮
                LinkButton btnE = (LinkButton)e.Row.FindControl("btnAllEnter");
                LinkButton btnH = (LinkButton)e.Row.FindControl("btnHaifEnter");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    btnH.Attributes.Add("onclick", "return winOpen('ShopReceiptPlanDetail.aspx?','SN=" + btnH.CommandArgument + "','360','420')");

                    //光标移动事件
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnE.Visible = false;
                    btnH.Visible = false;
                }
                e.Row.Cells[0].Visible = false;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }

        }
        protected void ReFromDate_Changed(object sender, EventArgs e)
        {
            if (txtReFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtReFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtReFromDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtReFromDate.Text = Convert.ToDateTime(txtReFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void ReToDate_Changed(object sender, EventArgs e)
        {
            if (txtReToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtReToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtReToDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtReToDate.Text = Convert.ToDateTime(txtReToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtReFromDate.Text.Trim() != "" && this.txtReToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtReToDate.Text) < Convert.ToDateTime(txtReFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"起始时间不能大于截止时间!\");document.getElementById('" + txtReToDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
    }
}

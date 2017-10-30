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
using SCM.Model;
using System.Text;
using SCM.Common;
using System.Reflection;
using log4net;

namespace SCM.Web.TransferOut
{
    public partial class TransferOutSearch : BasePage
    {
        BShipment bll = new BShipment();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                this.txtOutFromDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
                this.txtOutToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
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
            dt.Columns.Add("DEPARTUAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("SHIPMENT_TYPE", Type.GetType("System.String"));

            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }

        #region gridView_RowDataBound
        /// <summary>
        /// gridView_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnD = (LinkButton)e.Row.FindControl("btnDelete");
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                LinkButton btnE = (LinkButton)e.Row.FindControl("btnExport");
                if (btnS.Text.Trim() != "&nbsp;" && btnS.Text.Trim() != "")
                {
                    btnD.Attributes.Add("onclick", "return confirm(\"你确认要删除吗?\")");
                    btnM.Attributes.Add("onclick", "return winOpen('TransferOutModify.aspx?','SN=" + btnD.CommandArgument + "','630','1020');");
                    btnS.Attributes.Add("onclick", "return winOpen('TransferOutModify.aspx?','SN=" + btnS.CommandArgument + "&SHOW_FLAG=1','630','1020');");

                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnD.Visible = false;
                    btnM.Visible = false;
                    btnE.Visible = false;
                }
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetList(strWhere, "DEPARTUAL_DATE", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
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
            sb.AppendFormat(" STATUS_FLAG <> {0}", CConstant.DELETE);
            if (txtSlipNumber.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER = {0}", txtSlipNumber.Text.Trim());
            }
            if (txtFromWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND FROM_WAREHOUSE_CODE = '{0}'", txtFromWarehouseCode.Text.Trim());
            }
            if (txtToWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND TO_WAREHOUSE_CODE = '{0}'", txtToWarehouseCode.Text.Trim());
            }

            if (txtOutFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE >= '{0}'", txtOutFromDate.Text.Trim());
            }

            if (txtOutToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE < '{0}'", Convert.ToDateTime(txtOutToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }

            if (txtEnterFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE >= '{0}'", txtEnterFromDate.Text.Trim());
            }

            if (txtEnterToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND ARRIVAL_DATE < '{0}'", Convert.ToDateTime(txtEnterToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();
        }

        #endregion

        #region Changed 事件
        /// <summary>
        /// 出库仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void From_Warehouse_Change(object sender, EventArgs e)
        {
            if (txtFromWarehouseCode.Text.Trim() == "")
            {
                this.txtFromWarehouseCode.Text = "";
                this.lblFromWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtFromWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtFromWarehouseCode.Text = table.Code;
                this.lblFromWarehouseName.Text = table.Name;
            }
            else
            {
                this.lblFromWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库仓库不存在!\");document.getElementById('" + txtFromWarehouseCode.ClientID + "').value='';", true);
            }
        }

        /// <summary>
        /// 入库仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void To_Warehouse_Change(object sender, EventArgs e)
        {
            if (txtToWarehouseCode.Text.Trim() == "")
            {
                this.txtToWarehouseCode.Text = "";
                this.lblToWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtToWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtToWarehouseCode.Text = table.Code;
                this.lblToWarehouseName.Text = table.Name;
            }
            else
            {
                this.lblToWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"入库仓库不存在!\");document.getElementById('" + txtToWarehouseCode.ClientID + "').value='';", true);
            }
        }
        #endregion

        #region 点击事件
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="btnId"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":                   //查询
                    Search(sender, e);
                    break;
                case "btnNew":                      //新建
                    BindData();
                    break;
                case "btnExport":
                    LinkButton btn = (LinkButton)sender;
                    Export(btn.CommandArgument);
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

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="slipNumber"></param>
        private void Export(string slipNumber)
        {
            BShipmentPlan bll = new BShipmentPlan();
            string fileName = "";
            DataSet ds = bll.PrintShop(slipNumber);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                dt.TableName = "UserOutDatetable";
                string reportPath = Server.MapPath("~") + "\\ReportFroms" + "\\CustomerShopMonad.rpt";
                fileName = "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                string filePath = Server.MapPath("~") + "\\Pdf" + "\\" + fileName;
                if (CommonUtil.ExportToPDF(reportPath, dt, filePath))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "window.open('../../Pdf/" + fileName + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"你所需要的数据不存在！\");processCloseAndRefreshParent();", true);
            }
        }
        #endregion

        #region　输入验证

        /// <summary>
        /// 输入验证
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool b = true;
            if (!PageValidate.IsDateTimeOrEmpty(txtOutFromDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtOutFromDate.ClientID + "').value='';", true);
                b = false;
            }
            else if (!PageValidate.IsDateTimeOrEmpty(txtOutToDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtOutToDate.ClientID + "').value='';", true);
                b = false;
            }
            else if (!PageValidate.IsDateTimeOrEmpty(txtEnterFromDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtEnterFromDate.ClientID + "').value='';", true);
                b = false;
            }
            else if (!PageValidate.IsDateTimeOrEmpty(txtEnterToDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtEnterToDate.ClientID + "').value='';", true);
            }
            return b;
        }


        #endregion
    }
}

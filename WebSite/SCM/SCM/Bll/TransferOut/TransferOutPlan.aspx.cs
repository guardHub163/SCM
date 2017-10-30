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
using System.Text;
using SCM.Bll;
using SCM.Model;
using SCM.Common;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using CrystalDecisions.Shared;
using log4net;
using System.Reflection;


namespace SCM.Web.TransferOut
{
    public partial class TransferOutPlan : BasePage
    {
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        BShipmentPlan bll = new BShipmentPlan();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();

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
                BaseMaster ta = bCommon.GetCenterWarehouse();
                this.txtWarehouseCode.Text = ta.Code;
                this.lblWarehouseName.Text = ta.Name;
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
            dt.Columns.Add("FROM_WAREHOUSE_CODE", Type.GetType("System.String"));
            dt.Columns.Add("TO_WAREHOUSE_CODE", Type.GetType("System.String"));
            dt.Columns.Add("FROM_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("TO_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("DEPARTUAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("ARRIVAL_DATE", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_FLAG", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_NAME", Type.GetType("System.String"));

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
                LinkButton btnM = (LinkButton)e.Row.FindControl("btnModify");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    string param = "&WC=" + e.Row.Cells[0].Text;
                    param += "&WN=" + HttpUtility.UrlEncode(e.Row.Cells[1].Text);
                    param += "&SC=" + e.Row.Cells[2].Text;
                    param += "&SN=" + HttpUtility.UrlEncode(e.Row.Cells[3].Text);
                    param += "&DD=" + e.Row.Cells[4].Text;

                    btnM.Attributes.Add("onclick", "return winOpen('TransferOutPlanDetail.aspx?','" + param + "','510','1020');");

                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor= mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
                else
                {
                    btnM.Visible = false;
                }
            }
        }


        #region 查询
        private void Search(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }
            //插入数据后获得总的记录数
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

        private bool checkInput()
        {
            return true;
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetTransferOutPlanList(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
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
            sb.Append("1=1");
            if (this.txtWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND FROM_WAREHOUSE_CODE = '{0}'", txtWarehouseCode.Text.Trim());
            }
            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE BETWEEN '{0}' AND '{1}'", txtFromDate.Text.Trim(), Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE  >= '{0}' ", txtFromDate.Text.Trim());
            }
            else if (txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND DEPARTUAL_DATE  < '{0}' ", Convert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();
        }


        #endregion

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
                this.lblWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"入库仓库不存在!\");document.getElementById('" + txtWarehouseCode.ClientID + "').value='';", true);
            }
        }


        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    Search(sender, e);
                    break;
                case "btnModify":
                    BindData();
                    break;
                case "btnPrintWarehouse":
                    PrintWarehouse();
                    break;
            }
            return true;
        }

        private void PrintWarehouse()
        {
            if (this.txtWarehouseCode.Text != "" && this.txtFromDate.Text != "" && this.txtToDate.Text != "")
            {
                DataSet ds = bll.PrintOutMonad(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), txtWarehouseCode.Text);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    dt.TableName = "UserOutDatetable";
                    string reportPath = Server.MapPath("~") + "\\ReportFroms" + "\\CustomerOutMonad.rpt";
                    string fileName = "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    string filePath = Server.MapPath("~") + "\\Pdf" + "\\" + fileName;
                    if (CommonUtil.ExportToPDF(reportPath, dt, filePath))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "window.open('../../Pdf/" + fileName + "');", true);
                        //CommonUtil.DownLoad(filePath, true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert(\"你所需要的数据不存在！\");processCloseAndRefreshParent();", true);
                }
            }
        }

        protected void ToDate_Changed(object sender, EventArgs e)
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
        protected void FromDate_Changed(object sender, EventArgs e)
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
        }//end class
    }
}

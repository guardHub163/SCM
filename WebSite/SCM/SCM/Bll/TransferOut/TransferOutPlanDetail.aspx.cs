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
using System.Text;
using SCM.Model;
using System.Collections.Generic;
using SCM.Common;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Reflection;
using log4net;

namespace SCM.Web.TransferOut
{
    public partial class TransferOutPlanDetail : BaseModalDialogPage
    {
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        BShipmentPlan bll = new BShipmentPlan();
        BShipment bShipment = new BShipment();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        int PageSize = 16;
        private ReportDocument customerReport;
        private DiskFileDestinationOptions FileOPS = new DiskFileDestinationOptions();
        private ExportOptions ExOPS = new ExportOptions();
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                if (Request.Params["WC"] != null && Request.Params["WC"].ToString().Trim() != "")
                {
                    this.txtWarehouseCode.Text = Request.Params["WC"].ToString();
                    this.lblWarehouseName.Text = Request.Params["WN"].ToString();
                    this.txtShopCode.Text = Request.Params["SC"].ToString();
                    this.lblShopName.Text = Request.Params["SN"].ToString();
                    this.txtDepartureDate.Text = Request.Params["DD"].ToString();
                    BindData();
                }
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                TextBox txtQ = (TextBox)e.Row.FindControl("txtQuantity");
                if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != "")
                {
                    txtQ.Attributes.Add("onfocus", "this.select();");
                    txtQ.Attributes.Add("onkeyup", "this.value=this.value.replace(/[^\\d]/g,'') ");
                }
                else
                {
                    txtQ.Visible = false;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void Quantity_Change(object sender, EventArgs e)
        {
            TextBox qty = (TextBox)sender;
            if (qty.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"数量不能为空！\");document.getElementById('" + qty.ClientID + "').value='0';", true);
            }
        }

        private void BindData()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" STATUS_FLAG = {0}", CConstant.INIT);
            strSql.AppendFormat(" AND FROM_WAREHOUSE_CODE = '{0}'", this.txtWarehouseCode.Text.Trim());
            strSql.AppendFormat(" AND TO_WAREHOUSE_CODE = '{0}'", this.txtShopCode.Text.Trim());
            strSql.AppendFormat(" AND DEPARTUAL_DATE = '{0}'", this.txtDepartureDate.Text.Trim());
            ds = bll.GetTransferOutPlanDetail(strSql.ToString());
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }


        private void Save(object sender, EventArgs e)
        {
            BllShipmentTable shipmentTable = new BllShipmentTable();
            shipmentTable.SHIPMENT_TYPE = CConstant.SHIPMENT_TYPE_PLAN;
            shipmentTable.FROM_WAREHOUSE_CODE = this.txtWarehouseCode.Text.Trim();
            shipmentTable.DEPARTUAL_DATE = DateTime.Parse(this.txtDepartureDate.Text.Trim());
            shipmentTable.ARRIVAL_DATE = DateTime.Now.AddDays(2);
            shipmentTable.TO_WAREHOUSE_CODE = this.txtShopCode.Text.Trim();
            shipmentTable.STATUS_FLAG = CConstant.NORMAL;
            shipmentTable.CREATE_USER = UserTable.USER_ID;
            shipmentTable.LAST_UPDATE_USER = shipmentTable.CREATE_USER;
            List<BllShipmentLineTable> list = new List<BllShipmentLineTable>();
            BllShipmentLineTable shipmentLineTable = null;
            TextBox txtAssignQuantity = null;
            foreach (GridViewRow row in gridView.Rows)
            {
                if (row.Cells[0].Text.Trim() != "&nbsp;" && row.Cells[0].Text.Trim() != "")
                {
                    decimal assignQuantity = 0;
                    shipmentLineTable = new BllShipmentLineTable();
                    shipmentLineTable.LINE_NUMBER = row.RowIndex + 1;
                    shipmentLineTable.SHIPMENT_PLAN_SLIP_NUMBER = decimal.Parse(row.Cells[0].Text);
                    shipmentLineTable.UNIT_CODE = row.Cells[1].Text;
                    shipmentLineTable.PRODUCT_CODE = row.Cells[2].Text;
                    shipmentLineTable.STATUS_FLAG = CConstant.NORMAL;
                    try
                    {
                        txtAssignQuantity = (TextBox)row.FindControl("txtQuantity");
                        assignQuantity = decimal.Parse(txtAssignQuantity.Text);
                        shipmentLineTable.QUANTITY = assignQuantity;
                    }
                    catch { }
                    shipmentTable.AddShipmentLine(shipmentLineTable);
                }
                else
                {
                    break;
                }
            }
            if (shipmentTable.SHIPMENT_LINE.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库明细不能为空！\");", true);
                return;
            }
            string slipNumber = bShipment.InsertPlan(shipmentTable);
            if (slipNumber != "")
            {
                string script = "alert(\"出库确认成功！\");";


                if (ckd.Checked)
                {
                    string fileName = PrintShop(slipNumber);
                    if (fileName != "")
                    {
                        script += "window.open('../../Pdf/" + fileName + "');";
                    }
                }
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", script + "processCloseAndRefreshParent();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库确认失败！\");", true);
            }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave":
                    Save(sender, e);
                    break;
            }
            return true;
        }

        private string PrintShop(string slipnumber)
        {
            string fileName = "";
            DataSet ds = bll.PrintShop(slipnumber);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
            {
                dt.TableName = "UserOutDatetable";
                string reportPath = Server.MapPath("~") + "\\ReportFroms" + "\\CustomerShopMonad.rpt";
                fileName = "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                string filePath = Server.MapPath("~") + "\\Pdf" + "\\" + fileName;
                CommonUtil.ExportToPDF(reportPath, dt, filePath);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"你所需要的数据不存在！\");processCloseAndRefreshParent();", true);
            }
            return fileName;
        }
    }//end class
}

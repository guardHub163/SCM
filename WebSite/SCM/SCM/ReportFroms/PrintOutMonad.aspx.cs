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

namespace SCM.Web.ReportFroms
{
    public partial class PrintOutMonad : System.Web.UI.Page
    {
        BShipmentPlan bll = new BShipmentPlan();
        private ReportDocument customerReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["WarehouseCode"] != null && Request.Params["WarehouseCode"].Trim() != "")
                {
                    this.lblWarehouse.Text = Request.Params["WarehouseCode"].ToString();
                    this.lblTime1.Text = Request.Params["FromDate"].ToString();
                    this.lblTime2.Text = Request.Params["ToDate"].ToString();

                }
               
            }
            Printout();
        }

        private void Printout() 
        {
            DataSet dt = bll.PrintOutMonad(Convert.ToDateTime(lblTime1.Text), Convert.ToDateTime(lblTime2.Text), lblWarehouse.Text);
            DataTable da = dt.Tables[0];
            da.TableName = "UserOutDatetable"; 
            customerReport = new ReportDocument();
            string reportPath = Server.MapPath("CustomerOutMonad.rpt");
            customerReport.Load(reportPath);
            customerReport.SetDataSource(da);
            CrystalReportViewer.ReportSource = customerReport;
            CrystalReportViewer.DataBind();
        }

    }
}
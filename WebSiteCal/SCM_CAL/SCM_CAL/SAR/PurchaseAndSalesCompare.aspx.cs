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
using System.Web.UI.DataVisualization.Charting;

namespace SCM.Web.SAR
{
    public partial class _PurchaseAndSalesCompare : System.Web.UI.Page
    {
        BSarSalesOrder bll = new BSarSalesOrder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
                string fromDate = Request.QueryString["FROM_DATE"];
                string toDate = Request.QueryString["TO_DATE"];
                DataTable timeTable = TimeTable();
                DataTable saleTable = SaleTable();
                int year = Convert.ToDateTime(fromDate).Year;
                int month = Convert.ToDateTime(fromDate).Month;
                int lastyear = year - 1;//去年的年份
                for (int i = month; i <= 12; i++)//取出去年的所有月份
                {
                    DataRow row = timeTable.NewRow();
                    if (i < 10)
                    {
                        row["Time"] = lastyear + "-0" + i;
                    }
                    else
                    {
                        row["Time"] = lastyear + "-" + i;
                    }
                    timeTable.Rows.Add(row);
                }

                for (int i = 1; i <= month; i++)//取出今年的所有的月份
                {
                    DataRow row1 = timeTable.NewRow();
                    if (i < 10)
                    {
                        row1["Time"] = year + "-0" + i;
                    }
                    else
                    {
                        row1["Time"] = year + "-" + i;
                    }
                    timeTable.Rows.Add(row1);
                }
                foreach (DataRow timerow in timeTable.Rows)
                {
                    DataRow salerow = saleTable.NewRow();
                    salerow["SalesTime"] = timerow["Time"];
                    DataTable stable = bll.GetMothSale(departmentCode, timerow["Time"].ToString()).Tables[0];
                    if (stable != null)
                    {
                        salerow["SaleQuantity"] = Convert.ToInt32(stable.Rows[0]["QUANTITY"]);
                    }
                    else
                    {
                        salerow["SaleQuantity"] = 0;
                    }
                    DataTable ptable = bll.GetMonthQuantity(departmentCode, timerow["Time"].ToString()).Tables[0];
                    if (ptable != null)
                    {

                        salerow["PurchaseQuantity"] = Convert.ToInt32(ptable.Rows[0]["QUANTITY"]);
                    }
                    else
                    {
                        salerow["PurchaseQuantity"] = 0;
                    }
                    saleTable.Rows.Add(salerow);
                }

                SeriesChartType chartype = SeriesChartType.Line;
                ChartStyle.SetChart(Chart1);
                Title t1 = ChartStyle.SetTitle("title1");
                t1.Text = "进销比统计曲线图";
                Chart1.Titles.Add(t1);
                Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "SalesTime", "SaleQuantity");
                s1.LegendText = "销售数量统计";
                Chart1.Series.Add(s1);
                ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                Chart1.ChartAreas.Add(cArea1);
                Legend l1 = ChartStyle.SetLegend("颜色");
                Chart1.Legends.Add(l1);
                ChartHelper.GetSeriesPointValue(s1, saleTable, "SalesTime", "SaleQuantity");

                ChartStyle.SetChart(Chart1);
                Series s3 = ChartStyle.SetSeriesStyle("Series3", chartype, "SalesTime", "PurchaseQuantity");
                s3.LegendText = "进货数量统计";
                Chart1.Series.Add(s3);
                ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                Chart1.ChartAreas.Add(cArea3);
                ChartHelper.GetSeriesPointValue(s3, saleTable, "SalesTime", "PurchaseQuantity");
            }
        }
        private DataTable TimeTable() //整合12个月份
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Time", Type.GetType("System.String"));
            return dt;
        }

        private DataTable SaleTable() //整合同一时间的销售，进货的数量
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SalesTime", Type.GetType("System.String"));
            dt.Columns.Add("SaleQuantity", Type.GetType("System.Int32"));
            dt.Columns.Add("PurchaseQuantity", Type.GetType("System.Int32"));
            return dt;
        }

    }
}

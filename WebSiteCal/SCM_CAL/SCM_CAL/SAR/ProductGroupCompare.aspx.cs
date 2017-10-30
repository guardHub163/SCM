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
    public partial class _ProductGroupCompare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
                string fromDate = Request.QueryString["FROM_DATE"];
                string totalAmount = Request.QueryString["TOTOAL_AMOUNT"];
                string toDate = Request.QueryString["TO_DATE"];
                if (totalAmount != "" && totalAmount != "0")
                {
                    DataTable dt = GetStyleProductTable(departmentCode, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), totalAmount);

                    if (dt != null)
                    {
                        this.gridView.DataSource = dt;
                        this.gridView.DataBind();
                    }

                    //SeriesChartType chartype = SeriesChartType.Column;
                    //ChartStyle.SetChart(Chart1);
                    //Title t1 = ChartStyle.SetTitle("title1");
                    //t1.Text = "商品种类柱形图";
                    //Chart1.Titles.Add(t1);
                    //Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "NAME", "AMOUNT");
                    //s1.LegendText = "销售金额统计";
                    //Chart1.Series.Add(s1);
                    //ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    //Chart1.ChartAreas.Add(cArea1);
                    //Legend l1 = ChartStyle.SetLegend("颜色");
                    //Chart1.Legends.Add(l1);
                    //ChartHelper.GetSeriesPointValue(s1, dt, "NAME", "AMOUNT");

                    //ChartStyle.SetChart(Chart1);
                    //Series s3 = ChartStyle.SetSeriesStyle("Series3", chartype, "NAME", "QUANTITY");
                    //s3.LegendText = "销售数量统计";
                    //Chart1.Series.Add(s3);
                    //ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                    //Chart1.ChartAreas.Add(cArea3);
                    //ChartHelper.GetSeriesPointValue(s3, dt, "NAME", "QUANTITY");


                    //销售金额统计
                    SeriesChartType chartype = SeriesChartType.Column;
                    ChartStyle.SetChart(Chart1);
                    Title t1 = ChartStyle.SetTitle("title1");
                    t1.Text = "销售金额统计柱形图";
                    Chart1.Titles.Add(t1);
                    Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "NAME", "AMOUNT");
                    s1.LegendText = "销售金额统计";
                    Chart1.Series.Add(s1);
                    ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    Chart1.ChartAreas.Add(cArea1);
                    Legend l1 = ChartStyle.SetLegend("颜色");
                    Chart1.Legends.Add(l1);
                    ChartHelper.GetSeriesPointValue(s1, dt, "NAME", "AMOUNT");

                    //销售数量统计
                    ChartStyle.SetChart(Chart2);
                    Title t2 = ChartStyle.SetTitle("title2");
                    t2.Text = "销售数量统计柱形图";
                    Chart2.Titles.Add(t2);
                    Legend l2 = ChartStyle.SetLegend("颜色");
                    Chart2.Legends.Add(l2);
                    ChartStyle.SetChart(Chart2);
                    Series s4 = ChartStyle.SetSeriesStyle("Series4", chartype, "NAME", "QUANTITY");
                    s4.LegendText = "销售数量统计";
                    Chart2.Series.Add(s4);
                    ChartArea cArea4 = ChartStyle.SetChartAreaStyle("ChartArea4");
                    Chart2.ChartAreas.Add(cArea4);
                    ChartHelper.GetSeriesPointValue(s4, dt, "NAME", "QUANTITY");


                }
                else
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ProductDt().Copy();
                    for (int i = dt1.Rows.Count; i < 10; i++)
                    {
                        dt1.Rows.Add(dt1.NewRow());
                    }
                    this.gridView.DataSource = dt1;
                    this.gridView.DataBind();

                    //SeriesChartType chartype = SeriesChartType.Line;
                    //ChartStyle.SetChart(Chart1);
                    //Title t1 = ChartStyle.SetTitle("title1");
                    //t1.Text = "商品种类柱形图";
                    //Chart1.Titles.Add(t1);
                    //Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "NAME", "AMOUNT");
                    //s1.LegendText = "销售金额统计";
                    //Chart1.Series.Add(s1);
                    //ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    //Chart1.ChartAreas.Add(cArea1);
                    //Legend l1 = ChartStyle.SetLegend("颜色");
                    //Chart1.Legends.Add(l1);
                    //ChartHelper.GetSeriesPointValue(s1, dt1, "NAME", "AMOUNT");

                    //ChartStyle.SetChart(Chart1);
                    //Series s3 = ChartStyle.SetSeriesStyle("Series3", chartype, "NAME", "QUANTITY");
                    //s3.LegendText = "销售数量统计";
                    //Chart1.Series.Add(s3);
                    //ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                    //Chart1.ChartAreas.Add(cArea3);
                    //ChartHelper.GetSeriesPointValue(s3, dt1, "NAME", "QUANTITY");

                    //销售金额统计
                    SeriesChartType chartype = SeriesChartType.Column;
                    ChartStyle.SetChart(Chart1);
                    Title t1 = ChartStyle.SetTitle("title1");
                    t1.Text = "销售金额统计柱形图";
                    Chart1.Titles.Add(t1);
                    Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "NAME", "AMOUNT");
                    s1.LegendText = "销售金额统计";
                    Chart1.Series.Add(s1);
                    ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    Chart1.ChartAreas.Add(cArea1);
                    Legend l1 = ChartStyle.SetLegend("颜色");
                    Chart1.Legends.Add(l1);
                    ChartHelper.GetSeriesPointValue(s1, dt1, "NAME", "AMOUNT");

                    //销售数量统计
                    ChartStyle.SetChart(Chart2);
                    Title t2 = ChartStyle.SetTitle("title2");
                    t2.Text = "销售数量统计柱形图";
                    Chart2.Titles.Add(t2);
                    Legend l2 = ChartStyle.SetLegend("颜色");
                    Chart2.Legends.Add(l2);
                    ChartStyle.SetChart(Chart2);
                    Series s4 = ChartStyle.SetSeriesStyle("Series4", chartype, "NAME", "QUANTITY");
                    s4.LegendText = "销售数量统计";
                    Chart2.Series.Add(s4);
                    ChartArea cArea4 = ChartStyle.SetChartAreaStyle("ChartArea4");
                    Chart2.ChartAreas.Add(cArea4);
                    ChartHelper.GetSeriesPointValue(s4, dt1, "NAME", "QUANTITY");

                }
            }
        }

        private DataTable GetStyleProductTable(string departmentCode, DateTime datetime, DateTime todatetime, string amount)
        {
            BSarSalesOrder bll = new BSarSalesOrder();
            DataTable dt = ProductDt();
            DataSet ds = bll.GetStyleProductInfo(departmentCode, datetime, todatetime);
            DataTable da = ds.Tables[0];
            if (da.Rows.Count == 0)
            {
                return new DataTable();
            }
            int i = 1;
            foreach (DataRow row in da.Rows)
            {
                DataRow rows = dt.NewRow();
                rows["NUMBER"] = i;
                rows["NAME"] = row["PRODUCT_GROUP_NAME"];
                rows["AMOUNT"] = row["PRICE"];
                rows["SORT"] = (CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(row["PRICE"]) / Convert.ToDecimal(amount) * 100))).ToString();
                rows["QUANTITY"] = row["QUANTITY"];
                dt.Rows.Add(rows);
                i++;
            }

            return dt;
        }
        public DataTable ProductDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("NAME", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT", Type.GetType("System.Decimal"));
            dt.Columns.Add("SORT", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            return dt;
        }
    }
}
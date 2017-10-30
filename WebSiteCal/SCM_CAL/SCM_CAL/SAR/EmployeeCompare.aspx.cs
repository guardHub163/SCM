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
using SCM.Common;
using System.Web.UI.DataVisualization.Charting;

namespace SCM.Web.SAR
{
    public partial class _EmployeeCompare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
                string fromDate = Request.QueryString["FROM_DATE"];
                string employeeQuantity = Request.QueryString["EMPLOYEE_QUANTITY"];
                string toDate = Request.QueryString["TO_DATE"];

                DataTable dt = GetEmployeeSAR(departmentCode, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), employeeQuantity);
                if (dt.Rows.Count == 0)
                {
                    dt = EmployeeDt().Copy();
                    for (int i = dt.Rows.Count; i < 10; i++)
                    {
                        dt.Rows.Add(dt.NewRow());
                    }
                }

                this.gridView.DataSource = dt;
                this.gridView.DataBind();

                //SeriesChartType chartype = SeriesChartType.Column;
                //ChartStyle.SetChart(Chart1);
                //Title t1 = ChartStyle.SetTitle("title1");
                //t1.Text = "员工销售柱形图";
                //Chart1.Titles.Add(t1);
                //Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "USERNAME", "AMOUNT");
                //s1.LegendText = "销售金额统计";
                //Chart1.Series.Add(s1);
                //ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                //Chart1.ChartAreas.Add(cArea1);
                //Legend l1 = ChartStyle.SetLegend("颜色");
                //Chart1.Legends.Add(l1);
                //ChartHelper.GetSeriesPointValue(s1, dt, "USERNAME", "AMOUNT");

                //ChartStyle.SetChart(Chart1);
                //Series s3 = ChartStyle.SetSeriesStyle("Series3", chartype, "USERNAME", "QUANTITY");
                //s3.LegendText = "销售数量统计";
                //Chart1.Series.Add(s3);
                //ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                //Chart1.ChartAreas.Add(cArea3);
                //ChartHelper.GetSeriesPointValue(s3, dt, "USERNAME", "QUANTITY");

                //员工销售金额统计
                SeriesChartType chartype = SeriesChartType.Column;
                ChartStyle.SetChart(Chart1);
                Title t1 = ChartStyle.SetTitle("title1");
                t1.Text = "员工销售金额柱形图";
                Chart1.Titles.Add(t1);
                Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "USERNAME", "AMOUNT");
                s1.LegendText = "销售金额统计";
                Chart1.Series.Add(s1);
                ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                Chart1.ChartAreas.Add(cArea1);
                Legend l1 = ChartStyle.SetLegend("颜色");
                Chart1.Legends.Add(l1);
                ChartHelper.GetSeriesPointValue(s1, dt, "USERNAME", "AMOUNT");          

                //员工销售数量统计
                ChartStyle.SetChart(Chart2);
                Title t2 = ChartStyle.SetTitle("title2");
                t2.Text = "员工销售数量柱形图";
                Chart2.Titles.Add(t2);
                Legend l2 = ChartStyle.SetLegend("颜色");
                Chart2.Legends.Add(l2);
                ChartStyle.SetChart(Chart2);
                Series s4 = ChartStyle.SetSeriesStyle("Series4", chartype, "USERNAME", "QUANTITY");
                s4.LegendText = "销售数量统计";
                Chart2.Series.Add(s4);
                ChartArea cArea4 = ChartStyle.SetChartAreaStyle("ChartArea4");
                Chart2.ChartAreas.Add(cArea4);
                ChartHelper.GetSeriesPointValue(s4, dt, "USERNAME", "QUANTITY");

                this.lblAverAgeaAount.InnerText = "平均销售金额：" + dt.Rows[1]["AVERAGEAMOUNT"] + "元";
                this.lblAverageQuantity.InnerText = "平均销售数量：" + dt.Rows[1]["AVERAGEQUANTITY"] + "件";

            }
        }

        ///<summary>
        ///员工信息
        ///</summary>
        public DataTable GetEmployeeSAR(string departmentCode, DateTime datetime, DateTime todatetime, string totaluser)
        {
            string atv = "";
            string Amount = "";
            BSarSalesOrder bll = new BSarSalesOrder();
            DataTable dt = EmployeeDt();
            DataSet saleds = bll.GetAmountRanking(departmentCode, datetime, todatetime);
            DataTable saletable = saleds.Tables[0];//人员总金额的排名
            if (saletable.Rows.Count == 0)
            {
                return new DataTable();
            }
            saletable.Columns.Add("SaleNumber", Type.GetType("System.String"));
            saletable.Columns.Add("NumberId", Type.GetType("System.String"));
            saletable.Columns.Add("ATV", Type.GetType("System.String"));
            DataSet number = bll.GetSlipNumber(departmentCode, datetime, todatetime);
            DataTable numbertable = number.Tables[0];//人员销售单数的排名
            if (numbertable.Rows.Count == 0)
            {
                return new DataTable();
            }
            foreach (DataRow row in saletable.Rows)
            {
                foreach (DataRow nrow in numbertable.Rows)
                {
                    if (row["SALES_EMPLOYEE"].ToString() == nrow["SALES_EMPLOYEE"].ToString())
                    {
                        row["SaleNumber"] = nrow["QUANTITY"];
                        row["NumberId"] = nrow["QUANTITY_SORT"];
                        row["ATV"] = nrow["ATV"];
                        continue;
                    }
                }
            }
            DataSet daAmount = bll.GetOneDepartmentAmount(departmentCode, datetime, todatetime);
            DataTable da = daAmount.Tables[0];
            DataSet dsAllSlipNumber = bll.GetAllSlipNumbercount(departmentCode, datetime, todatetime);
            DataTable daAllSlipNumber = dsAllSlipNumber.Tables[0];
            DataSet dsSmallSlipNumber = bll.GetSmallSlipNumbercount(departmentCode, datetime, todatetime);
            DataTable daSmallSlipNumber = dsSmallSlipNumber.Tables[0];
            if (da != null & !"".Equals(da.Rows[0]))
            {
                Amount = da.Rows[0]["AMOUNT"].ToString();
                atv = Convert.ToString(Convert.ToDecimal(daAllSlipNumber.Rows[0]["SLIP_NUMBER"]) - Convert.ToDecimal(daSmallSlipNumber.Rows[0]["SLIP_NUMBER"]));
            }
            foreach (DataRow rows in saletable.Rows)
            {
                DataRow drow = dt.NewRow();
                drow["USERNAME"] = rows["SALES_EMPLOYEE"];
                drow["AMOUNT"] = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(rows["PRICE"])));
                drow["AMOUNT_SORT"] = rows["AMOUNT_SORT"];
                drow["AMOUNT_COMPARE"] = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(rows["PRICE"]) / Convert.ToDecimal(Amount) * 100));
                drow["QUANTITY"] = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(rows["SaleNumber"])));
                drow["QUANTITY_SORT"] = rows["NumberId"];
                drow["QUANTITY_COMPARE"] = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(rows["SaleNumber"]) / Convert.ToDecimal(atv) * 100));
                drow["JOINTSALESRATE"] = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(rows["ATV"]) / Convert.ToDecimal(atv) * 100));
                drow["AVERAGEAMOUNT"] = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(Amount) / Convert.ToDecimal(totaluser)));
                drow["AVERAGEQUANTITY"] = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(atv) / Convert.ToDecimal(totaluser)));
                dt.Rows.Add(drow);
            }
            return dt;
        }

        public DataTable EmployeeDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("USERNAME", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT", Type.GetType("System.Decimal"));
            dt.Columns.Add("AMOUNT_SORT", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT_COMPARE", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY_SORT", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY_COMPARE", Type.GetType("System.String"));
            dt.Columns.Add("JOINTSALESRATE", Type.GetType("System.String"));
            dt.Columns.Add("AVERAGEAMOUNT", Type.GetType("System.String"));
            dt.Columns.Add("AVERAGEQUANTITY", Type.GetType("System.String"));
            return dt;
        }
    }
}

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
using SCM.Web;
using SCM.Web.SAR;


public partial class ProductGroupCompares : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            ViewState["DEPARTMENT_CODE"] = Request.QueryString["DEPARTMENT_CODE"];
            ViewState["FROM_DATE"] = Request.QueryString["FROM_DATE"];
            ViewState["TO_DATE"] = Request.QueryString["TO_DATE"];
            DataTable dt = AjaxManage.GetProductGroupData();
            ddlGroup.DataSource = dt;
            ddlGroup.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlGroup.SelectedIndex = 0;
                ddlGroup_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        string groupCode = ddlGroup.SelectedValue;
        DataTable dt = GetStyleProductTable(Convert.ToString(ViewState["DEPARTMENT_CODE"]), Convert.ToDateTime(ViewState["FROM_DATE"]), Convert.ToDateTime(ViewState["TO_DATE"]), groupCode);


        if (dt == null || dt.Rows.Count == 0)
        {
            dt = ProductDt();
            for (int i = dt.Rows.Count; i < 10; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
        }

        //销售金额统计
        SeriesChartType chartype = SeriesChartType.Column;
        ChartStyle.SetChart(Chart1);
        Title t1 = ChartStyle.SetTitle("title1");
        t1.Text = "销售金额统计柱形图";
        Chart1.Titles.Add(t1);
        Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "STYLE_NAME", "PRICE");
        s1.LegendText = "销售金额统计";
        Chart1.Series.Add(s1);
        ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
        Chart1.ChartAreas.Add(cArea1);
        Legend l1 = ChartStyle.SetLegend("颜色");
        Chart1.Legends.Add(l1);
        ChartHelper.GetSeriesPointValue(s1, dt, "STYLE_NAME", "PRICE");

        //销售数量统计
        ChartStyle.SetChart(Chart2);
        Title t2 = ChartStyle.SetTitle("title2");
        t2.Text = "销售数量统计柱形图";
        Chart2.Titles.Add(t2);
        Legend l2 = ChartStyle.SetLegend("颜色");
        Chart2.Legends.Add(l2);
        ChartStyle.SetChart(Chart2);
        Series s4 = ChartStyle.SetSeriesStyle("Series4", chartype, "STYLE_NAME", "QUANTITY");
        s4.LegendText = "销售数量统计";
        Chart2.Series.Add(s4);
        ChartArea cArea4 = ChartStyle.SetChartAreaStyle("ChartArea4");
        Chart2.ChartAreas.Add(cArea4);
        ChartHelper.GetSeriesPointValue(s4, dt, "STYLE_NAME", "QUANTITY");

        gridView.DataSource = dt;
        this.gridView.DataBind();
    }

    public DataTable ProductDt()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NUMBER", Type.GetType("System.String"));
        dt.Columns.Add("PRICE", Type.GetType("System.Decimal"));
        dt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
        dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
        return dt;
    }

    private DataTable GetStyleProductTable(string departmentCode, DateTime datetime, DateTime todatetime, string productGroupCode)
    {
        BSarSalesOrder bll = new BSarSalesOrder();
        DataTable dt = ProductDt();
        DataSet ds = bll.GetProductStyleCompare(departmentCode, productGroupCode, datetime, todatetime);
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
            rows["PRICE"] = row["PRICE"];
            rows["STYLE_NAME"] = row["STYLE_NAME"];
            rows["QUANTITY"] = row["QUANTITY"];
            dt.Rows.Add(rows);
            i++;
        }

        return dt;
    }

}

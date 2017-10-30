using System;
using System.Collections;
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
using System.Drawing;
using System.Text;
using SCM.Web;
using SCM.Model;
using SCM.Common;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using log4net;

namespace SCM.Web.Cash
{
    public partial class SalesSatAmount : BasePage
    {
        BSalesOrder bll = new BSalesOrder();
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                CharType.Items.Insert(0, "曲线图");
                CharType.Items.Insert(1, "柱形图");
                CharType.Items.Insert(2, "饼型图");
                GroupCondition.DataSource = bCommon.GetNames("SALES_GROUP").Tables[0];
                GroupCondition.DataTextField = "NAME"; //dropdownlist的Text的字段 
                GroupCondition.DataValueField = "CODE";//dropdownlist的Value的字段 
                GroupCondition.DataBind();
                this.GroupCondition.SelectedIndex = 1;
                this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                string[] CODE = new string[] { "1", "2", "3", "4", "5", "6", "7" };
                string[] NAME = new string[] { "张一", "张二", "张三", "张四", "张五", "张六", "张七" };
                string[] QUANTITY = new string[] { "5", "8", "14", "9", "13", "6", "2" };
                DataTable da = new DataTable();
                da.Columns.Add("CODE", Type.GetType("System.String"));
                da.Columns.Add("NAME", Type.GetType("System.String"));
                da.Columns.Add("QUANTITY", Type.GetType("System.String"));
                for (int i = 0; i < CODE.Length; i++)
                {
                    DataRow dt = da.NewRow();
                    dt["CODE"] = CODE[i];
                    dt["NAME"] = NAME[i];
                    dt["QUANTITY"] = QUANTITY[i];
                    da.Rows.Add(dt);
                }
                ChartStyle.SetChart(Chart1);
                Title t0 = ChartStyle.SetTitle("title0");
                t0.Text = "示例统计";
                Chart1.Titles.Add(t0);
                Series s0 = ChartStyle.SetSeriesStyle("Series0", SeriesChartType.Line, "NAME", "QUANTITY");
                s0.LegendText = "示例统计";
                Chart1.Series.Add(s0);
                ChartArea cArea0 = ChartStyle.SetChartAreaStyle("ChartArea0");
                Chart1.ChartAreas.Add(cArea0);
                Legend l0 = ChartStyle.SetLegend("颜色");
                Chart1.Legends.Add(l0);
                ChartHelper.GetSeriesPointValue(s0, da, "NAME", "QUANTITY");
                this._userTable = (BaseUserTable)Session["UserInfo"];
                if (_userTable.ROLES_ID <= 5)
                {
                    this.btnExcel.Visible = false;
                }

            }

        }

        private void Search(object sender, EventArgs e)
        {

            DataSet ds = bll.GetSalesStatAmount(strGroup(), strWhere());
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"您查询的信息不存在！\");processCloseAndRefreshParent();", true);
                return;
            }
            else
            {
                ViewState["ORDER_TABLE"] = dt;
                dt.Columns.Add("SALES", Type.GetType("System.String"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Columns.Count == 4)
                    {

                        dt.Rows[i]["SALES"] = dt.Rows[i][0];
                    }
                    else if (dt.Columns.Count == 5)
                    {
                        dt.Rows[i]["SALES"] = "" + dt.Rows[i][1];
                    }
                    else
                    {

                        dt.Rows[i]["SALES"] = "" + dt.Rows[i][0] + "" + "/" + "" + dt.Rows[i][1] + "";
                    }
                }

                if (CharType.SelectedIndex != 2)
                {
                    SeriesChartType chartype = SeriesChartType.Line;
                    if (CharType.SelectedIndex == 0)
                    {
                        chartype = SeriesChartType.Line;
                    }
                    else
                    {
                        chartype = SeriesChartType.Column;
                    }
                    ChartStyle.SetChart(Chart1);
                    Title t1 = ChartStyle.SetTitle("title1");
                    t1.Text = "金额统计";
                    Chart1.Titles.Add(t1);
                    Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "SALES", "AMOUNT");
                    s1.LegendText = "金额统计";
                    Chart1.Series.Add(s1);
                    ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    Chart1.ChartAreas.Add(cArea1);
                    Legend l1 = ChartStyle.SetLegend("颜色");
                    Chart1.Legends.Add(l1);
                    ChartHelper.GetSeriesPointValue(s1, dt, "SALES", "AMOUNT");

                    ChartStyle.SetChart(Chart2);
                    Title t2 = ChartStyle.SetTitle("title1");
                    t2.Text = "数量统计";
                    Chart2.Titles.Add(t2);
                    Series s3 = ChartStyle.SetSeriesStyle("Series3", chartype, "SALES", "QUANTITY");
                    s3.LegendText = "数量统计";
                    Chart2.Series.Add(s3);
                    ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                    Chart2.ChartAreas.Add(cArea3);
                    Legend l3 = ChartStyle.SetLegend("颜色");
                    Chart2.Legends.Add(l3);
                    ChartHelper.GetSeriesPointValue(s3, dt, "SALES", "QUANTITY");
                }
                else
                {
                    ChartStyle.SetChart(Chart1);
                    Title t1 = ChartStyle.SetTitle("title1");
                    t1.Text = "金额统计";
                    Chart1.Titles.Add(t1);
                    Series s1 = ChartStyle.SetSeriesStyle("Series1", SeriesChartType.Pie);
                    s1.LegendText = "金额统计";
                    Chart1.Series.Add(s1);
                    ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    Chart1.ChartAreas.Add(cArea1);
                    Legend l1 = ChartStyle.SetLegend("颜色");
                    Chart1.Legends.Add(l1);
                    ChartHelper.GetPicSeriesPointValue(s1, dt, "SALES", "AMOUNT");

                    ChartStyle.SetChart(Chart2);
                    Title t2 = ChartStyle.SetTitle("title1");
                    t2.Text = "数量统计";
                    Chart2.Titles.Add(t2);
                    Series s3 = ChartStyle.SetSeriesStyle("Series3", SeriesChartType.Pie);
                    s3.LegendText = "数量统计";
                    Chart2.Series.Add(s3);
                    ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                    Chart2.ChartAreas.Add(cArea3);
                    Legend l3 = ChartStyle.SetLegend("颜色");
                    Chart2.Legends.Add(l3);
                    ChartHelper.GetPicSeriesPointValue(s3, dt, "SALES", "QUANTITY");
                }

            }
        }

        private string strWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");
            if (this.txtUserCode.Text != "")
            {
                sb.AppendFormat(" AND SALES_EMPLOYEE='{0}'", this.txtUserCode.Text.Trim());
            }
            if (this.txtStyleCode.Text != "")
            {
                sb.AppendFormat(" AND PRODUCT_STYLE='{0}'", this.txtStyleCode.Text.Trim());
            }
            if (this.txtProductGroupCode.Text != "")
            {
                sb.AppendFormat(" AND PRODUCT_GROUP_CODE='{0}'", this.txtProductGroupCode.Text.Trim());
            }
            if (this.txtDepartmentCode.Text != "")
            {
                sb.AppendFormat(" AND DEPARTMENT_CODE='{0}'", this.txtDepartmentCode.Text.Trim());
            }

            if (txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME  >= '{0}' ", CConvert.ToDateTime(txtFromDate.Text.Trim()).ToString("yyyy-MM-dd"));
            }

            if (txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME  < '{0}' ", CConvert.ToDateTime(txtToDate.Text.Trim()).AddDays(1).ToString("yyyy-MM-dd"));
            }
            return sb.ToString();
        }

        private string strGroup()
        {
            StringBuilder st = new StringBuilder();
            if (this.GroupCondition.SelectedItem.Value.ToString() == "0")
            {
                st.AppendFormat(" CREATE_DATE_TIME ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "1")
            {
                st.AppendFormat("DEPARTMENT_CODE, DEPARTMENT_NAME ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "2")
            {
                st.AppendFormat(" PRODUCT_STYLE,STYLE_NAME ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "3")
            {
                st.AppendFormat(" SALES_EMPLOYEE,SALES_NAME ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "4")
            {
                st.AppendFormat(" PRODUCT_GROUP_CODE,GROUP_NAME ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "5")
            {
                st.AppendFormat(" CREATE_DATE_TIME,DEPARTMENT_NAME,DEPARTMENT_CODE");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "6")
            {
                st.AppendFormat(" DEPARTMENT_NAME,STYLE_NAME,PRODUCT_STYLE ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "7")
            {
                st.AppendFormat("CREATE_DATE_TIME,STYLE_NAME,PRODUCT_STYLE ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "9")
            {
                st.AppendFormat("SALES_NAME ,STYLE_NAME,SALES_EMPLOYEE");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "10")
            {
                st.AppendFormat(" SALES_NAME,CREATE_DATE_TIME,SALES_EMPLOYEE ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "11")
            {
                st.Append(" COLOR,COLOR_NAME ");
            }
            if (this.GroupCondition.SelectedItem.Value.ToString() == "12")
            {
                st.Append(" SIZE,SIZE_NAME ");
            }
            return st.ToString();
        }

        public bool CheckInput()
        {
            bool b = true;
            if (!PageValidate.IsDateTimeOrEmpty(txtFromDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtFromDate.ClientID + "').value='';", true);
                b = false;
            }
            else if (!PageValidate.IsDateTimeOrEmpty(txtToDate.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtToDate.ClientID + "').value='';", true);
                b = false;
            }
            else if (this.GroupCondition.SelectedItem.Value.ToString() == "11" && this.txtProductGroupCode.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"请选择商品种类！\");processCloseAndRefreshParent();", true);
                b = false;
            }
            else if (this.GroupCondition.SelectedItem.Value.ToString() == "12" && this.txtProductGroupCode.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"请选择商品种类！\");processCloseAndRefreshParent();", true);
                b = false;
            }
            return b;
        }
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":
                    if (CheckInput())
                    {
                        Search(sender, e);
                    }
                    break;
                case "btnExcel":
                    GetExcel(sender, e);
                    break;
            }
            return true;
        }
        //导出成excel文件，放在scm下的excel文件夹下面
        private void GetExcel(object sender, EventArgs e)
        {
            DataSet ds = bll.GetSalesStatAmount(strGroup(), strWhere());
            DataTable dt = ds.Tables[0];
            CommonUtil.DataTable2Excel(dt);
        }
        protected void User_Change(object sender, EventArgs e)
        {
            if (this.txtUserCode.Text.Trim() == "")
            {
                this.lblUserName.Text = "";
                this.txtUserCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_USER", this.txtUserCode.Text, "");
            if (table != null)
            {
                this.lblUserName.Text = table.Name;
                this.txtUserCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"人员不存在！\");", true);
                this.lblUserName.Text = "";
                this.txtUserCode.Text = "";
            }
        }
        protected void SysleCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtStyleCode.Text.Trim() == "")
            {
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_STYLE", txtStyleCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblStyleName.Text = table.Name;
                this.txtStyleCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"款式不存在！\");", true);
                this.lblStyleName.Text = "";
                this.txtStyleCode.Text = "";
            }
        }
        protected void Department_Change(object sender, EventArgs e)
        {
            if (this.txtDepartmentCode.Text.Trim() == "")
            {
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_DEPARTMENT", this.txtDepartmentCode.Text, "");
            if (table != null)
            {
                this.lblDepartmentName.Text = table.Name;
                this.txtDepartmentCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"部门不存在！\");", true);
                this.lblDepartmentName.Text = "";
                this.txtDepartmentCode.Text = "";
            }
        }
        protected void ProductGroupCode_Chanage(object sender, EventArgs e)
        {
            if (this.txtProductGroupCode.Text.Trim() == "")
            {
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_PRODUCT_GROUP", txtProductGroupCode.Text.Trim(), "");
            if (table != null)
            {
                this.lblProductGroupName.Text = table.Name;
                this.txtProductGroupCode.Text = table.Code;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"种类不存在！\");", true);
                this.lblProductGroupName.Text = "";
                this.txtProductGroupCode.Text = "";
            }
        }
        protected void CharType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["ORDER_TABLE"];
            if (dt != null)
            {
                if (CharType.SelectedIndex != 2)
                {
                    SeriesChartType chartype = SeriesChartType.Line;
                    if (CharType.SelectedIndex == 0)
                    {
                        chartype = SeriesChartType.Line;
                    }
                    else
                    {
                        chartype = SeriesChartType.Column;
                    }
                    ChartStyle.SetChart(Chart1);
                    Title t1 = ChartStyle.SetTitle("title1");
                    t1.Text = "金额统计";
                    Chart1.Titles.Add(t1);
                    Series s1 = ChartStyle.SetSeriesStyle("Series1", chartype, "SALES", "AMOUNT");
                    s1.LegendText = "金额统计";
                    Chart1.Series.Add(s1);
                    ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    Chart1.ChartAreas.Add(cArea1);
                    Legend l1 = ChartStyle.SetLegend("颜色");
                    Chart1.Legends.Add(l1);
                    ChartHelper.GetSeriesPointValue(s1, dt, "SALES", "AMOUNT");

                    ChartStyle.SetChart(Chart2);
                    Title t2 = ChartStyle.SetTitle("title1");
                    t2.Text = "数量统计";
                    Chart2.Titles.Add(t2);
                    Series s3 = ChartStyle.SetSeriesStyle("Series3", chartype, "SALES", "QUANTITY");
                    s3.LegendText = "数量统计";
                    Chart2.Series.Add(s3);
                    ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                    Chart2.ChartAreas.Add(cArea3);
                    Legend l3 = ChartStyle.SetLegend("颜色");
                    Chart2.Legends.Add(l3);
                    ChartHelper.GetSeriesPointValue(s3, dt, "SALES", "QUANTITY");
                }
                else
                {
                    ChartStyle.SetChart(Chart1);
                    Title t1 = ChartStyle.SetTitle("title1");
                    t1.Text = "金额统计";
                    Chart1.Titles.Add(t1);
                    Series s1 = ChartStyle.SetSeriesStyle("Series1", SeriesChartType.Pie);
                    s1.LegendText = "金额统计";
                    Chart1.Series.Add(s1);
                    ChartArea cArea1 = ChartStyle.SetChartAreaStyle("ChartArea1");
                    Chart1.ChartAreas.Add(cArea1);
                    Legend l1 = ChartStyle.SetLegend("颜色");
                    Chart1.Legends.Add(l1);
                    ChartHelper.GetPicSeriesPointValue(s1, dt, "SALES", "AMOUNT");

                    ChartStyle.SetChart(Chart2);
                    Title t2 = ChartStyle.SetTitle("title1");
                    t2.Text = "数量统计";
                    Chart2.Titles.Add(t2);
                    Series s3 = ChartStyle.SetSeriesStyle("Series3", SeriesChartType.Pie);
                    s3.LegendText = "数量统计";
                    Chart2.Series.Add(s3);
                    ChartArea cArea3 = ChartStyle.SetChartAreaStyle("ChartArea3");
                    Chart2.ChartAreas.Add(cArea3);
                    Legend l3 = ChartStyle.SetLegend("颜色");
                    Chart2.Legends.Add(l3);
                    ChartHelper.GetPicSeriesPointValue(s3, dt, "SALES", "QUANTITY");
                }

            }
        }

    }
}

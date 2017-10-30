using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Generic;
using System.Drawing;

namespace SCM.Web
{
    /// <summary>
    ///ChartHelper 的摘要说明
    /// </summary>
    public class ChartHelper //ChartAddData
    {

        /// <summary>
        /// 非饼图SeriesPoint添加数据
        /// </summary>
        public static void GetSeriesPointValue(Series series, DataTable dt, string XColumnName, string YColumnName)
        {

            int count = dt.Rows.Count;
            double[] yValues = new double[count];
            string[] xValues = new string[count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                yValues[i] = string.IsNullOrEmpty(row[YColumnName].ToString()) ? 0.00 : double.Parse(row[YColumnName].ToString());
                xValues[i] = row[XColumnName].ToString();
                i++;
            }
            series.Points.DataBindXY(xValues, yValues);
        }

        /// <summary>
        /// 饼图SeriesPoint添加数据
        /// </summary>
        public static void GetPicSeriesPointValue(Series series, DataTable dt, string XColumnName, string YColumnName)
        {

            int count = dt.Rows.Count;
            double[] yValues = new double[count];
            string[] xValues = new string[count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                yValues[i] = string.IsNullOrEmpty(row[YColumnName].ToString()) ? 0.00 : double.Parse(row[YColumnName].ToString());
                xValues[i] = row[XColumnName].ToString();
                i++;
            }
            series.Points.DataBindXY(xValues, yValues);
            series.Label = "#PERCENT{P1}";
            series.ToolTip = "#VALY";
            series.LegendText = "#VALX (#VALY)";

        }

        /// <summary>
        /// 适用饼图 根据【列】统计
        /// </summary>
        public static void GetXYValues(DataTable dt, string[] xValues, double[] yValues, string YColumnName, string XColumnName)
        {

            int i = 0;

            foreach (DataRow row in dt.Rows)
            {

                yValues[i] = string.IsNullOrEmpty(row[YColumnName].ToString()) ? 0.00 : double.Parse(row[YColumnName].ToString());

                xValues[i] = row[XColumnName].ToString();

                i++;

            }


        }

        /// <summary>
        /// 适用饼图 根据【行】统计
        /// </summary>
        public static void GetXYValues(DataRow row, string[] xValues, double[] yValues, List<string> YColumnName)
        {

            int i = 0;

            foreach (string _column in YColumnName)
            {

                yValues[i] = string.IsNullOrEmpty(row[_column].ToString()) ? 0.00 : double.Parse(row[_column].ToString());

                xValues[i] = _column;

                i++;

            }


        }


        /// <summary>
        /// 行专列后用的 得到【 List<Series>】
        /// </summary>
        public static List<Series> GetListSeriersChange(PackageStyle.Style TBStyle)
        {


            string name = null;

            string XValueMember;

            string YValueMembers;

            DataTable dt = TBStyle.TB;

            List<Series> GetListSeriers = new List<Series>();

            for (int i = 1; i < dt.Columns.Count; i++)
            {

                name = dt.Columns[i].ColumnName.ToString();

                XValueMember = dt.Columns[0].ColumnName.ToString();

                YValueMembers = name;

                Series series = ChartStyle.SetSeriesStyle(name, TBStyle.SCtype, XValueMember, YValueMembers);


                GetListSeriers.Add(series);

            }

            return GetListSeriers;

        }


        /// <summary>
        /// 得到【 List<Series>】
        /// </summary>
        public static List<Series> GetListSeriers(PackageStyle.Style TBStyle)
        {


            string Xname = null;

            string XValueMember;

            string YValueMembers;

            List<Series> GetListSeriers = new List<Series>();

            int i = 0;

            foreach (string _YCol in TBStyle.YColumnName)
            {

                //for (int i = 0; i < YColumnName.Count; i++)

                //{

                Xname = _YCol;

                XValueMember = TBStyle.XColumnName;

                YValueMembers = Xname;

                // Series series = ChartStyle.SetSeriesStyle(Xname, stype, XValueMember, YValueMembers);

                Series series = ChartStyle.SetSeriesStyle(Xname, TBStyle.SCtype, XValueMember, YValueMembers);

                GetListSeriers.Add(series);

                // }

                i++;

            }

            return GetListSeriers;

        }
  
        /// <summary>
        /// DataTable添加空行
        /// </summary>
        public static void AddRow(DataTable table, int total)
        {

            int count = table.Columns.Count;

            int k = total - table.Rows.Count;

            if (total >= k)
            {

                object[] rowVals = new object[count];


                for (int i = 0; i < count; i++)
                {

                    rowVals[i] = null;

                }


                for (int i = 0; i < k; i++)
                {

                    table.Rows.Add(rowVals);

                }

            }

        }

        /// <summary>
        /// 行转列
        /// </summary>
       public static DataTable GetReverseTable(PackageStyle.Style TBStyle)
        {

            DataTable _table = new DataTable();

            _table.Columns.Add(TBStyle.XChinasesName);


            foreach (DataRow row in TBStyle.TB.Rows)
            {

                _table.Columns.Add(row[TBStyle.XColumnName].ToString());

            }


            foreach (string _YCol in TBStyle.YColumnName)
            {

                object[] _ObjectValue = new object[TBStyle.TB.Rows.Count + 1];

                _ObjectValue[0] = _YCol;

                int i = 1;

                foreach (DataRow row in TBStyle.TB.Rows)
                {

                    _ObjectValue[i] = row[_YCol];

                    i++;

                }

                _table.Rows.Add(_ObjectValue);

            }

            _table.TableName = TBStyle.TB.TableName;

            //TBStyle.TB = _table;

            return _table;

        }


        /// <summary>
        /// 换中文列名
        /// </summary>
        public static void GetChinasesName(PackageStyle.Style TBStyle)
        {

            TBStyle.TB.Columns[TBStyle.XColumnName].ColumnName = TBStyle.XChinasesName;


            for (int i = 0; i < TBStyle.YColumnName.Count; i++)
            {

                TBStyle.TB.Columns[TBStyle.YColumnName[i]].ColumnName = TBStyle.YChinasesName[i];

            }

            // return TBStyle.TB;

        }

    }
}
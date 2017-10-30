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


namespace SCM.Web
{
    /// <summary>
    ///Cache 的摘要说明
    /// </summary>
    public class DataCache
    {
        public DataCache()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private static DataTable _seriesChartTypeDt = null;

        public static DataTable SeriesChartTypeDt
        {
            get
            {
                if (_seriesChartTypeDt == null)
                {
                    _seriesChartTypeDt = new DataTable();
                    _seriesChartTypeDt.Columns.Add("value", Type.GetType("System.Object"));
                    _seriesChartTypeDt.Columns.Add("text", Type.GetType("System.String"));

                    DataRow row = _seriesChartTypeDt.NewRow();
                    row["value"] = SeriesChartType.Line;
                    row["text"] = "曲线图";
                    _seriesChartTypeDt.Rows.Add(row);

                    row = _seriesChartTypeDt.NewRow();
                    row["value"] = SeriesChartType.Column;
                    row["text"] = "柱形图";
                    _seriesChartTypeDt.Rows.Add(row);

                    row = _seriesChartTypeDt.NewRow();
                    row["value"] = SeriesChartType.Pie;
                    row["text"] = "饼型图";
                    _seriesChartTypeDt.Rows.Add(row);                    

                }
                return _seriesChartTypeDt;
            }
            set { _seriesChartTypeDt = value; }
        }
    }//end class   
}

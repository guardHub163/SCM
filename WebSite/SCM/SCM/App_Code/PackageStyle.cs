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
using System.Collections.Generic;
using System.Web.UI.DataVisualization.Charting;

namespace SCM.Web
{
    /// <summary>
    ///PackageStyle 的摘要说明
    /// </summary>
    public class PackageStyle
    {

        #region 定义一个完整的Table样式



        /// <summary>

        /// 定义一个完整的Table样式

        /// </summary>

        public struct Style
        {

            /// <summary>

            /// DataTable

            /// </summary>

            public DataTable TB;


            /// <summary>

            /// 是否行转列

            /// </summary>

            public bool GetReverseTable;


            /// <summary>

            /// Title的内容

            /// </summary>

            public string Title;


            /// <summary>

            /// X轴显示列名

            /// </summary>

            public string XColumnName;


            /// <summary>

            /// X轴中文对应名称

            /// </summary>

            public string XChinasesName;


            /// <summary>

            /// 需显示的在Y轴的列名

            /// </summary>

            public List<string> YColumnName;


            /// <summary>

            /// Y轴中文对应名称

            /// </summary>

            public List<string> YChinasesName;


            /// <summary>

            /// 显示图标类型

            /// </summary>

            public SeriesChartType SCtype;


            /// <summary>

            /// 是否开启3D效果

            /// </summary>

            public bool Area3DStyle;


            /// <summary>

            /// Title是否显示在图标中

            /// </summary>

            public bool TitleIsDockedInsideChartArea;


            /// <summary>

            /// 是否显示标题

            /// </summary>

            public bool TitleDisplay;


            /// <summary>

            /// legend是否显示在图标中

            /// </summary>

            public bool legendIsDockedInsideChartArea;




        }

        #endregion


    }

}
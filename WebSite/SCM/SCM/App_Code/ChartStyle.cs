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
using System.Drawing;

namespace SCM.Web
{
    /// <summary>
    ///ChartStyle 的摘要说明
    /// </summary>
    public class ChartStyle
    {
        /// <summary>
        /// 定义ChartStyle
        /// </summary>
        public static Chart SetChart(Chart chart)
        {
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BorderWidth = 2;
            chart.BackColor = System.Drawing.Color.Lavender ;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            return chart;
        }

        /// <summary>
        /// 定义ChartArea
        /// </summary>
        /// <param name="name">ChartArea名称</param>
        public static ChartArea SetChartAreaStyle(string name)
        {
            return SetChartAreaStyle(name, false);
        }


        /// <summary>
        /// 定义ChartArea
        /// </summary>
        /// <param name="name">ChartArea名称</param>
        /// <param name="Area3DStyle">是否开启3D</param>
        public static ChartArea SetChartAreaStyle(string name, bool Area3DStyle)
        {
            ChartArea mycharArea = new ChartArea(name);
            mycharArea.AxisX.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            mycharArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", float.Parse("8.25"), FontStyle.Regular);
            mycharArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);

            mycharArea.AxisY.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            mycharArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", float.Parse("8.25"), FontStyle.Regular);
            mycharArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);

            if (Area3DStyle)
            {
                mycharArea.Area3DStyle.Enable3D = true;
                mycharArea.Area3DStyle.IsRightAngleAxes = false;//是否倾斜
                mycharArea.Area3DStyle.IsClustered = false;
                //Chart1.ChartAreas["ChartArea1"].Area3DStyle.WallWidth = 0;

                mycharArea.Area3DStyle.Inclination = 15;//X倾斜度
                mycharArea.Area3DStyle.Rotation = 10;//Y倾斜度
                mycharArea.Area3DStyle.Perspective = 10;
            }


            //mycharArea.BackColor = System.Drawing.Color.FromArgb(64, 165, 191, 228);
            mycharArea.BackColor = System.Drawing.Color.Transparent;
            // BackSecondaryColor="Transparent" BorderColor="64, 64, 64, 64"
            mycharArea.BackSecondaryColor = System.Drawing.Color.Transparent;
            mycharArea.BorderColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            mycharArea.AxisY.ScaleBreakStyle.Enabled = false;//不开启对比悬殊的东东

            //mycharArea.AxisX.Interval = 1;
            //mycharArea.AxisY.Interval = 5000;
            //mycharArea.AxisX.MajorGrid.Interval = 1;
            //mycharArea.Area3DStyle.Enable3D = true;
            //mycharArea.AlignmentOrientation = AreaAlignmentOrientations.Horizontal;
            //mycharArea.AxisY.Enabled = AxisEnabled.False;
            //mycharArea.AxisY2.Enabled = AxisEnabled.True;
            //mycharArea.AxisX.LabelStyle.IsEndLabelVisible = false; 

            mycharArea.AxisX.Interval = 1; //设置X轴坐标的间隔为1
            mycharArea.AxisX.IntervalOffset = 1; //设置X轴坐标偏移为1
            mycharArea.AxisX.LabelStyle.IsStaggered = true;

            return mycharArea;
        }


        /// <summary>
        /// 定义Title
        /// </summary>
        public static Title SetTitle(string name)
        {
            return SetTitle(name, false);
        }

        /// <summary>
        /// 定义Title
        /// </summary>
        /// <param name="name">Title ID</param>
        /// <param name="TitleIsDockedInsideChartArea">是否包含在ChartArea中</param>
        public static Title SetTitle(string name, bool TitleIsDockedInsideChartArea)
        {
            Title title = new Title(name);
            title.Name = name;
            title.Alignment = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("Arial", float.Parse("14"), FontStyle.Bold);
            if (TitleIsDockedInsideChartArea)
            {
                title.IsDockedInsideChartArea = true;
                title.DockedToChartArea = name;
            }
            return title;
        }


        /// <summary>
        /// 定义Legend
        /// </summary>
        /// <param name="name">Legend ID</param>
        public static Legend SetLegend(string name)
        {
            return SetLegend(name, false);
        }

        /// <summary>
        /// 定义Legend
        /// <param name="legendIsDockedInsideChartArea">是否绘制到图表区</param>
        public static Legend SetLegend(string name, bool legendIsDockedInsideChartArea)
        {
            Legend legend = new Legend(name);
            legend.Title = name;
            legend.BackColor = Color.Transparent;//Color.FromArgb(26, 59, 105, 0);
            legend.BorderColor = Color.Gray;
            //legend.BorderColor = Color.Thistle;
            //legend.Font = new System.Drawing.Font("Trebuchet MS", float.Parse("8.25"), FontStyle.Bold, GraphicsUnit.World);
            if (legendIsDockedInsideChartArea)
            {
                legend.IsDockedInsideChartArea = true;
                legend.DockedToChartArea = name;
            }
            return legend;
        }

        /// <summary>
        /// 定义Series适用大部分图形样式------不适合样式有、饼图、空心饼图等
        /// </summary>
        public static Series SetSeriesStyle(string name, SeriesChartType stype, string XValueMember, string YValueMembers)
        {
            Series series = new Series(name);
            string PointWidth = "0.8";
            series.XValueMember = XValueMember;
            series.YValueMembers = name;
            series.ToolTip = "#VAL";
            series.Label = "#VAL";
            series["DrawingStyle"] = "Cylinder";
            // series.MarkerStyle = MarkerStyle.Circle;点标记 

            series["PointWidth"] = PointWidth;
            series.ChartType = stype;
            //series.ChartArea = name;
            //series.Legend = name;
            return series;
        }

        /// <summary>
        /// 适合样式有 、饼图、空心饼图
        /// </summary>
        public static Series SetSeriesStyle(string name, SeriesChartType stype)
        {
            Series series = new Series(name);
            series.ChartType = stype;
            return series;
        }


    }//END CLASS
}


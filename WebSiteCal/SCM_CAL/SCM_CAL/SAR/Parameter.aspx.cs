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
using SCM.Model;
using SCM.Bll;
using SCM.Common;
using log4net;
using System.Reflection;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.MobileControls;
using System.Collections.Generic;

namespace SCM.Web.SAR
{
    public partial class _Parameter : System.Web.UI.Page
    {
        BCommon bCommon = new BCommon();
        BSarParameter bll = new BSarParameter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["TYPE"];
                if (type != null && !"".Equals(type))
                {
                    switch (type)
                    {
                        case "PARAMETER_LOAD":
                            Response.Write(GetParameterInfo(type));
                            break;
                        case "PARAMETER_ONE":
                            Response.Write(GetParameterAdd(type));
                            break;
                    }
                    Response.End();
                }
            }
        }

        ///<sumary>
        ///参数界面显示
        /// </sumary>
        private string GetParameterInfo(string type)
        {
            DataTable dt = AjaxManage.ParameterLoad();
            return AjaxManage.CreateJsonParameters(dt, type);
        }



        ///<summary>
        ///参数输入界面
        ///<summary>
        private string GetParameterAdd(string type)
        {
            string INDICATOR = Request.QueryString["INDICATOR"];
            string ASP = Request.QueryString["ASP"];
            string ATV = Request.QueryString["ATV"];
            string PING = Request.QueryString["PING"];
            string HUMANEFFECT = Request.QueryString["HUMANEFFECT"];
            string Miss = Request.QueryString["MISS"];
            string PERFORMANCE = Request.QueryString["PERFORMANCE"];
            string VIP1 = Request.QueryString["VIP1"];
            string VIP2 = Request.QueryString["VIP2"];
            string LORDPRODUCTRATIO1 = Request.QueryString["LORDPRODUCTRATIO1"];
            string LORDPRODUCTRATIO2 = Request.QueryString["LORDPRODUCTRATIO2"];
            string SALESRATIO1 = Request.QueryString["SALESRATIO1"];
            string SALESRATIO2 = Request.QueryString["SALESRATIO2"];
            string DISCOUNT1 = Request.QueryString["DISCOUNT1"];
            string DISCOUNT2 = Request.QueryString["DISCOUNT2"];
            string LOSSRATEL1 = Request.QueryString["LOSSRATEL1"];
            string LOSSRATEL2 = Request.QueryString["LOSSRATEL2"];
            string COMPART1 = Request.QueryString["COMPART1"];
            string COMPART2 = Request.QueryString["COMPART2"];
            string ONEINDICATOR = Request.QueryString["ONEINDICATOR"];
            string TWOINDICATOR = Request.QueryString["TWOINDICATOR"];
            string THREEINDICATOR = Request.QueryString["THREEINDICATOR"];
            string FOURINDICATOR = Request.QueryString["FOURINDICATOR"];
            string FIVEINDICATOR = Request.QueryString["FIVEINDICATOR"];
            string SIXINDICATOR = Request.QueryString["SIXINDICATOR"];
            string SEVENINDICATOR = Request.QueryString["SEVENINDICATOR"];
            string EIGHTINDICATOR = Request.QueryString["EIGHTINDICATOR"];
            string NINEINDICATOR = Request.QueryString["NINEINDICATOR"];
            string TENINDICATOR = Request.QueryString["TENINDICATOR"];
            string ELEVENINDICATOR = Request.QueryString["ELEVENINDICATOR"];
            string TWELVEINDICATOR = Request.QueryString["TWELVEINDICATOR"];
            DataTable dt = AjaxManage.ParameterInt(INDICATOR, ASP, ATV, PING, HUMANEFFECT, Miss, PERFORMANCE, VIP1, VIP2, LORDPRODUCTRATIO1, LORDPRODUCTRATIO2, SALESRATIO1, SALESRATIO2, DISCOUNT1, DISCOUNT2, LOSSRATEL1, LOSSRATEL2, COMPART1, COMPART2, ONEINDICATOR, TWOINDICATOR, THREEINDICATOR, FOURINDICATOR, FIVEINDICATOR, SIXINDICATOR, SEVENINDICATOR, EIGHTINDICATOR, NINEINDICATOR, TENINDICATOR, ELEVENINDICATOR, TWELVEINDICATOR);
            return AjaxManage.CreateJsonParameters(dt, type);
        }
    }
}

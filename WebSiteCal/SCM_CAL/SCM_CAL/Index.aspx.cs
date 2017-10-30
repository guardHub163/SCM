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
using log4net;
using System.Reflection;
using System.Text;

namespace SCM.Web.SAR
{
    public partial class _Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string type = Request.QueryString["TYPE"];
                if (type != null && !"".Equals(type))
                {
                    switch (type)
                    {
                        case "ANALYSE_LOAD":
                            Response.Write(GetDepartmentInfo(type));
                            break;
                        case "ANALYSE_SEARCH_ONE":
                            Response.Write(GetDeparetment(type));
                            break;
                        case "ANALYSE_SEARCH_TWO":
                            Response.Write(GetDepartmentIndex(type));
                            break;
                        case "ANALYSE_SEARCH_THREE":
                            Response.Write(GetEmployeeSAR(type));
                            break;
                        case "ANALYSE_SEARCH_FOUR":
                            Response.Write(GetDepartmentScore(type));
                            break;
                        case "ANALYSE_SEARCH_FIVE":
                            Response.Write(GetProductAmountQuantity(type));
                            break;
                        case "ANALYSE_SEARCH_SIX":
                            Response.Write(GetOpinionInfo(type));
                            break;
                    }
                    Response.End();
                }
            }
        }


        /// <summary>
        /// 部门绑定
        /// </summary>
        private string GetDepartmentInfo(string type)
        {
            DataTable dt = AjaxManage.GetDepartmentInfo();
            return AjaxManage.CreateJsonParameters(dt, type);

        }
        ///<summary>
        ///门店基本信息绑定
        ///</summary>
        private string GetDeparetment(string type)
        {
            string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
            DataTable dt = AjaxManage.GetDepartment(departmentCode);
            return AjaxManage.CreateJsonParameters(dt, type);
        }
        ///<summary>
        ///指标栏
        ///</summary>
        private string GetDepartmentIndex(string type)
        {
            string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
            string datetime = Request.QueryString["DATE"];
            string todatatime = Request.QueryString["TODATE"];
            string employee = Request.QueryString["EMPLOYEE"];
            string area = Request.QueryString["AREA"];
            DataTable dt = AjaxManage.GetDepartmentIndex(departmentCode, Convert.ToDateTime(datetime), Convert.ToDateTime(todatatime), employee, area);
            return AjaxManage.CreateJsonParameters(dt, type);
        }

        /// <summary>
        /// 人员信息
        /// </summary>
        private string GetEmployeeSAR(string type)
        {
            string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
            string date = Request.QueryString["DATE"];
            string totaluser = Request.QueryString["TOTAL"];
            string todatatime = Request.QueryString["TODATE"];
            DataTable dt = AjaxManage.GetEmployeeSAR(departmentCode, Convert.ToDateTime(date), Convert.ToDateTime(todatatime), totaluser);
            return AjaxManage.CreateJsonParameters(dt, type);
        }

        ///<SUMMATY>
        ///分类商品销售占比报告
        /// </SUMMATY>
        private string GetProductAmountQuantity(string type)
        {
            string departmentCode = Request.QueryString["DEPARTMENT_CODE"];
            string date = Request.QueryString["DATE"];
            string AMOUNT = Request.QueryString["AMOUNT"];
            string todatatime = Request.QueryString["TODATE"];
            DataTable dt = AjaxManage.GetProductAmountQuantity(departmentCode, Convert.ToDateTime(date), Convert.ToDateTime(todatatime), AMOUNT);
            return AjaxManage.CreateJsonParameters(dt, type);
        }


        ///<summary>
        ///评分显示
        /// </summary>
        private string GetDepartmentScore(string type)
        {
            string StandardRate = Request.QueryString["STANDARDRATE"];
            string Lianx = Request.QueryString["LIANX"];
            string Ping = Request.QueryString["PING"];
            string HumanEffect = Request.QueryString["HUMANEFFECT"];
            string Asp = Request.QueryString["ASP"];
            string JointSalesRate = Request.QueryString["JOINTSALESRATE"];
            string vip = Request.QueryString["VIP"];
            string lossRate = Request.QueryString["LOSSRATE"];
            string Missing = Request.QueryString["MISSING"];
            string SalesRatio = Request.QueryString["SALESRATION"];
            string DiscountRate = Request.QueryString["DISCOUNTRATE"];
            string Fraction = Request.QueryString["FRACTION"];
            string compared = Request.QueryString["COMPARED"];
            DataTable dt = AjaxManage.GetScoreInfo(StandardRate, Lianx, Ping, HumanEffect, Asp, JointSalesRate, vip, lossRate, Missing, SalesRatio, DiscountRate, compared, Fraction);
            return AjaxManage.CreateJsonParameters(dt, type);
        }

        ///<summary>
        ///综合判断
        ///</summary>
        private string GetOpinionInfo(string type)
        {
            string StandardRate = Request.QueryString["STANDARDRATE"];
            string Asp = Request.QueryString["ASP"];
            string Atv = Request.QueryString["ATV"];
            string JointSalesRate = Request.QueryString["JOINTSALESRATE"];
            string Vip = Request.QueryString["VIP"];
            string LossRate = Request.QueryString["LOSSRATE"];
            string Missing = Request.QueryString["MISSING"];
            string SalesRatio = Request.QueryString["SALESRATION"];
            string DiscountRate = Request.QueryString["DISCOUNTRATE"];
            string Fraction = Request.QueryString["FRACTION"];
            string Compared = Request.QueryString["COMPARED"];
            DataTable dt = AjaxManage.GetOpinionInfo(StandardRate, Asp, Atv, JointSalesRate, Vip, LossRate, Missing, SalesRatio, DiscountRate, Compared, Fraction);
            return AjaxManage.CreateJsonParameters(dt, type);
        }

        
    }//end class
}
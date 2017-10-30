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
using SCM.Bll;
using SCM.Model;
using System.Collections.Generic;
using System.Collections;
using SCM.Common;
using System.Text;

/// <summary>
///AjaxManage 的摘要说明
/// </summary>
namespace SCM.Web.SAR
{
    public class AjaxManage
    {

        private static BSarSalesOrder bSales = new BSarSalesOrder();
        private static BSarParameter bParameter = new BSarParameter();
        private static BCommon bCommon = new BCommon();

        public AjaxManage()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 部门信息
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public static DataTable GetDepartment(string departmentCode)
        {
            DataSet ds = bSales.GetDepartmentInfo(departmentCode);
            DataTable dt = ds.Tables[0];
            if (dt != null & !"".Equals(dt.Rows[0]))
            {
                dt.Columns.Add("Employee", Type.GetType("System.String"));
            }
            else
            {
                return new DataTable();
            }

            DataSet dsusernumber = bSales.GetUserNumber(departmentCode);
            DataTable dausernumber = dsusernumber.Tables[0];
            if (dausernumber != null & !"".Equals(dausernumber.Rows[0]))
            {
                dt.Rows[0]["Employee"] = dausernumber.Rows[0]["NUMBER"];
            }
            else
            {
                return new DataTable();
            }
            return dt;
        }

        /// <summary>
        /// 指标栏
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDepartmentIndex(string departmentCode, DateTime datetime, DateTime todate, string employee, string area)
        {

            string asp = "";
            string atv = "";
            string Amount = "";
            string ATV = "";
            string ASP = "";
            string Compared = "";
            string StandardRate = "";
            string DiscountRate = "";
            string JointSalesRate = "";
            string VIP = "";
            string ClassificationSalesRatio = "";
            string HumanEffect = "";
            string Ping = "";
            string StorePerformance = "";
            string Discount = "";
            string PriceText = "";
            string StoreVIP = "";
            string Lianx = "";
            string ProductMix = "";
            string SamePerformance = "";
            string indicator = "";
            string pastlianx = "";
            string salesRatio = "";
            string salesRatioinfo = "";

            #region 时间控制业绩指标
            DataSet dsmonthcount = bSales.GetAddMonthCount();
            DataTable damonthcount = dsmonthcount.Tables[0];
            DataSet dsPerformance = bParameter.GetParameterNumber("INDICATOR");
            DataTable daPerformance = dsPerformance.Tables[0];
            string monthAmount = "";
            if (Convert.ToDecimal(damonthcount.Rows[0]["COUNT"]) < 0)
            {
                monthAmount = Convert.ToString(Convert.ToDecimal(daPerformance.Rows[0]["NUMBER1"]) / 12);
            }
            else
            {
                int monthnumber = bSales.GetMonthtd(datetime, todate);//相差的月份
                string datemonth = datetime.Month.ToString();//起始时间的月份
                string dateyear = datetime.Year.ToString();//其实时间的年份
                string dateday = datetime.Day.ToString();//起始时间的天数
                string tomonth = todate.Month.ToString();//结束时间的月份
                string toDay = todate.Day.ToString();//结束时间的天数
                string toyear = todate.Year.ToString();//结束时间的年份
                int datenumber = DateTime.DaysInMonth(Convert.ToInt32(dateyear), Convert.ToInt32(datemonth));//起始时间的总天数
                int tonember = DateTime.DaysInMonth(Convert.ToInt32(toyear), Convert.ToInt32(tomonth));//结束时间的总天数
                //起始月份的业绩指标（这个月的总业绩指标/这个月的总天数*这个月需求计算的天数）
                decimal dateamont = Convert.ToDecimal(GetOneMonthAmount(datemonth)) / Convert.ToDecimal(datenumber) * (Convert.ToDecimal(datenumber) - Convert.ToDecimal(dateday));
                //结束月份的业绩（这个月的总业绩指标/这个月的总天数*这个月需求计算的天数）
                decimal toamount = Convert.ToDecimal(GetOneMonthAmount(tomonth)) / Convert.ToDecimal(tonember) * (Convert.ToDecimal(toDay) - Convert.ToDecimal(1));
                decimal allamount = 0;
                if (monthnumber > 1)
                {
                    for (int i = 1; i < monthnumber; i++)
                    {
                        string month = datetime.AddMonths(i).Month.ToString();
                        allamount += Convert.ToDecimal(GetOneMonthAmount(month));
                    }
                }
                monthAmount = Convert.ToString(dateamont + toamount + allamount);


            }
            #endregion
            #region 各种时间已经总金额的取得
            double daynumber = (double)todate.Subtract(datetime).Days;
            DateTime pastTime = datetime.AddDays(-daynumber);
            DateTime pasttwoTime = pastTime.AddDays(-daynumber);
            DateTime pastyear = datetime.AddYears(-1);
            DateTime pasttoyear = todate.AddYears(-1);
            DataSet daAmount = bSales.GetOneDepartmentAmount(departmentCode, datetime, todate);
            DataSet dsAllSlipNumber = bSales.GetAllSlipNumbercount(departmentCode, datetime, todate);
            DataTable daAllSlipNumber = dsAllSlipNumber.Tables[0];
            DataSet dsSmallSlipNumber = bSales.GetSmallSlipNumbercount(departmentCode, datetime, todate);
            DataTable daSmallSlipNumber = dsSmallSlipNumber.Tables[0];
            DataTable da = daAmount.Tables[0];
            if (da.Rows[0]["AMOUNT"].ToString() != "" && !"".Equals(da.Rows[0].ToString()))
            {
                Amount = da.Rows[0]["AMOUNT"].ToString();
                atv = Convert.ToString(Convert.ToDecimal(daAllSlipNumber.Rows[0]["SLIP_NUMBER"]) - Convert.ToDecimal(daSmallSlipNumber.Rows[0]["SLIP_NUMBER"]));
                asp = da.Rows[0]["QUANTITY"].ToString();
            }
            else
            {
                return new DataTable();
            }
            #endregion

            #region 达标率的判断
            //DataSet dsPerformance = bsarp.GetParameterNumber("INDICATOR");
            //DataTable daPerformance = dsPerformance.Tables[0];
            if (daPerformance.Rows.Count != 0)
            {
                if (Convert.ToDecimal(Amount) / Convert.ToDecimal(monthAmount) < Convert.ToDecimal(0.8))
                {
                    StorePerformance = "达标率异常，请务必查明原因，请注意气候、季节、政治、环境等要素。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(monthAmount) >= Convert.ToDecimal(0.8) && Convert.ToDecimal(Amount) / Convert.ToDecimal(daPerformance.Rows[0]["NUMBER1"]) < Convert.ToDecimal(0.9))
                {
                    StorePerformance = "达标率不正常，请高度注意，查明原因，请注意员工销售能力、货品的市场认可度。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(monthAmount) >= Convert.ToDecimal(0.9) && Convert.ToDecimal(Amount) / Convert.ToDecimal(daPerformance.Rows[0]["NUMBER1"]) < Convert.ToDecimal(1))
                {
                    StorePerformance = "达标率不正常请查明原因。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(monthAmount) >= Convert.ToDecimal(1) && Convert.ToDecimal(Amount) / Convert.ToDecimal(daPerformance.Rows[0]["NUMBER1"]) < Convert.ToDecimal(1.1))
                {
                    StorePerformance = "达标率正常，请继续保持。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(monthAmount) >= Convert.ToDecimal(1.1))
                {
                    StorePerformance = "达标率远远超出设定值，请分析销售良好的原因，同时考虑是否有必要调高给出的业绩指标值。";
                }
            }
            #endregion

            #region 连续比
            DataSet dspastAmount = bSales.GetOneDepartmentAmount(departmentCode, pastTime, datetime);
            DataSet dspasttwoAmount = bSales.GetOneDepartmentAmount(departmentCode, pasttwoTime, pastTime);
            DataTable dapasttwo = dspasttwoAmount.Tables[0];
            DataTable dapast = dspastAmount.Tables[0];
            if (dapast.Rows.Count != 0 && dapast.Rows[0][0].ToString() != "")
            {
                Lianx = CConvert.FormateRate(Convert.ToString((Convert.ToDecimal(Amount) / Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]))));
            }
            if (dapasttwo.Rows[0]["AMOUNT"].ToString() != "" && dapasttwo.Rows[0]["AMOUNT"].ToString() != "0") //上个月的连续比
            {
                pastlianx = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]) / Convert.ToDecimal(dapasttwo.Rows[0]["AMOUNT"])));
            }
            else
            {
                pastlianx = "0";
            }
            #endregion

            #region 进销比
            DataSet dscountquantity = bSales.GetCountQuantity(departmentCode, datetime, todate);
            DataTable daquantity = dscountquantity.Tables[0];
            DataSet dsSalesratio = bParameter.GetParameterNumber("SALESRATIO");
            DataTable daSalesratio = dsSalesratio.Tables[0];
            if (daquantity.Rows[0]["QUANTITY"].ToString() != "0" && daSalesratio.Rows.Count != 0)
            {
                salesRatio = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(asp) / Convert.ToDecimal(daquantity.Rows[0]["QUANTITY"]) * 100)) + "%";
                if (Convert.ToDecimal(asp) / Convert.ToDecimal(daquantity.Rows[0]["QUANTITY"]) * 100 > Convert.ToDecimal(daSalesratio.Rows[0]["NUMBER1"]))
                {
                    salesRatioinfo = "销售状况良好，请注意库存量，适当增加补货。";
                }
                else if (Convert.ToDecimal(asp) / Convert.ToDecimal(daquantity.Rows[0]["QUANTITY"]) * 100 < Convert.ToDecimal(daSalesratio.Rows[0]["NUMBER2"]))
                {
                    salesRatioinfo = "销售数量低于预估，请减少补货量的同时，查明原因。";
                }
                else if (Convert.ToDecimal(asp) / Convert.ToDecimal(daquantity.Rows[0]["QUANTITY"]) * 100 >= Convert.ToDecimal(daSalesratio.Rows[0]["NUMBER1"]) && Convert.ToDecimal(asp) / Convert.ToDecimal(daquantity.Rows[0]["QUANTITY"]) * 100 <= Convert.ToDecimal(daSalesratio.Rows[0]["NUMBER2"]))
                {
                    salesRatioinfo = "进销状况在可控范围。";
                }
            }
            else
            {
                salesRatio = "0%";
            }
            #endregion

            #region 同比
            DataSet dspastYearAmount = bSales.GetOneDepartmentAmount(departmentCode, pastyear, pasttoyear);
            DataTable dapastyear = dspastYearAmount.Tables[0];
            if (dapastyear.Rows.Count != 0 && dapastyear.Rows[0][0].ToString() != "")
            {
                Compared = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100)) + "%";
            }
            else
            {
                Compared = "";
            }
            if (Compared != "" && Lianx != "" && pastlianx != "0")
            {
                if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 < 0 && (Convert.ToDecimal(Lianx) / Convert.ToDecimal(pastlianx)) * 100 < 100)
                {
                    SamePerformance = "年同期业绩增长非常不好。和上月相比业绩下降。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 < 0 && (Convert.ToDecimal(Lianx) / Convert.ToDecimal(pastlianx)) * 100 > 100)
                {
                    SamePerformance = "年同期业绩增长非常不好。和上月相比业绩上升。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 > 0 && Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 < 10 && (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]) / Convert.ToDecimal(pastlianx)) * 100 < 100)
                {
                    SamePerformance = "年同期业绩增长未达到预期设定，和上月相比业绩下降。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 > 0 && Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 < 10 && (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]) / Convert.ToDecimal(pastlianx)) * 100 > 100)
                {
                    SamePerformance = "年同期业绩增长未达到预期设定，和上月相比业绩上升。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 > 10 && Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 < 15 && (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]) / Convert.ToDecimal(pastlianx)) * 100 < 100)
                {
                    SamePerformance = "年同期业绩增长达到预期设定，和上月相比业绩下降。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 > 10 && Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 < 15 && (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]) / Convert.ToDecimal(pastlianx)) * 100 < 100)
                {
                    SamePerformance = "年同期业绩增长达到预期设定，和上月相比业绩上升。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 > 15 && (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]) / Convert.ToDecimal(pastlianx)) * 100 > 100)
                {
                    SamePerformance = "年同期业绩增长很好，和上月相比业绩上升。";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 > 15 && (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapast.Rows[0]["AMOUNT"]) / Convert.ToDecimal(pastlianx)) * 100 < 100)
                {
                    SamePerformance = "年同期业绩增长很好，和上月相比业绩下降。";
                }
            }
            #endregion

            #region 达标率
            StandardRate = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(Amount) / Convert.ToDecimal(monthAmount) * 100)) + "%";
            #endregion

            #region 折扣率
            DataSet dsDiscountRate = bSales.GetDiscountAmount(departmentCode, datetime, todate);
            DataTable daDiscountRate = dsDiscountRate.Tables[0];
            if (daDiscountRate.Rows.Count != 0 && daDiscountRate.Rows[0][0].ToString() != "" && daDiscountRate.Rows[0][1].ToString() != "" && daDiscountRate.Rows[0][2].ToString() != "")
            {
                DiscountRate = CConvert.FormateRate(Convert.ToString((Convert.ToDecimal(daDiscountRate.Rows[0]["DISCOUNT_RATE"]) + Convert.ToDecimal(daDiscountRate.Rows[0]["PROMOTION_DISCOUNTS"])) / Convert.ToDecimal(daDiscountRate.Rows[0]["ORI_PRICE"]) * 100)) + "%";
            }
            else
            {
                DiscountRate = "0" + "%";
            }
            DataSet dsparDiscount = bParameter.GetParameterNumber("DISCOUNT");
            DataTable daparDiscount = dsparDiscount.Tables[0];
            if (DiscountRate != "" || daparDiscount.Rows.Count != 0)
            {
                if ((Convert.ToDecimal(daDiscountRate.Rows[0]["DISCOUNT_RATE"]) + Convert.ToDecimal(daDiscountRate.Rows[0]["PROMOTION_DISCOUNTS"])) / Convert.ToDecimal(daDiscountRate.Rows[0]["ORI_PRICE"]) * 100 < Convert.ToDecimal(daparDiscount.Rows[0]["NUMBER1"]))
                {
                    Discount = "折扣率较低，请注意让利。";
                }
                else if ((Convert.ToDecimal(daDiscountRate.Rows[0]["DISCOUNT_RATE"]) + Convert.ToDecimal(daDiscountRate.Rows[0]["PROMOTION_DISCOUNTS"])) / Convert.ToDecimal(daDiscountRate.Rows[0]["ORI_PRICE"]) * 100 > Convert.ToDecimal(daparDiscount.Rows[0]["NUMBER2"]))
                {
                    Discount = "折扣率过高，请注意管控。";
                }
                else
                {
                    Discount = "折扣率在设定范围内，请继续保持。";
                }
            }
            #endregion

            #region 连带销售率
            if (asp != "" && atv != "")
            {
                JointSalesRate = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(asp) / Convert.ToDecimal(atv) * 100)) + "%";
            }
            //ATV,ASP
            ATV = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(Amount) / Convert.ToDecimal(atv)));
            ASP = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(Amount) / Convert.ToDecimal(asp)));
            DataSet dsparAtv = bParameter.GetParameterNumber("ATV");
            DataTable daparAtv = dsparAtv.Tables[0];
            DataSet dsparAsp = bParameter.GetParameterNumber("ASP");
            DataTable daparAsp = dsparAsp.Tables[0];
            if (asp != "" || daparAsp.Rows.Count != 0 || daparAtv.Rows.Count != 0)
            {
                if (Convert.ToDecimal(Amount) / Convert.ToDecimal(asp) * 100 < 105 && Convert.ToDecimal(Amount) / Convert.ToDecimal(asp) * 100 > 95)
                {
                    PriceText = "定价在设定范围内，请保持!";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(asp) * 100 < 95)
                {
                    PriceText = "未达到消费群体的消费能力  可考虑提高定价!";
                }
                else if (Convert.ToDecimal(Amount) / Convert.ToDecimal(asp) * 100 > 105)
                {
                    PriceText = "已超过消费群体的消费能力，可考虑降低定价!";
                }
            }
            #endregion.

            #region VIP
            DataSet dsvip = bSales.GetVipInfo(departmentCode, datetime, todate);
            DataTable davip = dsvip.Tables[0];
            if (davip.Rows.Count != 0 && davip.Rows[0]["PRICE"].ToString() != "")
            {
                VIP = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(davip.Rows[0]["PRICE"]) / Convert.ToDecimal(Amount) * 100)) + "%";
            }
            else
            {
                VIP = "0" + "%";
            }
            DataSet dsparVip = bParameter.GetParameterNumber("VIP");
            DataTable daparVip = dsparVip.Tables[0];
            if (VIP != "0%" && daparVip.Rows.Count != 0)
            {
                if (Convert.ToDecimal(davip.Rows[0]["PRICE"]) / Convert.ToDecimal(Amount) * 100 < Convert.ToDecimal(daparVip.Rows[0]["NUMBER1"]))
                {
                    StoreVIP = "VIP占比较低，有顾客流失，请检查服务质量，请检查商品是市场认可度。";
                }
                else if (Convert.ToDecimal(davip.Rows[0]["PRICE"]) / Convert.ToDecimal(Amount) * 100 > Convert.ToDecimal(daparVip.Rows[0]["NUMBER2"]))
                {
                    StoreVIP = "VIP占比较高，请注意开发新客户。";
                }
                else
                {
                    StoreVIP = "VIP占比适中，请保持。";
                }
            }
            #endregion

            #region 分类商品销售比
            DataSet dsproduct = bSales.GetProdeuctAmount(departmentCode, datetime, todate);
            DataTable daproduct = dsproduct.Tables[0];
            DataSet dsparproduct = bParameter.GetParameterNumber("LORD_PRODUCT_RATIO");
            DataTable daparproduct = dsparproduct.Tables[0];
            if (daproduct.Rows.Count != 0 && daproduct.Rows[0]["PRICE"].ToString() != "")
            {
                ClassificationSalesRatio = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(daproduct.Rows[0]["PRICE"]) / Convert.ToDecimal(Amount) * 100)) + "%";

            }
            else
            {
                ClassificationSalesRatio = "0" + "%";
            }
            if (ClassificationSalesRatio != "0%" && daparproduct.Rows.Count != 0)
            {
                if (Convert.ToDecimal(daproduct.Rows[0]["PRICE"]) / Convert.ToDecimal(Amount) * 100 < Convert.ToDecimal(daparproduct.Rows[0]["NUMBER1"]))
                {
                    ProductMix = "配比商品销售比例偏低，请注意员工的附件销售能力和货品组合的合理，以及顾客的消费心理。";
                }
                else if (Convert.ToDecimal(daproduct.Rows[0]["PRICE"]) / Convert.ToDecimal(Amount) * 100 > Convert.ToDecimal(daparproduct.Rows[0]["NUMBER2"]))
                {
                    ProductMix = "配比商品的销售比例偏高，请调整主商品PB的设定值。";
                }
                else
                {
                    ProductMix = "配比商品的销售比例适中，请保持。";
                }
            }
            #endregion

            #region 坪效
            if (area != "")
            {
                Ping = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(Amount) / Convert.ToDecimal(area))));
            }
            else
            {
                Ping = "0";
            }
            #endregion

            #region 人效
            if (employee != "" && employee != "0")
            {
                HumanEffect = CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(Amount) / Convert.ToDecimal(employee)));
            }
            else
            {
                HumanEffect = "0";
            }
            #endregion

            #region 业绩指标综合判断
            int a = 0;//达标率，同比完成的
            int c = 0;//坪效人效完成的
            int d = 0;//坪效人效未完成
            decimal fraction = 0;
            DataSet dsparpermance = bParameter.GetParameterNumber("PERFORMANCE");//达标率
            DataTable daparpermance = dsparpermance.Tables[0];
            DataSet dsparindicator = bParameter.GetParameterNumber("INDICATOR");//业绩指标
            DataTable daparindicator = dsparindicator.Tables[0];
            DataSet dsparcompared = bParameter.GetParameterNumber("COMPARED");//同比
            DataTable daparcompared = dsparcompared.Tables[0];
            DataSet dsparping = bParameter.GetParameterNumber("PING");
            DataTable daparping = dsparping.Tables[0];
            DataSet dsparhumaneffect = bParameter.GetParameterNumber("HUMANEFFECT");
            DataTable daparhumaneffect = dsparhumaneffect.Tables[0];
            if (dapastyear.Rows.Count != 0 && dapastyear.Rows[0][0].ToString() != "" || daparindicator.Rows.Count != 0 && daparindicator.Rows[0][0].ToString() != "" || daparpermance.Rows.Count != 0 && daparpermance.Rows[0][0].ToString() != "")
            {
                if (Convert.ToDecimal(Amount) / Convert.ToDecimal(daparindicator.Rows[0]["NUMBER1"]) * 100 > Convert.ToDecimal(daparpermance.Rows[0]["NUMBER1"]) || Convert.ToDecimal(Amount) / Convert.ToDecimal(daparindicator.Rows[0]["NUMBER1"]) * 100 == Convert.ToDecimal(daparpermance.Rows[0]["NUMBER1"]))
                {
                    a++;
                }
                if (dapastyear.Rows[0][0].ToString() != "")
                {
                    if (Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 > Convert.ToDecimal(daparcompared.Rows[0]["NUMBER1"]) || Convert.ToDecimal(Amount) / Convert.ToDecimal(dapastyear.Rows[0]["AMOUNT"]) * 100 == Convert.ToDecimal(daparcompared.Rows[0]["NUMBER1"]))
                    {
                        a++;
                    }
                }
                if (area != "")
                {
                    if (Convert.ToDecimal(Amount) / Convert.ToDecimal(area) * 100 < Convert.ToDecimal(daparping.Rows[0]["NUMBER1"]))
                    {
                        d++;
                    }
                    if (Convert.ToDecimal(Amount) / Convert.ToDecimal(area) * 100 > Convert.ToDecimal(daparping.Rows[0]["NUMBER1"]) || Convert.ToDecimal(Amount) / Convert.ToDecimal(area) * 100 == Convert.ToDecimal(daparping.Rows[0]["NUMBER1"]))
                    {
                        c++;
                    }
                }
                if (Convert.ToDecimal(Amount) / Convert.ToDecimal(employee) < Convert.ToDecimal(daparhumaneffect.Rows[0]["NUMBER1"]))
                {
                    d++;
                }
                if (Convert.ToDecimal(Amount) / Convert.ToDecimal(employee) > Convert.ToDecimal(daparhumaneffect.Rows[0]["NUMBER1"]) || Convert.ToDecimal(Amount) / Convert.ToDecimal(employee) == Convert.ToDecimal(daparhumaneffect.Rows[0]["NUMBER1"]))
                {
                    c++;
                }
                if (a == 0)
                {
                    indicator = "业绩指标完成状况极不理想，请严重关注，查明原因。";
                    fraction = 1;
                }
                if (a == 1 && d >= 1)
                {
                    indicator = "业绩指标完成状况不理想，请注意查明原因。";
                    fraction = 2;
                }
                if (a == 1 && c == 2)
                {
                    indicator = "业绩指标完成状况未全部完成。";
                    fraction = 3;
                }
                if (a == 2 && d >= 1)
                {
                    indicator = "业绩指标完成状况良好。";
                    fraction = 4;
                }
                if (a == 2 && c == 2)
                {
                    indicator = "业绩指标均已完成，请继续保持。";
                    fraction = 5;
                }
            }
            #endregion

            #region 绑定表
            DataTable dtable = new DataTable();
            dtable.Columns.Add("AMOUNT", Type.GetType("System.String"));
            dtable.Columns.Add("ATV", Type.GetType("System.String"));
            dtable.Columns.Add("ASP", Type.GetType("System.String"));
            dtable.Columns.Add("Compared", Type.GetType("System.String"));
            dtable.Columns.Add("StandardRate", Type.GetType("System.String"));
            dtable.Columns.Add("DiscountRate", Type.GetType("System.String"));
            dtable.Columns.Add("JointSalesRate", Type.GetType("System.String"));
            dtable.Columns.Add("VIP", Type.GetType("System.String"));
            dtable.Columns.Add("ClassificationSalesRatio", Type.GetType("System.String"));
            dtable.Columns.Add("HumanEffect", Type.GetType("System.String"));
            dtable.Columns.Add("Ping", Type.GetType("System.String"));
            dtable.Columns.Add("StorePerformance", Type.GetType("System.String"));
            dtable.Columns.Add("SalesRatio", Type.GetType("System.String"));
            dtable.Columns.Add("Discount", Type.GetType("System.String"));
            dtable.Columns.Add("PriceText", Type.GetType("System.String"));
            dtable.Columns.Add("StoreVIP", Type.GetType("System.String"));
            dtable.Columns.Add("ProductMix", Type.GetType("System.String"));
            dtable.Columns.Add("Lianx", Type.GetType("System.String"));
            dtable.Columns.Add("SamePerformance", Type.GetType("System.String"));
            dtable.Columns.Add("Indicator", Type.GetType("System.String"));
            dtable.Columns.Add("Fraction", Type.GetType("System.String"));
            dtable.Columns.Add("SalesRatioinfo", Type.GetType("System.String"));
            DataRow row = dtable.NewRow();
            row["AMOUNT"] = Amount;
            row["ATV"] = ATV;
            row["ASP"] = ASP;
            row["Compared"] = Compared;
            row["StandardRate"] = StandardRate;
            row["DiscountRate"] = DiscountRate;
            row["JointSalesRate"] = JointSalesRate;
            row["VIP"] = VIP;
            row["ClassificationSalesRatio"] = ClassificationSalesRatio;
            row["HumanEffect"] = HumanEffect;
            row["Ping"] = Ping;
            row["SalesRatio"] = salesRatio;
            row["StorePerformance"] = StorePerformance;
            row["Discount"] = Discount;
            row["PriceText"] = PriceText;
            row["StoreVIP"] = StoreVIP;
            if (Lianx != "")
            {
                row["Lianx"] = Convert.ToString(Convert.ToDecimal(Lianx) * 100) + "%";
            }
            else
            {
                row["Lianx"] = Lianx;
            }
            row["ProductMix"] = ProductMix;
            row["SamePerformance"] = SamePerformance;
            row["Indicator"] = indicator;
            row["Fraction"] = fraction;
            row["SalesRatioinfo"] = salesRatioinfo;
            dtable.Rows.Add(row);

            return dtable;
            #endregion

        }

        ///<summary>
        ///员工信息
        ///</summary>
        public static DataTable GetEmployeeSAR(string departmentCode, DateTime datetime, DateTime todatetime, string totaluser)
        {
            string atv = "";
            string Amount = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("USERNAME", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT_SORT", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT_COMPARE", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY_SORT", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY_COMPARE", Type.GetType("System.String"));
            dt.Columns.Add("JOINTSALESRATE", Type.GetType("System.String"));
            dt.Columns.Add("AVERAGEAMOUNT", Type.GetType("System.String"));
            dt.Columns.Add("AVERAGEQUANTITY", Type.GetType("System.String"));
            DataSet saleds = bSales.GetAmountRanking(departmentCode, datetime, todatetime);
            DataTable saletable = saleds.Tables[0];//人员总金额的排名
            if (saletable.Rows.Count == 0)
            {
                return new DataTable();
            }
            saletable.Columns.Add("SaleNumber", Type.GetType("System.String"));
            saletable.Columns.Add("NumberId", Type.GetType("System.String"));
            saletable.Columns.Add("ATV", Type.GetType("System.String"));
            DataSet number = bSales.GetSlipNumber(departmentCode, datetime, todatetime);
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
            DataSet daAmount = bSales.GetOneDepartmentAmount(departmentCode, datetime, todatetime);
            DataTable da = daAmount.Tables[0];
            DataSet dsAllSlipNumber = bSales.GetAllSlipNumbercount(departmentCode, datetime, todatetime);
            DataTable daAllSlipNumber = dsAllSlipNumber.Tables[0];
            DataSet dsSmallSlipNumber = bSales.GetSmallSlipNumbercount(departmentCode, datetime, todatetime);
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

        ///<summary>
        ///分类商品销售占比报告
        /// </summary>
        public static DataTable GetProductAmountQuantity(string departmentCode, DateTime datetime, DateTime todatetime, string amount)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("NAME", Type.GetType("System.String"));
            dt.Columns.Add("AMOUNT", Type.GetType("System.String"));
            dt.Columns.Add("SORT", Type.GetType("System.String"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.String"));
            DataSet ds = bSales.GetProductAmountQuantity(departmentCode, datetime, todatetime);
            DataTable da = ds.Tables[0];
            if (da.Rows.Count == 0)
            {
                return new DataTable();
            }
            foreach (DataRow row in da.Rows)
            {
                DataRow rows = dt.NewRow();
                rows["NUMBER"] = row["NUMBER"];
                rows["NAME"] = row["PRODUCT_NAME"];
                rows["AMOUNT"] = row["PRICE"];
                rows["SORT"] = (CConvert.FormateRate(Convert.ToString(Convert.ToDecimal(row["PRICE"]) / Convert.ToDecimal(amount) * 100)) + "%").ToString();
                rows["QUANTITY"] = row["QUANTITY"];
                dt.Rows.Add(rows);
            }

            return dt;

        }

        ///<summary>
        ///诊断评分栏
        ///</summary>
        public static DataTable GetScoreInfo(string StandardRate, string Lianx, string Ping, string HumanEffect, string Asp, string JointSalesRate, string vip, string lossRate, string Missing, string SalesRatio, string DiscountRate, string Compared, string Fraction)
        {
            DataTable dt = new DataTable();
            decimal salesratinonumber = 0;
            if (Convert.ToDecimal(StandardRate) < Convert.ToDecimal(110) && Convert.ToDecimal(StandardRate) > Convert.ToDecimal(100))//达标率
            {
                salesratinonumber += 25;
            }
            else if (Convert.ToDecimal(StandardRate) > 110)
            {
                salesratinonumber += 20;
            }
            else if (Convert.ToDecimal(StandardRate) < Convert.ToDecimal(100) && Convert.ToDecimal(StandardRate) > Convert.ToDecimal(90))
            {
                salesratinonumber += 18;
            }
            else if (Convert.ToDecimal(StandardRate) < Convert.ToDecimal(90) && Convert.ToDecimal(StandardRate) > Convert.ToDecimal(80))
            {
                salesratinonumber += 12;
            }
            else if (Convert.ToDecimal(StandardRate) < Convert.ToDecimal(80) && Convert.ToDecimal(StandardRate) > Convert.ToDecimal(70))
            {
                salesratinonumber += 8;
            }
            else if (Convert.ToDecimal(StandardRate) < Convert.ToDecimal(70))
            {
                salesratinonumber += 5;
            }
            //同期业绩增长率
            if (Compared != "")
            {
                if (Convert.ToDecimal(Compared) > Convert.ToDecimal(10) && Convert.ToDecimal(Compared) < Convert.ToDecimal(15))
                {
                    salesratinonumber += 10;
                }
                else if (Convert.ToDecimal(Compared) > Convert.ToDecimal(15))
                {
                    salesratinonumber += 15;
                }
                else if (Convert.ToDecimal(Compared) > Convert.ToDecimal(0) && Convert.ToDecimal(Compared) < Convert.ToDecimal(10))
                {
                    salesratinonumber += 7;
                }
                else if (Convert.ToDecimal(Compared) < Convert.ToDecimal(0))
                {
                    salesratinonumber += 3;
                }
            }
            //业绩指标分数设定
            if (Convert.ToString(Fraction) != "")
            {
                salesratinonumber += Convert.ToDecimal(Fraction);
            }

            //定金商品分数设定
            if (Convert.ToDecimal(-5) < (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP && (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP < Convert.ToDecimal(5))
            {
                salesratinonumber += 15;
            }
            else if (Convert.ToDecimal(5) < (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP && (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP < Convert.ToDecimal(10))
            {
                salesratinonumber += 10;
            }
            else if (Convert.ToDecimal(-10) < (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP && (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP < Convert.ToDecimal(-5))
            {
                salesratinonumber += 10;
            }
            else if (Convert.ToDecimal(10) < (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP && (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP < Convert.ToDecimal(20))
            {
                salesratinonumber += 5;
            }
            else if (Convert.ToDecimal(-20) < (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP && (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP < Convert.ToDecimal(-10))
            {
                salesratinonumber += 5;
            }
            else if ((Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP < Convert.ToDecimal(-20) || (Convert.ToDecimal(Asp) - Common.Parameter.ASP) / Common.Parameter.ASP > Convert.ToDecimal(20))
            {
                salesratinonumber += 0;
            }
            //配比商品销售份数
            if (Convert.ToDecimal(-5) < (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO && (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO < Convert.ToDecimal(5))
            {
                salesratinonumber += 10;
            }
            else if (Convert.ToDecimal(5) < (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO && (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO < Convert.ToDecimal(10))
            {
                salesratinonumber += 6;
            }
            else if (Convert.ToDecimal(-10) < (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO && (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO < Convert.ToDecimal(-5))
            {
                salesratinonumber += 6;
            }
            else if (Convert.ToDecimal(10) < (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO && (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO < Convert.ToDecimal(20))
            {
                salesratinonumber += 3;
            }
            else if (Convert.ToDecimal(-20) < (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO && (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO < Convert.ToDecimal(-10))
            {
                salesratinonumber += 3;
            }
            else if ((Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO > Convert.ToDecimal(20) || (Convert.ToDecimal(JointSalesRate) - Common.Parameter.RATIO) / Common.Parameter.RATIO < Convert.ToDecimal(-20))
            {
                salesratinonumber += 0;
            }
            //VIP分数设定
            if (Convert.ToDecimal(45) < Convert.ToDecimal(vip) && Convert.ToDecimal(vip) < Convert.ToDecimal(55))
            {
                salesratinonumber += 10;
            }
            else if (Convert.ToDecimal(35) < Convert.ToDecimal(vip) && Convert.ToDecimal(vip) < Convert.ToDecimal(45) || Convert.ToDecimal(0.55) < Convert.ToDecimal(vip) && Convert.ToDecimal(vip) < Convert.ToDecimal(65))
            {
                salesratinonumber += 6;
            }
            else if (Convert.ToDecimal(25) < Convert.ToDecimal(vip) && Convert.ToDecimal(vip) < Convert.ToDecimal(35) || Convert.ToDecimal(65) < Convert.ToDecimal(vip) && Convert.ToDecimal(vip) < Convert.ToDecimal(75))
            {
                salesratinonumber += 3;
            }
            else if (Convert.ToDecimal(25) > Convert.ToDecimal(vip) || Convert.ToDecimal(vip) > 75)
            {
                salesratinonumber += 1;
            }
            //质量分数的设定
            if (Convert.ToString(lossRate) != " " && Convert.ToString(lossRate) != "")
            {
                if (Convert.ToDecimal(lossRate) < Convert.ToDecimal(1))
                {
                    salesratinonumber += 10;
                }
                else if (Convert.ToDecimal(1) < Convert.ToDecimal(lossRate) && Convert.ToDecimal(lossRate) < Convert.ToDecimal(3))
                {
                    salesratinonumber += 6;
                }
                else if (Convert.ToDecimal(0.03) < Convert.ToDecimal(lossRate) && Convert.ToDecimal(lossRate) < Convert.ToDecimal(5))
                {
                    salesratinonumber += 3;
                }
                else if (Convert.ToDecimal(lossRate) > Convert.ToDecimal(5))
                {
                    salesratinonumber += 0;
                }
            }
            //防盗分数设定
            if (Missing != " " && Missing != "")
            {
                if (Convert.ToDecimal(Missing) > Convert.ToDecimal(0.6))
                {
                    salesratinonumber += 0;
                }
                else if (Convert.ToDecimal(0.3) < Convert.ToDecimal(Missing) && Convert.ToDecimal(Missing) < Convert.ToDecimal(0.6))
                {
                    salesratinonumber += 2;
                }
                else if (Convert.ToDecimal(0) < Convert.ToDecimal(Missing) && Convert.ToDecimal(Missing) < Convert.ToDecimal(0.3))
                {
                    salesratinonumber += 5;
                }
            }
            //进销平衡比分数设定
            if (SalesRatio != " " && SalesRatio != "")
            {
                if (Convert.ToDecimal(-20) < Convert.ToDecimal(SalesRatio) && Convert.ToDecimal(SalesRatio) < Convert.ToDecimal(20))
                {
                    salesratinonumber += 5;
                }
                else if (Convert.ToDecimal(20) < Convert.ToDecimal(SalesRatio) && Convert.ToDecimal(SalesRatio) < Convert.ToDecimal(30))
                {
                    salesratinonumber += 3;
                }
                else if (Convert.ToDecimal(-30) < Convert.ToDecimal(SalesRatio) & Convert.ToDecimal(SalesRatio) < Convert.ToDecimal(-20))
                {
                    salesratinonumber += 3;
                }
                else if (Convert.ToDecimal(SalesRatio) < Convert.ToDecimal(-30) || Convert.ToDecimal(SalesRatio) > Convert.ToDecimal(30))
                {
                    salesratinonumber += 1;
                }
            }
            //折扣分数设定
            if (Convert.ToDecimal(6) < Convert.ToDecimal(DiscountRate) && Convert.ToDecimal(DiscountRate) < Convert.ToDecimal(10))
            {
                salesratinonumber += 5;
            }
            else if (Convert.ToDecimal(1) < Convert.ToDecimal(DiscountRate) && Convert.ToDecimal(DiscountRate) < Convert.ToDecimal(6) || Convert.ToDecimal(10) < Convert.ToDecimal(DiscountRate) && Convert.ToDecimal(DiscountRate) < Convert.ToDecimal(15))
            {
                salesratinonumber += 3;
            }
            else if (Convert.ToDecimal(DiscountRate) < Convert.ToDecimal(1) || Convert.ToDecimal(DiscountRate) > Convert.ToDecimal(15))
            {
                salesratinonumber += 1;
            }
            dt.Columns.Add("SALESRATINONUMBER", Type.GetType("System.String"));
            dt.Columns.Add("SALESRATINOINFO", Type.GetType("System.String"));
            string SALESRATINO = "";
            DataRow row = dt.NewRow();
            row["SALESRATINONUMBER"] = salesratinonumber;
            if (Convert.ToDecimal(salesratinonumber) > 80)
            {
                SALESRATINO = "门店的经营状况综合判断为优秀。";
            }
            else if (Convert.ToDecimal(salesratinonumber) >= 70 && Convert.ToDecimal(salesratinonumber) < 80)
            {
                SALESRATINO = "门店的经营状况综合判断为良好。";
            }
            else if (Convert.ToDecimal(salesratinonumber) >= 60 && Convert.ToDecimal(salesratinonumber) < 70)
            {
                SALESRATINO = "门店的经营状况综合判断为合格。";
            }
            else if (Convert.ToDecimal(salesratinonumber) < 60)
            {
                SALESRATINO = "门店的经营状况判断为不合格。";
            }
            row["SALESRATINOINFO"] = SALESRATINO;
            dt.Rows.Add(row);
            return dt;

        }

        ///<summary>
        /// 综合判断栏
        ///</summary>
        public static DataTable GetOpinionInfo(string StandardRate, string Asp, string Atv, string JointSalesRate, string Vip, string LossRate, string Missing, string SalesRatio, string DiscountRate, string Compared, string Fraction)
        {
            string standarRate = "";
            string compared = "";
            string fraction = "";
            string jointSalesRate = "";
            string price = "";
            string vip = "";
            string lossRate = "";
            string salesRatio = "";
            string discountRate = "";
            string miss = "";
            if (StandardRate != "")
            {
                if (Convert.ToDecimal(StandardRate) < Convert.ToDecimal(80))
                {
                    standarRate = "达标率不理想。请注意分析员工的销售能力，货品销售配比，销售环境以及达标率设置是否合理。";
                }
            }
            if (Compared != "")
            {
                if (Convert.ToDecimal(Compared) < Convert.ToDecimal(10))
                {
                    compared = "和去年相比同期业绩增长不理想。请注意是否有货品变化、员工变化、销售环境变化。";
                }
            }
            if (Fraction != "")
            {
                if (Convert.ToDecimal(Fraction) == Convert.ToDecimal(3) || Convert.ToDecimal(Fraction) == Convert.ToDecimal(2) || Convert.ToDecimal(Fraction) == Convert.ToDecimal(1))
                {
                    fraction = "业绩指标值综合判断不理想。请注意分析员工的销售能力，货品的市场认可度以及业绩指标值设定的合理性。";
                }
            }
            if (JointSalesRate != "")
            {
                if (Convert.ToDecimal(JointSalesRate) > Convert.ToDecimal(55) || Convert.ToDecimal(JointSalesRate) < Convert.ToDecimal(45))
                {
                    jointSalesRate = "主、副货品的销售配比不理想，请注意货品组合的合理性，员工的连带销售能力，请研究顾客的消费心理。请分析是否需要调整主商品配比的设定值。";
                }
            }
            if (Asp != "" && Atv != "")
            {
                if (Convert.ToDecimal(Asp) < Convert.ToDecimal(5) && Convert.ToDecimal(Asp) > Convert.ToDecimal(-5) || Convert.ToDecimal(Atv) < Convert.ToDecimal(5))
                {
                    price = "定价可能不合适，请考虑调整定价。";
                }
            }
            if (Vip != "")
            {
                if (Convert.ToDecimal(Vip) > Convert.ToDecimal(55) || Convert.ToDecimal(Vip) < Convert.ToDecimal(45))
                {
                    vip = "VIP销售占比不合适，请注意检查服务质量和商品的市场认可度,或注意开发新客户。";
                }
            }
            if (LossRate != " " && LossRate != "")
            {
                if (Convert.ToDecimal(LossRate) > Convert.ToDecimal(3))
                {
                    lossRate = "请通知采购部门确认货品质量，同时确认门店处理质量问题的能力。";
                }
            }
            if (SalesRatio != "" && SalesRatio != "")
            {
                if (Convert.ToDecimal(SalesRatio) > Convert.ToDecimal(20) || Convert.ToDecimal(SalesRatio) < Convert.ToDecimal(-20))
                {
                    salesRatio = "进销比失衡，请注意库存调节，并查明原因。";
                }
            }
            if (DiscountRate != "" && DiscountRate != "")
            {
                if (Convert.ToDecimal(DiscountRate) > Convert.ToDecimal(10) && Convert.ToDecimal(DiscountRate) < Convert.ToDecimal(6))
                {
                    discountRate = "折扣率超出设定范围，请注意是否需要让利或严加管控。";
                }
            }
            if (Missing != " " && Missing != "")
            {
                if (Convert.ToDecimal(Missing) > Convert.ToDecimal(6))
                {
                    miss = "丢失率过高，请注意加强防盗措施，或考虑排班。";
                }
            }

            DataTable da = new DataTable();
            da.Columns.Add("Info", Type.GetType("System.String"));
            if (standarRate != "")
            {
                DataRow row1 = da.NewRow();
                row1["Info"] = salesRatio;
                da.Rows.Add(row1);
            }
            if (compared != "")
            {
                DataRow row2 = da.NewRow();
                row2["Info"] = compared;
                da.Rows.Add(row2);
            }
            if (fraction != "")
            {
                DataRow row3 = da.NewRow();
                row3["Info"] = fraction;
                da.Rows.Add(row3);
            }
            if (jointSalesRate != "")
            {
                DataRow row4 = da.NewRow();
                row4["Info"] = jointSalesRate;
                da.Rows.Add(row4);
            }
            if (price != "")
            {
                DataRow row5 = da.NewRow();
                row5["Info"] = price;
                da.Rows.Add(row5);
            }
            if (vip != "")
            {
                DataRow row6 = da.NewRow();
                row6["Info"] = vip;
                da.Rows.Add(row6);
            }
            if (lossRate != "")
            {
                DataRow row7 = da.NewRow();
                row7["Info"] = lossRate;
                da.Rows.Add(row7);
            }
            if (salesRatio != "")
            {
                DataRow row8 = da.NewRow();
                row8["Info"] = salesRatio;
                da.Rows.Add(row8);
            }
            if (discountRate != "")
            {
                DataRow row9 = da.NewRow();
                row9["Info"] = discountRate;
                da.Rows.Add(row9);
            }
            if (miss != "")
            {
                DataRow row10 = da.NewRow();
                row10["Info"] = miss;
                da.Rows.Add(row10);
            }
            return da;
        }


        ///<summary>
        ///获得所有部门
        ///</summary>
        public static DataTable GetDepartmentInfo()
        {
            DataSet ds = bCommon.GetMasterList("BASE_DEPARTMENT", "", " DEPARTMENT_TYPE<>99");
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }

        ///<summary>
        ///参数设定页面的显示
        ///</summary>
        public static DataTable ParameterLoad()
        {
            DataSet ds = bParameter.GetAllParameterInfo();
            DataTable dt1 = ds.Tables[0];
            string PERFORMANCE = "";
            string ASP = "";
            string ATV = "";
            string PING = "";
            string HUMANEFFECT = "";
            string COMPARED1 = "";
            string COMPARED2 = "";
            string VIP1 = "";
            string VIP2 = "";
            string LOSSRATEL1 = "";
            string LOSSRATEL2 = "";
            string LORD_PRODUCT_RATIO1 = "";
            string LORD_PRODUCT_RATIO2 = "";
            string SALESRATIO1 = "";
            string SALESRATIO2 = "";
            string DISCOUNT1 = "";
            string DISCOUNT2 = "";
            string MISS = "";
            string INDICATOR = "";
            string ONEINDICATOR = "";
            string TWOINDICATOR = "";
            string THREEINDICATOR = "";
            string FOURINDICATOR = "";
            string FIVEINDICATOR = "";
            string SIXINDICATOR = "";
            string SEVENINDICATOR = "";
            string EIGHTINDICATOR = "";
            string NINEINDICATOR = "";
            string TENINDICATOR = "";
            string ELEVENINDICATOR = "";
            string TWELVEINDICATOR = "";
            #region
            if (dt1 != null)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    if (row["CODE"].ToString() == "PERFORMANCE")
                    {
                        PERFORMANCE = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "ASP")
                    {
                        ASP = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "ATV")
                    {
                        ATV = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "PING")
                    {
                        PING = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "HUMANEFFECT")
                    {
                        HUMANEFFECT = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "COMPARED")
                    {
                        COMPARED1 = row["NUMBER1"].ToString();
                        COMPARED2 = row["NUMBER2"].ToString();
                    }
                    else if (row["CODE"].ToString() == "VIP")
                    {
                        VIP1 = row["NUMBER1"].ToString();
                        VIP2 = row["NUMBER2"].ToString();
                    }
                    else if (row["CODE"].ToString() == "LOSSRATEL")
                    {
                        LOSSRATEL1 = row["NUMBER1"].ToString();
                        LOSSRATEL2 = row["NUMBER2"].ToString();
                    }
                    else if (row["CODE"].ToString() == "LORD_PRODUCT_RATIO")
                    {
                        LORD_PRODUCT_RATIO1 = row["NUMBER1"].ToString();
                        LORD_PRODUCT_RATIO2 = row["NUMBER2"].ToString();
                    }
                    else if (row["CODE"].ToString() == "SALESRATIO")
                    {
                        SALESRATIO1 = row["NUMBER1"].ToString();
                        SALESRATIO2 = row["NUMBER2"].ToString();
                    }
                    else if (row["CODE"].ToString() == "DISCOUNT")
                    {
                        DISCOUNT1 = row["NUMBER1"].ToString();
                        DISCOUNT2 = row["NUMBER2"].ToString();
                    }
                    else if (row["CODE"].ToString() == "MISS")
                    {
                        MISS = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "INDICATOR")
                    {
                        INDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "ONEINDICATOR")
                    {
                        ONEINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "TWOINDICATOR")
                    {
                        TWOINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "THREEINDICATOR")
                    {
                        THREEINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "FOURINDICATOR")
                    {
                        FOURINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "FIVEINDICATOR")
                    {
                        FIVEINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "SIXINDICATOR")
                    {
                        SIXINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "SEVENINDICATOR")
                    {
                        SEVENINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "EIGHTINDICATOR")
                    {
                        EIGHTINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "NINEINDICATOR")
                    {
                        NINEINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "TENINDICATOR")
                    {
                        TENINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "ELEVENINDICATOR")
                    {
                        ELEVENINDICATOR = row["NUMBER1"].ToString();
                    }
                    else if (row["CODE"].ToString() == "TWELVEINDICATOR")
                    {
                        TWELVEINDICATOR = row["NUMBER1"].ToString();
                    }
                }
            }
            #endregion
            DataTable dt = new DataTable();
            dt.Columns.Add("PERFORMANCE", Type.GetType("System.String"));
            dt.Columns.Add("ASP", Type.GetType("System.String"));
            dt.Columns.Add("ATV", Type.GetType("System.String"));
            dt.Columns.Add("PING", Type.GetType("System.String"));
            dt.Columns.Add("COMPARED1", Type.GetType("System.String"));
            dt.Columns.Add("COMPARED2", Type.GetType("System.String"));
            dt.Columns.Add("VIP1", Type.GetType("System.String"));
            dt.Columns.Add("VIP2", Type.GetType("System.String"));
            dt.Columns.Add("LOSSRATEL1", Type.GetType("System.String"));
            dt.Columns.Add("LOSSRATEL2", Type.GetType("System.String"));
            dt.Columns.Add("LORD_PRODUCT_RATIO1", Type.GetType("System.String"));
            dt.Columns.Add("LORD_PRODUCT_RATIO2", Type.GetType("System.String"));
            dt.Columns.Add("SALESRATIO1", Type.GetType("System.String"));
            dt.Columns.Add("SALESRATIO2", Type.GetType("System.String"));
            dt.Columns.Add("DISCOUNT1", Type.GetType("System.String"));
            dt.Columns.Add("DISCOUNT2", Type.GetType("System.String"));
            dt.Columns.Add("MISS", Type.GetType("System.String"));
            dt.Columns.Add("INDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("HUMANEFFECT", Type.GetType("System.String"));
            dt.Columns.Add("ONEINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("TWOINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("THREEINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("FOURINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("FIVEINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("SIXINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("SEVENINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("EIGHTINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("NINEINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("TENINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("ELEVENINDICATOR", Type.GetType("System.String"));
            dt.Columns.Add("TWELVEINDICATOR", Type.GetType("System.String"));
            DataRow rows = dt.NewRow();
            rows["PERFORMANCE"] = String.Format("{0:N}", Convert.ToDecimal(PERFORMANCE));
            rows["ASP"] = String.Format("{0:N}", Convert.ToDecimal(ASP));
            rows["ATV"] = String.Format("{0:N}", Convert.ToDecimal(ATV));
            rows["PING"] = String.Format("{0:N}", Convert.ToDecimal(PING));
            rows["HUMANEFFECT"] = String.Format("{0:N}", Convert.ToDecimal(HUMANEFFECT));
            rows["COMPARED1"] = String.Format("{0:N}", Convert.ToDecimal(COMPARED1));
            rows["COMPARED2"] = String.Format("{0:N}", Convert.ToDecimal(COMPARED2));
            rows["VIP1"] = String.Format("{0:N}", Convert.ToDecimal(VIP1));
            rows["VIP2"] = String.Format("{0:N}", Convert.ToDecimal(VIP2));
            rows["LOSSRATEL1"] = String.Format("{0:N}", Convert.ToDecimal(LOSSRATEL1));
            rows["LOSSRATEL2"] = String.Format("{0:N}", Convert.ToDecimal(LOSSRATEL2));
            rows["LORD_PRODUCT_RATIO1"] = String.Format("{0:N}", Convert.ToDecimal(LORD_PRODUCT_RATIO1));
            rows["LORD_PRODUCT_RATIO2"] = String.Format("{0:N}", Convert.ToDecimal(LORD_PRODUCT_RATIO2));
            rows["SALESRATIO1"] = String.Format("{0:N}", Convert.ToDecimal(SALESRATIO1));
            rows["SALESRATIO2"] = String.Format("{0:N}", Convert.ToDecimal(SALESRATIO2));
            rows["DISCOUNT1"] = String.Format("{0:N}", Convert.ToDecimal(DISCOUNT1));
            rows["DISCOUNT2"] = String.Format("{0:N}", Convert.ToDecimal(DISCOUNT2));
            rows["MISS"] = String.Format("{0:N}", Convert.ToDecimal(MISS));
            rows["INDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(INDICATOR));
            rows["ONEINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(ONEINDICATOR));
            rows["TWOINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(TWOINDICATOR));
            rows["THREEINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(THREEINDICATOR));
            rows["FOURINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(FOURINDICATOR));
            rows["FIVEINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(FIVEINDICATOR));
            rows["SIXINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(SIXINDICATOR));
            rows["SEVENINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(SEVENINDICATOR));
            rows["EIGHTINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(EIGHTINDICATOR));
            rows["NINEINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(NINEINDICATOR));
            rows["TENINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(TENINDICATOR));
            rows["ELEVENINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(ELEVENINDICATOR));
            rows["TWELVEINDICATOR"] = String.Format("{0:N}", Convert.ToDecimal(TWELVEINDICATOR));
            dt.Rows.Add(rows);
            return dt;

        }

        ///<summary>
        ///参数录入
        ///</summary>
        public static DataTable ParameterInt(string INDICATOR, string ASP, string ATV, string PING, string HUMANEFFECT, string Miss, string PERFORMANCE, string VIP1, string VIP2, string LORDPRODUCTRATIO1, string LORDPRODUCTRATIO2, string SALESRATIO1, string SALESRATIO2, string DISCOUNT1, string DISCOUNT2, string LOSSRATEL1,
            string LOSSRATEL2, string COMPART1, string COMPART2, string ONEINDICATOR, string TWOINDICATOR, string THREEINDICATOR, string FOURINDICATOR, string FIVEINDICATOR,
            string SIXINDICATOR, string SEVENINDICATOR, string EIGHTINDICATOR, string NINEINDICATOR, string TENINDICATOR, string ELEVENINDICATOR, string TWELVEINDICATOR)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("message", Type.GetType("System.String"));
            string CheckMessage = "";
            string INDICATOR_ = "";
            string ASP_ = "";
            string ATV_ = "";
            string PING_ = "";
            string HUMANEFFECT_ = "";
            string Miss_ = "";
            string PERFORMANCE_ = "";
            string VIP1_ = "";
            string VIP2_ = "";
            string LORDPRODUCTRATIO1_ = "";
            string LORDPRODUCTRATIO2_ = "";
            string SALESRATIO1_ = "";
            string SALESRATIO2_ = "";
            string DISCOUNT1_ = "";
            string DISCOUNT2_ = "";
            string LOSSRATEL1_ = "";
            string LOSSRATEL2_ = "";
            string COMPART1_ = "";
            string COMPART2_ = "";
            string ONEINDICATOR_ = "";
            string TWOINDICATOR_ = "";
            string THREEINDICATOR_ = "";
            string FOURINDICATOR_ = "";
            string FIVEINDICATOR_ = "";
            string SIXINDICATOR_ = "";
            string SEVENINDICATOR_ = "";
            string EIGHTINDICATOR_ = "";
            string NINEINDICATOR_ = "";
            string TENINDICATOR_ = "";
            string ELEVENINDICATOR_ = "";
            string TWELVEINDICATOR_ = "";
            int i = 0;
            try
            {
                INDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(INDICATOR));
            }
            catch { i++; }
            try
            {
                ASP_ = String.Format("{0:F0}", Convert.ToDecimal(ASP));
            }
            catch { i++; }
            try
            {
                ATV_ = String.Format("{0:F0}", Convert.ToDecimal(ATV));
            }
            catch { i++; }
            try
            {
                PING_ = String.Format("{0:F0}", Convert.ToDecimal(PING));
            }
            catch { i++; }
            try
            {
                HUMANEFFECT_ = String.Format("{0:F0}", Convert.ToDecimal(HUMANEFFECT));
            }
            catch { i++; }
            try
            {
                Miss_ = String.Format("{0:F0}", Convert.ToDecimal(Miss));
            }
            catch { i++; }
            try
            {
                PERFORMANCE_ = String.Format("{0:F0}", Convert.ToDecimal(PERFORMANCE));
            }
            catch { i++; }
            try
            {
                VIP1_ = String.Format("{0:F0}", Convert.ToDecimal(VIP1));
            }
            catch { i++; }
            try
            {
                VIP2_ = String.Format("{0:F0}", Convert.ToDecimal(VIP2));
            }
            catch { i++; }
            try
            {
                LORDPRODUCTRATIO1_ = String.Format("{0:F0}", Convert.ToDecimal(LORDPRODUCTRATIO1));
            }
            catch { i++; }
            try
            {
                LORDPRODUCTRATIO2_ = String.Format("{0:F0}", Convert.ToDecimal(LORDPRODUCTRATIO2));
            }
            catch { i++; }
            try
            {
                SALESRATIO1_ = String.Format("{0:F0}", Convert.ToDecimal(SALESRATIO1));
            }
            catch { i++; }
            try
            {
                SALESRATIO2_ = String.Format("{0:F0}", Convert.ToDecimal(SALESRATIO2));
            }
            catch { i++; }
            try
            {
                DISCOUNT1_ = String.Format("{0:F0}", Convert.ToDecimal(DISCOUNT1));
            }
            catch { i++; }
            try
            {
                DISCOUNT2_ = String.Format("{0:F0}", Convert.ToDecimal(DISCOUNT2));
            }
            catch { i++; }
            try
            {
                LOSSRATEL1_ = String.Format("{0:F0}", Convert.ToDecimal(LOSSRATEL1));
            }
            catch { i++; }
            try
            {
                LOSSRATEL2_ = String.Format("{0:F0}", Convert.ToDecimal(LOSSRATEL2));
            }
            catch { i++; }
            try
            {
                COMPART1_ = String.Format("{0:F0}", Convert.ToDecimal(COMPART1));
            }
            catch { i++; }
            try
            {
                COMPART2_ = String.Format("{0:F0}", Convert.ToDecimal(COMPART2));
            }
            catch { i++; }
            try
            {
                ONEINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(ONEINDICATOR));
            }
            catch { i++; }
            try
            {
                TWOINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(TWOINDICATOR));
            }
            catch { i++; }
            try
            {
                THREEINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(THREEINDICATOR));
            }
            catch { i++; }
            try
            {
                FOURINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(FOURINDICATOR));
            }
            catch { i++; }
            try
            {
                FIVEINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(FIVEINDICATOR));
            }
            catch { i++; }
            try
            {
                SIXINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(SIXINDICATOR));
            }
            catch { i++; }
            try
            {
                SEVENINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(SEVENINDICATOR));
            }
            catch { i++; }
            try
            {
                EIGHTINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(EIGHTINDICATOR));
            }
            catch { i++; }
            try
            {
                NINEINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(NINEINDICATOR));
            }
            catch { i++; }
            try
            {
                TENINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(TENINDICATOR));
            }
            catch { i++; }
            try
            {
                ELEVENINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(ELEVENINDICATOR));
            }
            catch { i++; }
            try
            {
                TWELVEINDICATOR_ = String.Format("{0:F0}", Convert.ToDecimal(TWELVEINDICATOR));
            }
            catch { i++; }
            if (i > 0)
            {
                CheckMessage = "输入参数只能为数字";
            }
            if (CheckMessage.Length > 0)
            {
                DataRow rows = dt.NewRow();
                rows["message"] = CheckMessage;
                dt.Rows.Add(rows);
            }
            else
            {
                List<SarParameterTable> sartablelist = new List<SarParameterTable>();
                if (PERFORMANCE_ != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "PERFORMANCE";
                    sartable.NUMBER1 = PERFORMANCE_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (INDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "INDICATOR";
                    sartable.NUMBER1 = INDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (ATV != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "ATV";
                    sartable.NUMBER1 = ATV_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (ASP != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "ASP";
                    sartable.NUMBER1 = ASP_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (PING != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "PING";
                    sartable.NUMBER1 = PING_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (HUMANEFFECT != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "HUMANEFFECT";
                    sartable.NUMBER1 = HUMANEFFECT_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (LOSSRATEL1 != "" || LOSSRATEL2 != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "LOSSRATEL";
                    sartable.NUMBER1 = LOSSRATEL1_;
                    sartable.NUMBER2 = LOSSRATEL2_;
                    sartablelist.Add(sartable);
                }
                if (VIP1 != "" || VIP2 != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "VIP";
                    sartable.NUMBER1 = VIP1_;
                    sartable.NUMBER2 = VIP2_;
                    sartablelist.Add(sartable);
                }
                if (LORDPRODUCTRATIO1 != "" || LORDPRODUCTRATIO2 != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "LORD_PRODUCT_RATIO";
                    sartable.NUMBER1 = LORDPRODUCTRATIO1_;
                    sartable.NUMBER2 = LORDPRODUCTRATIO2_;
                    sartablelist.Add(sartable);
                }
                if (SALESRATIO1 != "" || SALESRATIO2 != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "SALESRATIO";
                    sartable.NUMBER1 = SALESRATIO1_;
                    sartable.NUMBER2 = SALESRATIO2_;
                    sartablelist.Add(sartable);
                }
                if (Miss != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "MISS";
                    sartable.NUMBER1 = Miss_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (COMPART1 != "" || COMPART2 != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "COMPARED";
                    sartable.NUMBER1 = COMPART1_;
                    sartable.NUMBER2 = COMPART2_;
                    sartablelist.Add(sartable);
                }
                if (DISCOUNT1 != "" || DISCOUNT2 != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 0;
                    sartable.CODE = "DISCOUNT";
                    sartable.NUMBER1 = DISCOUNT1_;
                    sartable.NUMBER2 = DISCOUNT2_;
                    sartablelist.Add(sartable);
                }
                if (ONEINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "ONEINDICATOR";
                    sartable.NUMBER1 = ONEINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (TWOINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "TWOINDICATOR";
                    sartable.NUMBER1 = TWOINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (THREEINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "THREEINDICATOR";
                    sartable.NUMBER1 = THREEINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (FOURINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "FOURINDICATOR";
                    sartable.NUMBER1 = FOURINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (FIVEINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "FIVEINDICATOR";
                    sartable.NUMBER1 = FIVEINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (SIXINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "SIXINDICATOR";
                    sartable.NUMBER1 = SIXINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (SEVENINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "SEVENINDICATOR";
                    sartable.NUMBER1 = SEVENINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (EIGHTINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "EIGHTINDICATOR";
                    sartable.NUMBER1 = EIGHTINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (NINEINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "NINEINDICATOR";
                    sartable.NUMBER1 = NINEINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (TENINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "TENINDICATOR";
                    sartable.NUMBER1 = TENINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (ELEVENINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "ELEVENINDICATOR";
                    sartable.NUMBER1 = ELEVENINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                if (TWELVEINDICATOR != "")
                {
                    SarParameterTable sartable = new SarParameterTable();
                    sartable.STATUS_FLAG = 1;
                    sartable.CODE = "TWELVEINDICATOR";
                    sartable.NUMBER1 = TWELVEINDICATOR_;
                    sartable.NUMBER2 = "";
                    sartablelist.Add(sartable);
                }
                string message = "";
                if (bParameter.Update(sartablelist) > 0)
                {
                    message = "修改成功！";
                }
                else
                {
                    message = "修改失败！";
                }

                DataRow rows = dt.NewRow();
                rows["message"] = message;
                dt.Rows.Add(rows);
            }
            return dt;
        }

        //循环取出每一个月对应的业绩指标
        private static string GetOneMonthAmount(string month)
        {
            string amount = "";
            DataSet ds = bSales.GetMonthInfo();
            DataTable da = ds.Tables[0];
            Hashtable hs = new Hashtable();
            foreach (DataRow row in da.Rows)
            {
                hs.Add(row["NUMBER2"], row["NUMBER1"]);
            }
            foreach (DictionaryEntry db in hs)
            {
                if (db.Key.ToString() == month)
                {
                    amount = db.Value.ToString();
                }
            }
            return amount;
        }

        public static DataTable GetProductGroupData()
        {
            DataSet ds = bSales.GetProductInfo();
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 转换
        /// </summary>
        public static string CreateJsonParameters(DataTable dt, string type)
        {
            string fromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            string toDate = DateTime.Now.ToString("yyyy-MM-dd");
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{root:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("],type:\"" + type + "\"");
                if ("ANALYSE_LOAD".Equals(type))
                {
                    JsonString.Append(" ,frmDate:\"" + fromDate + "\"");
                    JsonString.Append(" ,toDate:\"" + toDate + "\"");
                }
                JsonString.Append("}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

    }//end class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Model;
using POS.Common;
using System.IO;
using FastReport;
using UserCache;
using POS.Bll;
using System.Windows.Forms;

namespace POS
{
    public class PrintInvoice
    {

        ///// <summary>
        ///// 销售小票打印
        ///// </summary>
        //public static void print(string slipNumber)
        //{
        //    DataSet ds = new BSalesOrder().GetPrintList(" SLIP_NUMBER = '" + slipNumber + "'");
        //    try
        //    {
        //        FastReport.Report report = new FastReport.Report();
        //        int count = ds.Tables[0].Rows.Count;
        //        if (File.Exists(@"Reports\SalesPrint.frx"))
        //        {
        //            report.Load(@"Reports\SalesPrint.frx");
        //            ((ReportPage)report.FindObject("Page1")).PaperHeight = (float)(80 + 7.5 * count);
        //            report.SetParameterValue("Bank_Amount", ds.Tables[0].Rows[0]["BANK_AMOUNT"]);
        //            report.SetParameterValue("Cash_Amount", ds.Tables[0].Rows[0]["CASH_AMOUNT"]);
        //            report.SetParameterValue("Change", ds.Tables[0].Rows[0]["CHANGE"]);
        //            report.SetParameterValue("CreateUserName", ds.Tables[0].Rows[0]["CREATE_USER_NAME"]);
        //            report.SetParameterValue("CreateDateTime", ds.Tables[0].Rows[0]["CREATE_DATE_TIME"]);
        //            report.SetParameterValue("Title", Cache.PRINT_HT["TITLE"]);
        //            report.SetParameterValue("Tel", Cache.PRINT_HT["TEL"]);
        //            report.SetParameterValue("Address", Cache.PRINT_HT["ADDRESS"]);
        //            report.SetParameterValue("WWW", Cache.PRINT_HT["WWW"]);
        //            report.SetParameterValue("Footer", Cache.PRINT_HT["MEMO"]);
        //            //剩余可用积分的获得
        //            try
        //            {
        //                BaseVipCustomerTable customerTable = new BVipCustomer().GetModel(ds.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString());
        //                report.SetParameterValue("TotalPoints", customerTable.POINTS - customerTable.USED_POINTS);
        //            }
        //            catch { }
        //            report.RegisterData(ds);
        //            report.Prepare();
        //            report.PrintSettings.ShowDialog = false;
        //            //打印小票
        //            report.Print();
        //        }
        //        else
        //        {
        //            MessageBox.Show("无法找到报表文件SalesPrint.frx!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public static void print(string slipNumber)
        {
            BCommon bcommon = new BCommon();
            string PointName = "0";
            if (Printer.GetPrinter() == null || Printer.GetPrinter().Length == 0)
            {
                MessageBox.Show("没有找到合适的打印机", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                DataSet ds = new BSalesOrder().GetPrintList(" SLIP_NUMBER = '" + slipNumber + "'");
                if (ds.Tables[0].Rows[0]["USED_POINTS"].ToString() != "0")
                {
                    DataTable NameDt = bcommon.GetNames("POINT_TYPE").Tables[0];
                    foreach (DataRow row in NameDt.Rows)
                    {
                        string code = row["CODE"].ToString();
                        string[] codeName = code.Split('-');
                        string oneName = codeName[0].ToString();
                        if (Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[0]["USED_POINTS"].ToString())).ToString() == oneName)
                        {
                            PointName = row["NAME"].ToString();
                        }

                    }
                }
                //剩余可用积分的获得
                decimal totalPoints = 0;
                try
                {
                    BaseVipCustomerTable customerTable = new BVipCustomer().GetModel(ds.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString());
                    totalPoints = customerTable.POINTS - customerTable.USED_POINTS;
                }
                catch { }


                for (int a = 1; a <= Convert.ToDecimal(Cache.PRINT_HT["SHARE"]); a++)
                {
                    LPTControl lpt = new LPTControl();
                    lpt.Open(Cache.PRN_PORT);
                    lpt.WriteLine(Convert.ToString(Cache.PRINT_HT["TITLE"]), LPTControl.HorPos.Center);
                    lpt.NewRow();
                    lpt.WriteLine("收据号: " + Convert.ToString(ds.Tables[0].Rows[0]["SLIP_NUMBER"]));
                    string StrTitle = "货号";
                    StrTitle += "数量".PadLeft(4, ' ');
                    StrTitle += "单价".PadLeft(6, ' ');
                    StrTitle += "折扣".PadLeft(4, ' ');
                    StrTitle += "小计".PadLeft(6, ' ');
                    lpt.WriteLine(StrTitle);
                    lpt.PrintLine();
                    decimal totoalQuantity = 0;
                    decimal totalAmount = 0;
                    int usedPoints = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lpt.WriteLine(ds.Tables[0].Rows[i]["PRODUCT_CODE"].ToString() + "  " + ds.Tables[0].Rows[i]["PRODUCT_NAME"].ToString());
                        lpt.WriteLine(
                                        Math.Floor(Convert.ToDecimal(ds.Tables[0].Rows[i]["QUANTITY"])).ToString().PadLeft(10, ' ') +
                                        Math.Floor(Convert.ToDecimal(ds.Tables[0].Rows[i]["ORI_PRICE"])).ToString().PadLeft(8, ' ') +
                                        Math.Floor(Convert.ToDecimal(ds.Tables[0].Rows[i]["DISCOUNT_RATE"])).ToString().PadLeft(6, ' ') +
                                        ds.Tables[0].Rows[i]["AMOUNT"].ToString().PadLeft(8, ' ')
                                        );
                        totoalQuantity += Math.Floor(Convert.ToDecimal(ds.Tables[0].Rows[i]["QUANTITY"]));
                        totalAmount += Convert.ToDecimal(ds.Tables[0].Rows[i]["AMOUNT"]);
                        usedPoints += Convert.ToInt32(ds.Tables[0].Rows[i]["USED_POINTS"]);
                    }
                    lpt.PrintLine();
                    lpt.WriteLine("总数量:   " + Convert.ToString(totoalQuantity).PadLeft(22, ' '));
                    lpt.WriteLine("总金额:   " + Convert.ToString(totalAmount).PadLeft(22, ' '));
                    lpt.WriteLine("刷卡:     " + Convert.ToString(ds.Tables[0].Rows[0]["BANK_AMOUNT"]).PadLeft(22, ' '));
                    lpt.WriteLine("现金:     " + Convert.ToString(ds.Tables[0].Rows[0]["CASH_AMOUNT"]).PadLeft(22, ' '));
                    if (PointName == "0")
                    {
                        lpt.WriteLine("抵扣积分: " + Convert.ToString(PointName).PadLeft(22, ' '));
                    }
                    else
                    {
                        lpt.WriteLine("抵扣积分: " + Convert.ToString(PointName).PadLeft(20, ' '));
                    }
                    lpt.WriteLine("找零:     " + Convert.ToString(ds.Tables[0].Rows[0]["CHANGE"]).PadLeft(22, ' '));
                    lpt.WriteLine("可用积分: " + Convert.ToString(totalPoints).PadLeft(22, ' '));
                    lpt.WriteLine("经手人:   " + Convert.ToString(ds.Tables[0].Rows[0]["CREATE_USER_NAME"]));
                    lpt.WriteLine("销售时间: " + Convert.ToDateTime(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"]).ToString("yyyy-MM-dd HH:mm:ss"));
                    lpt.WriteLine("联系地址: " + Convert.ToString(Cache.PRINT_HT["ADDRESS"]));
                    lpt.WriteLine("联系电话: " + Convert.ToString(Cache.PRINT_HT["TEL"]));
                    lpt.WriteLine("网址: " + Convert.ToString(Cache.PRINT_HT["WWW"]));
                    lpt.WriteLine(Convert.ToString(Cache.PRINT_HT["MEMO"]), LPTControl.HorPos.Center);
                    lpt.NewRow(5);
                    lpt.Close();
                    System.Threading.Thread.Sleep(2000);
                }



            }
            catch (Exception Ex)
            {
                MessageBox.Show("打印出错" + Ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }//end class
}

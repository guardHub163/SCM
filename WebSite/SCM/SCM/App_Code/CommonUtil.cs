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
using CrystalDecisions.Enterprise;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using System.Reflection;

namespace SCM.Web
{
    /// <summary>
    ///DownLoad 的摘要说明
    /// </summary>
    public class CommonUtil
    {
        public CommonUtil()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static DiskFileDestinationOptions FileOPS = new DiskFileDestinationOptions();
        private static ExportOptions ExOPS = new ExportOptions();

        /// <summary>
        ///  /以字符流的形式下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isDelete"></param>
        public static void DownLoad(string filePath, bool isDelete)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.Web.HttpContext curContext = System.Web.HttpContext.Current;
                    //以字符流的形式下载文件
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    //删除文件
                    if (isDelete)
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                        System.IO.File.Delete(filePath);
                    }

                    curContext.Response.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开
                    curContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath), System.Text.Encoding.UTF8));
                    curContext.Response.BinaryWrite(bytes);
                    curContext.Response.Flush();
                    curContext.Response.End();
                }
            }
            catch (Exception ex)
            {
                _log.Error(DateTime.Now.ToString() + ": [DownLoad]" + ex.ToString());
            }
        }

        /// <summary>
        /// 导出报表文件为PDF格式
        /// </summary>
        /// <param name="ReportFile">报表文件名称</param>
        /// <param name="ReportDataSource">报表文件所使用的数据源,是一个Dataset</param>
        /// <param name="PDFFileName">你要导成的目标文件名称,注意不要放在wwwroot等目录中,iis会不让你导出的</param>
        /// <returns>bool成功返回true,失败返回false</returns>
        public static bool ExportToPDF(string ReportFile, DataTable ReportDataSource, string PDFFileName)
        {
            try
            {
                ReportDocument customerReport = new ReportDocument();
                customerReport.Load(ReportFile);
                customerReport.SetDataSource(ReportDataSource);
                FileOPS.DiskFileName = PDFFileName;
                ExOPS = customerReport.ExportOptions;
                ExOPS.DestinationOptions = FileOPS;
                ExOPS.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                ExOPS.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                customerReport.Export();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(DateTime.Now.ToString() + ": [ExportToPDF]" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtData"></param>
        public static void DataTable2Excel(DataTable dtData)
        {
            try
            {
                System.Web.UI.WebControls.DataGrid dgExport = null;
                // 当前对话 
                System.Web.HttpContext curContext = System.Web.HttpContext.Current;
                // IO用于导出并返回excel文件 
                System.IO.StringWriter strWriter = null;
                System.Web.UI.HtmlTextWriter htmlWriter = null;

                if (dtData != null)
                {
                    // 设置编码和附件格式 
                    curContext.Response.Buffer = true;
                    // curContext.Response.Charset = "utf-8";
                    curContext.Response.ContentType = "application/vnd.ms-excel";
                    curContext.Response.AddHeader("content-disposition", "attachment;filename=scm_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                    HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

                    // 导出excel文件 
                    strWriter = new System.IO.StringWriter();
                    htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                    // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid 
                    dgExport = new System.Web.UI.WebControls.DataGrid();
                    dgExport.DataSource = dtData.DefaultView;
                    dgExport.AllowPaging = false;
                    dgExport.DataBind();

                    // 返回客户端 
                    dgExport.RenderControl(htmlWriter);
                    curContext.Response.Write(strWriter.ToString());
                    curContext.Response.End();
                }
            }
            catch (Exception ex)
            {
                _log.Error("[DataTable2Excel]" + ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtData"></param>
        public static void GridView2Excel(Control ctl, string FileName)
        {
            try
            {
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
                ctl.Page.EnableViewState = false;
                System.IO.StringWriter tw = new System.IO.StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                ctl.RenderControl(hw);
                HttpContext.Current.Response.Write(tw.ToString());
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                _log.Error("[GridView2Excel]" + ex.ToString());
            }
        }

        //保留小数位置后面的两位
        public static string FormateRate(string rateStr)
        {
            if (rateStr.IndexOf(".") != -1)
            {
                //获取小数点的位置  
                int num = 0;
                num = rateStr.IndexOf(".");

                //获取小数点后面的数字 是否有两位 不足两位补足两位  
                String dianAfter = rateStr.Substring(0, num + 1);
                String afterData = rateStr.Replace(dianAfter, "");
                if (afterData.Length < 2)
                {
                    afterData = afterData + "0";
                }
                else
                {
                    afterData = afterData;
                }
                return rateStr.Substring(0, num) + "." + afterData.Substring(0, 2);
            }
            else
            {
                if (rateStr == "1")
                {
                    return "100";
                }
                else
                {
                    return rateStr;
                }
            }

        }


    }//end class
}

using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.IO;
using System.Xml;
using SCM.Bll;
using SCM.Common;

namespace SCM.Web
{
    /// <summary>
    ///WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://czzd.scm.webservice")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        public WebService()
        {
            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        /// <summary>
        /// 门店数据导入
        /// </summary>
        [WebMethod]
        public string SetDataInfo(string tableName, string xmlData, string webServiceKey)
        {
            try
            {
                string WebServiceKey = Common.DESEncrypt.Decrypt(webServiceKey);
                if (WebServiceKey != tableName + xmlData.Length + CConstant.WEBSERVICE_POS2SCM_KEY)
                {
                    return CConstant.NO_DATA;
                }
                DataSet ds = ConvertXMLToDataSet(xmlData);
                DataTable dt = new DataTable();
                if (ds != null)
                {
                    switch (tableName)
                    {
                        case "SALES":　　//销售记录
                            BSalesOrder bSalesOrder = new BSalesOrder();
                            dt = bSalesOrder.Insert(ds);
                            break;
                        case "CUSTOMER":　//顾客新建
                            BVipCustomer bCustomer = new BVipCustomer();
                            dt = bCustomer.Insert(ds);
                            break;
                        case "CASH"://钱箱导入
                            BCash bcash = new BCash();
                            dt = bcash.Insert(ds);
                            break;
                        case "CASH_BANK": //钱箱流水号的导入
                            BCash bsh = new BCash();
                            dt = bsh.Update(ds);
                            break;
                    }
                    ds = new DataSet();
                    ds.Tables.Clear();
                    ds.Tables.Add(dt);
                }
                else
                {
                    return CConstant.NO_DATA;
                }
                return ds.GetXml();
            }
            catch (Exception e)
            {
                return CConstant.NO_DATA;
            }

        }

        /// <summary>
        /// 获得系统时间
        /// </summary>
        [WebMethod]
        public string GetSystemTime()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// 获得基础数据
        /// </summary>
        [WebMethod]
        public string GetDataInfo(string departmentCode, string tableName, string dateTime, string webServiceKey)
        {
            try
            {
                string WebServiceKey = Common.DESEncrypt.Decrypt(webServiceKey);
                string Web = departmentCode + tableName + dateTime + CConstant.WEBSERVICE_SCM2POS_KEY;
                if (WebServiceKey != Web)
                {
                    return CConstant.NO_DATA;
                }
                BCommon bll = new BCommon();
                DataSet ds = new DataSet();
                switch (tableName)
                {
                    case "PRODUCT_GROUP":
                        departmentCode = null;
                        break;
                    case "PRODUCT":
                        departmentCode = null;
                        break;
                    case "STYLE":
                        departmentCode = null;
                        break;
                    case "COLOR":
                        departmentCode = null;
                        break;
                    case "SIZE":
                        departmentCode = null;
                        break;
                    case "UNIT":
                        departmentCode = null;
                        break;
                    case "PRODUCT_PRICE":
                        //departmentCode = null;
                        break;
                    case "VIP_CUSTOMER":
                        departmentCode = null;
                        break;
                    case "USER":
                        dateTime = "";
                        break;
                    case "SALES_PROMOTION":
                        dateTime = "";
                        break;
                    case "NAMES":
                        dateTime = "";
                        break;


                }
                if (tableName == "NAMES")
                {
                    tableName = "NAMES";
                }
                else
                {
                    tableName = "BASE_" + tableName;
                }

                string ret = "";
                try
                {
                    ds = bll.GetExportList(departmentCode, tableName, dateTime);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ret = CConstant.SUCCESS + ds.GetXml();　//成功
                        ret = CConstant.SUCCESS + GetDataSetXml(tableName, ds.Tables[0]);
                    }
                    else
                    {
                        ret = CConstant.NO_DATA; //记录不存在
                    }
                }
                catch (Exception ex)
                {
                    ret = CConstant.ERROR;　//系统异常
                }
                return ret;
            }
            catch
            {
                return CConstant.ERROR;
            }

        }

        /// <summary>
        ///  XML字符串转换成DataSet
        /// </summary>
        private DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet ds = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                ds.ReadXml(reader);
                return ds;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        /// <summary>
        /// /// 获取DataSet的Xml格式 
        /// 
        /// </summary> 
        /// 
        /// <param name="tableName">名称 Table1</param>        
        /// <param name="table">DataTable</param>        
        private string GetDataSetXml(string tableName, DataTable table)
        {
            string str = string.Empty;
            str += "<" + tableName + ">";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str += "<ds>";
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string clName = table.Columns[j].ColumnName;
                    str += "<" + clName + ">" + table.Rows[i][clName].ToString() + "</" + clName + ">";
                }
                str += "</ds>";
            }
            str += "</" + tableName + ">";
            return str;
        }

        public string InportDepGrp(string datatime, string webServiceKey)//导入
        {

            string WebServiceKey = Common.DESEncrypt.Decrypt(webServiceKey);
            if (WebServiceKey == datatime + webServiceKey)
            {
                BStaDepGrpSales st = new BStaDepGrpSales();
                BStaDepGrpSizeSales ss = new BStaDepGrpSizeSales();
                try
                {
                    int i = st.InsertInfoOne();
                    int y = st.InsertInfoTwo();
                    int z = st.InsertInfoThree();
                }
                catch { }
                try
                {
                    int a = ss.InsertOneDepGrpSize();
                    int b = ss.InsertThreeDepGrpSize();
                    int c = ss.InsertTwoDepGrpSize();
                }
                catch { }
            }
            else
            {
                return CConstant.ERROR;
            }

            return "";

        }

    }
}


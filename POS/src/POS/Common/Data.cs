using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;

namespace POS.Common
{
   public class Data
    {

       private static Regex RegNumber = new Regex("^[0-9]+$");
       /// <summary>
       /// 是否数字字符串
       /// </summary>
       /// <param name="inputData">输入字符串</param>
       /// <returns></returns>
       public static bool IsNumber(string inputData)
       {
           Match m = RegNumber.Match(inputData);
           return m.Success;
       }

        #region 获取DataSet的Xml格式
        public static string GetDataSetXml(string tableName, DataTable table)
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
        #endregion

        #region 将xml转换成dataset
        public static DataSet ConvertXMLToDataSet(string xmlData)
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
        #endregion

        //
        public static string ToString(Object obj)
        {
            if (obj != null)
            {
                return Convert.ToString(obj);
            }
            return "";
        }

        public static DateTime ToDateTime(Object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static int ToInt(Object obj)
        {
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        public static decimal Todecimal(Object obj)
        {
            if (obj != null&&Convert.ToString(obj)!="")
            {
                return Convert.ToDecimal(obj);
            }
            return 0;
        }

    }
}

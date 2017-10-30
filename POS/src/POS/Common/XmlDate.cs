using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;

namespace POS.Common
{
   public class XmlDate
    {

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

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace SCM.Common
{
   public class FileOperator
    {
        #region 读取csv文件
        /// <summary>

        /// 读取CVS文件

        /// </summary>

        /// <param name="path">文件路径</param>

        /// <param name="name">文件名称</param>

        /// <returns>DataTable</returns>

       //// public static DataTable ReadCVS(string filepath, string filename)
       // {
       //     DataTable dt = null;
       //     using (Microsoft.VisualBasic.FileIO.TextFieldParser tfp = new Microsoft.VisualBasic.FileIO.TextFieldParser(
       //     filepath + "\\" + filename, Encoding.UTF8))
       //     {
       //         tfp.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
       //         tfp.Delimiters = new string[] { "," };

       //         tfp.HasFieldsEnclosedInQuotes = true;
       //         tfp.TrimWhiteSpace = true;
       //         dt = new DataTable();
       //         DataRow dr;
       //         DataColumn dc;

       //         bool b = true;
       //         while (!tfp.EndOfData)
       //         {

       //             string[] fields = tfp.ReadFields();

       //             int fieldCount = fields.Length;
       //             if (b)
       //             {
       //                 for (int i = 0; i < fieldCount; i++)
       //                 {
       //                     dc = new DataColumn(i.ToString(), typeof(String));
       //                     dt.Columns.Add(dc);
       //                 }
       //                 b = false;
       //             }

       //             dr = dt.NewRow();

       //             for (int i = 0; i < fieldCount; i++)
       //             {
       //                 dr[i.ToString()] = fields[i];
       //             }

       //             dt.Rows.Add(dr);
       //         }
       //     }
       //     return dt;
       // }

        #endregion

        #region 读取Excel文件

        /// <summary>    

        /// 读取Excel文件   

        /// </summary>    

        /// <param name="filepath">文件路径</param>    

        /// <param name="filename">文件名称</param>    

        /// <returns>DataTable</returns>    

        public static DataTable ReadExcel(string filepath, string filename)
        {

            System.Data.DataSet itemDS = new DataSet();

            if (filename.Trim().ToUpper().EndsWith("XLS") || filename.Trim().ToUpper().EndsWith("XLSX"))
            {
                //+ "\\" + filename
                string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath  + ";Extended Properties=\"Excel 12.0;HdR=YES;\"";

                System.Data.OleDb.OleDbConnection conn = null;

                System.Data.OleDb.OleDbCommand oledbCommd = null;

                try
                {

                    conn = new System.Data.OleDb.OleDbConnection();

                    conn.ConnectionString = connStr;

                    conn.Open();

                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    //判断连接Excel sheet名

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DataRow dr = dt.Rows[i];

                        string sqlText = "select * from [" + dr["TABLE_NAME"] + "]";
                        oledbCommd = new System.Data.OleDb.OleDbCommand(sqlText, conn);

                        oledbCommd.CommandTimeout = 100000;

                        //执行

                        System.Data.OleDb.OleDbDataAdapter oledbDA = new System.Data.OleDb.OleDbDataAdapter(oledbCommd);
                        oledbDA.Fill(itemDS);

                    }

                }

                catch(Exception ex)

                {
                    throw ex;
                }

                finally
                {

                    //释放

                    oledbCommd.Dispose();

                    conn.Close();

                }

                //创建连接
            }

            return itemDS.Tables[0];

        }

        #endregion

        #region 读取txt文件
        /// <summary>
        /// 读取Txt文本文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">文件名称</param>
        /// <returns>文本信息</returns>
        public static string ReadTxt(string filepath, string filename)
        {
            StringBuilder sb = new StringBuilder("");
            //StreamReader sr = new StreamReader(filepath + filename); ;
            StreamReader sr = new StreamReader(filepath + filename, Encoding.GetEncoding("GB2312"));
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sb.AppendLine(line);
            }
            sr.Close();
            sr.Dispose();
            return sb.ToString();
        }
        #endregion

        #region 文件删除
        /// <summary>
        /// 删除文件操作
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名称</param>
        public static void DeleteFile(string filePath, string fileName)
        {
            string destinationFile = filePath + fileName;
            //如果文件存在，删除文件
            if (File.Exists(destinationFile))
            {
                FileInfo fi = new FileInfo(destinationFile);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;

                File.Delete(destinationFile);
            }
        }
        #endregion

        #region 拷贝文件
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="fromFilePath">文件的路径</param>
        /// <param name="toFilePath">文件要拷贝到的路径</param>
        public static bool CopyFile(string fromFullPath, string toFilePath, string fileName)
        {
            try
            {
                if (File.Exists(fromFullPath))
                {
                    if (File.Exists(toFilePath + fileName))
                    {
                        File.Delete(toFilePath + fileName);
                    }
                    if (!Directory.Exists(toFilePath))
                    {
                        Directory.CreateDirectory(toFilePath);
                    }
                    File.Move(fromFullPath, toFilePath + fileName);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 写文件
        public static string writeFile(string path, string fileName, string aData)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FileStream fs = new FileStream(path + fileName, FileMode.Create);
                //byte[] data = new UTF8Encoding().GetBytes(aData);
                StreamWriter wr = new StreamWriter(fs);
                wr.Write(aData);
                wr.Flush();
                wr.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return "";
            }
            return path + fileName;
        }
        #endregion

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SCM.Model;
using SCM.IDAL;
using SCM.DBUtility;
using System.Data.SqlClient;
using SCM.DBUtility;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public delegate object PopulateDelegate(IDataReader dr);
    public class CommonManage : ICommon
    {


        public DataSet Query(string SQLString, int Times, string tabname)
        {
            using (SqlConnection connection = new SqlConnection(PubConstant.ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, tabname);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        public static ArrayList GetObjectList(PopulateDelegate pd, string strsql)
        {
            ArrayList lst = new ArrayList();
            using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                // 执行
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lst.Add(pd(dr));
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lst;
        }


        public bool IsDBConn(string strconn, string strsql)
        {
            int rInt = 0;
            using (SqlConnection Conn = new SqlConnection(strconn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(strsql, Conn);
                    cmd.CommandType = CommandType.Text;
                    Conn.Open();
                    rInt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    Conn.Dispose();
                    Conn.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public int Update_Table_Fileds(string Table, string Table_FiledsValue, string Wheres)
        {
            int rInt = 0;
            using (SqlConnection Conn = new SqlConnection(PubConstant.ConnectionString))
            {
                string strSql = string.Format("Update {0} Set {1}  Where {2}", Table, Table_FiledsValue, Wheres);
                SqlCommand cmd = new SqlCommand(strSql, Conn);
                cmd.CommandType = CommandType.Text;
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        public static int Update_Table_Fileds2(string Table, string Table_FiledsValue, string Wheres)
        {
            int rInt = 0;
            using (SqlConnection Conn = new SqlConnection(PubConstant.ConnectionString))
            {
                string strSql = string.Format("Update {0} Set {1}  Where {2}", Table, Table_FiledsValue, Wheres);
                SqlCommand cmd = new SqlCommand(strSql, Conn);
                cmd.CommandType = CommandType.Text;
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 公共查询数据函数Sql存储过程版
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="pp">查询字符串</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>返回记录集ArrayList</returns>
        public static ArrayList GetObjectList(PopulateDelegate pd, QueryParam pp, out int RecordCount)
        {
            ArrayList lst = new ArrayList();
            RecordCount = 0;
            using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SupesoftPage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter ParTotalRecord = new SqlParameter();
                SqlParameter ParTotalPage = new SqlParameter();
                // 设置参数
                cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 500).Value = pp.TableName;
                cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 500).Value = pp.ReturnFields;
                cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 500).Value = pp.Where;
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pp.PageIndex;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pp.PageSize;
                cmd.Parameters.Add("@Orderfld", SqlDbType.NVarChar, 200).Value = pp.Orderfld;
                cmd.Parameters.Add("@OrderType", SqlDbType.Int).Value = pp.OrderType;
                cmd.Parameters.Add("@IsPage", SqlDbType.Int).Value = pp.IsPage;
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.NVarChar).Value = pp.PrimaryKey;
                ParTotalRecord = cmd.Parameters.Add(new SqlParameter("@OTotalRecord", SqlDbType.Int, 1));
                ParTotalPage = cmd.Parameters.Add(new SqlParameter("@OTotalPage", SqlDbType.Int, 1));
                ParTotalPage.Direction = ParameterDirection.Output;
                ParTotalRecord.Direction = ParameterDirection.Output;
                // 执行
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lst.Add(pd(dr));
                }
                // 取记录总数 及页数
                if (dr.NextResult())
                {
                    if (dr.Read())
                    {
                        RecordCount = Convert.ToInt32(dr["RecordCount"]);
                        // RecordCount = Convert.ToInt32(ParTotalRecord.Value.ToString());
                    }
                }

                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lst;
        }



        public object GetobjectValue(string table_name, string table_fileds, string where_fileds)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool Exists(string table_name, string swhere)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DataTable GetCommDt(string table_name, string table_fileds, string where_fileds)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DataTable GetCommDt(string table_name, string table_fields, string where_fields, string orderby_fields)
        {
            throw new Exception("The method or operation is not implemented.");
        }



        public ArrayList GetObjectList(string table_name, string table_fileds, string where_fileds)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DataTable sys_CommonObjectList(QueryParam qp, out int RecordCount)
        {
            return CommonDoDB.sys_CommonObjectList(qp, out RecordCount);
        }

        public DataTable GetObjectDataTableBySql(string strsql)
        {
            DataTable lst = new DataTable();
            DataSet ds = null;
            SqlDataAdapter objAdp = null;

            using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.CommandType = CommandType.Text;
                //根据存储过程的参数个数及类型生成参数对象              
                // 执行
                conn.Open();
                ds = new DataSet();
                ///Instatiate Data Adopter
                objAdp = new SqlDataAdapter(cmd);
                ///Fill Data Set
                objAdp.Fill(ds);
                lst = ds.Tables["Table"];
                ds.Dispose();
                cmd.Dispose();
                objAdp.Dispose();
                conn.Dispose();
                conn.Close();
            }
            return lst;
        }

        public int GetIntCount(string sql)
        {
            return DbHelperSQL.Exists(sql) ? 1 : 0;
        }

        public static int GetCount(string sqlstr)
        {
            int RecordCount = 0;
            using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    RecordCount = Convert.ToInt32(dr["RecordCount"]);
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return RecordCount;
        }

        public static string GetSeq(string blltype)
        {
            string result = string.Empty;
            SqlParameter[] parames = {                                      
                                       new SqlParameter("@P_Bll_Type",SqlDbType.NVarChar),
                                       new SqlParameter("@P_Out_New_Bll_No", SqlDbType.NVarChar)
                                       };
            parames[0].Value = blltype;
            parames[1].Value = "";
            parames[1].Direction = ParameterDirection.Output;
            result = DbHelperSQL.RunProcedureScalarStr("SP_GetSeq", parames);
            return result;
        }


        public static DataTable GetReturnDataTable() 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("LINE_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("STATUS", Type.GetType("System.String"));
            return dt;
        }

        #region ICommon 成员

        public string GetSeqNumber(string blltype)
        {
            return GetSeq(blltype);
        }


        public DataSet GetMasterList(string tableName, string name, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select CODE ,NAME ");
            //strSql.Append(" from "+tableName );
            //strSql.Append(" where NAME LIKE @name");
            if (tableName == "BASE_USER")
            {
                strSql.AppendFormat(" select USER_ID AS CODE,TRUE_NAME AS NAME FROM " + tableName + " WHERE TRUE_NAME LIKE @name");
                strSql.Append(" and STATUS_FLAG <>" + CConstant.DELETE);
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);
                }
                strSql.Append(" order by USER_ID");
            }
            else if (tableName == "BASE_SIZE") 
            {
                strSql.AppendFormat(" select distinct CODE, NAME FROM " + tableName + " WHERE NAME LIKE @name");
                strSql.Append(" and STATUS_FLAG <>" + CConstant.DELETE);
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);
                }
                strSql.Append(" order by CODE");
            }
            else
            {
                strSql.AppendFormat(" select CODE ,NAME from " + tableName + " where NAME LIKE @name");
                strSql.Append(" and STATUS_FLAG <>" + CConstant.DELETE);
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);
                }
                strSql.Append(" order by CODE");
            }
            SqlParameter[] parames = {
					    new SqlParameter("@name", SqlDbType.VarChar)};
            parames[0].Value = name + "%";
            return DbHelperSQL.Query(strSql.ToString(), parames);
        }


        public BaseMaster GetBaseMaster(string tableName, string code, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select CODE ,NAME ");
            //strSql.Append(" from " + tableName);
            //strSql.Append(" where CODE=@code");
            if (tableName == "BASE_USER")
            {
                strSql.AppendFormat(" select USER_ID AS CODE,TRUE_NAME AS NAME FROM " + tableName + " WHERE USER_ID= @code");
                strSql.Append(" and STATUS_FLAG <>" + CConstant.DELETE);
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);
                }
                strSql.Append(" order by USER_ID");
            }
            else
            {
                strSql.AppendFormat(" select CODE ,NAME from " + tableName + " where CODE=@code");
                strSql.Append(" and STATUS_FLAG <>" + CConstant.DELETE);
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);
                }
                strSql.Append(" order by CODE");
            }
            SqlParameter[] parameters = {
					    new SqlParameter("@code", SqlDbType.VarChar)};
            parameters[0].Value = code;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            BaseMaster model = new BaseMaster();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CODE"] != null && ds.Tables[0].Rows[0]["CODE"].ToString() != "")
                {
                    model.Code = ds.Tables[0].Rows[0]["CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NAME"] != null && ds.Tables[0].Rows[0]["NAME"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["NAME"].ToString();
                }
            }
            else
            {
                return null;
            }
            return model;
        }


        public DataSet getNames(string codeType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" SELECT * FROM NAMES WHERE CODE_TYPE = '{0}' AND STATUS_FLAG <> {1} ORDER BY CODE ", codeType, CConstant.DELETE);
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetProductList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from base_Product_view ");
            strSql.Append(" where " + strWhere);
            strSql.Append(" order by CODE");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet getMenu(string userType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from menu_permissions_view ");
            strSql.AppendFormat(" where USER_TYPE='{0}' AND IS_MENU = 1", userType);
            strSql.Append(" ORDER BY USER_TYPE,CATEGORIES_INDEX,PERMISSIONS_INDEX");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetItemList(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from base_item ");
            strSql.Append(" where NAME LIKE @name");
            strSql.Append(" and STATUS_FLAG <>" + CConstant.DELETE);
            strSql.Append(" order by CODE");
            SqlParameter[] parames = {
					    new SqlParameter("@name", SqlDbType.VarChar)};
            parames[0].Value = name + "%";
            return DbHelperSQL.Query(strSql.ToString(), parames);
        }

        public BaseMaster GetCenterWarehouse()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" SELECT TOP 1 CODE ,NAME FROM BASE_WAREHOUSE WHERE STATUS_FLAG <> {0} AND TYPE = {1}", CConstant.DELETE, CConstant.WAREHOUSE_TYPE_CENTER);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            BaseMaster model = new BaseMaster();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CODE"] != null && ds.Tables[0].Rows[0]["CODE"].ToString() != "")
                {
                    model.Code = ds.Tables[0].Rows[0]["CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NAME"] != null && ds.Tables[0].Rows[0]["NAME"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["NAME"].ToString();
                }
            }
            else
            {
                return null;
            }
            return model;
        }
        #endregion

        #region ICommon 成员


        public DataSet GetExportList(string departmentCode, string tableName, string dateTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select * FROM {0} WHERE 1=1 AND STATUS_FLAG<>{1} ", tableName,CConstant.DELETE);
            if (departmentCode != null)
            {
                if ("BASE_USER".Equals(tableName))
                {
                    strSql.AppendFormat(" AND (DEPARTMENT_CODE = '{0}') ", departmentCode);
                }
                else if ("BASE_SALES_PROMOTION".Equals(tableName)) 
                {
                    strSql.AppendFormat("AND END_TIME>'{0}'AND DEPARTMENT_CODE='{1}'", DateTime.Now.ToString("yyyy/MM/dd"),departmentCode );
                }
                else
                {
                    strSql.AppendFormat(" AND (DEPARTMENT_CODE = '{0}' OR DEPARTMENT_CODE='D0000') ", departmentCode);
                }
            }
            if (dateTime != null && dateTime != "")
            {
                strSql.AppendFormat(" AND LAST_UPDATE_TIME >= '{0}' " ,dateTime);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }// end class
}

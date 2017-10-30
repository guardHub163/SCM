using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SCM.Model;
using SCM.DBUtility;

namespace SCM.SQLServerDAL
{
    public class CommonDoDB
    {
        public static DataTable sys_CommonObjectList(QueryParam qp, out int RecordCount)
        {
            DataTable lst = new DataTable();
            RecordCount = 0;
            DataSet ds = null;
            SqlDataAdapter objAdp = null;
            using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SupesoftPage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter ParTotalRecord = new SqlParameter();
                SqlParameter ParTotalPage = new SqlParameter();
                // 设置参数
                cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 500).Value = qp.TableName;
                cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 500).Value = qp.ReturnFields;
                cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 500).Value = qp.Where;
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = qp.PageIndex;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = qp.PageSize;
                cmd.Parameters.Add("@Orderfld", SqlDbType.NVarChar, 200).Value = qp.Orderfld;
                cmd.Parameters.Add("@OrderType", SqlDbType.Int).Value = qp.OrderType;
                cmd.Parameters.Add("@IsPage", SqlDbType.Int).Value = qp.IsPage;
                cmd.Parameters.Add("@PrimaryKey", SqlDbType.NVarChar).Value = qp.PrimaryKey;
                ParTotalRecord = cmd.Parameters.Add(new SqlParameter("@OTotalRecord", SqlDbType.Int, 1));
                ParTotalPage = cmd.Parameters.Add(new SqlParameter("@OTotalPage", SqlDbType.Int, 1));
                ParTotalPage.Direction = ParameterDirection.Output;
                ParTotalRecord.Direction = ParameterDirection.Output;
                // 执行
                conn.Open();
                ds = new DataSet();
                ///Instatiate Data Adopter
                objAdp = new SqlDataAdapter(cmd);
                ///Fill Data Set
                objAdp.Fill(ds);
                lst = ds.Tables["Table"];
                RecordCount = int.Parse(ParTotalRecord.Value.ToString());
                ds.Dispose();
                cmd.Dispose();
                objAdp.Dispose();
                conn.Dispose();
                conn.Close();
            }
            return lst;
        }


        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(PubConstant.ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
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


        //public static List<BaseFormsettingTable> GetFormSettingList(string strwhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    List<BaseFormsettingTable> lst = new List<BaseFormsettingTable>();
        //    strSql.Append("select * from Base_FormSetting where 1=1 ");
        //    if (!string.IsNullOrEmpty(strwhere))
        //        strSql.Append(strwhere);
        //    strSql.Append("order by FORM_ID,TABLE_NAME,DISPLAYED_INDEX  ");

        //    using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand(strSql.ToString(), conn);
        //        cmd.CommandType = CommandType.Text;
        //        if (conn.State != ConnectionState.Open)
        //            conn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            lst.Add(Populatesys_BaseFormsettingTableT(dr));
        //        }
        //        dr.Close();
        //        cmd.Dispose();
        //        conn.Close();
        //    }
        //    return lst;
        //}

        //private static BaseFormsettingTable Populatesys_BaseFormsettingTableT(IDataReader dr)
        //{
        //    BaseFormsettingTable nc = new BaseFormsettingTable();
        //    if (!Convert.IsDBNull(dr["ID"])) nc.ID = Convert.ToInt32(dr["ID"]);
        //    if (!Convert.IsDBNull(dr["TABLE_NAME"])) nc.TABLE_NAME = Convert.ToString(dr["TABLE_NAME"]).Trim();
        //    if (!Convert.IsDBNull(dr["COLUMN_NAME"])) nc.COLUMN_NAME = Convert.ToString(dr["COLUMN_NAME"]).Trim();
        //    if (!Convert.IsDBNull(dr["COLUMN_ALIAS"])) nc.COLUMN_ALIAS = Convert.ToString(dr["COLUMN_ALIAS"]).Trim();
        //    if (!Convert.IsDBNull(dr["COLUMN_TYPE"])) nc.COLUMN_TYPE = Convert.ToString(dr["COLUMN_TYPE"]).Trim();
        //    if (!Convert.IsDBNull(dr["COLUMN_FORMATE"])) nc.COLUMN_FORMATE = Convert.ToString(dr["COLUMN_FORMATE"]).Trim();
        //    if (!Convert.IsDBNull(dr["COLUMN_WITH"])) nc.COLUMN_WITH = Convert.ToInt32(dr["COLUMN_WITH"]);
        //    if (!Convert.IsDBNull(dr["FORM_ID"])) nc.FORM_ID = Convert.ToString(dr["FORM_ID"]).Trim();
        //    if (!Convert.IsDBNull(dr["DGV_ID"])) nc.DGV_ID = Convert.ToString(dr["DGV_ID"]).Trim();
        //    if (!Convert.IsDBNull(dr["DISPLAYED_INDEX"])) nc.DISPLAYED_INDEX = Convert.ToInt32(dr["DISPLAYED_INDEX"]);
        //    if (!Convert.IsDBNull(dr["IS_FORZEN"])) nc.IS_FORZEN = Convert.ToInt32(dr["IS_FORZEN"]);
        //    if (!Convert.IsDBNull(dr["IS_READONLY"])) nc.IS_READONLY = Convert.ToInt32(dr["IS_READONLY"]);
        //    if (!Convert.IsDBNull(dr["IS_DISPLAYED"])) nc.IS_DISPLAYED = Convert.ToInt32(dr["IS_DISPLAYED"]);
        //    if (!Convert.IsDBNull(dr["DIGIT_LEN"])) nc.DIGIT_LEN = Convert.ToInt32(dr["DIGIT_LEN"]);
        //    if (!Convert.IsDBNull(dr["HEAD_TEXT_CN"])) nc.HEAD_TEXT_CN = Convert.ToString(dr["HEAD_TEXT_CN"]).Trim();
        //    if (!Convert.IsDBNull(dr["HEAD_TEXT_TW"])) nc.HEAD_TEXT_TW = Convert.ToString(dr["HEAD_TEXT_TW"]).Trim();
        //    if (!Convert.IsDBNull(dr["HEAD_TEXT_EN"])) nc.HEAD_TEXT_EN = Convert.ToString(dr["HEAD_TEXT_EN"]).Trim();
        //    return nc;
        //}

        //#region return base enterprise

        //public static BaseEnterpriseTable GetEnterpriseTable()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    BaseEnterpriseTable nc = new BaseEnterpriseTable();
        //    strSql.Append("select top 1 * from Base_Enterprise  ");
        //    using (SqlConnection conn = new SqlConnection(PubConstant.ConnectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand(strSql.ToString(), conn);
        //        cmd.CommandType = CommandType.Text;
        //        if (conn.State != ConnectionState.Open)
        //            conn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            nc = Populatesys_BaseEnterpriseTable(dr);
        //        }
        //        dr.Close();
        //        cmd.Dispose();
        //        conn.Close();
        //    }
        //    return nc;
        //}

        //private static BaseEnterpriseTable Populatesys_BaseEnterpriseTable(IDataReader dr)
        //{
        //    BaseEnterpriseTable nc = new BaseEnterpriseTable();
        //    if (!Convert.IsDBNull(dr["ID"])) nc.ID = Convert.ToInt32(dr["ID"]);
        //    if (!Convert.IsDBNull(dr["ENTERPRISE_CODE"])) nc.ENTERPRISE_CODE = Convert.ToString(dr["ENTERPRISE_CODE"]).Trim();
        //    if (!Convert.IsDBNull(dr["ENTERPRISE_NAME"])) nc.ENTERPRISE_NAME = Convert.ToString(dr["ENTERPRISE_NAME"]).Trim();
        //    if (!Convert.IsDBNull(dr["PRINCIPAL"])) nc.PRINCIPAL = Convert.ToString(dr["PRINCIPAL"]).Trim();
        //    if (!Convert.IsDBNull(dr["CONTACT_TEL"])) nc.CONTACT_TEL = Convert.ToString(dr["CONTACT_TEL"]).Trim();
        //    if (!Convert.IsDBNull(dr["CONTACT_FAX"])) nc.CONTACT_FAX = Convert.ToString(dr["CONTACT_FAX"]).Trim();
        //    if (!Convert.IsDBNull(dr["CONTACT_POST"])) nc.CONTACT_POST = Convert.ToString(dr["CONTACT_POST"]).Trim();
        //    if (!Convert.IsDBNull(dr["ENTERPRISE_ADDRESS"])) nc.ENTERPRISE_ADDRESS = Convert.ToString(dr["ENTERPRISE_ADDRESS"]).Trim();
        //    if (!Convert.IsDBNull(dr["ENTERPRISE_NAME_CN"])) nc.ENTERPRISE_NAME_CN = Convert.ToString(dr["ENTERPRISE_NAME_CN"]).Trim();
        //    if (!Convert.IsDBNull(dr["ENTERPRISE_NAME_TITLE"])) nc.ENTERPRISE_NAME_TITLE = Convert.ToString(dr["ENTERPRISE_NAME_TITLE"]).Trim();
        //    if (!Convert.IsDBNull(dr["CUS_CODE"])) nc.CUS_CODE = Convert.ToString(dr["CUS_CODE"]).Trim();
        //    if (!Convert.IsDBNull(dr["COUNTRY_CODE"])) nc.COUNTRY_CODE = Convert.ToString(dr["COUNTRY_CODE"]).Trim();
        //    if (!Convert.IsDBNull(dr["STATUS"])) nc.STATUS = Convert.ToBoolean(dr["STATUS"]);
        //    if (!Convert.IsDBNull(dr["IS_DELETE"])) nc.IS_DELETE = Convert.ToBoolean(dr["IS_DELETE"]);
        //    if (!Convert.IsDBNull(dr["WORK_CORP"])) nc.WORK_CORP = Convert.ToString(dr["WORK_CORP"]).Trim();
        //    if (!Convert.IsDBNull(dr["WORK_CORP_NAME"])) nc.WORK_CORP_NAME = Convert.ToString(dr["WORK_CORP_NAME"]).Trim();
        //    if (!Convert.IsDBNull(dr["RECEIVE_CORP"])) nc.RECEIVE_CORP = Convert.ToString(dr["RECEIVE_CORP"]).Trim();
        //    if (!Convert.IsDBNull(dr["RECEIVE_CORP_NAME"])) nc.RECEIVE_CORP_NAME = Convert.ToString(dr["RECEIVE_CORP_NAME"]).Trim();
        //    if (!Convert.IsDBNull(dr["REMARK"])) nc.REMARK = Convert.ToString(dr["REMARK"]).Trim();
        //    if (!Convert.IsDBNull(dr["ATTRIBUTE1"])) nc.ATTRIBUTE1 = Convert.ToString(dr["ATTRIBUTE1"]).Trim();
        //    if (!Convert.IsDBNull(dr["ATTRIBUTE2"])) nc.ATTRIBUTE2 = Convert.ToString(dr["ATTRIBUTE2"]).Trim();
        //    if (!Convert.IsDBNull(dr["ATTRIBUTE3"])) nc.ATTRIBUTE3 = Convert.ToString(dr["ATTRIBUTE3"]).Trim();
        //    if (!Convert.IsDBNull(dr["ATTRIBUTE4"])) nc.ATTRIBUTE4 = Convert.ToString(dr["ATTRIBUTE4"]).Trim();
        //    if (!Convert.IsDBNull(dr["ATTRIBUTE5"])) nc.ATTRIBUTE5 = Convert.ToString(dr["ATTRIBUTE5"]).Trim();
        //    if (!Convert.IsDBNull(dr["CREATE_USER_ID"])) nc.CREATE_USER_ID = Convert.ToInt32(dr["CREATE_USER_ID"]);
        //    if (!Convert.IsDBNull(dr["CREATE_DATE"])) nc.CREATE_DATE = Convert.ToDateTime(dr["CREATE_DATE"]);
        //    if (!Convert.IsDBNull(dr["LAST_UPDATE_USER_ID"])) nc.LAST_UPDATE_USER_ID = Convert.ToInt32(dr["LAST_UPDATE_USER_ID"]);
        //    if (!Convert.IsDBNull(dr["LAST_UPDATE_DATE"])) nc.LAST_UPDATE_DATE = Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
        //    return nc;
        //}
        //#endregion
    }
}

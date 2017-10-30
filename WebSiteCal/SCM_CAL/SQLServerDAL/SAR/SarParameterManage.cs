using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Collections;
using SCM.Common;
using System.Data;
using System.Data.SqlClient;

namespace SCM.SQLServerDAL
{
    public class SarParameterManage:ISarParameter
    {
        public SarParameterManage()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SarParameterTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_PARAMETER(");
            strSql.Append("CODE_TYPE,CODE,NAME,STATUS_FLAG,NUMBER1,NUMBER2,NUMBER3,NUMBER4,NUMBER5)");
            strSql.Append(" values (");
            strSql.Append("@CODE_TYPE,@CODE,@NAME,@STATUS_FLAG,@NUMBER1,@NUMBER2,@NUMBER3,@NUMBER4,@NUMBER5)");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE_TYPE", SqlDbType.VarChar,50),
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
					new SqlParameter("@NAME", SqlDbType.NVarChar,50),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@NUMBER1", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER2", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER3", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER4", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER5", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CODE_TYPE;
            parameters[1].Value = model.CODE;
            parameters[2].Value = model.NAME;
            parameters[3].Value = model.STATUS_FLAG;
            parameters[4].Value = model.NUMBER1;
            parameters[5].Value = model.NUMBER2;
            parameters[6].Value = model.NUMBER3;
            parameters[7].Value = model.NUMBER4;
            parameters[8].Value = model.NUMBER5;

           return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(List<SarParameterTable> sartablelist)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            foreach (SarParameterTable sartable in sartablelist)
            {

                if (sartable.STATUS_FLAG == 0)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update BASE_PARAMETER set ");
                    strSql.Append("NAME=@NAME,");
                    strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
                    strSql.Append("NUMBER1=@NUMBER1,");
                    strSql.Append("NUMBER2=@NUMBER2,");
                    strSql.Append("NUMBER3=@NUMBER3,");
                    strSql.Append("NUMBER4=@NUMBER4,");
                    strSql.Append("NUMBER5=@NUMBER5");
                    strSql.Append(" where CODE=@CODE ");
                    SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
					new SqlParameter("@NAME", SqlDbType.NVarChar,50),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@NUMBER1", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER2", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER3", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER4", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER5", SqlDbType.VarChar,50)};
                    parameters[0].Value = sartable.CODE;
                    parameters[1].Value = sartable.NAME;
                    parameters[2].Value = sartable.STATUS_FLAG;
                    parameters[3].Value = sartable.NUMBER1;
                    parameters[4].Value = sartable.NUMBER2;
                    parameters[5].Value = sartable.NUMBER3;
                    parameters[6].Value = sartable.NUMBER4;
                    parameters[7].Value = sartable.NUMBER5;
                    sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
                }
                else 
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update BASE_PARAMETER set ");
                    strSql.Append("NAME=@NAME,");
                    strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
                    strSql.Append("NUMBER1=@NUMBER1,");
                    strSql.Append("NUMBER3=@NUMBER3,");
                    strSql.Append("NUMBER4=@NUMBER4,");
                    strSql.Append("NUMBER5=@NUMBER5");
                    strSql.Append(" where CODE=@CODE ");
                    SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
					new SqlParameter("@NAME", SqlDbType.NVarChar,50),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@NUMBER1", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER3", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER4", SqlDbType.VarChar,50),
					new SqlParameter("@NUMBER5", SqlDbType.VarChar,50)};
                    parameters[0].Value = sartable.CODE;
                    parameters[1].Value = sartable.NAME;
                    parameters[2].Value = sartable.STATUS_FLAG;
                    parameters[3].Value = sartable.NUMBER1;
                    parameters[4].Value = sartable.NUMBER3;
                    parameters[5].Value = sartable.NUMBER4;
                    parameters[6].Value = sartable.NUMBER5;
                    sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
                }
            }
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        ///<summary>
        ///获得所有的参数信息
        ///</summary>
        public DataSet GetAllParameterInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from BASE_PARAMETER");
            return DbHelperSQL.Query(strSql.ToString());
        }

        ///<summary>
        ///根据条件获得参数
        ///</summary>
        public DataSet GetParameterNumber(string code) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT NUMBER1,NUMBER2 FROM BASE_PARAMETER WHERE CODE='{0}'", code);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

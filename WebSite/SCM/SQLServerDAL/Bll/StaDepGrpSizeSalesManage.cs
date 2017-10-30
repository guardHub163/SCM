using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.Model;
using SCM.DBUtility;
using System.Data.SqlClient;
using System.Data;
using SCM.Common;

namespace SCM.SQLServerDAL
{
   public partial class StaDepGrpSizeSalesManage:IStaDepGrpSizeSales
   {
       #region 对于数据库中STA_DEP_GRP_SIZE_SALES表的操作
       //判断记录是否存在
        public bool Exists(DateTime Salesdate, string groupcode, string departmentcode,string sizecode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM STA_DEP_GRP_SIZE_SALES ");
            strSql.AppendFormat("WHERE SALES_DATE='{0}' AND PRODUCT_GROUP_CODE='{1}' AND DEPARTMENT_CODE='{2}' AND SIZE_CODE='{3}'", Salesdate, groupcode, departmentcode,sizecode);
            return DbHelperSQL.Exists(strSql.ToString());
        }
       //第一次统计数据
        public DataSet GetTableOne() 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sta_dep_grp_size_sale_view");
            return DbHelperSQL.Query(strSql.ToString());
        }
       //第二次统计数据
        public DataSet GetTableTwo() 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.PARENT_DEPARTMENT_CODE as DEPARTMENT_CODE,A.COMPANY_CODE AS PARENT_DEPARTMENT_CODE, ");
            strSql.Append("A.GROUP_CODE,A.SALES_DATE,A.SIZE,SUM(A.QUANTITY) AS QUANTITY FROM ");
            strSql.Append("dbo.sta_dep_grp_size_sale_view AS A ");
            strSql.Append("GROUP BY A.PARENT_DEPARTMENT_CODE,A.COMPANY_CODE,A.GROUP_CODE,A.SALES_DATE,A.SIZE");
            return DbHelperSQL.Query(strSql.ToString());
        }
       //第三次统计数据
        public DataSet GetTableThree() 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT B.COMPANY_CODE AS DEPARTMENT_CODE,NULL AS PARENT_DEPARTMENT_CODE, ");
            strSql.Append("B.GROUP_CODE,B.SALES_DATE,B.SIZE,SUM(B.QUANTITY) AS QUANTITY FROM  ");
            strSql.Append("sta_dep_grp_size_sale_view AS B ");
            strSql.Append("GROUP BY B.COMPANY_CODE,B.GROUP_CODE,B.SALES_DATE,B.SIZE");
            return DbHelperSQL.Query(strSql.ToString());
        }

       //第一次插入数据
        public int InsertOneDepGrpSize() 
        {
            DataTable da = GetTableOne().Tables[0];
            int ret = 0;
            string name = "admin";
            foreach (DataRow row in da.Rows)
            {
                if (Exists(Convert.ToDateTime(row["SALES_DATE"]), row["GROUP_CODE"].ToString(), row["DEPARTMENT_CODE"].ToString(),row["SIZE"].ToString()))
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update STA_DEP_GRP_SIZE_SALES set ");
                    strSql.Append("QUANTITY=@QUANTITY,");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                    strSql.Append("LAST_UPDATE_TIME=getdate()");
                    strSql.Append(" where SALES_DATE=@SALES_DATE AND PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE AND DEPARTMENT_CODE=@DEPARTMENT_CODE AND SIZE_CODE=@SIZE_CODE");
                    SqlParameter[] parameters = {
                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE_CODE", SqlDbType.VarChar,20)};
                    parameters[0].Value =Convert.ToDecimal(row["QUANTITY"]);
                    parameters[1].Value = name;
                    parameters[2].Value = Convert.ToDateTime(row["SALES_DATE"]);
                    parameters[3].Value = row["GROUP_CODE"].ToString();
                    parameters[4].Value = row["DEPARTMENT_CODE"].ToString();
                    parameters[5].Value = row["SIZE"].ToString();
                    ret = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }
                else 
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into STA_DEP_GRP_SIZE_SALES(");
                    strSql.Append("SALES_DATE,PRODUCT_GROUP_CODE,DEPARTMENT_CODE,PARENT_DEPARTMENT_CODE,SIZE_CODE,QUANTITY,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                    strSql.Append(" values (");
                    strSql.Append("@SALES_DATE,@PRODUCT_GROUP_CODE,@DEPARTMENT_CODE,@PARENT_DEPARTMENT_CODE,@SIZE_CODE,@QUANTITY,@LAST_UPDATE_USER,getdate())");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PARENT_DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                    parameters[0].Value = Convert.ToDateTime(row["SALES_DATE"]);
                    parameters[1].Value = row["GROUP_CODE"].ToString();
                    parameters[2].Value = row["DEPARTMENT_CODE"].ToString();
                    parameters[3].Value = row["PARENT_DEPARTMENT_CODE"].ToString();
                    parameters[4].Value = row["SIZE"].ToString();
                    parameters[5].Value = Convert.ToDecimal(row["QUANTITY"]);
                    parameters[6].Value = name;
                    ret = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }
            }
            return ret;
        }

       //第二次数据插入
        public int InsertTwoDepGrpSize() 
        {
            DataTable da = GetTableTwo().Tables[0];
            int ret = 0;
            string name = "admin";
            foreach (DataRow row in da.Rows)
            {
                if (Exists(Convert.ToDateTime(row["SALES_DATE"]), row["GROUP_CODE"].ToString(), row["DEPARTMENT_CODE"].ToString(), row["SIZE"].ToString()))
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update STA_DEP_GRP_SIZE_SALES set ");
                    strSql.Append("QUANTITY=@QUANTITY,");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                    strSql.Append("LAST_UPDATE_TIME=getdate()");
                    strSql.Append(" where SALES_DATE=@SALES_DATE AND PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE AND DEPARTMENT_CODE=@DEPARTMENT_CODE AND SIZE_CODE=@SIZE_CODE");
                    SqlParameter[] parameters = {
                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE_CODE", SqlDbType.VarChar,20)};
                    parameters[0].Value = Convert.ToDecimal(row["QUANTITY"]);
                    parameters[1].Value = name;
                    parameters[2].Value = Convert.ToDateTime(row["SALES_DATE"]);
                    parameters[3].Value = row["GROUP_CODE"].ToString();
                    parameters[4].Value = row["DEPARTMENT_CODE"].ToString();
                    parameters[5].Value = row["SIZE"].ToString();
                    ret = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }
                else 
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into STA_DEP_GRP_SIZE_SALES(");
                    strSql.Append("SALES_DATE,PRODUCT_GROUP_CODE,DEPARTMENT_CODE,PARENT_DEPARTMENT_CODE,SIZE_CODE,QUANTITY,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                    strSql.Append(" values (");
                    strSql.Append("@SALES_DATE,@PRODUCT_GROUP_CODE,@DEPARTMENT_CODE,@PARENT_DEPARTMENT_CODE,@SIZE_CODE,@QUANTITY,@LAST_UPDATE_USER,getdate())");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PARENT_DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                    parameters[0].Value = Convert.ToDateTime(row["SALES_DATE"]);
                    parameters[1].Value = row["GROUP_CODE"].ToString();
                    parameters[2].Value = row["DEPARTMENT_CODE"].ToString();
                    parameters[3].Value = row["PARENT_DEPARTMENT_CODE"].ToString();
                    parameters[4].Value = row["SIZE"].ToString();
                    parameters[5].Value = Convert.ToDecimal(row["QUANTITY"]);
                    parameters[6].Value = name;
                    ret = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }
            }
            return ret;
        }

       //第三次数据插入
        public int InsertThreeDepGrpSize() 
        {
            DataTable da = GetTableThree().Tables[0];
            int ret = 0;
            string name = "admin";
            foreach (DataRow row in da.Rows) 
            {
                if (Exists(Convert.ToDateTime(row["SALES_DATE"]), row["GROUP_CODE"].ToString(), row["DEPARTMENT_CODE"].ToString(), row["SIZE"].ToString()))
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update STA_DEP_GRP_SIZE_SALES set ");
                    strSql.Append("QUANTITY=@QUANTITY,");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                    strSql.Append("LAST_UPDATE_TIME=getdate()");
                    strSql.Append(" where SALES_DATE=@SALES_DATE AND PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE AND DEPARTMENT_CODE=@DEPARTMENT_CODE AND SIZE_CODE=@SIZE_CODE");
                    SqlParameter[] parameters = {
                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE_CODE", SqlDbType.VarChar,20)};
                    parameters[0].Value = Convert.ToDecimal(row["QUANTITY"]);
                    parameters[1].Value = name;
                    parameters[2].Value = Convert.ToDateTime(row["SALES_DATE"]);
                    parameters[3].Value = row["GROUP_CODE"].ToString();
                    parameters[4].Value = row["DEPARTMENT_CODE"].ToString();
                    parameters[5].Value = row["SIZE"].ToString();
                    ret = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }
                else 
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into STA_DEP_GRP_SIZE_SALES(");
                    strSql.Append("SALES_DATE,PRODUCT_GROUP_CODE,DEPARTMENT_CODE,PARENT_DEPARTMENT_CODE,SIZE_CODE,QUANTITY,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                    strSql.Append(" values (");
                    strSql.Append("@SALES_DATE,@PRODUCT_GROUP_CODE,@DEPARTMENT_CODE,@PARENT_DEPARTMENT_CODE,@SIZE_CODE,@QUANTITY,@LAST_UPDATE_USER,getdate())");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PARENT_DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                    parameters[0].Value = Convert.ToDateTime(row["SALES_DATE"]);
                    parameters[1].Value = row["GROUP_CODE"].ToString();
                    parameters[2].Value = row["DEPARTMENT_CODE"].ToString();
                    parameters[3].Value = null;
                    parameters[4].Value = row["SIZE"].ToString();
                    parameters[5].Value = Convert.ToDecimal(row["QUANTITY"]);
                    parameters[6].Value = name;
                    ret = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                }
            }
            return ret;
        }
       #endregion
   }
}

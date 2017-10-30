using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public partial class VipCustomerManage : IVipCustomer
    {
        public VipCustomerManage()
        { }
        #region IVipCustomer 成员
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_VIP_CUSTOMER");
            strSql.Append(" where CODE=@CODE AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 记录是否己删除
        /// </summary>
        private bool isDelete(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_VIP_CUSTOMER");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseVipCustomerTable customerTable)
        {
            if (isDelete(customerTable.CODE))
            {
                return Update(customerTable) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_VIP_CUSTOMER(");
            strSql.Append("CODE,VIP_LEVEL,NAME,DEPARTMENT_CODE,ADDRESS,QQ,EMAIL,WW,BIRTH_DATE,LAST_SALES_DATE,DISCOUNT_RATE,POINTS,USED_POINTS,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@VIP_LEVEL,@NAME,@DEPARTMENT_CODE,@ADDRESS,@QQ,@EMAIL,@WW,@BIRTH_DATE,@LAST_SALES_DATE,@DISCOUNT_RATE,@POINTS,@USED_POINTS,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,getdate(),@CREATE_USER,getdate(),@LAST_UPDATE_USER)");
            SqlParameter[] parameters = {
                    new SqlParameter("@CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@VIP_LEVEL", SqlDbType.Int,4),
                    new SqlParameter("@NAME", SqlDbType.NVarChar,255),
                    new SqlParameter("@DEPARTMENT_CODE", SqlDbType.NVarChar,255),
                    new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
                    new SqlParameter("@QQ", SqlDbType.NVarChar,255),
                    new SqlParameter("@EMAIL", SqlDbType.NVarChar,255),
                    new SqlParameter("@WW", SqlDbType.NVarChar,255),
                    new SqlParameter("@BIRTH_DATE", SqlDbType.DateTime),
                    new SqlParameter("@LAST_SALES_DATE", SqlDbType.DateTime),
                    new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
                    new SqlParameter("@POINTS", SqlDbType.Int,4),
                    new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
                    new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
                    new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
                    new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = customerTable.CODE;
            parameters[1].Value = customerTable.VIP_LEVEL;
            parameters[2].Value = customerTable.NAME;
            parameters[3].Value = customerTable.DEPARTMENT_CODE;
            parameters[4].Value = customerTable.ADDRESS;
            parameters[5].Value = customerTable.QQ;
            parameters[6].Value = customerTable.EMAIL;
            parameters[7].Value = customerTable.WW;
            parameters[8].Value = customerTable.BIRTH_DATE.ToString("yyyy/MM/dd");
            parameters[9].Value = customerTable.LAST_SALES_DATE;
            parameters[10].Value = customerTable.DISCOUNT_RATE;
            parameters[11].Value = customerTable.POINTS;
            parameters[12].Value = 0;
            parameters[13].Value = customerTable.ATTRIBUTE1;
            parameters[14].Value = customerTable.ATTRIBUTE2;
            parameters[15].Value = customerTable.ATTRIBUTE3;
            parameters[16].Value = customerTable.CREATE_USER;
            parameters[17].Value = customerTable.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseVipCustomerTable cstomerTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_VIP_CUSTOMER set ");
            strSql.Append("VIP_LEVEL=@VIP_LEVEL,");
            strSql.Append("NAME=@NAME,");
            strSql.Append("DEPARTMENT_CODE=@DEPARTMENT_CODE,");
            strSql.Append("ADDRESS=@ADDRESS,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("EMAIL=@EMAIL,");
            strSql.Append("WW=@WW,");
            strSql.Append("BIRTH_DATE=@BIRTH_DATE,");
            strSql.Append("LAST_SALES_DATE=@LAST_SALES_DATE,");
            strSql.Append("DISCOUNT_RATE=@DISCOUNT_RATE,");
            strSql.Append("POINTS=@POINTS,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_TIME=getdate(),");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@VIP_LEVEL", SqlDbType.Int,4),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.NVarChar,255),
					new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
					new SqlParameter("@QQ", SqlDbType.NVarChar,255),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,255),
					new SqlParameter("@WW", SqlDbType.NVarChar,255),
					new SqlParameter("@BIRTH_DATE", SqlDbType.DateTime),
					new SqlParameter("@LAST_SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					new SqlParameter("@POINTS", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = cstomerTable.CODE;
            parameters[1].Value = cstomerTable.VIP_LEVEL;
            parameters[2].Value = cstomerTable.NAME;
            parameters[3].Value = cstomerTable.DEPARTMENT_CODE;
            parameters[4].Value = cstomerTable.ADDRESS;
            parameters[5].Value = cstomerTable.QQ;
            parameters[6].Value = cstomerTable.EMAIL;
            parameters[7].Value = cstomerTable.WW;
            parameters[8].Value = cstomerTable.BIRTH_DATE;
            parameters[9].Value = cstomerTable.LAST_SALES_DATE;
            parameters[10].Value = cstomerTable.DISCOUNT_RATE;
            parameters[11].Value = cstomerTable.POINTS;
            parameters[12].Value = cstomerTable.ATTRIBUTE1;
            parameters[13].Value = cstomerTable.ATTRIBUTE2;
            parameters[14].Value = cstomerTable.ATTRIBUTE3;
            parameters[15].Value = cstomerTable.LAST_UPDATE_USER;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_VIP_CUSTOMER set STATUS_FLAG =" + CConstant.DELETE);
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BaseVipCustomerTable GetModel(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 BV.*,BD.NAME AS DEPARTEMENT_NAME,BU1.TRUE_NAME AS CREATE_NAME,BU2.TRUE_NAME AS UPADATE_NAME ");
            strSql.Append("from BASE_VIP_CUSTOMER BV ");
            strSql.Append("LEFT JOIN BASE_DEPARTMENT BD ON BD.CODE=BV.DEPARTMENT_CODE ");
            strSql.Append("LEFT JOIN BASE_USER BU1 ON BU1.USER_ID=BV.CREATE_USER ");
            strSql.Append("LEFT JOIN BASE_USER BU2 ON BU2.USER_ID=BV.LAST_UPDATE_USER");
            strSql.Append(" where BV.CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            BaseVipCustomerTable customerTable = new BaseVipCustomerTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                customerTable.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                if (ds.Tables[0].Rows[0]["VIP_LEVEL"].ToString() != "")
                {
                    customerTable.VIP_LEVEL = int.Parse(ds.Tables[0].Rows[0]["VIP_LEVEL"].ToString());
                }
                customerTable.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                customerTable.DEPARTMENT_CODE = ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString();
                customerTable.ADDRESS = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                customerTable.QQ =ds.Tables[0].Rows[0]["QQ"].ToString();              
                customerTable.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                customerTable.WW = ds.Tables[0].Rows[0]["WW"].ToString();
                if (ds.Tables[0].Rows[0]["BIRTH_DATE"].ToString() != "")
                {
                    customerTable.BIRTH_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["BIRTH_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_SALES_DATE"].ToString() != "")
                {
                    customerTable.LAST_SALES_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_SALES_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString() != "")
                {
                    customerTable.DISCOUNT_RATE = decimal.Parse(ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["POINTS"].ToString() != "")
                {
                    customerTable.POINTS = int.Parse(ds.Tables[0].Rows[0]["POINTS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USED_POINTS"].ToString() != "")
                {
                    customerTable.USED_POINTS = int.Parse(ds.Tables[0].Rows[0]["USED_POINTS"].ToString());
                }
                customerTable.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                customerTable.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                customerTable.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                customerTable.Department = ds.Tables[0].Rows[0]["DEPARTEMENT_NAME"].ToString();
                customerTable.Creat_name = ds.Tables[0].Rows[0]["CREATE_NAME"].ToString();
                customerTable.Update_name = ds.Tables[0].Rows[0]["UPADATE_NAME"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    customerTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                customerTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    customerTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                customerTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                return customerTable;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_vip_customer_view ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.CODE desc");
            }
            strSql.Append(")AS Row, T.* from base_vip_customer_view T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 门店客户信息导入
        /// </summary>
        public DataTable Insert(DataSet ds)
        {
            DataTable dt = CommonManage.GetReturnDataTable();
            StringBuilder strSql = null;
            DataRow dr = null;
            int rows = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                rows = 0;
                dr = dt.NewRow();
                dr["SLIP_NUMBER"] = row["CODE"];
                try
                {
                    if (Exists(Convert.ToString(row["CODE"])))
                    {
                        strSql = new StringBuilder();
                        strSql.Append("update BASE_VIP_CUSTOMER set ");
                        strSql.Append("NAME=@NAME,");
                        strSql.Append("ADDRESS=@ADDRESS,");
                        strSql.Append("QQ=@QQ,");
                        strSql.Append("EMAIL=@EMAIL,");
                        strSql.Append("WW=@WW,");
                        strSql.Append("BIRTH_DATE=@BIRTH_DATE,");
                        strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
                        strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
                        strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
                        strSql.Append("LAST_UPDATE_TIME=@LAST_UPDATE_TIME,");
                        strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                        strSql.Append(" where CODE=@CODE ");
                        SqlParameter[] parameters = {
					                new SqlParameter("@CODE", SqlDbType.VarChar,20),
					                new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					                new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
					                new SqlParameter("@QQ", SqlDbType.NVarChar,255),
					                new SqlParameter("@EMAIL", SqlDbType.NVarChar,255),
					                new SqlParameter("@WW", SqlDbType.NVarChar,255),
					                new SqlParameter("@BIRTH_DATE", SqlDbType.DateTime),
					                new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                                     new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
					                new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                        parameters[0].Value = row["CODE"];
                        parameters[1].Value = row["NAME"];
                        parameters[2].Value = row["ADDRESS"];
                        parameters[3].Value = row["QQ"];
                        parameters[4].Value = row["EMAIL"];
                        parameters[5].Value = row["WW"];
                        parameters[6].Value = row["BIRTH_DATE"];
                        parameters[7].Value = row["ATTRIBUTE1"];
                        parameters[8].Value = row["ATTRIBUTE2"];
                        parameters[9].Value = row["ATTRIBUTE3"];
                        parameters[10].Value = row["LAST_UPDATE_TIME"];
                        parameters[11].Value = row["LAST_UPDATE_USER"];

                        rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                    }
                    else
                    {

                        strSql = new StringBuilder();
                        strSql.Append("insert into BASE_VIP_CUSTOMER(");
                        strSql.Append("CODE,VIP_LEVEL,NAME,DEPARTMENT_CODE,ADDRESS,QQ,EMAIL,WW,BIRTH_DATE,LAST_SALES_DATE,DISCOUNT_RATE,STATUS_FLAG,POINTS,USED_POINTS,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
                        strSql.Append(" values (");
                        strSql.Append("@CODE,@VIP_LEVEL,@NAME,@DEPARTMENT_CODE,@ADDRESS,@QQ,@EMAIL,@WW,@BIRTH_DATE,@LAST_SALES_DATE,@DISCOUNT_RATE,@STATUS_FLAG,@POINTS,@USED_POINTS,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER)");
                        SqlParameter[] parameters = {
					                new SqlParameter("@CODE", SqlDbType.VarChar,20),
					                new SqlParameter("@VIP_LEVEL", SqlDbType.Int,4),
					                new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					                new SqlParameter("@DEPARTMENT_CODE", SqlDbType.NVarChar,255),
					                new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
					                new SqlParameter("@QQ", SqlDbType.NVarChar,255),
					                new SqlParameter("@EMAIL", SqlDbType.NVarChar,255),
					                new SqlParameter("@WW", SqlDbType.NVarChar,255),
					                new SqlParameter("@BIRTH_DATE", SqlDbType.DateTime),
					                new SqlParameter("@LAST_SALES_DATE", SqlDbType.DateTime),
					                new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					                new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					                new SqlParameter("@POINTS", SqlDbType.Int,4),
					                new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
					                new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					                new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
					                new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					                new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
					                new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                        parameters[0].Value = row["CODE"];
                        parameters[1].Value = row["VIP_LEVEL"];
                        parameters[2].Value = row["NAME"];
                        parameters[3].Value = row["DEPARTMENT_CODE"];
                        parameters[4].Value = row["ADDRESS"];
                        parameters[5].Value = row["QQ"];
                        parameters[6].Value = row["EMAIL"];
                        parameters[7].Value = row["WW"];
                        parameters[8].Value = row["BIRTH_DATE"];
                        parameters[9].Value = row["LAST_SALES_DATE"];
                        parameters[10].Value = row["DISCOUNT_RATE"];
                        parameters[11].Value = row["STATUS_FLAG"];
                        parameters[12].Value = row["POINTS"];
                        parameters[13].Value = row["USED_POINTS"];
                        parameters[14].Value = row["ATTRIBUTE1"];
                        parameters[15].Value = row["ATTRIBUTE2"];
                        parameters[16].Value = row["ATTRIBUTE3"];
                        parameters[17].Value = row["CREATE_DATE_TIME"];
                        parameters[18].Value = row["CREATE_USER"];
                        parameters[19].Value = row["LAST_UPDATE_TIME"];
                        parameters[20].Value = row["LAST_UPDATE_USER"];

                        rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                    }
                }
                catch
                {
                    dr["STATUS"] = CConstant.SUCCESS;
                    dt.Rows.Add(dr);
                    continue;
                }
                if (rows > 0)
                {
                    dr["STATUS"] = CConstant.SUCCESS;
                }
                else
                {
                    dr["STATUS"] = CConstant.ERROR;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #endregion
    }
}

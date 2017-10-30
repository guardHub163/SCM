using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using POS.IDAL;
using POS.DBUtility;
using POS.Model;
using POS.Common;

namespace POS.SQLServerDAL
{
    public partial class VipCustomerManage : IVipCustomer
    {
        public VipCustomerManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_VIP_CUSTOMER");
            strSql.Append(" where CODE=@CODE AND STATUS_FLAG <> " + Constant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 记录是否己删除
        /// </summary>
        public bool isDelete(string CODE)
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
        public int Add(BaseVipCustomerTable model)
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
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
					new SqlParameter("@QQ", SqlDbType.VarChar,20),
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
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.VIP_LEVEL;
            parameters[2].Value = model.NAME;
            parameters[3].Value = model.DEPARTMENT_CODE;
            parameters[4].Value = model.ADDRESS;
            parameters[5].Value = model.QQ;
            parameters[6].Value = model.EMAIL;
            parameters[7].Value = model.WW;
            parameters[8].Value = model.BIRTH_DATE.ToString("yyyy/MM/dd");
            parameters[9].Value = model.LAST_SALES_DATE;
            parameters[10].Value = model.DISCOUNT_RATE;
            parameters[11].Value = model.POINTS;
            parameters[12].Value = model.USED_POINTS;
            parameters[13].Value = model.ATTRIBUTE1;
            parameters[14].Value = model.ATTRIBUTE2;
            parameters[15].Value = model.ATTRIBUTE3;
            parameters[16].Value = model.CREATE_USER;
            parameters[17].Value = model.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int ToAdd(BaseVipCustomerTable model) 
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_VIP_CUSTOMER(");
            strSql.Append("CODE,VIP_LEVEL,NAME,DEPARTMENT_CODE,ADDRESS,QQ,EMAIL,WW,BIRTH_DATE,LAST_SALES_DATE,DISCOUNT_RATE,POINTS,USED_POINTS,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@VIP_LEVEL,@NAME,@DEPARTMENT_CODE,@ADDRESS,@QQ,@EMAIL,@WW,@BIRTH_DATE,@LAST_SALES_DATE,@DISCOUNT_RATE,@POINTS,@USED_POINTS,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER)");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@VIP_LEVEL", SqlDbType.Int,4),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.NVarChar,255),
					new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
					new SqlParameter("@QQ", SqlDbType.VarChar,20),
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
                    new SqlParameter("@CREATE_DATE_TIME",SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_TIME",SqlDbType.DateTime),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.VIP_LEVEL;
            parameters[2].Value = model.NAME;
            parameters[3].Value = model.DEPARTMENT_CODE;
            parameters[4].Value = model.ADDRESS;
            parameters[5].Value = model.QQ;
            parameters[6].Value = model.EMAIL;
            parameters[7].Value = model.WW;
            parameters[8].Value = model.BIRTH_DATE.ToString("yyyy/MM/dd");
            parameters[9].Value = model.LAST_SALES_DATE;
            parameters[10].Value = model.DISCOUNT_RATE;
            parameters[11].Value = model.POINTS;
            parameters[12].Value = model.USED_POINTS;
            parameters[13].Value = model.ATTRIBUTE1;
            parameters[14].Value = model.ATTRIBUTE2;
            parameters[15].Value = model.ATTRIBUTE3;
            parameters[16].Value = model.CREATE_USER;
            parameters[17].Value = model.CREATE_DATE_TIME;
            parameters[18].Value = model.LAST_UPDATE_TIME;
            parameters[19].Value = model.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseVipCustomerTable model)
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
					new SqlParameter("@QQ", SqlDbType.VarChar,20),
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
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.VIP_LEVEL;
            parameters[2].Value = model.NAME;
            parameters[3].Value = model.DEPARTMENT_CODE;
            parameters[4].Value = model.ADDRESS;
            parameters[5].Value = model.QQ;
            parameters[6].Value = model.EMAIL;
            parameters[7].Value = model.WW;
            parameters[8].Value = model.BIRTH_DATE.ToString("yyyy/MM/dd");
            parameters[9].Value = model.LAST_SALES_DATE;
            parameters[10].Value = model.DISCOUNT_RATE;
            parameters[11].Value = model.POINTS;
            parameters[12].Value = model.ATTRIBUTE1;
            parameters[13].Value = model.ATTRIBUTE2;
            parameters[14].Value = model.ATTRIBUTE3;
            parameters[15].Value = model.LAST_UPDATE_USER;

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

        public bool ToUpdate(BaseVipCustomerTable model) 
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
            strSql.Append("CREATE_USER=@CREATE_USER,");
            strSql.Append("CREATE_DATE_TIME=@CREATE_DATE_TIME,");
            strSql.Append("LAST_UPDATE_TIME=@LAST_UPDATE_TIME,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@VIP_LEVEL", SqlDbType.Int,4),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.NVarChar,255),
					new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
					new SqlParameter("@QQ", SqlDbType.VarChar,20),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,255),
					new SqlParameter("@WW", SqlDbType.NVarChar,255),
					new SqlParameter("@BIRTH_DATE", SqlDbType.DateTime),
					new SqlParameter("@LAST_SALES_DATE", SqlDbType.DateTime),
					new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					new SqlParameter("@POINTS", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@CREATE_USER", SqlDbType.NVarChar,20),
                    new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.VIP_LEVEL;
            parameters[2].Value = model.NAME;
            parameters[3].Value = model.DEPARTMENT_CODE;
            parameters[4].Value = model.ADDRESS;
            parameters[5].Value = model.QQ;
            parameters[6].Value = model.EMAIL;
            parameters[7].Value = model.WW;
            parameters[8].Value = model.BIRTH_DATE.ToString("yyyy/MM/dd");
            parameters[9].Value = model.LAST_SALES_DATE;
            parameters[10].Value = model.DISCOUNT_RATE;
            parameters[11].Value = model.POINTS;
            parameters[12].Value = model.ATTRIBUTE1;
            parameters[13].Value = model.ATTRIBUTE2;
            parameters[14].Value = model.ATTRIBUTE3;
            parameters[15].Value = model.CREATE_USER;
            parameters[16].Value = model.CREATE_DATE_TIME;
            parameters[17].Value = model.LAST_UPDATE_TIME;
            parameters[15].Value = model.LAST_UPDATE_USER;

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
            strSql.Append("update BASE_VIP_CUSTOMER set STATUS_FLAG =" + Constant.DELETE);
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
            strSql.Append("select top 1 * from BASE_VIP_CUSTOMER where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            BaseVipCustomerTable model = new BaseVipCustomerTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                if (ds.Tables[0].Rows[0]["VIP_LEVEL"].ToString() != "")
                {
                    model.VIP_LEVEL = int.Parse(ds.Tables[0].Rows[0]["VIP_LEVEL"].ToString());
                }
                model.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                model.DEPARTMENT_CODE = ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString();
                model.ADDRESS = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                if (ds.Tables[0].Rows[0]["QQ"].ToString() != "")
                {
                    model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                }
                model.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                model.WW = ds.Tables[0].Rows[0]["WW"].ToString();
                if (ds.Tables[0].Rows[0]["BIRTH_DATE"].ToString() != "")
                {
                    model.BIRTH_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["BIRTH_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_SALES_DATE"].ToString() != "")
                {
                    model.LAST_SALES_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_SALES_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString() != "")
                {
                    model.DISCOUNT_RATE = decimal.Parse(ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["POINTS"].ToString() != "")
                {
                    model.POINTS = int.Parse(ds.Tables[0].Rows[0]["POINTS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USED_POINTS"].ToString() != "")
                {
                    model.USED_POINTS = int.Parse(ds.Tables[0].Rows[0]["USED_POINTS"].ToString());
                }
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    model.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    model.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                model.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                return model;
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
            strSql.Append("select count(1) FROM base_VipCustomer_view ");
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.* from base_VipCustomer_view T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepartmetnCode()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM dbo.NAMES WHERE CODE_TYPE='DEPARTMENT_CODE'");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetAllInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM BASE_VIP_CUSTOMER ");
            if (strWhere.Trim() != "") 
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool UpdateFlag(int status_flag, string Code) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE BASE_VIP_CUSTOMER SET STATUS_FLAG=@STATUS_FLAG WHERE CODE=@CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@CODE", SqlDbType.NVarChar,225)
                                        };
            parameters[0].Value = status_flag;
            parameters[1].Value = Code;
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

        public DataSet GetVipInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM BASE_VIP_CUSTOMER ");
            if (strWhere.Trim() != "") 
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  Method
    }
}

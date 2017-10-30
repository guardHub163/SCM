using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public partial class SalesPromotionManage : ISalesPromotion
    {
        public SalesPromotionManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_SALES_PROMOTION");
            strSql.Append(" where CODE=@CODE AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 该记录是否删除
        /// </summary>
        private bool isDelete(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_SALES_PROMOTION");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCM.Model.BaseSalesPromotionTable model)
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_SALES_PROMOTION(");
            strSql.Append("CODE,NAME,DEPARTMENT_CODE,PROPERTY1,PROPERTY2,PROPERTY3,PROPERTY4,PROPERTY5,STATUS_FLAG,START_TIME,END_TIME,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@DEPARTMENT,@PROPERTY1,@PROPERTY2,@PROPERTY3,@PROPERTY4,@PROPERTY5,@STATUS_FLAG,@START_TIME,@END_TIME,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@DEPARTMENT", SqlDbType.VarChar,255),
					new SqlParameter("@PROPERTY1", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY2", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY3", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY4", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY5", SqlDbType.NVarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@START_TIME", SqlDbType.DateTime),
					new SqlParameter("@END_TIME", SqlDbType.DateTime),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,50),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.DEPARTMENT_CODE;
            parameters[3].Value = model.PROPERTY1;
            parameters[4].Value = model.PROPERTY2;
            parameters[5].Value = model.PROPERTY3;
            parameters[6].Value = model.PROPERTY4;
            parameters[7].Value = model.PROPERTY5;
            parameters[8].Value = model.STATUS_FLAG;
            parameters[9].Value = model.START_TIME;
            parameters[10].Value = model.END_TIME;
            parameters[11].Value = model.CREATE_USER;
            parameters[12].Value = model.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SCM.Model.BaseSalesPromotionTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_SALES_PROMOTION set ");
            strSql.Append("NAME=@NAME,");
            strSql.Append("DEPARTMENT_CODE=@DEPARTMENT,");
            strSql.Append("PROPERTY1=@PROPERTY1,");
            strSql.Append("PROPERTY2=@PROPERTY2,");
            strSql.Append("PROPERTY3=@PROPERTY3,");
            strSql.Append("PROPERTY4=@PROPERTY4,");
            strSql.Append("PROPERTY5=@PROPERTY5,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("START_TIME=@START_TIME,");
            strSql.Append("END_TIME=@END_TIME,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=getdate()");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@DEPARTMENT", SqlDbType.VarChar,255),
					new SqlParameter("@PROPERTY1", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY2", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY3", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY4", SqlDbType.NVarChar,255),
					new SqlParameter("@PROPERTY5", SqlDbType.NVarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@START_TIME", SqlDbType.DateTime),
					new SqlParameter("@END_TIME", SqlDbType.DateTime),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.DEPARTMENT_CODE;
            parameters[3].Value = model.PROPERTY1;
            parameters[4].Value = model.PROPERTY2;
            parameters[5].Value = model.PROPERTY3;
            parameters[6].Value = model.PROPERTY4;
            parameters[7].Value = model.PROPERTY5;
            parameters[8].Value = model.STATUS_FLAG;
            parameters[9].Value = model.START_TIME;
            parameters[10].Value = model.END_TIME;
            parameters[11].Value = model.LAST_UPDATE_USER;

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
        public SCM.Model.BaseSalesPromotionTable GetModel(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ,BD.NAME AS DEPARTMENT_NAME from BASE_SALES_PROMOTION SP ");
            strSql.Append("LEFT JOIN dbo.BASE_DEPARTMENT AS BD ON SP.DEPARTMENT_CODE = BD.CODE");
            strSql.Append(" where SP.CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            SCM.Model.BaseSalesPromotionTable model = new SCM.Model.BaseSalesPromotionTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                model.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                model.DEPARTMENT_CODE = ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString();
                model.DEPARTMENT_NAME = ds.Tables[0].Rows[0]["DEPARTMENT_NAME"].ToString();
                model.PROPERTY1 = ds.Tables[0].Rows[0]["PROPERTY1"].ToString();
                model.PROPERTY2 = ds.Tables[0].Rows[0]["PROPERTY2"].ToString();
                model.PROPERTY3 = ds.Tables[0].Rows[0]["PROPERTY3"].ToString();
                model.PROPERTY4 = ds.Tables[0].Rows[0]["PROPERTY4"].ToString();
                model.PROPERTY5 = ds.Tables[0].Rows[0]["PROPERTY5"].ToString();
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["START_TIME"].ToString() != "")
                {
                    model.START_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["START_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["END_TIME"].ToString() != "")
                {
                    model.END_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["END_TIME"].ToString());
                }
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    model.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                model.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    model.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_SALES_PROMOTION set STATUS_FLAG = " + CConstant.DELETE);
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
        #endregion  Method


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetPromotionCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_sales_promotion_view ");
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
            strSql.Append(")AS Row, T.* from base_sales_promotion_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}

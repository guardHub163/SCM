using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using POS.IDAL;
using POS.DBUtility;
using POS.Model;
using System.Collections;
using POS.Common;

namespace POS.SQLServerDAL
{
    public partial class SizeManage : ISize
    {
        public SizeManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_SIZE");
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
            strSql.Append("select count(1) from BASE_SIZE");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseSizeTable model)
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_SIZE(");
            strSql.Append("CODE,NAME,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,PRODUCT_GROUP_CODE)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,@CREATE_DATE_TIME,@LAST_UPDATE_USER,@LAST_UPDATE_TIME,@PRODUCT_GROUP_CODE)");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@CREATE_DATE_TIME",SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_TIME",SqlDbType.DateTime),
                    new SqlParameter("@PRODUCT_GROUP_CODE",SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.STATUS_FLAG;
            parameters[3].Value = model.ATTRIBUTE1;
            parameters[4].Value = model.ATTRIBUTE2;
            parameters[5].Value = model.ATTRIBUTE3;
            parameters[6].Value = model.CREATE_USER;
            parameters[7].Value = model.CREATE_DATE_TIME;
            parameters[8].Value = model.LAST_UPDATE_USER;
            parameters[9].Value = model.LAST_UPDATE_TIME;
            parameters[10].Value = model.PRODUCT_GROUP_CODE;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseSizeTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_SIZE set ");
            strSql.Append("NAME=@NAME,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=@LAST_UPDATE_TIME,");
            strSql.Append("CREATE_USER=@CREATE_USER,");
            strSql.Append("CREATE_DATE_TIME=@CREATE_DATE_TIME,");
            strSql.Append("PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_TIME",SqlDbType.DateTime),
                    new SqlParameter("@CREATE_USER",SqlDbType.VarChar,20),
                    new SqlParameter("@CREATE_DATE_TIME",SqlDbType.DateTime),
                    new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.STATUS_FLAG;
            parameters[3].Value = model.ATTRIBUTE1;
            parameters[4].Value = model.ATTRIBUTE2;
            parameters[5].Value = model.ATTRIBUTE3;
            parameters[6].Value = model.LAST_UPDATE_USER;
            parameters[7].Value = model.LAST_UPDATE_TIME;
            parameters[8].Value = model.CREATE_USER;
            parameters[9].Value = model.CREATE_DATE_TIME;
            parameters[10].Value = model.PRODUCT_GROUP_CODE;
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
            strSql.Append("update BASE_SIZE set STATUS_FLAG =" + Constant.DELETE);
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
        public BaseSizeTable GetModel(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 BUN.*,BU1.TRUE_NAME AS CREATE_NAME,BU2.TRUE_NAME AS LAST_UPDATE_NAME,BPG.NAME AS PRODUCT_GROUP_NAME");
            strSql.Append(" from BASE_SIZE BUN");
            strSql.Append(" left join Base_User BU1 ON BUN.CREATE_USER=BU1.USER_ID");
            strSql.Append(" left join Base_User BU2 ON BUN.CREATE_USER=BU2.USER_ID");
            strSql.Append(" left join BASE_PRODUCT_GROUP AS BPG ON BUN.PRODUCT_GROUP_CODE = BPG.CODE");
            strSql.Append(" where BUN.CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            BaseSizeTable model = new BaseSizeTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                model.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                model.PRODUCT_GROUP_CODE = ds.Tables[0].Rows[0]["PRODUCT_GROUP_CODE"].ToString();
                model.PRODUCT_GROUP_NAME = ds.Tables[0].Rows[0]["PRODUCT_GROUP_NAME"].ToString();
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                model.User_name = ds.Tables[0].Rows[0]["CREATE_NAME"].ToString();
                model.Update_name = ds.Tables[0].Rows[0]["LAST_UPDATE_NAME"].ToString();
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
        #endregion  Method
    }
}

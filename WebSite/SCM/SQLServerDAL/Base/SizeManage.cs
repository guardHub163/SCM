using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Collections;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public partial class SizeManage : ISize
    {
        public SizeManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string code,string groupCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_SIZE");
            strSql.Append(" where CODE=@CODE AND PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
                    new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = code;
            parameters[1].Value = groupCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 记录是否己删除
        /// </summary>
        private bool isDelete(string code, string groupCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_SIZE");
            strSql.Append(" where CODE=@CODE AND PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
                    new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = code;
            parameters[1].Value = groupCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseSizeTable model)
        {
            if (isDelete(model.CODE,model.PRODUCT_GROUP_CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_SIZE(");
            strSql.Append("CODE,NAME,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,PRODUCT_GROUP_CODE,REFERENCE_PERCENTAGE)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate(),@PRODUCT_GROUP_CODE,@REFERENCE_PERCENTAGE)");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@PRODUCT_GROUP_CODE",SqlDbType.VarChar,20),
                    new SqlParameter("@REFERENCE_PERCENTAGE",SqlDbType.Decimal,20)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.STATUS_FLAG;
            parameters[3].Value = model.ATTRIBUTE1;
            parameters[4].Value = model.ATTRIBUTE2;
            parameters[5].Value = model.ATTRIBUTE3;
            parameters[6].Value = model.CREATE_USER;
            parameters[7].Value = model.LAST_UPDATE_USER;
            parameters[8].Value = model.PRODUCT_GROUP_CODE;
            parameters[9].Value = model.REFERENCE_PERCENTAGE;
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
            strSql.Append("LAST_UPDATE_TIME=getdate(),");
            strSql.Append("REFERENCE_PERCENTAGE=@REFERENCE_PERCENTAGE");
            strSql.Append(" where CODE=@CODE AND PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@PRODUCT_GROUP_CODE",SqlDbType.VarChar,20),
                    new SqlParameter("@REFERENCE_PERCENTAGE",SqlDbType.Decimal,20)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.STATUS_FLAG;
            parameters[3].Value = model.ATTRIBUTE1;
            parameters[4].Value = model.ATTRIBUTE2;
            parameters[5].Value = model.ATTRIBUTE3;
            parameters[6].Value = model.LAST_UPDATE_USER;
            parameters[7].Value = model.PRODUCT_GROUP_CODE;
            parameters[8].Value = model.REFERENCE_PERCENTAGE;
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
        public bool Delete(string code, string groupCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_SIZE set STATUS_FLAG =" + CConstant.DELETE);
            strSql.Append(" where CODE=@CODE AND PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
                    new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = code;
            parameters[1].Value = groupCode;

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
        public BaseSizeTable GetModel(string code, string groupCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 BUN.*,BU1.TRUE_NAME AS CREATE_NAME,BU2.TRUE_NAME AS LAST_UPDATE_NAME,BPG.NAME AS PRODUCT_GROUP_NAME");
            strSql.Append(" from BASE_SIZE BUN");
            strSql.Append(" left join Base_User BU1 ON BUN.CREATE_USER=BU1.USER_ID");
            strSql.Append(" left join Base_User BU2 ON BUN.LAST_UPDATE_USER=BU2.USER_ID");
            strSql.Append(" left join BASE_PRODUCT_GROUP AS BPG ON BUN.PRODUCT_GROUP_CODE = BPG.CODE");
            strSql.Append(" where BUN.CODE=@CODE AND BUN.PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
                    new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = code;
            parameters[1].Value = groupCode;

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
                if (ds.Tables[0].Rows[0]["REFERENCE_PERCENTAGE"].ToString() != "")
                {
                    model.REFERENCE_PERCENTAGE = decimal.Parse(ds.Tables[0].Rows[0]["REFERENCE_PERCENTAGE"].ToString());
                }
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


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_size_view ");
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
            strSql.Append(")AS Row, T.* from base_size_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  Method

        #region ISize 成员

        /// <summary>
        /// 根据商品种类获得尺寸
        /// </summary>
        public DataSet GetSizeByGroupCode(string groupCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM BASE_SIZE WHERE PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = groupCode;

            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        #endregion
    }
}

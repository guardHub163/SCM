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
    public partial class NewsManage : INews
    {
        public NewsManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(decimal ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_NEWS");
            strSql.Append(" where ID=@ID AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 记录是否己删除
        /// </summary>
        private bool isDelete(decimal ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_NEWS");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseNewsTable model)
        {
            if (isDelete(model.ID))
            {
                return Update(model) ? 1 : 0;

            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_NEWS(");
            strSql.Append("PARENT_ID,PUBLISH_DATE,NEWS_TITLE,NEWS_CONTENT,NEWS_TYPE,STATUS_FLAG,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@PARENT_ID,@PUBLISH_DATE,@NEWS_TITLE,@NEWS_CONTENT,@NEWS_TYPE,@STATUS_FLAG,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
            SqlParameter[] parameters = {
					new SqlParameter("@PARENT_ID", SqlDbType.Decimal,9),
					new SqlParameter("@PUBLISH_DATE", SqlDbType.DateTime),
					new SqlParameter("@NEWS_TITLE", SqlDbType.VarChar,255),
					new SqlParameter("@NEWS_CONTENT", SqlDbType.VarChar,500),
					new SqlParameter("@NEWS_TYPE", SqlDbType.Int,4),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.PARENT_ID;
            parameters[1].Value = model.PUBLISH_DATE;
            parameters[2].Value = model.NEWS_TITLE;
            parameters[3].Value = model.NEWS_CONTENT;
            parameters[4].Value = model.NEWS_TYPE;
            parameters[5].Value = model.STATUS_FLAG;
            parameters[6].Value = model.CREATE_USER;
            parameters[7].Value = model.LAST_UPDATE_USER;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseNewsTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_NEWS set ");
            strSql.Append("PARENT_ID=@PARENT_ID,");
            strSql.Append("PUBLISH_DATE=@PUBLISH_DATE,");
            strSql.Append("NEWS_TITLE=@NEWS_TITLE,");
            strSql.Append("NEWS_CONTENT=@NEWS_CONTENT,");
            strSql.Append("NEWS_TYPE=@NEWS_TYPE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=getdate()");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal,9),
					new SqlParameter("@PARENT_ID", SqlDbType.Decimal,9),
					new SqlParameter("@PUBLISH_DATE", SqlDbType.DateTime),
					new SqlParameter("@NEWS_TITLE", SqlDbType.VarChar,255),
					new SqlParameter("@NEWS_CONTENT", SqlDbType.VarChar,500),
					new SqlParameter("@NEWS_TYPE", SqlDbType.Int,4),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)
                                         };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PARENT_ID;
            parameters[2].Value = model.PUBLISH_DATE;
            parameters[3].Value = model.NEWS_TITLE;
            parameters[4].Value = model.NEWS_CONTENT;
            parameters[5].Value = model.NEWS_TYPE;
            parameters[6].Value = model.STATUS_FLAG;
            parameters[7].Value = model.LAST_UPDATE_USER;
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
        public bool Delete(decimal ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BASE_NEWS ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)
};
            parameters[0].Value = ID;

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
        public BaseNewsTable GetModel(decimal ID)
        {

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select  top 1 ID,PARENT_ID,PUBLISH_DATE,NEWS_TITLE,NEWS_CONTENT,NEWS_TYPE,STATUS_FLAG,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME from BASE_NEWS ");
            //strSql.Append(" where ID=@ID");
            strSql.Append("select top 1 BN.*,BU1.TRUE_NAME AS CREATE_NAME,NA.NAME AS TYPE_NAME");
            strSql.Append(" from BASE_NEWS BN");
            strSql.Append(" left join Base_User BU1 ON BN.CREATE_USER=BU1.USER_ID");
            strSql.Append(" left join dbo.NAMES AS NA ON BN.NEWS_TYPE = NA.CODE AND NA.CODE_TYPE = 'NEW_TYPE'");
            strSql.Append(" where BN.ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)
};
            parameters[0].Value = ID;

            BaseNewsTable model = new BaseNewsTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = decimal.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PARENT_ID"].ToString() != "")
                {
                    model.PARENT_ID = decimal.Parse(ds.Tables[0].Rows[0]["PARENT_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PUBLISH_DATE"].ToString() != "")
                {
                    model.PUBLISH_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["PUBLISH_DATE"].ToString());
                }
                model.NEWS_TITLE = ds.Tables[0].Rows[0]["NEWS_TITLE"].ToString();
                model.NEWS_CONTENT = ds.Tables[0].Rows[0]["NEWS_CONTENT"].ToString();
                model.CREAT_NAME = ds.Tables[0].Rows[0]["CREATE_NAME"].ToString();
                model.TYPE_NAME = ds.Tables[0].Rows[0]["TYPE_NAME"].ToString();
                if (ds.Tables[0].Rows[0]["NEWS_TYPE"].ToString() != "")
                {
                    model.NEWS_TYPE = int.Parse(ds.Tables[0].Rows[0]["NEWS_TYPE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
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
        /// 获取记录总数
        /// </summary>
        public int GetNewsCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_news_view ");
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
        public DataSet GetNewsListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.* from base_news_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet NewsInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 10 BN.*, BU.TRUE_NAME AS CREATE_NAME, NA.NAME AS TYPE_NAME");
            strSql.Append(" from dbo.BASE_NEWS BN  LEFT OUTER JOIN");
            strSql.Append(" dbo.BASE_USER AS BU ON BN.CREATE_USER = BU.USER_ID LEFT OUTER JOIN");
            strSql.Append(" dbo.NAMES AS NA ON BN.NEWS_TYPE = NA.CODE AND NA.CODE_TYPE = 'NEW_TYPE'");
            strSql.Append(" where BN.PARENT_ID=0 AND NEWS_TYPE=2 order by BN.CREATE_DATE_TIME desc");
            return DbHelperSQL.Query(strSql.ToString());

        }

        public DataSet NewsSystemInfo() 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 10 BN.*, BU.TRUE_NAME AS CREATE_NAME, NA.NAME AS TYPE_NAME");
            strSql.Append(" from dbo.BASE_NEWS BN  LEFT OUTER JOIN");
            strSql.Append(" dbo.BASE_USER AS BU ON BN.CREATE_USER = BU.USER_ID LEFT OUTER JOIN");
            strSql.Append(" dbo.NAMES AS NA ON BN.NEWS_TYPE = NA.CODE AND NA.CODE_TYPE = 'NEW_TYPE'");
            strSql.Append(" where BN.PARENT_ID=0 AND NEWS_TYPE=1 order by BN.CREATE_DATE_TIME desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet UserPhoto(string username) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from BASE_USER where TRUE_NAME=@TRUE_NAME");
            SqlParameter[] parameters = {
					new SqlParameter("@TRUE_NAME", SqlDbType.VarChar)};
            parameters[0].Value = username;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        #endregion  Method
    }
}

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
using SCM.Model.Base;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public partial class SupplierManage : ISupplier
    {
        public SupplierManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_SUPPLIER");
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
            strSql.Append("select count(1) from BASE_SUPPLIER");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseSupplierTable model)
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_SUPPLIER(");
            strSql.Append("CODE,NAME,NAME_SHORT,ADDRESS,POST_CODE,TEL,FAX,CONTACT,EMAIL,TYPE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,WAREHOUSE_CODE)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@NAME_SHORT,@ADDRESS,@POST_CODE,@TEL,@FAX,@CONTACT,@EMAIL,@TYPE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate(),@WAREHOUSE_CODE)");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@NAME_SHORT", SqlDbType.NVarChar,255),
					new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
					new SqlParameter("@POST_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@TEL", SqlDbType.NVarChar,20),
					new SqlParameter("@FAX", SqlDbType.NVarChar,20),
					new SqlParameter("@CONTACT", SqlDbType.NVarChar,255),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,255),
					new SqlParameter("@TYPE", SqlDbType.Int,4),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.NAME_SHORT;
            parameters[3].Value = model.ADDRESS;
            parameters[4].Value = model.POST_CODE;
            parameters[5].Value = model.TEL;
            parameters[6].Value = model.FAX;
            parameters[7].Value = model.CONTACT;
            parameters[8].Value = model.EMAIL;
            parameters[9].Value = model.TYPE;
            parameters[10].Value = model.STATUS_FLAG;
            parameters[11].Value = model.ATTRIBUTE1;
            parameters[12].Value = model.ATTRIBUTE2;
            parameters[13].Value = model.ATTRIBUTE3;
            parameters[14].Value = model.CREATE_USER;
            parameters[15].Value = model.LAST_UPDATE_USER;
            parameters[16].Value=model.WAREHOUSE_CODE;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseSupplierTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_SUPPLIER set ");
            strSql.Append("NAME=@NAME,");
            strSql.Append("NAME_SHORT=@NAME_SHORT,");
            strSql.Append("ADDRESS=@ADDRESS,");
            strSql.Append("POST_CODE=@POST_CODE,");
            strSql.Append("TEL=@TEL,");
            strSql.Append("FAX=@FAX,");
            strSql.Append("CONTACT=@CONTACT,");
            strSql.Append("EMAIL=@EMAIL,");
            strSql.Append("TYPE=@TYPE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,"); ;
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=getdate(),");
            strSql.Append("WAREHOUSE_CODE=@WAREHOUSE_CODE");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@NAME_SHORT", SqlDbType.NVarChar,255),
					new SqlParameter("@ADDRESS", SqlDbType.NVarChar,255),
					new SqlParameter("@POST_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@TEL", SqlDbType.NVarChar,20),
					new SqlParameter("@FAX", SqlDbType.NVarChar,20),
					new SqlParameter("@CONTACT", SqlDbType.NVarChar,255),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,255),
					new SqlParameter("@TYPE", SqlDbType.Int,4),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.NAME_SHORT;
            parameters[3].Value = model.ADDRESS;
            parameters[4].Value = model.POST_CODE;
            parameters[5].Value = model.TEL;
            parameters[6].Value = model.FAX;
            parameters[7].Value = model.CONTACT;
            parameters[8].Value = model.EMAIL;
            parameters[9].Value = model.TYPE;
            parameters[10].Value = model.STATUS_FLAG;
            parameters[11].Value = model.ATTRIBUTE1;
            parameters[12].Value = model.ATTRIBUTE2;
            parameters[13].Value = model.ATTRIBUTE3;
            parameters[14].Value = model.LAST_UPDATE_USER;
            parameters[15].Value=model.WAREHOUSE_CODE;
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
            strSql.Append("update BASE_SUPPLIER set STATUS_FLAG =" + CConstant.DELETE);
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
        public BaseSupplierTable GetModel(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BS.*,CASE BS.TYPE WHEN 1 THEN '成品' WHEN 2 THEN '布料' ELSE '' END AS TYPE_NAME,BU.TRUE_NAME AS CREAT_NAME,BU1.TRUE_NAME AS UPDATE_NAME,BW.NAME AS WAREHOUSE_NAME");
            strSql.Append(" from BASE_SUPPLIER BS");
            strSql.Append(" LEFT JOIN Base_User BU ON BS.CREATE_USER=BU.USER_ID");
            strSql.Append(" LEFT JOIN Base_User BU1 ON BS.LAST_UPDATE_USER=BU1.USER_ID");
            strSql.Append(" LEFT JOIN BASE_WAREHOUSE BW ON BW.CODE=BS.WAREHOUSE_CODE");
            strSql.Append(" where BS.CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            BaseSupplierTable model = new BaseSupplierTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                model.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                model.NAME_SHORT = ds.Tables[0].Rows[0]["NAME_SHORT"].ToString();
                model.ADDRESS = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                model.POST_CODE = ds.Tables[0].Rows[0]["POST_CODE"].ToString();
                model.TEL = ds.Tables[0].Rows[0]["TEL"].ToString();
                model.FAX = ds.Tables[0].Rows[0]["FAX"].ToString();
                model.CONTACT = ds.Tables[0].Rows[0]["CONTACT"].ToString();
                model.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                if (ds.Tables[0].Rows[0]["TYPE"].ToString() != "")
                {
                    model.TYPE = int.Parse(ds.Tables[0].Rows[0]["TYPE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                model.WAREHOUSE_CODE = ds.Tables[0].Rows[0]["WAREHOUSE_CODE"].ToString();
                model.Typenama = ds.Tables[0].Rows[0]["TYPE_NAME"].ToString();
                model.Creat_name = ds.Tables[0].Rows[0]["CREAT_NAME"].ToString();
                model.Last_creat_name = ds.Tables[0].Rows[0]["UPDATE_NAME"].ToString();
                model.Warehouse_name = ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString();
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
            strSql.Append("select count(1) FROM base_supplier_view ");
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
            strSql.Append(")AS Row, T.* from base_supplier_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

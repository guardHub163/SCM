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
    public partial class DepartmentManage : IDepartment
    {
        /// <summary>
        /// 数据访问类:BASE_DEPARTMENT
        /// </summary>
        public DepartmentManage()
        { }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_DEPARTMENT");
            strSql.Append(" where CODE=@CODE AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 该记录是否删除
        /// </summary>
        public bool isDelete(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_DEPARTMENT");
            strSql.Append(" where CODE=@CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SCM.Model.BaseDepartmentTable model)
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;

            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_DEPARTMENT(");
            strSql.Append("CODE,NAME,PARENT_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,DEPARTMENT_TYPE)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@PARENT_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate(),@DEPARTMENT_TYPE)");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@PARENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@DEPARTMENT_TYPE",SqlDbType.Int,4)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.PARENT_CODE;
            parameters[3].Value = model.STATUS_FLAG;
            parameters[4].Value = model.ATTRIBUTE1;
            parameters[5].Value = model.ATTRIBUTE2;
            parameters[6].Value = model.ATTRIBUTE3;
            parameters[7].Value = model.CREATE_USER;
            parameters[8].Value = model.LAST_UPDATE_USER;
            parameters[9].Value = model.Department_type;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SCM.Model.BaseDepartmentTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_DEPARTMENT set ");
            strSql.Append("NAME=@NAME,");
            strSql.Append("PARENT_CODE=@PARENT_CODE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,DEPARTMENT_TYPE=@DEPARTMENT_TYPE,");
            strSql.Append("LAST_UPDATE_TIME=getdate()");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@PARENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@DEPARTMENT_TYPE",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.PARENT_CODE;
            parameters[3].Value = model.STATUS_FLAG;
            parameters[4].Value = model.ATTRIBUTE1;
            parameters[5].Value = model.ATTRIBUTE2;
            parameters[6].Value = model.ATTRIBUTE3;
            parameters[7].Value = model.LAST_UPDATE_USER;
            parameters[8].Value = model.Department_type;
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
            strSql.Append("update BASE_DEPARTMENT set STATUS_FLAG = " + CConstant.DELETE);
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
        public SCM.Model.BaseDepartmentTable GetModel(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  TOP 1 BD.*, BH.NAME AS PARENT_NAME,BU.TRUE_NAME AS CREATE_USER_NAME, BU2.TRUE_NAME AS UPDATE_USER_NAME");
            strSql.Append(" FROM  BASE_DEPARTMENT AS BD ");
            strSql.Append("LEFT JOIN BASE_DEPARTMENT AS BH ON BH.CODE = BD.PARENT_CODE ");
            strSql.Append("LEFT JOIN Base_User AS BU ON BD.CREATE_USER = BU.USER_ID ");
            strSql.Append("LEFT JOIN Base_User AS BU2 ON BD.LAST_UPDATE_USER = BU2.USER_ID");
            strSql.Append(" where BD.CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            SCM.Model.BaseDepartmentTable model = new SCM.Model.BaseDepartmentTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                model.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                model.PARENT_CODE = ds.Tables[0].Rows[0]["PARENT_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                model.Parent_name = ds.Tables[0].Rows[0]["PARENT_NAME"].ToString();
                model.Creat_user_name = ds.Tables[0].Rows[0]["CREATE_USER_NAME"].ToString();
                model.Update_user_name = ds.Tables[0].Rows[0]["UPDATE_USER_NAME"].ToString();
                model.Department_type = ds.Tables[0].Rows[0]["DEPARTMENT_TYPE"].ToString();
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
            strSql.Append("select count(1) FROM BASE_DEPARTMENT ");
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
            strSql.Append(")AS Row, T.* from base_departmnet_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE T." + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        ///<summary>
        ///获得部门的code,name
        ///</summary>
        ///
        public DataSet GetDepartmentInfo()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select CODE ,NAME from BASE_DEPARTMENT where DEPARTMENT_TYPE<>99");
            return DbHelperSQL.Query(str.ToString());
        }
    }
}


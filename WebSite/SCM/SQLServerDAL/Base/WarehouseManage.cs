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
    public partial class WarehouseManage : IWarehouse
    {
        public WarehouseManage() { }

        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_WAREHOUSE");
            strSql.Append(" where CODE=@CODE and STATUS_FLAG <> " + CConstant.DELETE);
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
            strSql.Append("select count(1) from BASE_WAREHOUSE");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseWarehouseTable model)
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_WAREHOUSE(");
            strSql.Append("CODE,NAME,TYPE,DEPARTMENT_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@TYPE,@DEPARTMENT_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@TYPE", SqlDbType.Int,4),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.NVarChar,255)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.TYPE;
            parameters[3].Value = model.DEPARTMENT_CODE;
            parameters[4].Value = model.STATUS_FLAG;
            parameters[5].Value = model.ATTRIBUTE1;
            parameters[6].Value = model.ATTRIBUTE2;
            parameters[7].Value = model.ATTRIBUTE3;
            parameters[8].Value = model.CREATE_USER;
            parameters[9].Value = model.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseWarehouseTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_WAREHOUSE set ");
            strSql.Append("NAME=@NAME,");
            strSql.Append("TYPE=@TYPE,");
            strSql.Append("DEPARTMENT_CODE=@DEPARTMENT_CODE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=getdate()");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@TYPE", SqlDbType.Int,4),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.NVarChar,255)
                                       };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.TYPE;
            parameters[3].Value = model.DEPARTMENT_CODE;
            parameters[4].Value = model.STATUS_FLAG;
            parameters[5].Value = model.ATTRIBUTE1;
            parameters[6].Value = model.ATTRIBUTE2;
            parameters[7].Value = model.ATTRIBUTE3;
            parameters[8].Value = model.LAST_UPDATE_USER;
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
            strSql.Append("update BASE_WAREHOUSE set STATUS_FLAG = " + CConstant.DELETE);
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
        public BaseWarehouseTable GetModel(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BW.*,BD.NAME AS DEPARTMENT_NAME,BU.TRUE_NAME AS CREATE_USER_NAME ,BU2.TRUE_NAME AS UPDATE_USER_NAME ");
            strSql.Append("from BASE_WAREHOUSE AS BW ");
            strSql.Append("left join BASE_DEPARTMENT BD ON BD.CODE=BW.DEPARTMENT_CODE ");
            strSql.Append("left join Base_User BU ON BW.CREATE_USER=BU.USER_ID ");
            strSql.Append("left join Base_User BU2 ON BW.LAST_UPDATE_USER=BU2.USER_ID ");
            strSql.Append("where BW.CODE=@CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return GetTable(ds); 
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CODE,NAME,TYPE,DEPARTMENT_ID,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME ");
            strSql.Append(" FROM BASE_WAREHOUSE ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_warehouse_view");
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
                strSql.Append("order by T.CODE asc");
            }
            strSql.Append(")AS Row, T.* from base_warehouse_view T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        public BaseWarehouseTable GetModelByDepartment(string departmentCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 BW.*,BD.NAME AS DEPARTMENT_NAME,BU.TRUE_NAME AS CREATE_USER_NAME ,BU2.TRUE_NAME AS UPDATE_USER_NAME ");
            strSql.Append("from BASE_WAREHOUSE AS BW ");
            strSql.Append("left join BASE_DEPARTMENT BD ON BD.CODE=BW.DEPARTMENT_CODE ");
            strSql.Append("left join Base_User BU ON BW.CREATE_USER=BU.USER_ID ");
            strSql.Append("left join Base_User BU2 ON BW.LAST_UPDATE_USER=BU2.USER_ID ");
            strSql.Append("where BW.DEPARTMENT_CODE=@DEPARTMENT_CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = departmentCode;           
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return GetTable(ds);            
        }      

        #endregion

        private BaseWarehouseTable GetTable(DataSet ds)
        {
            BaseWarehouseTable warehouseTable = new BaseWarehouseTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                warehouseTable.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                warehouseTable.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                if (ds.Tables[0].Rows[0]["TYPE"].ToString() != "")
                {
                    warehouseTable.TYPE = int.Parse(ds.Tables[0].Rows[0]["TYPE"].ToString());
                }
                warehouseTable.DEPARTMENT_CODE = ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    warehouseTable.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                warehouseTable.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                warehouseTable.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                warehouseTable.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                warehouseTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                warehouseTable.DEPARTMENT_NAME = ds.Tables[0].Rows[0]["DEPARTMENT_NAME"].ToString();
                warehouseTable.CREATE_USER_NAME = ds.Tables[0].Rows[0]["CREATE_USER_NAME"].ToString();
                warehouseTable.LAST_UPDATE_USER_NAME = ds.Tables[0].Rows[0]["UPDATE_USER_NAME"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    warehouseTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                warehouseTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    warehouseTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                return warehouseTable;
            }
            else
            {
                return null;
            }
        }
    }
}

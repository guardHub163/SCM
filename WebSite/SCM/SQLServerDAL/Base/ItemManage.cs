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
    public partial class ItemManage : IItem
    {
        public ItemManage()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CODE)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from BASE_ITEM");
			strSql.Append(" where CODE=@CODE AND STATUS_FLAG <> " + CConstant.DELETE);
			SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
			parameters[0].Value = CODE;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 该记录是否删除
        /// </summary>
        private bool isDelete(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_ITEM");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(BaseItemTable model)
		{
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
            }
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into BASE_ITEM(");
			strSql.Append("CODE,NAME,SPEC,UNIT_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ATTRIBUTE4,ATTRIBUTE5,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
			strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@SPEC,@UNIT_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ATTRIBUTE4,@ATTRIBUTE5,@CREATE_USER,getdate(),getdate(),@LAST_UPDATE_USER)");
			SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@SPEC", SqlDbType.NVarChar,255),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE4", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE5", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
			parameters[0].Value = model.CODE;
			parameters[1].Value = model.NAME;
			parameters[2].Value = model.SPEC;
			parameters[3].Value = model.UNIT_CODE;
			parameters[4].Value = model.STATUS_FLAG;
			parameters[5].Value = model.ATTRIBUTE1;
			parameters[6].Value = model.ATTRIBUTE2;
			parameters[7].Value = model.ATTRIBUTE3;
			parameters[8].Value = model.ATTRIBUTE4;
			parameters[9].Value = model.ATTRIBUTE5;
			parameters[10].Value = model.CREATE_USER;
			parameters[11].Value = model.LAST_UPDATE_USER;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(BaseItemTable model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update BASE_ITEM set ");
			strSql.Append("NAME=@NAME,");
			strSql.Append("SPEC=@SPEC,");
			strSql.Append("UNIT_CODE=@UNIT_CODE,");
			strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
			strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
			strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
			strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
			strSql.Append("ATTRIBUTE4=@ATTRIBUTE4,");
			strSql.Append("ATTRIBUTE5=@ATTRIBUTE5,");
            strSql.Append("LAST_UPDATE_TIME=getdate(),");
			strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
			strSql.Append(" where CODE=@CODE ");
			SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@SPEC", SqlDbType.NVarChar,255),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE4", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE5", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
			parameters[0].Value = model.CODE;
			parameters[1].Value = model.NAME;
			parameters[2].Value = model.SPEC;
			parameters[3].Value = model.UNIT_CODE;
			parameters[4].Value = model.STATUS_FLAG;
			parameters[5].Value = model.ATTRIBUTE1;
			parameters[6].Value = model.ATTRIBUTE2;
			parameters[7].Value = model.ATTRIBUTE3;
			parameters[8].Value = model.ATTRIBUTE4;
			parameters[9].Value = model.ATTRIBUTE5;
			parameters[10].Value = model.LAST_UPDATE_USER;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update BASE_ITEM set STATUS_FLAG = " + CConstant.DELETE);
			strSql.Append(" where CODE=@CODE ");
			SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
			parameters[0].Value = CODE;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        public BaseItemTable GetModel(string CODE)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select BRI.* ,BU.NAME AS UNIT_NAME,BU1.TRUE_NAME AS CREAT_NAME,BU2.TRUE_NAME AS UPDATE_NAME ");
            strSql.Append("from dbo.BASE_ITEM AS BRI ");
            strSql.Append("LEFT JOIN dbo.BASE_UNIT AS BU ON BU.CODE=BRI.UNIT_CODE ");
            strSql.Append("LEFT JOIN dbo.BASE_USER AS BU1 ON BU1.USER_ID=BRI.CREATE_USER ");
            strSql.Append("LEFT JOIN dbo.BASE_USER AS BU2 ON BU2.USER_ID=BRI.LAST_UPDATE_USER ");
            strSql.Append(" where BRI.CODE=@CODE ");
			SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
			parameters[0].Value = CODE;

            BaseItemTable model = new BaseItemTable();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				model.CODE=ds.Tables[0].Rows[0]["CODE"].ToString();
				model.NAME=ds.Tables[0].Rows[0]["NAME"].ToString();
				model.SPEC=ds.Tables[0].Rows[0]["SPEC"].ToString();
				model.UNIT_CODE=ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
				if(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString()!="")
				{
					model.STATUS_FLAG=int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
				}
				model.ATTRIBUTE1=ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
				model.ATTRIBUTE2=ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
				model.ATTRIBUTE3=ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
				model.ATTRIBUTE4=ds.Tables[0].Rows[0]["ATTRIBUTE4"].ToString();
				model.ATTRIBUTE5=ds.Tables[0].Rows[0]["ATTRIBUTE5"].ToString();
				model.CREATE_USER=ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                model.Creat_name = ds.Tables[0].Rows[0]["CREAT_NAME"].ToString();
                model.Unit_name = ds.Tables[0].Rows[0]["UNIT_NAME"].ToString();
                model.Update_name = ds.Tables[0].Rows[0]["UPDATE_NAME"].ToString();
				if(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString()!="")
				{
					model.CREATE_DATE_TIME=DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString()!="")
				{
					model.LAST_UPDATE_TIME=DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
				}
				model.LAST_UPDATE_USER=ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
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
        public int GetProductItemCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_Item_view");
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
        public DataSet GetProductItemList(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.* from base_Item_view T");
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

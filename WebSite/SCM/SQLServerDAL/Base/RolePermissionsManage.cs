using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCM.IDAL;
using SCM.DBUtility;//Please add references
namespace SCM.SQLServerDAL
{
	/// <summary>
	/// 数据访问类:Role_Permissions
	/// </summary>
	public partial class RolePermissionsManage:IRolePermissions
	{
		public RolePermissionsManage()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ROLE_ID", "Role_Permissions"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ROLE_ID,int PERMISSION_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Role_Permissions");
			strSql.Append(" where ROLE_ID=@ROLE_ID and PERMISSION_ID=@PERMISSION_ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ROLE_ID", SqlDbType.Int,4),
					new SqlParameter("@PERMISSION_ID", SqlDbType.Int,4)			};
			parameters[0].Value = ROLE_ID;
			parameters[1].Value = PERMISSION_ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SCM.Model.BaseRolePermissionsTable model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Role_Permissions(");
			strSql.Append("ROLE_ID,PERMISSION_ID)");
			strSql.Append(" values (");
			strSql.Append("@ROLE_ID,@PERMISSION_ID)");
			SqlParameter[] parameters = {
					new SqlParameter("@ROLE_ID", SqlDbType.Int,4),
					new SqlParameter("@PERMISSION_ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ROLE_ID;
			parameters[1].Value = model.PERMISSION_ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SCM.Model.BaseRolePermissionsTable model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Role_Permissions set ");

			strSql.Append("ROLE_ID=@ROLE_ID,");
			strSql.Append("PERMISSION_ID=@PERMISSION_ID");
			strSql.Append(" where ROLE_ID=@ROLE_ID and PERMISSION_ID=@PERMISSION_ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ROLE_ID", SqlDbType.Int,4),
					new SqlParameter("@PERMISSION_ID", SqlDbType.Int,4)};
			parameters[0].Value = model.ROLE_ID;
			parameters[1].Value = model.PERMISSION_ID;

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
		public bool Delete(int ROLE_ID,int PERMISSION_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Role_Permissions ");
			strSql.Append(" where ROLE_ID=@ROLE_ID and PERMISSION_ID=@PERMISSION_ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ROLE_ID", SqlDbType.Int,4),
					new SqlParameter("@PERMISSION_ID", SqlDbType.Int,4)			};
			parameters[0].Value = ROLE_ID;
			parameters[1].Value = PERMISSION_ID;

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
		public SCM.Model.BaseRolePermissionsTable GetModel(int ROLE_ID,int PERMISSION_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ROLE_ID,PERMISSION_ID from Role_Permissions ");
			strSql.Append(" where ROLE_ID=@ROLE_ID and PERMISSION_ID=@PERMISSION_ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ROLE_ID", SqlDbType.Int,4),
					new SqlParameter("@PERMISSION_ID", SqlDbType.Int,4)			};
			parameters[0].Value = ROLE_ID;
			parameters[1].Value = PERMISSION_ID;

			SCM.Model.BaseRolePermissionsTable model=new SCM.Model.BaseRolePermissionsTable();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ROLE_ID"]!=null && ds.Tables[0].Rows[0]["ROLE_ID"].ToString()!="")
				{
					model.ROLE_ID=int.Parse(ds.Tables[0].Rows[0]["ROLE_ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PERMISSION_ID"]!=null && ds.Tables[0].Rows[0]["PERMISSION_ID"].ToString()!="")
				{
					model.PERMISSION_ID=int.Parse(ds.Tables[0].Rows[0]["PERMISSION_ID"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ROLE_ID,PERMISSION_ID ");
			strSql.Append(" FROM Role_Permissions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ROLE_ID,PERMISSION_ID ");
			strSql.Append(" FROM Role_Permissions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Role_Permissions ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.PERMISSION_ID desc");
			}
			strSql.Append(")AS Row, T.*  from Role_Permissions T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Role_Permissions";
			parameters[1].Value = "PERMISSION_ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}


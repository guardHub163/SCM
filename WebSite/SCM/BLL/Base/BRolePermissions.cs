using System;
using System.Data;
using System.Collections.Generic;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
namespace SCM.Bll
{
	/// <summary>
	/// Role_Permissions
	/// </summary>
	public partial class BRolePermissions
	{
        private readonly IRolePermissions dal = DataAccess.CreateRolePermissionsManage();
		public BRolePermissions()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ROLE_ID,int PERMISSION_ID)
		{
			return dal.Exists(ROLE_ID,PERMISSION_ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SCM.Model.BaseRolePermissionsTable model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SCM.Model.BaseRolePermissionsTable model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ROLE_ID,int PERMISSION_ID)
		{
			
			return dal.Delete(ROLE_ID,PERMISSION_ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SCM.Model.BaseRolePermissionsTable GetModel(int ROLE_ID,int PERMISSION_ID)
		{
			
			return dal.GetModel(ROLE_ID,PERMISSION_ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public SCM.Model.BaseRolePermissionsTable GetModelByCache(int ROLE_ID,int PERMISSION_ID)
		{
			
			string CacheKey = "Role_PermissionsModel-" + ROLE_ID+PERMISSION_ID;
			object objModel = SCM.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ROLE_ID,PERMISSION_ID);
					if (objModel != null)
					{
						int ModelCache = SCM.Common.ConfigHelper.GetConfigInt("ModelCache");
						SCM.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SCM.Model.BaseRolePermissionsTable)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SCM.Model.BaseRolePermissionsTable> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SCM.Model.BaseRolePermissionsTable> DataTableToList(DataTable dt)
		{
			List<SCM.Model.BaseRolePermissionsTable> modelList = new List<SCM.Model.BaseRolePermissionsTable>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SCM.Model.BaseRolePermissionsTable model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new SCM.Model.BaseRolePermissionsTable();
					if(dt.Rows[n]["ROLE_ID"]!=null && dt.Rows[n]["ROLE_ID"].ToString()!="")
					{
						model.ROLE_ID=int.Parse(dt.Rows[n]["ROLE_ID"].ToString());
					}
					if(dt.Rows[n]["PERMISSION_ID"]!=null && dt.Rows[n]["PERMISSION_ID"].ToString()!="")
					{
						model.PERMISSION_ID=int.Parse(dt.Rows[n]["PERMISSION_ID"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}


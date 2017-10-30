using System;
using System.Data;
namespace SCM.IDAL
{
	/// <summary>
	/// 接口层Role_Permissions
	/// </summary>
	public interface IRolePermissions
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ROLE_ID,int PERMISSION_ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(SCM.Model.BaseRolePermissionsTable model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(SCM.Model.BaseRolePermissionsTable model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ROLE_ID,int PERMISSION_ID);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		SCM.Model.BaseRolePermissionsTable GetModel(int ROLE_ID,int PERMISSION_ID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
	} 
}

using System;
using System.Data;
using System.Collections;
using SCM.Model;
namespace SCM.IDAL
{
	/// <summary>
	/// 接口层Base_User
	/// </summary>
	public interface IUser
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string USER_ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(SCM.Model.BaseUserTable model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(SCM.Model.BaseUserTable model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string USER_ID);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		SCM.Model.BaseUserTable GetModel(int ID);

        DataSet GetDepartmentUserInfo(string strWhere);
		
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		
        /// <summary>
        /// 登录验证
        /// </summary>
        SCM.Model.BaseUserTable ValidateLogin(string userId, string pwd);
		#endregion  成员方法



        DataSet GetUserPageRole(string userType);

        DataSet GetCateGories();

        DataSet GetAllPower();

        DataSet GetSmallPower();

        bool GetUserPower(Hashtable userType, string type);

        bool UpdatePassWord(BaseUserTable busertable);
    } 
}

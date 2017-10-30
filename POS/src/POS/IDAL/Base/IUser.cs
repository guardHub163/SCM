using System;
using System.Data;
using POS.Model;
using System.Collections.Generic;
namespace POS.IDAL
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
        int Add(List<BaseUserTable> UserList);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(POS.Model.BaseUserTable model);
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
		POS.Model.BaseUserTable GetModel(int ID);	
		
        /// <summary>
        /// 登录验证
        /// </summary>
        POS.Model.BaseUserTable ValidateLogin(string userId, string pwd);

        DataSet GetList(string strWhere);

        bool isDelete(string USER_ID);

        int AddName(NamesTable model);

        bool isNameDelete(string CODE_TYPE, string CODE);

        bool UpdatenName(NamesTable model);
		#endregion  成员方法

       
    } 
}

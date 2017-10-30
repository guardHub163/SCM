using System;
using System.Data;
using System.Collections.Generic;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
using System.Collections;
namespace SCM.Bll
{
	/// <summary>
	/// Base_User
	/// </summary>
	public partial class BUser
	{
        private readonly IUser dal = DataAccess.CreateUserManage();
		public BUser()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string USER_ID)
		{
			return dal.Exists(USER_ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(BaseUserTable model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(BaseUserTable model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string USER_ID)
		{
			
			return dal.Delete(USER_ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BaseUserTable GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

        public DataSet GetDepartmentUserInfo(string strWhere) 
        {
            return dal.GetDepartmentUserInfo(strWhere);
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

        public BaseUserTable ValidateLogin(string userId, string pwd) 
        {
            return dal.ValidateLogin(userId, pwd);
        }

		#endregion  Method

        public DataSet GetUserPageRole(string  userType)
        {
            return dal.GetUserPageRole(userType);
        }

        public DataSet GetCateGories() 
        {
            return dal.GetCateGories();
        }

        public DataSet GetAllPower() 
        {
            return dal.GetAllPower();
        }

        public DataSet GetSmallPower() 
        {
            return dal.GetSmallPower();
        }

        public bool GetUserPower(Hashtable userType, string type) 
        {
            return dal.GetUserPower(userType, type);
        }

        public bool UpdatePassWord(BaseUserTable busertable) 
        {
            return dal.UpdatePassWord(busertable);
        }
    }
}


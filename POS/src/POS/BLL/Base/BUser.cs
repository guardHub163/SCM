using System;
using System.Data;
using System.Collections.Generic;
using POS.Common;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;
namespace POS.Bll
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

        public bool isDelete(string USER_ID)
        {
            return dal.isDelete(USER_ID);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(List<BaseUserTable> UserList)
		{
            return dal.Add(UserList);
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
        		

        public BaseUserTable ValidateLogin(string userId, string pwd) 
        {
            return dal.ValidateLogin(userId, pwd);
        }

        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public int AddName(NamesTable model) 
        {
            return dal.AddName(model);
        }

        public bool isNameDelete(string CODE_TYPE, string CODE) 
        {
            return dal.isNameDelete(CODE_TYPE, CODE);
        }

        public bool UpdatenName(NamesTable model)
        {
            return dal.UpdatenName(model);
        }
		#endregion  Method
	}
}


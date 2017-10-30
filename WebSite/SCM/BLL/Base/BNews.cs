using System;
using System.Data;
using System.Collections.Generic;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
namespace SCM.Bll
{
   public partial class BNews
    {
       private readonly INews dal=DataAccess.CreateNewsManage();
       public BNews()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(BaseNewsTable model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(BaseNewsTable model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(decimal ID)
		{
			
			return dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public BaseNewsTable GetModel(decimal ID)
		{
			
			return dal.GetModel(ID);
		}

        public DataSet NewsInfo() 
        {
            return dal.NewsInfo();
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetNewsCount(string strWhere)
        {
            return dal.GetNewsCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetNewsListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetNewsListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet UserPhoto(string username) 
        {
            return dal.UserPhoto(username);
        }

        public DataSet NewsSystemInfo() 
        {
            return dal.NewsSystemInfo();
        }

		#endregion  Method
    }
}

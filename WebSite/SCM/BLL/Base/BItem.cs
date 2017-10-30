using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
using System.Data;

namespace SCM.Bll
{
   public partial class BItem
    {
       private readonly IItem dal=DataAccess.CreateItemManage();
       public BItem()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string CODE)
		{
			return dal.Exists(CODE);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(BaseItemTable model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(BaseItemTable model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string CODE)
		{
			
			return dal.Delete(CODE);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public BaseItemTable GetModel(string CODE)
		{
			
			return dal.GetModel(CODE);
		}

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetProductItemCount(string strWhere)
        {
            return dal.GetProductItemCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetProductItemList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetProductItemList(strWhere, orderby, startIndex, endIndex);
        }

		#endregion  Method
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;

namespace SCM.Bll
{
    public partial class BVipCustomer
    {
        
		private readonly IVipCustomer dal=DataAccess.CreateVipCustomerManage();
        public BVipCustomer()
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
		public int Add(BaseVipCustomerTable model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(BaseVipCustomerTable model)
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
		public BaseVipCustomerTable GetModel(string CODE)
		{
			
			return dal.GetModel(CODE);
		}


        /// <summary>
        /// 分页获取数据总记录数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetList(strWhere, orderby, startIndex, endIndex);
        }		

        /// <summary>
        /// 门店客户信息导入
        /// </summary>
         public DataTable Insert(DataSet ds)
        {
            return dal.Insert(ds);
        }

        #endregion
    }
}

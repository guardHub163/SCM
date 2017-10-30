using System;
using System.Data;
using System.Collections.Generic;
using POS.Common;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;

namespace POS.Bll
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

        public bool isDelete(string CODE) 
        {
            return dal.isDelete(CODE);
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetDepartmetnCode() 
        {
            return dal.GetDepartmetnCode();
        }

        public DataSet GetAllInfo(string strWhere) 
        {
            return dal.GetAllInfo(strWhere);
        }

        public bool UpdateFlag(int status_flag, string Code) 
        {
            return dal.UpdateFlag(status_flag, Code);
        }

        public DataSet GetVipInfo(string strWhere) 
        {
            return dal.GetVipInfo(strWhere);
        }

        public bool ToUpdate(BaseVipCustomerTable model) 
        {
            return dal.ToUpdate(model);
        }

        public int ToAdd(BaseVipCustomerTable model) 
        {
            return dal.ToAdd(model);
        } 

		#endregion  Method
    }
}

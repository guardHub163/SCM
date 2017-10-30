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
	/// BCash
	/// </summary>
	public class BCash
	{
		private readonly ICash dal=DataAccess.CreateCashManage();
		public BCash()
		{}
		#region  Method		

		/// <summary>
        /// 新增一条数据
		/// </summary>
        public int Insert(CashTable model)
		{
			return dal.Insert(model);
		}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(CashTable cashTable)
        {
            return dal.Update(cashTable);
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得对像实体
		/// </summary>
        public CashTable GetModel(string strWhere)
		{
            return dal.GetModel(strWhere);
		}

        ///<summary>
        ///获得状态为1的数据
        ///<summary>
        public DataSet GetCashInfo(string strWhere) 
        {
            return dal.GetCashInfo(strWhere);
        }

        /// <summary>
        /// 信息返回后修改状态
        /// </summary>
        public bool UpdateFlag(int send_flag, string slip_number) 
        {
            return dal.UpdateFlag(send_flag, slip_number);
        }
		#endregion  Method
	}
}


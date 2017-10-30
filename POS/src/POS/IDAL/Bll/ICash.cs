using System;
using System.Data;
namespace POS.IDAL
{
	/// <summary>
	/// 接口层ICash 
	/// </summary>
	public interface ICash
	{
		#region  成员方法
		
		/// <summary>
		/// 新增一条数据
		/// </summary>
		int Insert(POS.Model.CashTable model);
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);

        /// <summary>
        /// 获得对像实体
        /// </summary>
        POS.Model.CashTable GetModel(string strWhere);       

        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(POS.Model.CashTable cashTable);

        /// <summary>
        /// 获得状态为1的数据
        /// </summary>
        DataSet GetCashInfo(string strWhere);

        /// <summary>
        /// 修改状态
        /// </summary>
        bool UpdateFlag(int send_flag, string slip_number);

        #endregion  成员方法
    }
}

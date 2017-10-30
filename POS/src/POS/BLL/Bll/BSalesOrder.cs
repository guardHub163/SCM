using System;
using System.Data;
using System.Collections.Generic;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;
namespace POS.Bll
{
	/// <summary>
	/// BllSalesOrderTable
	/// </summary>
	public class BSalesOrder
	{
		private readonly ISalesOrder dal=DataAccess.CreateSalesOrderManage();
		public BSalesOrder()
		{}
		#region  Method
		

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SalesOrderTable GetModel(string SLIP_NUMBER,int LINE_NUMBER)
		{
			
			return dal.GetModel(SLIP_NUMBER,LINE_NUMBER);
		}		

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}		
        
        /// <summary>
        /// 销售数据插入
        /// </summary>
        public int InsertSales(List<SalesOrderTable> salesList,string Slipnumber)
        {
            return dal.InsertSales(salesList, Slipnumber);
        }

        /// <summary>
        /// 销售数据插入
        /// </summary>
        public int InsertSales(List<SalesOrderTable> returnDatalist, SalesOrderTable salesData, string slipNumber)
        {
            return dal.InsertSales(returnDatalist, salesData, slipNumber);
        }

        /// <summary>
        /// 挂单
        /// </summary>
        public int InsertTmpSalesData(List<TmpSalesOrderTable> orderList)
        {
            return dal.InsertTmpSales(orderList);
        }

        /// <summary>
        /// 取单
        /// </summary>
        public DataSet GetTmpSalesData(string slipNumber) 
        {
            return dal.GetTmpSales(slipNumber);
        }

        /// <summary>
        /// 挂单一览
        /// </summary>
        public DataSet GetTmpSalesGroup(string strWhere)
        {
            return dal.GetTmpSalesGroup(strWhere);
        }

        /// <summary>
        /// 统计挂单数量
        /// </summary>
        public int GetTmpSalesCount() 
        {
            return dal.GetTmpSalesCount();
        }

        /// <summary>
        /// 获得状态为0的数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetSalesInfo(string strWhere) 
        {
            return dal.GetSalesInfo(strWhere);
        }

        public bool UpdateFlge( int send_flag, string slip_number,string line_number) 
        {
            return dal.UpdateFlge(send_flag, slip_number,line_number);
        }
		#endregion  Method

        public DataSet GetPrintList(string strWhere)
        {
            return dal.GetPrintList(strWhere);
        }

        //统计单个的金额
        public DataSet GetSalesStatAmount(string strGroup, string strWhere) 
        {
            return dal.GetSalesStatAmount(strGroup,strWhere);
        }
        //统计所有的金额
        public int GetAllSalesStatAmount(string strWhere) 
        {
            return dal.GetAllSalesStatAmount(strWhere);
        }

        //统计所有的信息的金额
        public DataSet GetAllSalesInfo(string strWhere) 
        {
            return dal.GetAllSalesInfo(strWhere);
        }

        public DataSet GetSaleOrderInfo(string strWhere) 
        {
            return dal.GetSaleOrderInfo(strWhere);
        }
        //统计订单的总金额
        public int GetSumAmount(string slipnumber) 
        {
            return dal.GetSumAmount(slipnumber);
        }

        
    }
}


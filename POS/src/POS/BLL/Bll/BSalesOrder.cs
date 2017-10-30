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
		/// �õ�һ������ʵ��
		/// </summary>
		public SalesOrderTable GetModel(string SLIP_NUMBER,int LINE_NUMBER)
		{
			
			return dal.GetModel(SLIP_NUMBER,LINE_NUMBER);
		}		

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}		
        
        /// <summary>
        /// �������ݲ���
        /// </summary>
        public int InsertSales(List<SalesOrderTable> salesList,string Slipnumber)
        {
            return dal.InsertSales(salesList, Slipnumber);
        }

        /// <summary>
        /// �������ݲ���
        /// </summary>
        public int InsertSales(List<SalesOrderTable> returnDatalist, SalesOrderTable salesData, string slipNumber)
        {
            return dal.InsertSales(returnDatalist, salesData, slipNumber);
        }

        /// <summary>
        /// �ҵ�
        /// </summary>
        public int InsertTmpSalesData(List<TmpSalesOrderTable> orderList)
        {
            return dal.InsertTmpSales(orderList);
        }

        /// <summary>
        /// ȡ��
        /// </summary>
        public DataSet GetTmpSalesData(string slipNumber) 
        {
            return dal.GetTmpSales(slipNumber);
        }

        /// <summary>
        /// �ҵ�һ��
        /// </summary>
        public DataSet GetTmpSalesGroup(string strWhere)
        {
            return dal.GetTmpSalesGroup(strWhere);
        }

        /// <summary>
        /// ͳ�ƹҵ�����
        /// </summary>
        public int GetTmpSalesCount() 
        {
            return dal.GetTmpSalesCount();
        }

        /// <summary>
        /// ���״̬Ϊ0������
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

        //ͳ�Ƶ����Ľ��
        public DataSet GetSalesStatAmount(string strGroup, string strWhere) 
        {
            return dal.GetSalesStatAmount(strGroup,strWhere);
        }
        //ͳ�����еĽ��
        public int GetAllSalesStatAmount(string strWhere) 
        {
            return dal.GetAllSalesStatAmount(strWhere);
        }

        //ͳ�����е���Ϣ�Ľ��
        public DataSet GetAllSalesInfo(string strWhere) 
        {
            return dal.GetAllSalesInfo(strWhere);
        }

        public DataSet GetSaleOrderInfo(string strWhere) 
        {
            return dal.GetSaleOrderInfo(strWhere);
        }
        //ͳ�ƶ������ܽ��
        public int GetSumAmount(string slipnumber) 
        {
            return dal.GetSumAmount(slipnumber);
        }

        
    }
}


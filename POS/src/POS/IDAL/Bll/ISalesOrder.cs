using System;
using System.Data;
using System.Collections.Generic;
using POS.Model;

namespace POS.IDAL
{
	/// <summary>
	/// �ӿڲ�ISalesOrder
	/// </summary>
	public interface ISalesOrder
	{
		#region  ��Ա����		
		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
        POS.Model.SalesOrderTable GetModel(string SLIP_NUMBER, int LINE_NUMBER);
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere);		

        /// <summary>
        /// �������ݲ���
        /// </summary>
        int InsertSales(List<SalesOrderTable> salesOrderList, string Slipnumber);


		#endregion  ��Ա����

        int InsertTmpSales(List<TmpSalesOrderTable> tmpSalesList);

        DataSet GetTmpSales(string slipNumber);

        DataSet GetTmpSalesGroup(string strWhere);

        DataSet GetSalesInfo(string strWhere);

        bool UpdateFlge(int send_flag, string slip_number,string line_number);

        int GetTmpSalesCount();

        DataSet GetPrintList(string strWhere);

        DataSet GetSalesStatAmount(string strGroup, string strWhere);

        int GetAllSalesStatAmount(string strWhere);

        DataSet GetAllSalesInfo(string strWhere);

        DataSet GetSaleOrderInfo(string strWhere);

        int GetSumAmount(string slipnumber);

        int InsertSales(List<SalesOrderTable> returnDatalist, SalesOrderTable salesData, string slipNumber);
    }
}

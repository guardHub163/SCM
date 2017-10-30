using System;
using System.Data;
using System.Collections.Generic;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;

namespace POS.Bll
{
   public class BSalesOrderPlan
    {
       private readonly ISalesOrderPlan dal = DataAccess.CreateSalesOrderPlanManage();

       public BSalesOrderPlan()
		{}
       public int InsertSales(List<SalesOrderPlanTable> salesList, SalesOrderPlanTable saleplan, decimal bankAmount, decimal cashAmount, string customer_code, string customer_phone) 
       {
           return dal.InsertSales(salesList, saleplan, bankAmount, cashAmount, customer_code,customer_phone);
       }

       public DataSet GetSalesOrderPlan(string strWhere) 
       {
           return dal.GetSalesOrderPlan(strWhere);
       }

       public DataSet GetSalesOrderPlanBySlipNumber(string slipnumber) 
       {
           return dal.GetSalesOrderPlanBySlipNumber(slipnumber);
       }

       public int InsertSalesOrder(List<SalesOrderTable> salesList, SalesOrderTable salestable, string slipnumber) 
       {
           return dal.InsertSalesOrder(salesList, salestable, slipnumber);
       }

       public int SalesOrderReturn(SalesOrderTable saleOrder, string slipnumber, decimal deposit) 
       {
           return dal.SalesOrderReturn(saleOrder,slipnumber,deposit);
       }
    }
}

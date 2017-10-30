using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using System.Data;

namespace POS.IDAL
{
    public interface ISalesOrderPlan
    {
        int InsertSales(List<SalesOrderPlanTable> salesList, SalesOrderPlanTable saleplan, decimal bankAmount, decimal cashAmount, string customer_code, string customer_phone);
        DataSet GetSalesOrderPlan(string strWhere);

        DataSet GetSalesOrderPlanBySlipNumber(string slipnumber);

        int InsertSalesOrder(List<SalesOrderTable> salesList, SalesOrderTable salestable, string slipnumber);

        int SalesOrderReturn(SalesOrderTable saleOrder, string slipnumber, decimal deposit);
    }
}

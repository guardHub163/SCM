using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCM.Model;

namespace SCM.IDAL
{
    public interface ISalesOrder
    {
        int GetSalesOrderCount(string sqlWhere);

        DataSet GetSalesOrderList(string sqlWhere, string orderby, int startIndex, int endIndex);

        int GetLastSalesOrderCount(string sqlWhere);

        DataSet GetLastSalesOrderList(string sqlWhere, string orderby, int startIndex, int endIndex);


        DataTable Insert(DataSet ds);

        DataSet GetSalesStatAmount(string strGroup, string strWhere);

        DataSet GetAllOrderInfo(string strWhere);

         DataSet GetSlipNumberInfo(string slipnumber);
         int UpdateStock();
    }
}

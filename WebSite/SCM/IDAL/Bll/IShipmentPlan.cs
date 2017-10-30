using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SCM.IDAL
{
    public interface IShipmentPlan
    {
        int CreateShipmentPlan(string trSlipNumber,string userId);

        int GetRecordCount(string strWhere);

        System.Data.DataSet GetTransferOutPlanList(string strWhere, string orderby, int startIndex, int endIndex);

        int DeleteShipmentPlan(string trSlipNumber, string userId);

        System.Data.DataSet GetTransferOutPlanDetail(string sqlWhere);

        System.Data.DataSet PrintOutMonad(DateTime fromdate, DateTime todate, string warehousecode);

        DataSet PrintShop(string slipnumber);
    }
}

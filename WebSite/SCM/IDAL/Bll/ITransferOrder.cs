using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.IDAL
{
    public interface ITransferOrder
    {
        int UpdateLine(List<SCM.Model.BllTransferOrderLineTable> list);

        int UpdateOrder(SCM.Model.BllTransferOrderTable BllTransferOrderTable);

        int DeleteOrder(string slipNumber);

        int DeleteLine(decimal orderId);

        System.Data.DataSet GetOrderGroupList(string strWhere);

        System.Data.DataSet GetOrderGroupList(string strWhere, string orderby, int startIndex, int endIndex);

        System.Data.DataSet GetTransferOutAssignInfo(string strWhere, string orderby, int startIndex, int endIndex, string departureDate, string warehouseCode);

        System.Data.DataSet GetTransferOutAssignDetailInfo(string slipNumber, string productCode);

        int GetRecordCount(string strWhere);

        int InsertOrder(SCM.Model.BllTransferOrderTable orderTable);

        SCM.Model.BllTransferOrderTable GetBllTransferOrderTable(string strWhere);

        int GetOrderGroupRecordCount(string strWhere);
    }
}

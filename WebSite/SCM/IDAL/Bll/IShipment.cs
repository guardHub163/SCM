using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;

namespace SCM.IDAL
{
    public interface IShipment
    {
        int Insert(SCM.Model.BllShipmentTable shipmentTable);

        int Update(SCM.Model.BllShipmentTable shipmentTable);

        int Delete(string slipNumber, string userId);

        int GetRecordCount(string strWhere);

        System.Data.DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex);

        SCM.Model.BllShipmentTable GetShipmentModel(string slipNumber);

        System.Data.DataSet GetShipmentDetail(string slipNumber);

        string InsertPlan(BllShipmentTable shipmentTable);
        
    }
}

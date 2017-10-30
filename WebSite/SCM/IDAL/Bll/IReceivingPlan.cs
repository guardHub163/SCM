using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.IDAL
{
    public interface IReceivingPlan
    {
        int GetRecordCount(string sqlWhere);

        System.Data.DataSet GetReceivingPlanList(string sqlWhere, string orderby, int startIndex, int endIndex);

        SCM.Model.BllReceivingPlanTable getSearchViewMode(decimal slipNumber);

        bool Insert(List<SCM.Model.BllReceivingPlanTable> list);

        bool Insert(SCM.Model.BllReceiptLineTable rlTable, SCM.Model.BllReceivingPlanTable rpTable, List<SCM.Model.BllReceiptReturnTable> returnlist, string userId);

        int Delete(decimal slipNumber);
    }
}

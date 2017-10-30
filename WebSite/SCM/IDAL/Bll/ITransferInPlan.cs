using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;

namespace SCM.IDAL
{
   public interface ITransferInPlan
    {
       int GetTransferPlanCount(string strWhere);

       System.Data.DataSet GetTransferPlanList(string strWhere, string orderby, int startIndex, int endIndex);

       SCM.Model.BllTransferInPlanTable GetModel(decimal SLIP_NUMBER);

       int GetTransferInfo(BllTransferInPlanTable btable);
    }
}

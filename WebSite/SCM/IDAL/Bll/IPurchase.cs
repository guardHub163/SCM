using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.IDAL
{
    public interface IPurchase
    {
        System.Data.DataSet GetPurchaseList(string sqlWhere, string orderby, int startIndex, int endIndex);

        int GetRecordCount(string sqlWhere);

        int Insert(SCM.Model.BllPurchaseTable purchaseTable);

        int Update(SCM.Model.BllPurchaseTable purchaseTable);

        System.Data.DataSet GetPurchaseDetail(string slipNumber);

        int Delete(string slipNumber);

        int GetPurchaseCount(); 
    }
}

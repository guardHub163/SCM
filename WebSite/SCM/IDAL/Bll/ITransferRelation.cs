using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using System.Data;

namespace SCM.IDAL
{
    public interface ITransferRelation
    {
        int GetTranferRelationCount(string strWhere);

        System.Data.DataSet GetTranferRelationByPage(string strWhere, string orderby, int startIndex, int endIndex);

       int Insert(BllShipmentTable shptable);

       DataSet GetTransferRelationDetail(string slipNumber);
    }
}

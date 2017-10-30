using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SCM.IDAL
{
    public interface ITransferIn
    {

        bool PlanExists(decimal slipNumber);

        System.Data.DataTable Insert(System.Data.DataSet ds);

        int GetTransferInCount(string sqlWhere);

        DataSet GetTransferInList(string sqlWhere, string orderby, int startIndex, int endIndex);
    }
}

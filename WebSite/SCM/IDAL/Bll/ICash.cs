using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SCM.IDAL
{
   public interface ICash
    {
       DataTable Insert(DataSet ds);

       DataTable Update(DataSet ds);


       int GetCashCount(string sqlWhere);

       DataSet GetCashInfo(string sqlWhere, string orderby, int startIndex, int endIndex);
    }
}

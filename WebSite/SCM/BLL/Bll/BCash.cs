using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.DALFactory;
using SCM.IDAL;
using System.Data;
using SCM.Model;

namespace SCM.Bll
{
  public partial class BCash
    {
      private readonly ICash dal = DataAccess.CreateCashManage();


      public DataTable Insert(DataSet ds) 
      {
          return dal.Insert(ds);
      }

      public DataSet GetCashInfo(string sqlWhere, string orderby, int startIndex, int endIndex) 
      {
          return dal.GetCashInfo(sqlWhere, orderby, startIndex, endIndex);
      }

      public int GetCashCount(string sqlWhere) 
      {
          return dal.GetCashCount(sqlWhere);
      }

      public DataTable Update(DataSet ds) 
      {
          return dal.Update(ds);
      }
    }
}

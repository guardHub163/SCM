using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCM.IDAL;
using SCM.DALFactory;

namespace SCM.Bll
{
    public class BTransferIn
    {
        private readonly ITransferIn dal = DataAccess.CreateTransferInManage();

        public BTransferIn()
        { }

        public bool PlanExists(decimal slipNumber) 
        {
            return dal.PlanExists(slipNumber); 
        }

        public DataTable Insert(DataSet ds)
        {
            return dal.Insert(ds);
        }

        public int GetTransferInCount(string sqlWhere) 
        {
            return dal.GetTransferInCount(sqlWhere);
        }

        public DataSet GetTransferInList(string sqlWhere, string orderby, int startIndex, int endIndex) 
        {
            return dal.GetTransferInList(sqlWhere, orderby, startIndex, endIndex);
        }
    }
}

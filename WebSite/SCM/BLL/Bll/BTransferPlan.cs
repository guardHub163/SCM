using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DALFactory;
using System.Data;
using SCM.Model;


namespace SCM.Bll
{
    public class BTransferPlan
    {
        private readonly ITransferInPlan dal = DataAccess.CreateTransferInPlanManage();

        public int GetTransferPlanCount(string sqlWhere) 
        {
            return dal.GetTransferPlanCount(sqlWhere);
        }

        public DataSet GetTransferPlanList(string sqlWhere, string orderby, int startIndex, int endIndex) 
        {
            return dal.GetTransferPlanList(sqlWhere, orderby, startIndex, endIndex);
        }

        public BllTransferInPlanTable GetModel(decimal SLIP_NUMBER)
        {

            return dal.GetModel(SLIP_NUMBER);
        }

        public int GetTransferInfo(BllTransferInPlanTable btable) 
        {
            return dal.GetTransferInfo(btable);
        }
    }
}

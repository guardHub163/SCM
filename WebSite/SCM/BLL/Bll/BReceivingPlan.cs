using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCM.IDAL;
using SCM.DALFactory;
using SCM.Model;

namespace SCM.Bll
{
    public class BReceivingPlan
    {
        private readonly IReceivingPlan dal = DataAccess.CreateReceivingPlanManage();

        /// <summary>
        /// 
        /// </summary>
        public int GetRecordCount(string sqlWhere)
        {
            return dal.GetRecordCount(sqlWhere);
        }

        /// <summary>
        /// 
        /// </summary>
        public DataSet GetReceivingPlanList(string sqlWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetReceivingPlanList(sqlWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        public BllReceivingPlanTable getSearchViewMode(decimal slipNumber) 
        {
            return dal.getSearchViewMode(slipNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Insert(List<BllReceivingPlanTable> list)
        {
            return dal.Insert(list);
        }

        /// <summary>
        /// 
        /// </summary>
         public bool Insert(BllReceiptLineTable rlTable, BllReceivingPlanTable rpTable, List<BllReceiptReturnTable> returnlist, string userId)
        {
             return dal.Insert(rlTable, rpTable, returnlist, userId);
        }

         public int Delete(decimal slipNumber)
         {
             return dal.Delete(slipNumber);
         }
    }
}

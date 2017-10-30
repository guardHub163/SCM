using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DALFactory;
using System.Data;

namespace SCM.Bll
{
    public class BShipmentPlan
    {
        private readonly IShipmentPlan dal = DataAccess.CreateShipmentPlanManage();

        /// <summary>
        /// 
        /// </summary>
        public int CreateShipmentPlan(string trSlipNumber, string userId)
        {
            return dal.CreateShipmentPlan(trSlipNumber, userId);
        }

        /// <summary>
        /// 
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 
        /// </summary>
        public DataSet GetTransferOutPlanList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetTransferOutPlanList(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        public int DeleteShipmentPlan(string trSlipNumber, string userId)
        {
            return dal.DeleteShipmentPlan(trSlipNumber, userId);
        }

        public DataSet GetTransferOutPlanDetail(string sqlWhere)
        {
            return dal.GetTransferOutPlanDetail(sqlWhere);
        }

        public DataSet PrintOutMonad(DateTime fromdate, DateTime todate, string warehousecode)
        {
            return dal.PrintOutMonad(fromdate, todate, warehousecode);
        }

        public DataSet PrintShop(string slipnumber) 
        {
            return dal.PrintShop(slipnumber);
        }
    }
}

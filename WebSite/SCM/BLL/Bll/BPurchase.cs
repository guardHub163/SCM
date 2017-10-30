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
    public class BPurchase
    {
        private readonly IPurchase dal = DataAccess.CreatePurchaseManage();

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
        public DataSet GetPurchaseList(string sqlWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetPurchaseList(sqlWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Insert(BllPurchaseTable purchaseTable)
        {
            return dal.Insert(purchaseTable);
        }
        /// <summary>
        /// 
        /// </summary>
        public int Update(BllPurchaseTable purchaseTable)
        {
            return dal.Update(purchaseTable);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Delete(string slipNumber) 
        {
            return dal.Delete(slipNumber);
        }
        /// <summary>
        /// 
        /// </summary>
        public DataSet GetPurchaseDetail(string slipNumber) 
        {
            return dal.GetPurchaseDetail(slipNumber);
        }

        public int GetPurchaseCount()
        {
            return dal.GetPurchaseCount();
        }

    }//end class
}

using System;
using System.Data;
using System.Collections.Generic;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;

namespace SCM.Bll
{
    public partial class BReceipt
    {
        private readonly IReceipt dal = DataAccess.CreateReceiptManage();
        public BReceipt()
        { }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetReceiptList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetReceiptList(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetReturnCount(string strWhere)
        {
            return dal.GetReturnCount(strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetReturnList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetReturnList(strWhere, orderby, startIndex, endIndex);
        }


        public int Delete(string slipNumber, string userId)
        {
            return dal.Delete(slipNumber, userId);
        }


        public DataSet GetReceiptDetail(string slipNumber)
        {
            return dal.GetReceiptDetail(slipNumber);
        }

        public BllReceiptTable GetReceiptModel(string slipNumber)
        {
            return dal.GetReceiptModel(slipNumber);
        }

        public int Insert(BllReceiptTable receiptTable)
        {
            return dal.Insert(receiptTable);
        }

        public int Update(BllReceiptTable receiptTable)
        {
            return dal.Update(receiptTable);
        }

        public DataSet ReceiptInfo() 
        {
            return dal.ReceiptInfo();
        }

        public DataSet TransferOutInfo() 
        {
            return dal.TransferOutInfo();
        }
    }
}

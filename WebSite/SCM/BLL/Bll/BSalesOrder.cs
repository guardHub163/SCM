using System;
using System.Data;
using System.Collections.Generic;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
using System.Collections;

namespace SCM.Bll
{
    public partial class BSalesOrder
    {
        private readonly ISalesOrder dal = DataAccess.CreateSalesOrderManage();
        public BSalesOrder() { }

        #region 销售
        public int GetSalesOrderCount(string sqlWhere)
        {
            return dal.GetSalesOrderCount(sqlWhere);
        }

        public DataSet GetSalesOrderList(string sqlWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetSalesOrderList(sqlWhere, orderby, startIndex, endIndex);
        }

        public int GetLastSalesOrderCount(string sqlWhere)
        {
            return dal.GetLastSalesOrderCount(sqlWhere);
        }

        public DataSet GetLastSalesOrderList(string sqlWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetLastSalesOrderList(sqlWhere, orderby, startIndex, endIndex);
        }

        public DataTable Insert(DataSet ds) 
        {
            return dal.Insert(ds);
        }

        public DataSet GetSalesStatAmount(string strGroup, string strWhere) 
        {
            return dal.GetSalesStatAmount(strGroup, strWhere);
        }

        public DataSet GetAllOrderInfo(string strWhere) 
        {
            return dal.GetAllOrderInfo(strWhere);
        }

        public DataSet GetSlipNumberInfo(string slipnumber) 
        {
            return dal.GetSlipNumberInfo(slipnumber);
        }

        public int UpdateStock() 
        {
            return dal.UpdateStock();
        }
        #endregion
    }
}

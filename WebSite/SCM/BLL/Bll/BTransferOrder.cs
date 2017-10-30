using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using SCM.IDAL;
using SCM.DALFactory;
using System.Data;

namespace SCM.Bll
{
    public class BTransferOrder
    {
        private readonly ITransferOrder dal = DataAccess.CreateTransferOrderManage();
        /// <summary>
        /// 增加数据
        /// </summary>        
        public int InsertOrder(BllTransferOrderTable orderTable) 
        {
            return dal.InsertOrder(orderTable);
        }

        /// <summary>
        /// 更新Order数据
        /// </summary>
        public int UpdateOrder(BllTransferOrderTable orderTable)
        {
            return dal.UpdateOrder(orderTable);
        }

        /// <summary>
        /// 更新Line数据
        /// </summary>
        public int UpdateLine(List<BllTransferOrderLineTable> list)
        {
            return dal.UpdateLine(list);
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        public int DeleteOrder(string slipNumber)
        {
            return dal.DeleteOrder(slipNumber);
        }

        /// <summary>
        /// 删除明细数据
        /// </summary>
        public int DeleteLine(decimal orderId)
        {
            return dal.DeleteLine(orderId);
        }


        /// <summary>
        /// 配分查询，配分单分组总记录数
        /// </summary>
        public int GetOrderGroupRecordCount(string strWhere)
        {
            return dal.GetOrderGroupRecordCount(strWhere);
        }
        /// <summary>
        /// 配分查询，配分单分组数据获得
        /// </summary>
        public DataSet GetOrderGroupList(string strWhere)
        {
            return dal.GetOrderGroupList(strWhere);
        }

        /// <summary>
        /// 配分查询，配分单分组分页数据获得
        /// </summary>
        public DataSet GetOrderGroupList(string strWhere, string orderby, int startIndex, int endIndex) 
        {
            return dal.GetOrderGroupList(strWhere, orderby, startIndex, endIndex);
        }


        /// <summary>
        /// 获取单张配分单的商品配分记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 出库配分,商品载入数据获得
        /// </summary>
        public DataSet GetTransferOutAssignInfo(string strWhere, string orderby, int startIndex, int endIndex,string departureDate,string warehouseCode)
        {
            return dal.GetTransferOutAssignInfo(strWhere, orderby, startIndex, endIndex, departureDate, warehouseCode);
        }

        /// <summary>
        /// 配分明细,门店载入数据获得
        /// </summary>
        public DataSet GetTransferOutAssignDetailInfo(string slipNumber, string productCode)
        {
            return dal.GetTransferOutAssignDetailInfo(slipNumber, productCode);
        }

       

        /// <summary>
        /// 
        /// </summary>        
        public BllTransferOrderTable GetBllTransferOrderTable(string strWhere)
        {
            return dal.GetBllTransferOrderTable(strWhere);
        }

        
    }
}

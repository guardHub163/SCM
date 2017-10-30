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
    public partial class BStock
    {
        private readonly IStock dal = DataAccess.CreateStockManage();
        public BStock()
		{}
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public bool Exists(string WAREHOUSE_CODE, string PRODUCT_CODE) 
        {
            return dal.Exists(WAREHOUSE_CODE, PRODUCT_CODE);
        }

        public BllStockTable GetModel(string warehouse_code, string product) 
        {
            return dal.GetModel(warehouse_code, product);
        }

        public int Update(BllStockTable stockTable) 
        {
            return dal.Update(stockTable);
        }

        public DataSet Show(string warehouse_code, string product) 
        {
            return dal.Show(warehouse_code, product);
        }

        public int GetStockHistoryCount(string strWhere)
        {
            return dal.GetStockHistoryCount(strWhere);
        }

        public DataSet GetStockInfo(string strWhere) 
        {
            return dal.GetStockInfo(strWhere);
        }

        public DataSet GetStockQuantity(string warehouse, string product) 
        {
            return dal.GetStockQuantity(warehouse, product);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetStockHistoryList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetStockHistoryList(strWhere, orderby, startIndex, endIndex);
        }

                /// <summary>
        /// 获得服装行业的库存
        /// </summary>
        public DataSet GetStockClothingList(string strWhere)
        {
            return dal.GetStockClothingList(strWhere);
        }

        #region 盘点

        public int GetInventoryScheduleRecordCount(string strWhere)
        {
            return dal.GetInventoryScheduleRecordCount(strWhere);
        }

        public DataSet GetInventoryScheduleList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetInventoryScheduleList(strWhere, orderby, startIndex, endIndex);
        }

        public int GetInventoryRecordCount(string strWhere)
        {
            return dal.GetInventoryRecordCount(strWhere);
        }

        public DataSet GetInventoryList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetInventoryList(strWhere, orderby, startIndex, endIndex);
        }

        public int InsertInventory(string warehouseCode, string product_group_code, string userId)
        {
            return dal.InsertInventory(warehouseCode,product_group_code, userId);
        }

        public DataSet GetInventoryScheduleInfo(string strWhere) 
        {
            return dal.GetInventoryScheduleInfo(strWhere);
        }

        public DataSet GetInventoryInfo(string strWhere) 
        {
            return dal.GetInventoryInfo(strWhere); 
        }

        public BaseInventoryScheduleTable GetInventoryScheduleMode(string slipNumber)
        {
            return dal.GetInventoryScheduleMode(slipNumber);
        }

        public DataSet GetInventoryMaxLineNumber(string slipNumber) 
        {
            return dal.GetInventoryMaxLineNumber(slipNumber);
        }

        public int AddInventory(BllInventoryTable model) 
        {
            return dal.AddInventory(model);
        }

        public int UpdateInventory(string slipNumber, Hashtable ht, int statusFlag, string userId)
        {
            return dal.UpdateInventory(slipNumber, ht, statusFlag,userId);
        }

        public int DeleteInventory(string slipNumber)
        {
            return dal.DeleteInventory(slipNumber);
        }
        #endregion        
    
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCM.Model;

namespace SCM.IDAL
{
    public interface IStock
    {
        DataSet GetRecordCount(string strWhere);

        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        BllStockTable GetModel(string warehouse_code, string product);

        int Update(BllStockTable stockTable);

        bool Exists(string WAREHOUSE_CODE, string PRODUCT_CODE);

        DataSet Show(string warehouse_code, string product);

        int GetStockHistoryCount(string strWhere);

        DataSet GetStockHistoryList(string strWhere, string orderby, int startIndex, int endIndex);

        DataSet GetStockQuantity(string warehouse, string product);

        int GetInventoryScheduleRecordCount(string strWhere);

        DataSet GetInventoryScheduleList(string strWhere, string orderby, int startIndex, int endIndex);

        int GetInventoryRecordCount(string strWhere);

        DataSet GetInventoryList(string strWhere, string orderby, int startIndex, int endIndex);

        int InsertInventory(string warehouseCode, string product_group_code, string userId);

        DataSet GetStockInfo(string strWhere);

        DataSet GetInventoryScheduleInfo(string strWhere);

        DataSet GetInventoryMaxLineNumber(string slipNumber);

        DataSet GetInventoryInfo(string strWhere);

        int AddInventory(BllInventoryTable model);

        BaseInventoryScheduleTable GetInventoryScheduleMode(string slipNumber);

        int UpdateInventory(string slipNumber, System.Collections.Hashtable ht, int statusFlag, string userId);

        int DeleteInventory(string slipNumber);

        DataSet GetStockClothingList(string strWhere);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using System.Data;

namespace SCM.IDAL
{
    public interface IPurchaseRequisition
    {
        DataSet GetWarehouseName(string code);

        int GetRecordCount(string strWhere);

        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// 更新一条数据
        /// </summary>
        int Update(BllPurchaseRequisitionTable prTable);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BllPurchaseRequisitionTable GetModel(string slipNumber);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Insert(BllPurchaseRequisitionTable prTable);

        /// <summary>
        /// 删除数据
        /// </summary>
        int Delete(string slipnumber);

        DataSet GetLineList(string slipNumber);

        DataSet GetStockList(string fromWarehouseCode, string toWarehouseCode, string productGroupCode);

        System.Collections.Hashtable GetReferenceInfo(string fromWarehouseCode, string toWarehouseCode, string productGroupCode,string departmentCode);

        DataSet GetMonitorData(string slipNumber);

        int Audit(string slipNumber);

        int Auditing(BllPurchaseRequisitionTable prTable);
    }
}

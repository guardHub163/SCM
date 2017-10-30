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
   public partial class BPurchaseRequisition
    {
       private readonly IPurchaseRequisition dal = DataAccess.CreatePurchaseRequisitionManage();

       public DataSet GetWarehouseName(string code) 
       {
           return dal.GetWarehouseName(code);
       }
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
       public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
       {
           return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
       }

       /// <summary>
       /// 得到一个对象实体
       /// </summary>
       public BllPurchaseRequisitionTable GetModel(string slipNumber)
       {
           return dal.GetModel(slipNumber);
       }

       public int Delete(string slipnumber) 
       {
           return dal.Delete(slipnumber);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       public int Update(BllPurchaseRequisitionTable prTable)
       {
           return dal.Update(prTable);
       }
       /// <summary>
       /// 增加一条数据
       /// </summary>
       public int Insert(BllPurchaseRequisitionTable prTable)
       {
           return dal.Insert(prTable);
       }

       /// <summary>
       /// 修正商品信息的取得
       /// </summary>
       public DataSet GetLineList(string slipNumber)
       {
           return dal.GetLineList(slipNumber);
       }

       /// <summary>
       /// 新增商品信息的取得
       /// </summary>
       public DataSet GetStockList(string fromWarehouseCode, string toWarehouseCode, string productGroupCode)
       {
           return dal.GetStockList(fromWarehouseCode, toWarehouseCode, productGroupCode);
       }

       /// <summary>
       /// 申请参考信息的取得
       /// </summary>
       public Hashtable GetReferenceInfo(string fromWarehouseCode, string toWarehouseCode, string productGroupCode, string departmentCode)
       {
           return dal.GetReferenceInfo(fromWarehouseCode, toWarehouseCode, productGroupCode,departmentCode);
       }

       public DataSet GetMonitorData(string slipNumber)
       {
           return dal.GetMonitorData(slipNumber);
       }

        /// <summary>
        /// 申请审核
        /// </summary>
       public int Audit(string slipNumber) 
       {
           return dal.Audit(slipNumber);
       }

       public int Auditing(BllPurchaseRequisitionTable prTable) 
       {
           return dal.Auditing(prTable);
       }
    }
}

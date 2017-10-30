using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DALFactory;
using System.Data;
using SCM.Model;

namespace SCM.Bll
{
   public partial class BTransferRelation
    {
       private readonly ITransferRelation dal = DataAccess.CreateTransferRelationManage();

       public int GetTranferRelationCount(string strWhere) 
       {
           return dal.GetTranferRelationCount(strWhere);
       }

       public DataSet GetTranferRelationByPage(string strWhere, string orderby, int startIndex, int endIndex) 
       {
           return dal.GetTranferRelationByPage(strWhere, orderby, startIndex, endIndex);
       }

       public int Insert(BllShipmentTable shptable) 
       {
           return dal.Insert(shptable);
       }

       public DataSet GetTransferRelationDetail(string slipNumber) 
       {
           return dal.GetTransferRelationDetail(slipNumber);
       }
    }
}

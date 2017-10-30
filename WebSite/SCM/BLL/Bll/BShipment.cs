using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DALFactory;
using SCM.Model;
using System.Data;

namespace SCM.Bll
{
    public class BShipment
    {
        private readonly IShipment dal = DataAccess.CreateShipmentManage();

        public int Insert(BllShipmentTable shipmentTable)
        {
            return dal.Insert(shipmentTable);
        }

        public int Update(BllShipmentTable shipmentTable)
        {
            return dal.Update(shipmentTable);
        }

        public int Delete(string slipNumber,string userId)
        {
            return dal.Delete(slipNumber,userId);
        }

        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);            
        }

        public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetList(strWhere, orderby, startIndex, endIndex);
        }

        public BllShipmentTable GetShipmentModel(string slipNumber) 
        {
            return dal.GetShipmentModel(slipNumber);
        }

        public DataSet GetShipmentDetail(string slipNumber)
        {
            return dal.GetShipmentDetail(slipNumber);
        }

        public string InsertPlan(BllShipmentTable shipmentTable) 
        {
            return dal.InsertPlan(shipmentTable);
        }
    }
}

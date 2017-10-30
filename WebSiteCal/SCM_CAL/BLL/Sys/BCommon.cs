using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using System.Data;
using SCM.Model;

namespace SCM.Bll
{

    public class BCommon
    {
        ICommon dal = DALFactory.DataAccess.CreateCommonManage();

        public string GetSeqNumber(string blltype)
        {
            return dal.GetSeqNumber(blltype);
        }

        public DataSet GetMasterList(string tableName, string name, string strWhere) 
        {
            return dal.GetMasterList(tableName, name, strWhere);
        }

        public BaseMaster GetBaseMaster(string tableName, string code, string strWhere)
        {
            return dal.GetBaseMaster(tableName, code, strWhere);
        }

        public DataSet GetNames(string codeType) 
        {
            return dal.getNames(codeType);
        }

        public DataSet GetProductList(string strWhere)
        {
            return dal.GetProductList(strWhere);
        }

        public DataSet GetItemList(string name) 
        {
            return dal.GetItemList(name);
        }

        public DataSet GetMenu(string userType) 
        {
            return dal.getMenu(userType);
        }
        public BaseMaster GetCenterWarehouse() 
        {
            return dal.GetCenterWarehouse();
        }

        public DataSet GetExportList(string departmentCode, string tableName, string dateTime)
        {
            return dal.GetExportList(departmentCode, tableName, dateTime);
        }
    }
}

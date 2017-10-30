using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.IDAL;
using System.Data;
using POS.Model;

namespace POS.Bll
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
            return dal.GetNames(codeType);
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
            return dal.GetMenu(userType);
        }

        public string getDepartmentCode()
        {
            return (string)dal.GetNames("DEPARTMENT_CODE").Tables[0].Rows[0]["CODE"];
        }

        public int UpdateNames(NamesTable names)
        {
            return dal.UpdateNames(names);
        }

        public int UpdateNames(List<NamesTable> names) 
        {
            return dal.UpdateNames(names);
        }
        public DataSet GetOldMaxSlipNumber(string blltype, string time)
        {
            return dal.GetOldMaxSlipNumber(blltype, time);
        }

        public DataSet GetPromotion() 
        {
            return dal.GetPromotion();
        }

        public bool IsDBConn() 
        {
            return dal.IsDBConn();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.IDAL
{
    public interface ICommon
    {
        string GetSeqNumber(string blltype);

        System.Data.DataSet GetMasterList(string tableName, string name, string strWhere);

        SCM.Model.BaseMaster GetBaseMaster(string tableName, string code, string strWhere);

        System.Data.DataSet getNames(string codeType);

        System.Data.DataSet GetProductList(string strWhere);

        System.Data.DataSet getMenu(string userType);

        System.Data.DataSet GetItemList(string name);

        SCM.Model.BaseMaster GetCenterWarehouse();

        System.Data.DataSet GetExportList(string departmentCode, string tableName, string dateTime);
    }
}

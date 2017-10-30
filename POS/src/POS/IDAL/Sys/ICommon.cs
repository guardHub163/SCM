using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using System.Data;

namespace POS.IDAL
{
    public interface ICommon
    {
        string GetSeqNumber(string blltype);

        System.Data.DataSet GetMasterList(string tableName, string name, string strWhere);

        POS.Model.BaseMaster GetBaseMaster(string tableName, string code, string strWhere);

        System.Data.DataSet GetNames(string codeType);

        System.Data.DataSet GetProductList(string strWhere);

        System.Data.DataSet GetMenu(string userType);

        System.Data.DataSet GetItemList(string name);

        int UpdateNames(NamesTable names);

        DataSet GetOldMaxSlipNumber(string blltype, string time);

        int UpdateNames(List<NamesTable> names);

        DataSet GetPromotion();

        bool IsDBConn();
    }
}

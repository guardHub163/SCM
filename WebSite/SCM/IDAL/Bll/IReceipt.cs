using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.IDAL
{
    public interface IReceipt
    {
        int GetRecordCount(string sqlWhere);

        System.Data.DataSet GetReceiptList(string sqlWhere, string orderby, int startIndex, int endIndex);

        int Delete(string slipNumber, string userId);

        System.Data.DataSet GetReceiptDetail(string slipNumber);

        SCM.Model.BllReceiptTable GetReceiptModel(string slipNumber);

        int Insert(SCM.Model.BllReceiptTable receiptable);

        int Update(SCM.Model.BllReceiptTable receiptable);

        int GetReturnCount(string sqlWhere);

        System.Data.DataSet ReceiptInfo();

        System.Data.DataSet TransferOutInfo();

        System.Data.DataSet GetReturnList(string sqlWhere, string orderby, int startIndex, int endIndex);

    }
}

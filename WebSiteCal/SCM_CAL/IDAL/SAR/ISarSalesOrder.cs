using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SCM.IDAL
{
    public interface ISarSalesOrder
    {
        DataSet GetSalesStatAmount(string strGroup, string strWhere);
        DataSet GetSalesStatQuantity(string strGroup, string strWhere);
        DataSet GetSalesInfo(string where);
        DataSet GetOneDepartmentAmount(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetDepartmentInfo(string code);
        DataSet GetUserNumber(string code);
        DataSet GetUserInfo(string code);
        DataSet GetDiscountAmount(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetProdeuctAmount(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetVipInfo(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetAmountRanking(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetSlipNumber(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetProductAmountQuantity(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetCountQuantity(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetAllSlipNumbercount(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetSmallSlipNumbercount(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetAddMonthCount();
        DataSet GetMonthInfo();
        int GetMonthtd(DateTime datetime, DateTime totime);
        DataSet GetMothSale(string departmentcode, string datetime);
        DataSet GetMonthQuantity(string departmentcode, string datetime);
        DataSet GetStyleProductInfo(string departmentcode, DateTime startime, DateTime endtime);
        DataSet GetProductStyleCompare(string departmentcode, string productGroupCode, DateTime startime, DateTime endtime);
        DataSet GetProductInfo();
    }
}

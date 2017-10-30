using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
using System.Collections;
using System.Data;

namespace SCM.Bll
{
    public partial class BSarSalesOrder
    {
        private readonly ISarSalesOrder dal = DataAccess.CreateSarSalesOrderManage();
        public BSarSalesOrder() { }

        public DataSet GetSalesStatAmount(string strGroup, string strWhere)
        {
            return dal.GetSalesStatAmount(strGroup, strWhere);
        }
        public DataSet GetSalesInfo(string where)
        {
            return dal.GetSalesInfo(where);
        }
        public DataSet GetSalesStatQuantity(string strGroup, string strWhere)
        {
            return dal.GetSalesStatQuantity(strGroup, strWhere);
        }

        public DataSet GetOneDepartmentAmount(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetOneDepartmentAmount(departmentcode, startime, endtime);
        }

        public DataSet GetDepartmentInfo(string code)
        {
            return dal.GetDepartmentInfo(code);
        }

        public DataSet GetUserNumber(string code)
        {
            return dal.GetUserNumber(code);
        }

        public DataSet GetUserInfo(string code)
        {
            return dal.GetUserInfo(code);
        }

        public DataSet GetDiscountAmount(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetDiscountAmount(departmentcode, startime, endtime);
        }

        public DataSet GetProdeuctAmount(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetProdeuctAmount(departmentcode, startime, endtime);
        }

        public DataSet GetVipInfo(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetVipInfo(departmentcode, startime, endtime);
        }

        public DataSet GetAmountRanking(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetAmountRanking(departmentcode, startime, endtime);
        }

        public DataSet GetSlipNumber(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetSlipNumber(departmentcode, startime, endtime);
        }

        public DataSet GetProductAmountQuantity(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetProductAmountQuantity(departmentcode, startime, endtime);
        }

        public DataSet GetCountQuantity(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetCountQuantity(departmentcode, startime, endtime);
        }

        public DataSet GetAllSlipNumbercount(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetAllSlipNumbercount(departmentcode, startime, endtime);
        }

        public DataSet GetSmallSlipNumbercount(string departmentcode, DateTime startime, DateTime endtime)
        {
            return dal.GetSmallSlipNumbercount(departmentcode, startime, endtime);
        }

        public DataSet GetAddMonthCount()
        {
            return dal.GetAddMonthCount();
        }

        public DataSet GetMonthInfo()
        {
            return dal.GetMonthInfo();
        }

        public int GetMonthtd(DateTime datetime, DateTime totime)
        {
            return dal.GetMonthtd(datetime, totime);
        }

        public DataSet GetMothSale(string departmentcode, string startime)
        {
            return dal.GetMothSale(departmentcode, startime);
        }

        public DataSet GetMonthQuantity(string departmentcode, string datetime) 
        {
            return dal.GetMonthQuantity(departmentcode, datetime);
        }

        public DataSet GetStyleProductInfo(string departmentcode, DateTime startime, DateTime endtime) 
        {
            return dal.GetStyleProductInfo(departmentcode, startime, endtime);
        }

        public DataSet GetProductStyleCompare(string departmentcode, string productGroupCode, DateTime startime, DateTime endtime)
        {
            return dal.GetProductStyleCompare(departmentcode, productGroupCode, startime, endtime);
        }

        public DataSet GetProductInfo()
        {
            return dal.GetProductInfo();
        }

    }
}

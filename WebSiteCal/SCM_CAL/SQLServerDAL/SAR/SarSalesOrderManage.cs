using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Collections;
using SCM.Common;
using System.Data;

namespace SCM.SQLServerDAL
{
    public partial class SarSalesOrderManage : ISarSalesOrder
    {
        public SarSalesOrderManage() { }
        #region

        //根据条件来统计金额
        public DataSet GetSalesStatAmount(string strGroup, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (strGroup.Trim() != "")
            {
                strSql.AppendFormat("select " + strGroup + ",");
            }
            else
            {
                strSql.Append("select ");
            }
            strSql.Append("SUM(AMOUNT) AS AMOUNT FROM sar_sales_order_info_view ");
            if (strWhere.Trim() != "")
            {
                strSql.AppendFormat(" WHERE " + strWhere);
            }
            if (strGroup.Trim() != "")
            {
                strSql.Append(" GROUP BY " + strGroup);
            }
            return DbHelperSQL.Query(strSql.ToString());

        }

        //根据条件统计数量
        public DataSet GetSalesStatQuantity(string strGroup, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (strGroup.Trim() != "")
            {
                strSql.AppendFormat("select " + strGroup + ",");
            }
            else
            {
                strSql.Append("select ");
            }
            strSql.Append("SUM(QUANTITY) AS QUANTITY FROM sar_sales_order_info_view ");
            if (strWhere.Trim() != "")
            {
                strSql.AppendFormat(" WHERE " + strWhere);
            }
            if (strGroup.Trim() != "")
            {
                strSql.Append(" GROUP BY " + strGroup);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询所有销售数据
        public DataSet GetSalesInfo(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from dbo.SAR_SALES_ORDER where " + where);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询某一间门店某一段时间的营业额,客单数，客单价
        public DataSet GetOneDepartmentAmount(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(AMOUNT) AS AMOUNT,sum(QUANTITY) AS QUANTITY FROM dbo.BLL_SALES_ORDER ");
            strSql.AppendFormat("WHERE CREATE_DATE_TIME>='{0}' AND CREATE_DATE_TIME<'{1}'AND DEPARTMENT_CODE='{2}'", startime, endtime, departmentcode);
            return DbHelperSQL.Query(strSql.ToString());
        }
        //查询某一时间段的总客单数
        public DataSet GetAllSlipNumbercount(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(SLIP_NUMBER) as SLIP_NUMBER from ( select SLIP_NUMBER from dbo.BLL_SALES_ORDER ");
            strSql.AppendFormat("WHERE CREATE_DATE_TIME>='{0}' AND CREATE_DATE_TIME<'{1}' AND DEPARTMENT_CODE='{2}'", startime, endtime, departmentcode);
            strSql.AppendFormat(" group by SLIP_NUMBER) as a");
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询某一时间段的总客单数
        public DataSet GetSmallSlipNumbercount(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(SLIP_NUMBER) as SLIP_NUMBER from ( select SLIP_NUMBER from dbo.BLL_SALES_ORDER ");
            strSql.AppendFormat("WHERE CREATE_DATE_TIME>='{0}' AND CREATE_DATE_TIME<'{1}' AND DEPARTMENT_CODE='{2}'AND PRICE<0", startime, endtime, departmentcode);
            strSql.AppendFormat(" group by SLIP_NUMBER) as a");
            return DbHelperSQL.Query(strSql.ToString());
        }


        //查询门店基本信息
        public DataSet GetDepartmentInfo(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM BASE_DEPARTMENT WHERE CODE='{0}'", code);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //获得某一个门店的人数
        public DataSet GetUserNumber(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(*) AS NUMBER from dbo.BASE_USER where DEPARTMENT_CODE='{0}' AND STATUS_FLAG<>'{1}'", code, CConstant.DELETE);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //获得某一个门店的所有人员信息
        public DataSet GetUserInfo(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select TRUE_NAME from dbo.BASE_USER where DEPARTMENT_CODE='{0}' AND STATUS_FLAG<>'{1}'", code, CConstant.DELETE);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //获得某一时间短的折扣的金额(折扣率)
        public DataSet GetDiscountAmount(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(ORI_PRICE) as ORI_PRICE,sum(DISCOUNT_RATE) as DISCOUNT_RATE,sum(PROMOTION_DISCOUNTS) as PROMOTION_DISCOUNTS FROM dbo.BLL_SALES_ORDER ");
            strSql.AppendFormat("WHERE CREATE_DATE_TIME>='{0}' AND CREATE_DATE_TIME<'{1}' AND DEPARTMENT_CODE='{2}'", startime, endtime, departmentcode);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //获得某一时间的某一类商品的销售金额(分类销售占比)
        public DataSet GetProdeuctAmount(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(PRICE) AS PRICE FROM dbo.BLL_SALES_ORDER ");
            strSql.AppendFormat("WHERE CREATE_DATE_TIME>='{0}' AND CREATE_DATE_TIME<'{1}' AND DEPARTMENT_CODE='{2}'AND PRODUCT_CODE='{3}'", startime, endtime, departmentcode, Parameter.PRODUCT_CODE);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //获得某一时间的vip的销售状况
        public DataSet GetVipInfo(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(PRICE) AS PRICE FROM dbo.BLL_SALES_ORDER ");
            strSql.AppendFormat("WHERE CREATE_DATE_TIME>='{0}' AND CREATE_DATE_TIME<'{1}' AND DEPARTMENT_CODE='{2}' AND CUSTOMER_CODE<>''", startime, endtime, departmentcode);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询店员的销售金额以及排名
        public DataSet GetAmountRanking(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Row_Number() OVER (ORDER BY PRICE desc) AMOUNT_SORT , * from(SELECT ");
            strSql.Append("SALES_EMPLOYEE,SUM(PRICE) AS PRICE from dbo.BLL_SALES_ORDER SO ");
            //strSql.Append("left join dbo.BASE_USER BU on BU.USER_ID=SO.SALES_EMPLOYEE ");
            strSql.AppendFormat("where DEPARTMENT_CODE='{0}' and CREATE_DATE_TIME>='{1}' AND CREATE_DATE_TIME<'{2}' ", departmentcode, startime, endtime);
            strSql.Append("GROUP BY SALES_EMPLOYEE) AS AA");
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询店员的销售单数以及排名
        public DataSet GetSlipNumber(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Row_Number() OVER (ORDER BY AA.QUANTITY desc) QUANTITY_SORT , * from( ");
            strSql.Append("select count(SLIP_NUMBER) AS QUANTITY,COUNT(QUANTITY) AS ATV,SALES_EMPLOYEE from dbo.BLL_SALES_ORDER AS SO ");
            strSql.AppendFormat("where DEPARTMENT_CODE='{0}' and CREATE_DATE_TIME>='{1}' AND CREATE_DATE_TIME<'{2}' ", departmentcode, startime, endtime);
            strSql.Append("group by SALES_EMPLOYEE) as AA ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询各种商品的销售金额与数量
        public DataSet GetProductAmountQuantity(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Row_Number() OVER (ORDER BY AA.PRICE desc) NUMBER , * from( ");
            strSql.Append("select  BP.NAME AS PRODUCT_NAME ,count(QUANTITY) AS QUANTITY,sum(PRICE) AS PRICE ");
            strSql.Append("from dbo.BLL_SALES_ORDER BS ");
            strSql.Append("LEFT JOIN dbo.BASE_PRODUCT BP ON BP.CODE=BS.PRODUCT_CODE ");
            strSql.AppendFormat("where BS.DEPARTMENT_CODE='{0}' and BS.CREATE_DATE_TIME>='{1}' AND BS.CREATE_DATE_TIME<'{2}' ", departmentcode, startime, endtime);
            strSql.Append("group by BP.NAME) AS AA");
            return DbHelperSQL.Query(strSql.ToString());
        }
        //统计某一时间的进货数量
        public DataSet GetCountQuantity(string departmentcode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(QUANTITY) AS QUANTITY FROM dbo.BLL_TRANSFER_IN_LINE BRL ");
            strSql.Append("LEFT JOIN dbo.BLL_TRANSFER_IN BR ON BR.SLIP_NUMBER=BRL.SLIP_NUMBER ");
            strSql.Append("LEFT JOIN dbo.BASE_WAREHOUSE BW ON BR.TO_WAREHOUSE_CODE=BW.CODE ");
            strSql.AppendFormat("where BW.DEPARTMENT_CODE='{0}' and BR.CREATE_DATE_TIME>='{1}' AND BR.CREATE_DATE_TIME<'{2}' ", departmentcode, startime, endtime);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //统计12月份中是否有参数录入
        public DataSet GetAddMonthCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) AS COUNT FROM BASE_PARAMETER WHERE STATUS_FLAG=1");
            return DbHelperSQL.Query(strSql.ToString());
        }

        //查询12月份中业绩指标的信息
        public DataSet GetMonthInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT NUMBER2,NUMBER1 FROM BASE_PARAMETER WHERE STATUS_FLAG=1");
            return DbHelperSQL.Query(strSql.ToString());
        }

        //获得两个时间的月份差
        public int GetMonthtd(DateTime datetime, DateTime totime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select DATEDIFF(month,'{0}','{1}')", datetime, totime);
            object jt = DbHelperSQL.GetSingle(strSql.ToString());
            return Convert.ToInt32(jt);
        }

        //根据月份获得销售数量
        public DataSet GetMothSale(string departmentcode, string datetime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select count(QUANTITY) AS QUANTITY from dbo.BLL_SALES_ORDER where DEPARTMENT_CODE='{0}' and convert(varchar(7),CREATE_DATE_TIME,120) ='{1}' ", departmentcode, datetime);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //统计某一月的进货数量
        public DataSet GetMonthQuantity(string departmentcode, string datetime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(QUANTITY) AS QUANTITY FROM dbo.BLL_TRANSFER_IN_LINE BRL ");
            strSql.Append("LEFT JOIN dbo.BLL_TRANSFER_IN BR ON BR.SLIP_NUMBER=BRL.SLIP_NUMBER ");
            strSql.Append("LEFT JOIN dbo.BASE_WAREHOUSE BW ON BR.TO_WAREHOUSE_CODE=BW.CODE ");
            strSql.AppendFormat("where BW.DEPARTMENT_CODE='{0}' and convert(varchar(7),BR.CREATE_DATE_TIME,120)='{1}'", departmentcode, datetime);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //统计所有销售种类的信息
        public DataSet GetStyleProductInfo(string departmentcode, DateTime startime, DateTime endtime) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT P.QUANTITY AS QUANTITY,P.PRICE,P.PRODUCT_GROUP_CODE,S.NAME AS PRODUCT_GROUP_NAME FROM ( ");
            strSql.Append("select SUM( QUANTITY) AS QUANTITY, sum(PRICE) AS PRICE,BPG.CODE AS PRODUCT_GROUP_CODE from dbo.BLL_SALES_ORDER BS ");
            strSql.Append("LEFT JOIN dbo.BASE_PRODUCT BP ON BP.CODE=BS.PRODUCT_CODE ");
            strSql.Append("LEFT JOIN dbo.BASE_PRODUCT_GROUP BPG ON BPG.CODE=BP.GROUP_CODE ");
            strSql.AppendFormat("where BS.DEPARTMENT_CODE='{0}' and BS.CREATE_DATE_TIME>='{1}' AND BS.CREATE_DATE_TIME<'{2}' ", departmentcode, startime, endtime);
            strSql.Append("GROUP BY BPG.CODE) P LEFT JOIN dbo.BASE_PRODUCT_GROUP S ON P.PRODUCT_GROUP_CODE=S.CODE ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region ISarSalesOrder 成员
        public DataSet GetProductStyleCompare(string departmentcode, string productGroupCode, DateTime startime, DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT P.QUANTITY AS QUANTITY,P.PRICE,P.STYLE AS STYLE_CODE,BS.NAME AS STYLE_NAME FROM ( ");
            strSql.Append("select SUM( QUANTITY) AS QUANTITY, sum(PRICE) AS PRICE,BP.STYLE AS STYLE from dbo.BLL_SALES_ORDER BS ");
            strSql.Append("LEFT JOIN dbo.BASE_PRODUCT BP ON BP.CODE=BS.PRODUCT_CODE ");
            strSql.AppendFormat("where BS.DEPARTMENT_CODE='{0}' and BS.CREATE_DATE_TIME>='{1}' and BS.CREATE_DATE_TIME<'{2}' AND BP.GROUP_CODE ='{3}'", departmentcode, startime, endtime, productGroupCode);
            strSql.Append("GROUP BY BP.STYLE) as P LEFT JOIN dbo.BASE_STYLE BS ON P.STYLE =BS.CODE ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion

        //获得商品种类   
        public DataSet GetProductInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM BASE_PRODUCT_GROUP WHERE PARENT_CODE !='' and STATUS_FLAG !='9'");
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}

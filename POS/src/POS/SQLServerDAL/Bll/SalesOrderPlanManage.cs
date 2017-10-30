using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.IDAL;
using POS.Model;
using POS.DBUtility;
using System.Data.SqlClient;
using System.Data;
using POS.Common;

namespace POS.SQLServerDAL
{
    public class SalesOrderPlanManage : ISalesOrderPlan
    {
        public int InsertSales(List<SalesOrderPlanTable> salesList, SalesOrderPlanTable saleplan, decimal bankAmount, decimal cashAmount, string customer_code, string customer_phone)
        {
            StringBuilder strSql;
            SqlParameter[] parames;
            List<CommandInfo> sqlList = new List<CommandInfo>();

            #region 销售数据更新

            strSql = new StringBuilder();
            strSql.Append("insert into BLL_SALES_ORDER(");
            strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,AMOUNT,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER,BANK_AMOUNT,CHANGE,CASH_AMOUNT)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@AMOUNT,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER,@BANK_AMOUNT,@CHANGE,@CASH_AMOUNT)");
            SqlParameter[] parameters = {
					    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
					    new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					    new SqlParameter("@SALES_EMPLOYEE", SqlDbType.VarChar,20),
					    new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
					    new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					    new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					    new SqlParameter("@ATTRIBUTE1", SqlDbType.VarChar,255),
					    new SqlParameter("@ATTRIBUTE2", SqlDbType.VarChar,255),
					    new SqlParameter("@ATTRIBUTE3", SqlDbType.VarChar,255),
					    new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
					    new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					    new SqlParameter("@PRICE", SqlDbType.Decimal,9),
					    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					    new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					    new SqlParameter("@AMOUNT", SqlDbType.Decimal,9),
					    new SqlParameter("@POINTS", SqlDbType.Int,4),
					    new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
					    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					    new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
					    new SqlParameter("@MEMO", SqlDbType.VarChar,255),
					    new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
					    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					    new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
                        new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					    new SqlParameter("@BANK_AMOUNT", SqlDbType.VarChar,20),
                        new SqlParameter("@CHANGE", SqlDbType.VarChar,20),
                        new SqlParameter("@CASH_AMOUNT", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = salesList[0].SALES_ORDER_SLIP_NUMBER;
            parameters[1].Value = salesList[0].DEPARTMENT_CODE;
            parameters[2].Value = salesList[0].SALES_EMPLOYEE;
            parameters[3].Value = customer_code;
            parameters[4].Value = 1;
            parameters[5].Value = Constant.PRODUCT_CODE;
            parameters[6].Value = "";
            parameters[7].Value = "";
            parameters[8].Value = "";
            parameters[9].Value = 0;
            parameters[10].Value = 0;
            parameters[11].Value = 0;
            parameters[12].Value = 1;
            parameters[13].Value = "";
            parameters[14].Value = saleplan.DEPOSIT;
            parameters[15].Value = 0;
            parameters[16].Value = 0;
            parameters[17].Value = salesList[0].STATUS_FLAG;
            parameters[18].Value = salesList[0].SEND_FLAG;
            parameters[19].Value = Constant.SALES_MEMO;
            parameters[20].Value = salesList[0].CREATE_DATE_TIME;
            parameters[21].Value = salesList[0].CREATE_USER;
            parameters[22].Value = salesList[0].LAST_UPDATE_TIME;
            parameters[23].Value = salesList[0].LAST_UPDATE_USER;
            parameters[24].Value = bankAmount;
            parameters[25].Value =0;
            parameters[26].Value = cashAmount;
            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));


            //钱箱更新
            if (cashAmount != 0)
            {
                strSql = new StringBuilder();
                strSql.Append(" update BLL_CASH set PROFIT_CASH=PROFIT_CASH+@PROFIT_CASH,BALANCE_CASH=BALANCE_CASH+@BALANCE_CASH,SALES_SLIP_NUMBER=@SALES_SLIP_NUMBER ");
                strSql.Append(" where status_flag=0 ");
                parames = new SqlParameter[] {
                                            new SqlParameter("@PROFIT_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@BALANCE_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@SALES_SLIP_NUMBER", SqlDbType.VarChar),
                                         };
                parames[0].Value = cashAmount;
                parames[1].Value = cashAmount;
                parames[2].Value = salesList[0].SALES_ORDER_SLIP_NUMBER;
                sqlList.Add(new CommandInfo(strSql.ToString(), parames));
            }

            //销售数据插入
            foreach (SalesOrderPlanTable salesPlanTable in salesList)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_SALES_ORDER_PLAN(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,SALES_ORDER_SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,CUSTOMER_PHONE,END_DATE_TIME,PRODUCT_CODE,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,AMOUNT,DEPOSIT,BALANCE,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@SALES_ORDER_SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@CUSTOMER_PHONE,@END_DATE_TIME,@PRODUCT_CODE,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@AMOUNT,@DEPOSIT,@BALANCE,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_USER,@CREATE_DATE_TIME,@LAST_UPDATE_USER,@LAST_UPDATE_TIME)");
                SqlParameter[] parametersPlan = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@SALES_ORDER_SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SALES_EMPLOYEE", SqlDbType.VarChar,20),
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@CUSTOMER_PHONE", SqlDbType.VarChar,20),
					new SqlParameter("@END_DATE_TIME", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					new SqlParameter("@PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@AMOUNT", SqlDbType.Decimal,9),
					new SqlParameter("@DEPOSIT", SqlDbType.Decimal,9),
					new SqlParameter("@BALANCE", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
					new SqlParameter("@MEMO", SqlDbType.VarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime)};
                parametersPlan[0].Value = salesPlanTable.SLIP_NUMBER;
                parametersPlan[1].Value = salesPlanTable.LINE_NUMBER;
                parametersPlan[2].Value = salesPlanTable.SALES_ORDER_SLIP_NUMBER;
                parametersPlan[3].Value = salesPlanTable.DEPARTMENT_CODE;
                parametersPlan[4].Value = salesPlanTable.SALES_EMPLOYEE;
                parametersPlan[5].Value = customer_code;
                parametersPlan[6].Value = customer_phone; 
                parametersPlan[7].Value = saleplan.END_DATE_TIME;
                parametersPlan[8].Value = salesPlanTable.PRODUCT_CODE;
                parametersPlan[9].Value = salesPlanTable.ORI_PRICE;
                parametersPlan[10].Value = salesPlanTable.DISCOUNT_RATE;
                parametersPlan[11].Value = salesPlanTable.PRICE;
                parametersPlan[12].Value = salesPlanTable.QUANTITY;
                parametersPlan[13].Value = salesPlanTable.UNIT_CODE;
                parametersPlan[14].Value = salesPlanTable.AMOUNT;
                parametersPlan[15].Value = saleplan.DEPOSIT;
                parametersPlan[16].Value = saleplan.AMOUNT - saleplan.DEPOSIT;
                parametersPlan[17].Value = salesPlanTable.STATUS_FLAG;
                parametersPlan[18].Value = salesPlanTable.SEND_FLAG;
                parametersPlan[19].Value ="预定销售"+ saleplan.MEMO;
                parametersPlan[20].Value = salesPlanTable.CREATE_USER;
                parametersPlan[21].Value = salesPlanTable.CREATE_DATE_TIME;
                parametersPlan[22].Value = salesPlanTable.LAST_UPDATE_USER;
                parametersPlan[23].Value = salesPlanTable.LAST_UPDATE_TIME;

                sqlList.Add(new CommandInfo(strSql.ToString(), parametersPlan));
            }

            return DbHelperSQL.ExecuteSqlTran(sqlList); ;
            #endregion
        }

        //获得销售预定的信息
        public DataSet GetSalesOrderPlan(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SR.*,BV.NAME AS CUSTOMER_NAME from ( select SLIP_NUMBER,SUM(AMOUNT) AS AMOUNT,DEPOSIT,CUSTOMER_CODE,BALANCE,CREATE_DATE_TIME, ");
            strSql.Append("CUSTOMER_PHONE,SALES_EMPLOYEE,MEMO,END_DATE_TIME,CASE STATUS_FLAG WHEN 1 THEN '未处理' WHEN 0 THEN '已处理' ELSE '已退订' END AS STATUS_NAME ");
            strSql.Append("from dbo.BLL_SALES_ORDER_PLAN ");
            if (strWhere.Trim() != "")
            {
                strSql.AppendFormat(" where {0}", strWhere);
            }
            strSql.Append("group by SLIP_NUMBER,DEPOSIT,CUSTOMER_CODE,CUSTOMER_PHONE,SALES_EMPLOYEE,MEMO,END_DATE_TIME,STATUS_FLAG,BALANCE,CREATE_DATE_TIME) AS SR ");
            strSql.Append("LEFT JOIN dbo.BASE_VIP_CUSTOMER BV ON BV.CODE=SR.CUSTOMER_CODE");
            return DbHelperSQL.Query(strSql.ToString());
        }


        //根据slipnumber获得商品的信息
        public DataSet GetSalesOrderPlanBySlipNumber(string slipnumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(SOP.ORI_PRICE*SOP.QUANTITY) over(partition by SOP.SLIP_NUMBER) as ORI_AMOUNT,");
            strSql.Append("sum(SOP.PRICE*SOP.QUANTITY) over(partition by SOP.SLIP_NUMBER) as SALES_AMOUNT,");
            strSql.Append("SOP.*,BP.NAME AS PRODUCT_NAME,BS.NAME AS STYLE_NAME from dbo.BLL_SALES_ORDER_PLAN SOP ");
            strSql.Append("left join BASE_PRODUCT BP ON BP.CODE=SOP.PRODUCT_CODE ");
            strSql.Append("left join BASE_STYLE BS ON BS.CODE=BP.STYLE ");
            strSql.AppendFormat("where SOP.SLIP_NUMBER='{0}'", slipnumber);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //预定销售付款操作
        public int InsertSalesOrder(List<SalesOrderTable> salesList, SalesOrderTable salestable,string slipnumber)
        {
            StringBuilder strSql;
            List<CommandInfo> sqlList = new List<CommandInfo>();

            //ORDER表插入数据
            strSql = new StringBuilder();
            strSql.Append("insert into BLL_SALES_ORDER(");
            strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,CASH_AMOUNT,BANK_AMOUNT,AMOUNT,CHANGE,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@CASH_AMOUNT,@BANK_AMOUNT,@AMOUNT,@CHANGE,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER)");
            SqlParameter[] parameters = {
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
                    new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@SALES_EMPLOYEE", SqlDbType.VarChar,20),
                    new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
                    new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@ATTRIBUTE1", SqlDbType.VarChar,255),
                    new SqlParameter("@ATTRIBUTE2", SqlDbType.VarChar,255),
                    new SqlParameter("@ATTRIBUTE3", SqlDbType.VarChar,255),
                    new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
                    new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
                    new SqlParameter("@PRICE", SqlDbType.Decimal,9),
                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@CASH_AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@BANK_AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@CHANGE", SqlDbType.Decimal,9),
                    new SqlParameter("@POINTS", SqlDbType.Int,4),
                    new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@MEMO", SqlDbType.VarChar,255),
                    new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = salestable.SLIP_NUMBER;
            parameters[1].Value = salestable.DEPARTMENT_CODE;
            parameters[2].Value = salestable.SALES_EMPLOYEE;
            parameters[3].Value = salestable.CUSTOMER_CODE;
            parameters[4].Value = salestable.LINE_NUMBER;
            parameters[5].Value = salestable.PRODUCT_CODE;
            parameters[6].Value = salestable.ATTRIBUTE1;
            parameters[7].Value = salestable.ATTRIBUTE2;
            parameters[8].Value = salestable.ATTRIBUTE3;
            parameters[9].Value = salestable.ORI_PRICE;
            parameters[10].Value = salestable.DISCOUNT_RATE;
            parameters[11].Value = salestable.PRICE;
            parameters[12].Value = salestable.QUANTITY;
            parameters[13].Value = salestable.UNIT_CODE;
            parameters[14].Value = salestable.CASH_AMOUNT;
            parameters[15].Value = salestable.BANK_AMOUNT;
            parameters[16].Value = salestable.AMOUNT;
            parameters[17].Value = salestable.CHANGE;
            parameters[18].Value = salestable.POINTS;
            parameters[19].Value = salestable.USED_POINTS;
            parameters[20].Value = salestable.STATUS_FLAG;
            parameters[21].Value = salestable.SEND_FLAG;
            parameters[22].Value = salestable.MEMO;
            parameters[23].Value = salestable.CREATE_DATE_TIME;
            parameters[24].Value = salestable.CREATE_USER;
            parameters[25].Value = salestable.LAST_UPDATE_TIME;
            parameters[26].Value = salestable.LAST_UPDATE_USER;
            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));

            //钱箱跟新
            strSql = new StringBuilder();
            strSql.Append(" update BLL_CASH set PROFIT_CASH=PROFIT_CASH-@PROFIT_CASH,BALANCE_CASH=BALANCE_CASH-@BALANCE_CASH,SALES_SLIP_NUMBER=@SALES_SLIP_NUMBER ");
            strSql.Append(" where status_flag=0 ");
            SqlParameter[] parames = new SqlParameter[] {
                                            new SqlParameter("@PROFIT_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@BALANCE_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@SALES_SLIP_NUMBER", SqlDbType.VarChar),
                                         };
            parames[0].Value = salesList[0].ORI_PRICE-salesList[0].PRICE;
            parames[1].Value = salesList[0].ORI_PRICE - salesList[0].PRICE;
            parames[2].Value = salesList[salesList.Count - 1].SLIP_NUMBER;
            sqlList.Add(new CommandInfo(strSql.ToString(), parames));

            //循环插入商品信息
            foreach (SalesOrderTable salesOrdeTable in salesList)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_SALES_ORDER(");
                strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,CASH_AMOUNT,BANK_AMOUNT,AMOUNT,CHANGE,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@CASH_AMOUNT,@BANK_AMOUNT,@AMOUNT,@CHANGE,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER)");
                SqlParameter[] parametersOrder = {
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
                    new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@SALES_EMPLOYEE", SqlDbType.VarChar,20),
                    new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
                    new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@ATTRIBUTE1", SqlDbType.VarChar,255),
                    new SqlParameter("@ATTRIBUTE2", SqlDbType.VarChar,255),
                    new SqlParameter("@ATTRIBUTE3", SqlDbType.VarChar,255),
                    new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
                    new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
                    new SqlParameter("@PRICE", SqlDbType.Decimal,9),
                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@CASH_AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@BANK_AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@CHANGE", SqlDbType.Decimal,9),
                    new SqlParameter("@POINTS", SqlDbType.Int,4),
                    new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@MEMO", SqlDbType.VarChar,255),
                    new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                parametersOrder[0].Value = salesOrdeTable.SLIP_NUMBER;
                parametersOrder[1].Value = salesOrdeTable.DEPARTMENT_CODE;
                parametersOrder[2].Value = salesOrdeTable.SALES_EMPLOYEE;
                parametersOrder[3].Value = salesOrdeTable.CUSTOMER_CODE;
                parametersOrder[4].Value = salesOrdeTable.LINE_NUMBER;
                parametersOrder[5].Value = salesOrdeTable.PRODUCT_CODE;
                parametersOrder[6].Value = salesOrdeTable.ATTRIBUTE1;
                parametersOrder[7].Value = salesOrdeTable.ATTRIBUTE2;
                parametersOrder[8].Value = salesOrdeTable.ATTRIBUTE3;
                parametersOrder[9].Value = salesOrdeTable.ORI_PRICE;
                parametersOrder[10].Value = salesOrdeTable.DISCOUNT_RATE;
                parametersOrder[11].Value = salesOrdeTable.PRICE;
                parametersOrder[12].Value = salesOrdeTable.QUANTITY;
                parametersOrder[13].Value = salesOrdeTable.UNIT_CODE;
                parametersOrder[14].Value = salesOrdeTable.CASH_AMOUNT;
                parametersOrder[15].Value = salesOrdeTable.BANK_AMOUNT;
                parametersOrder[16].Value = salesOrdeTable.AMOUNT;
                parametersOrder[17].Value = salesOrdeTable.CHANGE;
                parametersOrder[18].Value = salesOrdeTable.POINTS;
                parametersOrder[19].Value = salesOrdeTable.USED_POINTS;
                parametersOrder[20].Value = salesOrdeTable.STATUS_FLAG;
                parametersOrder[21].Value = salesOrdeTable.SEND_FLAG;
                parametersOrder[22].Value = salesOrdeTable.MEMO;
                parametersOrder[23].Value = salesOrdeTable.CREATE_DATE_TIME;
                parametersOrder[24].Value = salesOrdeTable.CREATE_USER;
                parametersOrder[25].Value = salesOrdeTable.LAST_UPDATE_TIME;
                parametersOrder[26].Value = salesOrdeTable.LAST_UPDATE_USER;
                sqlList.Add(new CommandInfo(strSql.ToString(), parametersOrder));
            }

            //付款之后钱箱的跟新
            strSql = new StringBuilder();
            strSql.Append(" update BLL_CASH set PROFIT_CASH=PROFIT_CASH+@PROFIT_CASH,BALANCE_CASH=BALANCE_CASH+@BALANCE_CASH,LAST_UPDATE_TIME=getdate(),SALES_SLIP_NUMBER=@SALES_SLIP_NUMBER ");
            strSql.Append(" where status_flag=0 ");
            SqlParameter[] paramese = new SqlParameter[] {
                                            new SqlParameter("@PROFIT_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@BALANCE_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@SALES_SLIP_NUMBER", SqlDbType.VarChar)
                                         };
            paramese[0].Value = salesList[0].CASH_AMOUNT-salesList[0].CHANGE;
            paramese[1].Value = salesList[0].CASH_AMOUNT - salesList[0].CHANGE;
            paramese[2].Value = salesList[salesList.Count - 1].SLIP_NUMBER;
            sqlList.Add(new CommandInfo(strSql.ToString(), paramese));

            if (salesList[0].CUSTOMER_CODE != "" )
            {
                strSql = new StringBuilder();
                strSql.Append("update BASE_VIP_CUSTOMER set ");
                strSql.Append("LAST_SALES_DATE=getdate(),");
                strSql.Append("POINTS=POINTS+@POINTS,");
                strSql.Append("LAST_UPDATE_TIME=getdate(),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                strSql.Append(" where CODE=@CODE ");
                SqlParameter[] cParameters = {
					        new SqlParameter("@CODE", SqlDbType.VarChar,20),
                            new SqlParameter("@POINTS", SqlDbType.Int,4),
					        new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                cParameters[0].Value = salesList[0].CUSTOMER_CODE;
                cParameters[1].Value = salesList[0].CASH_AMOUNT - salesList[0].CHANGE;
                cParameters[2].Value = salesList[0].LAST_UPDATE_USER;
                sqlList.Add(new CommandInfo(strSql.ToString(), cParameters));
            }

            //plan表状态的跟新
            strSql = new StringBuilder();
            strSql.AppendFormat("update BLL_SALES_ORDER_PLAN set STATUS_FLAG=0 ,MEMO=@MEMO  where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameterPlan = {
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
                        new SqlParameter("@MEMO", SqlDbType.VarChar,50)
                                           };
            parameterPlan[0].Value = slipnumber;
            parameterPlan[1].Value = Constant.SALES_PAY_MEMO;
            sqlList.Add(new CommandInfo(strSql.ToString(), parameterPlan));
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        //预定销售退订操作
        public int SalesOrderReturn(SalesOrderTable saleOrder,string slipnumber,decimal deposit) 
        {
            StringBuilder strSql;
            List<CommandInfo> sqlList = new List<CommandInfo>();

            //退货插入数据
            strSql = new StringBuilder();
            strSql.Append("insert into BLL_SALES_ORDER(");
            strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,CASH_AMOUNT,BANK_AMOUNT,AMOUNT,CHANGE,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@CASH_AMOUNT,@BANK_AMOUNT,@AMOUNT,@CHANGE,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER)");
            SqlParameter[] parameters = {
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
                    new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@SALES_EMPLOYEE", SqlDbType.VarChar,20),
                    new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
                    new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@ATTRIBUTE1", SqlDbType.VarChar,255),
                    new SqlParameter("@ATTRIBUTE2", SqlDbType.VarChar,255),
                    new SqlParameter("@ATTRIBUTE3", SqlDbType.VarChar,255),
                    new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
                    new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
                    new SqlParameter("@PRICE", SqlDbType.Decimal,9),
                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@CASH_AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@BANK_AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@AMOUNT", SqlDbType.Decimal,9),
                    new SqlParameter("@CHANGE", SqlDbType.Decimal,9),
                    new SqlParameter("@POINTS", SqlDbType.Int,4),
                    new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@MEMO", SqlDbType.VarChar,255),
                    new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = saleOrder.SLIP_NUMBER;
            parameters[1].Value = saleOrder.DEPARTMENT_CODE;
            parameters[2].Value = saleOrder.SALES_EMPLOYEE;
            parameters[3].Value = saleOrder.CUSTOMER_CODE;
            parameters[4].Value = saleOrder.LINE_NUMBER;
            parameters[5].Value = saleOrder.PRODUCT_CODE;
            parameters[6].Value = saleOrder.ATTRIBUTE1;
            parameters[7].Value = saleOrder.ATTRIBUTE2;
            parameters[8].Value = saleOrder.ATTRIBUTE3;
            parameters[9].Value = saleOrder.ORI_PRICE;
            parameters[10].Value = saleOrder.DISCOUNT_RATE;
            parameters[11].Value = saleOrder.PRICE;
            parameters[12].Value = saleOrder.QUANTITY;
            parameters[13].Value = saleOrder.UNIT_CODE;
            parameters[14].Value = saleOrder.CASH_AMOUNT;
            parameters[15].Value = saleOrder.BANK_AMOUNT;
            parameters[16].Value = saleOrder.AMOUNT;
            parameters[17].Value = saleOrder.CHANGE;
            parameters[18].Value = saleOrder.POINTS;
            parameters[19].Value = saleOrder.USED_POINTS;
            parameters[20].Value = saleOrder.STATUS_FLAG;
            parameters[21].Value = saleOrder.SEND_FLAG;
            parameters[22].Value = saleOrder.MEMO;
            parameters[23].Value = saleOrder.CREATE_DATE_TIME;
            parameters[24].Value = saleOrder.CREATE_USER;
            parameters[25].Value = saleOrder.LAST_UPDATE_TIME;
            parameters[26].Value = saleOrder.LAST_UPDATE_USER;
            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));

            //钱箱跟新
            strSql = new StringBuilder();
            strSql.Append(" update BLL_CASH set PROFIT_CASH=PROFIT_CASH-@PROFIT_CASH,BALANCE_CASH=BALANCE_CASH-@BALANCE_CASH,SALES_SLIP_NUMBER=@SALES_SLIP_NUMBER ");
            strSql.Append(" where status_flag=0 ");
            SqlParameter[] parames = new SqlParameter[] {
                                            new SqlParameter("@PROFIT_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@BALANCE_CASH", SqlDbType.Decimal),
                                            new SqlParameter("@SALES_SLIP_NUMBER", SqlDbType.VarChar),
                                         };
            parames[0].Value = deposit;
            parames[1].Value = deposit;
            parames[2].Value = saleOrder.SLIP_NUMBER;
            sqlList.Add(new CommandInfo(strSql.ToString(), parames));

            //退订之后的状态跟新
            strSql = new StringBuilder();
            strSql.AppendFormat("update BLL_SALES_ORDER_PLAN set STATUS_FLAG=@STATUS_FLAG,MEMO=@MEMO where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameterPlan = {
                     new SqlParameter("@STATUS_FLAG", SqlDbType.Int,55),
                     new SqlParameter("@MEMO",SqlDbType.VarChar,50),
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50)
                                           };
            parameterPlan[0].Value = Constant.PLAN_FLAG_RETURN;
            parameterPlan[1].Value = saleOrder.MEMO;
            parameterPlan[2].Value = slipnumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), parameterPlan));
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }
    }
}

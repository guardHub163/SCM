using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using POS.IDAL;
using POS.DBUtility;//Please add references
using POS.Model;
using System.Collections;
using System.Collections.Generic;
using POS.Common;

namespace POS.SQLServerDAL
{
    /// <summary>
    /// 数据访问类:SalesOrderManage
    /// </summary>
    public class SalesOrderManage : ISalesOrder
    {
        public SalesOrderManage()
        { }
        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SalesOrderTable GetModel(string SLIP_NUMBER, int LINE_NUMBER)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,CASH_AMOUNT,BANK_AMOUNT,CHANGE,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,AMOUNT,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER from BLL_SALES_ORDER ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER and LINE_NUMBER=@LINE_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4)};
            parameters[0].Value = SLIP_NUMBER;
            parameters[1].Value = LINE_NUMBER;

            SalesOrderTable salesTable = new SalesOrderTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                salesTable.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                salesTable.DEPARTMENT_CODE = ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString();
                salesTable.SALES_EMPLOYEE = ds.Tables[0].Rows[0]["SALES_EMPLOYEE"].ToString();
                salesTable.CUSTOMER_CODE = ds.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["LINE_NUMBER"].ToString() != "")
                {
                    salesTable.LINE_NUMBER = int.Parse(ds.Tables[0].Rows[0]["LINE_NUMBER"].ToString());
                }
                salesTable.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                salesTable.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                salesTable.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                salesTable.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                if (ds.Tables[0].Rows[0]["ORI_PRICE"].ToString() != "")
                {
                    salesTable.ORI_PRICE = decimal.Parse(ds.Tables[0].Rows[0]["ORI_PRICE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString() != "")
                {
                    salesTable.DISCOUNT_RATE = decimal.Parse(ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRICE"].ToString() != "")
                {
                    salesTable.PRICE = decimal.Parse(ds.Tables[0].Rows[0]["PRICE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
                {
                    salesTable.QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["QUANTITY"].ToString());
                }
                salesTable.UNIT_CODE = ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["AMOUNT"].ToString() != "")
                {
                    salesTable.AMOUNT = decimal.Parse(ds.Tables[0].Rows[0]["AMOUNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["POINTS"].ToString() != "")
                {
                    salesTable.POINTS = int.Parse(ds.Tables[0].Rows[0]["POINTS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USED_POINTS"].ToString() != "")
                {
                    salesTable.USED_POINTS = int.Parse(ds.Tables[0].Rows[0]["USED_POINTS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    salesTable.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SEND_FLAG"].ToString() != "")
                {
                    salesTable.SEND_FLAG = int.Parse(ds.Tables[0].Rows[0]["SEND_FLAG"].ToString());
                }
                salesTable.MEMO = ds.Tables[0].Rows[0]["MEMO"].ToString();
                if (ds.Tables[0].Rows[0]["CASH_AMOUNT"].ToString() != "")
                {
                    salesTable.CASH_AMOUNT = decimal.Parse(ds.Tables[0].Rows[0]["CASH_AMOUNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BANK_AMOUNT"].ToString() != "")
                {
                    salesTable.BANK_AMOUNT = decimal.Parse(ds.Tables[0].Rows[0]["BANK_AMOUNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHANGE"].ToString() != "")
                {
                    salesTable.CHANGE = decimal.Parse(ds.Tables[0].Rows[0]["CHANGE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    salesTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                salesTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    salesTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                salesTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                return salesTable;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到最大SLIP_NUMBER
        /// </summary>
        public string GetMaxSlipNumber()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 SLIP_NUMBER  FROM BLL_SALES_ORDER ORDER BY LAST_UPDATE_TIME DESC");
            return DbHelperSQL.GetSingle(strSql.ToString()).ToString();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  FROM bll_sales_order_view ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 销售数据插入
        /// </summary>
        public int InsertSales(List<SalesOrderTable> salesList, string Slipnumber)
        {

            StringBuilder strSql;
            SqlParameter[] parames;
            List<CommandInfo> sqlList = new List<CommandInfo>();
            decimal cashAmount = 0;

            foreach (SalesOrderTable salesTable in salesList)
            {
                strSql = new StringBuilder();
                #region 销售数据更新
                strSql.Append("insert into BLL_SALES_ORDER(");
                strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,CASH_AMOUNT,BANK_AMOUNT,AMOUNT,CHANGE,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER,PROMOTION_DISCOUNTS,PROMOTION_AMOUNT)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@CASH_AMOUNT,@BANK_AMOUNT,@AMOUNT,@CHANGE,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER,@PROMOTION_DISCOUNTS,@PROMOTION_AMOUNT)");
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
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@PROMOTION_DISCOUNTS", SqlDbType.VarChar,20),
                    new SqlParameter("@PROMOTION_AMOUNT", SqlDbType.VarChar,20)
                                            };
                parameters[0].Value = salesTable.SLIP_NUMBER;
                parameters[1].Value = salesTable.DEPARTMENT_CODE;
                parameters[2].Value = salesTable.SALES_EMPLOYEE;
                parameters[3].Value = salesTable.CUSTOMER_CODE;
                parameters[4].Value = salesTable.LINE_NUMBER;
                parameters[5].Value = salesTable.PRODUCT_CODE;
                parameters[6].Value = salesTable.ATTRIBUTE1;
                parameters[7].Value = salesTable.ATTRIBUTE2;
                parameters[8].Value = salesTable.ATTRIBUTE3;
                parameters[9].Value = salesTable.ORI_PRICE;
                parameters[10].Value = salesTable.DISCOUNT_RATE;
                parameters[11].Value = salesTable.PRICE;
                parameters[12].Value = salesTable.QUANTITY;
                parameters[13].Value = salesTable.UNIT_CODE;
                parameters[14].Value = salesTable.CASH_AMOUNT;
                parameters[15].Value = salesTable.BANK_AMOUNT;
                parameters[16].Value = salesTable.AMOUNT;
                parameters[17].Value = salesTable.CHANGE;
                parameters[18].Value = salesTable.POINTS;
                parameters[19].Value = salesTable.USED_POINTS;
                parameters[20].Value = salesTable.STATUS_FLAG;
                parameters[21].Value = salesTable.SEND_FLAG;
                parameters[22].Value = salesTable.MEMO;
                parameters[23].Value = salesTable.CREATE_DATE_TIME;
                parameters[24].Value = salesTable.CREATE_USER;
                parameters[25].Value = salesTable.LAST_UPDATE_TIME;
                parameters[26].Value = salesTable.LAST_UPDATE_USER;
                parameters[27].Value = salesTable.PROMOTION_DISCOUNTS;
                parameters[28].Value = salesTable.PROMOTION_AMOUNT;
                sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
                #endregion

                #region 客户更新
                if (salesTable.CUSTOMER_CODE != "" && (salesTable.POINTS != 0 || salesTable.USED_POINTS != 0))
                {
                    strSql = new StringBuilder();
                    strSql.Append("update BASE_VIP_CUSTOMER set ");
                    strSql.Append("LAST_SALES_DATE=getdate(),");
                    strSql.Append("POINTS=POINTS+@POINTS,");
                    strSql.Append("USED_POINTS=USED_POINTS+@USED_POINTS,");
                    strSql.Append("LAST_UPDATE_TIME=getdate(),");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                    strSql.Append(" where CODE=@CODE ");
                    SqlParameter[] cParameters = {
					        new SqlParameter("@CODE", SqlDbType.VarChar,20),
					        new SqlParameter("@POINTS", SqlDbType.Int,4),
                            new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
					        new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                    cParameters[0].Value = salesTable.CUSTOMER_CODE;
                    cParameters[1].Value = salesTable.POINTS;
                    cParameters[2].Value = salesTable.USED_POINTS;
                    cParameters[3].Value = salesTable.LAST_UPDATE_USER;
                    sqlList.Add(new CommandInfo(strSql.ToString(), cParameters));
                }
                #endregion
                //if (salesTable.QUANTITY > 0)
                //{
                //    cashAmount = salesTable.CASH_AMOUNT - salesTable.CHANGE;
                //}
                //else 
                //{
                //    cashAmount = -salesTable.CASH_AMOUNT;
                //}
                cashAmount = salesTable.CASH_AMOUNT - salesTable.CHANGE;
            }

            #region 钱箱现金更新
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
                parames[2].Value = salesList[salesList.Count - 1].SLIP_NUMBER;
                sqlList.Add(new CommandInfo(strSql.ToString(), parames));
            }
            if (Slipnumber != "")
            {
                //原有的数据更新
                strSql = new StringBuilder();
                strSql.Append("update BLL_SALES_ORDER set STATUS_FLAG=@STATUS_FLAG,MEMO=@MEMO where SLIP_NUMBER=@SLIP_NUMBER");
                SqlParameter[] update ={
                                       new SqlParameter("@STATUS_FLAG",SqlDbType.Int,4),
                                       new SqlParameter("@MEMO",SqlDbType.VarChar,50),
                                       new SqlParameter("@SLIP_NUMBER",SqlDbType.VarChar,50)
                                   };

                update[0].Value = 3;//Constant.SALES_ORDER_RETURN_STATUS_FLAG;
                update[1].Value = "已退货";//Constant.RETURN_MEMO;
                update[2].Value = Slipnumber;
                sqlList.Add(new CommandInfo(strSql.ToString(), update));
            }
            #endregion


            //数据库更新
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        /// <summary>
        /// 挂单
        /// </summary>
        public int InsertTmpSales(List<TmpSalesOrderTable> salesList)
        {
            StringBuilder strSql;
            List<CommandInfo> sqlList = new List<CommandInfo>();
            foreach (TmpSalesOrderTable salesTable in salesList)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into TMP_SALES_ORDER(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,CUSTOMER_CODE,SALES_EMPLOYEE,PRODUCT_CODE,PRODUCT_NAME,STYLE_NAME,COLOR_NAME,SIZE,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,USED_POINTS,AMOUNT,MEMO,MEMO2,CREATE_DATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@CUSTOMER_CODE,@SALES_EMPLOYEE,@PRODUCT_CODE,@PRODUCT_NAME,@STYLE_NAME,@COLOR_NAME,@SIZE,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@USED_POINTS,@AMOUNT,@MEMO,@MEMO2,@CREATE_DATE_TIME)");
                SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SALES_EMPLOYEE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_NAME", SqlDbType.VarChar,50),
					new SqlParameter("@STYLE_NAME", SqlDbType.VarChar,50),
					new SqlParameter("@COLOR_NAME", SqlDbType.VarChar,50),
					new SqlParameter("@SIZE", SqlDbType.VarChar,20),
					new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					new SqlParameter("@PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
					new SqlParameter("@AMOUNT", SqlDbType.Decimal,9),
					new SqlParameter("@MEMO", SqlDbType.VarChar,255),
					new SqlParameter("@MEMO2", SqlDbType.VarChar,255),
					new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime)};
                parameters[0].Value = salesTable.SLIP_NUMBER;
                parameters[1].Value = salesTable.LINE_NUMBER;
                parameters[2].Value = salesTable.CUSTOMER_CODE;
                parameters[3].Value = salesTable.SALES_EMPLOYEE;
                parameters[4].Value = salesTable.PRODUCT_CODE;
                parameters[5].Value = salesTable.PRODUCT_NAME;
                parameters[6].Value = salesTable.STYLE_NAME;
                parameters[7].Value = salesTable.COLOR_NAME;
                parameters[8].Value = salesTable.SIZE;
                parameters[9].Value = salesTable.ORI_PRICE;
                parameters[10].Value = salesTable.DISCOUNT_RATE;
                parameters[11].Value = salesTable.PRICE;
                parameters[12].Value = salesTable.QUANTITY;
                parameters[13].Value = salesTable.USED_POINTS;
                parameters[14].Value = salesTable.AMOUNT;
                parameters[15].Value = salesTable.MEMO;
                parameters[16].Value = salesTable.MEMO2;
                parameters[17].Value = salesTable.CREATE_DATE_TIME;
                sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
            }
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        /// <summary>
        /// 取单
        /// </summary>
        public DataSet GetTmpSales(string slipNumber)
        {
            //订单的取出
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT *  FROM TMP_SALES_ORDER WHERE SLIP_NUMBER = '{0}'", slipNumber);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            //订单的删除
            strSql = new StringBuilder();
            strSql.AppendFormat("DELETE FROM TMP_SALES_ORDER WHERE SLIP_NUMBER = '{0}'", slipNumber);
            DbHelperSQL.ExecuteSql(strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 挂单一览
        /// </summary>
        public DataSet GetTmpSalesGroup(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SLIP_NUMBER, SALES_EMPLOYEE,CUSTOMER_CODE,CREATE_DATE_TIME,MEMO2 FROM TMP_SALES_ORDER  GROUP BY SLIP_NUMBER, SALES_EMPLOYEE,CUSTOMER_CODE,CREATE_DATE_TIME,MEMO2 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 统计挂单数量
        /// </summary>
        public int GetTmpSalesCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM TMP_SALES_ORDER GROUP BY SLIP_NUMBER");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public DataSet GetSalesInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM BLL_SALES_ORDER");
            strSql.AppendFormat(" where 1=1 AND {0} ", strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool UpdateFlge(int send_flag, string slip_number,string line_number)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE BLL_SALES_ORDER SET SEND_FLAG=@SEND_FLAG WHERE SLIP_NUMBER=@SLIP_NUMBER AND LINE_NUMBER=@LINE_NUMBER");
            SqlParameter[] parameters = {
                    new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.NVarChar,225),
                    new SqlParameter("@LINE_NUMBER", SqlDbType.NVarChar,225)
                                        };
            parameters[0].Value = send_flag;
            parameters[1].Value = slip_number;
            parameters[2].Value = line_number;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion  Method

        #region ISalesOrder 成员


        public DataSet GetPrintList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *  FROM bll_sales_order_print_view ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion

        //根据条件来统计金额
        public DataSet GetSalesStatAmount(string strGroup, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (strGroup.Trim() != "")
            {
                strSql.AppendFormat("SELECT " + strGroup);
            }
            strSql.Append(",SUM(BS.AMOUNT) AS AMOUNT,SUM(QUANTITY) AS QUATITY FROM ");
            strSql.Append("(select BSO.SALES_EMPLOYEE,Convert(char(10),BSO.CREATE_DATE_TIME,126) as CREATE_DATE_TIME,AMOUNT, ");
            strSql.Append("QUANTITY,BP.STYLE AS PRODUCT_STYLE from dbo.BLL_SALES_ORDER BSO ");
            strSql.Append("left join dbo.BASE_PRODUCT BP ON BP.CODE=BSO.PRODUCT_CODE) AS BS ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("WHERE " + strWhere);
            }
            if (strGroup.Trim() != "")
            {
                strSql.Append(" GROUP BY " + strGroup);
            }
            return DbHelperSQL.Query(strSql.ToString());

        }
        //统计所有金额
        public int GetAllSalesStatAmount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(AMOUNT) as AMOUNT from dbo.BLL_SALES_ORDER ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("WHERE " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        //统计所有的信息
        public DataSet GetAllSalesInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BS.SALES_EMPLOYEE,BS.PRODUCT_STYLE,BS.CREATE_DATE_TIME,");
            strSql.Append("SUM(BS.AMOUNT) AS AMOUNT,SUM(QUANTITY) AS QUATITY FROM ");
            strSql.Append("(select BSO.SALES_EMPLOYEE,BSO.CREATE_DATE_TIME,AMOUNT, ");
            strSql.Append("QUANTITY,BP.STYLE AS PRODUCT_STYLE from dbo.BLL_SALES_ORDER BSO ");
            strSql.Append("left join dbo.BASE_PRODUCT BP ON BP.CODE=BSO.PRODUCT_CODE) AS BS ");
            if (strWhere.Trim() != "")
            {
                strSql.AppendFormat("where " + strWhere);
            }
            strSql.Append("GROUP BY BS.SALES_EMPLOYEE,BS.PRODUCT_STYLE,BS.CREATE_DATE_TIME");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetSaleOrderInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BS.* FROM(SELECT BSO.*,BP.STYLE AS PRODUCT_STYLE  FROM BLL_SALES_ORDER AS BSO LEFT JOIN BASE_PRODUCT AS BP ON BSO.PRODUCT_CODE=BP.CODE) AS BS");
            strSql.AppendFormat(" where " + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetSumAmount(string slipnumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT SUM(AMOUNT) FROM bll_sales_order_view WHERE SLIP_NUMBER='" + slipnumber + "'");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        #region ISalesOrder 成员

        //退货数据的更新
        public int InsertSales(List<SalesOrderTable> returnDatalist, SalesOrderTable salesData, string slipNumber)
        {
            StringBuilder strSql;
            SqlParameter[] parames;
            List<CommandInfo> sqlList = new List<CommandInfo>();
            decimal cashAmount = 0;

            #region 退货数据更新

            foreach (SalesOrderTable returnSalesTable in returnDatalist)
            {
                strSql = new StringBuilder();
                #region 数据更新
                strSql.Append("insert into BLL_SALES_ORDER(");
                strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,CASH_AMOUNT,BANK_AMOUNT,AMOUNT,CHANGE,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER,PROMOTION_DISCOUNTS,PROMOTION_AMOUNT)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@CASH_AMOUNT,@BANK_AMOUNT,@AMOUNT,@CHANGE,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER,@PROMOTION_DISCOUNTS,@PROMOTION_AMOUNT)");
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
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@PROMOTION_DISCOUNTS", SqlDbType.VarChar,20),
                    new SqlParameter("@PROMOTION_AMOUNT", SqlDbType.VarChar,20)
                                            };
                parameters[0].Value = returnSalesTable.SLIP_NUMBER;
                parameters[1].Value = returnSalesTable.DEPARTMENT_CODE;
                parameters[2].Value = returnSalesTable.SALES_EMPLOYEE;
                parameters[3].Value = returnSalesTable.CUSTOMER_CODE;
                parameters[4].Value = returnSalesTable.LINE_NUMBER;
                parameters[5].Value = returnSalesTable.PRODUCT_CODE;
                parameters[6].Value = returnSalesTable.ATTRIBUTE1;
                parameters[7].Value = returnSalesTable.ATTRIBUTE2;
                parameters[8].Value = returnSalesTable.ATTRIBUTE3;
                parameters[9].Value = returnSalesTable.ORI_PRICE;
                parameters[10].Value = returnSalesTable.DISCOUNT_RATE;
                parameters[11].Value = returnSalesTable.PRICE;
                parameters[12].Value = returnSalesTable.QUANTITY;
                parameters[13].Value = returnSalesTable.UNIT_CODE;
                parameters[14].Value = returnSalesTable.CASH_AMOUNT;
                parameters[15].Value = returnSalesTable.BANK_AMOUNT;
                parameters[16].Value = returnSalesTable.AMOUNT;
                parameters[17].Value = returnSalesTable.CHANGE;
                parameters[18].Value = returnSalesTable.POINTS;
                parameters[19].Value = returnSalesTable.USED_POINTS;
                parameters[20].Value = returnSalesTable.STATUS_FLAG;
                parameters[21].Value = returnSalesTable.SEND_FLAG;
                parameters[22].Value = returnSalesTable.MEMO;
                parameters[23].Value = returnSalesTable.CREATE_DATE_TIME;
                parameters[24].Value = returnSalesTable.CREATE_USER;
                parameters[25].Value = returnSalesTable.LAST_UPDATE_TIME;
                parameters[26].Value = returnSalesTable.LAST_UPDATE_USER;
                parameters[27].Value = returnSalesTable.PROMOTION_DISCOUNTS;
                parameters[28].Value = returnSalesTable.PROMOTION_AMOUNT;
                sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
                #endregion

                #region 客户更新
                if (returnSalesTable.CUSTOMER_CODE != "" && (returnSalesTable.POINTS != 0 || returnSalesTable.USED_POINTS != 0))
                {
                    strSql = new StringBuilder();
                    strSql.Append("update BASE_VIP_CUSTOMER set ");
                    strSql.Append("LAST_SALES_DATE=getdate(),");
                    strSql.Append("POINTS=POINTS+@POINTS,");
                    strSql.Append("USED_POINTS=USED_POINTS+@USED_POINTS,");
                    strSql.Append("LAST_UPDATE_TIME=getdate(),");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                    strSql.Append(" where CODE=@CODE ");
                    SqlParameter[] cParameters = {
					        new SqlParameter("@CODE", SqlDbType.VarChar,20),
					        new SqlParameter("@POINTS", SqlDbType.Int,4),
                            new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
					        new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                    cParameters[0].Value = returnSalesTable.CUSTOMER_CODE;
                    cParameters[1].Value = returnSalesTable.POINTS;
                    cParameters[2].Value = returnSalesTable.USED_POINTS;
                    cParameters[3].Value = returnSalesTable.LAST_UPDATE_USER;
                    sqlList.Add(new CommandInfo(strSql.ToString(), cParameters));
                }
                #endregion

                cashAmount = returnSalesTable.CASH_AMOUNT - returnSalesTable.CHANGE;
            }

            if (slipNumber != "")
            {
                //原有的数据更新
                strSql = new StringBuilder();
                strSql.Append("update BLL_SALES_ORDER set STATUS_FLAG=@STATUS_FLAG,MEMO=@MEMO where SLIP_NUMBER=@SLIP_NUMBER");
                SqlParameter[] update ={
                                       new SqlParameter("@STATUS_FLAG",SqlDbType.Int,4),
                                       new SqlParameter("@MEMO",SqlDbType.VarChar,50),
                                       new SqlParameter("@SLIP_NUMBER",SqlDbType.VarChar,50)
                                   };

                update[0].Value = 3;//Constant.SALES_ORDER_RETURN_STATUS_FLAG;
                update[1].Value = "已退货";//Constant.RETURN_MEMO;
                update[2].Value = slipNumber;
                sqlList.Add(new CommandInfo(strSql.ToString(), update));
            }

            #endregion

            #region 销售数据更新

            strSql = new StringBuilder();
            #region
            strSql.Append("insert into BLL_SALES_ORDER(");
            strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,CASH_AMOUNT,BANK_AMOUNT,AMOUNT,CHANGE,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER,PROMOTION_DISCOUNTS,PROMOTION_AMOUNT)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@CASH_AMOUNT,@BANK_AMOUNT,@AMOUNT,@CHANGE,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER,@PROMOTION_DISCOUNTS,@PROMOTION_AMOUNT)");
            SqlParameter[] parametersale = {
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
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@PROMOTION_DISCOUNTS", SqlDbType.VarChar,20),
                    new SqlParameter("@PROMOTION_AMOUNT", SqlDbType.VarChar,20)
                                            };
            parametersale[0].Value = salesData.SLIP_NUMBER;
            parametersale[1].Value = salesData.DEPARTMENT_CODE;
            parametersale[2].Value = salesData.SALES_EMPLOYEE;
            parametersale[3].Value = salesData.CUSTOMER_CODE;
            parametersale[4].Value = salesData.LINE_NUMBER;
            parametersale[5].Value = salesData.PRODUCT_CODE;
            parametersale[6].Value = salesData.ATTRIBUTE1;
            parametersale[7].Value = salesData.ATTRIBUTE2;
            parametersale[8].Value = salesData.ATTRIBUTE3;
            parametersale[9].Value = salesData.ORI_PRICE;
            parametersale[10].Value = salesData.DISCOUNT_RATE;
            parametersale[11].Value = salesData.PRICE;
            parametersale[12].Value = salesData.QUANTITY;
            parametersale[13].Value = salesData.UNIT_CODE;
            parametersale[14].Value = salesData.CASH_AMOUNT;
            parametersale[15].Value = salesData.BANK_AMOUNT;
            parametersale[16].Value = salesData.AMOUNT;
            parametersale[17].Value = salesData.CHANGE;
            parametersale[18].Value = salesData.POINTS;
            parametersale[19].Value = salesData.USED_POINTS;
            parametersale[20].Value = salesData.STATUS_FLAG;
            parametersale[21].Value = salesData.SEND_FLAG;
            parametersale[22].Value = salesData.MEMO;
            parametersale[23].Value = salesData.CREATE_DATE_TIME;
            parametersale[24].Value = salesData.CREATE_USER;
            parametersale[25].Value = salesData.LAST_UPDATE_TIME;
            parametersale[26].Value = salesData.LAST_UPDATE_USER;
            parametersale[27].Value = salesData.PROMOTION_DISCOUNTS;
            parametersale[28].Value = salesData.PROMOTION_AMOUNT;
            sqlList.Add(new CommandInfo(strSql.ToString(), parametersale));
            #endregion

            #region 客户更新
            if (salesData.CUSTOMER_CODE != "" && (salesData.POINTS != 0 || salesData.USED_POINTS != 0))
            {
                strSql = new StringBuilder();
                strSql.Append("update BASE_VIP_CUSTOMER set ");
                strSql.Append("LAST_SALES_DATE=getdate(),");
                strSql.Append("POINTS=POINTS+@POINTS,");
                strSql.Append("USED_POINTS=USED_POINTS+@USED_POINTS,");
                strSql.Append("LAST_UPDATE_TIME=getdate(),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                strSql.Append(" where CODE=@CODE ");
                SqlParameter[] cParameters = {
					        new SqlParameter("@CODE", SqlDbType.VarChar,20),
					        new SqlParameter("@POINTS", SqlDbType.Int,4),
                            new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
					        new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                cParameters[0].Value = salesData.CUSTOMER_CODE;
                cParameters[1].Value = salesData.POINTS;
                cParameters[2].Value = salesData.USED_POINTS;
                cParameters[3].Value = salesData.LAST_UPDATE_USER;
                sqlList.Add(new CommandInfo(strSql.ToString(), cParameters));
            }
            #endregion

            cashAmount = cashAmount + (salesData.CASH_AMOUNT - salesData.CHANGE);

            #endregion

            #region 钱箱现金更新
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
                parames[2].Value = returnDatalist[returnDatalist.Count - 1].SLIP_NUMBER;
                sqlList.Add(new CommandInfo(strSql.ToString(), parames));
            }

            #endregion


            //数据库更新
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        #endregion
    }
}


using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Collections;
using System.Collections.Generic;
using SCM.Common;


namespace SCM.SQLServerDAL
{
    public partial class SalesOrderManage : ISalesOrder
    {
        public SalesOrderManage() { }

        #region ISalesOrder 成员


        public int GetLastSalesOrderCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_sales_order_view ");
            if (sqlWhere.Trim() != "")
            {
                strSql.Append(" where " + sqlWhere);
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

        public DataSet GetLastSalesOrderList(string sqlWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.CREATE_DATE_TIME desc");
            }
            strSql.Append(")AS Row, T.*  from bll_sales_order_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetSalesOrderCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_sales_order_view ");
            if (sqlWhere.Trim() != "")
            {
                strSql.Append(" where " + sqlWhere);
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

        public DataSet GetSalesOrderList(string sqlWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.SLIP_NUMBER desc");
            }
            strSql.Append(")AS Row, T.*  from bll_sales_order_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetAllOrderInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM bll_sales_order_view WHERE" + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool Exists(string slipNumber, int lineNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BLL_SALES_ORDER");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER and LINE_NUMBER=@LINE_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4)			};
            parameters[0].Value = slipNumber;
            parameters[1].Value = lineNumber;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public DataTable Insert(DataSet ds)
        {
            DataTable dt = CommonManage.GetReturnDataTable();
            List<CommandInfo> list = null;
            StringBuilder strSql = null;
            DataRow dr = null;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int rows = 0;
                list = new List<CommandInfo>();
                strSql = new StringBuilder();
                dr = dt.NewRow();
                dr["SLIP_NUMBER"] = row["SLIP_NUMBER"];
                dr["LINE_NUMBER"] = row["LINE_NUMBER"];
                //订单是否重复导入
                try
                {
                    if (Exists(Convert.ToString(row["SLIP_NUMBER"]), Convert.ToInt32(row["LINE_NUMBER"])))
                    {
                        dr["STATUS"] = CConstant.SUCCESS;
                        dt.Rows.Add(dr);
                        continue;
                    }
                }
                catch
                {
                    dr["STATUS"] = CConstant.ERROR;
                    dt.Rows.Add(dr);
                    continue;
                }
                //销售订单的插入
                strSql.Append("insert into BLL_SALES_ORDER(");
                strSql.Append("SLIP_NUMBER,DEPARTMENT_CODE,SALES_EMPLOYEE,CUSTOMER_CODE,LINE_NUMBER,PRODUCT_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,ORI_PRICE,DISCOUNT_RATE,PRICE,QUANTITY,UNIT_CODE,AMOUNT,POINTS,USED_POINTS,STATUS_FLAG,SEND_FLAG,MEMO,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER,PROMOTION_AMOUNT,PROMOTION_DISCOUNTS)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@DEPARTMENT_CODE,@SALES_EMPLOYEE,@CUSTOMER_CODE,@LINE_NUMBER,@PRODUCT_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@ORI_PRICE,@DISCOUNT_RATE,@PRICE,@QUANTITY,@UNIT_CODE,@AMOUNT,@POINTS,@USED_POINTS,@STATUS_FLAG,@SEND_FLAG,@MEMO,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER,@PROMOTION_AMOUNT,@PROMOTION_DISCOUNTS)");
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
                    new SqlParameter("@PROMOTION_AMOUNT", SqlDbType.Decimal,20),
                    new SqlParameter("@PROMOTION_DISCOUNTS", SqlDbType.Decimal,20)
                                            };
                parameters[0].Value = row["SLIP_NUMBER"];
                parameters[1].Value = row["DEPARTMENT_CODE"];
                parameters[2].Value = row["SALES_EMPLOYEE"];
                parameters[3].Value = row["CUSTOMER_CODE"];
                parameters[4].Value = row["LINE_NUMBER"];
                parameters[5].Value = row["PRODUCT_CODE"];
                parameters[6].Value = row["ATTRIBUTE1"];
                parameters[7].Value = row["ATTRIBUTE2"];
                parameters[8].Value = row["ATTRIBUTE3"];
                parameters[9].Value = row["ORI_PRICE"];
                parameters[10].Value = row["DISCOUNT_RATE"];
                parameters[11].Value = row["PRICE"];
                parameters[12].Value = row["QUANTITY"];
                parameters[13].Value = row["UNIT_CODE"];
                parameters[14].Value = row["AMOUNT"];
                parameters[15].Value = row["POINTS"];
                parameters[16].Value = row["USED_POINTS"];
                parameters[17].Value = row["STATUS_FLAG"];
                parameters[18].Value = row["SEND_FLAG"];
                parameters[19].Value = row["MEMO"];
                parameters[20].Value = row["CREATE_DATE_TIME"];
                parameters[21].Value = row["CREATE_USER"];
                parameters[22].Value = row["LAST_UPDATE_TIME"];
                parameters[23].Value = row["LAST_UPDATE_USER"];
                parameters[24].Value = 0;
                parameters[25].Value = 0;
                list.Add(new CommandInfo(strSql.ToString(), parameters));

                //部门仓库的取得
                BaseWarehouseTable warehouseTable = new WarehouseManage().GetModelByDepartment(row["DEPARTMENT_CODE"].ToString());
                if (warehouseTable != null)
                {
                    //库存的更新
                    if (new StockManage().Exists(warehouseTable.CODE, Convert.ToString(row["PRODUCT_CODE"])))
                    {
                        strSql = new StringBuilder();
                        strSql.Append("update BASE_STOCK set ");
                        strSql.Append("QUANTITY=QUANTITY-@QUANTITY,");
                        strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                        strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                        strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                        SqlParameter[] stockParameters = {
					        new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					        new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					        new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					        new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                        stockParameters[0].Value = row["QUANTITY"];
                        stockParameters[1].Value = row["LAST_UPDATE_USER"];
                        stockParameters[2].Value = warehouseTable.CODE;
                        stockParameters[3].Value = row["PRODUCT_CODE"];
                        list.Add(new CommandInfo(strSql.ToString(), stockParameters));
                    }
                    else
                    {
                        strSql = new StringBuilder();
                        strSql.Append("insert into BASE_STOCK (");
                        strSql.Append("WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                        strSql.Append(" values(");
                        strSql.Append("@WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,0-@QUANTITY,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
                        SqlParameter[] stockParamenters = {
                            new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
                            new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
                            new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
                            new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                            new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					        new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					        new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                            new SqlParameter("@CREATE_USER",SqlDbType.NVarChar,255),
                            new SqlParameter("@LAST_UPDATE_USER",SqlDbType.NVarChar,255)};
                        stockParamenters[0].Value = warehouseTable.CODE;
                        stockParamenters[1].Value = row["PRODUCT_CODE"];
                        stockParamenters[2].Value = row["UNIT_CODE"];
                        stockParamenters[3].Value = row["QUANTITY"];
                        stockParamenters[4].Value = "";
                        stockParamenters[5].Value = "";
                        stockParamenters[6].Value = "";
                        stockParamenters[7].Value = row["LAST_UPDATE_USER"];
                        stockParamenters[8].Value = row["LAST_UPDATE_USER"];
                        list.Add(new CommandInfo(strSql.ToString(), stockParamenters));
                    }


                    if (row["CUSTOMER_CODE"].ToString() != "")
                    {
                        if (CustomerExists(row["CUSTOMER_CODE"].ToString()))
                        {
                            //客户积分的更新
                            strSql = new StringBuilder();
                            strSql.Append("update BASE_VIP_CUSTOMER set ");
                            strSql.Append("POINTS=POINTS+@POINTS,");
                            strSql.Append("USED_POINTS=USED_POINTS+@USED_POINTS,");
                            strSql.Append("LAST_SALES_DATE=@LAST_SALES_DATE,");
                            strSql.Append("LAST_UPDATE_TIME=GETDATE(),");
                            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                            strSql.Append(" where CODE=@CODE ");
                            SqlParameter[] customerParameters = {
					         new SqlParameter("@POINTS", SqlDbType.Int,4),
					         new SqlParameter("@USED_POINTS", SqlDbType.Int,4),
                             new SqlParameter("@LAST_SALES_DATE", SqlDbType.DateTime),
					         new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					         new SqlParameter("@CODE", SqlDbType.VarChar,20)};
                            customerParameters[0].Value = row["POINTS"];
                            customerParameters[1].Value = row["USED_POINTS"];
                            customerParameters[2].Value = row["CREATE_DATE_TIME"];
                            customerParameters[3].Value = row["LAST_UPDATE_USER"];
                            customerParameters[4].Value = row["CUSTOMER_CODE"];
                            list.Add(new CommandInfo(strSql.ToString(), customerParameters));
                        }
                        else
                        {
                            dr["STATUS"] = CConstant.ERROR;
                            dt.Rows.Add(dr);
                            continue;
                        }
                    }
                }

                try
                {
                    rows = DbHelperSQL.ExecuteSqlTran(list);
                }
                catch (Exception e) { }

                if (rows > 0)
                {
                    dr["STATUS"] = CConstant.SUCCESS;
                }
                else
                {
                    dr["STATUS"] = CConstant.ERROR;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #region
        public bool CustomerExists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_VIP_CUSTOMER");
            strSql.Append(" where CODE=@CODE AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        //根据条件来统计金额
        public DataSet GetSalesStatAmount(string strGroup, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (strGroup.Trim() != "")
            {
                strSql.AppendFormat("SELECT " + strGroup);
            }
            strSql.Append(",SUM(SA.AMOUNT) AS AMOUNT,SUM(QUANTITY) AS QUANTITY FROM ");
            strSql.Append("(select BSO.SALES_EMPLOYEE,BU.TRUE_NAME AS SALES_NAME,Convert(char(10),BSO.CREATE_DATE_TIME,126) as CREATE_DATE_TIME, ");
            strSql.Append("AMOUNT,BP.SIZE AS SIZE,BS.NAME AS SIZE_NAME,BP.COLOR AS COLOR, BC.NAME AS COLOR_NAME,");
            strSql.Append("BP.NAME AS PRODUCT_NAME,BP.GROUP_CODE AS PRODUCT_GROUP_CODE,BG.NAME AS GROUP_NAME,");
            strSql.Append("BSO.DEPARTMENT_CODE, BD.NAME AS DEPARTMENT_NAME,QUANTITY,BP.STYLE AS PRODUCT_STYLE,BT.NAME AS STYLE_NAME ");
            strSql.Append("from dbo.BLL_SALES_ORDER BSO ");
            strSql.Append("left join dbo.BASE_PRODUCT BP ON BP.CODE=BSO.PRODUCT_CODE  ");
            strSql.Append("left join dbo.BASE_DEPARTMENT BD ON BD.CODE=BSO.DEPARTMENT_CODE ");
            strSql.Append("LEFT JOIN dbo.BASE_COLOR BC ON BC.CODE=BP.COLOR ");
            strSql.Append("LEFT JOIN dbo.BASE_PRODUCT_GROUP BG ON BG.CODE=BP.GROUP_CODE ");
            strSql.Append("LEFT JOIN dbo.BASE_USER BU ON BU.USER_ID=BSO.SALES_EMPLOYEE ");
            strSql.Append("left join dbo.BASE_SIZE BS ON BS.PRODUCT_GROUP_CODE=BG.CODE AND BP.SIZE=BS.CODE ");
            strSql.Append("LEFT JOIN dbo.BASE_STYLE BT ON BT.CODE=BP.STYLE) AS SA ");
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

        //统计积分，金额，数量
        public DataSet GetSlipNumberInfo(string slipnumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SUM(QUANTITY) AS QUANTITY,SUM(AMOUNT) AS AMOUNT,SUM(POINTS) AS POINTS FROM bll_sales_order_view");
            strSql.AppendFormat(" WHERE SLIP_NUMBER='" + slipnumber + "'");
            return DbHelperSQL.Query(strSql.ToString());
        }


        #region //库存有问题修改用的,程序里面不起任何作用
        public DataSet Gestock1()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT (A.QUANTITY-isnull (B.QUANTITY,0)) AS QUANTITY,A.PRODUCT_CODE,A.TO_WAREHOUSE_CODE from ");
            strSql.Append("(SELECT SUM(BI.QUANTITY) AS QUANTITY,BI.PRODUCT_CODE,BT.TO_WAREHOUSE_CODE FROM dbo.BLL_TRANSFER_IN BT ");
            strSql.Append("LEFT JOIN dbo.BLL_TRANSFER_IN_LINE BI ON BT.SLIP_NUMBER=BI.SLIP_NUMBER ");
            strSql.Append("GROUP BY PRODUCT_CODE,TO_WAREHOUSE_CODE) AS A ");
            strSql.Append("LEFT JOIN (SELECT SUM(QUANTITY) AS QUANTITY,PRODUCT_CODE ,BW.CODE FROM dbo.BLL_SALES_ORDER BS ");
            strSql.Append("LEFT JOIN dbo.BASE_WAREHOUSE BW ON BW.DEPARTMENT_CODE=BS.DEPARTMENT_CODE ");
            strSql.Append("GROUP BY PRODUCT_CODE,CODE) AS B ");
            strSql.Append("ON A.PRODUCT_CODE=B.PRODUCT_CODE AND A.TO_WAREHOUSE_CODE=B.CODE ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int UpdateStock()
        {
            DataTable dt = Gestock1().Tables[0];
            List<CommandInfo> sqlList = new List<CommandInfo>();
            object a = 0;
            foreach (DataRow row in dt.Rows)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set QUANTITY=@QUANTITY where PRODUCT_CODE=@PRODUCT_CODE and WAREHOUSE_CODE=@WAREHOUSE_CODE");
                SqlParameter[] parameters = {
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,50),
                    new SqlParameter("@PRODUCT_CODE",SqlDbType.VarChar,50),
                    new SqlParameter("@WAREHOUSE_CODE",SqlDbType.VarChar,50)
                                            };
                parameters[0].Value = row["QUANTITY"];
                parameters[1].Value = row["PRODUCT_CODE"];
                parameters[2].Value = row["TO_WAREHOUSE_CODE"];
                sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
            }
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }
        #endregion
        #endregion
        #endregion
    }
}

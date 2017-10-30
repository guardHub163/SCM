using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using System.Data;
using SCM.DBUtility;
using System.Data.SqlClient;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public class TransferInManage : ITransferIn
    {
        /// <summary>
        /// 入库预定记录是否存在该记录并且未入库
        /// </summary>
        public bool PlanExists(decimal slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BLL_TRANSFER_IN_PLAN");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER AND STATUS_FLAG =@STATUS_FLAG");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal),
                    new SqlParameter("@STATUS_FLAG",SqlDbType.Int)
			};
            parameters[0].Value = slipNumber;
            parameters[1].Value = CConstant.INIT;


            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }        


        /// <summary>
        /// 门店入库确认
        /// </summary>
        public DataTable Insert(DataSet ds)
        {
            DataTable dt = CommonManage.GetReturnDataTable();
            List<CommandInfo> sqlList = null;
            StringBuilder strSql = null;
            DataRow dr = dt.NewRow();
            int rows = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dr["SLIP_NUMBER"] = row["SLIP_NUMBER"];
                sqlList = new List<CommandInfo>();
                rows = 0;
                try
                {
                    //入库预定单存在,并且未入库检查
                    if (row["SLIP_NUMBER"] != null && PlanExists(Convert.ToDecimal(row["SLIP_NUMBER"])))
                    {
                        //入库预定状态的更新
                        strSql = new StringBuilder();
                        strSql.Append("update BLL_TRANSFER_IN_PLAN set ");
                        strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
                        strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                        strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                        strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
                        SqlParameter[] tipParameters = {					
					                new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),					
					                new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					                new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal,9)};
                        tipParameters[0].Value = row["STATUS_FLAG"];
                        tipParameters[1].Value = row["LAST_UPDATE_USER"];
                        tipParameters[2].Value = row["SLIP_NUMBER"];
                        sqlList.Add(new CommandInfo(strSql.ToString(), tipParameters));
                        //入库表头信息的插入
                        string slipNumber = CommonManage.GetSeq("TI");
                        strSql = new StringBuilder();
                        strSql.Append("insert into BLL_RECEIPT(");
                        strSql.Append("SLIP_NUMBER,TRANSFER_IN_TYPE,ARRIVAL_DATE,FROM_WAREHOUSE_CODE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                        strSql.Append(" values (");
                        strSql.Append("@SLIP_NUMBER,@TRANSFER_IN_TYPE,@ARRIVAL_DATE,@FROM_WAREHOUSE_CODE,@TO_WAREHOUSE_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                        SqlParameter[] tiParameters = {
					                new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),                   
					                new SqlParameter("@TRANSFER_IN_TYPE", SqlDbType.Int,4),
                                    new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),					
					                new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),					
					                new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					                new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					                new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					                new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					                new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                        tiParameters[0].Value = slipNumber;
                        tiParameters[1].Value = CConstant.TRANSFER_IN_TYPE_PLAN;
                        tiParameters[2].Value = row["ARRIVAL_DATE"];
                        tiParameters[3].Value = row["FROM_WAREHOUSE_CODE"];
                        tiParameters[4].Value = row["TO_WAREHOUSE_CODE"];
                        tiParameters[5].Value = CConstant.INIT;
                        tiParameters[6].Value = row["ATTRIBUTE1"];
                        tiParameters[7].Value = row["ATTRIBUTE2"];
                        tiParameters[8].Value = row["ATTRIBUTE3"];
                        tiParameters[9].Value = row["LAST_UPDATE_USER"];
                        tiParameters[10].Value = row["LAST_UPDATE_USER"];
                        sqlList.Add(new CommandInfo(strSql.ToString(), tiParameters));
                        //入库明细信息插入
                        strSql = new StringBuilder();
                        strSql.Append("insert into BLL_TRANSFER_IN_LINE(");
                        strSql.Append("SLIP_NUMBER,LINE_NUMBER,TRANSFER_IN_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                        strSql.Append(" values (");
                        strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@TRANSFER_IN_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                        SqlParameter[] tilParameters = {
					                new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					                new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					                new SqlParameter("@TRANSFER_IN_PLAN_SLIP_NUMBER", SqlDbType.Decimal,9),
					                new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					                new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					                new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					                new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					                new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
                        tilParameters[0].Value = slipNumber;
                        tilParameters[1].Value = 1;
                        tilParameters[2].Value = row["SLIP_NUMBER"];
                        tilParameters[3].Value = row["PRODUCT_CODE"];
                        tilParameters[4].Value = row["UNIT_CODE"];
                        tilParameters[5].Value = row["QUANTITY"];
                        tilParameters[6].Value = row["STATUS_FLAG"];
                        tilParameters[7].Value = row["ATTRIBUTE1"];
                        tilParameters[8].Value = row["ATTRIBUTE2"];
                        tilParameters[9].Value = row["ATTRIBUTE3"];
                        sqlList.Add(new CommandInfo(strSql.ToString(), tilParameters));
                        //库存表的更新
                        #region 库存表的更新
                        strSql = new StringBuilder();
                        strSql.Append("select count(1) from BASE_STOCK");
                        strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                        SqlParameter[] searchStockParameters = {
					                new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					                new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)			};
                        searchStockParameters[0].Value = row["TO_WAREHOUSE_CODE"];
                        searchStockParameters[1].Value = row["PRODUCT_CODE"];

                        if (DbHelperSQL.Exists(strSql.ToString(), searchStockParameters))
                        {
                            #region 库存更新
                            strSql = new StringBuilder();
                            strSql.Append("update BASE_STOCK set ");
                            strSql.Append("QUANTITY=QUANTITY+@QUANTITY,");
                            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                            strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                            strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                            SqlParameter[] updateStockParameters = {
					                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					                    new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					                    new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                            updateStockParameters[0].Value = row["QUANTITY"];
                            updateStockParameters[1].Value = row["LAST_UPDATE_USER"];
                            updateStockParameters[2].Value = row["TO_WAREHOUSE_CODE"];
                            updateStockParameters[3].Value = row["PRODUCT_CODE"];
                            sqlList.Add(new CommandInfo(strSql.ToString(), updateStockParameters));
                            #endregion
                        }
                        else
                        {
                            #region 库存插入
                            strSql = new StringBuilder();
                            strSql.Append("insert into BASE_STOCK(");
                            strSql.Append("WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                            strSql.Append(" values (");
                            strSql.Append("@WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                            SqlParameter[] insertStockParameters = {
					                    new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					                    new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					                    new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					                    new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
                                        new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                            insertStockParameters[0].Value = row["TO_WAREHOUSE_CODE"];
                            insertStockParameters[1].Value = row["PRODUCT_CODE"];
                            insertStockParameters[2].Value = row["UNIT_CODE"];
                            insertStockParameters[3].Value = row["QUANTITY"];
                            insertStockParameters[4].Value = row["LAST_UPDATE_USER"];
                            insertStockParameters[5].Value = row["LAST_UPDATE_USER"];
                            sqlList.Add(new CommandInfo(strSql.ToString(), insertStockParameters));
                            #endregion
                        }
                        #endregion

                        try
                        {
                            rows = DbHelperSQL.ExecuteSqlTran(sqlList);
                        }
                        catch { }
                    }
                }
                catch (Exception ex) 
                {
                    //未知异常
                }

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

        public int GetTransferInCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_transfer_in_view ");
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

        /// <summary>
        /// 入库预定查询分页数据的获得
        /// </summary>
        public DataSet GetTransferInList(string sqlWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from bll_transfer_in_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


    }//end class
}

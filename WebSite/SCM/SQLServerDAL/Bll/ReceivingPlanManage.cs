using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using System.Data;
using SCM.DBUtility;
using System.Data.SqlClient;
using SCM.Model;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public class ReceivingPlanManage : IReceivingPlan
    {
        #region IReceivingPlan 成员

        /// <summary>
        /// 入库预定查询总记录数的获得
        /// </summary>
        public int GetRecordCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_receiving_plan_search_view ");
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
        public DataSet GetReceivingPlanList(string sqlWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from bll_receiving_plan_search_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 入库预定实体的获得
        /// </summary>
        public BllReceivingPlanTable getSearchViewMode(decimal slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,PURCHASE_SLIP_NUMBER,PURCHASE_LINE_NUMBER,INPUT_TYPE,SUPPLIER_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,INPUT_TYPE_NAME,STATUS_NAME,SUPPLIER_NAME,WAREHOUSE_NAME,PRODUCT_NAME,UNIT_NAME from bll_receiving_plan_search_view ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal,18)};
            parameters[0].Value = slipNumber;

            BllReceivingPlanTable receivingPlanTable = new BllReceivingPlanTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"] != null && ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    receivingPlanTable.SLIP_NUMBER = decimal.Parse(ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PURCHASE_SLIP_NUMBER"] != null && ds.Tables[0].Rows[0]["PURCHASE_SLIP_NUMBER"].ToString() != "")
                {
                    receivingPlanTable.PURCHASE_SLIP_NUMBER = ds.Tables[0].Rows[0]["PURCHASE_SLIP_NUMBER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PURCHASE_LINE_NUMBER"] != null && ds.Tables[0].Rows[0]["PURCHASE_LINE_NUMBER"].ToString() != "")
                {
                    receivingPlanTable.PURCHASE_LINE_NUMBER = int.Parse(ds.Tables[0].Rows[0]["PURCHASE_LINE_NUMBER"].ToString());
                }
                if (ds.Tables[0].Rows[0]["INPUT_TYPE"] != null && ds.Tables[0].Rows[0]["INPUT_TYPE"].ToString() != "")
                {
                    receivingPlanTable.INPUT_TYPE = int.Parse(ds.Tables[0].Rows[0]["INPUT_TYPE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SUPPLIER_CODE"] != null && ds.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString() != "")
                {
                    receivingPlanTable.SUPPLIER_CODE = ds.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DEPARTUAL_DATE"] != null && ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString() != "")
                {
                    receivingPlanTable.DEPARTUAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ARRIVAL_DATE"] != null && ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString() != "")
                {
                    receivingPlanTable.ARRIVAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"] != null && ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString() != "")
                {
                    receivingPlanTable.TO_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PRODUCT_CODE"] != null && ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString() != "")
                {
                    receivingPlanTable.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UNIT_CODE"] != null && ds.Tables[0].Rows[0]["UNIT_CODE"].ToString() != "")
                {
                    receivingPlanTable.UNIT_CODE = ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QUANTITY"] != null && ds.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
                {
                    receivingPlanTable.QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"] != null && ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    receivingPlanTable.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE1"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString() != "")
                {
                    receivingPlanTable.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE2"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString() != "")
                {
                    receivingPlanTable.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE3"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString() != "")
                {
                    receivingPlanTable.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_USER"] != null && ds.Tables[0].Rows[0]["CREATE_USER"].ToString() != "")
                {
                    receivingPlanTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"] != null && ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    receivingPlanTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_USER"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString() != "")
                {
                    receivingPlanTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    receivingPlanTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["INPUT_TYPE_NAME"] != null && ds.Tables[0].Rows[0]["INPUT_TYPE_NAME"].ToString() != "")
                {
                    receivingPlanTable.INPUT_TYPE_NAME = ds.Tables[0].Rows[0]["INPUT_TYPE_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["STATUS_NAME"] != null && ds.Tables[0].Rows[0]["STATUS_NAME"].ToString() != "")
                {
                    receivingPlanTable.STATUS_NAME = ds.Tables[0].Rows[0]["STATUS_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SUPPLIER_NAME"] != null && ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString() != "")
                {
                    receivingPlanTable.SUPPLIER_NAME = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WAREHOUSE_NAME"] != null && ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString() != "")
                {
                    receivingPlanTable.WAREHOUSE_NAME = ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PRODUCT_NAME"] != null && ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString() != "")
                {
                    receivingPlanTable.PRODUCT_NAME = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UNIT_NAME"] != null && ds.Tables[0].Rows[0]["UNIT_NAME"].ToString() != "")
                {
                    receivingPlanTable.UNIT_NAME = ds.Tables[0].Rows[0]["UNIT_NAME"].ToString();
                }
                return receivingPlanTable;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 供应商交期修正
        /// </summary>
        public bool Insert(List<BllReceivingPlanTable> list)
        {

            BllReceivingPlanTable rpTable = getSearchViewMode(list[0].SLIP_NUMBER);
            rpTable.ARRIVAL_DATE = list[0].ARRIVAL_DATE;
            rpTable.QUANTITY = list[0].QUANTITY;
            rpTable.LAST_UPDATE_USER = list[0].LAST_UPDATE_USER;

            List<CommandInfo> sqlList = new List<CommandInfo>();

            sqlList.Add(GetReceivingPlanUpdateSql(rpTable));

            if (list.Count > 1)
            {
                rpTable.ARRIVAL_DATE = list[1].ARRIVAL_DATE;
                rpTable.QUANTITY = list[1].QUANTITY;
                rpTable.CREATE_USER = list[1].CREATE_USER;
                rpTable.LAST_UPDATE_USER = list[1].LAST_UPDATE_USER;

                sqlList.Add(GetReceivingPlanInsertSql(rpTable));
            }
            return DbHelperSQL.ExecuteSqlTran(sqlList) > 0 ? true : false;

        }

        /// <summary>
        /// 入库确定
        /// </summary>
        public bool Insert(BllReceiptLineTable rlTable, BllReceivingPlanTable rpTable, List<BllReceiptReturnTable> returnlist, string userId)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            //原有入库预定数据取得
            BllReceivingPlanTable receivingTable = getSearchViewMode(rlTable.RECEIVING_PLAN_SLIP_NUMBER);

            #region 入库预定状态的更新
            receivingTable.STATUS_FLAG = CConstant.NORMAL;
            receivingTable.LAST_UPDATE_USER = userId;
            sqlList.Add(GetReceivingPlanUpdateSql(receivingTable));
            #endregion

            #region 分期入库数据的插入
            if (rpTable != null)
            {
                receivingTable.STATUS_FLAG = CConstant.INIT;
                receivingTable.ARRIVAL_DATE = rpTable.ARRIVAL_DATE;
                receivingTable.QUANTITY = rpTable.QUANTITY;
                receivingTable.CREATE_USER = userId;
                receivingTable.LAST_UPDATE_USER = userId;
                sqlList.Add(GetReceivingPlanInsertSql(receivingTable));
            }
            #endregion

            #region 入库记录的做成
            string receitpSlipNumber = CommonManage.GetSeq("RC");
            #region 表头的做成
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BLL_RECEIPT(");
            strSql.Append("SLIP_NUMBER,INPUT_TYPE,SUPPLIER_CODE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,RECEIPT_TYPE)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@INPUT_TYPE,@SUPPLIER_CODE,GETDATE(),@TO_WAREHOUSE_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE(),@RECEIPT_TYPE)");
            SqlParameter[] receiptParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@RECEIPT_TYPE", SqlDbType.Int,4)
                                               };
            receiptParameters[0].Value = receitpSlipNumber;
            receiptParameters[1].Value = receivingTable.INPUT_TYPE;
            receiptParameters[2].Value = receivingTable.SUPPLIER_CODE;
            receiptParameters[3].Value = receivingTable.TO_WAREHOUSE_CODE;
            receiptParameters[4].Value = CConstant.INIT;
            receiptParameters[5].Value = "";
            receiptParameters[6].Value = "";
            receiptParameters[7].Value = "";
            receiptParameters[8].Value = userId;
            receiptParameters[9].Value = userId;
            receiptParameters[10].Value = CConstant.RECEIPT_TYPE_PLAN;
            sqlList.Add(new CommandInfo(strSql.ToString(), receiptParameters));
            #endregion

            #region 明细的做成
            strSql = new StringBuilder();
            strSql.Append("insert into BLL_RECEIPT_LINE(");
            strSql.Append("SLIP_NUMBER,LINE_NUMBER,RECEIVING_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@RECEIVING_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
            SqlParameter[] receiptLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@RECEIVING_PLAN_SLIP_NUMBER", SqlDbType.Decimal,9),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
            receiptLineParameters[0].Value = receitpSlipNumber;
            receiptLineParameters[1].Value = 1;
            receiptLineParameters[2].Value = receivingTable.SLIP_NUMBER;
            receiptLineParameters[3].Value = receivingTable.PRODUCT_CODE;
            receiptLineParameters[4].Value = receivingTable.UNIT_CODE;
            receiptLineParameters[5].Value = rlTable.QUANTITY;
            receiptLineParameters[6].Value = CConstant.INIT;
            receiptLineParameters[7].Value = "";
            receiptLineParameters[8].Value = "";
            receiptLineParameters[9].Value = "";
            sqlList.Add(new CommandInfo(strSql.ToString(), receiptLineParameters));
            #endregion
            #endregion

            #region 库存表的更新
            strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_STOCK");
            strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
            SqlParameter[] searchStockParameters = {
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)			};
            searchStockParameters[0].Value = receivingTable.TO_WAREHOUSE_CODE;
            searchStockParameters[1].Value = receivingTable.PRODUCT_CODE;

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
                updateStockParameters[0].Value = rlTable.QUANTITY;
                updateStockParameters[1].Value = userId;
                updateStockParameters[2].Value = receivingTable.TO_WAREHOUSE_CODE;
                updateStockParameters[3].Value = receivingTable.PRODUCT_CODE;
                sqlList.Add(new CommandInfo(strSql.ToString(), updateStockParameters));
                #endregion
            }
            else
            {
                #region 库存插入
                strSql = new StringBuilder();
                strSql.Append("insert into BASE_STOCK(");
                strSql.Append("WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                SqlParameter[] insertStockParameters = {
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                insertStockParameters[0].Value = receivingTable.TO_WAREHOUSE_CODE;
                insertStockParameters[1].Value = receivingTable.PRODUCT_CODE;
                insertStockParameters[2].Value = receivingTable.UNIT_CODE;
                insertStockParameters[3].Value = rlTable.QUANTITY;
                insertStockParameters[4].Value = "";
                insertStockParameters[5].Value = "";
                insertStockParameters[6].Value = "";
                insertStockParameters[7].Value = userId;
                insertStockParameters[8].Value = userId;
                sqlList.Add(new CommandInfo(strSql.ToString(), insertStockParameters));
                #endregion
            }
            #endregion

            #region 入库返品信息的保存
            foreach (BllReceiptReturnTable returnTable in returnlist)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_RECEIPT_RETURN(");
                strSql.Append("RECIEPT_SLIP_NUMBER,RECIEPT_LINE_NUMBER,RETURN_REASON,SUPPLIER_CODE,RETURN_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,INPUT_TYPE)");
                strSql.Append(" values (");
                strSql.Append("@RECIEPT_SLIP_NUMBER,@RECIEPT_LINE_NUMBER,@RETURN_REASON,@SUPPLIER_CODE,@RETURN_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@INPUT_TYPE)");
                SqlParameter[] returnParameters = {
					new SqlParameter("@RECIEPT_SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@RECIEPT_LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@RETURN_REASON", SqlDbType.VarChar,20),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@RETURN_WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4)};
                returnParameters[0].Value = receitpSlipNumber;
                returnParameters[1].Value = 1;
                returnParameters[2].Value = returnTable.RETURN_REASON;
                returnParameters[3].Value = receivingTable.SUPPLIER_CODE;
                returnParameters[4].Value = receivingTable.TO_WAREHOUSE_CODE;
                returnParameters[5].Value = receivingTable.PRODUCT_CODE;
                returnParameters[6].Value = receivingTable.UNIT_CODE;
                returnParameters[7].Value = returnTable.QUANTITY;
                returnParameters[8].Value = CConstant.INIT;
                returnParameters[9].Value = "";
                returnParameters[10].Value = "";
                returnParameters[11].Value = "";
                returnParameters[12].Value = receivingTable.INPUT_TYPE;
                sqlList.Add(new CommandInfo(strSql.ToString(), returnParameters));
            }
            #endregion

            return DbHelperSQL.ExecuteSqlTran(sqlList) > 0 ? true : false;
        }

        public int Delete(decimal slipNumber)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();

            BllReceivingPlanTable rpTable = getSearchViewMode(slipNumber);
            BllPurchaseLineTable plTable = new PurchaseManage().GetPurchaseLineMode(rpTable.PURCHASE_SLIP_NUMBER, rpTable.PURCHASE_LINE_NUMBER);

            //入库预定数据的删除
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BLL_RECEIVING_PLAN ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] rpParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal)
			};
            rpParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), rpParameters));

            //采购明细数量的减少
            if (plTable.QUANTITY > rpTable.QUANTITY)
            {
                strSql = new StringBuilder();
                strSql.Append("update BLL_PURCHASE_LINE set ");
                strSql.Append("QUANTITY=QUANTITY-@QUANTITY");
                strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER and LINE_NUMBER=@LINE_NUMBER ");
                SqlParameter[] plParameters = {
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4)};

                plParameters[0].Value = rpTable.QUANTITY;
                plParameters[1].Value = plTable.SLIP_NUMBER;
                plParameters[2].Value = plTable.LINE_NUMBER;
                sqlList.Add(new CommandInfo(strSql.ToString(), plParameters));
            }
            else
            {
                strSql = new StringBuilder();
                strSql.Append("delete from BLL_PURCHASE_LINE ");
                strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER and LINE_NUMBER=@LINE_NUMBER ");
                SqlParameter[] plParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4)			};
                plParameters[0].Value = plTable.SLIP_NUMBER;
                plParameters[1].Value = plTable.LINE_NUMBER;
                sqlList.Add(new CommandInfo(strSql.ToString(), plParameters));

                //当采购明细不存在时，采购订单的删除
                strSql = new StringBuilder();
                strSql.Append(" DELETE FROM BLL_PURCHASE ");
                strSql.Append(" WHERE SLIP_NUMBER =@SLIP_NUMBER ");
                strSql.Append(" AND (SELECT COUNT(1) FROM BLL_PURCHASE_LINE WHERE SLIP_NUMBER =@PL_SLIP_NUMBER) = 0");
                SqlParameter[] pParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@PL_SLIP_NUMBER", SqlDbType.VarChar,20)};
                pParameters[0].Value = plTable.SLIP_NUMBER;
                pParameters[1].Value = plTable.SLIP_NUMBER;
                sqlList.Add(new CommandInfo(strSql.ToString(), pParameters));
            }

            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        #endregion

        /// <summary>
        /// 预定插入SQL的取得
        /// </summary>
        private CommandInfo GetReceivingPlanInsertSql(BllReceivingPlanTable rpTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BLL_RECEIVING_PLAN(");
            strSql.Append("PURCHASE_SLIP_NUMBER,PURCHASE_LINE_NUMBER,INPUT_TYPE,SUPPLIER_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@PURCHASE_SLIP_NUMBER,@PURCHASE_LINE_NUMBER,@INPUT_TYPE,@SUPPLIER_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] insertParameters = {
					new SqlParameter("@PURCHASE_SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@PURCHASE_LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTUAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            insertParameters[0].Value = rpTable.PURCHASE_SLIP_NUMBER;
            insertParameters[1].Value = rpTable.PURCHASE_LINE_NUMBER;
            insertParameters[2].Value = rpTable.INPUT_TYPE;
            insertParameters[3].Value = rpTable.SUPPLIER_CODE;
            insertParameters[4].Value = rpTable.DEPARTUAL_DATE;
            insertParameters[5].Value = rpTable.ARRIVAL_DATE;
            insertParameters[6].Value = rpTable.TO_WAREHOUSE_CODE;
            insertParameters[7].Value = rpTable.PRODUCT_CODE;
            insertParameters[8].Value = rpTable.UNIT_CODE;
            insertParameters[9].Value = rpTable.QUANTITY;
            insertParameters[10].Value = rpTable.STATUS_FLAG;
            insertParameters[11].Value = rpTable.ATTRIBUTE1;
            insertParameters[12].Value = rpTable.ATTRIBUTE2;
            insertParameters[13].Value = rpTable.ATTRIBUTE3;
            insertParameters[14].Value = rpTable.CREATE_USER;
            insertParameters[15].Value = rpTable.LAST_UPDATE_USER;

            return new CommandInfo(strSql.ToString(), insertParameters);
        }


        /// <summary>
        /// 预定修正SQL的取得
        /// </summary>
        private CommandInfo GetReceivingPlanUpdateSql(BllReceivingPlanTable rpTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BLL_RECEIVING_PLAN set ");
            strSql.Append("PURCHASE_SLIP_NUMBER=@PURCHASE_SLIP_NUMBER,");
            strSql.Append("PURCHASE_LINE_NUMBER=@PURCHASE_LINE_NUMBER,");
            strSql.Append("INPUT_TYPE=@INPUT_TYPE,");
            strSql.Append("SUPPLIER_CODE=@SUPPLIER_CODE,");
            strSql.Append("DEPARTUAL_DATE=@DEPARTUAL_DATE,");
            strSql.Append("ARRIVAL_DATE=@ARRIVAL_DATE,");
            strSql.Append("TO_WAREHOUSE_CODE=@TO_WAREHOUSE_CODE,");
            strSql.Append("PRODUCT_CODE=@PRODUCT_CODE,");
            strSql.Append("UNIT_CODE=@UNIT_CODE,");
            strSql.Append("QUANTITY=@QUANTITY,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=GETDATE()");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameters = {
					new SqlParameter("@PURCHASE_SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@PURCHASE_LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTUAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),				
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal,9)};
            parameters[0].Value = rpTable.PURCHASE_SLIP_NUMBER;
            parameters[1].Value = rpTable.PURCHASE_LINE_NUMBER;
            parameters[2].Value = rpTable.INPUT_TYPE;
            parameters[3].Value = rpTable.SUPPLIER_CODE;
            parameters[4].Value = rpTable.DEPARTUAL_DATE;
            parameters[5].Value = rpTable.ARRIVAL_DATE;
            parameters[6].Value = rpTable.TO_WAREHOUSE_CODE;
            parameters[7].Value = rpTable.PRODUCT_CODE;
            parameters[8].Value = rpTable.UNIT_CODE;
            parameters[9].Value = rpTable.QUANTITY;
            parameters[10].Value = rpTable.STATUS_FLAG;
            parameters[11].Value = rpTable.ATTRIBUTE1;
            parameters[12].Value = rpTable.ATTRIBUTE2;
            parameters[13].Value = rpTable.ATTRIBUTE3;
            parameters[14].Value = rpTable.LAST_UPDATE_USER;
            parameters[15].Value = rpTable.SLIP_NUMBER;

            return new CommandInfo(strSql.ToString(), parameters);
        }

    }
}

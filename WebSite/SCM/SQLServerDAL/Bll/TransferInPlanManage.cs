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
    public partial class TransferInPlanManage : ITransferInPlan
    {
        public int GetTransferPlanCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_transfer_in_plan_view ");
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
        public DataSet GetTransferPlanList(string sqlWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.SLIP_NUMBER desc,T.PRODUCT_CODE asc");
            }
            strSql.Append(")AS Row, T.*  from bll_transfer_in_plan_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BllTransferInPlanTable GetModel(decimal SLIP_NUMBER)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BT.*,BW.NAME AS WAREHOUSE_NAME,BW1.NAME AS SHOP_NAME, BP.NAME AS PRODUCT_NAME, BU.NAME AS UNIT_NAME  ");
            strSql.Append("FROM dbo.BLL_TRANSFER_IN_PLAN AS BT LEFT OUTER JOIN ");
            strSql.Append("dbo.BASE_WAREHOUSE AS BW ON BT.TO_WAREHOUSE_CODE = BW.CODE LEFT OUTER JOIN ");
            strSql.Append("dbo.BASE_WAREHOUSE AS BW1 ON BT.FROM_WAREHOUSE_CODE = BW1.CODE LEFT OUTER JOIN ");
            strSql.Append("dbo.BASE_PRODUCT AS BP ON BT.PRODUCT_CODE = BP.CODE LEFT OUTER JOIN ");
            strSql.Append("dbo.BASE_UNIT AS BU ON BT.UNIT_CODE = BU.CODE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal)
};
            parameters[0].Value = SLIP_NUMBER;

            BllTransferInPlanTable model = new BllTransferInPlanTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    model.SLIP_NUMBER = decimal.Parse(ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString());
                }
                model.SHIPMENT_SLIP_NUMBER = ds.Tables[0].Rows[0]["SHIPMENT_SLIP_NUMBER"].ToString();
                if (ds.Tables[0].Rows[0]["SHIPMENT_LINE_NUMBER"].ToString() != "")
                {
                    model.SHIPMENT_LINE_NUMBER = int.Parse(ds.Tables[0].Rows[0]["SHIPMENT_LINE_NUMBER"].ToString());
                }
                model.FROM_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString() != "")
                {
                    model.DEPARTUAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString() != "")
                {
                    model.ARRIVAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString());
                }
                model.TO_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString();
                model.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                model.UNIT_CODE = ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
                model.WAREHOUSE_NAME = ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString();
                model.SHOP_NAME = ds.Tables[0].Rows[0]["SHOP_NAME"].ToString();
                model.PRODUCT_NAME = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                model.UNIT_NAME = ds.Tables[0].Rows[0]["UNIT_NAME"].ToString();
                if (ds.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
                {
                    model.QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    model.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                model.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    model.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public int GetTransferInfo(BllTransferInPlanTable btable)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            string slipNumber = CommonManage.GetSeq("TI");
            StringBuilder strSql = new StringBuilder();

            //入库主表的添加
            #region
            strSql.Append("insert into BLL_TRANSFER_IN(");
            strSql.Append("SLIP_NUMBER,TRANSFER_IN_TYPE,ARRIVAL_DATE,FROM_WAREHOUSE_CODE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@TRANSFER_IN_TYPE,@ARRIVAL_DATE,@FROM_WAREHOUSE_CODE,@TO_WAREHOUSE_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
            SqlParameter[] transferParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@TRANSFER_IN_TYPE", SqlDbType.Int,4),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)
					                        };
            transferParameters[0].Value = slipNumber;
            transferParameters[1].Value = CConstant.TRANSFERIN_ENTER;
            transferParameters[2].Value = btable.ARRIVAL_DATE;
            transferParameters[3].Value = btable.FROM_WAREHOUSE_CODE;
            transferParameters[4].Value = btable.TO_WAREHOUSE_CODE;
            transferParameters[5].Value = CConstant.INIT;
            transferParameters[6].Value = "";
            transferParameters[7].Value = "";
            transferParameters[8].Value = "";
            transferParameters[9].Value = btable.CREATE_USER;
            transferParameters[10].Value = btable.LAST_UPDATE_USER;
            sqlList.Add(new CommandInfo(strSql.ToString(), transferParameters));
            #endregion

            //明细表的添加
            #region
            strSql = new StringBuilder();
            strSql.Append("insert into BLL_TRANSFER_IN_LINE(");
            strSql.Append("SLIP_NUMBER,LINE_NUMBER,TRANSFER_IN_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@TRANSFER_IN_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
            SqlParameter[] transferlineParameters = {
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
            transferlineParameters[0].Value = slipNumber;
            transferlineParameters[1].Value = 1;
            transferlineParameters[2].Value = btable.SLIP_NUMBER;
            transferlineParameters[3].Value = btable.PRODUCT_CODE;
            transferlineParameters[4].Value = btable.UNIT_CODE;
            transferlineParameters[5].Value = btable.TRANSFERQUANTITY;
            transferlineParameters[6].Value = CConstant.INIT;
            transferlineParameters[7].Value = "";
            transferlineParameters[8].Value = "";
            transferlineParameters[9].Value = "";
            sqlList.Add(new CommandInfo(strSql.ToString(), transferlineParameters));
            #endregion

            //库存表更新
            #region

            if (new StockManage().Exists(btable.TO_WAREHOUSE_CODE, btable.PRODUCT_CODE))
            {
                // 库存更新
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
                updateStockParameters[0].Value = btable.TRANSFERQUANTITY;
                updateStockParameters[1].Value = btable.CREATE_USER;
                updateStockParameters[2].Value = btable.TO_WAREHOUSE_CODE;
                updateStockParameters[3].Value = btable.PRODUCT_CODE;
                sqlList.Add(new CommandInfo(strSql.ToString(), updateStockParameters));

            }
            else
            {
                // 库存插入
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
                insertStockParameters[0].Value = btable.TO_WAREHOUSE_CODE;
                insertStockParameters[1].Value = btable.PRODUCT_CODE;
                insertStockParameters[2].Value = btable.UNIT_CODE;
                insertStockParameters[3].Value = btable.TRANSFERQUANTITY;
                insertStockParameters[4].Value = "";
                insertStockParameters[5].Value = "";
                insertStockParameters[6].Value = "";
                insertStockParameters[7].Value = btable.CREATE_USER;
                insertStockParameters[8].Value = btable.CREATE_USER;
                sqlList.Add(new CommandInfo(strSql.ToString(), insertStockParameters));
            }
            #endregion

            if (btable.TRANSFERQUANTITY != btable.QUANTITY)
            {
                //入库主表的添加(退货添加）
                slipNumber = CommonManage.GetSeq("TI");
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_TRANSFER_IN(");
                strSql.Append("SLIP_NUMBER,TRANSFER_IN_TYPE,ARRIVAL_DATE,FROM_WAREHOUSE_CODE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@TRANSFER_IN_TYPE,@ARRIVAL_DATE,@FROM_WAREHOUSE_CODE,@TO_WAREHOUSE_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                SqlParameter[] transferparameters2 = {
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
                    new SqlParameter("@TRANSFER_IN_TYPE", SqlDbType.Int,4),
                    new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
                    new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
                    new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
                    new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                transferparameters2[0].Value = slipNumber;
                transferparameters2[1].Value = CConstant.TRANSFERIN_OUT;
                transferparameters2[2].Value = btable.ARRIVAL_DATE;
                transferparameters2[3].Value = btable.TO_WAREHOUSE_CODE;
                transferparameters2[4].Value = btable.FROM_WAREHOUSE_CODE;
                transferparameters2[5].Value = CConstant.INIT;
                transferparameters2[6].Value = "";
                transferparameters2[7].Value = "";
                transferparameters2[8].Value = "";
                transferparameters2[9].Value = btable.CREATE_USER;
                transferparameters2[10].Value = btable.LAST_UPDATE_USER;
                sqlList.Add(new CommandInfo(strSql.ToString(), transferparameters2));

                //明细表的添加(退货库添加）
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_TRANSFER_IN_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,TRANSFER_IN_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@TRANSFER_IN_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] transferlineParameters2 = {
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
                transferlineParameters2[0].Value = slipNumber;
                transferlineParameters2[1].Value = 1;
                transferlineParameters2[2].Value = btable.SLIP_NUMBER;
                transferlineParameters2[3].Value = btable.PRODUCT_CODE;
                transferlineParameters2[4].Value = btable.UNIT_CODE;
                transferlineParameters2[5].Value = (btable.QUANTITY - btable.TRANSFERQUANTITY);
                transferlineParameters2[6].Value = CConstant.INIT;
                transferlineParameters2[7].Value = "";
                transferlineParameters2[8].Value = "";
                transferlineParameters2[9].Value = "";
                sqlList.Add(new CommandInfo(strSql.ToString(), transferlineParameters2));

                //库存表更新(退货添加）

                if (new StockManage().Exists(btable.FROM_WAREHOUSE_CODE, btable.PRODUCT_CODE))
                {
                    // 库存更新(退货添加）
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
                    updateStockParameters[0].Value = (btable.QUANTITY - btable.TRANSFERQUANTITY);
                    updateStockParameters[1].Value = btable.CREATE_USER;
                    updateStockParameters[2].Value = btable.FROM_WAREHOUSE_CODE;
                    updateStockParameters[3].Value = btable.PRODUCT_CODE;
                    sqlList.Add(new CommandInfo(strSql.ToString(), updateStockParameters));

                }
                else
                {
                    // 库存插入(退货添加）
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
                    insertStockParameters[0].Value = btable.FROM_WAREHOUSE_CODE;
                    insertStockParameters[1].Value = btable.PRODUCT_CODE;
                    insertStockParameters[2].Value = btable.UNIT_CODE;
                    insertStockParameters[3].Value = (btable.QUANTITY - btable.TRANSFERQUANTITY);
                    insertStockParameters[4].Value = "";
                    insertStockParameters[5].Value = "";
                    insertStockParameters[6].Value = "";
                    insertStockParameters[7].Value = btable.CREATE_USER;
                    insertStockParameters[8].Value = btable.CREATE_USER;
                    sqlList.Add(new CommandInfo(strSql.ToString(), insertStockParameters));
                }
            }

            strSql = new StringBuilder();
            strSql.Append("update BLL_TRANSFER_IN_PLAN set STATUS_FLAG=@STATUS_FLAG where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] updatetransfer ={
            new SqlParameter("@STATUS_FLAG",SqlDbType.Int,9),
            new SqlParameter("@SLIP_NUMBER",SqlDbType.VarChar,255)
                                          };
            updatetransfer[0].Value = CConstant.NORMAL;
            updatetransfer[1].Value = btable.SLIP_NUMBER;
            sqlList.Add(new CommandInfo(strSql.ToString(), updatetransfer));

            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

    }
}

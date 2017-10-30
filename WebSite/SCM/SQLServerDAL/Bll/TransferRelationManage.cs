using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SCM.Model.Base;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public partial class TransferRelationManage : ITransferRelation
    {
        public TransferRelationManage() { }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetTranferRelationCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM bll_transfer_relation_view ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetTranferRelationByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.* from bll_transfer_relation_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int Insert(BllShipmentTable shptable)
        {
            #region 出库的添加
            List<CommandInfo> sqlList = new List<CommandInfo>();
            //出库Number
            string slipNumber = CommonManage.GetSeq("SP");
            shptable.SLIP_NUMBER = slipNumber;
            //出库HEADER表的保存
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BLL_SHIPMENT(");
            strSql.Append("SLIP_NUMBER,SHIPMENT_TYPE,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@SHIPMENT_TYPE,@FROM_WAREHOUSE_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
                    new SqlParameter("@SHIPMENT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTUAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = slipNumber;
            parameters[1].Value = shptable.SHIPMENT_TYPE;
            parameters[2].Value = shptable.FROM_WAREHOUSE_CODE;
            parameters[3].Value = shptable.DEPARTUAL_DATE;
            parameters[4].Value = shptable.ARRIVAL_DATE;
            parameters[5].Value = shptable.TO_WAREHOUSE_CODE;
            parameters[6].Value = shptable.STATUS_FLAG;
            parameters[7].Value = shptable.ATTRIBUTE1;
            parameters[8].Value = shptable.ATTRIBUTE2;
            parameters[9].Value = shptable.ATTRIBUTE3;
            parameters[10].Value = shptable.CREATE_USER;
            parameters[11].Value = shptable.LAST_UPDATE_USER;

            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));

            foreach (BllShipmentLineTable shipmentLineTable in shptable.SHIPMENT_LINE)
            {
                shipmentLineTable.SLIP_NUMBER = slipNumber;
                //出库明细的保存
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_SHIPMENT_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,SHIPMENT_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@SHIPMENT_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] lineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@SHIPMENT_PLAN_SLIP_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
                lineParameters[0].Value = shipmentLineTable.SLIP_NUMBER;
                lineParameters[1].Value = shipmentLineTable.LINE_NUMBER;
                lineParameters[2].Value = shipmentLineTable.SHIPMENT_PLAN_SLIP_NUMBER;
                lineParameters[3].Value = shipmentLineTable.PRODUCT_CODE;
                lineParameters[4].Value = shipmentLineTable.UNIT_CODE;
                lineParameters[5].Value = shipmentLineTable.QUANTITY;
                lineParameters[6].Value = shipmentLineTable.STATUS_FLAG;
                lineParameters[7].Value = shipmentLineTable.ATTRIBUTE1;
                lineParameters[8].Value = shipmentLineTable.ATTRIBUTE2;
                lineParameters[9].Value = shipmentLineTable.ATTRIBUTE3;

                sqlList.Add(new CommandInfo(strSql.ToString(), lineParameters));

                //出库库存更新
                if (shipmentLineTable.QUANTITY != 0)
                {
                    strSql = new StringBuilder();
                    strSql.Append("update BASE_STOCK set ");
                    strSql.Append("QUANTITY=(QUANTITY - @QUANTITY),");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                    strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                    strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                    SqlParameter[] stockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                    stockParameters[0].Value = shipmentLineTable.QUANTITY;
                    stockParameters[1].Value = shptable.LAST_UPDATE_USER;
                    stockParameters[2].Value = shptable.FROM_WAREHOUSE_CODE;
                    stockParameters[3].Value = shipmentLineTable.PRODUCT_CODE;

                    sqlList.Add(new CommandInfo(strSql.ToString(), stockParameters));

                }
            }
            #endregion
            #region 入库的添加
            //入库主表的添加
            string SlipNumber = CommonManage.GetSeq("TI");
            strSql = new StringBuilder();
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
            transferParameters[0].Value = SlipNumber;
            transferParameters[1].Value = CConstant.TRANSFER_IN_TYPE_SHFIT;
            transferParameters[2].Value = shptable.ARRIVAL_DATE;
            transferParameters[3].Value = shptable.FROM_WAREHOUSE_CODE;
            transferParameters[4].Value = shptable.TO_WAREHOUSE_CODE;
            transferParameters[5].Value = shptable.STATUS_FLAG;
            transferParameters[6].Value = "";
            transferParameters[7].Value = "";
            transferParameters[8].Value = "";
            transferParameters[9].Value = shptable.CREATE_USER;
            transferParameters[10].Value = shptable.LAST_UPDATE_USER;
            sqlList.Add(new CommandInfo(strSql.ToString(), transferParameters));

            foreach (BllShipmentLineTable shipmentLineTable in shptable.SHIPMENT_LINE) 
            {
                //入库明细添加
                shipmentLineTable.SLIP_NUMBER = SlipNumber;
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
                transferlineParameters[0].Value = shipmentLineTable.SLIP_NUMBER;
                transferlineParameters[1].Value = shipmentLineTable.LINE_NUMBER;
                transferlineParameters[2].Value = shipmentLineTable.SHIPMENT_PLAN_SLIP_NUMBER;
                transferlineParameters[3].Value = shipmentLineTable.PRODUCT_CODE;
                transferlineParameters[4].Value = shipmentLineTable.UNIT_CODE;
                transferlineParameters[5].Value = shipmentLineTable.QUANTITY;
                transferlineParameters[6].Value = shipmentLineTable.STATUS_FLAG;
                transferlineParameters[7].Value = shipmentLineTable.ATTRIBUTE1;
                transferlineParameters[8].Value = shipmentLineTable.ATTRIBUTE2;
                transferlineParameters[9].Value = shipmentLineTable.ATTRIBUTE3;
                sqlList.Add(new CommandInfo(strSql.ToString(), transferlineParameters));

                if (new StockManage().Exists(shptable.TO_WAREHOUSE_CODE, shipmentLineTable.PRODUCT_CODE))
                {
                    // 入库库存库存更新
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
                    updateStockParameters[0].Value = shipmentLineTable.QUANTITY;
                    updateStockParameters[1].Value = shptable.CREATE_USER;
                    updateStockParameters[2].Value = shptable.TO_WAREHOUSE_CODE;
                    updateStockParameters[3].Value = shipmentLineTable.PRODUCT_CODE;
                    sqlList.Add(new CommandInfo(strSql.ToString(), updateStockParameters));

                }
                else
                {
                    // 入库库存插入
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
                    insertStockParameters[0].Value = shptable.TO_WAREHOUSE_CODE;
                    insertStockParameters[1].Value = shipmentLineTable.PRODUCT_CODE;
                    insertStockParameters[2].Value = shipmentLineTable.UNIT_CODE;
                    insertStockParameters[3].Value = shipmentLineTable.QUANTITY;
                    insertStockParameters[4].Value = "";
                    insertStockParameters[5].Value = "";
                    insertStockParameters[6].Value = "";
                    insertStockParameters[7].Value = shptable.CREATE_USER;
                    insertStockParameters[8].Value = shptable.CREATE_USER;
                    sqlList.Add(new CommandInfo(strSql.ToString(), insertStockParameters));
                }
            }
            #endregion
            #region 交互表的添加
            string TransferNumber = CommonManage.GetSeq("TE");
            strSql = new StringBuilder();
            strSql.Append("insert into BLL_TRANSFER_RELATION(");
            strSql.Append("SLIP_NUMBER,FROM_WAREHOUSE_CODE,TO_WAREHOUSE_CODE,SHIPMENT_SLIP_NUMBER,TRANSFER_IN_SLIP_NUMBER,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER_ID,CREATE_DATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@FROM_WAREHOUSE_CODE,@TO_WAREHOUSE_CODE,@SHIPMENT_SLIP_NUMBER,@TRANSFER_IN_SLIP_NUMBER,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER_ID,@CREATE_DATE_TIME)");
            SqlParameter[] parametersTransfer = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@SHIPMENT_SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@TRANSFER_IN_SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.VarChar,20),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.VarChar,20),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.VarChar,20),
					new SqlParameter("@CREATE_USER_ID", SqlDbType.VarChar,20),
					new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime)};
            parametersTransfer[0].Value = TransferNumber;
            parametersTransfer[1].Value = shptable.FROM_WAREHOUSE_CODE;
            parametersTransfer[2].Value = shptable.TO_WAREHOUSE_CODE;
            parametersTransfer[3].Value = slipNumber;
            parametersTransfer[4].Value = SlipNumber;
            parametersTransfer[5].Value = CConstant.INIT;
            parametersTransfer[6].Value = shptable.ATTRIBUTE1;
            parametersTransfer[7].Value = shptable.ATTRIBUTE2;
            parametersTransfer[8].Value = shptable.ATTRIBUTE3;
            parametersTransfer[9].Value = shptable.CREATE_USER;
            parametersTransfer[10].Value = shptable.DEPARTUAL_DATE;
            sqlList.Add(new CommandInfo(strSql.ToString(), parametersTransfer));
            #endregion

            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string SLIP_NUMBER)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BLL_TRANSFER_RELATION ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50)};
            parameters[0].Value = SLIP_NUMBER;

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

        ///<summary>
        ///获得仓库调拨的明细
        ///</summary>
        public DataSet GetTransferRelationDetail(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM  bll_transfer_relation_show_view ");
            strSql.AppendFormat(" WHERE SLIP_NUMBER ='{0}' ORDER BY LINE_NUMBER ", slipNumber);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}

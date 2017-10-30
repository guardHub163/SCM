using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.Model;
using SCM.DBUtility;
using System.Data.SqlClient;
using System.Data;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public class ShipmentManage : IShipment
    {
        #region IShipment 成员

        public int Insert(BllShipmentTable shipmentTable)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            //出库Number
            string slipNumber = CommonManage.GetSeq("SP");
            shipmentTable.SLIP_NUMBER = slipNumber;
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
            parameters[0].Value = shipmentTable.SLIP_NUMBER;
            parameters[1].Value = shipmentTable.SHIPMENT_TYPE;
            parameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
            parameters[3].Value = shipmentTable.DEPARTUAL_DATE;
            parameters[4].Value = shipmentTable.ARRIVAL_DATE;
            parameters[5].Value = shipmentTable.TO_WAREHOUSE_CODE;
            parameters[6].Value = shipmentTable.STATUS_FLAG;
            parameters[7].Value = shipmentTable.ATTRIBUTE1;
            parameters[8].Value = shipmentTable.ATTRIBUTE2;
            parameters[9].Value = shipmentTable.ATTRIBUTE3;
            parameters[10].Value = shipmentTable.CREATE_USER;
            parameters[11].Value = shipmentTable.LAST_UPDATE_USER;

            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));

            foreach (BllShipmentLineTable shipmentLineTable in shipmentTable.SHIPMENT_LINE)
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

                //库存更新
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
                    stockParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                    stockParameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
                    stockParameters[3].Value = shipmentLineTable.PRODUCT_CODE;

                    sqlList.Add(new CommandInfo(strSql.ToString(), stockParameters));
                }

                if (shipmentTable.SHIPMENT_TYPE == CConstant.SHIPMENT_TYPE_PLAN)
                {
                    //出库预定状态更新
                    strSql = new StringBuilder();
                    strSql.Append("update BLL_SHIPMENT_PLAN set  ");
                    strSql.Append("STATUS_FLAG = @STATUS_FLAG,  ");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                    strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                    strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
                    SqlParameter[] planParameters = {
					new SqlParameter("@STATUS_FLAG",SqlDbType.Int ,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal,18)};
                    planParameters[0].Value = CConstant.NORMAL;
                    planParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                    planParameters[2].Value = shipmentLineTable.SHIPMENT_PLAN_SLIP_NUMBER;

                    sqlList.Add(new CommandInfo(strSql.ToString(), planParameters));

                    //入库预定的做成
                    strSql = new StringBuilder();
                    strSql.Append("insert into BLL_TRANSFER_IN_PLAN(");
                    strSql.Append("SHIPMENT_SLIP_NUMBER,SHIPMENT_LINE_NUMBER,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                    strSql.Append(" values (");
                    strSql.Append("@SHIPMENT_SLIP_NUMBER,@SHIPMENT_LINE_NUMBER,@FROM_WAREHOUSE_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] transferInPlanParameters = {
					new SqlParameter("@SHIPMENT_SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@SHIPMENT_LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
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
                    transferInPlanParameters[0].Value = shipmentLineTable.SLIP_NUMBER;
                    transferInPlanParameters[1].Value = shipmentLineTable.LINE_NUMBER;
                    transferInPlanParameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
                    transferInPlanParameters[3].Value = shipmentTable.DEPARTUAL_DATE;
                    transferInPlanParameters[4].Value = shipmentTable.ARRIVAL_DATE;
                    transferInPlanParameters[5].Value = shipmentTable.TO_WAREHOUSE_CODE;
                    transferInPlanParameters[6].Value = shipmentLineTable.PRODUCT_CODE;
                    transferInPlanParameters[7].Value = shipmentLineTable.UNIT_CODE;
                    transferInPlanParameters[8].Value = shipmentLineTable.QUANTITY;
                    transferInPlanParameters[9].Value = CConstant.INIT;
                    transferInPlanParameters[10].Value = shipmentLineTable.ATTRIBUTE1;
                    transferInPlanParameters[11].Value = shipmentLineTable.ATTRIBUTE2;
                    transferInPlanParameters[12].Value = shipmentLineTable.ATTRIBUTE3;
                    transferInPlanParameters[13].Value = shipmentTable.CREATE_USER;
                    transferInPlanParameters[14].Value = shipmentTable.LAST_UPDATE_USER;

                    sqlList.Add(new CommandInfo(strSql.ToString(), transferInPlanParameters));

                }
            }

            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        public string  InsertPlan(BllShipmentTable shipmentTable)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            //出库Number
            string slipNumber = CommonManage.GetSeq("SP");
            shipmentTable.SLIP_NUMBER = slipNumber;
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
            parameters[0].Value = shipmentTable.SLIP_NUMBER;
            parameters[1].Value = shipmentTable.SHIPMENT_TYPE;
            parameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
            parameters[3].Value = shipmentTable.DEPARTUAL_DATE;
            parameters[4].Value = shipmentTable.ARRIVAL_DATE;
            parameters[5].Value = shipmentTable.TO_WAREHOUSE_CODE;
            parameters[6].Value = shipmentTable.STATUS_FLAG;
            parameters[7].Value = shipmentTable.ATTRIBUTE1;
            parameters[8].Value = shipmentTable.ATTRIBUTE2;
            parameters[9].Value = shipmentTable.ATTRIBUTE3;
            parameters[10].Value = shipmentTable.CREATE_USER;
            parameters[11].Value = shipmentTable.LAST_UPDATE_USER;

            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));

            foreach (BllShipmentLineTable shipmentLineTable in shipmentTable.SHIPMENT_LINE)
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

                //库存更新
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
                    stockParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                    stockParameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
                    stockParameters[3].Value = shipmentLineTable.PRODUCT_CODE;

                    sqlList.Add(new CommandInfo(strSql.ToString(), stockParameters));
                }

                if (shipmentTable.SHIPMENT_TYPE == CConstant.SHIPMENT_TYPE_PLAN)
                {
                    //出库预定状态更新
                    strSql = new StringBuilder();
                    strSql.Append("update BLL_SHIPMENT_PLAN set  ");
                    strSql.Append("STATUS_FLAG = @STATUS_FLAG,  ");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                    strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                    strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
                    SqlParameter[] planParameters = {
					new SqlParameter("@STATUS_FLAG",SqlDbType.Int ,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.Decimal,18)};
                    planParameters[0].Value = CConstant.NORMAL;
                    planParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                    planParameters[2].Value = shipmentLineTable.SHIPMENT_PLAN_SLIP_NUMBER;

                    sqlList.Add(new CommandInfo(strSql.ToString(), planParameters));

                    //入库预定的做成
                    strSql = new StringBuilder();
                    strSql.Append("insert into BLL_TRANSFER_IN_PLAN(");
                    strSql.Append("SHIPMENT_SLIP_NUMBER,SHIPMENT_LINE_NUMBER,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                    strSql.Append(" values (");
                    strSql.Append("@SHIPMENT_SLIP_NUMBER,@SHIPMENT_LINE_NUMBER,@FROM_WAREHOUSE_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] transferInPlanParameters = {
					new SqlParameter("@SHIPMENT_SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@SHIPMENT_LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
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
                    transferInPlanParameters[0].Value = shipmentLineTable.SLIP_NUMBER;
                    transferInPlanParameters[1].Value = shipmentLineTable.LINE_NUMBER;
                    transferInPlanParameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
                    transferInPlanParameters[3].Value = shipmentTable.DEPARTUAL_DATE;
                    transferInPlanParameters[4].Value = shipmentTable.ARRIVAL_DATE;
                    transferInPlanParameters[5].Value = shipmentTable.TO_WAREHOUSE_CODE;
                    transferInPlanParameters[6].Value = shipmentLineTable.PRODUCT_CODE;
                    transferInPlanParameters[7].Value = shipmentLineTable.UNIT_CODE;
                    transferInPlanParameters[8].Value = shipmentLineTable.QUANTITY;
                    transferInPlanParameters[9].Value = CConstant.INIT;
                    transferInPlanParameters[10].Value = shipmentLineTable.ATTRIBUTE1;
                    transferInPlanParameters[11].Value = shipmentLineTable.ATTRIBUTE2;
                    transferInPlanParameters[12].Value = shipmentLineTable.ATTRIBUTE3;
                    transferInPlanParameters[13].Value = shipmentTable.CREATE_USER;
                    transferInPlanParameters[14].Value = shipmentTable.LAST_UPDATE_USER;

                    sqlList.Add(new CommandInfo(strSql.ToString(), transferInPlanParameters));

                }
            }

            int ret= DbHelperSQL.ExecuteSqlTran(sqlList);
            if (ret > 0)
            {
                return slipNumber;
            }
            else 
            {
                return "";
            }

        }

        public int Update(BllShipmentTable shipmentTable)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();

            //出库主表更新
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BLL_SHIPMENT set ");
            strSql.Append("FROM_WAREHOUSE_CODE=@FROM_WAREHOUSE_CODE,");
            strSql.Append("DEPARTUAL_DATE=@DEPARTUAL_DATE,");
            strSql.Append("ARRIVAL_DATE=@ARRIVAL_DATE,");
            strSql.Append("TO_WAREHOUSE_CODE=@TO_WAREHOUSE_CODE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=GETDATE()");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] updateShipmentParameters = {
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTUAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            updateShipmentParameters[0].Value = shipmentTable.FROM_WAREHOUSE_CODE;
            updateShipmentParameters[1].Value = shipmentTable.DEPARTUAL_DATE;
            updateShipmentParameters[2].Value = shipmentTable.ARRIVAL_DATE;
            updateShipmentParameters[3].Value = shipmentTable.TO_WAREHOUSE_CODE;
            updateShipmentParameters[4].Value = shipmentTable.STATUS_FLAG;
            updateShipmentParameters[5].Value = shipmentTable.ATTRIBUTE1;
            updateShipmentParameters[6].Value = shipmentTable.ATTRIBUTE2;
            updateShipmentParameters[7].Value = shipmentTable.ATTRIBUTE3;
            updateShipmentParameters[8].Value = shipmentTable.LAST_UPDATE_USER;
            updateShipmentParameters[9].Value = shipmentTable.SLIP_NUMBER;
            sqlList.Add(new CommandInfo(strSql.ToString(), updateShipmentParameters));

            //原有出库信息的获得
            DataSet dsDetail = GetShipmentDetail(shipmentTable.SLIP_NUMBER);
            //原有明细库存的还原
            foreach (DataRow row in dsDetail.Tables[0].Rows)
            {
                //出库仓库库存的增加
                strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set ");
                strSql.Append("QUANTITY=(QUANTITY+@QUANTITY),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                SqlParameter[] fromStockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                fromStockParameters[0].Value = row["QUANTITY"] != null ? Convert.ToDecimal(row["QUANTITY"]) : 0;
                fromStockParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                fromStockParameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
                fromStockParameters[3].Value = row["PRODUCT_CODE"] != null ? Convert.ToString(row["PRODUCT_CODE"]) : "";

                sqlList.Add(new CommandInfo(strSql.ToString(), fromStockParameters));

                //入库仓库库存的减少
                strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set ");
                strSql.Append("QUANTITY=(QUANTITY-@QUANTITY),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                SqlParameter[] toStockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                toStockParameters[0].Value = row["QUANTITY"] != null ? Convert.ToDecimal(row["QUANTITY"]) : 0;
                toStockParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                toStockParameters[2].Value = shipmentTable.TO_WAREHOUSE_CODE;
                toStockParameters[3].Value = row["PRODUCT_CODE"] != null ? Convert.ToString(row["PRODUCT_CODE"]) : "";
                sqlList.Add(new CommandInfo(strSql.ToString(), toStockParameters));
            }

            //原有明细的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_SHIPMENT_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] deleteLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteLineParameters[0].Value = shipmentTable.SLIP_NUMBER;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteLineParameters));

            //新的明细的保存
            foreach (BllShipmentLineTable shipmentLineTable in shipmentTable.SHIPMENT_LINE)
            {
                //明细插入
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_SHIPMENT_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,SHIPMENT_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@SHIPMENT_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] insertLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@SHIPMENT_PLAN_SLIP_NUMBER", SqlDbType.Decimal,9),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
                insertLineParameters[0].Value = shipmentLineTable.SLIP_NUMBER;
                insertLineParameters[1].Value = shipmentLineTable.LINE_NUMBER;
                insertLineParameters[2].Value = shipmentLineTable.SHIPMENT_PLAN_SLIP_NUMBER;
                insertLineParameters[3].Value = shipmentLineTable.PRODUCT_CODE;
                insertLineParameters[4].Value = shipmentLineTable.UNIT_CODE;
                insertLineParameters[5].Value = shipmentLineTable.QUANTITY;
                insertLineParameters[6].Value = shipmentLineTable.STATUS_FLAG;
                insertLineParameters[7].Value = shipmentLineTable.ATTRIBUTE1;
                insertLineParameters[8].Value = shipmentLineTable.ATTRIBUTE2;
                insertLineParameters[9].Value = shipmentLineTable.ATTRIBUTE3;
                sqlList.Add(new CommandInfo(strSql.ToString(), insertLineParameters));

                //出库仓库库存的减少
                strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set ");
                strSql.Append("QUANTITY=(QUANTITY-@QUANTITY),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                SqlParameter[] fromStockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                fromStockParameters[0].Value = shipmentLineTable.QUANTITY;
                fromStockParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                fromStockParameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
                fromStockParameters[3].Value = shipmentLineTable.PRODUCT_CODE;

                sqlList.Add(new CommandInfo(strSql.ToString(), fromStockParameters));

                //入库仓库库存的增加
                strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set ");
                strSql.Append("QUANTITY=(QUANTITY+@QUANTITY),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                SqlParameter[] toStockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                toStockParameters[0].Value = shipmentLineTable.QUANTITY;
                toStockParameters[1].Value = shipmentTable.LAST_UPDATE_USER;
                toStockParameters[2].Value = shipmentTable.FROM_WAREHOUSE_CODE;
                toStockParameters[3].Value = shipmentLineTable.PRODUCT_CODE;
                sqlList.Add(new CommandInfo(strSql.ToString(), toStockParameters));
            }


            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        public int Delete(string slipNumber,string userId)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            //原有出库信息的获得
            DataSet dsDetail = GetShipmentDetail(slipNumber);
            //原有明细库存的还原
            foreach (DataRow row in dsDetail.Tables[0].Rows)
            {
                //出库仓库库存的增加
                strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set ");
                strSql.Append("QUANTITY=(QUANTITY+@QUANTITY),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                SqlParameter[] fromStockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                fromStockParameters[0].Value = row["QUANTITY"] != null ? Convert.ToDecimal(row["QUANTITY"]) : 0;
                fromStockParameters[1].Value = userId;
                fromStockParameters[2].Value = row["FROM_WAREHOUSE_CODE"] != null ? Convert.ToString(row["FROM_WAREHOUSE_CODE"]) : "";
                fromStockParameters[3].Value = row["PRODUCT_CODE"] != null ? Convert.ToString(row["PRODUCT_CODE"]) : "";
                sqlList.Add(new CommandInfo(strSql.ToString(), fromStockParameters));

                //入库仓库库存的减少
                strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set ");
                strSql.Append("QUANTITY=(QUANTITY-@QUANTITY),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                SqlParameter[] toStockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                toStockParameters[0].Value = row["QUANTITY"] != null ? Convert.ToDecimal(row["QUANTITY"]) : 0;
                toStockParameters[1].Value = userId;
                toStockParameters[2].Value = row["TO_WAREHOUSE_CODE"] != null ? Convert.ToString(row["TO_WAREHOUSE_CODE"]) : "";
                toStockParameters[3].Value = row["PRODUCT_CODE"] != null ? Convert.ToString(row["PRODUCT_CODE"]) : "";
                sqlList.Add(new CommandInfo(strSql.ToString(), toStockParameters));
            }

            //原有明细的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_SHIPMENT_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] deleteLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteLineParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteLineParameters));

            //出库主表信息的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_SHIPMENT ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] deleteHeaderParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteHeaderParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteHeaderParameters));

            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        public int GetRecordCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_shipment_view ");
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

        public DataSet GetList(string sqlWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from bll_shipment_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public BllShipmentTable GetShipmentModel(string SLIP_NUMBER)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,FROM_WAREHOUSE_NAME,TO_WAREHOUSE_NAME from bll_shipment_view ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            parameters[0].Value = SLIP_NUMBER;
            BllShipmentTable shipmentTable = new BllShipmentTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"] != null && ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    shipmentTable.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"] != null && ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"].ToString() != "")
                {
                    shipmentTable.FROM_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DEPARTUAL_DATE"] != null && ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString() != "")
                {
                    shipmentTable.DEPARTUAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ARRIVAL_DATE"] != null && ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString() != "")
                {
                    shipmentTable.ARRIVAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"] != null && ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString() != "")
                {
                    shipmentTable.TO_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"] != null && ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    shipmentTable.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE1"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString() != "")
                {
                    shipmentTable.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE2"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString() != "")
                {
                    shipmentTable.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE3"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString() != "")
                {
                    shipmentTable.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_USER"] != null && ds.Tables[0].Rows[0]["CREATE_USER"].ToString() != "")
                {
                    shipmentTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"] != null && ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    shipmentTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_USER"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString() != "")
                {
                    shipmentTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    shipmentTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FROM_WAREHOUSE_NAME"] != null && ds.Tables[0].Rows[0]["FROM_WAREHOUSE_NAME"].ToString() != "")
                {
                    shipmentTable.FROM_WAREHOUSE_NAME = ds.Tables[0].Rows[0]["FROM_WAREHOUSE_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TO_WAREHOUSE_NAME"] != null && ds.Tables[0].Rows[0]["TO_WAREHOUSE_NAME"].ToString() != "")
                {
                    shipmentTable.TO_WAREHOUSE_NAME = ds.Tables[0].Rows[0]["TO_WAREHOUSE_NAME"].ToString();
                }
                return shipmentTable;
            }
            else
            {
                return null;
            }
        }

        public DataSet GetShipmentDetail(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM  bll_shipment_detail_view ");
            strSql.AppendFormat(" WHERE SLIP_NUMBER ='{0}' ORDER BY LINE_NUMBER ", slipNumber);
            return DbHelperSQL.Query(strSql.ToString());
        }        

        #endregion
    }
}

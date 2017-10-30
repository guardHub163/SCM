using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Data.SqlClient;
using System.Data;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public class PurchaseManage : IPurchase
    {
        #region IPurchase 成员

        public PurchaseManage() { }

        public int GetRecordCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_purchase_view ");
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

        public DataSet GetPurchaseList(string sqlWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from bll_purchase_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int Insert(BllPurchaseTable purchaseTable)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            //采购Number
            string slipNumber = CommonManage.GetSeq("PU");
            purchaseTable.SLIP_NUMBER = slipNumber;
            //采购表头
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BLL_PURCHASE(");
            strSql.Append("SLIP_NUMBER,INPUT_TYPE,SUPPLIER_CODE,WAREHOUSE_CODE,PURCHASE_DATE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@INPUT_TYPE,@SUPPLIER_CODE,@WAREHOUSE_CODE,@PURCHASE_DATE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@PURCHASE_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = purchaseTable.SLIP_NUMBER;
            parameters[1].Value = purchaseTable.INPUT_TYPE;
            parameters[2].Value = purchaseTable.SUPPLIER_CODE;
            parameters[3].Value = purchaseTable.WAREHOUSE_CODE;
            parameters[4].Value = purchaseTable.PURCHASE_DATE;
            parameters[5].Value = purchaseTable.STATUS_FLAG;
            parameters[6].Value = purchaseTable.ATTRIBUTE1;
            parameters[7].Value = purchaseTable.ATTRIBUTE2;
            parameters[8].Value = purchaseTable.ATTRIBUTE3;
            parameters[9].Value = purchaseTable.CREATE_USER;
            parameters[10].Value = purchaseTable.LAST_UPDATE_USER;
            list.Add(new CommandInfo(strSql.ToString(), parameters));

            foreach (BllPurchaseLineTable purchaseLineTable in purchaseTable.PURCHASE_LINE)
            {
                //采购明细
                purchaseLineTable.SLIP_NUMBER = slipNumber;
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_PURCHASE_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,ARRIVAL_DATE,PRODUCT_CODE,UNIT_CODE,QUANTITY,PRICE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@ARRIVAL_DATE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@PRICE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] lineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
                lineParameters[0].Value = purchaseLineTable.SLIP_NUMBER;
                lineParameters[1].Value = purchaseLineTable.LINE_NUMBER;
                lineParameters[2].Value = purchaseLineTable.ARRIVAL_DATE;
                lineParameters[3].Value = purchaseLineTable.PRODUCT_CODE;
                lineParameters[4].Value = purchaseLineTable.UNIT_CODE;
                lineParameters[5].Value = purchaseLineTable.QUANTITY;
                lineParameters[6].Value = purchaseLineTable.PRICE;
                lineParameters[7].Value = purchaseLineTable.STATUS_FLAG;
                lineParameters[8].Value = purchaseLineTable.ATTRIBUTE1;
                lineParameters[9].Value = purchaseLineTable.ATTRIBUTE2;
                lineParameters[10].Value = purchaseLineTable.ATTRIBUTE3;
                list.Add(new CommandInfo(strSql.ToString(), lineParameters));

                //入库预定
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_RECEIVING_PLAN(");
                strSql.Append("PURCHASE_SLIP_NUMBER,PURCHASE_LINE_NUMBER,INPUT_TYPE,SUPPLIER_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PURCHASE_SLIP_NUMBER,@PURCHASE_LINE_NUMBER,@INPUT_TYPE,@SUPPLIER_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                SqlParameter[] receivingPlagParameters = {
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
                receivingPlagParameters[0].Value = purchaseTable.SLIP_NUMBER;
                receivingPlagParameters[1].Value = purchaseLineTable.LINE_NUMBER;
                receivingPlagParameters[2].Value = purchaseTable.INPUT_TYPE;
                receivingPlagParameters[3].Value = purchaseTable.SUPPLIER_CODE;
                receivingPlagParameters[4].Value = purchaseTable.PURCHASE_DATE;
                receivingPlagParameters[5].Value = purchaseLineTable.ARRIVAL_DATE;
                receivingPlagParameters[6].Value = purchaseTable.WAREHOUSE_CODE;
                receivingPlagParameters[7].Value = purchaseLineTable.PRODUCT_CODE;
                receivingPlagParameters[8].Value = purchaseLineTable.UNIT_CODE;
                receivingPlagParameters[9].Value = purchaseLineTable.QUANTITY;
                receivingPlagParameters[10].Value = CConstant.INIT;
                receivingPlagParameters[11].Value = "";
                receivingPlagParameters[12].Value = "";
                receivingPlagParameters[13].Value = "";
                receivingPlagParameters[14].Value = purchaseTable.CREATE_USER;
                receivingPlagParameters[15].Value = purchaseTable.LAST_UPDATE_USER;
                list.Add(new CommandInfo(strSql.ToString(), receivingPlagParameters));

                if (purchaseTable.INPUT_TYPE == CConstant.INPUT_TYPE_HEADER && purchaseTable.CHK_ITEM)
                {
                   
                    try
                    {
                        //原料的收集
                        DataSet ds = new ProductItemManage().GetItemList(purchaseLineTable.PRODUCT_CODE);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            decimal qty = purchaseLineTable.QUANTITY;
                            if(row["QUANTITY"] != null  && row["QUANTITY"].ToString() != "")
                            {
                                qty = qty * Convert.ToDecimal(row["QUANTITY"]);
                            }
                            //原料采购订单的做成
                            string childSlipNumber = CommonManage.GetSeq("PU");
                            strSql = new StringBuilder();
                            strSql.Append("insert into BLL_PURCHASE(");
                            strSql.Append("SLIP_NUMBER,PARENT_SLIP_NUMBER,PARENT_LINE_NUMBER,INPUT_TYPE,SUPPLIER_CODE,WAREHOUSE_CODE,PURCHASE_DATE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                            strSql.Append(" values (");
                            strSql.Append("@SLIP_NUMBER,@PARENT_SLIP_NUMBER,@PARENT_LINE_NUMBER,@INPUT_TYPE,@SUPPLIER_CODE,@WAREHOUSE_CODE,@PURCHASE_DATE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                            SqlParameter[] itemParameters = {
				                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
                                    new SqlParameter("@PARENT_SLIP_NUMBER", SqlDbType.VarChar,20),
				                    new SqlParameter("@PARENT_LINE_NUMBER", SqlDbType.Int,4),
				                    new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
				                    new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
				                    new SqlParameter("@WAREHOUSE_CODE", SqlDbType.NVarChar,20),
				                    new SqlParameter("@PURCHASE_DATE", SqlDbType.DateTime),
				                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
				                    new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
				                    new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
				                    new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
				                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
				                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                            itemParameters[0].Value = childSlipNumber;
                            itemParameters[1].Value = purchaseTable.SLIP_NUMBER;
                            itemParameters[2].Value = purchaseLineTable.LINE_NUMBER;
                            itemParameters[3].Value = CConstant.INPUT_TYPE_ITEM;
                            itemParameters[4].Value = purchaseTable.SUPPLIER_CODE;
                            itemParameters[5].Value = purchaseTable.WAREHOUSE_CODE;
                            itemParameters[6].Value = purchaseTable.PURCHASE_DATE;
                            itemParameters[7].Value = purchaseTable.STATUS_FLAG;
                            itemParameters[8].Value = purchaseTable.ATTRIBUTE1;
                            itemParameters[9].Value = purchaseTable.ATTRIBUTE2;
                            itemParameters[10].Value = purchaseTable.ATTRIBUTE3;
                            itemParameters[11].Value = purchaseTable.CREATE_USER;
                            itemParameters[12].Value = purchaseTable.LAST_UPDATE_USER;
                            list.Add(new CommandInfo(strSql.ToString(), itemParameters));

                            //原料采购明细
                            strSql = new StringBuilder();
                            strSql.Append("insert into BLL_PURCHASE_LINE(");
                            strSql.Append("SLIP_NUMBER,LINE_NUMBER,ARRIVAL_DATE,PRODUCT_CODE,UNIT_CODE,QUANTITY,PRICE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                            strSql.Append(" values (");
                            strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@ARRIVAL_DATE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@PRICE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                            SqlParameter[] itemLineParameters = {
					                new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					                new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					                new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					                new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					                new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					                new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					                new SqlParameter("@PRICE", SqlDbType.Decimal,9),
					                new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					                new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					                new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
                            itemLineParameters[0].Value = childSlipNumber;
                            itemLineParameters[1].Value = 1;
                            itemLineParameters[2].Value = purchaseLineTable.ARRIVAL_DATE;
                            itemLineParameters[3].Value = row["PRODUCT_CODE"];
                            itemLineParameters[4].Value = row["UNIT_CODE"];
                            itemLineParameters[5].Value = qty;
                            itemLineParameters[6].Value = 0; //PRICE;
                            itemLineParameters[7].Value = purchaseLineTable.STATUS_FLAG;
                            itemLineParameters[8].Value = purchaseLineTable.ATTRIBUTE1;
                            itemLineParameters[9].Value = purchaseLineTable.ATTRIBUTE2;
                            itemLineParameters[10].Value = purchaseLineTable.ATTRIBUTE3;
                            list.Add(new CommandInfo(strSql.ToString(), itemLineParameters));

                            //原料入库预定的做成
                            strSql = new StringBuilder();
                            strSql.Append("insert into BLL_RECEIVING_PLAN(");
                            strSql.Append("PURCHASE_SLIP_NUMBER,PURCHASE_LINE_NUMBER,INPUT_TYPE,SUPPLIER_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                            strSql.Append(" values (");
                            strSql.Append("@PURCHASE_SLIP_NUMBER,@PURCHASE_LINE_NUMBER,@INPUT_TYPE,@SUPPLIER_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                            SqlParameter[] itemReceivingPlagParameters = {
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
                            itemReceivingPlagParameters[0].Value = childSlipNumber;
                            itemReceivingPlagParameters[1].Value = 1;
                            itemReceivingPlagParameters[2].Value = CConstant.INPUT_TYPE_ITEM;
                            itemReceivingPlagParameters[3].Value = row["SUPPLIER_CODE"];
                            itemReceivingPlagParameters[4].Value = purchaseTable.PURCHASE_DATE;
                            itemReceivingPlagParameters[5].Value = purchaseLineTable.ARRIVAL_DATE;
                            itemReceivingPlagParameters[6].Value = row["SUPPLIER_WAREHOUSE_CODE"];
                            itemReceivingPlagParameters[7].Value = row["PRODUCT_CODE"];
                            itemReceivingPlagParameters[8].Value = row["UNIT_CODE"];
                            itemReceivingPlagParameters[9].Value = qty;
                            itemReceivingPlagParameters[10].Value = CConstant.INIT;
                            itemReceivingPlagParameters[11].Value = "";
                            itemReceivingPlagParameters[12].Value = "";
                            itemReceivingPlagParameters[13].Value = "";
                            itemReceivingPlagParameters[14].Value = purchaseTable.CREATE_USER;
                            itemReceivingPlagParameters[15].Value = purchaseTable.LAST_UPDATE_USER;
                            list.Add(new CommandInfo(strSql.ToString(), itemReceivingPlagParameters));
                           
                        }
                    }
                    catch (Exception ex) { }
                }
            }

            return DbHelperSQL.ExecuteSqlTran(list);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Update(BllPurchaseTable purchaseTable)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            //采购订单表头更新
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BLL_PURCHASE set ");
            strSql.Append("INPUT_TYPE=@INPUT_TYPE,");
            strSql.Append("SUPPLIER_CODE=@SUPPLIER_CODE,");
            strSql.Append("WAREHOUSE_CODE=@WAREHOUSE_CODE,");
            strSql.Append("PURCHASE_DATE=@PURCHASE_DATE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=GETDATE()");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@PURCHASE_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            parameters[0].Value = purchaseTable.INPUT_TYPE;
            parameters[1].Value = purchaseTable.SUPPLIER_CODE;
            parameters[2].Value = purchaseTable.WAREHOUSE_CODE;
            parameters[3].Value = purchaseTable.PURCHASE_DATE;
            parameters[4].Value = purchaseTable.STATUS_FLAG;
            parameters[5].Value = purchaseTable.ATTRIBUTE1;
            parameters[6].Value = purchaseTable.ATTRIBUTE2;
            parameters[7].Value = purchaseTable.ATTRIBUTE3;
            parameters[8].Value = purchaseTable.LAST_UPDATE_USER;
            parameters[9].Value = purchaseTable.SLIP_NUMBER;
            list.Add(new CommandInfo(strSql.ToString(), parameters));

            //原有采购订单明细删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_PURCHASE_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] deleteLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteLineParameters[0].Value = purchaseTable.SLIP_NUMBER;
            list.Add(new CommandInfo(strSql.ToString(), deleteLineParameters));

            //原有入库预定删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_RECEIVING_PLAN ");
            strSql.Append(" where PURCHASE_SLIP_NUMBER=@PURCHASE_SLIP_NUMBER");
            SqlParameter[] deletePlanParameters = {
					new SqlParameter("@PURCHASE_SLIP_NUMBER", SqlDbType.VarChar)
			};
            deletePlanParameters[0].Value = purchaseTable.SLIP_NUMBER;
            list.Add(new CommandInfo(strSql.ToString(), deletePlanParameters));


            foreach (BllPurchaseLineTable purchaseLineTable in purchaseTable.PURCHASE_LINE)
            {
                //采购订单明细更新
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_PURCHASE_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,ARRIVAL_DATE,PRODUCT_CODE,UNIT_CODE,QUANTITY,PRICE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@ARRIVAL_DATE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@PRICE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] lineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
                lineParameters[0].Value = purchaseLineTable.SLIP_NUMBER;
                lineParameters[1].Value = purchaseLineTable.LINE_NUMBER;
                lineParameters[2].Value = purchaseLineTable.ARRIVAL_DATE;
                lineParameters[3].Value = purchaseLineTable.PRODUCT_CODE;
                lineParameters[4].Value = purchaseLineTable.UNIT_CODE;
                lineParameters[5].Value = purchaseLineTable.QUANTITY;
                lineParameters[6].Value = purchaseLineTable.PRICE;
                lineParameters[7].Value = purchaseLineTable.STATUS_FLAG;
                lineParameters[8].Value = purchaseLineTable.ATTRIBUTE1;
                lineParameters[9].Value = purchaseLineTable.ATTRIBUTE2;
                lineParameters[10].Value = purchaseLineTable.ATTRIBUTE3;
                list.Add(new CommandInfo(strSql.ToString(), lineParameters));

                //入库预定增加
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_RECEIVING_PLAN(");
                strSql.Append("PURCHASE_SLIP_NUMBER,PURCHASE_LINE_NUMBER,INPUT_TYPE,SUPPLIER_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PURCHASE_SLIP_NUMBER,@PURCHASE_LINE_NUMBER,@INPUT_TYPE,@SUPPLIER_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
                SqlParameter[] receivingPlagParameters = {
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
                receivingPlagParameters[0].Value = purchaseTable.SLIP_NUMBER;
                receivingPlagParameters[1].Value = purchaseLineTable.LINE_NUMBER;
                receivingPlagParameters[2].Value = purchaseTable.INPUT_TYPE;
                receivingPlagParameters[3].Value = purchaseTable.SUPPLIER_CODE;
                receivingPlagParameters[4].Value = purchaseTable.PURCHASE_DATE;
                receivingPlagParameters[5].Value = purchaseLineTable.ARRIVAL_DATE;
                receivingPlagParameters[6].Value = purchaseTable.WAREHOUSE_CODE;
                receivingPlagParameters[7].Value = purchaseLineTable.PRODUCT_CODE;
                receivingPlagParameters[8].Value = purchaseLineTable.UNIT_CODE;
                receivingPlagParameters[9].Value = purchaseLineTable.QUANTITY;
                receivingPlagParameters[10].Value = 0;
                receivingPlagParameters[11].Value = "";
                receivingPlagParameters[12].Value = "";
                receivingPlagParameters[13].Value = "";
                receivingPlagParameters[14].Value = purchaseTable.CREATE_USER;
                receivingPlagParameters[15].Value = purchaseTable.LAST_UPDATE_USER;
                list.Add(new CommandInfo(strSql.ToString(), receivingPlagParameters));
            }

            return DbHelperSQL.ExecuteSqlTran(list);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Delete(string slipNumber)
        {
            List<CommandInfo> splList = new List<CommandInfo>();
            //入库预定删除
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BLL_RECEIVING_PLAN ");
            strSql.Append(" where PURCHASE_SLIP_NUMBER=@PURCHASE_SLIP_NUMBER");
            SqlParameter[] receivingPlanParameters = {
					new SqlParameter("@PURCHASE_SLIP_NUMBER", SqlDbType.VarChar)
			};
            receivingPlanParameters[0].Value = slipNumber;
            splList.Add(new CommandInfo(strSql.ToString(), receivingPlanParameters));

            //采购明细删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_PURCHASE_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] purchaseLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            purchaseLineParameters[0].Value = slipNumber;
            splList.Add(new CommandInfo(strSql.ToString(), purchaseLineParameters));

            //采购订单删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_PURCHASE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] purchaseParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)			};
            purchaseParameters[0].Value = slipNumber;
            splList.Add(new CommandInfo(strSql.ToString(), purchaseParameters));

            //事务执行
            return DbHelperSQL.ExecuteSqlTran(splList);
        }


        public DataSet GetPurchaseDetail(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM  bll_purchaseDetail_view ");
            strSql.AppendFormat(" WHERE SLIP_NUMBER ='{0}' ORDER BY LINE_NUMBER ", slipNumber);
            return DbHelperSQL.Query(strSql.ToString());
        }


        public BllPurchaseLineTable GetPurchaseLineMode(string slipNumber, int lineNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,LINE_NUMBER,ARRIVAL_DATE,PRODUCT_CODE,UNIT_CODE,QUANTITY,PRICE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3 from BLL_PURCHASE_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER and LINE_NUMBER=@LINE_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4)			};
            parameters[0].Value = slipNumber;
            parameters[1].Value = lineNumber;

            BllPurchaseLineTable model = new BllPurchaseLineTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"] != null && ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    model.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LINE_NUMBER"] != null && ds.Tables[0].Rows[0]["LINE_NUMBER"].ToString() != "")
                {
                    model.LINE_NUMBER = int.Parse(ds.Tables[0].Rows[0]["LINE_NUMBER"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ARRIVAL_DATE"] != null && ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString() != "")
                {
                    model.ARRIVAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRODUCT_CODE"] != null && ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString() != "")
                {
                    model.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UNIT_CODE"] != null && ds.Tables[0].Rows[0]["UNIT_CODE"].ToString() != "")
                {
                    model.UNIT_CODE = ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QUANTITY"] != null && ds.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
                {
                    model.QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRICE"] != null && ds.Tables[0].Rows[0]["PRICE"].ToString() != "")
                {
                    model.PRICE = decimal.Parse(ds.Tables[0].Rows[0]["PRICE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"] != null && ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE1"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString() != "")
                {
                    model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE2"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString() != "")
                {
                    model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE3"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString() != "")
                {
                    model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public int GetPurchaseCount() 
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT COUNT(*) FROM BLL_PURCHASE_LINE");
            int obj = DbHelperSQL.ExecuteSql(str.ToString());
            return obj;
        }

        #endregion
    }
}

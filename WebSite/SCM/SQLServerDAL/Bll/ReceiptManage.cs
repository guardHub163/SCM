using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using System.Data;
using SCM.DBUtility;
using SCM.Model;
using SCM.Common;
using System.Data.SqlClient;

namespace SCM.SQLServerDAL
{
    public partial class ReceiptManage : IReceipt
    {
        public ReceiptManage()
        { }

        #region IReceipt 成员

        /// <summary>
        /// 入库记录数的获得
        /// </summary>
        public int GetRecordCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_receipt_view ");
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
        /// 入库数据获得
        /// </summary>
        public DataSet GetReceiptList(string sqlWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from bll_receipt_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 入库返品记录数的获得
        /// </summary>
        public int GetReturnCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_receipt_return_view ");
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
        /// 入库返品数据获得
        /// </summary>
        public DataSet GetReturnList(string sqlWhere, string orderby, int startIndex, int endIndex)
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
            strSql.Append(")AS Row, T.*  from bll_receipt_return_view T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 入库明细获得
        /// </summary>
        public DataSet GetReceiptDetail(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM  bll_receipt_detail_view ");
            strSql.AppendFormat(" WHERE SLIP_NUMBER ='{0}' ORDER BY LINE_NUMBER ", slipNumber);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 入库主表实体的获得
        /// </summary>
        public BllReceiptTable GetReceiptModel(string SLIP_NUMBER)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,INPUT_TYPE,INPUT_TYPE_NAME,SUPPLIER_CODE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,SUPPLIER_NAME,WAREHOUSE_NAME,RECEIPT_TYPE from bll_Receipt_view ");
            strSql.Append("  where SLIP_NUMBER=@SLIP_NUMBER AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            parameters[0].Value = SLIP_NUMBER;

            BllReceiptTable model = new BllReceiptTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                if (ds.Tables[0].Rows[0]["INPUT_TYPE"].ToString() != "")
                {
                    model.INPUT_TYPE = int.Parse(ds.Tables[0].Rows[0]["INPUT_TYPE"].ToString());
                }
                model.Input_type_name = ds.Tables[0].Rows[0]["INPUT_TYPE_NAME"].ToString();
                model.SUPPLIER_CODE = ds.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString() != "")
                {
                    model.ARRIVAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString());
                }
                model.TO_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString();
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
                model.Supplier_name = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                model.Warehousename = ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString();
                if (ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString() != "")
                {
                    model.RECEIPT_TYPE = int.Parse(ds.Tables[0].Rows[0]["RECEIPT_TYPE"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 临时入库
        /// </summary>
        public int Insert(BllReceiptTable receiptTable)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            //入库Number
            string slipNumber = CommonManage.GetSeq("RC");
            receiptTable.SLIP_NUMBER = slipNumber;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BLL_RECEIPT(");
            strSql.Append("SLIP_NUMBER,RECEIPT_TYPE,INPUT_TYPE,SUPPLIER_CODE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@RECEIPT_TYPE,@INPUT_TYPE,@SUPPLIER_CODE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@RECEIPT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = receiptTable.SLIP_NUMBER;
            parameters[1].Value = receiptTable.RECEIPT_TYPE;
            parameters[2].Value = receiptTable.INPUT_TYPE;
            parameters[3].Value = receiptTable.SUPPLIER_CODE;
            parameters[4].Value = receiptTable.ARRIVAL_DATE;
            parameters[5].Value = receiptTable.TO_WAREHOUSE_CODE;
            parameters[6].Value = receiptTable.STATUS_FLAG;
            parameters[7].Value = receiptTable.ATTRIBUTE1;
            parameters[8].Value = receiptTable.ATTRIBUTE2;
            parameters[9].Value = receiptTable.ATTRIBUTE3;
            parameters[10].Value = receiptTable.CREATE_USER;
            parameters[11].Value = receiptTable.LAST_UPDATE_USER;
            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));


            foreach (BllReceiptLineTable receiptLineTable in receiptTable.ReceiptLine)
            {
                receiptLineTable.SLIP_NUMBER = slipNumber;
                //明细插入
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_RECEIPT_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,RECEIVING_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@RECEIVING_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] insertLineparameters = {
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
                insertLineparameters[0].Value = receiptLineTable.SLIP_NUMBER;
                insertLineparameters[1].Value = receiptLineTable.LINE_NUMBER;
                insertLineparameters[2].Value = receiptLineTable.RECEIVING_PLAN_SLIP_NUMBER;
                insertLineparameters[3].Value = receiptLineTable.PRODUCT_CODE;
                insertLineparameters[4].Value = receiptLineTable.UNIT_CODE;
                insertLineparameters[5].Value = receiptLineTable.QUANTITY;
                insertLineparameters[6].Value = receiptLineTable.STATUS_FLAG;
                insertLineparameters[7].Value = receiptLineTable.ATTRIBUTE1;
                insertLineparameters[8].Value = receiptLineTable.ATTRIBUTE2;
                insertLineparameters[9].Value = receiptLineTable.ATTRIBUTE3;

                sqlList.Add(new CommandInfo(strSql.ToString(), insertLineparameters));
                if (StockNumber(receiptLineTable.PRODUCT_CODE, receiptTable.TO_WAREHOUSE_CODE))
                {
                    //库存更新
                    if (receiptLineTable.QUANTITY != 0)
                    {
                        strSql = new StringBuilder();
                        strSql.Append("update BASE_STOCK set ");
                        strSql.Append("QUANTITY=(QUANTITY + @QUANTITY),");
                        strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                        strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                        strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
                        SqlParameter[] stockParameters = {
					new SqlParameter("@QUANTITY",SqlDbType.Decimal ,8),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                        stockParameters[0].Value = receiptLineTable.QUANTITY;
                        stockParameters[1].Value = receiptTable.LAST_UPDATE_USER;
                        stockParameters[2].Value = receiptTable.TO_WAREHOUSE_CODE;
                        stockParameters[3].Value = receiptLineTable.PRODUCT_CODE;

                        sqlList.Add(new CommandInfo(strSql.ToString(), stockParameters));
                    }
                }
                else
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into BASE_STOCK (");
                    strSql.Append("WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                    strSql.Append(" values(");
                    strSql.Append("@WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
                    SqlParameter[] stockParamenters = {
                    new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@QUANTITY", SqlDbType.VarChar,20),
                    new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@CREATE_USER",SqlDbType.NVarChar,255),
                    new SqlParameter("@LAST_UPDATE_USER",SqlDbType.NVarChar,255)
                                                  };
                    stockParamenters[0].Value = receiptTable.TO_WAREHOUSE_CODE;
                    stockParamenters[1].Value = receiptLineTable.PRODUCT_CODE;
                    stockParamenters[2].Value = receiptLineTable.UNIT_CODE;
                    stockParamenters[3].Value = receiptLineTable.QUANTITY;
                    stockParamenters[4].Value = "";
                    stockParamenters[5].Value = "";
                    stockParamenters[6].Value = "";
                    stockParamenters[7].Value = receiptTable.LAST_UPDATE_USER;
                    stockParamenters[8].Value = receiptTable.LAST_UPDATE_USER;
                    sqlList.Add(new CommandInfo(strSql.ToString(), stockParamenters));
                }
            }
            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        /// <summary>
        /// 临时入库修正
        /// </summary>
        public int Update(BllReceiptTable receiptable)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            //入库主表更新
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BLL_RECEIPT set ");
            strSql.Append("RECEIPT_TYPE=@RECEIPT_TYPE,");
            strSql.Append("INPUT_TYPE=@INPUT_TYPE,");
            strSql.Append("SUPPLIER_CODE=@SUPPLIER_CODE,");
            strSql.Append("ARRIVAL_DATE=@ARRIVAL_DATE,");
            strSql.Append("TO_WAREHOUSE_CODE=@TO_WAREHOUSE_CODE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=GETDATE()");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@RECEIPT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@INPUT_TYPE", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.NVarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime)};
            parameters[0].Value = receiptable.SLIP_NUMBER;
            parameters[1].Value = receiptable.RECEIPT_TYPE;
            parameters[2].Value = receiptable.INPUT_TYPE;
            parameters[3].Value = receiptable.SUPPLIER_CODE;
            parameters[4].Value = receiptable.ARRIVAL_DATE;
            parameters[5].Value = receiptable.TO_WAREHOUSE_CODE;
            parameters[6].Value = receiptable.STATUS_FLAG;
            parameters[7].Value = receiptable.ATTRIBUTE1;
            parameters[8].Value = receiptable.ATTRIBUTE2;
            parameters[9].Value = receiptable.ATTRIBUTE3;
            parameters[10].Value = receiptable.LAST_UPDATE_USER;
            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));

            //原有出库信息的获得
            DataSet dsDetail = GetReceiptDetail(receiptable.SLIP_NUMBER);
            //原有明细库存的还原
            foreach (DataRow row in dsDetail.Tables[0].Rows)
            {
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
                toStockParameters[1].Value = receiptable.LAST_UPDATE_USER;
                toStockParameters[2].Value = receiptable.TO_WAREHOUSE_CODE;
                toStockParameters[3].Value = row["PRODUCT_CODE"] != null ? Convert.ToString(row["PRODUCT_CODE"]) : "";
                sqlList.Add(new CommandInfo(strSql.ToString(), toStockParameters));
            }


            //原有明细的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_RECEIPT_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] deleteLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteLineParameters[0].Value = receiptable.SLIP_NUMBER;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteLineParameters));

            //新的明细的保存
            foreach (BllReceiptLineTable receiptLineTable in receiptable.ReceiptLine)
            {
                //明细插入
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_RECEIPT_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,RECEIVING_PLAN_SLIP_NUMBER,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@RECEIVING_PLAN_SLIP_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] insertLineparameters = {
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
                insertLineparameters[0].Value = receiptLineTable.SLIP_NUMBER;
                insertLineparameters[1].Value = receiptLineTable.LINE_NUMBER;
                insertLineparameters[2].Value = receiptLineTable.RECEIVING_PLAN_SLIP_NUMBER;
                insertLineparameters[3].Value = receiptLineTable.PRODUCT_CODE;
                insertLineparameters[4].Value = receiptLineTable.UNIT_CODE;
                insertLineparameters[5].Value = receiptLineTable.QUANTITY;
                insertLineparameters[6].Value = receiptLineTable.STATUS_FLAG;
                insertLineparameters[7].Value = receiptLineTable.ATTRIBUTE1;
                insertLineparameters[8].Value = receiptLineTable.ATTRIBUTE2;
                insertLineparameters[9].Value = receiptLineTable.ATTRIBUTE3;

                sqlList.Add(new CommandInfo(strSql.ToString(), insertLineparameters));

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
                toStockParameters[0].Value = receiptLineTable.QUANTITY;
                toStockParameters[1].Value = receiptable.LAST_UPDATE_USER;
                toStockParameters[2].Value = receiptable.TO_WAREHOUSE_CODE;
                toStockParameters[3].Value = receiptLineTable.PRODUCT_CODE;
                sqlList.Add(new CommandInfo(strSql.ToString(), toStockParameters));
            }

            return DbHelperSQL.ExecuteSqlTran(sqlList);
        }

        /// <summary>
        /// 临时入库删除
        /// </summary>
        public int Delete(string slipNumber, string userId)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            //原有入库信息的获得
            DataSet dsDetail = GetReceiptDetail(slipNumber);
            //原有明细库存的还原
            foreach (DataRow row in dsDetail.Tables[0].Rows)
            {
                //库存的减少
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
            strSql.Append("delete from BLL_RECEIPT_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] deleteLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteLineParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteLineParameters));

            //出库主表信息的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_RECEIPT ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] deleteHeaderParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteHeaderParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteHeaderParameters));

            return DbHelperSQL.ExecuteSqlTran(sqlList);

        }

        /// <summary>
        /// 库存查询
        /// </summary>
        public bool StockNumber(string proudctcode, string warehousecode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT count(1) FROM  BASE_STOCK ");
            strSql.AppendFormat(" WHERE WAREHOUSE_CODE =@WAREHOUSE_CODE AND PRODUCT_CODE=@PRODUCT_CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
            parameters[0].Value = warehousecode;
            parameters[1].Value = proudctcode;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        ///<summary>
        ///入库预定信息的查询
        ///</summary>
        public DataSet ReceiptInfo() 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 10 TIP.*,BW.NAME AS TO_WAREHOUSE_NAME FROM ");
            strSql.Append("(SELECT ARRIVAL_DATE,TO_WAREHOUSE_CODE,SUM(QUANTITY) AS SUM_QUANTITY FROM dbo.BLL_RECEIVING_PLAN GROUP BY TO_WAREHOUSE_CODE,ARRIVAL_DATE) AS TIP ");
            strSql.Append("LEFT JOIN dbo.BASE_WAREHOUSE AS BW ON BW.CODE=TIP.TO_WAREHOUSE_CODE ");
            strSql.Append("WHERE TIP.ARRIVAL_DATE BETWEEN Convert(varchar(10),getDate(),120) AND Convert(varchar(10),getDate()+1,120) ");
            strSql.Append("ORDER BY TIP.ARRIVAL_DATE asc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        ///<summary>
        ///出库预定信息查询
        ///</summary>
        public DataSet TransferOutInfo() 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 10 TIP.*,BW.NAME AS FROM_WAREHOUSE_NAME FROM ");
            strSql.Append("(SELECT DEPARTUAL_DATE,FROM_WAREHOUSE_CODE,SUM(QUANTITY) AS SUM_QUANTITY FROM dbo.BLL_TRANSFER_IN_PLAN GROUP BY FROM_WAREHOUSE_CODE,DEPARTUAL_DATE) AS TIP ");
            strSql.Append("LEFT JOIN dbo.BASE_WAREHOUSE AS BW ON BW.CODE=TIP.FROM_WAREHOUSE_CODE ");
            strSql.Append("WHERE TIP.DEPARTUAL_DATE BETWEEN Convert(varchar(10),getDate(),120) AND Convert(varchar(10),getDate()+1,120) ");
            strSql.Append("ORDER BY TIP.DEPARTUAL_DATE asc");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}

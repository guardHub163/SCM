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
    public partial class StockManage : IStock
    {
        public StockManage() { }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string WAREHOUSE_CODE, string PRODUCT_CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_STOCK");
            strSql.Append(" where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = WAREHOUSE_CODE;
            parameters[1].Value = PRODUCT_CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public DataSet GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) AS CODE,sum(QUANTITY) AS STOCK,sum(SP_QUANTITY) AS ENTERSTOCK,sum(RP_QUANTITY) AS OUTSTOCK FROM bll_stock_view ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());


        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.WAREHOUSE_CODE desc");
            }
            strSql.Append(")AS Row, T.* from bll_stock_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetStockHistoryCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_stockhistory_view ");
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

        public DataSet GetStockQuantity(string warehouse, string product)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT QUANTITY FROM BASE_STOCK WHERE WAREHOUSE_CODE='{0}' AND PRODUCT_CODE='{1}'", warehouse, product);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetStockInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM bll_stock_view WHERE " + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetStockHistoryList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.WAREHOUSE_CODE desc");
            }
            strSql.Append(")AS Row, T.* from bll_stockhistory_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public BllStockTable GetModel(string warehouse_code, string product)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from bll_stock_view ");
            if (warehouse_code.Trim() != "")
            {
                strSql.Append("where WAREHOUSE_CODE =@WAREHOUSE_CODE");
            }
            if (product.Trim() != "")
            {
                strSql.Append(" and PRODUCT_CODE=@PRODUCT_CODE");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,50),
                    new SqlParameter("@PRODUCT_CODE",SqlDbType.VarChar,50)};
            parameters[0].Value = warehouse_code;
            parameters[1].Value = product;
            BllStockTable model = new BllStockTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.WAREHOUSE_CODE = ds.Tables[0].Rows[0]["WAREHOUSE_CODE"].ToString();
                model.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
                {
                    model.QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SP_QUANTITY"].ToString() != "")
                {
                    model.SP_QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["SP_QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RP_QUANTITY"].ToString() != "")
                {
                    model.RP_QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["RP_QUANTITY"].ToString());
                }
                model.WAREHOUSE_NAME = ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString();
                model.PRODUCT_NAME = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                model.PRODUCT_GROUP_CODE = ds.Tables[0].Rows[0]["PRODUCT_GROUP_CODE"].ToString();
                model.PRODUCT_GROUP_NAME = ds.Tables[0].Rows[0]["PRODUCT_GROUP_NAME"].ToString();
                model.UNIT_CODE = ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
                model.UNIT_NAME = ds.Tables[0].Rows[0]["UNIT_NAME"].ToString();
                model.STYLE_CODE = ds.Tables[0].Rows[0]["STYLE_CODE"].ToString();
                model.STYLE_NAME = ds.Tables[0].Rows[0]["STYLE_NAME"].ToString();
                model.COLOR_CODE = ds.Tables[0].Rows[0]["COLOR_CODE"].ToString();
                model.COLOR_NAME = ds.Tables[0].Rows[0]["COLOR_NAME"].ToString();
                model.SIZE_CODE = ds.Tables[0].Rows[0]["SIZE_CODE"].ToString();
                model.SIZE_NAME = ds.Tables[0].Rows[0]["SIZE_NAME"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        public int Update(BllStockTable stockTable)
        {

            List<CommandInfo> list = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select count(*) from BASE_STOCK where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE");
            //SqlParameter[] parameterste = {
            //        new SqlParameter("@WAREHOUSE_CODE", SqlDbType.NVarChar,255),
            //        new SqlParameter("@PRODUCT_CODE", SqlDbType.NVarChar,225)
            //                            };
            //parameterste[0].Value = stockTable.WAREHOUSE_CODE;
            //parameterste[1].Value = stockTable.PRODUCT_CODE;
            //object row = DbHelperSQL.GetSingle(strSql.ToString(), parameterste);
            if (Exists(stockTable.WAREHOUSE_CODE, stockTable.PRODUCT_CODE))
            {

                strSql = new StringBuilder();
                strSql.Append("update BASE_STOCK set QUANTITY=@QUANTITY where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE");
                SqlParameter[] parameters = {
					new SqlParameter("@QUANTITY", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.NVarChar,255),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.NVarChar,225)
                                        };
                parameters[0].Value = Convert.ToDecimal(stockTable.Lastquantity);
                parameters[1].Value = stockTable.WAREHOUSE_CODE;
                parameters[2].Value = stockTable.PRODUCT_CODE;
                list.Add(new CommandInfo(strSql.ToString(), parameters));
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
                stockParamenters[0].Value = stockTable.WAREHOUSE_CODE;
                stockParamenters[1].Value = stockTable.PRODUCT_CODE;
                stockParamenters[2].Value = stockTable.UNIT_CODE;
                stockParamenters[3].Value = stockTable.Lastquantity;
                stockParamenters[4].Value = "";
                stockParamenters[5].Value = "";
                stockParamenters[6].Value = "";
                stockParamenters[7].Value = stockTable.Creat_name;
                stockParamenters[8].Value = stockTable.Creat_name;
                list.Add(new CommandInfo(strSql.ToString(), stockParamenters));
            }

            strSql = new StringBuilder();
            strSql.Append("insert into BASE_STOCK_HISTORY (");
            strSql.Append("WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,FROM_QUANTITY,DIFF_QUANTITY,TO_QUANTITY");
            strSql.Append(",REASON ,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values(");
            strSql.Append("@WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@FROM_QUANTITY,@DIFF_QUANTITY,@TO_QUANTITY,@REASON,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
            SqlParameter[] lineParameters = {
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@FROM_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@DIFF_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@TO_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@REASON", SqlDbType.VarChar,20),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@CREATE_USER", SqlDbType.NVarChar,255),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.NVarChar,255)
        	                       };
            lineParameters[0].Value = stockTable.WAREHOUSE_CODE;
            lineParameters[1].Value = stockTable.PRODUCT_CODE;
            lineParameters[2].Value = stockTable.UNIT_CODE;
            lineParameters[3].Value = stockTable.QUANTITY;
            lineParameters[4].Value = stockTable.Lastquantity;
            lineParameters[5].Value = stockTable.Toquantity;
            lineParameters[6].Value = stockTable.Reason;
            lineParameters[7].Value = "";
            lineParameters[8].Value = "";
            lineParameters[9].Value = "";
            lineParameters[10].Value = stockTable.Creat_name;
            lineParameters[11].Value = stockTable.Creat_name;
            list.Add(new CommandInfo(strSql.ToString(), lineParameters));

            return DbHelperSQL.ExecuteSqlTran(list);
        }

        public DataSet Show(string warehouse_code, string product)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from bll_stock_detail_view");
            if (warehouse_code.Trim() != "")
            {
                strSql.Append(" where WAREHOUSE_CODE ='" + warehouse_code + "'");
            }
            if (product.Trim() != "")
            {
                strSql.Append(" and PRODUCT_CODE ='" + product + "'");
            }
            strSql.Append(" order by opt_date");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 盘点
        public int GetInventoryScheduleRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_inventory_schedule_view ");
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

        public DataSet GetInventoryScheduleList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.slip_number");
            }
            strSql.Append(")AS Row, T.* from base_inventory_schedule_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetInventoryScheduleInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM base_inventory_schedule_view WHERE" + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetInventoryInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM base_inventory_view WHERE " + strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetInventoryMaxLineNumber(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT MAX(LINE_NUMBER) AS LINE_NUMBER FROM BASE_INVENTORY WHERE SLIP_NUMBER='{0}'", slipNumber);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetInventoryRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_inventory_view ");
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

        public DataSet GetInventoryList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.product_code");
            }
            strSql.Append(")AS Row, T.* from base_inventory_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int InsertInventory(string warehouseCode, string product_group_code, string userId)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            string slipNumber = CommonManage.GetSeq("INV");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_INVENTORY_SCHEDULE(");
            strSql.Append("SLIP_NUMBER,WAREHOUSE_CODE,INVENTORY_START_DATE,STATUS_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER,PRODUCT_GROUP_CODE)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@WAREHOUSE_CODE,@INVENTORY_START_DATE,@STATUS_FLAG,GETDATE(),@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,@PRODUCT_GROUP_CODE)");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@INVENTORY_START_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@PRODUCT_GROUP_CODE",SqlDbType.VarChar,20)};
            parameters[0].Value = slipNumber;
            parameters[1].Value = warehouseCode;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = CConstant.INIT;
            parameters[4].Value = userId;
            parameters[5].Value = userId;
            parameters[6].Value = product_group_code;
            list.Add(new CommandInfo(strSql.ToString(), parameters));

            strSql = new StringBuilder();
            strSql.Append("insert into BASE_INVENTORY(");
            strSql.Append("SLIP_NUMBER,LINE_NUMBER,PRODUCT_CODE,UNIT_CODE,INVENTORY,REAL_INVENTORY,STATUS_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.AppendFormat("SELECT @SLIP_NUMBER,ROW_NUMBER()OVER(ORDER BY PRODUCT_CODE),PRODUCT_CODE,BS.UNIT_CODE AS UNIT_CDOE,QUANTITY,0,{0},GETDATE(),'{1}',GETDATE(),'{2}'", CConstant.INIT, userId, userId);
            strSql.Append("FROM BASE_STOCK BS LEFT JOIN BASE_PRODUCT BP ON BP.CODE=BS.PRODUCT_CODE WHERE BS.WAREHOUSE_CODE = @WAREHOUSE_CODE AND BP.GROUP_CODE=@GROUP_CODE");
            SqlParameter[] lineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
                     new SqlParameter("@GROUP_CODE", SqlDbType.VarChar,20)                       };
            lineParameters[0].Value = slipNumber;
            lineParameters[1].Value = warehouseCode;
            lineParameters[2].Value = product_group_code;
            list.Add(new CommandInfo(strSql.ToString(), lineParameters));

            return DbHelperSQL.ExecuteSqlTran(list);
        }

        public int AddInventory(BllInventoryTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_INVENTORY(");
            strSql.Append("SLIP_NUMBER,LINE_NUMBER,PRODUCT_CODE,UNIT_CODE,INVENTORY,REAL_INVENTORY,STATUS_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@INVENTORY,@REAL_INVENTORY,@STATUS_FLAG,getdate(),@CREATE_USER,getdate(),@LAST_UPDATE_USER)");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@INVENTORY", SqlDbType.Decimal,9),
					new SqlParameter("@REAL_INVENTORY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = model.SLIP_NUMBER;
            parameters[1].Value = model.LINE_NUMBER;
            parameters[2].Value = model.PRODUCT_CODE;
            parameters[3].Value = model.UNIT_CODE;
            parameters[4].Value = model.INVENTORY;
            parameters[5].Value = model.REAL_INVENTORY;
            parameters[6].Value = model.STATUS_FLAG;
            parameters[7].Value = model.CREATE_USER;
            parameters[8].Value = model.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public BaseInventoryScheduleTable GetInventoryScheduleMode(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,WAREHOUSE_CODE,INVENTORY_START_DATE,INVENTORY_END_DATE,STATUS_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER,WAREHOUSE_NAME,STATUS_NAME,GROUP_NAME from base_inventory_schedule_view ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            parameters[0].Value = slipNumber;

            BaseInventoryScheduleTable isTable = new BaseInventoryScheduleTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"] != null && ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    isTable.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WAREHOUSE_CODE"] != null && ds.Tables[0].Rows[0]["WAREHOUSE_CODE"].ToString() != "")
                {
                    isTable.WAREHOUSE_CODE = ds.Tables[0].Rows[0]["WAREHOUSE_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["INVENTORY_START_DATE"] != null && ds.Tables[0].Rows[0]["INVENTORY_START_DATE"].ToString() != "")
                {
                    isTable.INVENTORY_START_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["INVENTORY_START_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["INVENTORY_END_DATE"] != null && ds.Tables[0].Rows[0]["INVENTORY_END_DATE"].ToString() != "")
                {
                    isTable.INVENTORY_END_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["INVENTORY_END_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"] != null && ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    isTable.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"] != null && ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    isTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CREATE_USER"] != null && ds.Tables[0].Rows[0]["CREATE_USER"].ToString() != "")
                {
                    isTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    isTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_USER"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString() != "")
                {
                    isTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WAREHOUSE_NAME"] != null && ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString() != "")
                {
                    isTable.WAREHOUSE_NAME = ds.Tables[0].Rows[0]["WAREHOUSE_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GROUP_NAME"] != null && ds.Tables[0].Rows[0]["GROUP_NAME"].ToString() != "")
                {
                    isTable.GROUP_NAME = ds.Tables[0].Rows[0]["GROUP_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["STATUS_NAME"] != null && ds.Tables[0].Rows[0]["STATUS_NAME"].ToString() != "")
                {
                    isTable.STATUS_NAME = ds.Tables[0].Rows[0]["STATUS_NAME"].ToString();
                }
                return isTable;
            }
            else
            {
                return null;
            }
        }

        #endregion


        #region IStock 成员


        public int UpdateInventory(string slipNumber, Hashtable ht, int statusFlag, string userId)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            if (ht != null)
            {
                foreach (DictionaryEntry de in ht)
                {
                    strSql = new StringBuilder();
                    strSql.Append("update BASE_INVENTORY set ");
                    strSql.Append("REAL_INVENTORY=@REAL_INVENTORY,");
                    strSql.Append("LAST_UPDATE_TIME=GETDATE(),");
                    strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                    strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER and LINE_NUMBER=@LINE_NUMBER ");
                    SqlParameter[] parameters = {
					new SqlParameter("@REAL_INVENTORY", SqlDbType.Decimal,9),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4)};
                    parameters[0].Value = de.Value;
                    parameters[1].Value = userId;
                    parameters[2].Value = slipNumber;
                    parameters[3].Value = de.Key;
                    list.Add(new CommandInfo(strSql.ToString(), parameters));
                }
            }
            int ret = 0;
            if (list.Count > 0)
            {
                ret = DbHelperSQL.ExecuteSqlTran(list);
            }
            else
            {
                ret = 1;
            }

            if (statusFlag == 1 && ret > 0)
            {
                list = new List<CommandInfo>();
                strSql = new StringBuilder();
                strSql.Append(" SELECT BIS.SLIP_NUMBER,BIS.WAREHOUSE_CODE,BI.LINE_NUMBER,BI.PRODUCT_CODE,BI.UNIT_CODE,BI.REAL_INVENTORY-BI.INVENTORY AS DIFF_INVENTORY ");
                strSql.Append(" FROM BASE_INVENTORY_SCHEDULE BIS ");
                strSql.Append(" LEFT JOIN BASE_INVENTORY AS BI ON BIS.SLIP_NUMBER  = BI.SLIP_NUMBER ");
                strSql.Append(" WHERE BI.REAL_INVENTORY-INVENTORY <> 0 ");
                strSql.AppendFormat(" AND BIS.SLIP_NUMBER = '{0}'", slipNumber);
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //原有库存的取得
                    BllStockTable stockTable = GetModel(row["WAREHOUSE_CODE"].ToString(), row["PRODUCT_CODE"].ToString());
                    //库存更新
                    strSql = new StringBuilder();
                    strSql.Append("update BASE_STOCK set QUANTITY=QUANTITY+@QUANTITY where WAREHOUSE_CODE=@WAREHOUSE_CODE and PRODUCT_CODE=@PRODUCT_CODE");
                    SqlParameter[] parameters = {
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.NVarChar,255),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.NVarChar,225)};
                    parameters[0].Value = row["DIFF_INVENTORY"];
                    parameters[1].Value = row["WAREHOUSE_CODE"];
                    parameters[2].Value = row["PRODUCT_CODE"];
                    list.Add(new CommandInfo(strSql.ToString(), parameters));

                    //库存记录的保存
                    strSql = new StringBuilder();
                    strSql.Append("insert into BASE_STOCK_HISTORY (");
                    strSql.Append("WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,FROM_QUANTITY,DIFF_QUANTITY,TO_QUANTITY");
                    strSql.Append(",REASON ,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                    strSql.Append(" values(");
                    strSql.Append("@WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@FROM_QUANTITY,@DIFF_QUANTITY,@TO_QUANTITY,@REASON,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
                    SqlParameter[] historyParameters = {
					new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@FROM_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@DIFF_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@TO_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@REASON", SqlDbType.VarChar,20),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
                    new SqlParameter("@CREATE_USER", SqlDbType.NVarChar,255),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.NVarChar,255)};
                    historyParameters[0].Value = row["WAREHOUSE_CODE"];
                    historyParameters[1].Value = row["PRODUCT_CODE"];
                    historyParameters[2].Value = row["UNIT_CODE"];
                    historyParameters[3].Value = stockTable.QUANTITY;
                    historyParameters[4].Value = row["DIFF_INVENTORY"];
                    historyParameters[5].Value = stockTable.QUANTITY + Convert.ToDecimal(row["DIFF_INVENTORY"]);
                    historyParameters[6].Value = "99";
                    historyParameters[7].Value = "";
                    historyParameters[8].Value = "";
                    historyParameters[9].Value = "";
                    historyParameters[10].Value = userId;
                    historyParameters[11].Value = userId;
                    list.Add(new CommandInfo(strSql.ToString(), historyParameters));
                }
                //盘点状态的修改
                strSql = new StringBuilder();
                strSql.Append("update BASE_INVENTORY_SCHEDULE set ");
                strSql.Append("INVENTORY_END_DATE=@INVENTORY_END_DATE,");
                strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE(),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
                SqlParameter[] headParameters = {
					new SqlParameter("@INVENTORY_END_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
                headParameters[0].Value = DateTime.Now;
                headParameters[1].Value = CConstant.NORMAL;
                headParameters[2].Value = userId;
                headParameters[3].Value = slipNumber;
                list.Add(new CommandInfo(strSql.ToString(), headParameters));

                //盘点明细状态的修改
                strSql = new StringBuilder();
                strSql.Append("update BASE_INVENTORY set ");
                strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE(),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
                strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
                SqlParameter[] lineParameters = {
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
                lineParameters[0].Value = CConstant.NORMAL;
                lineParameters[1].Value = userId;
                lineParameters[2].Value = slipNumber;
                list.Add(new CommandInfo(strSql.ToString(), lineParameters));

                ret = DbHelperSQL.ExecuteSqlTran(list);
            }
            return ret;
        }

        public int DeleteInventory(string slipNumber)
        {
            List<CommandInfo> list = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BASE_INVENTORY_SCHEDULE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] headParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)			};
            headParameters[0].Value = slipNumber;
            list.Add(new CommandInfo(strSql.ToString(), headParameters));

            strSql = new StringBuilder();
            strSql.Append("delete from BASE_INVENTORY ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] lineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)	};
            lineParameters[0].Value = slipNumber;
            list.Add(new CommandInfo(strSql.ToString(), lineParameters));

            return DbHelperSQL.ExecuteSqlTran(list);
        }

        #endregion


        /// <summary>
        /// 获得服装行业的库存
        /// </summary>
        public DataSet GetStockClothingList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT T.* FROM ( ");
            strSql.Append("SELECT  ");
            strSql.Append("BP.STYLE AS STYLE_CODE, ");
            strSql.Append("BP.COLOR AS COLOR_CODE, ");
            strSql.Append("BC.NAME AS COLOR_NAME, ");
            strSql.Append("BP.SIZE AS SIZE_CODE, ");
            strSql.Append("SY.NAME AS STYLE_NAME, ");
            //strSql.Append("BS.PRODUCT_CODE, ");
            strSql.Append("ISNULL(SUM(BS.QUANTITY),0) AS STOCK ");
            strSql.Append("FROM BASE_STOCK AS BS ");
            strSql.Append("LEFT JOIN BASE_PRODUCT AS BP ON BS.PRODUCT_CODE  = BP.CODE ");
            strSql.Append("LEFT JOIN BASE_STYLE AS SY ON BP.STYLE= SY.CODE ");
            strSql.Append("LEFT JOIN BASE_COLOR AS BC ON BP.COLOR = BC.CODE ");
            strSql.Append("WHERE  ");
            strSql.Append("BP.STATUS_FLAG <> 9 ");
            strSql.Append(strWhere);
            //strSql.Append("--AND WAREHOUSE_CODE = 'W0102' ");
            //strSql.Append("GROUP BY BS.PRODUCT_CODE,BP.STYLE,SY.NAME,BP.COLOR,BC.NAME,BP.SIZE ");
            strSql.Append("GROUP BY BP.STYLE,SY.NAME,BP.COLOR,BC.NAME,BP.SIZE ");
            strSql.Append(") AS T  ");
            strSql.Append("ORDER BY T.STYLE_CODE,T.COLOR_CODE,T.SIZE_CODE ");

            return DbHelperSQL.Query(strSql.ToString());

            //
        }

    }//end class
}

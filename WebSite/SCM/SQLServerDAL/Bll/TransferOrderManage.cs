using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.Model;
using SCM.DBUtility;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public class TransferOrderManage : ITransferOrder
    {
        #region ITransferOrder 成员

        public int UpdateLine(List<BllTransferOrderLineTable> list)
        {
            int ret = 0;
            //Hashtable sqlList = new Hashtable();
            List<CommandInfo> sqlList = new List<CommandInfo>();
            bool first = true;
            StringBuilder strSql = null;
            foreach (BllTransferOrderLineTable lineTable in list)
            {
                if (first)
                {
                    strSql = new StringBuilder();
                    strSql.Append("delete from BLL_TRANSFER_ORDER_LINE ");
                    strSql.Append(" where ORDER_ID=@ORDER_ID");
                    SqlParameter[] param = {
					    new SqlParameter("@ORDER_ID", SqlDbType.Decimal)};
                    param[0].Value = lineTable.ORDER_ID;
                    //sqlList.Add(strSql.ToString(), param);
                    sqlList.Add(new CommandInfo(strSql.ToString(), param));
                    first = false;
                }

                strSql = new StringBuilder();
                strSql.Append("insert into BLL_TRANSFER_ORDER_LINE(");
                strSql.Append("ORDER_ID,LINE_NUMBER,SLIP_NUMBER,TO_WAREHOUSE_CODE,QUANTITY,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3)");
                strSql.Append(" values (");
                strSql.Append("@ORDER_ID,@LINE_NUMBER,@SLIP_NUMBER,@TO_WAREHOUSE_CODE,@QUANTITY,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3)");
                SqlParameter[] parameters = {
					new SqlParameter("@ORDER_ID", SqlDbType.Decimal,9),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.NVarChar,255),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255)};
                parameters[0].Value = lineTable.ORDER_ID;
                parameters[1].Value = lineTable.LINE_NUMBER;
                parameters[2].Value = lineTable.SLIP_NUMBER;
                parameters[3].Value = lineTable.TO_WAREHOUSE_CODE;
                parameters[4].Value = lineTable.QUANTITY;
                parameters[5].Value = lineTable.ATTRIBUTE1;
                parameters[6].Value = lineTable.ATTRIBUTE2;
                parameters[7].Value = lineTable.ATTRIBUTE3;
                sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
                //sqlList.Add(strSql.ToString(), parameters);
            }


            try
            {
                ret = DbHelperSQL.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex) { }
            return ret;
        }

        public int InsertOrder(BllTransferOrderTable orderTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO BLL_TRANSFER_ORDER(SLIP_NUMBER,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,PRODUCT_CODE,UNIT_CODE,STATUS_FLAG,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME) ");
            strSql.Append(" SELECT @SLIP_NUMBER,@FROM_WAREHOUSE_CODE,@DEPARTUAL_DATE,PRODUCT_CODE,UNIT_CODE,@STATUS_FLAG,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate() FROM warehouse_stock_view");
            strSql.Append(" WHERE WAREHOUSE_CODE = @WAREHOUSE_CODE AND STOCK > 0");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@DEPARTUAL_DATE", SqlDbType.DateTime,8),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,50),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,50),
                    new SqlParameter("@WAREHOUSE_CODE", SqlDbType.VarChar,50)
					};
            parameters[0].Value = orderTable.SLIP_NUMBER;
            parameters[1].Value = orderTable.FROM_WAREHOUSE_CODE;
            parameters[2].Value = orderTable.DEPARTUAL_DATE;
            parameters[3].Value = orderTable.STATUS_FLAG;
            parameters[4].Value = orderTable.CREATE_USER;
            parameters[5].Value = orderTable.LAST_UPDATE_USER;
            parameters[6].Value = orderTable.FROM_WAREHOUSE_CODE;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        public int UpdateOrder(BllTransferOrderTable orderTable)
        {
            throw new NotImplementedException();
        }

        public int DeleteOrder(string slipNumber)
        {
            int ret = 0;

            List<CommandInfo> sqlList = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();

            //明细表的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_TRANSFER_ORDER_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] lineParameters = {
					    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar)};
            lineParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), lineParameters));

            //主表的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_TRANSFER_ORDER ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] orderParameters = {
					    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar)};
            orderParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), orderParameters));

            try
            {
                ret = DbHelperSQL.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex) { }
            return ret;
        }

        public int DeleteLine(decimal orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BLL_TRANSFER_ORDER_LINE ");
            strSql.Append(" where ORDER_ID=@ORDER_ID");
            SqlParameter[] param = {
					    new SqlParameter("@ORDER_ID", SqlDbType.Decimal)};
            param[0].Value = orderId;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), param);
        }
        /// <summary>
        /// 实体类的获得
        /// </summary>
        public BllTransferOrderTable GetBllTransferOrderTable(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,SLIP_NUMBER,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME from BLL_TRANSFER_ORDER ");
            if (sqlWhere.Trim() != "")
            {
                strSql.Append(" where " + sqlWhere);
            }

            BllTransferOrderTable orderTable = new BllTransferOrderTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    orderTable.ID = decimal.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"] != null && ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    orderTable.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"] != null && ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"].ToString() != "")
                {
                    orderTable.FROM_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DEPARTUAL_DATE"] != null && ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString() != "")
                {
                    orderTable.DEPARTUAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRODUCT_CODE"] != null && ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString() != "")
                {
                    orderTable.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UNIT_CODE"] != null && ds.Tables[0].Rows[0]["UNIT_CODE"].ToString() != "")
                {
                    orderTable.UNIT_CODE = ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QUANTITY"] != null && ds.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
                {
                    orderTable.QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"] != null && ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    orderTable.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE1"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString() != "")
                {
                    orderTable.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE2"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString() != "")
                {
                    orderTable.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE3"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString() != "")
                {
                    orderTable.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_USER"] != null && ds.Tables[0].Rows[0]["CREATE_USER"].ToString() != "")
                {
                    orderTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"] != null && ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    orderTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_USER"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString() != "")
                {
                    orderTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    orderTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                return orderTable;
            }
            else
            {
                return null;
            }
        }

        #region　配分分组数据
        /// <summary>
        /// 获取单张配分单的商品配分记录总数
        /// </summary>
        public int GetOrderGroupRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) ");
            strSql.Append(" from bll_transfer_order_group_view  ");
            strSql.AppendFormat(" where {0}", strWhere);
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
        /// 配分查询，配分单分组单条数据获得
        /// </summary>
        public DataSet GetOrderGroupList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * ");
            strSql.Append(" FROM bll_transfer_order_group_view  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 配分查询，配分单分组分页数据获得
        /// </summary>
        public DataSet GetOrderGroupList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ( ");
            strSql.Append(" select row_number() over (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by t." + orderby);
            }
            strSql.Append(")as ROW, t.*  from bll_transfer_order_group_view t ");
            strSql.Append(" where " + strWhere);
            strSql.Append(" ) tt");
            strSql.AppendFormat(" where tt.ROW between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 商品配分明细
        /// <summary>
        /// 出库配分,商品载入记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from load_assign_view ");
            strSql.AppendFormat(" where {0}", strWhere);
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
        /// 出库配分,商品载入数据分页获得
        /// </summary>
        public DataSet GetTransferOutAssignInfo(string strWhere, string orderby, int startIndex, int endIndex, string departureDate, string warehouseCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT X.* ");
            strSql.Append(",ISNULL(Z.QUANTITY,0) AS IN_PLAN_QUANTITY ");
            strSql.Append(",X.STOCK-X.OUT_PLAN_QUANTITY+ISNULL(Z.QUANTITY,0) AS UN_ASSIGN_QUANTITY");
            strSql.Append(" FROM (");
            //---分页
            strSql.Append("  SELECT * FROM ( ");
            strSql.Append("      SELECT ROW_NUMBER() OVER (ORDER BY T." + orderby);
            strSql.AppendFormat(" )AS Row, T.*  FROM load_assign_view T WHERE {0} )TT", strWhere);
            strSql.AppendFormat(" WHERE TT.Row BETWEEN {0} and {1} ) X", startIndex, endIndex);
            //--入库预定
            strSql.AppendFormat(" LEFT OUTER JOIN (SELECT PRODUCT_CODE, SUM(QUANTITY) AS QUANTITY FROM BLL_RECEIVING_PLAN WHERE ARRIVAL_DATE <= '{0}' AND TO_WAREHOUSE_CODE = '{1}' AND STATUS_FLAG ={2} GROUP BY PRODUCT_CODE) AS Z ", departureDate, warehouseCode, CConstant.INIT);
            strSql.Append(" ON X.PRODUCT_CODE = Z.PRODUCT_CODE");

            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        /// <summary>
        /// 配分明细,门店载入数据获得
        /// </summary>
        public DataSet GetTransferOutAssignDetailInfo(string slipNumber, string productCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT BS.CODE,BS.NAME,BS.STOCK, ISNULL(TR.QUANTITY,0) AS ASSIGN_QUANTITY , 0 AS SALES_PLAN_QUANTITY, 0 AS PROM_PLAN_QUANTITY,0 AS ADD_PLAN_QUANTITY  ");
            strSql.Append(" FROM ");
            strSql.Append(" (SELECT BW.CODE , BW.NAME , ISNULL(BS.QUANTITY, 0) AS STOCK FROM BASE_WAREHOUSE AS BW LEFT OUTER JOIN BASE_STOCK AS BS ON BW.CODE = BS.WAREHOUSE_CODE AND PRODUCT_CODE = '" + productCode + "' WHERE TYPE = 1) BS ");
            strSql.Append(" LEFT JOIN  ");
            strSql.Append(" (SELECT  TRL.TO_WAREHOUSE_CODE, ISNULL(TRL.QUANTITY,0) AS QUANTITY FROM BLL_TRANSFER_ORDER AS TRO LEFT OUTER JOIN BLL_TRANSFER_ORDER_LINE AS TRL ON TRO.ID = TRL.ORDER_ID WHERE TRO.STATUS_FLAG = 0  AND TRO.SLIP_NUMBER = '" + slipNumber + "' AND TRO.PRODUCT_CODE = '" + productCode + "') TR  ");
            strSql.Append(" ON  ");
            strSql.Append(" BS.CODE = TR.TO_WAREHOUSE_CODE ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}

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
using System.Collections;

namespace SCM.SQLServerDAL
{
    public partial class PurchaseRequisitionManage : IPurchaseRequisition
    {
        public PurchaseRequisitionManage() { }

        public DataSet GetWarehouseName(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 CODE,NAME from BASE_WAREHOUSE where DEPARTMENT_CODE='" + code + "'");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM bll_Purchase_Requisition_view");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());

            return obj == null ? 0 : Convert.ToInt32(obj);

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
                strSql.Append("order by T.DEPARTUAL_DATE,T.ARRIVAL_DATE");
            }
            strSql.Append(")AS Row, T.* from bll_Purchase_Requisition_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 实体的取得
        /// </summary>
        public BllPurchaseRequisitionTable GetModel(string SLIP_NUMBER)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,FROM_WAREHOUSE_CODE,TO_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,PRODUCT_GROUP_CODE,REQUISITION_PERIOD,GROUP_STOCK,AREA_PERCENTAGE,AREA_MAX_QUANTITY,SHOP_PERCENTAGE,SHOP_MAX_QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME,STATUS_NAME,FROM_WAREHOUSE_NAME,TO_WAREHOUSE_NAME,PRODUCT_GROUP_NAME,USER_NAME from bll_Purchase_Requisition_view ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            parameters[0].Value = SLIP_NUMBER;

            BllPurchaseRequisitionTable prTable = new BllPurchaseRequisitionTable();

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"] != null && ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    prTable.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"] != null && ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"].ToString() != "")
                {
                    prTable.FROM_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["FROM_WAREHOUSE_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"] != null && ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString() != "")
                {
                    prTable.TO_WAREHOUSE_CODE = ds.Tables[0].Rows[0]["TO_WAREHOUSE_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DEPARTUAL_DATE"] != null && ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString() != "")
                {
                    prTable.DEPARTUAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["DEPARTUAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ARRIVAL_DATE"] != null && ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString() != "")
                {
                    prTable.ARRIVAL_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["ARRIVAL_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRODUCT_GROUP_CODE"] != null && ds.Tables[0].Rows[0]["PRODUCT_GROUP_CODE"].ToString() != "")
                {
                    prTable.PRODUCT_GROUP_CODE = ds.Tables[0].Rows[0]["PRODUCT_GROUP_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["REQUISITION_PERIOD"] != null && ds.Tables[0].Rows[0]["REQUISITION_PERIOD"].ToString() != "")
                {
                    prTable.REQUISITION_PERIOD = ds.Tables[0].Rows[0]["REQUISITION_PERIOD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GROUP_STOCK"] != null && ds.Tables[0].Rows[0]["GROUP_STOCK"].ToString() != "")
                {
                    prTable.GROUP_STOCK = decimal.Parse(ds.Tables[0].Rows[0]["GROUP_STOCK"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AREA_PERCENTAGE"] != null && ds.Tables[0].Rows[0]["AREA_PERCENTAGE"].ToString() != "")
                {
                    prTable.AREA_PERCENTAGE = decimal.Parse(ds.Tables[0].Rows[0]["AREA_PERCENTAGE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AREA_MAX_QUANTITY"] != null && ds.Tables[0].Rows[0]["AREA_MAX_QUANTITY"].ToString() != "")
                {
                    prTable.AREA_MAX_QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["AREA_MAX_QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SHOP_PERCENTAGE"] != null && ds.Tables[0].Rows[0]["SHOP_PERCENTAGE"].ToString() != "")
                {
                    prTable.SHOP_PERCENTAGE = decimal.Parse(ds.Tables[0].Rows[0]["SHOP_PERCENTAGE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SHOP_MAX_QUANTITY"] != null && ds.Tables[0].Rows[0]["SHOP_MAX_QUANTITY"].ToString() != "")
                {
                    prTable.SHOP_MAX_QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["SHOP_MAX_QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"] != null && ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    prTable.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE1"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString() != "")
                {
                    prTable.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE2"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString() != "")
                {
                    prTable.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ATTRIBUTE3"] != null && ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString() != "")
                {
                    prTable.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_USER"] != null && ds.Tables[0].Rows[0]["CREATE_USER"].ToString() != "")
                {
                    prTable.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"] != null && ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    prTable.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_USER"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString() != "")
                {
                    prTable.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    prTable.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_NAME"] != null && ds.Tables[0].Rows[0]["STATUS_NAME"].ToString() != "")
                {
                    prTable.STATUS_NAME = ds.Tables[0].Rows[0]["STATUS_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FROM_WAREHOUSE_NAME"] != null && ds.Tables[0].Rows[0]["FROM_WAREHOUSE_NAME"].ToString() != "")
                {
                    prTable.FROM_WAREHOUSE_NAME = ds.Tables[0].Rows[0]["FROM_WAREHOUSE_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TO_WAREHOUSE_NAME"] != null && ds.Tables[0].Rows[0]["TO_WAREHOUSE_NAME"].ToString() != "")
                {
                    prTable.TO_WAREHOUSE_NAME = ds.Tables[0].Rows[0]["TO_WAREHOUSE_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PRODUCT_GROUP_NAME"] != null && ds.Tables[0].Rows[0]["PRODUCT_GROUP_NAME"].ToString() != "")
                {
                    prTable.PRODUCT_GROUP_NAME = ds.Tables[0].Rows[0]["PRODUCT_GROUP_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["USER_NAME"] != null && ds.Tables[0].Rows[0]["USER_NAME"].ToString() != "")
                {
                    prTable.USER_NAME = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                }
                //if (ds.Tables[0].Rows[0]["REQUISTION_QUANTITY"] != null && ds.Tables[0].Rows[0]["REQUISTION_QUANTITY"].ToString() != "")
                //{
                //    prTable.Apply_quantity = Convert.ToDecimal(ds.Tables[0].Rows[0]["REQUISTION_QUANTITY"]);
                //}
                //if (ds.Tables[0].Rows[0]["CONFIRM_QUANTITY"] != null && ds.Tables[0].Rows[0]["CONFIRM_QUANTITY"].ToString() != "")
                //{
                //    prTable.Auditing_quantity = Convert.ToDecimal(ds.Tables[0].Rows[0]["CONFIRM_QUANTITY"]);
                //}
                return prTable;
            }
            else
            {
                return null;
            }
        }

        public DataSet GetLineList(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM  bll_purchase_requisition_line_view ");
            strSql.AppendFormat("WHERE SLIP_NUMBER = '{0}'", slipNumber);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetStockList(string fromWarehouseCode, string toWarehouseCode, string productGroupCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.PRODUCT_CODE,A.UNIT_CODE,ISNULL(A.QUANTITY,0) AS WAREHOUSE_STOCK,ISNULL(B.QUANTITY,0) AS SHOP_STOCK,A.STYLE_CODE,BS.NAME AS STYLE_NAME,A.COLOR_CODE,BC.NAME AS COLOR_NAME,A.SIZE_CODE,SE.NAME AS SIZE_NAME,0 AS BEFORE_SALES_QUANTITY,0 AS REQUISTION_QUANTITY ,1 AS BOX_NUMBER ");
            /*补货仓库库存*/
            strSql.AppendFormat(" FROM (SELECT BS.PRODUCT_CODE,BS.UNIT_CODE,BS.QUANTITY,BP.NAME AS PRODUCT_NAME,BP.STYLE AS STYLE_CODE,BP.COLOR AS COLOR_CODE,BP.SIZE AS SIZE_CODE, BP.GROUP_CODE FROM BASE_STOCK AS BS LEFT JOIN BASE_PRODUCT AS BP ON BS.PRODUCT_CODE = BP.CODE WHERE BS.WAREHOUSE_CODE = '{0}' AND BP.GROUP_CODE = '{1}' AND BS.QUANTITY IS NOT NULL AND BS.QUANTITY > 0 ) AS A ", fromWarehouseCode, productGroupCode);
            /*申请仓库库存*/
            strSql.AppendFormat(" LEFT JOIN (SELECT BS.WAREHOUSE_CODE,BS.PRODUCT_CODE,BS.QUANTITY FROM BASE_STOCK AS BS LEFT JOIN BASE_PRODUCT AS BP ON BS.PRODUCT_CODE = BP.CODE WHERE WAREHOUSE_CODE = '{0}' AND BP.GROUP_CODE = '{1}' ) AS B ON A.PRODUCT_CODE = B.PRODUCT_CODE ", toWarehouseCode, productGroupCode);
            strSql.Append(" LEFT JOIN BASE_STYLE AS BS ON A.STYLE_CODE = BS.CODE ");
            strSql.Append(" LEFT JOIN BASE_COLOR AS BC ON A.COLOR_CODE = BC.CODE ");
            strSql.Append(" LEFT JOIN BASE_SIZE  AS SE ON A.SIZE_CODE = SE.CODE AND A.GROUP_CODE = SE.PRODUCT_GROUP_CODE");
            strSql.Append(" ORDER BY A.PRODUCT_CODE");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public Hashtable GetReferenceInfo(string fromWarehouseCode, string toWarehouseCode, string productGroupCode, string departmentCode)
        {
            Hashtable ht = new Hashtable();
            //
            decimal stock = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT SUM(BS.QUANTITY) AS TOTAL_QUANTITY FROM BASE_STOCK AS BS LEFT JOIN BASE_PRODUCT AS BP ON BS.PRODUCT_CODE = BP.CODE WHERE BS.WAREHOUSE_CODE = '{0}' AND BP.GROUP_CODE='{1}'", fromWarehouseCode, productGroupCode);
            Object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                stock = Convert.ToDecimal(obj);
            }
            ht.Add("GroupStock", stock);
            //
            string[] dCode = { departmentCode, "", "" };
            strSql = new StringBuilder();
            strSql.Append("SELECT D.CODE,D.PARENT_CODE,PD.PARENT_CODE AS PARENT_PARENT_CODE FROM BASE_DEPARTMENT AS D LEFT JOIN BASE_DEPARTMENT AS PD ON D.PARENT_CODE = PD.CODE ");
            strSql.AppendFormat(" WHERE D.CODE = '{0}'", departmentCode);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PARENT_CODE"] != null && ds.Tables[0].Rows[0]["PARENT_CODE"].ToString() != "")
                {
                    dCode[1] = ds.Tables[0].Rows[0]["PARENT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PARENT_PARENT_CODE"] != null && ds.Tables[0].Rows[0]["PARENT_PARENT_CODE"].ToString() != "")
                {
                    dCode[2] = ds.Tables[0].Rows[0]["PARENT_PARENT_CODE"].ToString();
                }
            }

            //
            decimal[] quantity = { 0, 0, 0 };
            strSql = new StringBuilder();
            strSql.Append(" SELECT DEPARTMENT_CODE,SUM(QUANTITY) AS QUANTITY FROM STA_DEP_GRP_SALES ");
            strSql.AppendFormat(" WHERE DEPARTMENT_CODE IN ('{0}','{1}','{2}')", dCode[0], dCode[1], dCode[2]);
            strSql.AppendFormat(" AND PRODUCT_GROUP_CODE = '{0}' ", productGroupCode);
            strSql.Append(" AND SALES_DATE BETWEEN DATEADD(dd,-14,GETDATE())  AND GETDATE() ");
            strSql.Append(" GROUP BY DEPARTMENT_CODE");
            ds = DbHelperSQL.Query(strSql.ToString());
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                decimal qty = 0;
                if (row["QUANTITY"] != null && row["QUANTITY"].ToString() != "")
                {
                    qty = Convert.ToDecimal(row["QUANTITY"]);
                }

                if (row["DEPARTMENT_CODE"] != null && row["DEPARTMENT_CODE"].ToString() != "")
                {
                    string dpCode = row["DEPARTMENT_CODE"].ToString();
                    if (dCode[0] == dpCode)
                    {
                        quantity[0] = qty;
                    }
                    else if (dCode[1] == dpCode)
                    {
                        quantity[1] = qty;
                    }
                    else if (dCode[2] == dpCode)
                    {
                        quantity[2] = qty;
                    }
                }
            }
            decimal AreaPercentage = 0;
            decimal ShopPercentage = 0;
            if (quantity[2] != 0)
            {
                AreaPercentage = quantity[1] / quantity[2];
            }

            if (quantity[1] != 0)
            {
                ShopPercentage = quantity[0] / quantity[1];
            }

            ht.Add("AreaPercentage", String.Format("{0:N2}", AreaPercentage * 100));
            ht.Add("ShopPercentage", String.Format("{0:N2}", ShopPercentage * 100));
            ht.Add("AreaMaxQuantity", Math.Floor(AreaPercentage * stock));
            ht.Add("ShopMaxQuantity", Math.Floor(ShopPercentage * AreaPercentage * stock));

            return ht;
        }

        public int Delete(string slipNumber)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            //主表的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_PURCHASE_REQUISITION ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] deleteLineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteLineParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteLineParameters));

            //信息表的删除
            strSql = new StringBuilder();
            strSql.Append("delete from BLL_PURCHASE_REQUISITION_LINE ");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] deleteHeaderParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            deleteHeaderParameters[0].Value = slipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), deleteHeaderParameters));

            return DbHelperSQL.ExecuteSqlTran(sqlList);

        }


        #region IPurchaseRequisition 成员


        public int Insert(BllPurchaseRequisitionTable prTable)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BLL_PURCHASE_REQUISITION(");
            strSql.Append("SLIP_NUMBER,FROM_WAREHOUSE_CODE,TO_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,PRODUCT_GROUP_CODE,REQUISITION_PERIOD,GROUP_STOCK,AREA_PERCENTAGE,AREA_MAX_QUANTITY,SHOP_PERCENTAGE,SHOP_MAX_QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,@FROM_WAREHOUSE_CODE,@TO_WAREHOUSE_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@PRODUCT_GROUP_CODE,@REQUISITION_PERIOD,@GROUP_STOCK,@AREA_PERCENTAGE,@AREA_MAX_QUANTITY,@SHOP_PERCENTAGE,@SHOP_MAX_QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE())");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTUAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@REQUISITION_PERIOD", SqlDbType.VarChar,50),
					new SqlParameter("@GROUP_STOCK", SqlDbType.Decimal,9),
					new SqlParameter("@AREA_PERCENTAGE", SqlDbType.Decimal,9),
					new SqlParameter("@AREA_MAX_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@SHOP_PERCENTAGE", SqlDbType.Decimal,9),
					new SqlParameter("@SHOP_MAX_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = prTable.SLIP_NUMBER;
            parameters[1].Value = prTable.FROM_WAREHOUSE_CODE;
            parameters[2].Value = prTable.TO_WAREHOUSE_CODE;
            parameters[3].Value = prTable.DEPARTUAL_DATE;
            parameters[4].Value = prTable.ARRIVAL_DATE;
            parameters[5].Value = prTable.PRODUCT_GROUP_CODE;
            parameters[6].Value = prTable.REQUISITION_PERIOD;
            parameters[7].Value = prTable.GROUP_STOCK;
            parameters[8].Value = prTable.AREA_PERCENTAGE;
            parameters[9].Value = prTable.AREA_MAX_QUANTITY;
            parameters[10].Value = prTable.SHOP_PERCENTAGE;
            parameters[11].Value = prTable.SHOP_MAX_QUANTITY;
            parameters[12].Value = prTable.STATUS_FLAG;
            parameters[13].Value = prTable.ATTRIBUTE1;
            parameters[14].Value = prTable.ATTRIBUTE2;
            parameters[15].Value = prTable.ATTRIBUTE3;
            parameters[16].Value = prTable.CREATE_USER;
            parameters[17].Value = prTable.LAST_UPDATE_USER;

            list.Add(new CommandInfo(strSql.ToString(), parameters));

            foreach (BllPurchaseRequisitionLineTable prlTable in prTable.LINES)
            {
                strSql = new StringBuilder();
                strSql.Append("insert into BLL_PURCHASE_REQUISITION_LINE(");
                strSql.Append("SLIP_NUMBER,LINE_NUMBER,PRODUCT_CODE,UNIT_CODE,REQUISTION_QUANTITY,CONFIRM_QUANTITY,SHOP_STOCK,WAREHOUSE_STOCK,BEFORE_SALES_QUANTITY,AFTER_SALES_QUANTITY,BOX_NUMBER)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@LINE_NUMBER,@PRODUCT_CODE,@UNIT_CODE,@REQUISTION_QUANTITY,@CONFIRM_QUANTITY,@SHOP_STOCK,@WAREHOUSE_STOCK,@BEFORE_SALES_QUANTITY,@AFTER_SALES_QUANTITY,@BOX_NUMBER)");
                SqlParameter[] lineParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@LINE_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@REQUISTION_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@CONFIRM_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@SHOP_STOCK", SqlDbType.Decimal,9),
					new SqlParameter("@WAREHOUSE_STOCK", SqlDbType.Decimal,9),
					new SqlParameter("@BEFORE_SALES_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@AFTER_SALES_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@BOX_NUMBER", SqlDbType.Int,4)};
                lineParameters[0].Value = prTable.SLIP_NUMBER;
                lineParameters[1].Value = prlTable.LINE_NUMBER;
                lineParameters[2].Value = prlTable.PRODUCT_CODE;
                lineParameters[3].Value = prlTable.UNIT_CODE;
                lineParameters[4].Value = prlTable.REQUISTION_QUANTITY;
                lineParameters[5].Value = prlTable.REQUISTION_QUANTITY;
                lineParameters[6].Value = prlTable.SHOP_STOCK;
                lineParameters[7].Value = prlTable.WAREHOUSE_STOCK;
                lineParameters[8].Value = prlTable.BEFORE_SALES_QUANTITY;
                lineParameters[9].Value = prlTable.AFTER_SALES_QUANTITY;
                lineParameters[10].Value = prlTable.BOX_NUMBER;
                list.Add(new CommandInfo(strSql.ToString(), lineParameters));
            }
            return DbHelperSQL.ExecuteSqlTran(list);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(BllPurchaseRequisitionTable prTable)
        {
            List<CommandInfo> list = new List<CommandInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BLL_PURCHASE_REQUISITION set ");
            strSql.Append("FROM_WAREHOUSE_CODE=@FROM_WAREHOUSE_CODE,");
            strSql.Append("TO_WAREHOUSE_CODE=@TO_WAREHOUSE_CODE,");
            strSql.Append("DEPARTUAL_DATE=@DEPARTUAL_DATE,");
            strSql.Append("ARRIVAL_DATE=@ARRIVAL_DATE,");
            strSql.Append("PRODUCT_GROUP_CODE=@PRODUCT_GROUP_CODE,");
            strSql.Append("REQUISITION_PERIOD=@REQUISITION_PERIOD,");
            strSql.Append("GROUP_STOCK=@GROUP_STOCK,");
            strSql.Append("AREA_PERCENTAGE=@AREA_PERCENTAGE,");
            strSql.Append("AREA_MAX_QUANTITY=@AREA_MAX_QUANTITY,");
            strSql.Append("SHOP_PERCENTAGE=@SHOP_PERCENTAGE,");
            strSql.Append("SHOP_MAX_QUANTITY=@SHOP_MAX_QUANTITY,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=GETDATE()");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@TO_WAREHOUSE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DEPARTUAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@ARRIVAL_DATE", SqlDbType.DateTime),
					new SqlParameter("@PRODUCT_GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@REQUISITION_PERIOD", SqlDbType.VarChar,50),
					new SqlParameter("@GROUP_STOCK", SqlDbType.Decimal,9),
					new SqlParameter("@AREA_PERCENTAGE", SqlDbType.Decimal,9),
					new SqlParameter("@AREA_MAX_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@SHOP_PERCENTAGE", SqlDbType.Decimal,9),
					new SqlParameter("@SHOP_MAX_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            parameters[0].Value = prTable.FROM_WAREHOUSE_CODE;
            parameters[1].Value = prTable.TO_WAREHOUSE_CODE;
            parameters[2].Value = prTable.DEPARTUAL_DATE;
            parameters[3].Value = prTable.ARRIVAL_DATE;
            parameters[4].Value = prTable.PRODUCT_GROUP_CODE;
            parameters[5].Value = prTable.REQUISITION_PERIOD;
            parameters[6].Value = prTable.GROUP_STOCK;
            parameters[7].Value = prTable.AREA_PERCENTAGE;
            parameters[8].Value = prTable.AREA_MAX_QUANTITY;
            parameters[9].Value = prTable.SHOP_PERCENTAGE;
            parameters[10].Value = prTable.SHOP_MAX_QUANTITY;
            parameters[11].Value = prTable.STATUS_FLAG;
            parameters[12].Value = prTable.ATTRIBUTE1;
            parameters[13].Value = prTable.ATTRIBUTE2;
            parameters[14].Value = prTable.ATTRIBUTE3;
            parameters[15].Value = prTable.LAST_UPDATE_USER;
            parameters[16].Value = prTable.SLIP_NUMBER;
            list.Add(new CommandInfo(strSql.ToString(), parameters));

            foreach (BllPurchaseRequisitionLineTable prlTable in prTable.LINES)
            {
                strSql = new StringBuilder();
                strSql.Append("update BLL_PURCHASE_REQUISITION_LINE set ");
                strSql.Append("REQUISTION_QUANTITY=@REQUISTION_QUANTITY,");
                strSql.Append("CONFIRM_QUANTITY=@CONFIRM_QUANTITY,");
                strSql.Append("BEFORE_SALES_QUANTITY=@BEFORE_SALES_QUANTITY,");
                strSql.Append("BOX_NUMBER=@BOX_NUMBER");
                strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER and PRODUCT_CODE=@PRODUCT_CODE ");
                SqlParameter[] lineParameters = {
					new SqlParameter("@REQUISTION_QUANTITY", SqlDbType.Decimal,9),
                    new SqlParameter("@CONFIRM_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@BEFORE_SALES_QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@BOX_NUMBER", SqlDbType.Int,4),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20)};
                lineParameters[0].Value = prlTable.REQUISTION_QUANTITY;
                lineParameters[1].Value = prlTable.REQUISTION_QUANTITY;
                lineParameters[2].Value = prlTable.BEFORE_SALES_QUANTITY;
                lineParameters[3].Value = prlTable.BOX_NUMBER;
                lineParameters[4].Value = prlTable.SLIP_NUMBER;
                lineParameters[5].Value = prlTable.PRODUCT_CODE;
                list.Add(new CommandInfo(strSql.ToString(), lineParameters));
            }
            return DbHelperSQL.ExecuteSqlTran(list);
        }

        #endregion


        public DataSet GetMonitorData(string slipNumber)
        {
            BllPurchaseRequisitionTable prTable = GetModel(slipNumber);
            string departmentCode = new WarehouseManage().GetModel(prTable.TO_WAREHOUSE_CODE).DEPARTMENT_CODE;
            string period = "0";
            if (prTable.REQUISITION_PERIOD == "3")
            {
                period = "3,4,5";
            }
            else if (prTable.REQUISITION_PERIOD == "4")
            {
                period = "1,2,6,7";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT A.SIZE_CODE, ");
            strSql.Append(" A.NAME, ");
            strSql.Append(" A.REFERENCE_PERCENTAGE, ");
            strSql.Append(" ISNULL(B.SHOP_STOCK,0) AS SHOP_STOCK, ");
            strSql.Append(" ISNULL(B.BEFORE_SALES_QUANTITY,0) AS BEFORE_SALES_QUANTITY, ");
            strSql.Append(" ISNULL(B.REQUISTION_QUANTITY,0) AS REQUISTION_QUANTITY, ");
            strSql.Append(" ISNULL(C.AFTER_SALES_QUANTITY,0) AS AFTER_SALES_QUANTITY ");
            strSql.AppendFormat(" FROM (SELECT CODE AS SIZE_CODE,NAME,REFERENCE_PERCENTAGE,PRODUCT_GROUP_CODE FROM BASE_SIZE WHERE PRODUCT_GROUP_CODE = '{0}') AS A ", prTable.PRODUCT_GROUP_CODE);
            strSql.Append(" LEFT JOIN ( ");
            strSql.Append(" SELECT BP.SIZE AS SIZE_CODE,BP.GROUP_CODE AS PRODUCT_GROUP_CODE,SUM(SHOP_STOCK) AS SHOP_STOCK, ");
            strSql.Append(" SUM(BEFORE_SALES_QUANTITY) AS BEFORE_SALES_QUANTITY, ");
            strSql.Append(" SUM(REQUISTION_QUANTITY) AS REQUISTION_QUANTITY ");
            strSql.Append(" FROM BLL_PURCHASE_REQUISITION AS BPR ");
            strSql.Append(" LEFT JOIN BLL_PURCHASE_REQUISITION_LINE AS BPRL ON BPR.SLIP_NUMBER = BPRL.SLIP_NUMBER ");
            strSql.Append(" LEFT JOIN BASE_PRODUCT AS BP ON BPRL.PRODUCT_CODE = BP.CODE ");
            strSql.AppendFormat(" WHERE BPR.PRODUCT_GROUP_CODE = '{0}' ", prTable.PRODUCT_GROUP_CODE);
            strSql.AppendFormat(" AND BPR.SLIP_NUMBER='{0}'",prTable.SLIP_NUMBER);
            strSql.Append(" GROUP BY BP.SIZE,BP.GROUP_CODE ");
            strSql.Append(" ) AS B ON A.SIZE_CODE = B.SIZE_CODE AND A.PRODUCT_GROUP_CODE = B.PRODUCT_GROUP_CODE");
            strSql.Append(" LEFT JOIN ( ");
            strSql.Append(" SELECT SIZE_CODE ,PRODUCT_GROUP_CODE,AVG(QUANTITY) AS AFTER_SALES_QUANTITY FROM STA_DEP_GRP_SIZE_SALES ");
            strSql.AppendFormat(" WHERE PRODUCT_GROUP_CODE='{0}' ", prTable.PRODUCT_GROUP_CODE);
            strSql.AppendFormat(" AND DEPARTMENT_CODE='{0}'", departmentCode);
            strSql.Append(" AND SALES_DATE  BETWEEN DATEADD(dd,-14,GETDATE())  AND GETDATE() ");
            strSql.AppendFormat(" AND DATEPART(dw,SALES_DATE) IN ({0}) GROUP BY SIZE_CODE,PRODUCT_GROUP_CODE ", period);
            strSql.Append(" ) AS C ON A.SIZE_CODE = C.SIZE_CODE AND A.PRODUCT_GROUP_CODE = C.PRODUCT_GROUP_CODE");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int Audit(string slipNumber)
        {
            StringBuilder strSql = new StringBuilder();
            List<CommandInfo> list = new List<CommandInfo>();
            strSql.Append(" UPDATE BLL_PURCHASE_REQUISITION SET STATUS_FLAG=1 WHERE SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameterste = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,255)
                                        };
            parameterste[0].Value = slipNumber;

            list.Add(new CommandInfo(strSql.ToString(), parameterste));

            BllPurchaseRequisitionTable prTable = GetModel(slipNumber);
            DataSet ds = GetLineList(slipNumber);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                decimal quantity = 0;
                try
                {
                    quantity = Convert.ToDecimal(row["CONFIRM_QUANTITY"]);
                }
                catch { }
                if (quantity <= 0)
                {
                    continue;
                }

                strSql = new StringBuilder();
                strSql.Append("insert into BLL_SHIPMENT_PLAN(");
                strSql.Append("PURCHASE_REQUISITION_SLIP_NUMBER,PURCHASE_REQUISITION_LINE_NUMBER,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PURCHASE_REQUISITION_SLIP_NUMBER,@PURCHASE_REQUISITION_LINE_NUMBER,@FROM_WAREHOUSE_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
                SqlParameter[] transferInPlanParameters = {
					new SqlParameter("@PURCHASE_REQUISITION_SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@PURCHASE_REQUISITION_LINE_NUMBER", SqlDbType.Int,4),
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
                transferInPlanParameters[0].Value = slipNumber;
                transferInPlanParameters[1].Value = Convert.ToInt32(row["LINE_NUMBER"]);
                transferInPlanParameters[2].Value = prTable.FROM_WAREHOUSE_CODE;
                transferInPlanParameters[3].Value = prTable.DEPARTUAL_DATE;
                transferInPlanParameters[4].Value = prTable.ARRIVAL_DATE;
                transferInPlanParameters[5].Value = prTable.TO_WAREHOUSE_CODE;
                transferInPlanParameters[6].Value = row["PRODUCT_CODE"].ToString();
                transferInPlanParameters[7].Value = row["UNIT_CODE"].ToString();
                transferInPlanParameters[8].Value = quantity;
                transferInPlanParameters[9].Value = 0;
                transferInPlanParameters[10].Value = "";
                transferInPlanParameters[11].Value = "";
                transferInPlanParameters[12].Value = "";
                transferInPlanParameters[13].Value = prTable.LAST_UPDATE_USER;
                transferInPlanParameters[14].Value = prTable.LAST_UPDATE_USER;

                list.Add(new CommandInfo(strSql.ToString(), transferInPlanParameters));
            }
            return DbHelperSQL.ExecuteSqlTran(list);

        }

        public int Auditing(BllPurchaseRequisitionTable prTable)
        {
            StringBuilder strSql = new StringBuilder();
            List<CommandInfo> list = new List<CommandInfo>();
            strSql.Append(" UPDATE BLL_PURCHASE_REQUISITION SET STATUS_FLAG=1 WHERE SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameterste = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,255)
                                        };
            parameterste[0].Value = prTable.SLIP_NUMBER;
            list.Add(new CommandInfo(strSql.ToString(), parameterste));

            foreach (BllPurchaseRequisitionLineTable prlTable in prTable.LINES)
            {
                strSql = new StringBuilder();
                strSql.Append(" UPDATE BLL_PURCHASE_REQUISITION_LINE SET CONFIRM_QUANTITY=@CONFIRM_QUANTITY WHERE SLIP_NUMBER=@SLIP_NUMBER AND PRODUCT_CODE=@PRODUCT_CODE");
                SqlParameter[] parametersteline = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,255),
                    new SqlParameter("@CONFIRM_QUANTITY",SqlDbType.Decimal,20),
                    new SqlParameter("@PRODUCT_CODE",SqlDbType.VarChar,225)
                                        };
                parametersteline[0].Value = prlTable.SLIP_NUMBER;
                parametersteline[1].Value = prlTable.Quantity;
                parametersteline[2].Value = prlTable.PRODUCT_CODE;
                list.Add(new CommandInfo(strSql.ToString(), parametersteline));

                if (prlTable.Quantity <= 0)
                {
                    continue;
                }

                strSql = new StringBuilder();
                strSql.Append("insert into BLL_SHIPMENT_PLAN(");
                strSql.Append("PURCHASE_REQUISITION_SLIP_NUMBER,PURCHASE_REQUISITION_LINE_NUMBER,FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
                strSql.Append(" values (");
                strSql.Append("@PURCHASE_REQUISITION_SLIP_NUMBER,@PURCHASE_REQUISITION_LINE_NUMBER,@FROM_WAREHOUSE_CODE,@DEPARTUAL_DATE,@ARRIVAL_DATE,@TO_WAREHOUSE_CODE,@PRODUCT_CODE,@UNIT_CODE,@QUANTITY,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),@LAST_UPDATE_USER,getdate())");
                SqlParameter[] transferInPlanParameters = {
					new SqlParameter("@PURCHASE_REQUISITION_SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@PURCHASE_REQUISITION_LINE_NUMBER", SqlDbType.Int,4),
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
                transferInPlanParameters[0].Value = prTable.SLIP_NUMBER;
                transferInPlanParameters[1].Value = prlTable.LINE_NUMBER;
                transferInPlanParameters[2].Value = prTable.FROM_WAREHOUSE_CODE;
                transferInPlanParameters[3].Value = prTable.DEPARTUAL_DATE;
                transferInPlanParameters[4].Value = prTable.ARRIVAL_DATE;
                transferInPlanParameters[5].Value = prTable.TO_WAREHOUSE_CODE;
                transferInPlanParameters[6].Value = prlTable.PRODUCT_CODE;
                transferInPlanParameters[7].Value = prlTable.UNIT_CODE;
                transferInPlanParameters[8].Value = prlTable.Quantity;
                transferInPlanParameters[9].Value = 0;
                transferInPlanParameters[10].Value = "";
                transferInPlanParameters[11].Value = "";
                transferInPlanParameters[12].Value = "";
                transferInPlanParameters[13].Value = prTable.LAST_UPDATE_USER;
                transferInPlanParameters[14].Value = prTable.LAST_UPDATE_USER;
                list.Add(new CommandInfo(strSql.ToString(), transferInPlanParameters));
            }
            return DbHelperSQL.ExecuteSqlTran(list);

        }

    }//class end
}

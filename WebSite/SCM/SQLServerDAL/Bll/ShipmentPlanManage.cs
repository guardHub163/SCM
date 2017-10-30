using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.Model;
using System.Data.SqlClient;
using SCM.DBUtility;
using System.Data;
using SCM.Common;

namespace SCM.SQLServerDAL
{
    public class ShipmentPlanManage : IShipmentPlan
    {

        #region IShipmentPlan 成员

        public int CreateShipmentPlan(string trSlipNumber, string userId)
        {
            int ret = 0;
            List<CommandInfo> sqlList = new List<CommandInfo>();

            //BLL_SHIPMENT_PLAN 创建
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" INSERT INTO BLL_SHIPMENT_PLAN (TRANSFER_ORDER_ID ,TRANSFER_ORDER_SLIP_NUMBER,TRANSFER_ORDER_LINE_NUMBER,FROM_WAREHOUSE_CODE ");
            strSql.Append(" ,DEPARTUAL_DATE,ARRIVAL_DATE,TO_WAREHOUSE_CODE,PRODUCT_CODE,UNIT_CODE,QUANTITY,STATUS_FLAG ");
            strSql.Append(" ,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" SELECT TRO.ID,TRO.SLIP_NUMBER,TRL.LINE_NUMBER,TRO.FROM_WAREHOUSE_CODE,TRO.DEPARTUAL_DATE,TRO.DEPARTUAL_DATE+1,");
            strSql.Append(" TRL.TO_WAREHOUSE_CODE,TRO.PRODUCT_CODE,TRO.UNIT_CODE,TRL.QUANTITY,@STATUS_FLAG,'' ,'' ,'' ,@CREATE_USER ,GETDATE() ,@LAST_UPDATE_USER,GETDATE()");
            strSql.Append(" FROM BLL_TRANSFER_ORDER  TRO LEFT JOIN BLL_TRANSFER_ORDER_LINE TRL ON TRO.ID = TRL.ORDER_ID ");
            strSql.Append(" WHERE TRL.LINE_NUMBER IS NOT NULL AND TRO.SLIP_NUMBER = @SLIP_NUMBER ORDER BY TRO.ID");
            SqlParameter[] insertParameters = {
                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,50),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,50),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50)};
            insertParameters[0].Value = CConstant.INIT;
            insertParameters[1].Value = userId;
            insertParameters[2].Value = userId;
            insertParameters[3].Value = trSlipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), insertParameters));

            //BLL_TRANSFER_ORDER 状态更新
            strSql = new StringBuilder();
            strSql.Append("UPDATE BLL_TRANSFER_ORDER SET STATUS_FLAG = @STATUS_FLAG,LAST_UPDATE_USER=@LAST_UPDATE_USER,LAST_UPDATE_TIME=GETDATE() WHERE SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] updateParameters = {
                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,50),
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50)};
            updateParameters[0].Value = CConstant.NORMAL;
            updateParameters[1].Value = userId;
            updateParameters[2].Value = trSlipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), updateParameters));

            //事务处理
            try
            {
                ret = DbHelperSQL.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex) { }
            return ret;
        }


        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bll_shipment_plan_view ");
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

        public DataSet GetTransferOutPlanList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.TO_WAREHOUSE_CODE desc");
            }
            strSql.Append(")AS Row, T.*  from bll_shipment_plan_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int DeleteShipmentPlan(string trSlipNumber, string userId)
        {
            int ret = 0;
            List<CommandInfo> sqlList = new List<CommandInfo>();

            //BLL_SHIPMENT_PLAN 创建
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DELETE FROM BLL_SHIPMENT_PLAN WHERE TRANSFER_ORDER_SLIP_NUMBER=@TRANSFER_ORDER_SLIP_NUMBER");
            SqlParameter[] insertParameters = {
					new SqlParameter("@TRANSFER_ORDER_SLIP_NUMBER", SqlDbType.VarChar,50)};
            insertParameters[0].Value = trSlipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), insertParameters));

            //BLL_TRANSFER_ORDER 状态更新
            strSql = new StringBuilder();
            strSql.Append("UPDATE BLL_TRANSFER_ORDER SET STATUS_FLAG = @STATUS_FLAG,LAST_UPDATE_USER=@LAST_UPDATE_USER,LAST_UPDATE_TIME=GETDATE() WHERE SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] updateParameters = {
                    new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,50),
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50)};
            updateParameters[0].Value = CConstant.INIT;
            updateParameters[1].Value = userId;
            updateParameters[2].Value = trSlipNumber;
            sqlList.Add(new CommandInfo(strSql.ToString(), updateParameters));

            //事务处理
            try
            {
                ret = DbHelperSQL.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex) { }
            return ret;
        }

        public DataSet GetTransferOutPlanDetail(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM bll_shipment_plan_search_view ");
            if (sqlWhere.Trim() != "")
            {
                strSql.Append(" where " + sqlWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        //拣货单打印
        public DataSet PrintOutMonad(DateTime fromdate, DateTime todate, string warehousecode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  CONVERT(CHAR(10),BSP.DEPARTUAL_DATE,126) AS DEPARTUAL_DATE,(cast(BSP.QUANTITY as int)) AS QUANTITY,BSP.FROM_WAREHOUSE_CODE AS FROM_WAREHOUSE_CODE,BWF.NAME AS FROM_WAREHOUSE_NAME,BP.NAME AS PRODUCT_NAME, ");
            strSql.Append("BU.NAME AS UNIT_NAME,BS.NAME AS SIZE_NAME,BST.NAME AS STYLE_NAME,BSP.PRODUCT_CODE,BC.NAME AS COLOR_NAME ");
            strSql.Append("FROM (SELECT  FROM_WAREHOUSE_CODE, ");
            strSql.Append("DEPARTUAL_DATE,SUM(QUANTITY) AS QUANTITY,PRODUCT_CODE,UNIT_CODE ");
            strSql.Append("FROM  dbo.BLL_SHIPMENT_PLAN	WHERE STATUS_FLAG=0 AND QUANTITY<>0 ");
            strSql.Append("AND DEPARTUAL_DATE BETWEEN @DEPARTUAL_DATE1 AND @DEPARTUAL_DATE2 ");
            strSql.Append("AND FROM_WAREHOUSE_CODE=@FROM_WAREHOUSE_CODE ");
            strSql.Append("GROUP BY FROM_WAREHOUSE_CODE,DEPARTUAL_DATE,PRODUCT_CODE,UNIT_CODE) AS BSP ");
            strSql.Append("LEFT JOIN  dbo.BASE_WAREHOUSE AS BWF ON BSP.FROM_WAREHOUSE_CODE = BWF.CODE ");
            strSql.Append("LEFT JOIN  dbo.BASE_PRODUCT AS BP ON BSP.PRODUCT_CODE=BP.CODE ");
            strSql.Append("LEFT JOIN  dbo.BASE_UNIT AS BU ON BU.CODE=BSP.UNIT_CODE ");
            strSql.Append("LEFT JOIN  dbo.BASE_SIZE AS BS ON BS.CODE=BP.SIZE AND BS.PRODUCT_GROUP_CODE = BP.GROUP_CODE ");
            strSql.Append("LEFT JOIN  dbo.BASE_STYLE AS BST ON BST.CODE=BP.STYLE ");
            strSql.Append("LEFT JOIN dbo.BASE_COLOR AS BC ON BC.CODE=BP.COLOR ");
            strSql.Append("ORDER BY BSP.DEPARTUAL_DATE,BSP.PRODUCT_CODE ASC");
            SqlParameter[] Parameters = {
                    new SqlParameter("@DEPARTUAL_DATE1", SqlDbType.DateTime),
                     new SqlParameter("@DEPARTUAL_DATE2", SqlDbType.DateTime),
                    new SqlParameter("@FROM_WAREHOUSE_CODE", SqlDbType.VarChar,50)
                                              };
            Parameters[0].Value = fromdate;
            Parameters[1].Value = todate;
            Parameters[2].Value = warehousecode;
            return DbHelperSQL.Query(strSql.ToString(), Parameters);
        }

        //出库单打印
        public DataSet PrintShop(string slipnumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM bll_printShop_view ");
            strSql.AppendFormat("where SLIP_NUMBER='{0}'", slipnumber);
            strSql.Append("ORDER BY DEPARTUAL_DATE,PRODUCT_CODE ASC");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}

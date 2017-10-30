using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using POS.IDAL;
using POS.DBUtility;
using POS.Model;
using System.Collections.Generic;//Please add references
namespace POS.SQLServerDAL
{
    /// <summary>
    /// 数据访问类:CashManager
    /// </summary>
    public class CashManager : ICash
    {
        public CashManager()
        { }
        #region  Method
        /// <summary>
        /// 新增一条数据
        /// </summary>
        public int Insert(CashTable cashTable)
        {
            List<CommandInfo> sqlList = new List<CommandInfo>();

            CashTable oldCashTable = GetModel(" STATUS_FLAG = 0 ");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BLL_CASH set ");
            strSql.Append("TAKE_CASH=@TAKE_CASH,");
            strSql.Append("BALANCE_CASH=@BALANCE_CASH,");
            strSql.Append("SALES_SLIP_NUMBER=@SALES_SLIP_NUMBER,");
            strSql.Append("BANK_NAME=@BANK_NAME,");
            strSql.Append("MEMO=@MEMO,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("LAST_UPDATE_TIME=getDate(),");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] updateParameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),					
					new SqlParameter("@TAKE_CASH", SqlDbType.Decimal,9),
					new SqlParameter("@BALANCE_CASH", SqlDbType.Decimal,9),
					new SqlParameter("@SALES_SLIP_NUMBER", SqlDbType.VarChar,50),
                    new SqlParameter("@BANK_NAME", SqlDbType.VarChar,20),
					new SqlParameter("@MEMO", SqlDbType.VarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            updateParameters[0].Value = oldCashTable.SLIP_NUMBER;
            updateParameters[1].Value = cashTable.TAKE_CASH;
            updateParameters[2].Value = cashTable.BALANCE_CASH;
            try
            {
                updateParameters[3].Value = new SalesOrderManage().GetMaxSlipNumber();
            }
            catch
            {
                updateParameters[3].Value = "";
            }

            updateParameters[4].Value = cashTable.BANK_NAME;
            updateParameters[5].Value = cashTable.MEMO;
            updateParameters[6].Value = cashTable.STATUS_FLAG;
            updateParameters[7].Value = cashTable.LAST_UPDATE_USER;

            sqlList.Add(new CommandInfo(strSql.ToString(), updateParameters));

            strSql = new StringBuilder();
            strSql.Append("insert into BLL_CASH(");
            strSql.Append("SLIP_NUMBER,CASH_DATE,PROFIT_CASH,LAST_CASH,TAKE_CASH,BALANCE_CASH,SALES_SLIP_NUMBER,BANK_NAME,BANK_SLIP_NUMBER,MEMO,STATUS_FLAG,SEND_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@SLIP_NUMBER,GETDATE(),@PROFIT_CASH,@LAST_CASH,@TAKE_CASH,@BALANCE_CASH,@SALES_SLIP_NUMBER,@MEMO,@BANK_NAME,@BANK_SLIP_NUMBER,@STATUS_FLAG,@SEND_FLAG,GETDATE(),@CREATE_USER,GETDATE(),@LAST_UPDATE_USER)");
            SqlParameter[] insertParameters = {
                        new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
                        new SqlParameter("@PROFIT_CASH", SqlDbType.Decimal,9),
                        new SqlParameter("@LAST_CASH", SqlDbType.Decimal,9),
                        new SqlParameter("@TAKE_CASH", SqlDbType.Decimal,9),
                        new SqlParameter("@BALANCE_CASH", SqlDbType.Decimal,9),
                        new SqlParameter("@SALES_SLIP_NUMBER", SqlDbType.VarChar,50),
                        new SqlParameter("@BANK_NAME", SqlDbType.VarChar,50),
                        new SqlParameter("@BANK_SLIP_NUMBER", SqlDbType.VarChar,50),
                        new SqlParameter("@MEMO", SqlDbType.VarChar,255),
                        new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                        new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
                        new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                        new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            insertParameters[0].Value = cashTable.SLIP_NUMBER;
            insertParameters[1].Value = 0;
            insertParameters[2].Value = cashTable.LAST_CASH;
            insertParameters[3].Value = 0;
            insertParameters[4].Value = cashTable.BALANCE_CASH;
            insertParameters[5].Value = "";
            insertParameters[6].Value = "";
            insertParameters[7].Value = "";
            insertParameters[8].Value = "";
            insertParameters[9].Value = 0;
            insertParameters[10].Value = 0;
            insertParameters[11].Value = cashTable.CREATE_USER;
            insertParameters[12].Value = cashTable.LAST_UPDATE_USER;

            sqlList.Add(new CommandInfo(strSql.ToString(), insertParameters));

            return DbHelperSQL.ExecuteSqlTran(sqlList);

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CashTable GetModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SLIP_NUMBER,CASH_DATE,PROFIT_CASH,LAST_CASH,TAKE_CASH,BALANCE_CASH,SALES_SLIP_NUMBER,BANK_NAME,BANK_SLIP_NUMBER,MEMO,STATUS_FLAG,SEND_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER from BLL_CASH ");
            strSql.AppendFormat(" where {0} ", strWhere);

            CashTable model = new CashTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString() != "")
                {
                    model.SLIP_NUMBER = ds.Tables[0].Rows[0]["SLIP_NUMBER"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CASH_DATE"].ToString() != "")
                {
                    model.CASH_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["CASH_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PROFIT_CASH"].ToString() != "")
                {
                    model.PROFIT_CASH = decimal.Parse(ds.Tables[0].Rows[0]["PROFIT_CASH"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_CASH"].ToString() != "")
                {
                    model.LAST_CASH = decimal.Parse(ds.Tables[0].Rows[0]["LAST_CASH"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TAKE_CASH"].ToString() != "")
                {
                    model.TAKE_CASH = decimal.Parse(ds.Tables[0].Rows[0]["TAKE_CASH"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BALANCE_CASH"].ToString() != "")
                {
                    model.BALANCE_CASH = decimal.Parse(ds.Tables[0].Rows[0]["BALANCE_CASH"].ToString());
                }
                model.SALES_SLIP_NUMBER = ds.Tables[0].Rows[0]["SALES_SLIP_NUMBER"].ToString();
                model.BANK_NAME = ds.Tables[0].Rows[0]["BANK_NAME"].ToString();
                model.BANK_SLIP_NUMBER = ds.Tables[0].Rows[0]["BANK_SLIP_NUMBER"].ToString();
                model.MEMO = ds.Tables[0].Rows[0]["MEMO"].ToString();
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SEND_FLAG"].ToString() != "")
                {
                    model.SEND_FLAG = int.Parse(ds.Tables[0].Rows[0]["SEND_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    model.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    model.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                model.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (top > 0)
            {
                strSql.AppendFormat(" top {0}", top);
            }
            strSql.Append(" SLIP_NUMBER,CASH_DATE,PROFIT_CASH,LAST_CASH,TAKE_CASH,BALANCE_CASH,SALES_SLIP_NUMBER,BANK_NAME,BANK_SLIP_NUMBER,MEMO,STATUS_FLAG,SEND_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER ");
            strSql.Append(" FROM BLL_CASH ");
            if (strWhere.Trim() != "")
            {
                strSql.AppendFormat(" where {0}", strWhere);
            }
            strSql.AppendFormat(" order by {0} desc ", filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(CashTable cashTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BLL_CASH set ");
            strSql.Append("CASH_DATE=@CASH_DATE,");
            strSql.Append("PROFIT_CASH=@PROFIT_CASH,");
            strSql.Append("LAST_CASH=@LAST_CASH,");
            strSql.Append("TAKE_CASH=@TAKE_CASH,");
            strSql.Append("BALANCE_CASH=@BALANCE_CASH,");
            strSql.Append("SALES_SLIP_NUMBER=@SALES_SLIP_NUMBER,");
            strSql.Append("BANK_NAME=@BANK_NAME,");
            strSql.Append("BANK_SLIP_NUMBER=@BANK_SLIP_NUMBER,");
            strSql.Append("MEMO=@MEMO,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("SEND_FLAG=@SEND_FLAG,");
            strSql.Append("LAST_UPDATE_TIME=GETDATE(),");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@CASH_DATE", SqlDbType.DateTime),
					new SqlParameter("@PROFIT_CASH", SqlDbType.Decimal,9),
					new SqlParameter("@LAST_CASH", SqlDbType.Decimal,9),
					new SqlParameter("@TAKE_CASH", SqlDbType.Decimal,9),
					new SqlParameter("@BALANCE_CASH", SqlDbType.Decimal,9),
					new SqlParameter("@SALES_SLIP_NUMBER", SqlDbType.VarChar,50),
					new SqlParameter("@BANK_NAME", SqlDbType.VarChar,20),
					new SqlParameter("@BANK_SLIP_NUMBER", SqlDbType.VarChar,20),
					new SqlParameter("@MEMO", SqlDbType.VarChar,255),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20)};
            parameters[0].Value = cashTable.CASH_DATE;
            parameters[1].Value = cashTable.PROFIT_CASH;
            parameters[2].Value = cashTable.LAST_CASH;
            parameters[3].Value = cashTable.TAKE_CASH;
            parameters[4].Value = cashTable.BALANCE_CASH;
            parameters[5].Value = cashTable.SALES_SLIP_NUMBER;
            parameters[6].Value = cashTable.BANK_NAME;
            parameters[7].Value = cashTable.BANK_SLIP_NUMBER;
            parameters[8].Value = cashTable.MEMO;
            parameters[9].Value = cashTable.STATUS_FLAG;
            parameters[10].Value = cashTable.SEND_FLAG;
            parameters[11].Value = cashTable.LAST_UPDATE_USER;
            parameters[12].Value = cashTable.SLIP_NUMBER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        //获得当前状态为1的数据
        public DataSet GetCashInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM BLL_CASH");
            strSql.AppendFormat(" WHERE 1=1 AND {0}", strWhere);
            return DbHelperSQL.Query(strSql.ToString());
        }

        //修改cash的状态
        public bool UpdateFlag( int send_flag, string slip_number)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE BLL_CASH SET SEND_FLAG=@SEND_FLAG WHERE SLIP_NUMBER=@SLIP_NUMBER");
            SqlParameter[] parameters = {
                    new SqlParameter("@SEND_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@SLIP_NUMBER", SqlDbType.NVarChar,225)
                                        };
            parameters[0].Value = send_flag;
            parameters[1].Value = slip_number;
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

        #endregion
    }
}


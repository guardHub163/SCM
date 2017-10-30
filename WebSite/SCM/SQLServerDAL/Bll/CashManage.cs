using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using System.Data;
using SCM.DBUtility;
using SCM.Common;
using System.Data.SqlClient;

namespace SCM.SQLServerDAL
{
    public partial class CashManage : ICash
    {

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SLIP_NUMBER)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BLL_CASH");
            strSql.Append(" where SLIP_NUMBER=@SLIP_NUMBER ");
            SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,50)};
            parameters[0].Value = SLIP_NUMBER;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        #endregion  Method


        #region  导入钱箱
        public DataTable Insert(DataSet ds)
        {
            DataTable dt = CommonManage.GetReturnDataTable();
            StringBuilder strSql = null;
            DataRow dr = null;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int rows = 0;
                strSql = new StringBuilder();
                dr = dt.NewRow();
                dr["SLIP_NUMBER"] = row["SLIP_NUMBER"];
                //订单是否重复导入
                try
                {
                    if (Exists(Convert.ToString(row["SLIP_NUMBER"])))
                    {
                        dr["STATUS"] = CConstant.SUCCESS;
                        dt.Rows.Add(dr);
                        continue;
                    }
                }
                catch
                {
                    dr["STATUS"] = CConstant.ERROR;
                    dt.Rows.Add(dr);
                    continue;
                }

                //钱箱的插入
                strSql.Append("insert into BLL_CASH(");
                strSql.Append("SLIP_NUMBER,CASH_DATE,PROFIT_CASH,LAST_CASH,TAKE_CASH,BALANCE_CASH,SALES_SLIP_NUMBER,BANK_NAME,BANK_SLIP_NUMBER,MEMO,STATUS_FLAG,SEND_FLAG,CREATE_DATE_TIME,CREATE_USER,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
                strSql.Append(" values (");
                strSql.Append("@SLIP_NUMBER,@CASH_DATE,@PROFIT_CASH,@LAST_CASH,@TAKE_CASH,@BALANCE_CASH,@SALES_SLIP_NUMBER,@BANK_NAME,@BANK_SLIP_NUMBER,@MEMO,@STATUS_FLAG,@SEND_FLAG,@CREATE_DATE_TIME,@CREATE_USER,@LAST_UPDATE_TIME,@LAST_UPDATE_USER)");
                SqlParameter[] parameters = {
					new SqlParameter("@SLIP_NUMBER", SqlDbType.VarChar,20),
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
					new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                parameters[0].Value = row["SLIP_NUMBER"];
                parameters[1].Value = row["CASH_DATE"];
                parameters[2].Value = row["PROFIT_CASH"];
                parameters[3].Value = row["LAST_CASH"];
                parameters[4].Value = row["TAKE_CASH"];
                parameters[5].Value = row["BALANCE_CASH"];
                parameters[6].Value = row["SALES_SLIP_NUMBER"];
                parameters[7].Value = row["BANK_NAME"];
                parameters[8].Value = row["BANK_SLIP_NUMBER"];
                parameters[9].Value = row["MEMO"];
                parameters[10].Value = row["STATUS_FLAG"];
                parameters[11].Value = row["SEND_FLAG"];
                parameters[12].Value = row["CREATE_DATE_TIME"];
                parameters[13].Value = row["CREATE_USER"];
                parameters[14].Value = row["LAST_UPDATE_TIME"];
                parameters[15].Value = row["LAST_UPDATE_USER"];
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

                if (rows > 0)
                {
                    dr["STATUS"] = CConstant.SUCCESS;
                }
                else
                {
                    dr["STATUS"] = CConstant.ERROR;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        //钱箱流水号修改
        public DataTable Update(DataSet ds)
        {
            DataTable dt = CommonManage.GetReturnDataTable();
            StringBuilder strSql = null;
            DataRow dr = null;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int rows = 0;
                strSql = new StringBuilder();
                dr = dt.NewRow();
                dr["SLIP_NUMBER"] = row["SLIP_NUMBER"];
                //修改流水号
                strSql.Append("UPDATE BLL_CASH SET BANK_SLIP_NUMBER=@BANK_SLIP_NUMBER WHERE SLIP_NUMBER=@SLIP_NUMBER");
                SqlParameter[] Updateparameters ={
                                                        new SqlParameter("@BANK_SLIP_NUMBER",SqlDbType.VarChar,225),
                                                        new SqlParameter("@SLIP_NUMBER",SqlDbType.VarChar,225)
                                                    };
                Updateparameters[0].Value = row["BANK_SLIP_NUMBER"];
                Updateparameters[1].Value = row["SLIP_NUMBER"];
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), Updateparameters);
                if (rows > 0)
                {
                    dr["STATUS"] = CConstant.SUCCESS;
                }
                else
                {
                    dr["STATUS"] = CConstant.ERROR;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public int GetCashCount(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BLL_CASH ");
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

        public DataSet GetCashInfo(string sqlWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.LAST_UPDATE_TIME desc");
            }
            strSql.Append(")AS Row, T.*  from BLL_CASH T ");
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                strSql.Append(" WHERE " + sqlWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}

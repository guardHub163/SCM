using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using POS.IDAL;
using POS.DBUtility;
using POS.Model;
using System.Collections;
using POS.Common;

namespace POS.SQLServerDAL
{
    public partial class ProductPriceManage : IProductPrice
    {
        public ProductPriceManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(decimal ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_PRODUCT_PRICE");
            strSql.Append(" where ID=@ID AND STATUS_FLAG <> " + Constant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 该记录是否删除
        /// </summary>
        public bool isDelete(decimal ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_PRODUCT_PRICE");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseProductPriceTable model)
        {
            if (isDelete(model.ID))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_PRODUCT_PRICE(");
            strSql.Append("ID,DEPARTMENT_CODE,STYLE_CODE,ORI_PRICE,PRICE_CODE,DISCOUNT_RATE,SALES_PRICE,DEFAULT_FLAG,START_DATE,END_DATE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_USER,LAST_UPDATE_TIME)");
            strSql.Append(" values (");
            strSql.Append("@ID,@DEPARTMENT_CODE,@STYLE_CODE,@ORI_PRICE,@PRICE_CODE,@DISCOUNT_RATE,@SALES_PRICE,@DEFAULT_FLAG,@START_DATE,@END_DATE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,@CREATE_DATE_TIME,@LAST_UPDATE_USER,@LAST_UPDATE_TIME)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                                            new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STYLE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@PRICE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					new SqlParameter("@SALES_PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@DEFAULT_FLAG", SqlDbType.Int,4),
					new SqlParameter("@START_DATE", SqlDbType.DateTime),
					new SqlParameter("@END_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@CREATE_DATE_TIME",SqlDbType.DateTime),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                     new SqlParameter("@LAST_UPDATE_TIME",SqlDbType.DateTime)
					                     };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.DEPARTMENT_CODE;
            parameters[2].Value = model.STYLE_CODE;
            parameters[3].Value = model.ORI_PRICE;
            parameters[4].Value = model.PRICE_CODE;
            parameters[5].Value = model.DISCOUNT_RATE;
            parameters[6].Value = model.SALES_PRICE;
            parameters[7].Value = model.DEFAULT_FLAG;
            parameters[8].Value = model.START_DATE;
            parameters[9].Value = model.END_DATE;
            parameters[10].Value = model.STATUS_FLAG;
            parameters[11].Value = model.ATTRIBUTE1;
            parameters[12].Value = model.ATTRIBUTE2;
            parameters[13].Value = model.ATTRIBUTE3;
            parameters[14].Value = model.CREATE_USER;
            parameters[15].Value = model.CREATE_DATE_TIME;
            parameters[16].Value = model.LAST_UPDATE_USER;
            parameters[17].Value = model.LAST_UPDATE_TIME;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseProductPriceTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_PRODUCT_PRICE set ");
            strSql.Append("DEPARTMENT_CODE=@DEPARTMENT_CODE,");
            strSql.Append("STYLE_CODE=@STYLE_CODE,");
            strSql.Append("ORI_PRICE=@ORI_PRICE,");
            strSql.Append("PRICE_CODE=@PRICE_CODE,");
            strSql.Append("DISCOUNT_RATE=@DISCOUNT_RATE,");
            strSql.Append("SALES_PRICE=@SALES_PRICE,");
            strSql.Append("DEFAULT_FLAG=@DEFAULT_FLAG,");
            strSql.Append("START_DATE=@START_DATE,");
            strSql.Append("END_DATE=@END_DATE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=@LAST_UPDATE_TIME,");
            strSql.Append("CREATE_USER=@CREATE_USER,");
            strSql.Append("CREATE_DATE_TIME=@CREATE_DATE_TIME");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal,9),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STYLE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ORI_PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@PRICE_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@DISCOUNT_RATE", SqlDbType.Decimal,9),
					new SqlParameter("@SALES_PRICE", SqlDbType.Decimal,9),
					new SqlParameter("@DEFAULT_FLAG", SqlDbType.Int,4),
					new SqlParameter("@START_DATE", SqlDbType.DateTime),
					new SqlParameter("@END_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@CREATE_USER",SqlDbType.VarChar,20),
                    new SqlParameter("@CREATE_DATE_TIME",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.DEPARTMENT_CODE;
            parameters[2].Value = model.STYLE_CODE;
            parameters[3].Value = model.ORI_PRICE;
            parameters[4].Value = model.PRICE_CODE;
            parameters[5].Value = model.DISCOUNT_RATE;
            parameters[6].Value = model.SALES_PRICE;
            parameters[7].Value = model.DEFAULT_FLAG;
            parameters[8].Value = model.START_DATE;
            parameters[9].Value = model.END_DATE;
            parameters[10].Value = model.STATUS_FLAG;
            parameters[11].Value = model.ATTRIBUTE1;
            parameters[12].Value = model.ATTRIBUTE2;
            parameters[13].Value = model.ATTRIBUTE3;
            parameters[14].Value = model.LAST_UPDATE_USER;
            parameters[15].Value = model.LAST_UPDATE_TIME;
            parameters[16].Value = model.CREATE_USER;
            parameters[17].Value = model.CREATE_DATE_TIME;
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(decimal ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE BASE_PRODUCT_PRICE SET STATUS_FLAG = " + Constant.DELETE);
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)
};
            parameters[0].Value = ID;

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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BaseProductPriceTable GetModel(decimal ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TOP 1 BPG.*,BU1.TRUE_NAME AS CREATE_NAME,BU2.TRUE_NAME AS LAST_UPDATE_NAME,NA.NAME AS PARENT_NAME,BD.NAME AS DEPARTMENT_NAME,BS.NAME AS STYLE_NAME ");
            strSql.Append("from BASE_PRODUCT_PRICE BPG ");
            strSql.Append("left join Base_User BU1 ON BPG.CREATE_USER=BU1.USER_ID ");
            strSql.Append("left join Base_User BU2 ON BPG.LAST_UPDATE_USER=BU2.USER_ID ");
            strSql.Append("left join NAMES NA ON NA.CODE=BPG.PRICE_CODE AND NA.CODE_TYPE='PRICE_CODE' ");
            strSql.Append("LEFT JOIN BASE_DEPARTMENT BD ON BPG.DEPARTMENT_CODE=BD.CODE ");
            strSql.Append("left join BASE_STYLE BS ON BPG.STYLE_CODE=BS.CODE ");
            strSql.Append(" where BPG.ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Decimal)
                                        };
            parameters[0].Value = ID;

            BaseProductPriceTable model = new BaseProductPriceTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = decimal.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.DEPARTMENT_CODE = ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString();
                model.STYLE_CODE = ds.Tables[0].Rows[0]["STYLE_CODE"].ToString();
                model.PRICE_CODE = ds.Tables[0].Rows[0]["PRICE_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["SALES_PRICE"].ToString() != "")
                {
                    model.SALES_PRICE = decimal.Parse(ds.Tables[0].Rows[0]["SALES_PRICE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ORI_PRICE"].ToString() != "")
                {
                    model.ORI_PRICE = decimal.Parse(ds.Tables[0].Rows[0]["ORI_PRICE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString() != "")
                {
                    model.DISCOUNT_RATE = decimal.Parse(ds.Tables[0].Rows[0]["DISCOUNT_RATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DEFAULT_FLAG"].ToString() != "")
                {
                    model.DEFAULT_FLAG = int.Parse(ds.Tables[0].Rows[0]["DEFAULT_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["START_DATE"].ToString() != "")
                {
                    model.START_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["START_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["END_DATE"].ToString() != "")
                {
                    model.END_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["END_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                model.Department_name = ds.Tables[0].Rows[0]["DEPARTMENT_NAME"].ToString();
                model.Price_name = ds.Tables[0].Rows[0]["PARENT_NAME"].ToString();
                model.Style_name = ds.Tables[0].Rows[0]["STYLE_NAME"].ToString();
                model.Creat_name = ds.Tables[0].Rows[0]["CREATE_NAME"].ToString();
                model.Update_name = ds.Tables[0].Rows[0]["LAST_UPDATE_NAME"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    model.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
                model.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    model.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataSet getSalesPrice(string styleCode, string departmentCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT DEPARTMENT_CODE,NA.NAME AS PRICE_NAME,SALES_PRICE,DISCOUNT_RATE,ORI_PRICE FROM BASE_PRODUCT_PRICE BPP LEFT JOIN dbo.NAMES AS NA ON NA.CODE=BPP.PRICE_CODE AND NA.CODE_TYPE='PRICE_CODE' ");
            strSql.Append("WHERE BPP.STYLE_CODE='" + styleCode + "' AND BPP.START_DATE<=getdate() ");
            strSql.Append("AND (BPP.END_DATE is NULL OR BPP.END_DATE>=getdate()) ");
            strSql.Append("AND (BPP.DEPARTMENT_CODE='D0000' OR BPP.DEPARTMENT_CODE='" + departmentCode + "') ");
            strSql.Append("AND BPP.STATUS_FLAG<>9 ORDER BY DEPARTMENT_CODE ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

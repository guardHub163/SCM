using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.IDAL;
using SCM.Model;
using System.Data;
using SCM.Common;
using SCM.DBUtility;
using System.Data.SqlClient;

namespace SCM.SQLServerDAL
{
    public class ProductItemManage : IProductItem
    {
        public DataSet GetItemList(string productCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from base_product_item_view ");
            strSql.AppendFormat(" where STATUS_FLAG <> {0}", CConstant.DELETE);
            strSql.AppendFormat(" and PRODUCT_CODE = '{0}'  order by PRODUCT_CODE", productCode);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string poductCode, string itemCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(ITEM_CODE) from BASE_PRODUCT_ITEM");
            strSql.Append(" where PRODUCT_CODE=@PRODUCT_CODE AND ITEM_CODE=@ITEM_CODE AND STATUS_FLAG <> " + CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@ITEM_CODE", SqlDbType.VarChar,20)};
            parameters[0].Value = poductCode;
            parameters[1].Value = itemCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 该记录是否删除
        /// </summary>
        private bool isDelete(string PRODUCT_CODE, string ITEM_CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_PRODUCT_ITEM");
            strSql.Append(" where PRODUCT_CODE=@PRODUCT_CODE and ITEM_CODE=@ITEM_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@ITEM_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = PRODUCT_CODE;
            parameters[1].Value = ITEM_CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseProductItemTable model)
        {
            if (isDelete(model.PRODUCT_CODE, model.ITEM_CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_PRODUCT_ITEM(");
            strSql.Append("PRODUCT_CODE,ITEM_CODE,QUANTITY,STATUS_FLAG,SUPPLIER_CODE,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@PRODUCT_CODE,@ITEM_CODE,@QUANTITY,@STATUS_FLAG,@SUPPLIER_CODE,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,getdate(),getdate(),@LAST_UPDATE_USER)");
            SqlParameter[] parameters = {
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ITEM_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.VarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.VarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.VarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,255)};
            parameters[0].Value = model.PRODUCT_CODE;
            parameters[1].Value = model.ITEM_CODE;
            parameters[2].Value = model.QUANTITY;
            parameters[3].Value = model.STATUS_FLAG;
            parameters[4].Value = model.SUPPLIER_CODE;
            parameters[5].Value = model.ATTRIBUTE1;
            parameters[6].Value = model.ATTRIBUTE2;
            parameters[7].Value = model.ATTRIBUTE3;
            parameters[8].Value = model.CREATE_USER;
            parameters[9].Value = model.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseProductItemTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_PRODUCT_ITEM set ");
            strSql.Append("QUANTITY=@QUANTITY,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("SUPPLIER_CODE=@SUPPLIER_CODE,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,");
            strSql.Append("LAST_UPDATE_TIME=getdate(),");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER");
            strSql.Append(" where PRODUCT_CODE=@PRODUCT_CODE and ITEM_CODE=@ITEM_CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ITEM_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@QUANTITY", SqlDbType.Decimal,9),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@SUPPLIER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.VarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.VarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.VarChar,255),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,255)};
            parameters[0].Value = model.PRODUCT_CODE;
            parameters[1].Value = model.ITEM_CODE;
            parameters[2].Value = model.QUANTITY;
            parameters[3].Value = model.STATUS_FLAG;
            parameters[4].Value = model.SUPPLIER_CODE;
            parameters[5].Value = model.ATTRIBUTE1;
            parameters[6].Value = model.ATTRIBUTE2;
            parameters[7].Value = model.ATTRIBUTE3;
            parameters[8].Value = model.LAST_UPDATE_USER;

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
        public bool Delete(string PRODUCT_CODE, string ITEM_CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_PRODUCT_ITEM set STATUS_FLAG = " + CConstant.DELETE);
            strSql.Append(" where PRODUCT_CODE=@PRODUCT_CODE and ITEM_CODE=@ITEM_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@ITEM_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = PRODUCT_CODE;
            parameters[1].Value = ITEM_CODE;

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
        public BaseProductItemTable GetModel(string PRODUCT_CODE, string ITEM_CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 BPI.*,BI.NAME AS ITEM_NAME,BP.NAME AS PRODUCT_NAME,BS.NAME AS SUPPLIER_NAME ");
            strSql.Append("FROM BASE_PRODUCT_ITEM BPI ");
            strSql.Append("LEFT JOIN dbo.BASE_ITEM BI ON BPI.ITEM_CODE=BI.CODE ");
            strSql.Append("LEFT JOIN dbo.BASE_PRODUCT BP ON BP.CODE=BPI.PRODUCT_CODE ");
            strSql.Append("LEFT JOIN dbo.BASE_SUPPLIER BS ON BS.CODE=BPI.SUPPLIER_CODE");
            strSql.Append(" where BPI.PRODUCT_CODE=@PRODUCT_CODE and BPI.ITEM_CODE=@ITEM_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@PRODUCT_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@ITEM_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = PRODUCT_CODE;
            parameters[1].Value = ITEM_CODE;

            BaseProductItemTable model = new BaseProductItemTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PRODUCT_CODE = ds.Tables[0].Rows[0]["PRODUCT_CODE"].ToString();
                model.PRODUCT_NAME = ds.Tables[0].Rows[0]["PRODUCT_NAME"].ToString();
                model.ITEM_CODE = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                model.ITEM_NAME = ds.Tables[0].Rows[0]["ITEM_NAME"].ToString();
                if (ds.Tables[0].Rows[0]["QUANTITY"].ToString() != "")
                {
                    model.QUANTITY = decimal.Parse(ds.Tables[0].Rows[0]["QUANTITY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.SUPPLIER_CODE = ds.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString();
                model.SUPPLIER_NAME = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString() != "")
                {
                    model.CREATE_DATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE_TIME"].ToString());
                }
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
        /// 获取记录总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_product_item_view");
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.QUANTITY asc");
            }
            strSql.Append(")AS Row, T.* from base_product_item_view T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion
    }
}

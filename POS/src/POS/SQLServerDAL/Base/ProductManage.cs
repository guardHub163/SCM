using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.IDAL;
using POS.DBUtility;
using POS.Model;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using POS.Common;

namespace POS.SQLServerDAL
{
    public partial class ProductManage : IProduct
    {
        public ProductManage()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_PRODUCT");
            strSql.Append(" where CODE=@CODE AND STATUS_FLAG <> " + Constant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 该记录是否删除
        /// </summary>
        public bool isDelete(string CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_PRODUCT");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(POS.Model.BaseProductTable model)
        {
            if (isDelete(model.CODE))
            {
                return Update(model) ? 1 : 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BASE_PRODUCT(");
            strSql.Append("CODE,NAME,PRODUCT_SPEC,GROUP_CODE,STYLE,COLOR,SIZE,UNIT_CODE,STATUS_FLAG,ATTRIBUTE1,ATTRIBUTE2,ATTRIBUTE3,CREATE_USER,CREATE_DATE_TIME,LAST_UPDATE_TIME,LAST_UPDATE_USER)");
            strSql.Append(" values (");
            strSql.Append("@CODE,@NAME,@PRODUCT_SPEC,@GROUP_CODE,@STYLE,@COLOR,@SIZE,@UNIT_CODE,@STATUS_FLAG,@ATTRIBUTE1,@ATTRIBUTE2,@ATTRIBUTE3,@CREATE_USER,@CREATE_DATE_TIME,@LAST_UPDATE_TIME,@LAST_UPDATE_USER)");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@PRODUCT_SPEC", SqlDbType.NVarChar,255),
					new SqlParameter("@GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STYLE", SqlDbType.VarChar,20),
					new SqlParameter("@COLOR", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@CREATE_DATE_TIME",SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_TIME",SqlDbType.DateTime),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.PRODUCT_SPEC;
            parameters[3].Value = model.GROUP_CODE;
            parameters[4].Value = model.STYLE;
            parameters[5].Value = model.COLOR;
            parameters[6].Value = model.SIZE;
            parameters[7].Value = model.UNIT_CODE;
            parameters[8].Value = model.STATUS_FLAG;
            parameters[9].Value = model.ATTRIBUTE1;
            parameters[10].Value = model.ATTRIBUTE2;
            parameters[11].Value = model.ATTRIBUTE3;
            parameters[12].Value = model.CREATE_USER;
            parameters[13].Value = model.CREATE_DATE_TIME;
            parameters[14].Value = model.LAST_UPDATE_TIME;
            parameters[15].Value = model.LAST_UPDATE_USER;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(POS.Model.BaseProductTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_PRODUCT set ");
            strSql.Append("NAME=@NAME,");
            strSql.Append("PRODUCT_SPEC=@PRODUCT_SPEC,");
            strSql.Append("GROUP_CODE=@GROUP_CODE,");
            strSql.Append("STYLE=@STYLE,");
            strSql.Append("COLOR=@COLOR,");
            strSql.Append("SIZE=@SIZE,");
            strSql.Append("UNIT_CODE=@UNIT_CODE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("ATTRIBUTE1=@ATTRIBUTE1,");
            strSql.Append("ATTRIBUTE2=@ATTRIBUTE2,");
            strSql.Append("ATTRIBUTE3=@ATTRIBUTE3,"); ;
            strSql.Append("LAST_UPDATE_TIME=@LAST_UPDATE_TIME,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("CREATE_USER=@CREATE_USER,");
            strSql.Append("CREATE_DATE_TIME=@CREATE_DATE_TIME");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,20),
					new SqlParameter("@NAME", SqlDbType.NVarChar,255),
					new SqlParameter("@PRODUCT_SPEC", SqlDbType.NVarChar,255),
					new SqlParameter("@GROUP_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STYLE", SqlDbType.VarChar,20),
					new SqlParameter("@COLOR", SqlDbType.VarChar,20),
					new SqlParameter("@SIZE", SqlDbType.VarChar,20),
					new SqlParameter("@UNIT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@ATTRIBUTE1", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE2", SqlDbType.NVarChar,255),
					new SqlParameter("@ATTRIBUTE3", SqlDbType.NVarChar,255),
					new SqlParameter("@LAST_UPDATE_TIME", SqlDbType.DateTime),
                    new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@CREATE_DATE_TIME", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.CODE;
            parameters[1].Value = model.NAME;
            parameters[2].Value = model.PRODUCT_SPEC;
            parameters[3].Value = model.GROUP_CODE;
            parameters[4].Value = model.STYLE;
            parameters[5].Value = model.COLOR;
            parameters[6].Value = model.SIZE;
            parameters[7].Value = model.UNIT_CODE;
            parameters[8].Value = model.STATUS_FLAG;
            parameters[9].Value = model.ATTRIBUTE1;
            parameters[10].Value = model.ATTRIBUTE2;
            parameters[11].Value = model.ATTRIBUTE3;
            parameters[12].Value = model.LAST_UPDATE_TIME;
            parameters[13].Value = model.LAST_UPDATE_USER;
            parameters[14].Value = model.CREATE_USER;
            parameters[15].Value = model.CREATE_DATE_TIME;
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
        public bool Delete(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_PRODUCT set STATUS_FLAG = " + Constant.DELETE);
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

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
        public POS.Model.BaseProductTable GetModel(string CODE)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from base_Product_view");
            strSql.Append(" where CODE=@CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CODE;

            POS.Model.BaseProductTable model = new POS.Model.BaseProductTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CODE = ds.Tables[0].Rows[0]["CODE"].ToString();
                model.NAME = ds.Tables[0].Rows[0]["NAME"].ToString();
                model.PRODUCT_SPEC = ds.Tables[0].Rows[0]["PRODUCT_SPEC"].ToString();
                model.GROUP_CODE = ds.Tables[0].Rows[0]["GROUP_CODE"].ToString();
                model.STYLE = ds.Tables[0].Rows[0]["STYLE"].ToString();
                model.COLOR = ds.Tables[0].Rows[0]["COLOR"].ToString();
                model.SIZE = ds.Tables[0].Rows[0]["SIZE"].ToString();
                model.UNIT_CODE = ds.Tables[0].Rows[0]["UNIT_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.ATTRIBUTE1 = ds.Tables[0].Rows[0]["ATTRIBUTE1"].ToString();
                model.ATTRIBUTE2 = ds.Tables[0].Rows[0]["ATTRIBUTE2"].ToString();
                model.ATTRIBUTE3 = ds.Tables[0].Rows[0]["ATTRIBUTE3"].ToString();
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                model.STYLE_NAME = ds.Tables[0].Rows[0]["STYLE_NAME"].ToString();
                model.PRODUCT_GROUP_NAME = ds.Tables[0].Rows[0]["PRODUCT_GROUP_NAME"].ToString();
                model.COLOR_NAME = ds.Tables[0].Rows[0]["COLOR_NAME"].ToString();
                model.SIZE_NAME = ds.Tables[0].Rows[0]["SIZE_NAME"].ToString();
                model.UNIT_NAME = ds.Tables[0].Rows[0]["UNIT_NAME"].ToString();
                model.UPDATE_USER_NAME = ds.Tables[0].Rows[0]["UPDATE_USER_NAME"].ToString();
                model.CREATE_USER_NAME = ds.Tables[0].Rows[0]["CREATE_USER_NAME"].ToString();
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
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_Product_view");
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
                strSql.Append("order by T.CODE asc");
            }
            strSql.Append(")AS Row, T.* from base_Product_view T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CODE,PRODUCT_NAME,PRODUCT_SPEC,PRODUCT_GROUP,STYLE,COLOR,SIZE,UNIT_CODE,STATUS_FLAG,LAST_UPDATE_TIME,LAST_UPDATE_USER ");
            strSql.Append(" FROM BASE_PRODUCT ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得商品数据列表
        /// </summary>
        public DataSet GetProductList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT P.CODE,P.NAME AS P_NAME,P.STYLE,P.SIZE AS P_SIZE,C.NAME AS C_NAME ");
            strSql.Append(" FROM BASE_PRODUCT P LEFT JOIN  BASE_COLOR C ON (P.COLOR=C.CODE) ");
            strSql.Append(" WHERE P.STATUS_FLAG <> 9 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  Method
    }
}

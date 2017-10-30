using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SCM.IDAL;
using SCM.DBUtility;
using SCM.Model;
using System.Collections;
using SCM.Common;
using System.Collections.Generic;//Please add references
namespace SCM.SQLServerDAL
{
    /// <summary>
    /// 数据访问类:Base_User
    /// </summary>
    public partial class UserManage : IUser
    {
        public UserManage()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string USER_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Base_User");
            strSql.AppendFormat(" where USER_ID=@USER_ID and status_flag <> {0} ", CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@USER_ID", SqlDbType.VarChar,50)};
            parameters[0].Value = USER_ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 记录是否己删除
        /// </summary>
        private bool isDelete(string USER_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Base_User");
            strSql.Append(" where USER_ID=@USER_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@USER_ID", SqlDbType.VarChar,50)};
            parameters[0].Value = USER_ID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseUserTable userTable)
        {
            if (isDelete(userTable.USER_ID))
            {
                return Update(userTable) ? 1 : 0;

            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Base_User(");
            strSql.Append("USER_ID,PASSWORD,TRUE_NAME,SEX,PHONE,EMAIL,DEPARTMENT_CODE,STATUS_FLAG,USER_TYPE,ROLES_ID,STYLE,CREATE_USER_ID,CREATE_DATE,LAST_UPDATE_USER_ID,LAST_UPDATE_TIME,PHOTO_PATH,PHOTO,SUPPLIER_CODE)");
            strSql.Append(" values (");
            strSql.Append("@USER_ID,@PASSWORD,@TRUE_NAME,@SEX,@PHONE,@EMAIL,@DEPARTMENT_CODE,@STATUS_FLAG,@USER_TYPE,@ROLES_ID,@STYLE,@CREATE_USER_ID,getdate(),@LAST_UPDATE_USER_ID,getdate(),@PHOTO_PATH,@PHOTO,@SUPPLIER_CODE)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@USER_ID", SqlDbType.VarChar,50),
					new SqlParameter("@PASSWORD", SqlDbType.VarChar,50),
					new SqlParameter("@TRUE_NAME", SqlDbType.VarChar,50),
					new SqlParameter("@SEX", SqlDbType.VarChar,2),
					new SqlParameter("@PHONE", SqlDbType.VarChar,20),
					new SqlParameter("@EMAIL", SqlDbType.VarChar,100),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@USER_TYPE", SqlDbType.VarChar,2),
					new SqlParameter("@ROLES_ID", SqlDbType.Int,4),
					new SqlParameter("@STYLE", SqlDbType.Int,4),
					new SqlParameter("@CREATE_USER_ID", SqlDbType.VarChar,50),					
					new SqlParameter("@LAST_UPDATE_USER_ID", SqlDbType.VarChar,50),
                    new SqlParameter("@PHOTO_PATH", SqlDbType.VarChar,200),					
					new SqlParameter("@PHOTO", SqlDbType.Binary),
                    new SqlParameter("@SUPPLIER_CODE",SqlDbType.VarChar,20)};
            parameters[0].Value = userTable.USER_ID;
            parameters[1].Value = SCM.DBUtility.DESEncrypt.Encrypt(userTable.PASSWORD);
            parameters[2].Value = userTable.TRUE_NAME;
            parameters[3].Value = userTable.SEX;
            parameters[4].Value = userTable.PHONE;
            parameters[5].Value = userTable.EMAIL;
            parameters[6].Value = userTable.DEPARTMENT_CODE;
            parameters[7].Value = userTable.STATUS_FLAG;
            parameters[8].Value = userTable.USER_TYPE;
            parameters[9].Value = userTable.ROLES_ID;
            parameters[10].Value = userTable.STYLE;
            parameters[11].Value = userTable.CREATE_USER_ID;
            parameters[12].Value = userTable.LAST_UPDATE_USER_ID;
            parameters[13].Value = userTable.PHOTO_PATH;
            parameters[14].Value = userTable.PHOTO;
            parameters[15].Value = userTable.SUPPLIER_CODE;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseUserTable userTable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Base_User set ");
            strSql.Append("TRUE_NAME=@TRUE_NAME,");
            strSql.Append("SEX=@SEX,");
            strSql.Append("PHONE=@PHONE,");
            strSql.Append("EMAIL=@EMAIL,");
            strSql.Append("DEPARTMENT_CODE=@DEPARTMENT_CODE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("USER_TYPE=@USER_TYPE,");
            strSql.Append("ROLES_ID=@ROLES_ID,");
            strSql.Append("STYLE=@STYLE,");
            strSql.Append("LAST_UPDATE_USER_ID=@LAST_UPDATE_USER_ID,");
            strSql.Append("LAST_UPDATE_TIME=getdate(),");
            strSql.Append("PHOTO_PATH=@PHOTO_PATH,");
            strSql.Append("PHOTO=@PHOTO,");
            strSql.Append("SUPPLIER_CODE=@SUPPLIER_CODE");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@TRUE_NAME", SqlDbType.VarChar,50),
					new SqlParameter("@SEX", SqlDbType.VarChar,2),
					new SqlParameter("@PHONE", SqlDbType.VarChar,20),
					new SqlParameter("@EMAIL", SqlDbType.VarChar,100),
					new SqlParameter("@DEPARTMENT_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@USER_TYPE", SqlDbType.VarChar,2),
					new SqlParameter("@ROLES_ID", SqlDbType.Int,4),
					new SqlParameter("@STYLE", SqlDbType.Int,4),
					new SqlParameter("@LAST_UPDATE_USER_ID", SqlDbType.VarChar,50),
                    new SqlParameter("@PHOTO_PATH", SqlDbType.VarChar,200),					
					new SqlParameter("@PHOTO", SqlDbType.Binary),
                    new SqlParameter("@SUPPLIER_CODE",SqlDbType.VarChar,20),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = userTable.TRUE_NAME;
            parameters[1].Value = userTable.SEX;
            parameters[2].Value = userTable.PHONE;
            parameters[3].Value = userTable.EMAIL;
            parameters[4].Value = userTable.DEPARTMENT_CODE;
            parameters[5].Value = userTable.STATUS_FLAG;
            parameters[6].Value = userTable.USER_TYPE;
            parameters[7].Value = userTable.ROLES_ID;
            parameters[8].Value = userTable.STYLE;
            parameters[9].Value = userTable.LAST_UPDATE_USER_ID;
            parameters[10].Value = userTable.PHOTO_PATH;
            parameters[11].Value = userTable.PHOTO;
            parameters[12].Value = userTable.SUPPLIER_CODE;
            parameters[13].Value = userTable.ID;

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
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Base_User set STATUS_FLAG = " + CConstant.DELETE);
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string USER_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Base_User set STATUS_FLAG = " + CConstant.DELETE);
            strSql.Append(" where USER_ID=@USER_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@USER_ID", SqlDbType.VarChar,50)			};
            parameters[0].Value = USER_ID;

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
        public BaseUserTable GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,USER_ID,PASSWORD,TRUE_NAME,SEX,PHONE,EMAIL,DEPARTMENT_CODE,STATUS_FLAG,USER_TYPE,ROLES_ID,STYLE,CREATE_USER_ID,CREATE_DATE,LAST_UPDATE_USER_ID,LAST_UPDATE_TIME,PHOTO_PATH,PHOTO,SUPPLIER_CODE,DEPARTMENT_NAME,SUPPLIER_NAME from base_user_view ");
            strSql.AppendFormat(" where STATUS_FLAG <> {0} AND ID=@ID", CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            BaseUserTable model = new SCM.Model.BaseUserTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USER_ID"] != null && ds.Tables[0].Rows[0]["USER_ID"].ToString() != "")
                {
                    model.USER_ID = ds.Tables[0].Rows[0]["USER_ID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PASSWORD"] != null && ds.Tables[0].Rows[0]["PASSWORD"].ToString() != "")
                {
                    model.PASSWORD = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TRUE_NAME"] != null && ds.Tables[0].Rows[0]["TRUE_NAME"].ToString() != "")
                {
                    model.TRUE_NAME = ds.Tables[0].Rows[0]["TRUE_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SEX"] != null && ds.Tables[0].Rows[0]["SEX"].ToString() != "")
                {
                    model.SEX = ds.Tables[0].Rows[0]["SEX"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PHONE"] != null && ds.Tables[0].Rows[0]["PHONE"].ToString() != "")
                {
                    model.PHONE = ds.Tables[0].Rows[0]["PHONE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EMAIL"] != null && ds.Tables[0].Rows[0]["EMAIL"].ToString() != "")
                {
                    model.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DEPARTMENT_CODE"] != null && ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString() != "")
                {
                    model.DEPARTMENT_CODE = ds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"] != null && ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USER_TYPE"] != null && ds.Tables[0].Rows[0]["USER_TYPE"].ToString() != "")
                {
                    model.USER_TYPE = ds.Tables[0].Rows[0]["USER_TYPE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ROLES_ID"] != null && ds.Tables[0].Rows[0]["ROLES_ID"].ToString() != "")
                {
                    model.ROLES_ID = int.Parse(ds.Tables[0].Rows[0]["ROLES_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STYLE"] != null && ds.Tables[0].Rows[0]["STYLE"].ToString() != "")
                {
                    model.STYLE = int.Parse(ds.Tables[0].Rows[0]["STYLE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CREATE_USER_ID"] != null && ds.Tables[0].Rows[0]["CREATE_USER_ID"].ToString() != "")
                {
                    model.CREATE_USER_ID = ds.Tables[0].Rows[0]["CREATE_USER_ID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CREATE_DATE"] != null && ds.Tables[0].Rows[0]["CREATE_DATE"].ToString() != "")
                {
                    model.CREATE_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_USER_ID"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_USER_ID"].ToString() != "")
                {
                    model.LAST_UPDATE_USER_ID = ds.Tables[0].Rows[0]["LAST_UPDATE_USER_ID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"] != null && ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    model.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PHOTO_PATH"] != null && ds.Tables[0].Rows[0]["PHOTO_PATH"].ToString() != "")
                {
                    model.PHOTO_PATH = ds.Tables[0].Rows[0]["PHOTO_PATH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PHOTO"] != null && ds.Tables[0].Rows[0]["PHOTO"].ToString() != "")
                {
                    model.PHOTO = (byte[])ds.Tables[0].Rows[0]["PHOTO"];
                }
                if (ds.Tables[0].Rows[0]["SUPPLIER_CODE"] != null && ds.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString() != "")
                {
                    model.SUPPLIER_CODE = ds.Tables[0].Rows[0]["SUPPLIER_CODE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DEPARTMENT_NAME"] != null && ds.Tables[0].Rows[0]["DEPARTMENT_NAME"].ToString() != "")
                {
                    model.DEPARTMENT_NAME = ds.Tables[0].Rows[0]["DEPARTMENT_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SUPPLIER_NAME"] != null && ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString() != "")
                {
                    model.SUPPLIER_NAME = ds.Tables[0].Rows[0]["SUPPLIER_NAME"].ToString();
                }
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
            strSql.Append("select count(1) FROM Base_User ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from base_user_view T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }


        //根据部门获得用户
        public DataSet GetDepartmentUserInfo(string Deparetment_code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT USER_ID,TRUE_NAME FROM BASE_USER BU ");
            strSql.Append("LEFT JOIN BASE_DEPARTMENT AS BD ON BD.CODE=BU.DEPARTMENT_CODE ");
            strSql.AppendFormat("WHERE BU.DEPARTMENT_CODE='{0}' OR BD.PARENT_CODE='{1}'", Deparetment_code, Deparetment_code);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #region 登陆验证
        /// <summary>
        /// 登陆验证
        /// </summary>
        public BaseUserTable ValidateLogin(string userId, string password)
        {
            BaseUserTable user = null;
            string sqlStr = "select top 1 * from base_user_view where user_id=@user_id and password=@password and status_flag <> " + CConstant.DELETE;
            SqlParameter[] parameters = {
                    new SqlParameter("@user_id", SqlDbType.VarChar, 255),
                    new SqlParameter("@password", SqlDbType.VarChar, 255)};
            parameters[0].Value = userId;
            parameters[1].Value = SCM.DBUtility.DESEncrypt.Encrypt(password);
            SqlDataReader dr = DbHelperSQL.ExecuteReader(sqlStr, parameters);
            if (dr.Read())
            {
                user = (BaseUserTable)Populatesys_BaseUserTable(dr);
            }
            return user;
        }

        public BaseUserTable BaseUserTableDispByStr(string swhere)
        {
            BaseUserTable fam = new BaseUserTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = swhere;
            int RecordCount = 0;
            ArrayList lst = BaseUserTableList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (BaseUserTable)lst[0];
            }
            return fam;
        }

        public ArrayList BaseUserTableList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(Populatesys_BaseUserTable);
            qp.TableName = "base_user_view";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "ID";
            }
            return CommonManage.GetObjectList(mypd, qp, out RecordCount);
        }

        public object Populatesys_BaseUserTable(IDataReader dr)
        {
            BaseUserTable nc = new BaseUserTable();
            if (!Convert.IsDBNull(dr["ID"])) nc.ID = Convert.ToInt32(dr["ID"]);
            if (!Convert.IsDBNull(dr["USER_ID"])) nc.USER_ID = Convert.ToString(dr["USER_ID"]);
            if (!Convert.IsDBNull(dr["PASSWORD"])) nc.PASSWORD = Convert.ToString(dr["PASSWORD"]).Trim();
            if (!Convert.IsDBNull(dr["TRUE_NAME"])) nc.TRUE_NAME = Convert.ToString(dr["TRUE_NAME"]).Trim();
            if (!Convert.IsDBNull(dr["SEX"])) nc.SEX = Convert.ToString(dr["SEX"]).Trim();
            if (!Convert.IsDBNull(dr["PHONE"])) nc.PHONE = Convert.ToString(dr["PHONE"]).Trim();
            if (!Convert.IsDBNull(dr["EMAIL"])) nc.EMAIL = Convert.ToString(dr["EMAIL"]).Trim();
            if (!Convert.IsDBNull(dr["DEPARTMENT_CODE"])) nc.DEPARTMENT_CODE = Convert.ToString(dr["DEPARTMENT_CODE"]);
            if (!Convert.IsDBNull(dr["STATUS_FLAG"])) nc.STATUS_FLAG = Convert.ToInt32(dr["STATUS_FLAG"]);
            if (!Convert.IsDBNull(dr["USER_TYPE"])) nc.USER_TYPE = Convert.ToString(dr["USER_TYPE"]).Trim();
            if (!Convert.IsDBNull(dr["ROLES_ID"])) nc.ROLES_ID = Convert.ToInt32(dr["ROLES_ID"]);
            if (!Convert.IsDBNull(dr["STYLE"])) nc.STYLE = Convert.ToInt32(dr["STYLE"]);
            if (!Convert.IsDBNull(dr["CREATE_USER_ID"])) nc.CREATE_USER_ID = Convert.ToString(dr["CREATE_USER_ID"]);
            if (!Convert.IsDBNull(dr["CREATE_DATE"])) nc.CREATE_DATE = Convert.ToDateTime(dr["CREATE_DATE"]);
            if (!Convert.IsDBNull(dr["LAST_UPDATE_USER_ID"])) nc.LAST_UPDATE_USER_ID = Convert.ToString(dr["LAST_UPDATE_USER_ID"]);
            if (!Convert.IsDBNull(dr["LAST_UPDATE_TIME"])) nc.LAST_UPDATE_TIME = Convert.ToDateTime(dr["LAST_UPDATE_TIME"]);
            if (!Convert.IsDBNull(dr["PHOTO_PATH"])) nc.PHOTO_PATH = Convert.ToString(dr["PHOTO_PATH"]);
            if (!Convert.IsDBNull(dr["PHOTO"])) nc.PHOTO = (byte[])dr["PHOTO"];
            if (!Convert.IsDBNull(dr["SUPPLIER_CODE"])) nc.SUPPLIER_CODE = Convert.ToString(dr["SUPPLIER_CODE"]);
            if (!Convert.IsDBNull(dr["DEPARTMENT_NAME"])) nc.DEPARTMENT_NAME = Convert.ToString(dr["DEPARTMENT_NAME"]);
            if (!Convert.IsDBNull(dr["SUPPLIER_NAME"])) nc.SUPPLIER_NAME = Convert.ToString(dr["SUPPLIER_NAME"]);
            return nc;
        }
        #endregion

        #endregion  Method

        //修改密码
        public bool UpdatePassWord(BaseUserTable busertable)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE BASE_USER SET PASSWORD=@PASSWORD WHERE USER_ID=@USER_ID");
            SqlParameter[] parameters = {
					new SqlParameter("@PASSWORD", SqlDbType.VarChar,50),
                    new SqlParameter("@USER_ID",SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = SCM.DBUtility.DESEncrypt.Encrypt(busertable.PASSWORD);
            parameters[1].Value = busertable.USER_ID;

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


        #region IUser 成员


        public DataSet GetUserPageRole(string userType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT BP.* FROM BASE_TYPE_PERMISSIONS AS BTP");
            strSql.Append(" LEFT JOIN BASE_PERMISSIONS AS BP ON (BTP.PERMISSION_ID =BP.ID OR BTP.PERMISSION_ID = BP.CATEGORY_ID)");
            strSql.AppendFormat(" WHERE BTP.USER_TYPE = '{0}' ", userType);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetCateGories()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM BASE_CATEGORIES");
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetAllPower()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select BT.*,BC.DESCIRIPTION AS CATEGORY_DESCIRIPTION from dbo.BASE_PERMISSIONS AS BT ");
            //strSql.Append("LEFT JOIN dbo.BASE_TYPE_PERMISSIONS BTP ON BTP.PERMISSION_ID=BT.ID ");
            strSql.Append("LEFT JOIN dbo.BASE_CATEGORIES BC ON BC.ID=BT.CATEGORY_ID WHERE BT.IS_MENU=1 ORDER BY bc.SHOW_INDEX"); ;
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetSmallPower()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select N.CODE AS USER_TYPE,ISNULL(PERMISSION_ID,'') AS PERMISSION_ID, ");
            strSql.Append("ISNULL(CATEGORY_ID,'') AS CATEGORY_ID from dbo.NAMES AS N ");
            strSql.Append("LEFT JOIN (SELECT BTP.*,BP.CATEGORY_ID from BASE_TYPE_PERMISSIONS as btp ");
            strSql.Append("LEFT JOIN BASE_PERMISSIONS BP ON btp.PERMISSION_ID  = BP.id ) AS A ");
            strSql.Append("ON A.USER_TYPE=N.CODE WHERE N.CODE_TYPE='USER_TYPE' ");
            strSql.Append("ORDER BY A.USER_TYPE,A.CATEGORY_ID");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool PowerExcit(string type, string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_TYPE_PERMISSIONS");
            strSql.Append(" where USER_TYPE=@USER_TYPE and PERMISSION_ID=@PERMISSION_ID");
            SqlParameter[] parameters = {
					new SqlParameter("@USER_TYPE", SqlDbType.VarChar,50),
                                        new SqlParameter("@PERMISSION_ID",SqlDbType.VarChar,50)};
            parameters[0].Value = type;
            parameters[1].Value = value;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);

        }
        //权限保存
        public bool GetUserPower(Hashtable userType, string type)
        {
            Hashtable hsParentRoles = new Hashtable();
            List<CommandInfo> sqlList = new List<CommandInfo>();
            hsParentRoles = (Hashtable)userType[type];
            int rows = 0;
            foreach (DictionaryEntry hsp in hsParentRoles)
            {
                RolesTable parentRoles = (RolesTable)hsp.Value;
                foreach (RolesTable roles in parentRoles.ROLE_LIST)
                {
                    if (roles.IS_CHECK != roles.INIT_STATUS)
                    {
                        if (roles.IS_CHECK)
                        {
                            if (!PowerExcit(type, roles.VALUE))
                            {
                                StringBuilder strSql = new StringBuilder();
                                strSql.Append("insert into BASE_TYPE_PERMISSIONS(");
                                strSql.Append("USER_TYPE,PERMISSION_ID)");
                                strSql.Append(" values (");
                                strSql.Append("@USER_TYPE,@PERMISSION_ID)");
                                SqlParameter[] parameters = {
				                    new SqlParameter("@USER_TYPE", SqlDbType.VarChar,2),
				                    new SqlParameter("@PERMISSION_ID", SqlDbType.VarChar,4)};
                                parameters[0].Value = type;
                                parameters[1].Value = roles.VALUE;
                                sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
                            }
                        }
                        else
                        {
                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("delete from BASE_TYPE_PERMISSIONS ");
                            strSql.Append(" where USER_TYPE=@USER_TYPE and PERMISSION_ID=@PERMISSION_ID ");
                            SqlParameter[] parameters = {
					            new SqlParameter("@USER_TYPE", SqlDbType.VarChar,50),
					            new SqlParameter("@PERMISSION_ID", SqlDbType.VarChar,50)};
                            parameters[0].Value = type;
                            parameters[1].Value = roles.VALUE;
                            sqlList.Add(new CommandInfo(strSql.ToString(), parameters));
                        }
                    }

                }
            }

            if (sqlList.Count > 0)
            {
                rows = DbHelperSQL.ExecuteSqlTran(sqlList);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Bll;
using System.Data;
using POS.Model;
using UserCache;
using POS.Common;
using System.Collections;
using System.Net;
using System.Windows.Forms;

namespace POS
{
    public class WebServieceOperate
    {
        BCommon bcomm = new BCommon();

        #region 基本信息的获得
        public Hashtable GetDataInfo(string[] tableNames)
        {
            Hashtable ht = new Hashtable();
            int i = 0;
            try
            {
                POS.CZZD.SCM.WebService webService = new POS.CZZD.SCM.WebService();
                string serverDate = webService.GetSystemTime();
                string departmentCode = Cache.DEPARTMENT_CODE;
                foreach (string tableName in tableNames)
                {
                    string lastUpdateDate = Cache.GetLastUpdate(tableName);
                    string webServiceKey = departmentCode + tableName + lastUpdateDate + Common.Constant.WEBSERVICE_SCM2POS_KEY;
                    string xmlData = webService.GetDataInfo(departmentCode, tableName, lastUpdateDate, Common.DESEncrypt.Encrypt(webServiceKey));
                    string status = "";
                    try
                    {
                        status = xmlData.Substring(0, 2);
                    }
                    catch { }

                    if (status == Constant.SUCCESS)
                    {
                        i++;
                        DataSet ds = Data.ConvertXMLToDataSet(xmlData.Substring(2));
                        string str = Insert(tableName, serverDate, ds);
                        ht.Add(i, str);
                    }
                    else if (status == Constant.NO_DATA)
                    {

                    }
                    else if (status == Constant.ERROR)
                    {

                    }
                }
            }
            catch (WebException we)
            {
                ht.Clear();
                ht.Add("EXCEPTION", we.Message);
            }
            catch (Exception ex)
            {
                ht.Clear();
                ht.Add("EXCEPTION", ex.Message);
            }
            Cache.LAST_UPDATE = null;
            return ht;
        }

        private string Insert(string tableName, string serverDate, DataSet ds)
        {
            string str = "";
            switch (tableName)
            {
                case "PRODUCT_GROUP":
                    str = InsertProductGroup(tableName, serverDate, ds);
                    break;
                case "PRODUCT":
                    str = InsertProduct(tableName, serverDate, ds);
                    break;
                case "STYLE":
                    str = InsertStyle(tableName, serverDate, ds);
                    break;
                case "COLOR":
                    str = InsertColor(tableName, serverDate, ds);
                    break;
                case "SIZE":
                    str = InsertSize(tableName, serverDate, ds);
                    break;
                case "UNIT":
                    str = InsertUnit(tableName, serverDate, ds);
                    break;
                case "PRODUCT_PRICE":
                    str = InsertProductPrice(tableName, serverDate, ds);
                    break;
                case "VIP_CUSTOMER":
                    str = InsertVipCustomer(tableName, serverDate, ds);
                    break;
                case "USER":
                    str = InsertUser(tableName, serverDate, ds);
                    break;
                case "SALES_PROMOTION":
                    str = InsetSalesPromotion(tableName, serverDate, ds);
                    break;
                case "NAMES":
                    str = InsertNames(tableName, serverDate, ds);
                    break;
            }
            return str;
        }

        public DataTable NameTable() 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE_TYPE", Type.GetType("System.String"));
            dt.Columns.Add("CODE", Type.GetType("System.String"));
            dt.Columns.Add("NAME", Type.GetType("System.String"));
            dt.Columns.Add("STATUS_FLAG", Type.GetType("System.Int32"));
            return dt;

        }
        private string InsertNames(string tableName, string serverDate, DataSet ds)
        {
            DataRow[] dt = ds.Tables[0].Select("CODE_TYPE='POINT_TYPE'");
            DataTable nametable = NameTable();
            for (int i = 0; i < dt.Length; i++)
            {
                nametable.ImportRow(dt[i]);
            }
            int d = nametable.Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int c = 0;//添加失败的条数
            int e = 0;//修改失败的条数
            NamesTable btable = new NamesTable();
            BUser bll = new BUser();
            foreach (DataRow row in nametable.Rows)
            {
                btable.CODE_TYPE = Data.ToString(row["CODE_TYPE"]);
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                if (bll.isNameDelete(btable.CODE_TYPE,btable.CODE))
                {
                    if (bll.UpdatenName(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.AddName(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        c++;
                    }
                }
            }
            string items = "积分抵扣信息下载：本次总共下载" + d + "条，其中添加成功了" + a + "条，添加失败了" + c + "条，跟新成功了" + b + "条，跟新失败了" + c + "条。";
            return items;
        }

        private string InsetSalesPromotion(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int c = 0;//添加失败的条数
            int e = 0;//修改失败的条数
            BaseSalesPromotionTable btable = new BaseSalesPromotionTable();
            BSalesPromotion bll = new BSalesPromotion();
            foreach (DataRow  row in ds.Tables [0].Rows )
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.DEPARTMENT_CODE = Data.ToString(row["DEPARTMENT_CODE"]);
                btable.PROPERTY1 = Data.ToString(row["PROPERTY1"]);
                btable.PROPERTY2 = Data.ToString(row["PROPERTY2"]);
                btable.PROPERTY3 = Data.ToString(row["PROPERTY3"]);
                btable.PROPERTY4 = Data.ToString(row["PROPERTY4"]);
                btable.PROPERTY5 = Data.ToString(row["PROPERTY5"]);
                btable.START_TIME = Data.ToDateTime(row["START_TIME"]);
                btable.END_TIME = Data.ToDateTime(row["END_TIME"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.Update (btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.Add (btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        c++;
                    }
                }
            }
            string items = "客户信息下载：本次总共下载" + d + "条，其中添加成功了" + a + "条，添加失败了" + c + "条，跟新成功了" + b + "条，跟新失败了" + c + "条。";
            return items;
        }

        private string InsertUser(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            List<BaseUserTable> UserList = new List<BaseUserTable>();
            string items = "";
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            if (d <= 0)
            {
                items = "没有可下载的信息！";
            }
            else
            {
                BUser bll = new BUser();        
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    BaseUserTable btable = new BaseUserTable();
                    btable.ID = Data.ToInt(row["ID"]);
                    btable.USER_ID = Data.ToString(row["USER_ID"]);
                    btable.PASSWORD = Data.ToString(row["PASSWORD"]);
                    btable.TRUE_NAME = Data.ToString(row["TRUE_NAME"]);
                    btable.SEX = Data.ToString(row["SEX"]);
                    btable.PHONE = Data.ToString(row["PHONE"]);
                    btable.EMAIL = Data.ToString(row["EMAIL"]);
                    btable.DEPARTMENT_CODE = Data.ToString(row["DEPARTMENT_CODE"]);
                    btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                    btable.ROLES_ID = Data.ToInt(row["ROLES_ID"]);
                    btable.STYLE = Data.ToInt(row["STYLE"]);
                    btable.PHOTO_PATH = Data.ToString(row["PHOTO_PATH"]);
                    btable.SUPPLIER_CODE = Data.ToString(row["SUPPLIER_CODE"]);
                    btable.CREATE_USER_ID = Data.ToString(row["CREATE_USER_ID"]);
                    btable.CREATE_DATE = Data.ToDateTime(row["CREATE_DATE"]);
                    btable.LAST_UPDATE_USER_ID = Data.ToString(row["LAST_UPDATE_USER_ID"]);
                    btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                    UserList.Add(btable);
                }
                if (bll.Add(UserList) > 0) 
                {
                    a = d;
                }
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
                items = "用户信息下载：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            }
            return items;
        }

        private string InsertVipCustomer(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int c = 0;//添加失败的条数
            int e = 0;//修改失败的条数
            BaseVipCustomerTable btable = new BaseVipCustomerTable();
            BVipCustomer bll = new BVipCustomer();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.VIP_LEVEL = Data.ToInt(row["VIP_LEVEL"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.DEPARTMENT_CODE = Data.ToString(row["DEPARTMENT_CODE"]);
                btable.ADDRESS = Data.ToString(row["ADDRESS"]);
                btable.QQ = Data.ToString(row["QQ"]);
                btable.EMAIL = Data.ToString(row["EMAIL"]);
                btable.WW = Data.ToString(row["WW"]);
                btable.BIRTH_DATE = Data.ToDateTime(row["BIRTH_DATE"]);
                btable.LAST_SALES_DATE = Data.ToDateTime(row["LAST_SALES_DATE"]);
                btable.DISCOUNT_RATE = Data.Todecimal(row["DISCOUNT_RATE"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.POINTS = Data.ToInt(row["POINTS"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Convert.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.ToUpdate(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.ToAdd(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        c++;
                    }
                }

            }
            if (a == d || b == d)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "客户信息下载：本次总共下载" + d + "条，其中添加成功了" + a + "条，添加失败了" + c + "条，跟新成功了" + b + "条，跟新失败了" + c + "条。";
            return items;
        }

        private string InsertProductPrice(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            BProductPrice bll = new BProductPrice();
            BaseProductPriceTable btable = new BaseProductPriceTable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.ID = Data.Todecimal(row["ID"]);
                btable.DEPARTMENT_CODE = Data.ToString(row["DEPARTMENT_CODE"]);
                btable.STYLE_CODE = Data.ToString(row["STYLE_CODE"]);
                btable.PRICE_CODE = Data.ToString(row["PRICE_CODE"]);
                btable.SALES_PRICE = Data.Todecimal(row["SALES_PRICE"]);
                btable.ORI_PRICE = Data.Todecimal(row["ORI_PRICE"]);
                btable.DISCOUNT_RATE = Data.Todecimal(row["DISCOUNT_RATE"]);
                btable.DEFAULT_FLAG = Data.ToInt(row["DEFAULT_FLAG"]);
                btable.START_DATE = Data.ToDateTime(row["START_DATE"]);
                btable.END_DATE = Data.ToDateTime(row["END_DATE"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Data.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                if (bll.isDelete(btable.ID))
                {
                    if (bll.Update(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.Add(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        e++;
                    }
                }

            }
            if (e == 0)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "商品单价下载：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            return items;
        }

        private string InsertUnit(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            BUnit bll = new BUnit();
            BaseUnitTable btable = new BaseUnitTable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Data.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.Update(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.Add(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        e++;
                    }
                }
            }
            if (e == 0)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "商品单位下载：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            return items;

        }

        private string InsertSize(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            BSize bll = new BSize();
            BaseSizeTable btable = new BaseSizeTable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.PRODUCT_GROUP_CODE = Data.ToString(row["PRODUCT_GROUP_CODE"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Data.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.Update(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.Add(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        e++;
                    }
                }

            }
            if (e == 0)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "商品尺码下在：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            return items;
        }

        private string InsertColor(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            BColor bll = new BColor();
            BaseColorTable btable = new BaseColorTable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Data.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.Update(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.Add(btable) > 0)
                    {
                        a++;
                    }
                    else { e++; }
                }
            }
            if (e == 0)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "商品颜色下载：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            return items;
        }

        private string InsertStyle(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            BStyle bll = new BStyle();
            BaseStyleTable btable = new BaseStyleTable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Data.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.Update(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }
                }
                else
                {
                    if (bll.Add(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        e++;
                    }
                }
            }
            if (e == 0)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "商品款式下载：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            return items;
        }

        private string InsertProduct(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            BProduct bll = new BProduct();
            BaseProductTable btable = new BaseProductTable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.PRODUCT_SPEC = Data.ToString(row["PRODUCT_SPEC"]);
                btable.GROUP_CODE = Data.ToString(row["GROUP_CODE"]);
                btable.STYLE = Data.ToString(row["STYLE"]);
                btable.COLOR = Data.ToString(row["COLOR"]);
                btable.SIZE = Data.ToString(row["SIZE"]);
                btable.UNIT_CODE = Data.ToString(row["UNIT_CODE"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Data.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.Update(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }

                }
                else
                {
                    if (bll.Add(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        e++;
                    }
                }

            }
            if (e == 0)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "商品信息下载：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            return items;

        }

        private string InsertProductGroup(string tableName, string serverDate, DataSet ds)
        {
            int d = ds.Tables[0].Rows.Count;//下载的总条数
            int a = 0;//添加成功的条数
            int b = 0;//修改成功的条数
            int e = 0;//修改失败的条数
            BProductGroup bll = new BProductGroup();
            BaseProductGroupTable btable = new BaseProductGroupTable();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                btable.CODE = Data.ToString(row["CODE"]);
                btable.NAME = Data.ToString(row["NAME"]);
                btable.PARENT_CODE = Data.ToString(row["PARENT_CODE"]);
                btable.STATUS_FLAG = Data.ToInt(row["STATUS_FLAG"]);
                btable.ATTRIBUTE1 = Data.ToString(row["ATTRIBUTE1"]);
                btable.ATTRIBUTE2 = Data.ToString(row["ATTRIBUTE2"]);
                btable.ATTRIBUTE3 = Data.ToString(row["ATTRIBUTE3"]);
                btable.CREATE_DATE_TIME = Data.ToDateTime(row["CREATE_DATE_TIME"]);
                btable.CREATE_USER = Data.ToString(row["CREATE_USER"]);
                btable.LAST_UPDATE_TIME = Data.ToDateTime(row["LAST_UPDATE_TIME"]);
                btable.LAST_UPDATE_USER = Data.ToString(row["LAST_UPDATE_USER"]);
                if (bll.isDelete(btable.CODE))
                {
                    if (bll.Update(btable))
                    {
                        b++;
                    }
                    else
                    {
                        e++;
                    }

                }
                else
                {
                    if (bll.Add(btable) > 0)
                    {
                        a++;
                    }
                    else
                    {
                        e++;
                    }
                }

            }
            if (e == 0)
            {
                NamesTable name = new NamesTable();
                name.CODE = tableName;
                name.NAME = serverDate;
                name.CODE_TYPE = "LAST_UPDATE";
                name.STATUS_FLAG = 1;
                bcomm.UpdateNames(name);
            }
            string items = "商品款式下载：本次总共下载" + d + "条，新增：" + a + "条，更新：" + b + "条，失败：" + e + "条。";
            return items;
        }
        #endregion

        //吧新建的信息传回SCM
        public Hashtable SendDate(string[] tableNames)
        {
            Hashtable ht = new Hashtable();
            try
            {

                int a = 0;//成功返回的条数
                int b = 0;//失败返回的条数
                foreach (string tableName in tableNames)
                {
                    if (tableName == "CUSTOMER")//客户上传
                    {
                        BVipCustomer bvip = new BVipCustomer();
                        DataSet ds = bvip.GetAllInfo(getCostumerConduction());
                        POS.CZZD.SCM.WebService webService = new POS.CZZD.SCM.WebService();
                        DataTable da = ds.Tables[0];
                        string dataSetXml = Data.GetDataSetXml(tableName, da);
                        string webServiceKey = tableName + dataSetXml.Length + Common.Constant.WEBSERVICE_POS2SCM_KEY;
                        string retXml = webService.SetDataInfo(tableName, dataSetXml, Common.DESEncrypt.Encrypt(webServiceKey));

                        DataSet dst = Data.ConvertXMLToDataSet(retXml);
                        foreach (DataRow row in dst.Tables[0].Rows)
                        {
                            if (row["STATUS"].ToString() == Constant.SUCCESS)
                            {
                                bvip.UpdateFlag(Constant.NORMAL, row["SLIP_NUMBER"].ToString());
                                a++;
                            }
                            else

                            {
                                bvip.UpdateFlag(Constant.INIT, row["SLIP_NUMBER"].ToString());
                                b++;
                            }
                        }

                    }
                    else if (tableName == "CASH") //钱箱上传
                    {
                        BCash bCash = new BCash();
                        DataSet ds = bCash.GetCashInfo(getCashConduction());
                        DataTable da = ds.Tables[0];
                        string salesXml = Data.GetDataSetXml(tableName, da);
                        POS.CZZD.SCM.WebService webService = new POS.CZZD.SCM.WebService();
                        string webServiceKey = tableName + salesXml.Length + Common.Constant.WEBSERVICE_POS2SCM_KEY;
                        string retXml = webService.SetDataInfo(tableName, salesXml, Common.DESEncrypt.Encrypt(webServiceKey));

                        DataSet dst = Data.ConvertXMLToDataSet(retXml);
                        foreach (DataRow row in dst.Tables[0].Rows)
                        {
                            if (row["STATUS"].ToString() == Constant.SUCCESS)
                            {
                                bCash.UpdateFlag(Constant.NORMAL, row["SLIP_NUMBER"].ToString());
                                a++;
                            }
                            else
                            {
                                bCash.UpdateFlag(Constant.INIT, row["SLIP_NUMBER"].ToString());
                                b++;
                            }
                        }

                    }
                    else if (tableName == "SALES") //销售记录上传
                    {
                        BSalesOrder bSalesOrder = new BSalesOrder();
                        DataSet ds = bSalesOrder.GetSalesInfo(getSalesConduction());
                        DataTable da = ds.Tables[0];
                        string salesXml = Data.GetDataSetXml(tableName, da);
                        POS.CZZD.SCM.WebService webService = new POS.CZZD.SCM.WebService();
                        string webServiceKey = tableName + salesXml.Length + Common.Constant.WEBSERVICE_POS2SCM_KEY;
                        string retXml = webService.SetDataInfo(tableName, salesXml, Common.DESEncrypt.Encrypt(webServiceKey));

                        DataSet dst = Data.ConvertXMLToDataSet(retXml);
                        foreach (DataRow row in dst.Tables[0].Rows)
                        {
                            if (row["STATUS"].ToString() == Constant.SUCCESS)
                            {
                                bSalesOrder.UpdateFlge(Constant.NORMAL, row["SLIP_NUMBER"].ToString(), row["LINE_NUMBER"].ToString());
                                a++;
                            }
                            else
                            {
                                bSalesOrder.UpdateFlge(Constant.INIT, row["SLIP_NUMBER"].ToString(), row["LINE_NUMBER"].ToString());
                                b++;
                            }
                        }

                    }
                    else if (tableName == "CASH_BANK")
                    {
                        BCash bCash = new BCash();
                        DataSet ds = bCash.GetCashInfo(getCashConduction());
                        DataTable da = ds.Tables[0];
                        string salesXml = Data.GetDataSetXml(tableName, da);
                        POS.CZZD.SCM.WebService webService = new POS.CZZD.SCM.WebService();
                        string webServiceKey = tableName + salesXml.Length + Common.Constant.WEBSERVICE_POS2SCM_KEY;
                        string retXml = webService.SetDataInfo(tableName, salesXml, Common.DESEncrypt.Encrypt(webServiceKey));
                        DataSet dst = Data.ConvertXMLToDataSet(retXml);
                        foreach (DataRow row in dst.Tables[0].Rows)
                        {
                            if (row["STATUS"].ToString() == Constant.SUCCESS)
                            {
                                bCash.UpdateFlag(Constant.TO_CASH_FLAG, row["SLIP_NUMBER"].ToString());
                            }
                            else
                            {
                                bCash.UpdateFlag(Constant.NORMAL, row["SLIP_NUMBER"].ToString());
                            }
                        }
                    }
                    int c = a + b;
                    string items = "本次总共上传" + c + "条信息，其中上传成功" + a + "条，上传失败" + b + "条";
                    ht.Add(1, items);
                }

            }
            catch (WebException we)
            {
                ht.Clear();
                ht.Add("EXCEPTION", we.Message);
            }
            catch (Exception ex)
            {
                ht.Clear();
                ht.Add("EXCEPTION", "没有可上传的信息！");
            }
            return ht;
        }
        //客户上传条件
        public string getCostumerConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            return sb.ToString();
        }

        //钱箱上传条件
        private string getCashConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SEND_FLAG ={0} AND STATUS_FLAG<>0 OR STATUS_FLAG={1} AND SEND_FLAG={2} AND BANK_SLIP_NUMBER IS NOT NULL AND BANK_SLIP_NUMBER<>''", Constant.INIT, Constant.BANK_CASH, Constant.NORMAL);
            return sb.ToString();
        }
        //销售记录上传条件
        private string getSalesConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SEND_FLAG =" + Constant.INIT);
            return sb.ToString();
        }
    }
}

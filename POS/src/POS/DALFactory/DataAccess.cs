using System;
using System.Reflection;
using System.Configuration;
namespace POS.DALFactory
{
    /// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="Maticsoft.SQLServerDAL" />。
    /// </summary>
    public sealed class DataAccess
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
        public DataAccess()
        { }

        #region CreateObject

        //不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }

        }
        //使用缓存
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch (System.Exception ex)
                {
                    //string str=ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
        #endregion

        #region 泛型生成
        ///// <summary>
        ///// 创建数据层接口。
        ///// </summary>
        //public static t Create(string ClassName)
        //{

        //    string ClassNamespace = AssemblyPath +"."+ ClassName;
        //    object objType = CreateObject(AssemblyPath, ClassNamespace);
        //    return (t)objType;
        //}
        #endregion

        
        #region CreateCommonManage
        public static POS.IDAL.ICommon CreateCommonManage()
        {
            string classNamespace = AssemblyPath + ".CommonManage";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (POS.IDAL.ICommon)objType;
        }
        #endregion



        /// <summary>
        /// 创建Base_User数据层接口。
        /// </summary>
        public static POS.IDAL.IUser CreateUserManage()
        {

            string ClassNamespace = AssemblyPath + ".UserManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IUser)objType;
        }


      
        /// <summary>
        /// 创建BASE_PRODUCT数据层接口。
        /// </summary>
        public static POS.IDAL.IProduct CreateProductManage()
        {

            string ClassNamespace = AssemblyPath + ".ProductManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IProduct)objType;
        }

        /// <summary>
        /// 创建BASE_UNIT数据层接口。
        /// </summary>
        public static POS.IDAL.IUnit CreateUnitManage()
        {

            string ClassNamespace = AssemblyPath + ".UnitManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IUnit)objType;
        }
      

        /// <summary>
        /// 创建BASE_COLOR数据层接口。
        /// </summary>
        public static POS.IDAL.IColor CreateColorManage()
        {

            string ClassNamespace = AssemblyPath + ".ColorManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IColor)objType;
        }

        /// <summary>
        /// 创建BASE_SIZE数据层接口。
        /// </summary>
        public static POS.IDAL.ISize CreateSizeManage()
        {

            string ClassNamespace = AssemblyPath + ".SizeManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.ISize)objType;
        }

        /// <summary>
        /// 创建BASE_STYLE数据层接口。
        /// </summary>
        public static POS.IDAL.IStyle CreateStyleManage()
        {

            string ClassNamespace = AssemblyPath + ".StyleManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IStyle)objType;
        }

        /// <summary>
        /// 创建BASE_PRODUCT_GROUP数据层接口。
        /// </summary>
        public static POS.IDAL.IProductGroup CreateProductGroupManage()
        {

            string ClassNamespace = AssemblyPath + ".ProductGroupManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IProductGroup)objType;
        }

        /// <summary>
        /// 创建BASE_PRODUCT_PRICE数据层接口。
        /// </summary>
        public static POS.IDAL.IProductPrice CreateProductpriceManage()
        {

            string ClassNamespace = AssemblyPath + ".ProductPriceManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IProductPrice)objType;
        }        

        /// <summary>
        /// 创建BASE_VIP_CUSTOMER数据层接口。
        /// </summary>
        public static POS.IDAL.IVipCustomer CreateVipCustomerManage()
        {

            string ClassNamespace = AssemblyPath + ".VipCustomerManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.IVipCustomer)objType;
        }

   
        /// <summary>
        /// 创建Cash数据层接口。
        /// </summary>
        public static POS.IDAL.ICash CreateCashManage()
        {

            string ClassNamespace = AssemblyPath + ".CashManager";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.ICash)objType;
        }
        /// <summary>
        /// 创建SalesOrderManage数据层接口。
        /// </summary>
        public static POS.IDAL.ISalesOrder CreateSalesOrderManage()
        {

            string ClassNamespace = AssemblyPath + ".SalesOrderManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.ISalesOrder)objType;
        }

        ///<summary>
        ///创建SalesOrderPlan数据层接口
        ///</summary>
        public static POS.IDAL.ISalesOrderPlan CreateSalesOrderPlanManage() 
        {
            string ClassNamespace = AssemblyPath + ".SalesOrderPlanManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.ISalesOrderPlan)objType;
        }


        /// <summary>
        /// 创建BASE_SALES_PROMOTION数据层接口。
        /// </summary>
        public static POS.IDAL.ISalesPromotion CreateSalesPromotionManage()
        {

            string ClassNamespace = AssemblyPath + ".SalesPromotionManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (POS.IDAL.ISalesPromotion)objType;
        }

    }
}
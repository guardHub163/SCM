using System;
using System.Reflection;
using System.Configuration;
namespace SCM.DALFactory
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

        #region CreateSysManage
        public static SCM.IDAL.ISysManage CreateSysManage()
        {
            //方式1			
            //return (Maticsoft.IDAL.ISysManage)Assembly.Load(AssemblyPath).CreateInstance(AssemblyPath+".SysManage");

            //方式2 			
            string classNamespace = AssemblyPath + ".SysManage";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (SCM.IDAL.ISysManage)objType;
        }
        #endregion

        #region CreateCommonManage
        public static SCM.IDAL.ICommon CreateCommonManage()
        {
            string classNamespace = AssemblyPath + ".CommonManage";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (SCM.IDAL.ICommon)objType;
        }
        #endregion



        /// <summary>
        /// 创建Base_Permissions数据层接口。
        /// </summary>
        public static SCM.IDAL.IBasePermissions CreatePermissionsManage()
        {

            string ClassNamespace = AssemblyPath + ".PermissionsManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IBasePermissions)objType;
        }


        /// <summary>
        /// 创建Base_Permissions_Categories数据层接口。
        /// </summary>
        public static SCM.IDAL.IPermissionsCategories CreatePermissionsCategoriesManage()
        {

            string ClassNamespace = AssemblyPath + ".PermissionsCategoriesManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IPermissionsCategories)objType;
        }


        /// <summary>
        /// 创建Base_Roles数据层接口。
        /// </summary>
        public static SCM.IDAL.IRoles CreateRolesManage()
        {

            string ClassNamespace = AssemblyPath + ".RolesManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IRoles)objType;
        }


        /// <summary>
        /// 创建Base_User数据层接口。
        /// </summary>
        public static SCM.IDAL.IUser CreateUserManage()
        {

            string ClassNamespace = AssemblyPath + ".UserManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IUser)objType;
        }


        /// <summary>
        /// 创建Role_Permissions数据层接口。
        /// </summary>
        public static SCM.IDAL.IRolePermissions CreateRolePermissionsManage()
        {

            string ClassNamespace = AssemblyPath + ".RolePermissionsManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IRolePermissions)objType;
        }

        /// <summary>
        /// 创建Transfer_Order数据层接口。
        /// </summary>
        public static SCM.IDAL.ITransferOrder CreateTransferOrderManage()
        {

            string ClassNamespace = AssemblyPath + ".TransferOrderManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ITransferOrder)objType;
        }

        /// <summary>
        /// 创建BASE_WAREHOUSE数据层接口。
        /// </summary>
        public static SCM.IDAL.IWarehouse CreateWarehouseManage()
        {

            string ClassNamespace = AssemblyPath + ".WarehouseManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IWarehouse)objType;
        }

        /// <summary>
        /// 创建BASE_DEPARTMENT数据层接口。
        /// </summary>
        public static SCM.IDAL.IDepartment CreateDepartmentManage()
        {

            string ClassNamespace = AssemblyPath + ".DepartmentManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IDepartment)objType;
        }

        /// <summary>
        /// 创建BASE_PRODUCT数据层接口。
        /// </summary>
        public static SCM.IDAL.IProduct CreateProductManage()
        {

            string ClassNamespace = AssemblyPath + ".ProductManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IProduct)objType;
        }

        /// <summary>
        /// 创建BASE_UNIT数据层接口。
        /// </summary>
        public static SCM.IDAL.IUnit CreateUnitManage()
        {

            string ClassNamespace = AssemblyPath + ".UnitManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IUnit)objType;
        }
        /// <summary>
        /// 创建BASE_SUPPLIER数据层接口。
        /// </summary>
        public static SCM.IDAL.ISupplier CreateSupplierManage()
        {

            string ClassNamespace = AssemblyPath + ".SupplierManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ISupplier)objType;
        }

        /// <summary>
        /// 创建BASE_COLOR数据层接口。
        /// </summary>
        public static SCM.IDAL.IColor CreateColorManage()
        {

            string ClassNamespace = AssemblyPath + ".ColorManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IColor)objType;
        }

        /// <summary>
        /// 创建BASE_SIZE数据层接口。
        /// </summary>
        public static SCM.IDAL.ISize CreateSizeManage()
        {

            string ClassNamespace = AssemblyPath + ".SizeManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ISize)objType;
        }

        /// <summary>
        /// 创建BASE_STYLE数据层接口。
        /// </summary>
        public static SCM.IDAL.IStyle CreateStyleManage()
        {

            string ClassNamespace = AssemblyPath + ".StyleManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IStyle)objType;
        }

        /// <summary>
        /// 创建BASE_PRODUCT_GROUP数据层接口。
        /// </summary>
        public static SCM.IDAL.IProductGroup CreateProductGroupManage()
        {

            string ClassNamespace = AssemblyPath + ".ProductGroupManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IProductGroup)objType;
        }

        /// <summary>
        /// 创建BASE_PRODUCT_PRICE数据层接口。
        /// </summary>
        public static SCM.IDAL.IProductprice CreateProductpriceManage()
        {

            string ClassNamespace = AssemblyPath + ".ProductpriceManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IProductprice)objType;
        }

        /// <summary>
        /// 创建BASE_SALES_PROMOTION数据层接口。
        /// </summary>
        public static SCM.IDAL.ISalesPromotion CreateSalesPromotionManage()
        {

            string ClassNamespace = AssemblyPath + ".SalesPromotionManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ISalesPromotion)objType;
        }

        /// <summary>
        /// 创建bll_stock_view数据层接口。
        /// </summary>
        public static SCM.IDAL.IStock CreateStockManage()
        {
            string ClassNamespace = AssemblyPath + ".StockManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IStock)objType;
        }

        /// <summary>
        /// 创建BLL_PURCHASE_REQUISITION数据层接口。
        /// </summary>
        public static SCM.IDAL.IPurchaseRequisition CreatePurchaseRequisitionManage()
        {

            string ClassNamespace = AssemblyPath + ".PurchaseRequisitionManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IPurchaseRequisition)objType;
        }

        /// <summary>
        /// 创建BASE_VIP_CUSTOMER数据层接口。
        /// </summary>
        public static SCM.IDAL.IVipCustomer CreateVipCustomerManage()
        {

            string ClassNamespace = AssemblyPath + ".VipCustomerManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IVipCustomer)objType;
        }

        /// <summary>
        /// 创建BASE_BLL_RECEIPT数据层接口。
        /// </summary>
        public static SCM.IDAL.IReceipt CreateReceiptManage()
        {

            string ClassNamespace = AssemblyPath + ".ReceiptManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IReceipt)objType;
        }

        /// <summary>
        /// 创建BASE_PRODUCT_ITEM数据层接口。
        /// </summary>
        public static SCM.IDAL.IItem CreateItemManage()
        {

            string ClassNamespace = AssemblyPath + ".ItemManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IItem)objType;
        }

        /// <summary>
        /// 创建BLL_SALES_ORDER数据层接口。
        /// </summary>
        public static SCM.IDAL.ISalesOrder CreateSalesOrderManage()
        {

            string ClassNamespace = AssemblyPath + ".SalesOrderManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ISalesOrder)objType;
        }

        /// <summary>
        /// 创建BASE_NEWS数据层接口。
        /// </summary>
        public static SCM.IDAL.INews CreateNewsManage()
        {

            string ClassNamespace = AssemblyPath + ".NewsManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.INews)objType;
        }

        /// <summary>
        /// 创建BLL_TRANSFER_IN_PLAN数据层接口。
        /// </summary>
        public static SCM.IDAL.ITransferInPlan CreateTransferInPlanManage()
        {

            string ClassNamespace = AssemblyPath + ".TransferInPlanManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ITransferInPlan)objType;
        }

        /// <summary>
        /// 创建BLL_TRANSFER_RELATION数据层接口。
        /// </summary>
        public static SCM.IDAL.ITransferRelation CreateTransferRelationManage()
        {

            string ClassNamespace = AssemblyPath + ".TransferRelationManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ITransferRelation)objType;
        }

        /// <summary>
        /// 创建BLL_CASH数据层接口。
        /// </summary>
        public static SCM.IDAL.ICash CreateCashManage()
        {

            string ClassNamespace = AssemblyPath + ".CashManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ICash)objType;
        }

        /// <summary>
        /// 创建STA_DEP_GRP_SALES数据层接口
        /// </summary>
        public static SCM.IDAL.IStaDepGrpSales CreateStaDepGrpSalesManage()
        {

            string ClassNamespace = AssemblyPath + ".StaDepGrpSalesManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IStaDepGrpSales)objType;
        }

        /// <summary>
        /// 创建STA_DEP_GRP_SIZE_SALES数据接口层
        /// </summary>
        public static SCM.IDAL.IStaDepGrpSizeSales CreateStaDepGrpSizeSalesManage()
        {

            string ClassNamespace = AssemblyPath + ".StaDepGrpSizeSalesManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IStaDepGrpSizeSales)objType;
        }       


        public static SCM.IDAL.IShipment CreateShipmentManage()
        {
            string ClassNamespace = AssemblyPath + ".ShipmentManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IShipment)objType;
        }

        public static SCM.IDAL.IShipmentPlan CreateShipmentPlanManage()
        {
            string ClassNamespace = AssemblyPath + ".ShipmentPlanManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IShipmentPlan)objType;
        }

        public static SCM.IDAL.IPurchase CreatePurchaseManage()
        {
            string ClassNamespace = AssemblyPath + ".PurchaseManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IPurchase)objType;
        }

        public static SCM.IDAL.IReceivingPlan CreateReceivingPlanManage()
        {
            string ClassNamespace = AssemblyPath + ".ReceivingPlanManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IReceivingPlan)objType;
        }

        public static SCM.IDAL.IProductItem CreateProductItemManage()
        {
            string ClassNamespace = AssemblyPath + ".ProductItemManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.IProductItem)objType;
        }

        public static SCM.IDAL.ITransferIn CreateTransferInManage()
        {
            string ClassNamespace = AssemblyPath + ".TransferInManage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (SCM.IDAL.ITransferIn)objType;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Common
{
    public class CConstant
    {
        //初始，未处理
        public static int INIT = 0;

        //己经处理完成
        public static int NORMAL = 1;

        //删除
        public static int DELETE = 9;

        //预定出库类型
        public static int SHIPMENT_TYPE_PLAN = 1;

        //临时出库类型
        public static int SHIPMENT_TYPE_TEMP = 2;

        //出库仓库转移类型
        public static int SHIPMENT_TYPE_SHFIT = 3;

        //成品
        public static int INPUT_TYPE_HEADER = 1;

        //原料
        public static int INPUT_TYPE_ITEM = 2;

        //预定入库类型
        public static int RECEIPT_TYPE_PLAN = 1;

        //临时入库类型
        public static int RECEIPT_TYPE_TEMP = 2;

        //预定入库类型
        public static int TRANSFER_IN_TYPE_PLAN = 1;

        //临时入库类型
        public static int TRANSFER_IN_TYPE_TEMP = 2;

        //入库仓库转移类型
        public static int TRANSFER_IN_TYPE_SHFIT=3;

        //中心仓库
        public static int WAREHOUSE_TYPE_CENTER = 0;

        //一般仓库
        public static int WAREHOUSE_TYPE_NORMAL = 1;

        //导入导出　成功
        public static string SUCCESS = "00";

        //导入导出　没有数据
        public static string NO_DATA = "01";

        //导入导出　失败
        public static string ERROR = "02";

        //门店入库

        public static int TRANSFERIN_ENTER = 0;

        //门店退货

        public static int TRANSFERIN_OUT = 1;

        #region 用户权限
        /// <summary>
        /// 门店
        /// </summary>
        public static string USER_TYPE_A = "A"; 
        /// <summary>
        /// 仓库
        /// </summary>
        public static string USER_TYPE_B = "B";
        /// <summary>
        /// 供应部
        /// </summary>
        public static string USER_TYPE_C = "C"; 
        /// <summary>
        /// 成品供应商
        /// </summary>
        public static string USER_TYPE_D = "D"; 
        /// <summary>
        /// 原料供应商
        /// </summary>
        public static string USER_TYPE_E = "E"; 
        /// <summary>
        /// 系统管理员
        /// </summary>
        public static string USER_TYPE_F = "F";
        #endregion

        //web service 验证字符串
        public static string WEBSERVICE_SCM2POS_KEY = "CZZD.SCM2POS";

        public static string WEBSERVICE_POS2SCM_KEY = "CZZD.POS2SCM";


    }
}

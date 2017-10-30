using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Common
{
    public class Constant
    {
        //初始，未处理
        public static int INIT = 0;

        //己经处理完成
        public static int NORMAL = 1;

        //删除
        public static int DELETE = 9;

        //导入导出　成功
        public static string SUCCESS = "00";

        //导入导出　没有数据
        public static string NO_DATA = "01";

        //导入导出　失败
        public static string ERROR = "02";

        //钱箱存款流水号导入成功之后返回的状态
        public static int TO_CASH_FLAG = 2;

        //虚拟的商品编号
        public static string PRODUCT_CODE = "11110000";

        //预定
        public static string SALES_MEMO = "预定";

        //预定销售结算(这里是预定销售结算之后明细表的MEMO)
        public static string SALES_PAY_MEMO = "预定销售";

        ////换货的明细
        //public static string RETURN_MEMO = "销售换货";

        //退货明细(销售结算)
        public static string RETURN_MEMO = "已退货";

        //上传成功返回的状态（Send_flag）
        public static int UP_SUCCESS_SEND_FLAG = 1;

        //上传失败返回的状态（Send_flag）
        public static int UP_LOSE_SUCCESS_FLAG = 0;

        //销售结算STATUS_FLAG
        public static int SALES_PAY_STATUS_FLAG = 0;

        //销售预定STATUS_FLAG
        public static int SALES_ORDER_PLAN_STATUS_FLAG = 1;

        //销售退单STATUS_FLAG
        public static int SALES_ORDER_BACK_PLAN_STATUS_FLAG = 2;

        //销售退货（换货）的STATUS_FLAG
        public static int SALES_ORDER_RETURN_STATUS_FLAG = 3;

        //销售预定退单
        public static int SALES_ORDER_BACK_STATUS_FLAG = 4;

        //钱箱存款的初始STATUS_FLAG
        public static int INT_CASH = 1;

        //钱箱取款的初始STATUS_FLAG
        public static int OUT_CASH = 2;

        //钱箱存取银行的初始状态STATUS_FLAG
        public static int BANK_CASH = 3;

        //销售预定的状态（未处理）
        public static int PLAN_FLAG_NULL = 1;

        //销售预定的状态（已处理）
        public static int PLAN_FLAG = 0;

        //销售预定的状态(已退订)
        public static int PLAN_FLAG_RETURN = 2;

        //清空
        public static string SCREEN_TYPE_CLEAR = "0";

        //单价
        public static string SCREEN_TYPE_PRICE = "1";

        //总计
        public static string SCREEN_TYPE_TOTAL_AMOUNT = "2";

        //付款
        public static string SCREEN_TYPE_PAY_AMOUNT = "3";

        //找零
        public static string SCREEN_TYPE_CHANGE = "4";

        //web service 验证字符串
        public static string WEBSERVICE_SCM2POS_KEY = "CZZD.SCM2POS";

        public static string WEBSERVICE_POS2SCM_KEY = "CZZD.POS2SCM";


    }
}

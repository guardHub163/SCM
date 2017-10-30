using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
///Parameter 的摘要说明
/// </summary>
namespace SCM.Common
{
    public class Parameter
    {
        //一定时间内的业绩指标
        public static decimal PERFORMANCE = 10000;

        //一定时间内的达标率
        public static decimal STANDARDRATE = 100;

        //同比
        public static decimal COMPARED = 10;

        //分类商品
        public static string PRODUCT_CODE = "02002359929";
        
        //ASP设定的值
        public static decimal ASP = 200;

        //ATV设定的值
        public static decimal ATV = 200;

        //分类配比的值
        public static decimal PB = 500;

        //同期增长率
        public static decimal PERIOD = 500;

        //人效
        public static decimal HUMANEFFECT = 500;

        //坪效
        public static decimal PING = 500;

        //主商品的配比
        public static decimal RATIO = 500;
    }
}

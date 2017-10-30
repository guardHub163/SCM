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

using System.Reflection;

namespace SCM.Web
{
    /// <summary>
    ///DownLoad 的摘要说明
    /// </summary>
    public class CConvert
    {        
        public CConvert()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }        

        //保留小数位置后面的两位
        public static string FormateRate(string rateStr)
        {
            if (rateStr.IndexOf(".") != -1)
            {
                //获取小数点的位置  
                int num = 0;
                num = rateStr.IndexOf(".");

                //获取小数点后面的数字 是否有两位 不足两位补足两位  
                String dianAfter = rateStr.Substring(0, num + 1);
                String afterData = rateStr.Replace(dianAfter, "");
                if (afterData.Length < 2)
                {
                    afterData = afterData + "0";
                }
                else
                {
                    afterData = afterData;
                }
                return rateStr.Substring(0, num) + "." + afterData.Substring(0, 2);
            }
            else
            {
                if (rateStr == "1")
                {
                    return "100";
                }
                else
                {
                    return rateStr;
                }
            }

        }


    }//end class
}

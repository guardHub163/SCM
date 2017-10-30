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

        //中心仓库
        public static int WAREHOUSE_TYPE_CENTER = 0;

        //一般仓库
        public static int WAREHOUSE_TYPE_NORMAL = 1;

    }
}

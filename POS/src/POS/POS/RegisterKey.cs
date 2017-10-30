using System;
using System.Collections.Generic;
using System.Text;
//要使用DllImport语句必须引用该命名空间
using System.Runtime.InteropServices;
//要使用Process语句必须引用该命名空间
using System.Diagnostics;
using System.Windows.Forms;

namespace POS
{
    //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）

        [Flags()]

        public enum KeyModifiers
        {

            None = 0,

            Alt = 1,

            Ctrl = 2,

            Shift = 4,

            WindowsKey = 8,

            CtrlAndShift = 6

        }

   public static class RegisterKey
    {
        //user32.dll是非托管代码，不能用命名空间的方式直接引用，所以需要用“DllImport”进行引入后才能使用

        [DllImport("user32.dll", SetLastError = true)]

        public static extern bool RegisterHotKey(

        IntPtr hWnd, //要定义热键的窗口的句柄

        int id, //定义热键ID（不能与其它ID重复）

        KeyModifiers fsModifiers, //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效

        Keys vk //定义热键的内容

        );


        [DllImport("user32.dll", SetLastError = true)]

        public static extern bool UnregisterHotKey(

        IntPtr hWnd, //要取消热键的窗口的句柄

        int id //要取消热键的ID

        );

        
    }
}

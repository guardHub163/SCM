using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Management;

namespace POS.Common
{
    public class POSPrinter
    {
        const int OPEN_EXISTING = 3;

        //打印机接口
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        int dwCreationDisposition,
        int dwFlagsAndAttributes,
        int hTemplateFile);
        //顾显接口
        [DllImport("api_com.dll", CharSet = CharSet.Auto)]
        private static extern void com_init(Int32 com, int baud);

        //// <summary>
        //// 小票打印/打开钱箱
        //// </summary>
        //// <param name="prnPort"></param>
        //// <returns></returns>
        ////public static string PrintLine(string prnPort,byte[] bytes)
        ////{
        ////    IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0);
        ////    if (iHandle.ToInt32() == -1)
        ////    {
        ////        return "没有连接打印机或者打印机端口不是" + prnPort;
        ////    }
        ////    else
        ////    {
        ////        FileStream fs = new FileStream(iHandle, FileAccess.ReadWrite);
        ////        BinaryWriter bw = new BinaryWriter(fs);
                
        ////        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
        ////        打开钱箱
        ////        bw.Write(((char)27).ToString() + "p" + ((char)0).ToString() + ((char)60).ToString() + ((char)255).ToString());

        ////        小票打印
        ////        if (bytes.Length > 0)
        ////        {
        ////            sw.WriteLine(bytes);
        ////            bw.Write(bytes);
        ////        }
        ////        sw.Close();
        ////        bw.Close();
        ////        fs.Close();

        ////        FileStream fs2 = new FileStream("d:/2.jpg", FileMode.Create);
        ////        BinaryWriter bw = new BinaryWriter(fs2);
        ////        bw.Write(bytes);
        ////        bw.Close();
        ////        fs2.Close();
        ////        return "OK";
        ////    }
        ////}

        /// <summary>
        /// 打开钱箱
        /// </summary>
        public static string OpenCash(string prnPort)
        {
            if (Printer.GetPrinter() == null || Printer.GetPrinter().Length == 0)
            {
                return "没有找到打印机";
            }

            IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0);
            if (iHandle.ToInt32() == -1)
            {
                return "钱箱没有连接打印机或者打印机端口不是" + prnPort;
            }
            else
            {
                FileStream fs = new FileStream(iHandle, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                //打开钱箱
                sw.Write(((char)27).ToString() + "p" + ((char)0).ToString() + ((char)60).ToString() + ((char)255).ToString());
                sw.Close();
                fs.Close();
                return "OK";
            }
        }

        /// <summary>
        /// 顾客屏幕显示
        /// </summary>
        /// <param name="prnPort">显示端口</param>
        /// <param name="type">显示类型</param>
        /// <param name="amount">显示金额</param>
        /// <returns></returns>
        public static string ShowCustomerScreen(string screenPort, string type, string amount)
        {
            try
            {
                SerialPort sp = new SerialPort(screenPort, 2400, Parity.None, 8, StopBits.One);
                sp.Open();
                sp.Write(((char)27).ToString() + ((char)115).ToString() + type);
                sp.Write(((char)27).ToString() + ((char)81).ToString() + ((char)65).ToString() + amount + ((char)13).ToString());
                sp.Close();
            }
            catch (Exception e) 
            {
                return e.Message; 
            }
            return "OK";
        }

        /// <summary>
        /// 打钱机/打开钱箱测试
        /// </summary>
        public static string PrintTest(string prnPort)
        {

            if (Printer.GetPrinter() == null || Printer.GetPrinter().Length == 0)
            {
                return "没有找到打印机";
            }

            IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0);
            if (iHandle.ToInt32() == -1)
            {
                return "钱箱没有连接打印机或者打印机端口不是" + prnPort;
            }
            else
            {
                FileStream fs = new FileStream(iHandle, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                //打开钱箱
                sw.Write(((char)27).ToString() + "p" + ((char)0).ToString() + ((char)60).ToString() + ((char)255).ToString());
                sw.WriteLine("------------------------");
                sw.WriteLine("打印机连接成功!");
                sw.WriteLine("------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.Flush();
                sw.Close();
                fs.Close();
                return "OK";
            }             
        }
        /// <summary>
        /// 顾客屏幕显示测试
        /// </summary>
        public static string ScreenTest(string screenPort, string type, string amount)
        {
            try
            {
                SerialPort sp = new SerialPort(screenPort, 2400, Parity.None, 8, StopBits.One);
                sp.Open();
                sp.Write(((char)27).ToString() + ((char)115).ToString() + type);
                sp.Write(((char)27).ToString() + ((char)81).ToString() + ((char)65).ToString() + amount + ((char)13).ToString());
                sp.Close();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "OK";
        }
    }//end class
}

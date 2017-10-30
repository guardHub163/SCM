using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;

namespace POS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists(Application.StartupPath + "\\Update")) 
            {
                if (File.Exists(Application.StartupPath + "\\Update\\UpdateServers.exe")) 
                {
                    if (File.Exists(Application.StartupPath + "\\UpdateServers.exe"))
                    {
                        File.Delete(Application.StartupPath + "\\UpdateServers.exe");
                        File.Copy(Application.StartupPath + "\\Update\\UpdateServers.exe", Application.StartupPath + "\\UpdateServers.exe", true);
                    }
                    else 
                    {
                        File.Copy(Application.StartupPath + "\\Update\\UpdateServers.exe", Application.StartupPath + "\\UpdateServers.exe", true);
                    }
                }
            }
                Application.Run(new FrmLogin());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
namespace UpdateServers
{
    public class AutoUpdater
    {
        const string FILENAME = "update.config";
        private string serverpath = "http://112.82.245.2:8080/scm-low/update/service.xml";
        private WebClient clientDownload = null;
        DataSet ServerDs = new DataSet();
        DataSet LocalDs = new DataSet();
        public AutoUpdater()
        {

        }

        public void ServerXml()
        {
            if (System.IO.Directory.Exists(Application.StartupPath + "\\DownFile"))
            {
                System.IO.Directory.Delete(Application.StartupPath + "\\DownFile",true);
            }
            if (System.IO.Directory.Exists(Application.StartupPath + "\\Update"))
            {
                System.IO.Directory.Delete(Application.StartupPath + "\\Update",true);
            }
            try
            {
                Uri uri = new Uri(serverpath);
                clientDownload = new WebClient();
                clientDownload.DownloadFile(uri, Application.StartupPath + "\\service.xml");
                clientDownload.CancelAsync();
                clientDownload.Dispose();
                if (File.Exists(Application.StartupPath + "\\service.xml"))//将xml文件携程dataset
                {
                    ServerDs.ReadXml(Application.StartupPath + "\\service.xml");//服务器的xml文件
                    ServerDs.Tables["File"].Columns.Add("STATUS_FLAG", Type.GetType("System.Int32"));
                }
                if (File.Exists(Application.StartupPath + "\\UpdateList.xml"))
                {
                    LocalDs.ReadXml(Application.StartupPath + "\\UpdateList.xml");//本地的xml文件
                    File.Delete(Application.StartupPath + "\\UpdateList.xml");
                }
                for (int i = 0; i < ServerDs.Tables["File"].Rows.Count; i++)//判断文件版本是否相同
                {
                    for (int j = 0; j < LocalDs.Tables["File"].Rows.Count; j++)
                    {
                        if (ServerDs.Tables["File"].Rows[i]["filename"].ToString() == LocalDs.Tables["File"].Rows[j]["filename"].ToString())
                        {
                            if (ServerDs.Tables["File"].Rows[i]["version"].ToString() == LocalDs.Tables["File"].Rows[j]["version"].ToString())
                            {
                                ServerDs.Tables["File"].Rows[i]["STATUS_FLAG"] = 7;
                                break;
                            }
                            else
                            {
                                LocalDs.Tables["File"].Rows[j]["version"] = ServerDs.Tables["File"].Rows[i]["version"].ToString();
                                break;
                            }
                        }
                    }
                }
                ServerDs.WriteXml(Application.StartupPath + "\\service.xml");//将过滤好的文件放到xml中去
                LocalDs.WriteXml(Application.StartupPath + "\\UpdateList.xml");
            }
            catch { return; }
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace UpdateServers
{
    public partial class FrmUpdate : Form
    {
        public FrmUpdate()
        {
            InitializeComponent();
        }
        private string serverpath = "http://112.82.245.2:8080/scm-low/update/";
        private WebClient clientDownload = null;
        DataSet ServerDs = new DataSet();
        List<DownloadFileInfo> downloadFileList = new List<DownloadFileInfo>();
        private void Form1_Load(object sender, EventArgs e)
        {
            AutoUpdater update = new AutoUpdater();
            this.progressBar1.Visible = false;
            update.ServerXml();
            int Flag = 0;//是否有需要更新的文件数量
            try
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\DownFile");
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Update");
                if (File.Exists(Application.StartupPath + "\\service.xml"))
                {
                    ServerDs.ReadXml(Application.StartupPath + "\\service.xml");//将过滤好的xml文件转成datatable
                }
                else 
                {
                    this.Close();
                    Process.Start(Application.StartupPath + "\\POS.exe", "");
                }
                if (ServerDs.Tables["File"].Rows.Count > 0)
                {
                    foreach (DataRow rows in ServerDs.Tables["File"].Rows)
                    {
                        if (rows["STATUS_FLAG"].ToString() != "9")
                        {
                            Flag++;
                            downloadFileList.Add(new DownloadFileInfo(rows["filename"].ToString(), rows["version"].ToString(), Convert.ToInt32(rows["size"])));
                            if (File.Exists(Application.StartupPath + "\\" + rows["filename"].ToString()))//本地存在这个文件就Copy
                            {
                                File.Copy(Application.StartupPath + "\\" + rows["filename"].ToString(), Application.StartupPath + "\\DownFile\\" + rows["filename"].ToString(), false);
                            }
                        }
                    }
                    foreach (DownloadFileInfo file in this.downloadFileList)//将需要更新的文件放入list中并绑定到listview
                    {
                        ListViewItem item = new ListViewItem(new string[] { file.FileName, file.Version, file.Size.ToString() });
                        this.listDownloadFile.Items.Add(item);
                    }

                }
               if (Flag == 0)
                {
                    this.Close();
                    if (File.Exists(Application.StartupPath + "\\service.xml"))
                    {
                        File.Delete(Application.StartupPath + "\\service.xml");
                    }
                    Process.Start(Application.StartupPath + "\\POS.exe", "");
                }
            }
            catch
            {
                
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ServerDs.Tables["File"].Rows.Count > 0)
            {
                double SizeLength = 0;
                double SizeNowLength = 0;
                this.progressBar1.Visible = true;
                for (int i = 0; i < ServerDs.Tables["File"].Rows.Count; i++)
                {
                    if (ServerDs.Tables["File"].Rows[i]["STATUS_FLAG"].ToString() != "9")
                    {
                        SizeLength += Convert.ToDouble(ServerDs.Tables["File"].Rows[i]["size"].ToString());
                    }
                }
                clientDownload = new WebClient();
                try
                {
                    foreach (DataRow rows in ServerDs.Tables["File"].Rows)
                    {
                        if (rows["filename"].ToString() != "UpdateServers.exe")
                        {
                            if (rows["STATUS_FLAG"].ToString() != "9")
                            {
                                SizeNowLength += Convert.ToDouble(rows["size"].ToString());
                                Uri uri = new Uri(serverpath + rows["filename"]);
                                if (File.Exists(Application.StartupPath + "\\" + rows["filename"].ToString()))//本地存在这个文件就删除，防止新增加文件之后取不到文件名
                                {
                                    File.Delete(Application.StartupPath + "\\" + rows["filename"].ToString());
                                }
                                clientDownload.DownloadFile(uri, Application.StartupPath + "\\" + rows["filename"].ToString());
                                progressBar1.Value = Convert.ToInt32(SizeNowLength * 100 / SizeLength);
                                clientDownload.CancelAsync();
                                clientDownload.Dispose();
                                Application.DoEvents();
                                Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            Uri uri = new Uri(serverpath + rows["filename"]);
                            File.Delete(Application.StartupPath + "\\" + rows["filename"]);
                            clientDownload.DownloadFileAsync(uri, Application.StartupPath + "\\Update\\" + rows["filename"].ToString());
                        }

                    }
                    this.Visible=false;
                    MessageBox.Show("更新成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (File.Exists(Application.StartupPath + "\\service.xml"))
                    {
                        File.Delete(Application.StartupPath + "\\service.xml");
                    }
                    Process.Start(Application.StartupPath + "\\POS.exe", "");
                    System.Environment.Exit(0);

                }
                catch { }
            }
        }


        public class DownloadFileInfo
        {
            string fileName = "";
            string version = "";
            int size = 0;

            public string FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }

            public string Version
            {
                get { return version; }
                set { version = value; }
            }


            public int Size
            {
                get { return size; }
                set { size = value; }
            }

            public DownloadFileInfo(string name, string ver, int size)
            {
                this.FileName = name;
                this.Version = ver;
                this.Size = size;
            }
        }

    }
}

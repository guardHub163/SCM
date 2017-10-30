using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Reflection;
using POS.Model;
using POS.Bll;
using POS.SQLServerDAL;
using UserCache;
using POS.Common;
using System.Threading;
using System.Collections;

namespace POS
{
    public partial class MainForm : Form
    {
        private BaseUserTable _tuser;

        BCommon bcomm = new BCommon();

        string currentFormName = "";

        Form salesForm = null;

        Hashtable frmHt = new Hashtable();
        #region init
        public BaseUserTable TUser
        {
            get { return _tuser; }
            set { _tuser = value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(BaseUserTable _usertable)
        {
            _tuser = _usertable;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.MinimumSize = this.MaximumSize;
            this.WindowState = FormWindowState.Maximized;

            BindFunclist(lblMenu1);
            this.lblUserName.Text = _tuser.USER_ID + " | " + _tuser.TRUE_NAME;

            //基本资料的获得
            Thread thread = new Thread(new ThreadStart(GetDataInfo));
            thread.IsBackground = true;
            thread.Start();
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            Label menu = (Label)sender;
            if (menu.Tag != lblMenu.Tag)
            {
                MainForm_Deactivate(sender, e);
                BindFunclist(menu);
                MainForm_Activated(sender, e);
            }
        }

        private void BindFunclist(Label menu)
        {
            int x = 30;
            int y = 5;
            lblMenu.Text = menu.Text;
            lblMenu.Tag = menu.Tag;
            navigateBar.Controls.Clear();
            switch (menu.Name)
            {
                case "lblMenu1":
                    newFrmPictureBox(navigateBar, x, y, "SALES", "Alt+2 销售管理", POS.Properties.Resources.销售管理);
                    newFrmPictureBox(navigateBar, x, y = y + 65, "SALES_PLAN", "Alt+3 销售定货", POS.Properties.Resources.销售预定);
                    newFrmPictureBox(navigateBar, x, y = y + 65, "CASH", "Alt+4 钱箱费用", POS.Properties.Resources.cash);
                    newFrmPictureBox(navigateBar, x, y = y + 65, "CUSTOMER", "Alt+5 客户管理", POS.Properties.Resources.客户管理);
                    newFrmPictureBox(navigateBar, x, y = y + 65, "PRODUCT", "Alt+6 商品浏览", POS.Properties.Resources.商品浏览);
                    break;
                case "lblMenu2":
                    newFrmPictureBox(navigateBar, x, y, "SALES_STAT", "Alt+2 销售统计", POS.Properties.Resources.Sales_Statistics);
                    break;
                case "lblMenu3":
                    newFrmPictureBox(navigateBar, x, y, "DOWN_LOAD", "Alt+2 数据下载", POS.Properties.Resources.download);
                    newFrmPictureBox(navigateBar, x, y = y + 65, "UP_LOAD", "Alt+3 数据上传", POS.Properties.Resources.upload);
                    newFrmPictureBox(navigateBar, x, y = y + 65, "PRINT_SET", "Alt+4 打印设定", POS.Properties.Resources.print);
                    break;
                case "lblMenu4":

                    break;
            }
        }

        private void newFrmPictureBox(Panel panel, int x, int y, string name, string text, Image image)
        {
            PictureBox pic = new System.Windows.Forms.PictureBox();
            pic.Image = image;
            pic.Location = new System.Drawing.Point(x, y);
            pic.Padding = new Padding(5);
            pic.Name = "FRM_" + name;
            pic.Size = new System.Drawing.Size(50, 50);
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 1;
            pic.TabStop = false;
            pic.Cursor = Cursors.Hand;
            pic.Click += new System.EventHandler(this.FRM_Click);
            pic.MouseLeave += new System.EventHandler(this.FRM_MouseLeave);
            pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FRM_MouseMove);
            panel.Controls.Add(pic);

            Label lbl = new System.Windows.Forms.Label();
            lbl.Location = new System.Drawing.Point(x - 15, y + 50);
            lbl.Name = "LBL" + name;
            lbl.Size = new System.Drawing.Size(90, 15);
            lbl.TabIndex = 0;
            lbl.Text = text;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            panel.Controls.Add(lbl);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void FRM_Click(object sender, EventArgs e)
        {
            PictureBox frm = (PictureBox)sender;
            if (frm.Name.Equals("FRM_SALES"))
            {
                OpenForm(new FrmSales(_tuser));
            }
            else if (frm.Name.Equals("FRM_PRODUCT"))
            {
                OpenForm(new FrmProduct());
            }
            else if (frm.Name.Equals("FRM_CASH"))
            {
                OpenForm(new FrmCash(_tuser.USER_ID));
            }
            else if (frm.Name.Equals("FRM_SALES_PLAN"))
            {
                OpenForm(new FrmSalesPlan(_tuser));
            }
            else if (frm.Name.Equals("FRM_DOWN_LOAD"))
            {
                OpenForm(new FrmDownload());
            }
            else if (frm.Name.Equals("FRM_UP_LOAD"))
            {
                OpenForm(new FrmUpload());
            }
            else if (frm.Name.Equals("FRM_CUSTOMER"))
            {
                OpenForm(new FrmVipSearch(_tuser));
            }
            else if (frm.Name.Equals("FRM_SALES_STAT"))
            {
                OpenForm(new FrmSalesStat(_tuser));
            }
            else if (frm.Name.Equals("FRM_PRINT_SET"))
            {
                OpenForm(new FrmPrinterSet(_tuser));
            }
        }

        /// <summary>
        /// 在PANEL中打开窗口
        /// </summary>
        private void OpenForm(Form frm)
        {
            if (currentFormName != "" && frm.Name.Equals(currentFormName))
            {
                try
                {
                    ((Form)panelFrm.Controls.Find(currentFormName, false)[0]).Activate();
                    return;
                }
                catch { }
            }

            try
            {
                if ("FrmSales".Equals(currentFormName))
                {
                    salesForm.Visible = false;

                }
                else
                {
                    ((Form)panelFrm.Controls.Find(currentFormName, false)[0]).Dispose();
                }
            }
            catch { }

            if ("FrmSales".Equals(frm.Name))
            {
                if (salesForm != null)
                {
                    frm = salesForm;
                    frm.Visible = true;
                    currentFormName = frm.Name;
                    return;
                }
                else
                {
                    salesForm = frm;
                }

            }
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Width = panelFrm.Width;
            frm.Height = panelFrm.Height;
            panelFrm.Controls.Add(frm);//subtabcontrol是另一个子窗体
            currentFormName = frm.Name;
            frm.Show();

        }

        /// <summary>
        /// 
        /// </summary>
        private void FRM_MouseLeave(object sender, EventArgs e)
        {
            PictureBox frm = (PictureBox)sender;
            frm.BackgroundImage = null;
        }
        /// <summary>
        /// 
        /// </summary>
        private void FRM_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox frm = (PictureBox)sender;
            frm.BackgroundImage = POS.Properties.Resources.边框;
            frm.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void TimeShowsystemtime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.GetDateTimeFormats('D')[3].ToString() + string.Format("{0:T}", DateTime.Now);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "你确定要关闭吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Exit();
            }

        }

        private void Exit()
        {
            //注销Id号为100的热键设定
            RegisterKey.UnregisterHotKey(Handle, 102);
            RegisterKey.UnregisterHotKey(Handle, 103);
            RegisterKey.UnregisterHotKey(Handle, 104);
            RegisterKey.UnregisterHotKey(Handle, 105);
            RegisterKey.UnregisterHotKey(Handle, 106);

            Application.Exit();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            //热键注册
            switch (lblMenu.Tag.ToString())
            {
                case "1":
                    RegisterKey.RegisterHotKey(Handle, 102, KeyModifiers.Alt, Keys.D2);
                    RegisterKey.RegisterHotKey(Handle, 103, KeyModifiers.Alt, Keys.D3);
                    RegisterKey.RegisterHotKey(Handle, 104, KeyModifiers.Alt, Keys.D4);
                    RegisterKey.RegisterHotKey(Handle, 105, KeyModifiers.Alt, Keys.D5);
                    RegisterKey.RegisterHotKey(Handle, 106, KeyModifiers.Alt, Keys.D6);
                    break;
                case "2":
                    RegisterKey.RegisterHotKey(Handle, 102, KeyModifiers.Alt, Keys.D2);
                    break;
                case "3":
                    RegisterKey.RegisterHotKey(Handle, 102, KeyModifiers.Alt, Keys.D2);
                    RegisterKey.RegisterHotKey(Handle, 103, KeyModifiers.Alt, Keys.D3);
                    RegisterKey.RegisterHotKey(Handle, 104, KeyModifiers.Alt, Keys.D4);
                    break;
                case "4":
                    break;

            }

        }
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            //热键注销
            switch (lblMenu.Tag.ToString())
            {
                case "1":
                    RegisterKey.UnregisterHotKey(Handle, 102);
                    RegisterKey.UnregisterHotKey(Handle, 103);
                    RegisterKey.UnregisterHotKey(Handle, 104);
                    RegisterKey.UnregisterHotKey(Handle, 105);
                    RegisterKey.UnregisterHotKey(Handle, 106);
                    break;
                case "2":
                    RegisterKey.UnregisterHotKey(Handle, 102);
                    break;
                case "3":
                    RegisterKey.UnregisterHotKey(Handle, 102);
                    RegisterKey.UnregisterHotKey(Handle, 103);
                    RegisterKey.UnregisterHotKey(Handle, 104);
                    break;
                case "4":
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {

            const int WM_HOTKEY = 0x0312;


            //按快捷键
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 102: //按下的是Alt+2
                            switch (lblMenu.Tag.ToString())
                            {
                                case "1":
                                    OpenForm(new FrmSales(_tuser));
                                    break;
                                case "2":
                                    OpenForm(new FrmSalesStat());
                                    break;
                                case "3":
                                    OpenForm(new FrmDownload());
                                    break;
                                case "4":
                                    break;
                            }
                            break;
                        case 103: //按下的是Alt+3
                            switch (lblMenu.Tag.ToString())
                            {
                                case "1":
                                    OpenForm(new FrmSalesPlan());
                                    break;
                                case "2":
                                    break;
                                case "3":
                                    OpenForm(new FrmUpload());
                                    break;
                                case "4":
                                    break;
                            }
                            break;
                        case 104: //按下的是Alt+4
                            switch (lblMenu.Tag.ToString())
                            {
                                case "1":
                                    OpenForm(new FrmCash(_tuser.USER_ID));
                                    break;
                                case "2":
                                    break;
                                case "3":
                                    OpenForm(new FrmPrinterSet());
                                    break;
                                case "4":
                                    break;
                            }
                            break;
                        case 105://按下的是Alt+5
                            switch (lblMenu.Tag.ToString())
                            {
                                case "1":
                                    OpenForm(new FrmVipSearch());
                                    break;
                                case "2":
                                    break;
                                case "3":
                                    break;
                                case "4":
                                    break;
                            }
                            break;
                        case 106://按下的是Alt+6 
                            switch (lblMenu.Tag.ToString())
                            {
                                case "1":
                                    OpenForm(new FrmProduct());
                                    break;
                                case "2":
                                    break;
                                case "3":
                                    break;
                                case "4":
                                    break;
                            }
                            break;
                        case 107://按下的是Alt+7

                            break;
                        case 108:

                            break;
                        case 109:

                            break;
                    }

                    break;

            }
            base.WndProc(ref m);
        }

        #region 基本资料的获得
        /// <summary>
        /// 基本资料的获得
        /// </summary>
        /// <returns></returns>
        private void GetDataInfo()
        {
            try
            {
                string[] tableNames = { "PRODUCT_GROUP", "STYLE", "COLOR", "SIZE", "UNIT", "PRODUCT", "PRODUCT_PRICE", "VIP_CUSTOMER", "USER", "SALES_PROMOTION","NAMES" };
                new WebServieceOperate().GetDataInfo(tableNames);
            }
            catch { }
        }
        #endregion
    }//END CLASS
}

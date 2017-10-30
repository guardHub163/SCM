using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Model;
using UserCache;
using System.Collections;
using POS.Common;
using FastReport;
using System.IO;
using POS.Bll;

namespace POS
{
    public partial class FrmPrinterSet : Form
    {
        private BaseUserTable _tuser = null;
        private BSalesOrder bSalesOrder = new BSalesOrder();
        BCommon bCommon = new BCommon();
        FastReport.Preview.PreviewControl previewControl1 = new FastReport.Preview.PreviewControl();
        FastReport.Report report = new FastReport.Report();
        public FrmPrinterSet()
        {
            InitializeComponent();
        }

        public FrmPrinterSet(BaseUserTable tuser)
        {
            InitializeComponent();
            _tuser = tuser;
        }

        private void FrmPrinterSet_Load(object sender, EventArgs e)
        {
            Hashtable ht = Cache.PRINT_HT;
            cboScreenPort.Text = Convert.ToString(ht["SCREEN_PORT"]);
            cboPrintPort.Text = Convert.ToString(ht["PRINT_PORT"]);
            txtTitle.Text = Convert.ToString(ht["TITLE"]);
            txtAddress.Text = Convert.ToString(ht["ADDRESS"]);
            txtTel.Text = Convert.ToString(ht["TEL"]);
            txtWWW.Text = Convert.ToString(ht["WWW"]);
            txtMemo.Text = Convert.ToString(ht["MEMO"]);
            printNumber.Value = Convert.ToDecimal(ht["SHARE"]);
            
            panelPerView.Controls.Add(previewControl1);
            previewControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            previewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            previewControl1.Font = new System.Drawing.Font("Tahoma", 8F);
            previewControl1.Location = new System.Drawing.Point(3, 3);
            previewControl1.Name = "previewControl1";
            previewControl1.Size = new System.Drawing.Size(779, 542);
            previewControl1.TabIndex = 0;
            previewControl1.UIStyle = FastReport.Utils.UIStyle.VisualStudio2005;
            previewControl1.ToolbarVisible = false;

            report.Preview = previewControl1;

            btnView_Click(sender, EventArgs.Empty);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<NamesTable> names = new List<NamesTable>();
            names.Add(new NamesTable("PRINT_PARAM","PRINT_PORT",cboPrintPort.Text,Constant.INIT));
            names.Add(new NamesTable("PRINT_PARAM", "SCREEN_PORT", cboScreenPort.Text, Constant.INIT));
            names.Add(new NamesTable("PRINT_PARAM", "TITLE", txtTitle.Text, Constant.INIT));
            names.Add(new NamesTable("PRINT_PARAM", "TEL", txtTel.Text, Constant.INIT));
            names.Add(new NamesTable("PRINT_PARAM", "ADDRESS", txtAddress.Text, Constant.INIT));
            names.Add(new NamesTable("PRINT_PARAM", "WWW", txtWWW.Text, Constant.INIT));
            names.Add(new NamesTable("PRINT_PARAM", "MEMO", txtMemo.Text, Constant.INIT));
            names.Add(new NamesTable("PRINT_PARAM", "SHARE", printNumber.Value.ToString(), Constant.INIT));
            if (bCommon.UpdateNames(names) > 0)
            {
                MessageBox.Show("设定成功!", this.Text);
                Cache.PRINT_HT = new Hashtable();
                this.Close();
            }
            else 
            {
                MessageBox.Show("设定失败!", this.Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestPrint_Click(object sender, EventArgs e)
        {
            string ret = POSPrinter.PrintTest(cboPrintPort.Text);
            if ("OK".Equals(ret))
            {
                MessageBox.Show("打印机连接成功!",this.Text);
            }
            else 
            {
                MessageBox.Show(ret, this.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestScreen_Click(object sender, EventArgs e)
        {
            string ret = POSPrinter.ScreenTest(cboScreenPort.Text, "1", "100.50");
            if ("OK".Equals(ret))
            {
                MessageBox.Show("顾客显示屏幕连接成功!", this.Text);
            }
            else
            {
                MessageBox.Show(ret, this.Text);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = bSalesOrder.GetPrintList("SLIP_NUMBER = 'A'");
                DataRow row = ds.Tables[0].NewRow();
                row["SLIP_NUMBER"] = "A01_S000000002";
                row["PRODUCT_CODE"] = "P_000000000001";
                row["PRODUCT_NAME"] = "NAXXXXX-XXXXXX";
                row["QUANTITY"] = 1;
                row["ORI_PRICE"] = 300;
                row["DISCOUNT_RATE"] = 20;
                row["AMOUNT"] = 280;
                ds.Tables[0].Rows.Add(row);


                if (File.Exists(@"Reports\SalesPrint.frx"))
                {
                    report.Load(@"Reports\SalesPrint.frx");
                    ((ReportPage)report.FindObject("Page1")).PaperHeight = (float)(88 + 7.5 * 1);
                    report.SetParameterValue("Bank_Amount", 100);
                    report.SetParameterValue("Cash_Amount", 200);
                    report.SetParameterValue("Change", 20);
                    report.SetParameterValue("CreateUserName", _tuser.TRUE_NAME);
                    report.SetParameterValue("CreateDateTime", DateTime.Now);
                    report.SetParameterValue("Address", txtAddress.Text.Trim());
                    report.SetParameterValue("Tel", txtTel.Text.Trim());
                    report.SetParameterValue("WWW", txtWWW.Text.Trim());
                    report.SetParameterValue("Title", txtTitle.Text.Trim());
                    report.SetParameterValue("Footer", txtMemo.Text.Trim());
                    report.SetParameterValue("Total_Used_Points", 0);
                    report.SetParameterValue("TotalPoints", 0);
                    report.RegisterData(ds);
                    report.Prepare();
                    report.ShowPrepared();                    
                }
            }
            catch { }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

    }//end class
}

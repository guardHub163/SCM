using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Model;
using POS.Bll;
using UserCache;
using System.Collections;
using POS.Common;
using System.Threading;

namespace POS
{
    public partial class FrmSalesPlan : Form
    {
        BSalesOrderPlan SaleOrderPlan = new BSalesOrderPlan();
        BSalesOrder bSales = new BSalesOrder();
        private Hashtable gvHt = new Hashtable();
        public BaseUserTable _tuser;
        DataSet ds = new DataSet();
        private bool flag = true;
        private WebServieceOperate operate = new WebServieceOperate();

        public FrmSalesPlan()
        {
            InitializeComponent();
        }

        public FrmSalesPlan(BaseUserTable tUser)
        {
            InitializeComponent();
            _tuser = tUser;
        }
        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (BindData() == false)
            {
                MessageBox.Show("这个时间里没有预定的订单！", this.Text);
            }
        }

        private bool BindData()
        {
            ds = SaleOrderPlan.GetSalesOrderPlan(getConduction());
            if (ds.Tables[0].Rows.Count == 0)
            {
                gvSalesPlan.DataSource = ds.Tables[0];
                flag = false;
            }
            else
            {
                gvSalesPlan.DataSource = ds.Tables[0];
                flag = true;
            }
            return flag;
        }

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1");
            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME BETWEEN '{0}' AND '{1}'", txtFromDate.Value.ToString("yyyy/MM/dd"), txtToDate.Value.AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME  >= '{0}' ", txtFromDate.Value.ToString("yyyy/MM/dd"));
            }
            else if (txtToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME  < '{0}' ", txtToDate.Value.AddDays(1).ToString("yyyy/MM/dd"));
            }

            if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                sb.AppendFormat(" AND CUSTOMER_CODE  = '{0}' ", txtCustomerCode.Text.Trim());
            }

            if (this.radioButton2.Checked == true)
            {
                sb.AppendFormat(" AND STATUS_FLAG={0}", Constant.PLAN_FLAG);
            }
            else if (this.radioButton3.Checked == true)
            {
                sb.AppendFormat(" AND STATUS_FLAG={0}", Constant.PLAN_FLAG_NULL);
            }
            else if (this.radioButton4.Checked == true)
            {
                sb.AppendFormat(" AND STATUS_FLAG={0}", Constant.PLAN_FLAG_RETURN);
            }
            return sb.ToString();
        }

        private void FrmSalesPlan_Load(object sender, EventArgs e)
        {
            gvSalesPlan.AutoGenerateColumns = false;
            this.Show();
            txtCustomerCode.Focus();
        }
        //销售
        private void btnSales_Click(object sender, EventArgs e)
        {
            if (gvSalesPlan.CurrentRow != null)
            {
                if (gvSalesPlan.SelectedRows[0].Cells[9].Value.ToString() == "未处理")
                {
                    string slipnumber = gvSalesPlan.SelectedRows[0].Cells[0].Value.ToString();
                    FrmSalesPlanPay frm = new FrmSalesPlanPay(slipnumber, _tuser);
                    DialogResult ret = frm.ShowDialog();
                    if (DialogResult.OK.Equals(ret))
                    {
                        ds = SaleOrderPlan.GetSalesOrderPlan(getConduction());
                        gvSalesPlan.DataSource = ds.Tables[0];
                        Thread thread = new Thread(SendSalesOrderData);
                        thread.Start();
                    }
                }
                else
                {
                    MessageBox.Show("商品已经售出或者退订，无法进行此操作！", this.Text);
                }
            }
            else
            {
                MessageBox.Show("没有可销售的商品！", this.Text);
            }
        }

        //退订按钮
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (gvSalesPlan.CurrentRow != null)
            {
                if (gvSalesPlan.SelectedRows[0].Cells[9].Value.ToString() == "未处理")
                {
                    string slipnumber = gvSalesPlan.SelectedRows[0].Cells[0].Value.ToString();
                    decimal deposit = Convert.ToDecimal(gvSalesPlan.SelectedRows[0].Cells[4].Value);
                    DialogResult result = MessageBox.Show(this, "你确定要退订?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        if (SaleOrderPlan.SalesOrderReturn(GetReturnData(), slipnumber, deposit) > 0)
                        {
                            POSPrinter.OpenCash(Cache.PRN_PORT);
                            MessageBox.Show("退订成功,退回订金" + deposit + "RMB!");
                            Thread thread = new Thread(SendSalesOrderData);
                            thread.Start();
                            BindData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("此商品已售出或者已退订，无法进行此操作", this.Text);
                }
            }
            else
            {
                MessageBox.Show("没有可退订的商品！", this.Text);
            }
        }

        //退订信息获得
        private SalesOrderTable GetReturnData()
        {
            string slipnumber = gvSalesPlan.SelectedRows[0].Cells[0].Value.ToString();
            DataSet dsp = SaleOrderPlan.GetSalesOrderPlanBySlipNumber(slipnumber);
            string slipNumber = dsp.Tables[0].Rows[0]["SALES_ORDER_SLIP_NUMBER"].ToString();
            string customercode = dsp.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString();
            string lineNumber = "1";
            SalesOrderTable salesOrderTable = bSales.GetModel(slipNumber, Convert.ToInt32(lineNumber));
            salesOrderTable.MEMO = "预定取消(" + salesOrderTable.SLIP_NUMBER + ")";
            salesOrderTable.SLIP_NUMBER = new BCommon().GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"));
            salesOrderTable.LINE_NUMBER = 1;
            salesOrderTable.STATUS_FLAG = Constant.SALES_ORDER_BACK_STATUS_FLAG;
            salesOrderTable.SEND_FLAG = Constant.INIT;
            salesOrderTable.PRODUCT_CODE = Constant.PRODUCT_CODE;
            salesOrderTable.CUSTOMER_CODE = customercode;
            salesOrderTable.SALES_EMPLOYEE = _tuser.USER_ID;
            salesOrderTable.CREATE_USER = _tuser.USER_ID;
            salesOrderTable.LAST_UPDATE_USER = _tuser.USER_ID;
            salesOrderTable.CREATE_DATE_TIME = DateTime.Now;
            salesOrderTable.LAST_UPDATE_TIME = salesOrderTable.CREATE_DATE_TIME;
            salesOrderTable.QUANTITY = 0 - salesOrderTable.QUANTITY;
            salesOrderTable.AMOUNT = 0 - salesOrderTable.AMOUNT;
            salesOrderTable.POINTS = 0 - salesOrderTable.POINTS;
            salesOrderTable.USED_POINTS = 0 - salesOrderTable.USED_POINTS;
            return salesOrderTable;
        }

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void FrmSalesPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                btnSales_Click(btnSales, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnReturn_Click(btnReturn, EventArgs.Empty);
            }
        }

        private void SendSalesOrderData()
        {
            string[] names = { "SALES" };
            Hashtable ht = operate.SendDate(names);
        }

    }//end class
}

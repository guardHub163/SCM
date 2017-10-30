using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Bll;
using POS.Common;
using POS.Model;
using UserCache;

namespace POS
{
    public partial class FrmSalesPlanPay : Form
    {
        private string _slipNumber;
        BSalesOrderPlan bSaleOrderPlan = new BSalesOrderPlan();
        BSalesOrder bSales = new BSalesOrder();
        BCommon bCommon = new BCommon();
        private BaseUserTable _userTable;
        DataSet ds = new DataSet();
        string salesOrderSlipNumber = "";

        #region init
        public FrmSalesPlanPay()
        {
            InitializeComponent();
        }

        public FrmSalesPlanPay(string slipnumber, BaseUserTable userTable)
        {
            InitializeComponent();
            _slipNumber = slipnumber;
            _userTable = userTable;
        }

        /// <summary>
        /// 
        /// </summary>
        private void FrmSalesPay_Load(object sender, EventArgs e)
        {
            productGridView.AutoGenerateColumns = false;
            ds = bSaleOrderPlan.GetSalesOrderPlanBySlipNumber(_slipNumber);
            this.productGridView.DataSource = ds.Tables[0];
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                this.txtCustomerCode.Text = row["CUSTOMER_CODE"].ToString();
                this.txtCustomerPhone.Text = row["CUSTOMER_PHONE"].ToString();
                decimal Deposit = Convert.ToDecimal(row["DEPOSIT"]);
                this.txtDeposit.Text = Convert.ToString(Math.Round(Deposit, 2));
                decimal OriAmount = Convert.ToDecimal(row["ORI_AMOUNT"]);
                this.txtOriAmount.Text = Convert.ToString(Math.Round(OriAmount, 2));
                decimal SalesAmount = Convert.ToDecimal(row["SALES_AMOUNT"]);
                this.txtSalesAmount.Text = Convert.ToString(Math.Round(SalesAmount, 2));
                this.txtDiscountRate.Text = (Convert.ToDecimal(txtOriAmount.Text) - Convert.ToDecimal(txtSalesAmount.Text)).ToString();
                this.txtBalance.Text = (Convert.ToDecimal(txtSalesAmount.Text) - Convert.ToDecimal(txtDeposit.Text)).ToString();
                this.txtCharge.Text = "0";
                break;
            }

            //付款方式
            BCommon bCommon = new BCommon();
            this.cmbPayType.DisplayMember = "NAME";
            this.cmbPayType.ValueMember = "CODE";
            this.cmbPayType.DataSource = bCommon.GetNames("PAY_TYPE").Tables[0];

            //
            this.txtCustomerCode.Enabled = false;
            this.txtCustomerPhone.Enabled = false;
            this.txtDeposit.Enabled = false;
            this.txtDiscountRate.Enabled = false;
            this.txtOriAmount.Enabled = false;
            this.txtBalance.Enabled = false;
            this.txtSalesAmount.Enabled = false;
            this.txtCharge.Enabled = false;

            this.Show();
            txtPayAmount.Focus();

            //顾显总价
            POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_TOTAL_AMOUNT, txtBalance.Text.Trim());
        }
        #endregion

        /// <summary>
        /// 金额输入验证
        /// </summary>
        private void txtPayAmount_TextChanged(object sender, EventArgs e)
        {
            decimal payAmount = 0;
            decimal payBankAmount = 0;
            decimal balance = Convert.ToDecimal(txtBalance.Text.ToString());

            switch (((Control)sender).Name)
            {
                //数量 单价 金额
                case "txtPayAmount":
                case "txtPayBankAmount":
                    try
                    {
                        payAmount = Convert.ToDecimal(txtPayAmount.Text);
                        string[] str = txtPayAmount.Text.Split('.');
                        if (str.Length >= 2 && str[1] != "")
                        {
                            payAmount = Math.Round(payAmount, 2);
                            txtPayAmount.Text = payAmount.ToString();
                        }
                    }
                    catch
                    {
                        txtPayAmount.Text = "0";
                    }

                    try
                    {
                        payBankAmount = Convert.ToDecimal(txtPayBankAmount.Text);
                        string[] str = txtPayBankAmount.Text.Split('.');
                        if (str.Length >= 2 && str[1] != "")
                        {
                            payBankAmount = Math.Round(payBankAmount, 2);
                            txtPayBankAmount.Text = payBankAmount.ToString();
                        }
                    }
                    catch
                    {
                        txtPayBankAmount.Text = "0";
                    }

                    if (this.cmbPayType.SelectedValue.Equals("1"))
                    {
                        txtCharge.Text = (payAmount - balance).ToString();
                    }
                    else if (this.cmbPayType.SelectedValue.Equals("2"))
                    {
                        txtCharge.Text = (payBankAmount - balance).ToString();
                    }
                    else if (this.cmbPayType.SelectedValue.Equals("3"))
                    {
                        txtCharge.Text = (payAmount + payBankAmount - balance).ToString();
                    }
                    //顾显付款
                    POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_PAY_AMOUNT, Convert.ToString(payAmount + payBankAmount));
                    break;
            }

        }



        /// <summary>
        /// 付款方式的选择
        /// </summary>
        private void cmbPayType_SelectedValueChanged(object sender, EventArgs e)
        {
            txt_Control();
        }

        /// <summary>
        /// 付款方式的选择
        /// </summary>
        private void txt_Control()
        {
            if (this.cmbPayType.SelectedValue == null) return;
            //现金的情况下
            if (this.cmbPayType.SelectedValue.Equals("1"))
            {
                txtPayAmount.Enabled = true;
                txtPayBankAmount.Enabled = false;
                txtPayAmount.Text = "0";
                txtPayBankAmount.Text = "0";
            }
            //银行卡的情况下
            else if (this.cmbPayType.SelectedValue.Equals("2"))
            {
                txtPayAmount.Enabled = false;
                txtCharge.Enabled = false;
                txtPayBankAmount.Enabled = true;
                txtPayBankAmount.Text = "0";
                txtPayAmount.Text = "0";
                txtCharge.Text = "0";
            }
            //现金+银行卡的情况下
            else if (this.cmbPayType.SelectedValue.Equals("3"))
            {
                txtPayAmount.Enabled = true;
                txtPayBankAmount.Enabled = true;
                txtPayAmount.Text = "0";
                txtPayBankAmount.Text = "0";
            }
        }

        #region 销售结算
        /// <summary>
        /// 确认
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(this.txtCharge.Text) >= 0)
            {
                string qustionMessage = "销售单结算?" + "\r\n" +
                                        "应收合计：" + txtBalance.Text + "  RMB" + "\r\n" +
                                        "收款金额：" + (Convert.ToDecimal(txtPayAmount.Text.Trim()) + Convert.ToDecimal(txtPayBankAmount.Text.Trim())) + "  RMB" + "\r\n" +
                                        "找零金额：" + txtCharge.Text + "  RMB" + "\r\n";

                DialogResult result = MessageBox.Show(this, qustionMessage, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (bSaleOrderPlan.InsertSalesOrder(GegSalesList(ds.Tables[0]), GetReturnData(), _slipNumber) > 0)
                    {
                        //顾显找零
                        POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_CHANGE, txtCharge.Text.Trim());
                        //打开钱箱
                        POSPrinter.OpenCash(Cache.PRN_PORT);
                        PrintInvoice.print(salesOrderSlipNumber);
                        MessageBox.Show("成功进行结算,谢谢您的惠顾!", this.Text);
                        //顾显还原
                        POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_CLEAR, "");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("结算失败,请与店长或管理员联系!", this.Text);
                        this.DialogResult = DialogResult.Cancel;
                    }
                    this.Close();
                }
            }
            else
            {
                string errorMessage = "应收金额：  " + txtBalance.Text + "  RMB,收银金额少于应收金额";
                MessageBox.Show(this, errorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPayAmount.Focus();
            }
        }

        /// <summary>
        /// 销售记录的取得
        /// </summary>
        private List<SalesOrderTable> GegSalesList(DataTable salesDt)
        {
            List<SalesOrderTable> salesList = new List<SalesOrderTable>();

            //门店代号
            string departmentCode = Cache.DEPARTMENT_CODE;
            //销售单号
            salesOrderSlipNumber = bCommon.GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"));
            //明细号
            int lineNumber = 1;
            //更新时间 
            DateTime updateTime = DateTime.Now;

            foreach (DataRow row in salesDt.Rows)
            {
                SalesOrderTable salesOrderTable = new SalesOrderTable();
                //销售单号
                salesOrderTable.SLIP_NUMBER = salesOrderSlipNumber;
                //部门
                salesOrderTable.DEPARTMENT_CODE = departmentCode;
                //销售人员
                salesOrderTable.SALES_EMPLOYEE = _userTable.USER_ID;
                //顾客编号
                salesOrderTable.CUSTOMER_CODE = this.txtCustomerCode.Text;
                //明细号
                salesOrderTable.LINE_NUMBER = lineNumber++;
                //商品
                salesOrderTable.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                //原价
                salesOrderTable.ORI_PRICE = Convert.ToDecimal(row["ORI_PRICE"]);
                //售价
                salesOrderTable.PRICE = Convert.ToDecimal(row["PRICE"]);
                //折扣
                salesOrderTable.DISCOUNT_RATE = Convert.ToDecimal(Convert.ToDecimal(row["ORI_PRICE"]) - Convert.ToDecimal(row["PRICE"]));
                //数量
                salesOrderTable.QUANTITY = Convert.ToDecimal(row["QUANTITY"]);
                //单位
                salesOrderTable.UNIT_CODE = "01";
                //金额
                salesOrderTable.AMOUNT = Convert.ToDecimal(row["AMOUNT"]);
                //积分
                salesOrderTable.POINTS = Convert.ToInt32(row["AMOUNT"]);
                //使用积分
                salesOrderTable.USED_POINTS = 0;
                //状态
                salesOrderTable.STATUS_FLAG = 0;
                //集成状态
                salesOrderTable.SEND_FLAG = 0;
                //备考
                salesOrderTable.MEMO = "预定结算"; //Convert.ToString(row["MEMO"]);
                //创建时间
                salesOrderTable.CREATE_DATE_TIME = updateTime;
                //创建者
                salesOrderTable.CREATE_USER = _userTable.USER_ID;
                //更新时间
                salesOrderTable.LAST_UPDATE_TIME = updateTime;
                //更新者
                salesOrderTable.LAST_UPDATE_USER = _userTable.USER_ID;
                //收款（现金）
                salesOrderTable.CASH_AMOUNT = (Convert.ToDecimal(this.txtPayAmount.Text) + Convert.ToDecimal(this.txtDeposit.Text));
                //收款（银行）
                salesOrderTable.BANK_AMOUNT = Convert.ToDecimal(this.txtPayBankAmount.Text);
                //找零
                salesOrderTable.CHANGE = Convert.ToDecimal(this.txtCharge.Text);
                salesList.Add(salesOrderTable);
            }
            return salesList;
        }

        /// <summary>
        /// 退货商品信息的取得
        /// </summary>
        private SalesOrderTable GetReturnData()
        {
            DataSet dsp = bSaleOrderPlan.GetSalesOrderPlanBySlipNumber(_slipNumber);
            string slipNumber = dsp.Tables[0].Rows[0]["SALES_ORDER_SLIP_NUMBER"].ToString();
            string lineNumber = "1";
            SalesOrderTable salesOrderTable = bSales.GetModel(slipNumber, Convert.ToInt32(lineNumber));
            salesOrderTable.MEMO = "预定退单(" + salesOrderTable.SLIP_NUMBER + ")";
            salesOrderTable.SLIP_NUMBER = new BCommon().GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"));
            salesOrderTable.LINE_NUMBER = 1;
            salesOrderTable.STATUS_FLAG = Constant.SALES_ORDER_BACK_PLAN_STATUS_FLAG;
            salesOrderTable.SEND_FLAG = 0;
            salesOrderTable.PRODUCT_CODE = Constant.PRODUCT_CODE;
            salesOrderTable.CUSTOMER_CODE = this.txtCustomerCode.Text;
            salesOrderTable.SALES_EMPLOYEE = _userTable.USER_ID;
            salesOrderTable.CREATE_USER = _userTable.USER_ID;
            salesOrderTable.LAST_UPDATE_USER = _userTable.USER_ID;
            salesOrderTable.CREATE_DATE_TIME = DateTime.Now;
            salesOrderTable.LAST_UPDATE_TIME = salesOrderTable.CREATE_DATE_TIME;
            salesOrderTable.QUANTITY = 0 - salesOrderTable.QUANTITY;
            salesOrderTable.AMOUNT = 0 - salesOrderTable.AMOUNT;
            salesOrderTable.POINTS = 0 - salesOrderTable.POINTS;
            salesOrderTable.USED_POINTS = 0 - salesOrderTable.USED_POINTS;
            return salesOrderTable;
        }
        #endregion


        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 文本框回车键
        /// </summary>
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        /// <summary>
        /// 文本框输入控制
        /// </summary>
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyValue = (int)e.KeyChar;
            if ((keyValue >= 48 && keyValue <= 57) || keyValue == 8 || keyValue == 46)
            {
                if (sender != null && sender is TextBox && keyValue == 46)
                {
                    if (((TextBox)sender).Text.IndexOf(".") > 0)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

    }
}

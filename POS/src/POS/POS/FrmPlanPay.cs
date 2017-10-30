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

namespace POS
{
    public partial class FrmPlanPay : Form
    {
        public SalesOrderPlanTable salesPlanTable = new SalesOrderPlanTable();
        BaseVipCustomerTable customerTable = new BaseVipCustomerTable();
        BVipCustomer bCustomer = new BVipCustomer();
        public decimal cashAmount = 0;
        public decimal bankAmount = 0;
        public string customer_code = "";
        public string customer_phone = "";
        public BaseUserTable _tuser;
        DataSet ds = new DataSet();

        public FrmPlanPay()
        {
            InitializeComponent();
        }

        public FrmPlanPay(SalesOrderPlanTable _salesPlanTable)
        {
            InitializeComponent();
            salesPlanTable = _salesPlanTable;
        }

        public FrmPlanPay(SalesOrderPlanTable _salesPlanTable, decimal _bankAmount, decimal _cashAmount, BaseUserTable user, string _customer_code, string _customer_phone)
        {
            InitializeComponent();
            salesPlanTable = _salesPlanTable;
            cashAmount = _cashAmount;
            bankAmount = _bankAmount;
            customer_code = _customer_code;
            customer_phone = _customer_phone;
            _tuser = user;
        }

        private void FrmPayPlan_Load(object sender, EventArgs e)
        {
            txtCustomerCode.Text = salesPlanTable.CUSTOMER_CODE;
            txtAmount.Text = salesPlanTable.AMOUNT.ToString();
            txtOriAmount.Text = salesPlanTable.ORI_AMOUNT.ToString();
            txtDiscountRate.Text = Convert.ToString(salesPlanTable.ORI_AMOUNT - salesPlanTable.AMOUNT);
            txtSalesEmployee.Text = salesPlanTable.SALES_EMPLOYEE;
            txtOperateEmployee.Text = salesPlanTable.CREATE_USER;
            if (!"".Equals(salesPlanTable.CUSTOMER_CODE))
            {
                customerTable = bCustomer.GetModel(salesPlanTable.CUSTOMER_CODE);
                lblCustomerName.Text = customerTable.NAME;
            }
            cboPayType.Items.Add(new ItemList("1", "现金"));
            cboPayType.Items.Add(new ItemList("2", "银行卡"));


            this.cboPayType.SelectedIndex = 0;

            txtEndDate.Value = DateTime.Now.AddDays(7);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                if (Convert.ToDecimal(this.txtDeposit.Text) < 0)
                {
                    string errorMessage = "应付定金不能小于0";
                    MessageBox.Show(this, errorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Convert.ToDecimal(this.txtAmount.Text) < Convert.ToDecimal(this.txtDeposit.Text))
                {
                    string ErrorMessage = "预付定金不能大于应付金额";
                    MessageBox.Show(this, ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string qustionMessage = "预售结算？" + "\r\n" +
                                       "实收定金：" + txtDeposit.Text + "  RMB";

                    DialogResult result = MessageBox.Show(this, qustionMessage, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        customer_code = this.txtCustomerCode.Text;
                        customer_phone = this.txtCustomerPhone.Text;
                        salesPlanTable.END_DATE_TIME = Convert.ToDateTime(this.txtEndDate.Text);
                        salesPlanTable.MEMO = this.txtMemo.Text;
                        salesPlanTable.AMOUNT = Convert.ToDecimal(this.txtAmount.Text);
                        salesPlanTable.DEPOSIT = Convert.ToDecimal(this.txtDeposit.Text);
                        if (((ItemList)cboPayType.SelectedItem).Text == "2")
                        {
                            bankAmount = Convert.ToDecimal(this.txtDeposit.Text);
                        }
                        else
                        {
                            cashAmount = Convert.ToDecimal(this.txtDeposit.Text);
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }

        private bool Check()
        {
            BVipCustomer bll = new BVipCustomer();
            if (this.txtCustomerCode.Text == "")
            {
                MessageBox.Show("客户不能为空！");
                return false;
            }
            if (this.txtCustomerPhone.Text == "")
            {
                MessageBox.Show("联系方式不能为空！");
                return false;
            }
            if (this.txtDeposit.Text == "")
            {
                MessageBox.Show("预付定金不能为空!");
                return false;
            }
            return true;

        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

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

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            BVipCustomer bll = new BVipCustomer();
            string customerCode = txtCustomerCode.Text.Trim();
            if (customerCode != "")
            {
                if (!bll.Exists(customerCode))
                {
                    DialogResult result = MessageBox.Show(this, "客户不存在,是否新建客户?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        FrmAddVipInfo frm = new FrmAddVipInfo(_tuser, txtCustomerCode.Text.Trim());
                        if (frm.ShowDialog() != DialogResult.OK)
                        {
                            customerCode = "";
                        }
                    }
                    else
                    {
                        customerCode = "";
                    }
                }
                if (customerCode != "")
                {
                    BaseVipCustomerTable btable = bll.GetModel(this.txtCustomerCode.Text);
                    lblCustomerName.Text = btable.NAME;
                }
                txtCustomerCode.Text = customerCode;
            }
            else
            {
                ClearCustomerInfo();
            }
        }

        /// <summary>
        /// 客户信息的清空
        /// </summary>
        private void ClearCustomerInfo()
        {
            txtCustomerCode.Text = "";
            lblCustomerName.Text = "";
        }
    }//end class
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Bll;
using POS.Model;
using UserCache;
using System.IO;
using FastReport;
using POS.Common;
using System.Drawing.Printing;

namespace POS
{
    public partial class FrmPay : Form
    {

        private decimal _amount = 0;
        private decimal _oriAmount = 0;
        private decimal _points = 0;
        private DataTable _salesDt = null;
        private string _customerCode = "";
        private string _slipNumber = "";
        private string _salesEmployee = "";
        private BaseUserTable _tuser = null;
        private string CustomerCode = "";
        public decimal amoumt = 0;//页面加载的时候总金额
        private BSalesOrder bSalesOrder = new BSalesOrder();
        private BCommon bCommon = new BCommon();
        ItemList item = null;
        private int points = 0;
        #region init
        public FrmPay()
        {
            InitializeComponent();
        }


        public FrmPay(decimal[] total, DataTable salesDt, string customerCode, string salesEmployee, BaseUserTable tuser, int point)
        {
            InitializeComponent();
            this._amount = total[0];
            this._oriAmount = total[1];
            this._points = total[3];
            this._salesDt = salesDt;
            this._customerCode = customerCode;
            this._salesEmployee = salesEmployee;
            this._tuser = tuser;
            amoumt = _amount;
            this.points = point;
        }

        private void FrmPay_Load(object sender, EventArgs e)
        {
            dateoldTime.Enabled = false;
            dateoldTime.Value = DateTime.Now.AddDays(-1);
            //应付款
            txtAmount.Text = _amount.ToString();
            //原价
            txtOriAmount.Text = _oriAmount.ToString();
            //找零
            txtCharge.Text = "0";
            //优惠
            txtDiscount.Text = (_oriAmount - _amount).ToString();
            // txtPoints.Text = _points.ToString();

            //付款方式
            BCommon bCommon = new BCommon();
            this.cmbPayType.DisplayMember = "NAME";
            this.cmbPayType.ValueMember = "CODE";
            this.cmbPayType.DataSource = bCommon.GetNames("PAY_TYPE").Tables[0];
            this.Show();
            this.txtPayAmount.Focus();
            CmbPromotion();
            cmbPromotion.SelectedIndex = 0;
            DataTable NameTable = bCommon.GetNames("POINT_TYPE").Tables[0];
            if (NameTable != null && NameTable.Rows.Count != 0)
            {
                this.cmbPointType.DisplayMember = "NAME";
                this.cmbPointType.ValueMember = "CODE";
                this.cmbPointType.DataSource = NameTable;
               
            }
            else
            {
                cmbPointType.Items.Add(new ItemList("0000-0", "无抵兑"));
            }
            cmbPointType.SelectedIndex = 0;
            //顾显总价
            POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_TOTAL_AMOUNT, Convert.ToString(_amount));
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        #region 输入控制
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

        private void txtDecimal_TextChanged(object sender, System.EventArgs e)
        {
            decimal payAmount = 0;
            decimal payBankAmount = 0;
            decimal amount = Convert.ToDecimal(txtAmount.Text.ToString());
            decimal dMoney = 0;
            if (cmbPointType.SelectedIndex == -1)
            {
                dMoney = 0;
            }
            else
            {
                try
                {
                    if (this.cmbPointType.SelectedValue.ToString().Contains("S"))
                    {
                        this.txtPoints.Enabled = true;
                        string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                        string[] PointsMoneyArry = pointsMoney.Split('-');
                        dMoney = Convert.ToDecimal(txtPoints.Text) * (Convert.ToDecimal(PointsMoneyArry[1]) / Convert.ToDecimal(PointsMoneyArry[0].ToString()));

                    }
                    else
                    {
                        this.txtPoints.Enabled = false;
                        string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                        string[] PointsMoneyArry = pointsMoney.Split('-');
                        dMoney = Convert.ToDecimal(PointsMoneyArry[1].ToString());
                    }
                }
                catch { }
            }

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
                        if (_amount > 0)
                        {
                            txtCharge.Text = (payAmount - amount + dMoney).ToString();
                        }
                        else
                        {
                            txtCharge.Text = (payAmount - amount - dMoney).ToString();
                        }
                    }
                    else if (this.cmbPayType.SelectedValue.Equals("2"))
                    {
                        if (_amount > 0)
                        {
                            txtCharge.Text = (payBankAmount - amount + dMoney).ToString();
                        }
                        else
                        {
                            txtCharge.Text = (payBankAmount - amount - dMoney).ToString();
                        }
                    }
                    else if (this.cmbPayType.SelectedValue.Equals("3"))
                    {
                        if (_amount > 0)
                        {
                            txtCharge.Text = (payAmount + payBankAmount - amount + dMoney).ToString();
                        }
                        else
                        {
                            txtCharge.Text = (payAmount + payBankAmount - amount - dMoney).ToString();
                        }
                    }
                    //顾显付款
                    POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_PAY_AMOUNT, Convert.ToString(payAmount + payBankAmount));
                    break;
            }



        }

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
                txtPayBankAmount.Enabled = true;
                txtPayBankAmount.Text = "0";
                txtPayAmount.Text = "0";
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

        private void cmbPayType_SelectedValueChanged(object sender, EventArgs e)
        {
            txt_Control();
        }
        #endregion


        #region 销售结算
        /// <summary>
        /// 结算
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            decimal PMoney = 0;//积分抵用的金额

            if (cmbPointType.SelectedIndex == 0)
            {
                PMoney = 0;
            }
            else
            {
                string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                string[] PointsMoneyArry = pointsMoney.Split('-');
                PMoney = Convert.ToDecimal(PointsMoneyArry[1].ToString());
            }

            if (chkoldSlipnumber.Checked)
            {
                if (dateoldTime.Value.ToString() == "")
                {
                    MessageBox.Show("补单的日期不能大于或等于今天", this.Text);
                    return;
                }
                if (Convert.ToDateTime(dateoldTime.Value.ToString("yyyy/MM/dd")) >= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")))
                {
                    MessageBox.Show("补单的日期不能大于或等于今天", this.Text);
                    return;
                }
            }
            DialogResult result = new DialogResult();
            if (Convert.ToDecimal(txtCharge.Text) < 0)
            {
                string errorMessage = "应收合计：  " + (Convert.ToDecimal(txtAmount.Text)) + "  RMB,收银金额少于应收金额";
                MessageBox.Show(this, errorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPayAmount.Focus();
            }
            else
            {
                string qustionMessage = "销售单结算?" + "\r\n" +
                                        "应收合计：" + txtAmount.Text + "  RMB" + "\r\n" +
                                        "收款金额：" + (Convert.ToDecimal(txtPayAmount.Text.Trim()) + Convert.ToDecimal(txtPayBankAmount.Text.Trim())) + "  RMB" + "\r\n" +
                                        "找零金额：" + txtCharge.Text + "  RMB" + "\r\n";

                result = MessageBox.Show(this, qustionMessage, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (PayMent())
                    {
                        // 顾显找零
                        POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_CHANGE, txtCharge.Text.Trim());
                        //打开钱箱
                        POSPrinter.OpenCash(Cache.PRN_PORT);
                        PrintInvoice.print(_slipNumber);
                        MessageBox.Show("成功进行结算,谢谢您的惠顾!", this.Text);
                        this.DialogResult = DialogResult.OK;
                        // 顾显还原
                        //POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_CLEAR, "");
                    }
                    else
                    {
                        MessageBox.Show("结算失败,请与店长或管理员联系!", this.Text);
                        this.DialogResult = DialogResult.No;
                    }
                    this.Close();
                }

            }
        }

        /// <summary>
        ///  销售结算
        /// </summary>
        private bool PayMent()
        {
            List<SalesOrderTable> salesList = new List<SalesOrderTable>();

            //门店代号
            string departmentCode = Cache.DEPARTMENT_CODE;
            //销售单号
            if (chkoldSlipnumber.Checked)
            {
                DataSet ds = bCommon.GetOldMaxSlipNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"), dateoldTime.Value.ToString("yyyyMMdd"));
                if (ds.Tables[0].Rows.Count == 0)
                {
                    _slipNumber = Cache.GetBllStyleName("BLL_SALES_ORDER") + dateoldTime.Value.ToString("yyyyMMdd") + "0001";
                }
                else
                {
                    string slipNumber = ds.Tables[0].Rows[0]["slip_number"].ToString();
                    int number = Convert.ToInt32(slipNumber.Substring(slipNumber.Length - 4, 4).ToString()) + 1;
                    _slipNumber = Cache.GetBllStyleName("BLL_SALES_ORDER") + dateoldTime.Value.ToString("yyyyMMdd") + Convert.ToString(number).PadLeft(4, '0');
                }
            }
            else
            {
                _slipNumber = bCommon.GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"));
            }
            //营业员
            string salesEmployeeCode = _salesEmployee;
            //顾客编号
            string customerCode = _customerCode;
            //明细号
            int lineNumber = 1;
            //更新时间 
            DateTime updateTime = DateTime.Now;

            decimal bankAmount = Convert.ToDecimal(txtPayBankAmount.Text.Trim());

            decimal cashAmount = Convert.ToDecimal(txtPayAmount.Text.Trim());

            decimal changeAmount = Convert.ToDecimal(txtCharge.Text.Trim());

            decimal promotinamount = 0;

            decimal promotion = 0;

            decimal totalpromotion = 0;//累计促销的金额

            int i = 0;//次数计时器
            decimal PMoney = 0;//积分抵用的金额
            string Ppoints = ""; //抵用的积分
            if (cmbPointType.SelectedIndex == 0)
            {
                PMoney = 0;
                Ppoints = "0000";
            }
            else
            {
                if (this.cmbPointType.SelectedValue.ToString().Contains("S"))
                {
                    this.txtPoints.Enabled = true;
                    string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                    string[] PointsMoneyArry = pointsMoney.Split('-');
                    Ppoints = txtPoints.Text;
                    PMoney = Convert.ToDecimal(txtPoints.Text) * (Convert.ToDecimal(PointsMoneyArry[1]) / Convert.ToDecimal(PointsMoneyArry[0].ToString()));

                }
                else
                {
                    this.txtPoints.Enabled = false;
                    string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                    string[] PointsMoneyArry = pointsMoney.Split('-');
                    Ppoints = PointsMoneyArry[0].ToString();//抵用的积分
                    PMoney = Convert.ToDecimal(PointsMoneyArry[1].ToString());
                }
            }

            foreach (DataRow row in _salesDt.Rows)
            {
                SalesOrderTable salesOrderTable = new SalesOrderTable();
                if (cmbPromotion.SelectedIndex != 0)
                {
                    item = (ItemList)(cmbPromotion.SelectedItem);
                    promotinamount = Convert.ToDecimal(Cache.GetPromotionPrice(item.Value.ToString()));
                    salesOrderTable.PROMOTION_AMOUNT = promotinamount;
                    if (_salesDt.Rows.Count - i == 0)
                    {
                        promotion = promotinamount - totalpromotion;
                    }
                    else
                    {
                        promotion = decimal.Round(Convert.ToDecimal(Convert.ToDecimal(row["AMOUNT"]) / amoumt * promotinamount), 2);
                        totalpromotion = totalpromotion + promotion;
                        i++;
                    }
                    salesOrderTable.PROMOTION_DISCOUNTS = promotion;
                    salesOrderTable.ATTRIBUTE1 = item.Value + "@" + item.Text;
                }
                else
                {
                    salesOrderTable.PROMOTION_AMOUNT = 0;
                    salesOrderTable.PROMOTION_DISCOUNTS = 0;
                }

                //现金支付金额
                salesOrderTable.CASH_AMOUNT = cashAmount;
                //刷卡支付金额
                salesOrderTable.BANK_AMOUNT = bankAmount;
                //找零
                salesOrderTable.CHANGE = changeAmount;
                //销售单号
                salesOrderTable.SLIP_NUMBER = _slipNumber;
                //部门
                salesOrderTable.DEPARTMENT_CODE = departmentCode;
                //销售人员
                salesOrderTable.SALES_EMPLOYEE = salesEmployeeCode;
                //顾客编号
                salesOrderTable.CUSTOMER_CODE = customerCode;
                //明细号
                salesOrderTable.LINE_NUMBER = lineNumber++;
                //商品
                salesOrderTable.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                //原价
                salesOrderTable.ORI_PRICE = Convert.ToDecimal(row["ORI_PRICE"]);
                //折扣
                salesOrderTable.DISCOUNT_RATE = Convert.ToDecimal(row["DISCOUNT_RATE"]);
                //售价
                salesOrderTable.PRICE = Convert.ToDecimal(row["PRICE"]);
                //数量
                salesOrderTable.QUANTITY = Convert.ToDecimal(row["QUANTITY"]);
                //单位
                salesOrderTable.UNIT_CODE = "01";

                if (Ppoints != "0000")//使用积分支付
                {
                    if (_amount > 0)  //销售
                    {
                        if (points > Convert.ToInt32(Ppoints))
                        {
                            if (salesOrderTable.LINE_NUMBER == 1)//明细的第一条拥有积分
                            {
                                //金额
                                salesOrderTable.AMOUNT = Convert.ToDecimal(row["AMOUNT"]) - promotion;
                                //积分
                                //salesOrderTable.POINTS = Convert.ToInt32(salesOrderTable.AMOUNT);
                                salesOrderTable.POINTS = Convert.ToInt32(cashAmount + bankAmount - changeAmount);
                                //使用积分
                                salesOrderTable.USED_POINTS = Convert.ToInt32(Ppoints);
                            }
                            else
                            {
                                //金额
                                salesOrderTable.AMOUNT = Convert.ToDecimal(row["AMOUNT"]) - promotion;
                            }
                        }
                        else
                        {
                            MessageBox.Show("客户所拥有的积分不足以抵兑！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else    //退货
                    {
                        if (_customerCode != "")
                        {

                            if (salesOrderTable.LINE_NUMBER == 1)//明细的第一条拥有积分
                            {
                                //金额
                                salesOrderTable.AMOUNT = Convert.ToDecimal(row["AMOUNT"]) - promotion;
                                //积分
                                //salesOrderTable.POINTS = Convert.ToInt32(salesOrderTable.AMOUNT);
                                salesOrderTable.POINTS = 0 - Convert.ToInt32(changeAmount);
                                //使用积分}
                                salesOrderTable.USED_POINTS = 0 - Convert.ToInt32(Ppoints);
                            }
                            else
                            {
                                //金额
                                salesOrderTable.AMOUNT = Convert.ToDecimal(row["AMOUNT"]) - promotion;
                            }
                        }
                        else
                        {
                            MessageBox.Show("没有客户无法退货！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                    }


                }
                else //不使用积分
                {
                    //金额
                    salesOrderTable.AMOUNT = Convert.ToDecimal(row["AMOUNT"]) - promotion;
                    //积分
                    salesOrderTable.POINTS = Convert.ToInt32(salesOrderTable.AMOUNT);
                    //使用积分
                    salesOrderTable.USED_POINTS = 0;
                }
                //状态
                if (salesOrderTable.QUANTITY > 0)
                {
                    salesOrderTable.STATUS_FLAG = Constant.SALES_PAY_STATUS_FLAG;
                }
                else
                {
                    salesOrderTable.STATUS_FLAG = Constant.SALES_ORDER_BACK_PLAN_STATUS_FLAG;
                }
                //集成状态
                salesOrderTable.SEND_FLAG = Constant.INIT;
                //备考
                if (Convert.ToDecimal(row["QUANTITY"].ToString()) > 0)
                {
                    salesOrderTable.MEMO = Convert.ToString(row["MEMO"]);
                }
                else
                {
                    salesOrderTable.MEMO = "退货";
                }
                //创建时间
                if (chkoldSlipnumber.Checked)
                {
                    salesOrderTable.CREATE_DATE_TIME = dateoldTime.Value;
                }
                else
                {
                    salesOrderTable.CREATE_DATE_TIME = updateTime;
                }
                //创建者
                salesOrderTable.CREATE_USER = _tuser.USER_ID;
                //更新时间
                salesOrderTable.LAST_UPDATE_TIME = updateTime;
                //更新者
                salesOrderTable.LAST_UPDATE_USER = _tuser.USER_ID;


                salesList.Add(salesOrderTable);
            }

            if (bSalesOrder.InsertSales(salesList, "") > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void chkoldSlipnumber_CheckedChanged(object sender, EventArgs e)
        {
            if (chkoldSlipnumber.Checked == true)
            {
                dateoldTime.Enabled = true;
            }
            if (chkoldSlipnumber.Checked == false)
            {
                dateoldTime.Enabled = false;
            }
        }

        private void CmbPromotion()
        {
            DataSet ds = Cache.PROMOTIONINFO;
            cmbPromotion.Items.Add(new ItemList("00", "无促销"));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                cmbPromotion.Items.Add(new ItemList(row["CODE"].ToString(), row["NAME"].ToString()));
            }
        }


        private void cmbPromotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPromotion.SelectedIndex != 0)
            {
                item = (ItemList)(this.cmbPromotion.SelectedItem);
                decimal price = Convert.ToDecimal(Cache.GetPromotionPrice(item.Value.ToString()));
                this.txtAmount.Text = Convert.ToString(amoumt - price);
                this.txtDiscount.Text = Convert.ToString(Convert.ToDecimal(txtOriAmount.Text) - amoumt + price);
                this.txtCharge.Text = Convert.ToString(0 - Convert.ToDecimal(this.txtAmount.Text));
            }
            else
            {
                this.txtAmount.Text = Convert.ToString(amoumt);
                this.txtDiscount.Text = Convert.ToString(Convert.ToDecimal(txtOriAmount.Text) - amoumt);
                this.txtCharge.Text = Convert.ToString(0 - Convert.ToDecimal(this.txtAmount.Text));
            }
        }

        private void cmbPointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_customerCode == "")
            {

                //MessageBox.Show("没有客户无法进行积分抵兑。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbPointType.SelectedIndex = 0;
                return;
            }

            this.txtPayAmount.Text = "0";

            decimal PMoney = 0;//积分抵用的金额

            if (this.cmbPointType.SelectedValue.ToString().Contains("S"))
            {
                this.txtPoints.Enabled = true;
                string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                string[] PointsMoneyArry = pointsMoney.Split('-');
                PMoney = Convert.ToDecimal(txtPoints.Text) * (Convert.ToDecimal(PointsMoneyArry[1]) / Convert.ToDecimal(PointsMoneyArry[0].ToString()));

            }
            else
            {
                this.txtPoints.Enabled = false;
                string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                string[] PointsMoneyArry = pointsMoney.Split('-');
                PMoney = Convert.ToDecimal(PointsMoneyArry[1].ToString());
            }
            if (cmbPointType.SelectedIndex == 0)
            {
                PMoney = 0;
            }
            if (_amount > 0)
            {
                txtCharge.Text = Convert.ToString(Convert.ToDecimal(txtPayAmount.Text) - Convert.ToDecimal(txtAmount.Text) + Convert.ToDecimal(PMoney));
            }
            else
            {
                txtCharge.Text = Convert.ToString(Convert.ToDecimal(txtPayAmount.Text) - Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(PMoney));
            }
        }

        private void txtPoints_TextChanged(object sender, EventArgs e)
        {
            decimal PMoney = 0;//积分抵用的金额
            if (this.cmbPointType.SelectedValue.ToString().Contains("S"))
            {
                this.txtPoints.Enabled = true;
                string pointsMoney = this.cmbPointType.SelectedValue.ToString();
                string[] PointsMoneyArry = pointsMoney.Split('-');
                PMoney = Convert.ToDecimal(txtPoints.Text) * (Convert.ToDecimal(PointsMoneyArry[1]) / Convert.ToDecimal(PointsMoneyArry[0].ToString()));

            }
            if (_amount > 0)
            {
                txtCharge.Text = Convert.ToString(Convert.ToDecimal(txtPayAmount.Text) - Convert.ToDecimal(txtAmount.Text) + Convert.ToDecimal(PMoney));
            }
            else
            {
                txtCharge.Text = Convert.ToString(Convert.ToDecimal(txtPayAmount.Text) - Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(PMoney));
            }
        }


    }//end class
}

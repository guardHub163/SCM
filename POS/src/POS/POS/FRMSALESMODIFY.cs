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

namespace POS
{
    public partial class FrmSalesModify : Form
    {

        public SalesOrderTable salesOrder = new SalesOrderTable();
        private BaseUserTable _tuser;
        private string _styleCode;

        #region init
        public FrmSalesModify()
        {
            InitializeComponent();
        }

        public FrmSalesModify(BaseUserTable _usertable)
        {
            InitializeComponent();
            _tuser = _usertable;

        }

        private void FrmSalesModify_Load(object sender, EventArgs e)
        {
            //商品信息初始化
            lblProductCode.Text = salesOrder.PRODUCT_CODE;
            lblProductName.Text = salesOrder.NAME;
            lblStyle.Text = salesOrder.STYLE_NAME;
            lblColorSize.Text = salesOrder.COLOR_NAME + "  " + salesOrder.SIZE;

            //单价
            txtPrice.Text = salesOrder.PRICE.ToString();
            //原价
            txtOriPrice.Text = salesOrder.ORI_PRICE.ToString();
            //折扣
            txtDiscountRate.Text = salesOrder.DISCOUNT_RATE.ToString();
            //数量
            txtQuantity.Text = salesOrder.QUANTITY.ToString();
            //使用积分
            txtUsedPoints.Text = salesOrder.USED_POINTS.ToString();
            //金额
            txtAmount.Text = (salesOrder.QUANTITY * salesOrder.PRICE).ToString();
            //备注
            txtMemo.Text = salesOrder.MEMO;
            //款号
            _styleCode = new BProduct().GetModel(salesOrder.PRODUCT_CODE).STYLE;

            this.Show();
            this.txtPrice.Focus();

        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (!CheckInput())
            {
                return;
            }
            //单价
            salesOrder.PRICE = Convert.ToDecimal(txtPrice.Text.Trim());
            //原价
            salesOrder.ORI_PRICE = Convert.ToDecimal(txtOriPrice.Text.Trim());
            //折扣
            salesOrder.DISCOUNT_RATE = Convert.ToDecimal(txtDiscountRate.Text.Trim());
            //数量
            salesOrder.QUANTITY = Convert.ToDecimal(txtQuantity.Text.Trim());
            //使用积分
            salesOrder.USED_POINTS = Convert.ToInt32(txtUsedPoints.Text.Trim());
            //金额
            salesOrder.AMOUNT = Convert.ToDecimal(txtAmount.Text.Trim());
            //备注
            salesOrder.MEMO = txtMemo.Text.Trim();

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 输入CHECK
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

        /// <summary>
        /// 输入CHECK
        /// </summary>
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyValue = (int)e.KeyChar;
            if ((keyValue >= 48 && keyValue <= 57) || keyValue == 8 || keyValue == 45)
            {
                if (sender != null && sender is TextBox && keyValue == 45)
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

        /// <summary>
        /// 接收页面的KEYDOWN事件
        /// </summary>
        private void FrmSalesModify_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    lkPrice_LinkClicked(null, null);
                    break;
            }
        }

        private void lkPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CheckInput())
            {
                FrmPrice priceForm = new FrmPrice(_styleCode, _tuser.DEPARTMENT_CODE);
                priceForm.ShowDialog();
                if (priceForm.DialogResult == DialogResult.OK)
                {
                    txtOriPrice.Text = priceForm.productPrices[0].ToString();
                    txtPrice.Text = priceForm.productPrices[1].ToString();
                    txtDiscountRate.Text = priceForm.productPrices[2].ToString();
                    CalculateAmount();
                }
                priceForm.Dispose();
            }
        }


        /// <summary>
        /// 接收文本框的KEYDOWN事件
        /// </summary>
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                switch (((TextBox)sender).Name)
                {
                    case "txtPrice":
                    case "txtQuantity":
                    case "txtUsedPoints":
                        if (CheckInput())
                        {
                            SendKeys.Send("{Tab}");
                        }
                        break;
                    default:
                        SendKeys.Send("{Tab}");
                        break;
                }
            }
        }
        /// <summary>
        /// 接收文本框的leave事件
        /// </summary>
        private void txt_Leave(object sender, EventArgs e)
        {
            CheckInput();
        }

        /// <summary>
        /// 金额计算更新
        /// </summary>
        private void CalculateAmount()
        {
            decimal qty = Convert.ToDecimal(txtQuantity.Text.Trim());
            decimal price = Convert.ToDecimal(txtPrice.Text.Trim());
            int usedPoints = Convert.ToInt32(txtUsedPoints.Text.Trim());
            txtAmount.Text = Convert.ToString(Math.Round(qty * price - usedPoints / 20, 2));
        }

        /// <summary>
        /// 验证页面输入
        /// </summary>
        private bool CheckInput()
        {
            decimal quantity = 0;
            decimal usedPoints = 0;
            decimal money = 0;
            try
            {
                quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                //if (quantity < 1)
                //{
                //    MessageBox.Show("数量不能小1!", this.Text);
                //    txtQuantity.Text = Convert.ToString(1);
                //    return false;
                //}
            }
            catch
            {
                MessageBox.Show("数量输入格式错误!", this.Text);
                txtQuantity.Text = Convert.ToString(1);
                return false;
            }
            try
            {
                money = Convert.ToDecimal(txtPrice.Text.Trim());
                if (money < 0)
                {
                    MessageBox.Show("单价不能小0!", this.Text);
                    txtPrice.Text = Convert.ToString(0);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("单价输入格式错误!", this.Text);
                txtPrice.Text = Convert.ToString(0);
                return false;
            }

            try
            {
                if (Convert.ToDecimal(txtQuantity.Text.Trim()) > 0)
                {
                    usedPoints = Convert.ToDecimal(txtUsedPoints.Text.Trim());
                    if (usedPoints < 0)
                    {
                        MessageBox.Show("使用积分不能为负数!", this.Text);
                        txtUsedPoints.Text = Convert.ToString(0);
                        return false;
                    }
                    if (usedPoints % 20 > 0)
                    {
                        MessageBox.Show("使用积分必须是20的整倍数!", this.Text);
                        txtUsedPoints.Text = Convert.ToString(usedPoints - usedPoints % 20);
                        return false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("使用积分输入格式错误!", this.Text);
                txtUsedPoints.Text = Convert.ToString(0);
                return false;
            }
            if (Convert.ToDecimal(txtQuantity.Text.Trim()) > 0)
            {
                if (usedPoints > quantity * Convert.ToDecimal(txtPrice.Text.Trim()))
                {
                    MessageBox.Show("使用积分数不能大于金额数!", this.Text);
                    return false;
                }
            }
            CalculateAmount();
            return true;
        }

    }//end class
}

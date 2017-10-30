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
using System.IO;
using System.Xml;
using POS.Common;
using System.Collections;
using System.Threading;

namespace POS
{
    public partial class FrmCashDetail : Form
    {
        private BCash bCash = new BCash();
        private decimal _balanceCash = 0;
        private string _userId = "";
        private BCommon bcommon = new BCommon();
        private WebServieceOperate operate = new WebServieceOperate();

        #region init
        public FrmCashDetail()
        {
            InitializeComponent();
        }

        public FrmCashDetail(decimal balanceCash, string userId)
        {
            InitializeComponent();
            _balanceCash = balanceCash;
            _userId = userId;
        }

        private void FrmCashDetail_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in Cache.BANK.Tables[0].Rows)
                {
                    cboBack.Items.Add(new ItemList(Convert.ToString(row["CODE"]), Convert.ToString(row["NAME"])));
                }
                cboBack.SelectedIndex = 0;
            }
            catch { }

            txtBalanceCash.Text = _balanceCash.ToString();
            rdoGet.Checked = true;
            this.Show();
            txtAmount.Focus();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                SendKeys.Send("{Tab}"); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if ("0".Equals(txtAmount.Text.Trim()))
            {
                MessageBox.Show("存取金额不能为零！");
                return;
            }
            string message = "";
            if (rdoGet.Checked)
            {
                message = "你确定要取出现金" + txtAmount.Text + "元吗?";
            }
            else
            {
                message = "你确定要存入现金" + txtAmount.Text + "元吗?";
            }
            if (MessageBox.Show(message, this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CashTable cashTable = new CashTable();
                cashTable.SLIP_NUMBER = new BCommon().GetSeqNumber(Cache.GetBllStyleName("BLL_CASH"));
                if (rdoGet.Checked)
                {
                    cashTable.TAKE_CASH = -Convert.ToDecimal(txtAmount.Text.Trim());
                }
                else
                {
                    cashTable.TAKE_CASH = Convert.ToDecimal(txtAmount.Text.Trim());
                }
                cashTable.LAST_CASH = Convert.ToDecimal(txtSurplus.Text.Trim());
                cashTable.BALANCE_CASH = cashTable.LAST_CASH;
                if (rdoGet.Checked)
                {
                    cashTable.BANK_NAME = ((ItemList)cboBack.SelectedItem).Text;
                    if (cashTable.BANK_NAME == "自提")
                    {
                        cashTable.STATUS_FLAG = Constant.OUT_CASH;
                    }
                    else 
                    {
                        cashTable.STATUS_FLAG = Constant.BANK_CASH;
                    }
                }
                else
                {
                    cashTable.BANK_NAME = "存款操作";
                    cashTable.STATUS_FLAG = Constant.INT_CASH;
                }
                cashTable.MEMO = txtMemo.Text;
                cashTable.CREATE_USER = _userId;
                cashTable.LAST_UPDATE_USER = _userId;
                bCash.Insert(cashTable);
                Thread thread = new Thread(SendCashData);
                thread.Start();
                this.DialogResult = DialogResult.OK;
                ////打开钱箱
                POSPrinter.OpenCash(Cache.PRN_PORT);
                this.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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
        /// 
        /// </summary>
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            decimal amount = 0;
            try
            {
                amount = Convert.ToDecimal(txtAmount.Text.Trim());
                string[] str = txtAmount.Text.Trim().Split('.');
                if (str.Length >= 2 && str[1] != "")
                {
                    amount = Math.Round(amount, 2);
                    txtAmount.Text = amount.ToString();
                }
            }
            catch
            {
                txtAmount.Text = "0";
            }
            if (rdoGet.Checked)
            {
                if (_balanceCash - amount < 0)
                {
                    MessageBox.Show("取出金额不能大于钱箱金额!", this.Text);
                    txtAmount.Text = "0";
                    txtAmount.Focus();
                    return;
                }
                txtSurplus.Text = (_balanceCash - amount).ToString();
            }
            else
            {
                txtSurplus.Text = (_balanceCash + amount).ToString();
            }


        }
       

        /// <summary>
        /// 
        /// </summary>
        private void Rdo_Changed(object sender, EventArgs e)
        {
            if (rdoGet.Checked)
            {
                cboBack.Enabled = true;
            }
            else if (rdoSet.Checked)
            {
                cboBack.Enabled = false;
            }

        }
        //信息传回SCM
        private void SendCashData()
        {
            string[] names = { "CASH" };
            Hashtable ht= operate.SendDate(names);
        }
    }
}

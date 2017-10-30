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
using POS.Common;
using System.IO;
using System.Xml;
using System.Threading;
using System.Collections;

namespace POS
{
    public partial class FrmCashModify : Form
    {
        private string _slipNumber = "";
        BCash bCash = new BCash();
        CashTable cashTable = null;
        BCommon bcommon = new BCommon();
        private WebServieceOperate operate = new WebServieceOperate();
        private string _userId = "";

        #region init
        public FrmCashModify()
        {
            InitializeComponent();
        }

        public FrmCashModify(string slipNumber,string userId)
        {
            InitializeComponent();
            _slipNumber = slipNumber;
            _userId = userId;
        }

        private void FrmCashModify_Load(object sender, EventArgs e)
        {
            cashTable = bCash.GetModel(" slip_number = '" + _slipNumber + "'");
            if (cashTable != null)
            {
                txtSlipNumber.Text = cashTable.SLIP_NUMBER;
                txtBankName.Text = cashTable.BANK_NAME;
                txtTakeCash.Text = cashTable.TAKE_CASH.ToString();
            }
            else 
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(txtBankSlipNumber.Text.Trim() == "")
            {
                MessageBox.Show("存款流水号不能为空！", this.Text);
                return;
            }

            cashTable.BANK_SLIP_NUMBER = txtBankSlipNumber.Text.Trim();
            cashTable.LAST_UPDATE_USER = _userId;
            bCash.Update(cashTable);
            Thread thread = new Thread(SendCashData);
            thread.Start();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtBankSlipNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyValue = (int)e.KeyChar;
            if ((keyValue >= 48 && keyValue <= 57) || keyValue == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtBankSlipNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(this, EventArgs.Empty);
            }
        }

        private void SendCashData()
        {
            string[] names = { "CASH_BANK" };
            Hashtable ht = operate.SendDate(names);

        }
    }
}

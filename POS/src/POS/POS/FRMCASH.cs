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

namespace POS
{
    public partial class FrmCash : Form
    {

        private float X; private float Y;

        public DataTable dtCashD = new DataTable();
        BCash bCash = new BCash();
        private string _userId = "";

        #region  init
        public FrmCash()
        {
            InitializeComponent();
        }

        public FrmCash(string userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void FrmCash_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(FrmCash_Resize);//窗体调整大小时引发事件
            X = 700;//获取窗体的宽度
            Y = 80;//获取窗体的高度
            setTag(this.panel2);//调用方法
            FrmCash_Resize(sender, e);

            //绑定数据源
            initGridView();

            initCurrentCash();
        }

        private void setTag(Control cons)
        {
            //遍历窗体中的控件
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        //根据窗体大小调整控件大小
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                if (con.Tag == null)
                {
                    return;
                }
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
                float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
                con.Width = (int)a;//宽度
                a = Convert.ToSingle(mytag[1]) * newy;//高度
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;//左边距离
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
                con.Top = (int)(a);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        private void FrmCash_Resize(object sender, EventArgs e)
        {
            float newx = (this.panel2.Width) / X; //窗体宽度缩放比例
            float newy = this.panel2.Height / Y;//窗体高度缩放比例
            setControls(newx, newy, this.panel2);//随窗体改变控件大小
        }

        private void initGridView()
        {
            this.cashGridView.AutoGenerateColumns = false;
            dtCashD = bCash.GetList(30, " status_flag<>0 ", "cash_date ").Tables[0];
            DataView dvDetail = new DataView(dtCashD);
            this.cashGridView.DataSource = dvDetail;

        }

        private void initCurrentCash()
        {

            CashTable cashTable = bCash.GetModel(" status_flag = 0 ");
            //本次收益现金
            this.lblProfitCash.Text = cashTable.PROFIT_CASH.ToString();
            //上次留存金额
            this.lblLastCash.Text = cashTable.LAST_CASH.ToString();
            //钱箱总金额
            this.lblBalanceCash.Text = cashTable.BALANCE_CASH.ToString();
        }
        #endregion

        private void btnCash_Click(object sender, EventArgs e)
        {
            FrmCashDetail frm = new FrmCashDetail(Convert.ToDecimal(lblBalanceCash.Text.Trim()), _userId);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                initGridView();
                initCurrentCash();
            }
            frm.Dispose();

        }

        private void FrmCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnCash_Click(btnCash, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnBankSlipNumber_Click(btnBankSlipNumber, EventArgs.Empty);
            }
        }

        private void btnBankSlipNumber_Click(object sender, EventArgs e)
        {
            if (cashGridView.CurrentRow != null)
            {
                DataRow row = dtCashD.Rows[cashGridView.SelectedRows[0].Index];
                if (Convert.ToDecimal(row["TAKE_CASH"]) >= 0)
                {
                    MessageBox.Show("存款操作,不需要存款流水号!", this.Text);
                }
                else if ("自提".Equals(row["BANK_NAME"]))
                {
                    MessageBox.Show("自提操作,不需要存款流水号!", this.Text);
                }
                else if (Convert.ToString(row["BANK_SLIP_NUMBER"]) != "")
                {
                    MessageBox.Show("己经输入过存款流水号!", this.Text);
                }
                else
                {
                    FrmCashModify frm = new FrmCashModify(Convert.ToString(row["SLIP_NUMBER"]), _userId);
                    frm.ShowDialog(this);
                    initGridView();
                    initCurrentCash();
                }
            }
            else 
            {
                MessageBox.Show("没有选择流水号！");
            }
        }

    }
}

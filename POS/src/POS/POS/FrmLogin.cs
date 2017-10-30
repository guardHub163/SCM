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
using System.Collections;

namespace POS
{
    public partial class FrmLogin : Form
    {
        private MainForm _mainWin;

        public FrmLogin()
        {
            InitializeComponent();

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            login();
        }


        private void login()
        {

            string strErrorlog = string.Empty;
            try
            {
                BUser buser = new BUser();
                if(string.IsNullOrEmpty(this.txtPassword.Text.Trim())){
                    
                    strErrorlog = "密码不能为空!";
                    MessageBox.Show(strErrorlog, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPassword.Focus();
                    this.txtPassword.Select();
                    return;
                }
                
                BaseUserTable baseUserTable = buser.ValidateLogin(this.cmbUser.SelectedValue.ToString(), this.txtPassword.Text.Trim());
                if (baseUserTable == null)
                {
                    strErrorlog = "密码错误,请重新输入!";
                    MessageBox.Show(strErrorlog, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPassword.Focus();
                    this.txtPassword.Select();
                    return;
                }
                this.Visible = false;
                MainForm mainForm = new MainForm(baseUserTable);
                this.Hide();
                mainForm.Show();
                _mainWin = mainForm;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information); //数据库。登录失败
                this.Cursor = Cursors.Default;
            }

        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            BCommon bcomm = new BCommon();
            try
            {
                bcomm.IsDBConn();
            }
            catch (Exception a) 
            {
                MessageBox.Show("连接失败!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

            //用户显示初始化
            BUser buser = new BUser();
            string where = " 1=1 ";
            this.cmbUser.DataSource = buser.GetList(where).Tables[0];
            this.cmbUser.ValueMember = "USER_ID";
            this.cmbUser.DisplayMember = "TRUE_NAME";

            this.cmbUser.DropDownStyle = ComboBoxStyle.DropDownList;

            this.txtPassword.Focus();
            this.txtPassword.Select();
        }
    }
}

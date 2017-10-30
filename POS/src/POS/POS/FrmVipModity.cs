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
using POS.Common;

namespace POS
{
    public partial class FrmVipModity : Form
    {
        private string code;
        BVipCustomer bvip = new BVipCustomer();
        private BaseUserTable usertable;
        public bool flag = false;
        public FrmVipModity()
        {
            InitializeComponent();
        }

        public FrmVipModity(string _code,BaseUserTable _usertable) 
        {
            code = _code;
            usertable = _usertable;
            InitializeComponent();
        }

        private void FrmVipModity_Load(object sender, EventArgs e)
        {
            this.txtCode.Text=code;
            DataSet ds = bvip.GetAllInfo(strWhere());
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                this.txtName.Text =Data.ToString( row["NAME"]);
                this.txtAddress.Text = Data.ToString(row["ADDRESS"]);
                this.txtQQ.Text =Data.ToString( row["QQ"]);
                this.txtWW.Text = Data.ToString(row["WW"]);
                this.txtEmail.Text =Data.ToString( row["EMAIL"]);
              
                 this.txtBirth.Value =Data.ToDateTime( row["BIRTH_DATE"]);
                this.txtPoints.Text =Data.ToString( row["POINTS"]);
                this.txtDiscount.Text =Data.ToString( row["DISCOUNT_RATE"]);
                this.txtDate.Text =Data.ToString (row["LAST_SALES_DATE"]);
            }
            this.txtCode.Enabled = false;
            this.txtDate.Enabled = false;
            this.txtDiscount.Enabled = false;
            this.txtPoints.Enabled = false;
        }

        private string strWhere()
        {
            StringBuilder str = new StringBuilder();
            if (code != "") 
            {
                str.AppendFormat(" CODE='{0}'", code);
            }
            return str.ToString();
        }

        private void btnModity_Click(object sender, EventArgs e)
        {
            BaseVipCustomerTable bVipTable = new BaseVipCustomerTable();
            bVipTable.CODE = this.txtCode.Text;
            bVipTable.NAME = this.txtName.Text;
            bVipTable.ADDRESS = this.txtAddress.Text;
            bVipTable.QQ = this.txtQQ.Text;
            bVipTable.EMAIL = this.txtEmail.Text;
            bVipTable.BIRTH_DATE = Convert.ToDateTime(this.txtBirth.Text);
            bVipTable.POINTS = Convert.ToInt32(this.txtPoints.Text);
            bVipTable.DISCOUNT_RATE = Convert.ToDecimal(this.txtDiscount.Text);
            bVipTable.LAST_SALES_DATE = Convert.ToDateTime(this.txtDate.Text);
            bVipTable.WW = this.txtWW.Text;
            bVipTable.LAST_UPDATE_USER = usertable.USER_ID;
            if (bvip.Update(bVipTable))
            {
                MessageBox.Show("修改成功！");
                flag = true;
                this.Close();
            }
            else 
            {
                MessageBox.Show("修改失败！");
                
            }
            
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

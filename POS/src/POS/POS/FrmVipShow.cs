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
    public partial class FrmVipShow : Form
    {
        private string code;
        BVipCustomer bvip = new BVipCustomer();
        private BaseUserTable usertable;
        public bool flag = false;
        public FrmVipShow()
        {
            InitializeComponent();
        }

        public FrmVipShow(string _code) 
        {
            code = _code;
            InitializeComponent();
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

        private void FrmVipShow_Load_1(object sender, EventArgs e)
        {
            this.txtCode.Text = code;
            DataSet ds = bvip.GetAllInfo(strWhere());
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                this.txtName.Text = Data.ToString(row["NAME"]);
                this.txtAddress.Text = Data.ToString(row["ADDRESS"]);
                this.txtQQ.Text = Data.ToString(row["QQ"]);
                this.txtWW.Text = Data.ToString(row["WW"]);
                this.txtEmail.Text = Data.ToString(row["EMAIL"]);

                this.txtBirth.Value = Data.ToDateTime(row["BIRTH_DATE"]);
                this.txtPoints.Text = Data.ToString(row["POINTS"]);
                this.txtDiscount.Text = Data.ToString(row["DISCOUNT_RATE"]);
                this.txtDate.Text = Data.ToString(row["LAST_SALES_DATE"]);
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}

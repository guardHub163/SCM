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
    public partial class FrmVipSearch : Form
    {
        BVipCustomer bVip = new BVipCustomer();
        private BaseUserTable usertable;
        public FrmVipSearch()
        {
            InitializeComponent();
        }
        public FrmVipSearch(BaseUserTable _usertable) 
        {
            InitializeComponent();
            usertable = _usertable;
        }

        private void FrmVipSearch_Load(object sender, EventArgs e)
        {
            VIpdgv.AutoGenerateColumns = false;
            VIpdgv.ReadOnly = true;
            this.SalesToTime.Value = DateTime.Now.AddDays(1);
            this.SalesFromTime.Value = DateTime.Now.AddDays(-30);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = bVip.GetVipInfo(getConduction());
            ds.Tables[0].Columns.Add("UPorint",Type.GetType("System.Int32"));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                row["UPorint"] = Convert.ToInt32(row["POINTS"]) - Convert.ToInt32(row["USED_POINTS"]);
            }

            this.VIpdgv.DataSource = ds.Tables[0];
        }

        private string getConduction()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("1=1");
            if (this.txtCustomer.Text != "") 
            {
                strSql.AppendFormat(" AND CODE='{0}'", this.txtCustomer.Text);
            }
            if (this.txtName.Text != "") 
            {
                strSql.AppendFormat(" AND NAME='{0}'", this.txtName.Text);
            }
            if (this.txtPoints.Text != "") 
            {
                if (Data.IsNumber(txtPoints.Text))
                {
                    strSql.AppendFormat(" AND POINTS >{0}", Convert.ToDecimal(txtPoints.Text));
                }
            }
            if (this.SalesFromTime.Text != "" && this.SalesToTime.Text != "") 
            {
                strSql.AppendFormat(" AND LAST_SALES_DATE BETWEEN '{0}' AND '{1}'", SalesFromTime.Value.ToString("yyyy/MM/dd"), SalesToTime.Value.ToString("yyyy/MM/dd"));
            }
            else if (SalesFromTime.Text != "")
            {
                strSql.AppendFormat(" AND LAST_SALES_DATE  >= '{0}' ", SalesFromTime.Value.ToString("yyyy/MM/dd"));
            }
            else if (SalesToTime.Text!= "")
            {
                strSql.AppendFormat(" AND LAST_SALES_DATE  <= '{0}' ", SalesToTime.Value.ToString("yyyy/MM/dd"));
            }
            return strSql.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddVipInfo frm = new FrmAddVipInfo(usertable,"");
            frm.ShowDialog();
            if (frm.flag == true)
            {
                btnSearch_Click(btnSearch, EventArgs.Empty);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (VIpdgv.CurrentRow != null)
            {
                string code = VIpdgv.SelectedRows[0].Cells[0].Value.ToString();
                FrmVipModity frm = new FrmVipModity(code, usertable);
                frm.ShowDialog();
                if (frm.flag == true) 
                {
                    btnSearch_Click(btnSearch, EventArgs.Empty);
                }
            }
            else 
            {
                MessageBox.Show("没有可修改的信息！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            btnSearch_Click(btnSearch, KeyEventArgs.Empty);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            btnSearch_Click(btnSearch, KeyEventArgs.Empty);
        }

        private void txtPoints_KeyDown(object sender, KeyEventArgs e)
        {
            btnSearch_Click(btnSearch, KeyEventArgs.Empty);
        }

    }
}

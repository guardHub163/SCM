using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Bll;

namespace POS
{
    public partial class FrmSalesShow : Form
    {
        private string sales_employee = "";
        private string product_code = "";
        private string sale_time = "";
        BSalesOrder salesOrder = new BSalesOrder();
        public FrmSalesShow()
        {
            InitializeComponent();
        }

        public FrmSalesShow(string _sales_employee,string _product_code,string _sale_time)
        {
            InitializeComponent();
            sales_employee = _sales_employee;
            product_code = _product_code;
            sale_time = _sale_time;
        }

        private void FrmSalesShow_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            DataSet ds = salesOrder.GetSaleOrderInfo(StrWher());
            this.dataGridView1.DataSource = ds.Tables[0];
        }

        private string StrWher()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" 2=2 ");
            if (sales_employee != "") 
            {
                str.AppendFormat(" AND SALES_EMPLOYEE='{0}'", sales_employee);
            }
            if (product_code != "") 
            {
                str.AppendFormat(" AND PRODUCT_STYLE='{0}'", product_code);
            }
            if (sale_time != "") 
            {
                str.AppendFormat(" AND CREATE_DATE_TIME BETWEEN '{0}' AND '{1}'", Convert.ToDateTime(sale_time), Convert.ToDateTime(sale_time).AddDays(1));
            }
            return str.ToString();
        }
    }
}

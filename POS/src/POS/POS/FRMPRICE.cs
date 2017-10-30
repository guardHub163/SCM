using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Bll;
using UserCache;

namespace POS
{
    public partial class FrmPrice : Form
    {
        public decimal[] productPrices = { 0, 0, 0 };
        private string styleCode, departmentCode;

        #region init
        public FrmPrice()
        {
            InitializeComponent();
        }

        public FrmPrice(string styleCode, string departmentCode)
        {
            InitializeComponent();
            this.styleCode = styleCode;
            this.departmentCode = departmentCode;
        }
        private void FrmPrice_Load(object sender, EventArgs e)
        {
            string departmentCode = Cache.DEPARTMENT_CODE;
            dgSalesPrice.AutoGenerateColumns = false;
            SetPriceByStyle(styleCode, departmentCode);
            this.Show();
            dgSalesPrice.Focus();
        }

        private void SetPriceByStyle(string styleCode, string departmentCode)
        {
            BProductPrice bProductprice = new BProductPrice();
            DataSet ds = bProductprice.getSalesPrice(styleCode, departmentCode);
            DataTable dt = ds.Tables[0];
            dt.Columns.Add("HOT_KEY", Type.GetType("System.String"));
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dr["HOT_KEY"] = i++;
            }
            dgSalesPrice.DataSource = dt;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void FrmPrice_KeyDown(object sender, KeyEventArgs e)
        {
            int hotKey = -1;
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                hotKey = 0;
            }
            else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                hotKey = 1;
            }
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                hotKey = 2;
            }
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                hotKey = 3;
            }
            else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                hotKey = 4;
            }
            else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                hotKey = 5;
            }
            else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                hotKey = 6;
            }
            else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                hotKey = 7;
            }
            else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                hotKey = 8;
            }
            else if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                hotKey = 9;
            }
            if (hotKey != -1 && hotKey < dgSalesPrice.RowCount)
            {
                setReturnData();
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                setReturnData();
                this.Close();
            }
        }        


        private void dgSalesPrice_CellMouseDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            setReturnData();
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            setReturnData();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void setReturnData()
        {
            try
            {
                DataGridViewRow row = dgSalesPrice.SelectedRows[0];
                productPrices[0] = Convert.ToDecimal(row.Cells["ORI_PRICE"].Value);
                productPrices[1] = Convert.ToDecimal(row.Cells["SALES_PRICE"].Value);
                productPrices[2] = Convert.ToDecimal(row.Cells["DISCOUNT_RATE"].Value);
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

    }//end class
}

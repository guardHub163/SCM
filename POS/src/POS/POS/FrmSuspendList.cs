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
    public partial class FrmSuspendList : Form
    {
        public string slipNumber = "";
        private BSalesOrder bSalesOrder = new BSalesOrder();
        private DataSet ds = new DataSet();

        public FrmSuspendList()
        {
            InitializeComponent();
        }

        private void FrmSuspendList_Load(object sender, EventArgs e)
        {
            ds = bSalesOrder.GetTmpSalesGroup("");

            dgView.DataSource = ds.Tables[0];
        }

        private void dgView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOK_Click(null, EventArgs.Empty);
        }

        private void dgView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, EventArgs.Empty);
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgView.Rows.Count > 0)
            {
                slipNumber = dgView.SelectedRows[0].Cells[0].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }

            this.Close();
        }
    }//end class
}

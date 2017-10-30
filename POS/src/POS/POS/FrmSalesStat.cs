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

namespace POS
{
    public partial class FrmSalesStat : Form
    {
        private BaseUserTable usertable;
        BCommon bCommon = new BCommon();
        BSalesOrder bOrder = new BSalesOrder();
        DataSet ds = new DataSet();

        public FrmSalesStat()
        {
            InitializeComponent();
        }
        public FrmSalesStat(BaseUserTable _usertable)
        {
            InitializeComponent();
            usertable = _usertable;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.ckb1.Checked == false && this.ckb2.Checked == false && this.ckb3.Checked == false)
            {
                ds = bOrder.GetAllSalesInfo(getallconduction());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = ds.Tables[0];
                    int Amount = bOrder.GetAllSalesStatAmount(getallconduction());
                    this.lblAmount.Text = Convert.ToString(Amount) + "元";
                }
                else 
                {
                    MessageBox.Show("查询的信息不存在！");
                }
            }
            else
            {
                ds = bOrder.GetSalesStatAmount(GetGroup(), getConduction());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = ds.Tables[0];
                    int Amount = bOrder.GetAllSalesStatAmount(getallconduction());
                    this.lblAmount.Text = Convert.ToString(Amount) + "元";
                }
                else 
                {
                    MessageBox.Show("查询的信息不存在！");
                }
            }

        }
        //所有金额的统计
        private string getallconduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");
            if (this.dateFromTime.Value.ToString() != "" && this.dateToTime.Value.ToString() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME BETWEEN '{0}' AND '{1}'", this.dateFromTime.Value.ToString("yyyy/MM/dd"), this.dateToTime.Value.AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (this.dateFromTime.Value.ToString() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME>'{0}'", this.dateFromTime.Value.ToString("yyyy/MM/dd"));
            }
            else if (this.dateToTime.Value.ToString() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME<'{0}'", this.dateToTime.Value.AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();

        }
        //单个金额的查询
        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");
            if (this.cmbName.SelectedValue.ToString() == "0")
            {
                sb.AppendFormat(" AND 2=2");
            }
            else
            {
                sb.AppendFormat(" AND SALES_EMPLOYEE='{0}'", this.cmbName.SelectedValue.ToString());
            }
            if (this.cmbStyle.SelectedValue.ToString() == "0")
            {
                sb.AppendFormat(" AND 3=3 ");
            }
            else
            {
                sb.AppendFormat(" AND PRODUCT_STYLE='{0}'", this.cmbStyle.SelectedValue.ToString());
            }
            if (this.dateFromTime.Value.ToString() != "" && this.dateToTime.Value.ToString() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME BETWEEN '{0}' AND '{1}'", this.dateFromTime.Value.ToString("yyyy/MM/dd"), this.dateToTime.Value.ToString("yyyy/MM/dd"));
            }
            else if (this.dateFromTime.Value.ToString() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME>'{0}'", this.dateFromTime.Value.ToString("yyyy/MM/dd"));
            }
            else if (this.dateToTime.Value.ToString() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME<'{0}'", this.dateToTime.Value.ToString("yyyy/MM/dd"));
            }
            return sb.ToString();
        }
        //统计条件
        private string GetGroup()
        {
            StringBuilder sb = new StringBuilder();
            if (this.ckb1.Checked && this.ckb2.Checked && this.ckb3.Checked)
            {
                sb.Append("SALES_EMPLOYEE,PRODUCT_STYLE,CREATE_DATE_TIME ");
            }
            else if (this.ckb1.Checked == true && this.ckb2.Checked == true)
            {
                sb.Append(" SALES_EMPLOYEE,PRODUCT_STYLE ");
            }
            else if (this.ckb1.Checked == true && this.ckb3.Checked == true)
            {
                sb.Append(" SALES_EMPLOYEE,CREATE_DATE_TIME ");
            }
            else if (this.ckb2.Checked == true && this.ckb3.Checked == true)
            {
                sb.Append(" PRODUCT_STYLE,CREATE_DATE_TIME ");
            }
            else if (this.ckb1.Checked == true)
            {
                sb.Append(" SALES_EMPLOYEE ");
            }
            else if (this.ckb2.Checked == true)
            {
                sb.Append(" PRODUCT_STYLE ");
            }
            else if (this.ckb3.Checked == true)
            {
                sb.Append(" CREATE_DATE_TIME ");
            }
            return sb.ToString();
        }

        private void FrmSalesStat_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            this.dateToTime.Value = DateTime.Now;
            BUser buser = new BUser();
            string where = " 1=1 ";
            DataTable da = buser.GetList(where).Tables[0];
            DataRow dr = da.NewRow();
            dr["USER_ID"] = "0";
            dr["TRUE_NAME"] = "全部";
            da.Rows.InsertAt(dr, 0);
            this.cmbName.DataSource = da;
            this.cmbName.ValueMember = "USER_ID";
            this.cmbName.DisplayMember = "TRUE_NAME";

            BStyle bstyle = new BStyle();
            string where1 = " 1=1 ";
            DataTable dt = bstyle.GetList(where1).Tables[0];
            DataRow row = dt.NewRow();
            row["CODE"] = "0";
            row["NAME"] = "全部";
            dt.Rows.InsertAt(row, 0);
            this.cmbStyle.DataSource = dt;
            this.cmbStyle.ValueMember = "CODE";
            this.cmbStyle.DisplayMember = "NAME";

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            string sales_employee = null;
            string product_code = null;
            string sale_time = null;
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.SelectedRows[0].Cells[0].FormattedValue.ToString()=="")
                {
                     sales_employee = "";
                }
                else
                {
                     sales_employee = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                }

                if (dataGridView1.SelectedRows[0].Cells[1].FormattedValue.ToString()=="")
                {
                     product_code = "";
                }
                else
                {
                    product_code = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }

                if (dataGridView1.SelectedRows[0].Cells[4].FormattedValue.ToString()=="")
                {
                     sale_time = "";
                }
                else
                {
                    sale_time = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

                }

                FrmSalesShow frm = new FrmSalesShow(sales_employee, product_code, sale_time);
                frm.ShowDialog();
            }
        }

    }
}

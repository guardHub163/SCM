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
using UserCache;
using System.IO;
using System.Xml;
using POS.Common;
using System.Threading;
using System.Collections;

namespace POS
{
    public partial class FrmSalesSearch : Form
    {
        BSalesOrder bSales = new BSalesOrder();
        BProduct bProduct = new BProduct();
        BCommon bCommon = new BCommon();
        DataSet ds = new DataSet();
        BSalesOrder bSalesOrder = new BSalesOrder();
        private WebServieceOperate operate = new WebServieceOperate();
        ItemList item = null;
        private string _type = "";
        private string _departMentCode = "";
        private string _userId;
        private string _oldProductCode = "";
        string NewSlipnumber = "";
        private bool flag = false;
        private decimal ori_price = 0;

        #region init
        public FrmSalesSearch()
        {
            InitializeComponent();
        }
        public FrmSalesSearch(string type)
        {
            this._type = type;
            InitializeComponent();
        }

        public FrmSalesSearch(string type, string departMentCode)
        {
            this._type = type;
            this._departMentCode = departMentCode;
            InitializeComponent();
        }

        public FrmSalesSearch(string type, string departMentCode, string userId)
        {
            this._type = type;
            this._departMentCode = departMentCode;
            this._userId = userId;
            InitializeComponent();
        }

        private void FrmSalesSearch_Load(object sender, EventArgs e)
        {
            switch (_type)
            {
                case "1":
                    this.Text = "销售查询";
                    this.panel4.Visible = false;
                    this.panel6.Visible = false;
                    this.btnReturn.Visible = false;
                    this.CancelButton = btnClose;
                    break;
                case "2":
                    this.Text = "换货操作";
                    this.btnReturn.Visible = false;
                    this.btnClose.Visible = false;
                    this.CancelButton = btnCancel;
                    break;
                case "3":
                    this.Text = "退货操作";
                    this.panel4.Visible = false;
                    this.panel6.Visible = false;
                    this.CancelButton = btnClose;
                    break;
            }

            try
            {
                ds = bCommon.GetNames("SEARCH_KEY");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cboSearchKey.Items.Add(new ItemList(Convert.ToString(row["CODE"]), Convert.ToString(row["NAME"])));
                }
                cboSearchKey.SelectedIndex = 2;
            }
            catch { }

            try
            {
                ds = bCommon.GetNames("SEARCH_DATE");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cboSearchDate.Items.Add(new ItemList(Convert.ToString(row["CODE"]), Convert.ToString(row["NAME"])));
                }
                cboSearchDate.SelectedIndex = 0;
            }
            catch { }
            dgView.AutoGenerateColumns = false;
            dgvProduct.AutoGenerateColumns = false;
            this.Show();
            this.txtSearchKey.Focus();
        }
        #endregion

        /// <summary>
        ///  查询／退货窗口关闭
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 换货窗口关闭
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要取消换货操作吗？", this.Text, MessageBoxButtons.OK) == DialogResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 检索
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Bind() == false)
            {
                MessageBox.Show("对不起，没有相关的信息", this.Text);
            }
        }

        private bool Bind()
        {
            StringBuilder sb = new StringBuilder();
            if (_type == "1")
            {
                sb.AppendFormat(" CREATE_DATE_TIME >='{0}' AND CREATE_DATE_TIME < '{1}'", txtFromDate.Value.ToString("yyyy/MM/dd"), txtToDate.Value.AddDays(1).ToString("yyyy/MM/dd"));
            }
            else
            {
                sb.AppendFormat("STATUS_FLAG={0} AND CREATE_DATE_TIME >='{1}' AND CREATE_DATE_TIME < '{2}'", Constant.SALES_PAY_STATUS_FLAG, txtFromDate.Value.ToString("yyyy/MM/dd"), txtToDate.Value.AddDays(1).ToString("yyyy/MM/dd"));
            }
            if (!string.IsNullOrEmpty(txtSearchKey.Text.Trim()))
            {
                item = (ItemList)(cboSearchKey.SelectedItem);
                switch (item.Value)
                {
                    case "1":    //小票收据号
                        sb.AppendFormat(" AND SLIP_NUMBER = '{0}'", txtSearchKey.Text.Trim());
                        break;
                    case "2":    //销售单号
                        sb.AppendFormat(" AND SLIP_NUMBER = '{0}'", txtSearchKey.Text.Trim());
                        break;
                    case "3":    //商品编号
                        sb.AppendFormat(" AND PRODUCT_CODE = '{0}%'", txtSearchKey.Text.Trim());
                        break;
                    case "4":    //商品款号
                        sb.AppendFormat(" AND STYLE_CODE = '{0}%'", txtSearchKey.Text.Trim());
                        break;
                    case "5":    //销售价格
                        sb.AppendFormat(" AND PRICE = {0}", txtSearchKey.Text.Trim());
                        break;
                    case "6":    //客户编号
                        sb.AppendFormat(" AND CUSTOMER_CODE = '{0}%'", txtSearchKey.Text.Trim());
                        break;
                    case "7":    //备注信息
                        sb.AppendFormat(" AND MEMO = '%{0}%'", txtSearchKey.Text.Trim());
                        break;
                }
            }
            ds = bSales.GetList(sb.ToString());
            if (ds.Tables[0].Rows.Count != 0)
            {
                dgView.DataSource = ds.Tables[0];
                flag = true;
            }
            else
            {
                dgView.DataSource = ds.Tables[0];
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 商品退货确认
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgView.SelectedRows.Count == 0)
            {
                return;

            }
            DataGridViewRow row = dgView.SelectedRows[0];
            string slipNumber = row.Cells["SLIP_NUMBER"].Value.ToString();
            decimal sumAmount = 0;
            decimal sumQuantity = 0;
            DataSet ds = bSales.GetList("SLIP_NUMBER= '" + slipNumber + "'");
            StringBuilder sb = new StringBuilder();
            sb.Append("你确定要退货么？ \r\n");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sb.AppendFormat("{0} * {1} * {2}\r\n", dr["PRODUCT_CODE"], dr["PRODUCT_NAME"], Convert.ToString(dr["QUANTITY"]));
                sumAmount += Convert.ToDecimal(dr["AMOUNT"]);
                sumQuantity += Convert.ToDecimal(dr["QUANTITY"]);
            }
            string[] AMOUNT = sumAmount.ToString().Split('.');
            decimal SumAmount = Convert.ToDecimal(AMOUNT[0]);
            string[] QUANTITY = sumQuantity.ToString().Split('.');
            decimal sumquantity = Convert.ToDecimal(QUANTITY[0]);
            sb.AppendFormat("{0}{1}\r\n", "数量" + sumquantity, "*总金额" + SumAmount + "元");
            if (dgView.CurrentRow != null)
            {
                List<SalesOrderTable> list = GetReturnData();
                if (MessageBox.Show(this, sb.ToString(), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (bSales.InsertSales(list, slipNumber) > 0)
                    {
                        //打开钱箱
                        POSPrinter.OpenCash(Cache.PRN_PORT);
                        Bind();
                        Thread thread = new Thread(SendSalesOrderData);
                        thread.Start();
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择退货商品！");
            }
        }

        /// <summary>
        /// 商品换货确认
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (dgView.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = dgView.SelectedRows[0];
            string slipNumber = row.Cells["SLIP_NUMBER"].Value.ToString();
            if (CheckInput())
            {
                //退货商品
                List<SalesOrderTable> ReturnDatalist = GetReturnData();

                //
                SalesOrderTable salesData = GetSalesData();
                if (MessageBox.Show(this, "换货的差价是:" + txtDiffAmount.Text + "\r\n" + "确定要换货吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (bSales.InsertSales(ReturnDatalist, salesData, slipNumber) > 0)
                    {
                        //打开钱箱
                        POSPrinter.OpenCash(Cache.PRN_PORT);
                        PrintInvoice.print(NewSlipnumber);
                        Bind();
                        Thread thread = new Thread(SendSalesOrderData);
                        thread.Start();
                        ClearNewProductInfo();
                    }
                }
                else
                {
                    MessageBox.Show("换货失败！", this.Text);
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private bool CheckInput()
        {
            if (dgView.Rows.Count == 0 || "".Equals(txtSlipNumber.Text.Trim()))
            {
                MessageBox.Show("请选择退货商品！");
                return false;
            }
            if ("".Equals(txtProductCode.Text.Trim()))
            {
                MessageBox.Show("请选择换货商品！");
                return false;
            }
            else if (!bProduct.Exists(txtProductCode.Text.Trim()))
            {
                MessageBox.Show("换货商品不存在！");
                return false;
            }

            if ("".Equals(txtQuantity.Text.Trim()))
            {
                MessageBox.Show("换货商品数量不能为空！");
                return false;
            }
            else if ("0".Equals(txtQuantity.Text.Trim()))
            {
                MessageBox.Show("换货商品数量不能为空！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 退货商品信息的取得
        /// </summary>
        /// <returns></returns>
        private List<SalesOrderTable> GetReturnData()
        {
            List<SalesOrderTable> list = new List<SalesOrderTable>();
            DataGridViewRow row = dgView.SelectedRows[0];
            string slipNumber = row.Cells["SLIP_NUMBER"].Value.ToString();
            DataSet ds = bSales.GetSalesInfo(" SLIP_NUMBER='" + slipNumber + "'");
            string SLIP_NUMBER = new BCommon().GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"));
            DateTime DATE_TIME = DateTime.Now;
            int i = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int lineNumber = Convert.ToInt32(dr["LINE_NUMBER"]);
                SalesOrderTable salesOrderTable = bSales.GetModel(slipNumber, lineNumber);
                salesOrderTable.MEMO = "销售退货(" + salesOrderTable.SLIP_NUMBER + ")";
                salesOrderTable.SLIP_NUMBER = SLIP_NUMBER;
                salesOrderTable.LINE_NUMBER = i++;
                salesOrderTable.STATUS_FLAG = Constant.SALES_ORDER_BACK_PLAN_STATUS_FLAG;
                salesOrderTable.SEND_FLAG = 0;
                //salesOrderTable.SALES_EMPLOYEE = _userId;
                salesOrderTable.CREATE_USER = _userId;
                salesOrderTable.LAST_UPDATE_USER = _userId;
                salesOrderTable.CREATE_DATE_TIME = DATE_TIME;
                salesOrderTable.LAST_UPDATE_TIME = DATE_TIME;
                salesOrderTable.QUANTITY = 0 - salesOrderTable.QUANTITY;
                salesOrderTable.AMOUNT = 0 - salesOrderTable.AMOUNT;
                salesOrderTable.CHANGE = (salesOrderTable.CASH_AMOUNT + salesOrderTable.BANK_AMOUNT - salesOrderTable.CHANGE);
                salesOrderTable.CASH_AMOUNT = 0;
                salesOrderTable.POINTS = 0 - salesOrderTable.POINTS;
                salesOrderTable.USED_POINTS = 0 - salesOrderTable.USED_POINTS;
                list.Add(salesOrderTable);
            }
            return list;
        }

        /// <summary>
        /// 销售商品信息的取得
        /// </summary>
        private SalesOrderTable GetSalesData()
        {
            DataGridViewRow row = dgView.SelectedRows[0];
            string slipNumber = row.Cells["SLIP_NUMBER"].Value.ToString();
            string lineNumber = row.Cells["LINE_NUMBER"].Value.ToString();
            SalesOrderTable salesOrderTable = bSales.GetModel(slipNumber, Convert.ToInt32(lineNumber));
            salesOrderTable.SLIP_NUMBER = new BCommon().GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"));
            NewSlipnumber = salesOrderTable.SLIP_NUMBER;
            salesOrderTable.LINE_NUMBER = 1;
            salesOrderTable.SALES_EMPLOYEE = _userId;
            salesOrderTable.PRODUCT_CODE = txtProductCode.Text.Trim();
            salesOrderTable.QUANTITY = Convert.ToDecimal(txtQuantity.Text.Trim());
            salesOrderTable.AMOUNT = Convert.ToDecimal(txtAmount.Text.Trim());
            salesOrderTable.CASH_AMOUNT = salesOrderTable.AMOUNT;
            salesOrderTable.CHANGE = 0;
            salesOrderTable.POINTS = Convert.ToInt32(salesOrderTable.AMOUNT);
            salesOrderTable.USED_POINTS = 0;
            salesOrderTable.STATUS_FLAG = Constant.SALES_PAY_STATUS_FLAG;
            salesOrderTable.PRICE = Convert.ToDecimal(txtPrice.Text);
            salesOrderTable.SEND_FLAG = 0;
            salesOrderTable.MEMO = "销售";
            salesOrderTable.CREATE_USER = _userId;
            salesOrderTable.LAST_UPDATE_USER = _userId;
            salesOrderTable.CREATE_DATE_TIME = DateTime.Now;
            salesOrderTable.ORI_PRICE = ori_price;
            salesOrderTable.DISCOUNT_RATE = ori_price - salesOrderTable.PRICE;
            salesOrderTable.LAST_UPDATE_TIME = DateTime.Now;
            return salesOrderTable;
        }

        /// <summary>
        /// 换货商品选择改变时
        /// </summary>
        private void dgView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                DataGridViewRow row = dgView.SelectedRows[0];
                if (row.Cells["SLIP_NUMBER"].Value.ToString() != txtSlipNumber.Text)
                {
                    txtSlipNumber.Text = row.Cells["SLIP_NUMBER"].Value.ToString();
                    txtSalesDate.Text = row.Cells["CREATE_DATE_TIME"].Value.ToString();
                    txtCustomerCode.Text = row.Cells["CUSTOMER_CODE"].Value.ToString();
                    txtSumAmount.Text = bSales.GetSumAmount(txtSlipNumber.Text).ToString();
                    DataSet ds = bSales.GetList("SLIP_NUMBER= '" + txtSlipNumber.Text + "'");

                    dgvProduct.DataSource = ds.Tables[0];
                }


            }
            catch { }
        }

        /// <summary>
        /// 商品查询
        /// </summary>
        private void btnProduct_Click(object sender, EventArgs e)
        {
            FrmProduct frm = new FrmProduct();
            frm.ShowDialog();
            if (frm.retProductCode != "")
            {
                SetProduct(frm.retProductCode);
            }
        }

        /// <summary>
        /// 商品不再是当前控件时验证
        /// </summary>
        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Trim() != "")
            {
                if (!_oldProductCode.Equals(txtProductCode.Text.Trim()))
                {
                    BaseProductTable pTable = bProduct.GetModel(txtProductCode.Text.Trim());
                    if (pTable == null)
                    {
                        MessageBox.Show("请输入正确的商品编号！", this.Text);
                        ClearNewProductInfo();
                    }
                    else
                    {
                        SetProduct(txtProductCode.Text.Trim());
                    }
                }
            }
            else
            {
                ClearNewProductInfo();
            }
        }

        /// <summary>
        /// 清空换货商品信息
        /// </summary>
        private void ClearNewProductInfo()
        {
            txtProductCode.Text = "";
            txtProductName.Text = "";
            txtSize.Text = "";
            txtStyle.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
            txtAmount.Text = "";
            txtDiffAmount.Text = "";
            _oldProductCode = "";
        }

        /// <summary>
        /// 设置新商品的详细信息
        /// </summary>
        private void SetProduct(string productCode)
        {
            if (productCode == "" || !bProduct.Exists(productCode))
            {
                FrmProduct productFrm = new FrmProduct(productCode);
                productFrm.ShowDialog();
                productCode = productFrm.retProductCode;
            }
            if (productCode == "")
            {
                return;
            }
            BaseProductTable pTable = bProduct.GetModel(productCode);
            FrmPrice priceForm = new FrmPrice(pTable.STYLE, _departMentCode);
            priceForm.ShowDialog();
            decimal[] productPrices = priceForm.productPrices;
            if (priceForm.DialogResult != DialogResult.OK)
            {
                ClearNewProductInfo();
                return;
            }
            txtProductCode.Text = pTable.CODE;
            txtProductName.Text = pTable.NAME;
            txtSize.Text = pTable.SIZE_NAME;
            txtStyle.Text = pTable.STYLE_NAME;
            txtPrice.Text = Convert.ToString(productPrices[1]);
            txtAmount.Text = Convert.ToString(productPrices[1]);
            txtQuantity.Text = Convert.ToString(1);
            ori_price = productPrices[0];
            _oldProductCode = pTable.CODE;
            txtDiffAmount.Text = Convert.ToString(Convert.ToDecimal(txtAmount.Text.Trim()) - Convert.ToDecimal(txtSumAmount.Text.Trim()));
            txtQuantity.Focus();

        }

        /// <summary>
        /// 输入数量CHECK
        /// </summary>
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_keyDown(object sender, KeyEventArgs e)
        {
            //回车键
            if (e.KeyCode == Keys.Enter)
            {
                if ("txtProductCode".Equals(((Control)sender).Name))
                {
                    SetProduct(txtProductCode.Text.Trim());
                }
                else if ("txtQuantity".Equals(((Control)sender).Name))
                {
                    btnChange_Click(null, EventArgs.Empty);
                    if (txtQuantity.Text != "")
                    {
                        SendKeys.Send("{Tab}");
                    }
                }
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if ("".Equals(txtQuantity.Text.Trim()))
            {
                txtAmount.Text = "";
                txtDiffAmount.Text = "";
                return;
            }
            if ("".Equals(txtProductCode.Text.Trim()))
            {
                MessageBox.Show("请选择换货商品！");
                txtQuantity.Text = "";
                return;
            }
            if (!"".Equals(txtPrice.Text.Trim()))
            {
                txtAmount.Text = Math.Round(Convert.ToDecimal(txtQuantity.Text.Trim()) * Convert.ToDecimal(txtPrice.Text.Trim()), 2).ToString();
            }
            if (!"".Equals(txtSlipNumber.Text.Trim()))
            {
                txtDiffAmount.Text = Math.Round(Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(txtSumAmount.Text.Trim()), 2).ToString();
            }
            else
            {
                txtDiffAmount.Text = txtAmount.Text;
            }
        }

        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            btnSearch_Click(btnSearch, KeyEventArgs.Empty);
        }

        #region 将销售信息传回scm
        private void SendSalesOrderData()
        {
            string[] names = { "SALES" };
            Hashtable ht = operate.SendDate(names);
        }

        #endregion

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            if (dgView.Rows.Count > 0 && Convert.ToDecimal(dgView.SelectedRows[0].Cells["QUANTITY"].Value.ToString()) > 0)
            {
                PrintInvoice.print(dgView.SelectedRows[0].Cells["SLIP_NUMBER"].Value.ToString());
            }
            else
            {
                MessageBox.Show("请选中要打印的信息！");
            }
        }

        private void cboSearchDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSearchDate.SelectedIndex == 0)
            {
                this.txtFromDate.Value = DateTime.Now;
                this.txtToDate.Value = DateTime.Now;
            }
            else if (cboSearchDate.SelectedIndex == 1)
            {
                this.txtFromDate.Value = DateTime.Now.AddDays(-1);
                this.txtToDate.Value = DateTime.Now.AddDays(-1);
            }
            else if (cboSearchDate.SelectedIndex == 2)
            {
                ////this.txtFromDate.Value = DateTime.Now.AddDays(-7);
                int weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);
                int daydiff = (-1) * weeknow + 1;
                int dayadd = 7 - weeknow;
                this.txtFromDate.Value = DateTime.Now.AddDays(daydiff);
                this.txtToDate.Value = DateTime.Now.AddDays(dayadd);
            }
            else if (cboSearchDate.SelectedIndex == 3)
            {
                this.txtFromDate.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                this.txtToDate.Value = DateTime.Now;
            }
        }







    }//end class
}

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
using UserCache;
using POS.Common;
using System.IO;
using System.Xml;
using FastReport;
using System.Threading;

namespace POS
{
    public partial class FrmSales : Form
    {
        private float X;
        private float Y;
        private BaseProductTable baseProduct;
        private BVipCustomer bCustomerTable = new BVipCustomer();
        private BSalesOrder bSalesOrder = new BSalesOrder();
        private BSalesOrderPlan bSalesOrderPlan = new BSalesOrderPlan();
        private BProduct bProduct = new BProduct();
        private DataSet ds = new DataSet();
        private BCommon bCommon = new BCommon();
        private Hashtable gvHt = new Hashtable();
        private string currentUserId = "";
        private string slipNumber = "";
        private SalesOrderPlanTable SalesOrderPlan = new SalesOrderPlanTable();
        private WebServieceOperate operate = new WebServieceOperate();
        public int points = 0;
        public BaseUserTable _tuser;

        #region init　页面初始化
        public FrmSales()
        {
            InitializeComponent();
        }

        public FrmSales(BaseUserTable _usertable)
        {
            _tuser = _usertable;
            InitializeComponent();
        }

        private void FrmSales_Load(object sender, EventArgs e)
        {
            X = 794;//获取窗体的宽度
            Y = 120;//获取窗体的高度
            setTag(this.panel1);//调用方法
            float newx = (this.panel1.Width) / X; //窗体宽度缩放比例
            float newy = this.panel1.Height / Y;//窗体高度缩放比例
            setControls(newx, newy, this.panel1);//随窗体改变控件大小
            InitDataGridView();
            gvSales.AutoGenerateColumns = false;
            txtProductCode.Focus();
            //顾显还原
            POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_CLEAR, "");
        }


        //获取控件的width、height、left、top、字体大小的值
        //存放在控件的Tag属性中
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
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private DataTable GetInitDataTable()
        {
            DataTable salesDt = new DataTable();
            //商品编号
            salesDt.Columns.Add("PRODUCT_CODE", Type.GetType("System.String"));
            //商品名称
            salesDt.Columns.Add("PRODUCT_NAME", Type.GetType("System.String"));
            //款号
            salesDt.Columns.Add("STYLE_NAME", Type.GetType("System.String"));
            //颜色
            salesDt.Columns.Add("COLOR_NAME", Type.GetType("System.String"));
            //尺码
            salesDt.Columns.Add("SIZE", Type.GetType("System.String"));
            //原价
            salesDt.Columns.Add("ORI_PRICE", Type.GetType("System.Decimal"));
            //折扣
            salesDt.Columns.Add("DISCOUNT_RATE", Type.GetType("System.Decimal"));
            //单价
            salesDt.Columns.Add("PRICE", Type.GetType("System.Decimal"));
            //数量
            salesDt.Columns.Add("QUANTITY", Type.GetType("System.Decimal"));
            //使用积分
            salesDt.Columns.Add("USED_POINTS", Type.GetType("System.Int32"));
            //金额
            salesDt.Columns.Add("AMOUNT", Type.GetType("System.Decimal"));
            //备注
            salesDt.Columns.Add("MEMO", Type.GetType("System.String"));

            return salesDt;
        }

        /// <summary>
        /// DataGridView 生成
        /// </summary>
        private void InitDataGridView()
        {
            BUser buser = new BUser();
            string where = " 1=1 ";
            DataTable userTable = buser.GetList(where).Tables[0];
            int i = 0;
            foreach (DataRow row in userTable.Rows)
            {
                gvHt.Add(row["USER_ID"], GetInitDataTable());
                Button btn = GetUserBut(2 + 81 * i++, 2, Convert.ToString(row["USER_ID"]), Convert.ToString(row["TRUE_NAME"]));
                panelButtons.Controls.Add(btn);
                if (i == 1)
                {
                    currentUserId = Convert.ToString(row["USER_ID"]);
                    BindData();
                }
            }
        }

        /// <summary>
        /// 创建销售人员按钮
        /// </summary>
        private Button GetUserBut(int x, int y, string name, string text)
        {
            Button btn = new Button();
            btn.BackgroundImage = global::POS.Properties.Resources.button_1;
            btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btn.Dock = System.Windows.Forms.DockStyle.Left;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = "btn" + name;
            btn.Tag = name;
            btn.Size = new System.Drawing.Size(80, 25);
            btn.TabIndex = 0;
            btn.Text = text;
            btn.UseVisualStyleBackColor = true;
            btn.ForeColor = Color.White;
            btn.Font = new Font("宋体", 9, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Click += new System.EventHandler(this.Btn_User_Click);
            return btn;
        }
        #endregion

        #region 页面KeyDown
        /// <summary>
        /// 页面KeyDown
        /// </summary>
        private void FrmSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.txtCustomerCode.Focus();
            }
            //F2 商品
            else if (e.KeyCode == Keys.F2)
            {
                this.txtProductCode.Focus();
            }
            //F3 修改
            else if (e.KeyCode == Keys.F3)
            {
                btnModify_Click(this, EventArgs.Empty);
            }
            //F4 删除
            else if (e.KeyCode == Keys.F4)
            {
                btnDelete_Click(this, EventArgs.Empty);
            }
            //F5 结算
            else if (e.KeyCode == Keys.F5)
            {
                payment();
            }
            //F6 清空
            else if (e.KeyCode == Keys.F6)
            {
                btnClear_Click(this, EventArgs.Empty);
            }
            //F7 挂单
            else if (e.KeyCode == Keys.F7)
            {
                btnSuspend_Click(this, EventArgs.Empty);
            }
            //F8 取单
            else if (e.KeyCode == Keys.F8)
            {
                btnExtract_Click(this, EventArgs.Empty);
            }
            //F9 退货
            else if (e.KeyCode == Keys.F9)
            {
                btnReturn_Click(this, EventArgs.Empty);
            }
            //F10 换货
            else if (e.KeyCode == Keys.F10)
            {
                btnChange_Click(this, EventArgs.Empty);
            }
            //F11 查询
            else if (e.KeyCode == Keys.F11)
            {
                btnSearch_Click(this, EventArgs.Empty);
            }
            //F12预定
            else if (e.KeyCode == Keys.F12)
            {
                PlanPay();
            }
        }

        /// <summary>
        /// 页面文本框KEYDOWN事件
        /// </summary>
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            //回车键
            if (e.KeyCode == Keys.Enter)
            {
                if (sender is TextBox)
                {
                    TextBox txt = (TextBox)sender;
                    if (txt.Name == "txtProductCode")
                    {
                        string productCode = txtProductCode.Text.Trim();
                        if (!productCode.Equals(""))
                        {
                            //商品增加
                            addProduct(productCode);
                        }
                        else
                        {
                            //结算
                            payment();
                        }
                    }
                    else if (txt.Name == "txtCustomerCode")
                    {
                        SetCustomerInfo();
                    }
                }
                else if (sender is DataGridView)
                {
                    //结算
                    payment();
                }
            }
        }
        #endregion

        #region 销售明细行增加
        /// <summary>
        /// 销售明细行增加
        /// </summary>
        private void addProduct(string productCode)
        {
            if (productCode == "" || !bProduct.Exists(productCode))
            {
                FrmProduct productFrm = new FrmProduct(productCode);
                productFrm.ShowDialog();
                productCode = productFrm.retProductCode;
                txtProductCode.Text = productCode;
            }
            if (productCode == "")
            {
                return;
            }

            baseProduct = bProduct.GetModel(productCode);

            FrmPrice priceForm = new FrmPrice(baseProduct.STYLE, _tuser.DEPARTMENT_CODE);
            priceForm.ShowDialog();
            decimal[] productPrices = priceForm.productPrices;
            if (priceForm.DialogResult != DialogResult.OK)
            {
                txtProductCode.Text = "";
                return;
            }

            bool isExists = false;
            foreach (DataRow row in ((DataTable)gvHt[currentUserId]).Rows)
            {
                if (productCode.Equals(row["PRODUCT_CODE"]) && productPrices[1].Equals(row["PRICE"]))
                {
                    //数量
                    row["QUANTITY"] = Convert.ToDecimal(row["QUANTITY"]) + 1;
                    //金额
                    row["AMOUNT"] = Convert.ToDecimal(row["AMOUNT"]) + productPrices[1];

                    isExists = true;
                }
            }

            if (!isExists)
            {
                DataRow dr = ((DataTable)gvHt[currentUserId]).NewRow();
                //商品编号
                dr["PRODUCT_CODE"] = baseProduct.CODE;
                //商品名称
                dr["PRODUCT_NAME"] = baseProduct.NAME;
                //款号
                dr["STYLE_NAME"] = baseProduct.STYLE_NAME;
                //颜色
                dr["COLOR_NAME"] = baseProduct.COLOR_NAME;
                //尺码
                dr["SIZE"] = baseProduct.SIZE;
                //原价
                dr["ORI_PRICE"] = productPrices[0];
                //折扣
                dr["DISCOUNT_RATE"] = productPrices[2];
                //单价
                dr["PRICE"] = productPrices[1];
                //数量
                dr["QUANTITY"] = 1;
                //使用积分
                dr["USED_POINTS"] = 0;
                //金额
                dr["AMOUNT"] = productPrices[1];
                //备注
                dr["MEMO"] = "销售";
                ((DataTable)gvHt[currentUserId]).Rows.Add(dr);
            }
            POSPrinter.ShowCustomerScreen(Cache.SCREEN_PORT, Constant.SCREEN_TYPE_PRICE, Convert.ToString(productPrices[1]));
            setTotalAmount();
            txtProductCode.Text = "";
            txtProductCode.Focus();
        }
        #endregion

        #region 销售明细行修改
        /// <summary>
        /// 销售明细修正
        /// </summary>
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (((DataTable)gvHt[currentUserId]).Rows.Count > 0)
            {
                SalesOrderTable salesOrder = getBllSalesOrderTable();
                FrmSalesModify modifyForm = new FrmSalesModify(_tuser);
                modifyForm.salesOrder = salesOrder;
                modifyForm.ShowDialog();
                if (modifyForm.DialogResult == DialogResult.OK)
                {
                    modifySalesDetail(modifyForm.salesOrder);
                    setTotalAmount();
                }
                modifyForm.Dispose();
            }

        }

        /// <summary>
        /// 获得当前选中的销售明细
        /// </summary>
        public SalesOrderTable getBllSalesOrderTable()
        {
            SalesOrderTable salesOrderDetail = new SalesOrderTable();
            DataRow dr = ((DataTable)gvHt[currentUserId]).Rows[gvSales.CurrentRow.Index];

            //商品编号
            salesOrderDetail.PRODUCT_CODE = Convert.ToString(dr["PRODUCT_CODE"]);
            //商品名称
            salesOrderDetail.NAME = Convert.ToString(dr["PRODUCT_NAME"]);
            //款号
            salesOrderDetail.STYLE_NAME = Convert.ToString(dr["STYLE_NAME"]);
            //颜色
            salesOrderDetail.COLOR_NAME = Convert.ToString(dr["COLOR_NAME"]);
            //尺码
            salesOrderDetail.SIZE = Convert.ToString(dr["SIZE"]);
            //原价
            salesOrderDetail.ORI_PRICE = Convert.ToDecimal(dr["ORI_PRICE"]);
            //折扣
            salesOrderDetail.DISCOUNT_RATE = Convert.ToDecimal(dr["DISCOUNT_RATE"]);
            //单价
            salesOrderDetail.PRICE = Convert.ToDecimal(dr["PRICE"]);
            //数量
            salesOrderDetail.QUANTITY = Convert.ToDecimal(dr["QUANTITY"]);
            //使用积分
            salesOrderDetail.USED_POINTS = Convert.ToInt32(dr["USED_POINTS"]);
            //金额
            salesOrderDetail.AMOUNT = Convert.ToDecimal(dr["AMOUNT"]);
            //备注
            salesOrderDetail.MEMO = Convert.ToString(dr["MEMO"]);

            return salesOrderDetail;
        }

        /// <summary>
        /// 明细行修改
        /// </summary>
        public void modifySalesDetail(SalesOrderTable salesOrderDetail)
        {
            // 数量 单价 金额  备注的修改
            DataRow dr = ((DataTable)gvHt[currentUserId]).Rows[gvSales.CurrentRow.Index];
            dr.BeginEdit();
            dr["QUANTITY"] = salesOrderDetail.QUANTITY;
            dr["ORI_PRICE"] = salesOrderDetail.ORI_PRICE;
            dr["DISCOUNT_RATE"] = salesOrderDetail.DISCOUNT_RATE;
            dr["PRICE"] = salesOrderDetail.PRICE;
            dr["AMOUNT"] = salesOrderDetail.AMOUNT;
            dr["MEMO"] = salesOrderDetail.MEMO;
            dr["USED_POINTS"] = salesOrderDetail.USED_POINTS;
            dr.EndEdit();
            gvSales.Focus();
        }
        #endregion

        #region 销售明细行删除
        /// <summary>
        /// 销售明细行删除
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            result = MessageBox.Show(this, "是否确认删除此行数据！", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes && gvSales.CurrentRow != null && gvSales.CurrentRow.Index >= 0)
            {
                ((DataTable)gvHt[currentUserId]).Rows[this.gvSales.CurrentRow.Index].Delete();
            }
            setTotalAmount();
            gvSales.Focus();

        }
        #endregion

        #region 销售明细清空
        /// <summary>
        /// 销售明细清空
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "是否清空所有数据！", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                ((DataTable)gvHt[currentUserId]).Clear();
                setTotalAmount();
            }
        }
        #endregion

        #region 总的销售信息显示
        /// <summary>
        /// 总的销售信息显示
        /// </summary>
        private void setTotalAmount()
        {
            decimal[] total = getTotal();
            this.lblAmounts.Text = "售:" + Math.Floor(total[0]);
            this.lblQty.Text = "数量:" + Math.Floor(total[2]) + " 原价:" + Math.Floor(total[1]);
        }
        #endregion

        #region 销售结算
        /// <summary>
        /// 结算
        /// </summary>
        private void btnPay_Click(object sender, EventArgs e)
        {
            payment();
        }

        /// <summary>
        /// 销售结算
        /// </summary>
        private int payment()
        {
            if (((DataTable)gvHt[currentUserId]).Rows.Count > 0)
            {
                FrmPay payFrm = new FrmPay(getTotal(), (DataTable)gvHt[currentUserId], txtCustomerCode.Text, currentUserId, _tuser, points);
                DialogResult result = payFrm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Thread thread = new Thread(SendSalesOrderData);
                    thread.Start();
                    ((DataTable)gvHt[currentUserId]).Clear();
                    setTotalAmount();
                    //清空客记显示区
                    ClearCustomerInfo();
                }
            }
            else
            {
                MessageBox.Show("没有可结算的商品！", this.Text);
            }
            return 1;
        }
        #endregion

        /// <summary>
        /// 商品选择
        /// </summary>
        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            addProduct("");
        }

        /// <summary>
        /// 退货
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            FrmSalesSearch searchFrm = new FrmSalesSearch("3", _tuser.DEPARTMENT_CODE, _tuser.USER_ID);
            searchFrm.ShowDialog();
        }

        /// <summary>
        /// 换货
        /// </summary>
        private void btnChange_Click(object sender, EventArgs e)
        {
            FrmSalesSearch searchFrm = new FrmSalesSearch("2", _tuser.DEPARTMENT_CODE, _tuser.USER_ID);
            searchFrm.ShowDialog();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmSalesSearch searchFrm = new FrmSalesSearch("1", _tuser.DEPARTMENT_CODE, _tuser.USER_ID);
            searchFrm.ShowDialog();
        }

        #region 客户处理

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            SetCustomerInfo();
        }

        /// <summary>
        /// 客户显示
        /// </summary>
        public void SetCustomerInfo()
        {
            BVipCustomer bll = new BVipCustomer();
            string customerCode = txtCustomerCode.Text.Trim();
            if (customerCode != "")
            {
                if (!bll.Exists(customerCode))
                {
                    DialogResult result = MessageBox.Show(this, "客户不存在,是否新建客户?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        FrmAddVipInfo frm = new FrmAddVipInfo(_tuser, txtCustomerCode.Text.Trim());
                        if (frm.ShowDialog() != DialogResult.OK)
                        {
                            customerCode = "";
                        }
                    }
                    else
                    {
                        customerCode = "";
                    }
                }
                if (customerCode != "")
                {
                    BaseVipCustomerTable btable = bll.GetModel(this.txtCustomerCode.Text);
                    lblCustomerName.Text = btable.NAME;
                    lblPorint.Text = (btable.POINTS - btable.USED_POINTS).ToString();
                    points =Convert.ToInt32( lblPorint.Text);
                }
                txtCustomerCode.Text = customerCode;
            }
            else
            {
                ClearCustomerInfo();
            }
        }

        /// <summary>
        /// 客户信息的清空
        /// </summary>
        private void ClearCustomerInfo()
        {
            txtCustomerCode.Text = "";
            lblCustomerName.Text = "";
            lblPorint.Text = "";
        }
        #endregion

        #region 挂单
        /// <summary>
        /// 挂单
        /// </summary>
        private void btnSuspend_Click(object sender, EventArgs e)
        {
            if (((DataTable)gvHt[currentUserId]).Rows.Count > 0)
            {
                List<TmpSalesOrderTable> list = GetTmpSalesOrderList(((DataTable)gvHt[currentUserId]));
                if (bSalesOrder.InsertTmpSalesData(list) > 0)
                {
                    //清空显示区域
                    ((DataTable)gvHt[currentUserId]).Clear();
                    //总金额的重新计算
                    setTotalAmount();
                    //清空客记显示区
                    ClearCustomerInfo();

                }
            }
        }

        /// <summary>
        /// 销售记录的取得
        /// </summary>
        private List<TmpSalesOrderTable> GetTmpSalesOrderList(DataTable salesOrderDt)
        {
            List<TmpSalesOrderTable> list = new List<TmpSalesOrderTable>();

            //顾客编号
            string slipNumber = DateTime.Now.ToString("yyyyMMddHHmmss");
            //明细号
            int lineNumber = 1;

            //更新时间 
            DateTime updateTime = DateTime.Now;

            foreach (DataRow row in salesOrderDt.Rows)
            {
                TmpSalesOrderTable tmpSalesOrderTable = new TmpSalesOrderTable();
                //销售单号
                tmpSalesOrderTable.SLIP_NUMBER = slipNumber;
                //明细号
                tmpSalesOrderTable.LINE_NUMBER = lineNumber++;
                //销售人员
                tmpSalesOrderTable.SALES_EMPLOYEE = currentUserId;
                //顾客编号
                tmpSalesOrderTable.CUSTOMER_CODE = this.txtCustomerCode.Text.Trim();
                //商品
                tmpSalesOrderTable.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                //商品名称
                tmpSalesOrderTable.PRODUCT_NAME = Convert.ToString(row["PRODUCT_NAME"]);
                //款号
                tmpSalesOrderTable.STYLE_NAME = Convert.ToString(row["STYLE_NAME"]);
                //颜色
                tmpSalesOrderTable.COLOR_NAME = Convert.ToString(row["COLOR_NAME"]);
                //尺码
                tmpSalesOrderTable.SIZE = Convert.ToString(row["SIZE"]);
                //原价
                tmpSalesOrderTable.ORI_PRICE = Convert.ToDecimal(row["ORI_PRICE"]);
                //积分
                tmpSalesOrderTable.USED_POINTS = Convert.ToDecimal(row["USED_POINTS"]);
                //折扣
                tmpSalesOrderTable.DISCOUNT_RATE = Convert.ToDecimal(row["DISCOUNT_RATE"]);
                //售价
                tmpSalesOrderTable.PRICE = Convert.ToDecimal(row["PRICE"]);
                //数量
                tmpSalesOrderTable.QUANTITY = Convert.ToDecimal(row["QUANTITY"]);
                //金额
                tmpSalesOrderTable.AMOUNT = Convert.ToDecimal(row["AMOUNT"]);
                //备考
                tmpSalesOrderTable.MEMO = Convert.ToString(row["MEMO"]);
                //备考2　挂单备注信息
                tmpSalesOrderTable.MEMO2 = "";
                //创建时间
                tmpSalesOrderTable.CREATE_DATE_TIME = updateTime;


                list.Add(tmpSalesOrderTable);
            }

            return list;
        }
        #endregion


        #region 取单
        /// <summary>
        /// 取单
        /// </summary>
        private void btnExtract_Click(object sender, EventArgs e)
        {


            FrmSuspendList suspendFrm = new FrmSuspendList();
            suspendFrm.ShowDialog();
            string slipNumber = suspendFrm.slipNumber;
            suspendFrm.Dispose();
            if (gvSales.Rows.Count <= 0)
            {
                if (slipNumber != "")
                {
                    ds = bSalesOrder.GetTmpSalesData(slipNumber);
                    //客户信息显示
                    txtCustomerCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["CUSTOMER_CODE"]);
                    SetCustomerInfo();
                    //销售信息显示
                    currentUserId = Convert.ToString(ds.Tables[0].Rows[0]["SALES_EMPLOYEE"]);
                    gvHt[currentUserId] = ds.Tables[0];
                    BindData();
                }
            }
            else
            {
                string errorMessage = "你尚有订单未处理！是否继续取单并且覆盖？";
                DialogResult result = MessageBox.Show(this, errorMessage, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (slipNumber != "")
                    {
                        ds = bSalesOrder.GetTmpSalesData(slipNumber);
                        //客户信息显示
                        txtCustomerCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["CUSTOMER_CODE"]);
                        SetCustomerInfo();
                        //销售信息显示
                        currentUserId = Convert.ToString(ds.Tables[0].Rows[0]["SALES_EMPLOYEE"]);
                        gvHt[currentUserId] = ds.Tables[0];
                        BindData();
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void SetUserButton()
        {
            foreach (Control c in panelButtons.Controls)
            {
                if (currentUserId.Equals(c.Tag))
                {
                    c.BackgroundImage = global::POS.Properties.Resources.button_1_selected;
                    c.ForeColor = Color.Blue;
                }
                else
                {
                    c.BackgroundImage = global::POS.Properties.Resources.button_1;
                    c.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region 销售预定
        /// <summary>
        /// 销售预定
        /// </summary>
        private void btnSalesPlan_Click(object sender, EventArgs e)
        {
            PlanPay();
        }

        private SalesOrderPlanTable GetSalesOrderPlanTable(DataTable dt)
        {

            decimal[] total = getTotal();
            SalesOrderPlanTable salesPlanTable = new SalesOrderPlanTable();
            salesPlanTable.CUSTOMER_CODE = txtCustomerCode.Text.Trim();
            salesPlanTable.SALES_EMPLOYEE = currentUserId;
            salesPlanTable.CREATE_USER = _tuser.USER_ID;
            salesPlanTable.AMOUNT = total[0];
            salesPlanTable.ORI_AMOUNT = total[1];
            salesPlanTable.QUANTITY = total[2];

            return salesPlanTable;
        }


        /// <summary>
        /// 销售预定
        /// </summary>
        private int PlanPay()
        {
            if (((DataTable)gvHt[currentUserId]).Rows.Count > 0)
            {
                DataTable dt = (DataTable)gvHt[currentUserId];
                GetSalesOrderPlanTable(dt);
                FrmPlanPay frm = new FrmPlanPay(GetSalesOrderPlanTable(dt), 0, 0, _tuser, "", "");
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {

                    if (bSalesOrderPlan.InsertSales(GegSalesPlanList((DataTable)gvHt[currentUserId]), frm.salesPlanTable, frm.bankAmount, frm.cashAmount, frm.customer_code, frm.customer_phone) > 0)
                    {
                        ////打开钱箱
                        POSPrinter.OpenCash(Cache.PRN_PORT);
                        MessageBox.Show("预定成功！", this.Text);
                        Thread thread = new Thread(SendSalesOrderData);
                        thread.Start();
                        ((DataTable)gvHt[currentUserId]).Clear();
                        setTotalAmount();
                        //清空客记显示区
                        ClearCustomerInfo();

                    }
                }
            }
            else
            {
                MessageBox.Show("没有可预定的商品！", this.Text);
            }
            return 1;
        }

        private List<SalesOrderPlanTable> GegSalesPlanList(DataTable salesDt)
        {
            List<SalesOrderPlanTable> salesList = new List<SalesOrderPlanTable>();

            //门店代号
            string departmentCode = Cache.DEPARTMENT_CODE;
            //单号
            string slipPlanNumber = bCommon.GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER_PLAN"));
            //销售单号
            string slipNumber = bCommon.GetSeqNumber(Cache.GetBllStyleName("BLL_SALES_ORDER"));
            //营业员
            string salesEmployeeCode = currentUserId;
            //顾客编号
            string customerCode = this.txtCustomerCode.Text.Trim();
            //明细号
            int lineNumber = 1;
            //更新时间 
            DateTime updateTime = DateTime.Now;

            foreach (DataRow row in salesDt.Rows)
            {
                SalesOrderPlanTable salesOrderPlanTable = new SalesOrderPlanTable();
                //单号
                salesOrderPlanTable.SLIP_NUMBER = slipPlanNumber;
                //部门
                salesOrderPlanTable.DEPARTMENT_CODE = departmentCode;
                //销售人员
                salesOrderPlanTable.SALES_EMPLOYEE = salesEmployeeCode;
                //销售单号
                salesOrderPlanTable.SALES_ORDER_SLIP_NUMBER = slipNumber;
                //顾客编号
                salesOrderPlanTable.CUSTOMER_CODE = SalesOrderPlan.CUSTOMER_CODE;
                //顾客联系方式
                salesOrderPlanTable.CUSTOMER_PHONE = SalesOrderPlan.CUSTOMER_PHONE;
                //明细号
                salesOrderPlanTable.LINE_NUMBER = lineNumber++;
                //有效日期
                salesOrderPlanTable.END_DATE_TIME = Convert.ToDateTime(SalesOrderPlan.END_DATE_TIME);
                //商品
                salesOrderPlanTable.PRODUCT_CODE = Convert.ToString(row["PRODUCT_CODE"]);
                //原价
                salesOrderPlanTable.ORI_PRICE = Convert.ToDecimal(row["ORI_PRICE"]);
                //折扣
                salesOrderPlanTable.DISCOUNT_RATE = Convert.ToDecimal(row["DISCOUNT_RATE"]);
                //售价
                salesOrderPlanTable.PRICE = Convert.ToDecimal(row["PRICE"]);
                //数量
                salesOrderPlanTable.QUANTITY = Convert.ToDecimal(row["QUANTITY"]);
                //单位
                salesOrderPlanTable.UNIT_CODE = "01";
                //金额
                salesOrderPlanTable.AMOUNT = Convert.ToDecimal(Convert.ToDecimal(row["QUANTITY"]) * Convert.ToDecimal(row["PRICE"]));
                //状态
                salesOrderPlanTable.STATUS_FLAG = Constant.SALES_ORDER_PLAN_STATUS_FLAG;
                //集成状态
                salesOrderPlanTable.SEND_FLAG = Constant.INIT;
                //定金
                salesOrderPlanTable.DEPOSIT = Convert.ToDecimal(SalesOrderPlan.DEPOSIT);
                //余额
                salesOrderPlanTable.BALANCE = Convert.ToDecimal(row["ORI_PRICE"]);
                //备考
                salesOrderPlanTable.MEMO = Convert.ToString(SalesOrderPlan.MEMO);
                //创建时间
                salesOrderPlanTable.CREATE_DATE_TIME = updateTime;
                //创建者
                salesOrderPlanTable.CREATE_USER = _tuser.USER_ID;
                //更新时间
                salesOrderPlanTable.LAST_UPDATE_TIME = updateTime;
                //更新者
                salesOrderPlanTable.LAST_UPDATE_USER = _tuser.USER_ID;

                salesList.Add(salesOrderPlanTable);
            }

            return salesList;
        }

        #endregion


        private void Btn_User_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            currentUserId = btn.Tag.ToString();
            BindData();

        }

        private void BindData()
        {
            SetUserButton();
            gvSales.DataSource = (DataTable)gvHt[currentUserId];
            setTotalAmount();
        }


        #region 返回总金额，原价金额，总数量
        /// <summary>
        /// 返回总金额，原价金额，总数量
        /// </summary>
        /// <returns></returns>
        public decimal[] getTotal()
        {
            decimal[] total = new decimal[] { 0, 0, 0, 0 };

            foreach (DataRow dr in ((DataTable)gvHt[currentUserId]).Rows)
            {
                //金额
                total[0] = total[0] + Convert.ToDecimal(dr["AMOUNT"]);
                //原价金额
                total[1] = total[1] + Convert.ToDecimal(dr["ORI_PRICE"]) * Convert.ToDecimal(dr["QUANTITY"]);
                //总数量
                total[2] = total[2] + Convert.ToDecimal(dr["QUANTITY"]);
                //总使用积分
                total[3] = total[3] + Convert.ToInt32(dr["USED_POINTS"]);
            }

            return total;
        }
        #endregion

        #region 将销售信息传回scm
        private void SendSalesOrderData()
        {
            string[] names = { "SALES" };
            Hashtable ht = operate.SendDate(names);
        }
        #endregion

        private void lblCustomerName_Click(object sender, EventArgs e)
        {
            FrmVipShow frm = new FrmVipShow(this.txtCustomerCode.Text);
            frm.ShowDialog();
        }



    }//end class


}

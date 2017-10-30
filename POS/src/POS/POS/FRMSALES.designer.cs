namespace POS
{
    partial class FrmSales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSales));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPorint = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAmounts = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.btnSearchProduct = new System.Windows.Forms.PictureBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSalesPlan = new System.Windows.Forms.Button();
            this.btnExtract = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelGV = new System.Windows.Forms.Panel();
            this.gvSales = new POS.MyDataGrid();
            this.ColProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStyle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOriPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USED_POINTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchProduct)).BeginInit();
            this.panelGV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSales)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lblPorint);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblCustomerName);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txtCustomerCode);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnSalesPlan);
            this.panel1.Controls.Add(this.btnExtract);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnChange);
            this.panel1.Controls.Add(this.btnSuspend);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnModify);
            this.panel1.Controls.Add(this.btnPay);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 452);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 120);
            this.panel1.TabIndex = 1;
            // 
            // lblPorint
            // 
            this.lblPorint.AutoSize = true;
            this.lblPorint.ForeColor = System.Drawing.Color.White;
            this.lblPorint.Location = new System.Drawing.Point(133, 102);
            this.lblPorint.Name = "lblPorint";
            this.lblPorint.Size = new System.Drawing.Size(0, 12);
            this.lblPorint.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "F1-会员输入, F2-商品输入";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCustomerName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCustomerName.ForeColor = System.Drawing.Color.White;
            this.lblCustomerName.Location = new System.Drawing.Point(73, 102);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(54, 12);
            this.lblCustomerName.TabIndex = 4;
            this.lblCustomerName.Text = "       ";
            this.lblCustomerName.Click += new System.EventHandler(this.lblCustomerName_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel4.Controls.Add(this.lblAmounts);
            this.panel4.Controls.Add(this.lblQty);
            this.panel4.Location = new System.Drawing.Point(529, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(260, 100);
            this.panel4.TabIndex = 17;
            // 
            // lblAmounts
            // 
            this.lblAmounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.lblAmounts.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold);
            this.lblAmounts.ForeColor = System.Drawing.Color.White;
            this.lblAmounts.Location = new System.Drawing.Point(3, 4);
            this.lblAmounts.Name = "lblAmounts";
            this.lblAmounts.Size = new System.Drawing.Size(254, 55);
            this.lblAmounts.TabIndex = 6;
            this.lblAmounts.Text = "售";
            this.lblAmounts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblQty
            // 
            this.lblQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.lblQty.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblQty.ForeColor = System.Drawing.Color.White;
            this.lblQty.Location = new System.Drawing.Point(3, 62);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(254, 34);
            this.lblQty.TabIndex = 8;
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtProductCode);
            this.panel3.Controls.Add(this.btnSearchProduct);
            this.panel3.Location = new System.Drawing.Point(5, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(172, 30);
            this.panel3.TabIndex = 16;
            // 
            // txtProductCode
            // 
            this.txtProductCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProductCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductCode.Location = new System.Drawing.Point(0, 4);
            this.txtProductCode.MaxLength = 20;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(138, 19);
            this.txtProductCode.TabIndex = 1;
            this.txtProductCode.WordWrap = false;
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // btnSearchProduct
            // 
            this.btnSearchProduct.BackColor = System.Drawing.Color.White;
            this.btnSearchProduct.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchProduct.Image = global::POS.Properties.Resources.search;
            this.btnSearchProduct.Location = new System.Drawing.Point(144, 0);
            this.btnSearchProduct.Name = "btnSearchProduct";
            this.btnSearchProduct.Size = new System.Drawing.Size(26, 28);
            this.btnSearchProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSearchProduct.TabIndex = 1;
            this.btnSearchProduct.TabStop = false;
            this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerCode.Location = new System.Drawing.Point(75, 69);
            this.txtCustomerCode.MaxLength = 11;
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(100, 21);
            this.txtCustomerCode.TabIndex = 0;
            this.txtCustomerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtCustomerCode.Leave += new System.EventHandler(this.txtCustomerCode_Leave);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(444, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 32);
            this.button2.TabIndex = 15;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(444, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 32);
            this.button1.TabIndex = 15;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSalesPlan
            // 
            this.btnSalesPlan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSalesPlan.BackgroundImage")));
            this.btnSalesPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalesPlan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalesPlan.FlatAppearance.BorderSize = 0;
            this.btnSalesPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalesPlan.ForeColor = System.Drawing.Color.White;
            this.btnSalesPlan.Location = new System.Drawing.Point(444, 6);
            this.btnSalesPlan.Name = "btnSalesPlan";
            this.btnSalesPlan.Size = new System.Drawing.Size(80, 32);
            this.btnSalesPlan.TabIndex = 15;
            this.btnSalesPlan.Text = "F12-预定";
            this.btnSalesPlan.UseVisualStyleBackColor = true;
            this.btnSalesPlan.Click += new System.EventHandler(this.btnSalesPlan_Click);
            // 
            // btnExtract
            // 
            this.btnExtract.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExtract.BackgroundImage")));
            this.btnExtract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExtract.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExtract.FlatAppearance.BorderSize = 0;
            this.btnExtract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtract.ForeColor = System.Drawing.Color.White;
            this.btnExtract.Location = new System.Drawing.Point(274, 82);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(80, 32);
            this.btnExtract.TabIndex = 15;
            this.btnExtract.Text = "F8-取单";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(359, 82);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 32);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "F11-查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "会员编号:";
            // 
            // btnChange
            // 
            this.btnChange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChange.BackgroundImage")));
            this.btnChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChange.FlatAppearance.BorderSize = 0;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChange.ForeColor = System.Drawing.Color.White;
            this.btnChange.Location = new System.Drawing.Point(359, 44);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(80, 32);
            this.btnChange.TabIndex = 14;
            this.btnChange.Text = "F10-换货";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnSuspend
            // 
            this.btnSuspend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSuspend.BackgroundImage")));
            this.btnSuspend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSuspend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuspend.FlatAppearance.BorderSize = 0;
            this.btnSuspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuspend.ForeColor = System.Drawing.Color.White;
            this.btnSuspend.Location = new System.Drawing.Point(274, 44);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(80, 32);
            this.btnSuspend.TabIndex = 12;
            this.btnSuspend.Text = "F7-挂起";
            this.btnSuspend.UseVisualStyleBackColor = true;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReturn.BackgroundImage")));
            this.btnReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(359, 6);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 32);
            this.btnReturn.TabIndex = 13;
            this.btnReturn.Text = "F9-退货";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(274, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 32);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "F6-清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(189, 44);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 32);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "F4-删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModify.ForeColor = System.Drawing.Color.White;
            this.btnModify.Location = new System.Drawing.Point(189, 6);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(80, 32);
            this.btnModify.TabIndex = 10;
            this.btnModify.Text = "F3-修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.btnPay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPay.BackgroundImage")));
            this.btnPay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(189, 82);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(80, 32);
            this.btnPay.TabIndex = 2;
            this.btnPay.Text = "F5-结算";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(2, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "会员昵称:";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(794, 25);
            this.panelButtons.TabIndex = 12;
            // 
            // panelGV
            // 
            this.panelGV.Controls.Add(this.gvSales);
            this.panelGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGV.Location = new System.Drawing.Point(0, 25);
            this.panelGV.Name = "panelGV";
            this.panelGV.Size = new System.Drawing.Size(794, 427);
            this.panelGV.TabIndex = 13;
            // 
            // gvSales
            // 
            this.gvSales.AllowUserToAddRows = false;
            this.gvSales.AllowUserToDeleteRows = false;
            this.gvSales.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.gvSales.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSales.BackgroundColor = System.Drawing.Color.White;
            this.gvSales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gvSales.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvSales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSales.ColumnHeadersHeight = 25;
            this.gvSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColProductCode,
            this.ColProductName,
            this.ColStyle,
            this.ColColor,
            this.ColSize,
            this.colOriPrice,
            this.colDiscount,
            this.ColPrice,
            this.ColQuantity,
            this.USED_POINTS,
            this.colAmount,
            this.Column1});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(167)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSales.DefaultCellStyle = dataGridViewCellStyle5;
            this.gvSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvSales.EnableHeadersVisualStyles = false;
            this.gvSales.GridColor = System.Drawing.Color.PaleGreen;
            this.gvSales.Location = new System.Drawing.Point(0, 0);
            this.gvSales.Name = "gvSales";
            this.gvSales.ReadOnly = true;
            this.gvSales.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gvSales.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.gvSales.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gvSales.RowTemplate.Height = 23;
            this.gvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSales.Size = new System.Drawing.Size(794, 427);
            this.gvSales.TabIndex = 4;
            this.gvSales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // ColProductCode
            // 
            this.ColProductCode.DataPropertyName = "PRODUCT_CODE";
            this.ColProductCode.FillWeight = 9.769924F;
            this.ColProductCode.HeaderText = "商品编号";
            this.ColProductCode.Name = "ColProductCode";
            this.ColProductCode.ReadOnly = true;
            // 
            // ColProductName
            // 
            this.ColProductName.DataPropertyName = "PRODUCT_NAME";
            this.ColProductName.FillWeight = 7.164611F;
            this.ColProductName.HeaderText = "品名";
            this.ColProductName.Name = "ColProductName";
            this.ColProductName.ReadOnly = true;
            // 
            // ColStyle
            // 
            this.ColStyle.DataPropertyName = "STYLE_NAME";
            this.ColStyle.FillWeight = 5.210627F;
            this.ColStyle.HeaderText = "款号";
            this.ColStyle.Name = "ColStyle";
            this.ColStyle.ReadOnly = true;
            // 
            // ColColor
            // 
            this.ColColor.DataPropertyName = "COLOR_NAME";
            this.ColColor.FillWeight = 3.90797F;
            this.ColColor.HeaderText = "色";
            this.ColColor.Name = "ColColor";
            this.ColColor.ReadOnly = true;
            // 
            // ColSize
            // 
            this.ColSize.DataPropertyName = "SIZE";
            this.ColSize.FillWeight = 3.90797F;
            this.ColSize.HeaderText = "码";
            this.ColSize.Name = "ColSize";
            this.ColSize.ReadOnly = true;
            // 
            // colOriPrice
            // 
            this.colOriPrice.DataPropertyName = "ORI_PRICE";
            this.colOriPrice.FillWeight = 5.210627F;
            this.colOriPrice.HeaderText = "原价";
            this.colOriPrice.Name = "colOriPrice";
            this.colOriPrice.ReadOnly = true;
            // 
            // colDiscount
            // 
            this.colDiscount.DataPropertyName = "DISCOUNT_RATE";
            this.colDiscount.FillWeight = 3.90797F;
            this.colDiscount.HeaderText = "折扣";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            // 
            // ColPrice
            // 
            this.ColPrice.DataPropertyName = "PRICE";
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.ColPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColPrice.FillWeight = 5.210627F;
            this.ColPrice.HeaderText = "单价";
            this.ColPrice.Name = "ColPrice";
            this.ColPrice.ReadOnly = true;
            // 
            // ColQuantity
            // 
            this.ColQuantity.DataPropertyName = "QUANTITY";
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.ColQuantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColQuantity.FillWeight = 5.210627F;
            this.ColQuantity.HeaderText = "数量";
            this.ColQuantity.Name = "ColQuantity";
            this.ColQuantity.ReadOnly = true;
            // 
            // USED_POINTS
            // 
            this.USED_POINTS.DataPropertyName = "USED_POINTS";
            this.USED_POINTS.FillWeight = 10F;
            this.USED_POINTS.HeaderText = "使用积分";
            this.USED_POINTS.MaxInputLength = 4;
            this.USED_POINTS.Name = "USED_POINTS";
            this.USED_POINTS.ReadOnly = true;
            this.USED_POINTS.Visible = false;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "AMOUNT";
            this.colAmount.FillWeight = 9.118598F;
            this.colAmount.HeaderText = "金额";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MEMO";
            this.Column1.HeaderText = "明细";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // FrmSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.panelGV);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmSales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "销售";
            this.Load += new System.EventHandler(this.FrmSales_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSales_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearchProduct)).EndInit();
            this.panelGV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSales)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Label lblAmounts;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox btnSearchProduct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelGV;
        internal MyDataGrid gvSales;
        private System.Windows.Forms.Button btnSalesPlan;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStyle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn USED_POINTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label lblPorint;

    }
}
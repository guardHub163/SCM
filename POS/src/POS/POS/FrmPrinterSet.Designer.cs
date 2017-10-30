namespace POS
{
    partial class FrmPrinterSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrinterSet));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTestScreen = new System.Windows.Forms.Button();
            this.btnTestPrint = new System.Windows.Forms.Button();
            this.cboScreenPort = new System.Windows.Forms.ComboBox();
            this.cboPrintPort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelPerView = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWWW = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.printNumber = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.printNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 25);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "打印机/顾显端口配置";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.btnTestScreen);
            this.panel2.Controls.Add(this.btnTestPrint);
            this.panel2.Controls.Add(this.cboScreenPort);
            this.panel2.Controls.Add(this.cboPrintPort);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 71);
            this.panel2.TabIndex = 1;
            // 
            // btnTestScreen
            // 
            this.btnTestScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTestScreen.BackgroundImage")));
            this.btnTestScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTestScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestScreen.FlatAppearance.BorderSize = 0;
            this.btnTestScreen.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnTestScreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTestScreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTestScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestScreen.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestScreen.ForeColor = System.Drawing.Color.Black;
            this.btnTestScreen.Location = new System.Drawing.Point(219, 40);
            this.btnTestScreen.Name = "btnTestScreen";
            this.btnTestScreen.Size = new System.Drawing.Size(80, 25);
            this.btnTestScreen.TabIndex = 4;
            this.btnTestScreen.Text = "测试(&S)";
            this.btnTestScreen.UseVisualStyleBackColor = true;
            this.btnTestScreen.Click += new System.EventHandler(this.btnTestScreen_Click);
            // 
            // btnTestPrint
            // 
            this.btnTestPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTestPrint.BackgroundImage")));
            this.btnTestPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTestPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestPrint.FlatAppearance.BorderSize = 0;
            this.btnTestPrint.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnTestPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTestPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTestPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestPrint.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestPrint.ForeColor = System.Drawing.Color.Black;
            this.btnTestPrint.Location = new System.Drawing.Point(219, 9);
            this.btnTestPrint.Name = "btnTestPrint";
            this.btnTestPrint.Size = new System.Drawing.Size(80, 25);
            this.btnTestPrint.TabIndex = 2;
            this.btnTestPrint.Text = "测试(&P)";
            this.btnTestPrint.UseVisualStyleBackColor = true;
            this.btnTestPrint.Click += new System.EventHandler(this.btnTestPrint_Click);
            // 
            // cboScreenPort
            // 
            this.cboScreenPort.BackColor = System.Drawing.SystemColors.Info;
            this.cboScreenPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScreenPort.Font = new System.Drawing.Font("宋体", 10F);
            this.cboScreenPort.FormattingEnabled = true;
            this.cboScreenPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cboScreenPort.Location = new System.Drawing.Point(103, 42);
            this.cboScreenPort.Name = "cboScreenPort";
            this.cboScreenPort.Size = new System.Drawing.Size(110, 21);
            this.cboScreenPort.TabIndex = 3;
            // 
            // cboPrintPort
            // 
            this.cboPrintPort.BackColor = System.Drawing.SystemColors.Info;
            this.cboPrintPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrintPort.Font = new System.Drawing.Font("宋体", 10F);
            this.cboPrintPort.FormattingEnabled = true;
            this.cboPrintPort.Items.AddRange(new object[] {
            "LPT1",
            "LPT2",
            "LPT3",
            "LPT4",
            "LPT5",
            "LPT6"});
            this.cboPrintPort.Location = new System.Drawing.Point(103, 11);
            this.cboPrintPort.Name = "cboPrintPort";
            this.cboPrintPort.Size = new System.Drawing.Size(110, 21);
            this.cboPrintPort.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(14, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "顾显端口:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(14, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "打印机端口:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 25);
            this.panel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "打印小票设定";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.Location = new System.Drawing.Point(14, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "品牌设定:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelPerView);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 121);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(584, 306);
            this.panel4.TabIndex = 4;
            // 
            // panelPerView
            // 
            this.panelPerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPerView.Location = new System.Drawing.Point(337, 0);
            this.panelPerView.Name = "panelPerView";
            this.panelPerView.Size = new System.Drawing.Size(247, 306);
            this.panelPerView.TabIndex = 13;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.panel8.Controls.Add(this.printNumber);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.btnView);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.txtMemo);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.txtWWW);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.txtTel);
            this.panel8.Controls.Add(this.txtTitle);
            this.panel8.Controls.Add(this.txtAddress);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(337, 306);
            this.panel8.TabIndex = 7;
            // 
            // btnView
            // 
            this.btnView.BackgroundImage = global::POS.Properties.Resources.button_4;
            this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnView.ForeColor = System.Drawing.Color.Black;
            this.btnView.Location = new System.Drawing.Point(241, 240);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(80, 25);
            this.btnView.TabIndex = 10;
            this.btnView.Text = "预览(&I)";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label6.Location = new System.Drawing.Point(14, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "联系地址:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label7.Location = new System.Drawing.Point(14, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "联系电话:";
            // 
            // txtMemo
            // 
            this.txtMemo.BackColor = System.Drawing.SystemColors.Info;
            this.txtMemo.Font = new System.Drawing.Font("宋体", 10F);
            this.txtMemo.Location = new System.Drawing.Point(103, 167);
            this.txtMemo.MaxLength = 25;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(218, 23);
            this.txtMemo.TabIndex = 9;
            this.txtMemo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label8.Location = new System.Drawing.Point(14, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "网店或网址:";
            // 
            // txtWWW
            // 
            this.txtWWW.BackColor = System.Drawing.SystemColors.Info;
            this.txtWWW.Font = new System.Drawing.Font("宋体", 10F);
            this.txtWWW.Location = new System.Drawing.Point(103, 128);
            this.txtWWW.MaxLength = 25;
            this.txtWWW.Name = "txtWWW";
            this.txtWWW.Size = new System.Drawing.Size(218, 23);
            this.txtWWW.TabIndex = 8;
            this.txtWWW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label9.Location = new System.Drawing.Point(14, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "其他:";
            // 
            // txtTel
            // 
            this.txtTel.BackColor = System.Drawing.SystemColors.Info;
            this.txtTel.Font = new System.Drawing.Font("宋体", 10F);
            this.txtTel.Location = new System.Drawing.Point(103, 89);
            this.txtTel.MaxLength = 25;
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(218, 23);
            this.txtTel.TabIndex = 7;
            this.txtTel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Info;
            this.txtTitle.Font = new System.Drawing.Font("宋体", 10F);
            this.txtTitle.Location = new System.Drawing.Point(103, 11);
            this.txtTitle.MaxLength = 25;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(218, 23);
            this.txtTitle.TabIndex = 5;
            this.txtTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.Info;
            this.txtAddress.Font = new System.Drawing.Font("宋体", 10F);
            this.txtAddress.Location = new System.Drawing.Point(103, 50);
            this.txtAddress.MaxLength = 25;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(218, 23);
            this.txtAddress.TabIndex = 6;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 427);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(584, 35);
            this.panel5.TabIndex = 5;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Controls.Add(this.btnOK);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(412, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(172, 35);
            this.panel6.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(87, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消(&ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(3, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定(&OK)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "打印份数：";
            // 
            // printNumber
            // 
            this.printNumber.Location = new System.Drawing.Point(103, 203);
            this.printNumber.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.printNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.printNumber.Name = "printNumber";
            this.printNumber.Size = new System.Drawing.Size(218, 21);
            this.printNumber.TabIndex = 12;
            this.printNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FrmPrinterSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrinterSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印机设定";
            this.Load += new System.EventHandler(this.FrmPrinterSet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.printNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboScreenPort;
        private System.Windows.Forms.ComboBox cboPrintPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtWWW;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelPerView;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnTestScreen;
        private System.Windows.Forms.Button btnTestPrint;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown printNumber;

    }
}
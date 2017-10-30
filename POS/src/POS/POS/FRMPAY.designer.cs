namespace POS
{
    partial class FrmPay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPay));
            this.label1 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPayAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPayType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOriAmount = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtPayBankAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateoldTime = new System.Windows.Forms.DateTimePicker();
            this.chkoldSlipnumber = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPointType = new System.Windows.Forms.ComboBox();
            this.cmbPromotion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(21, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "应付款";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtAmount.Enabled = false;
            this.txtAmount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtAmount.Location = new System.Drawing.Point(107, 80);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(114, 26);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(24, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "收   款（现金）  Pay";
            // 
            // txtPayAmount
            // 
            this.txtPayAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtPayAmount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPayAmount.Location = new System.Drawing.Point(24, 87);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.Size = new System.Drawing.Size(188, 26);
            this.txtPayAmount.TabIndex = 1;
            this.txtPayAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPayAmount.TextChanged += new System.EventHandler(this.txtDecimal_TextChanged);
            this.txtPayAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtPayAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(24, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "找  零  Change";
            // 
            // txtCharge
            // 
            this.txtCharge.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCharge.Enabled = false;
            this.txtCharge.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCharge.Location = new System.Drawing.Point(24, 252);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.Size = new System.Drawing.Size(188, 26);
            this.txtCharge.TabIndex = 5;
            this.txtCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(19, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "收款方式";
            // 
            // cmbPayType
            // 
            this.cmbPayType.BackColor = System.Drawing.SystemColors.Info;
            this.cmbPayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayType.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.Location = new System.Drawing.Point(107, 17);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Size = new System.Drawing.Size(114, 24);
            this.cmbPayType.TabIndex = 0;
            this.cmbPayType.SelectedValueChanged += new System.EventHandler(this.cmbPayType_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(21, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "原价";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(21, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 26);
            this.label6.TabIndex = 9;
            this.label6.Text = "优惠";
            // 
            // txtOriAmount
            // 
            this.txtOriAmount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtOriAmount.Enabled = false;
            this.txtOriAmount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtOriAmount.Location = new System.Drawing.Point(107, 141);
            this.txtOriAmount.Name = "txtOriAmount";
            this.txtOriAmount.Size = new System.Drawing.Size(114, 26);
            this.txtOriAmount.TabIndex = 10;
            this.txtOriAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDiscount
            // 
            this.txtDiscount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDiscount.Enabled = false;
            this.txtDiscount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtDiscount.Location = new System.Drawing.Point(107, 194);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(114, 26);
            this.txtDiscount.TabIndex = 11;
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPayBankAmount
            // 
            this.txtPayBankAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtPayBankAmount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPayBankAmount.Location = new System.Drawing.Point(24, 173);
            this.txtPayBankAmount.Name = "txtPayBankAmount";
            this.txtPayBankAmount.Size = new System.Drawing.Size(188, 26);
            this.txtPayBankAmount.TabIndex = 2;
            this.txtPayBankAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPayBankAmount.TextChanged += new System.EventHandler(this.txtDecimal_TextChanged);
            this.txtPayBankAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtPayBankAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(24, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 26);
            this.label8.TabIndex = 16;
            this.label8.Text = "收   款（银行）  Pay";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label9.Location = new System.Drawing.Point(21, 249);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 26);
            this.label9.TabIndex = 9;
            this.label9.Text = "抵扣积分";
            // 
            // txtPoints
            // 
            this.txtPoints.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPoints.Enabled = false;
            this.txtPoints.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtPoints.Location = new System.Drawing.Point(107, 296);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(114, 26);
            this.txtPoints.TabIndex = 11;
            this.txtPoints.Text = "0";
            this.txtPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPoints.TextChanged += new System.EventHandler(this.txtPoints_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(47, 309);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确认(&OK)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(132, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateoldTime);
            this.panel1.Controls.Add(this.chkoldSlipnumber);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 40);
            this.panel1.TabIndex = 17;
            // 
            // dateoldTime
            // 
            this.dateoldTime.Location = new System.Drawing.Point(107, 11);
            this.dateoldTime.Name = "dateoldTime";
            this.dateoldTime.Size = new System.Drawing.Size(132, 21);
            this.dateoldTime.TabIndex = 1;
            // 
            // chkoldSlipnumber
            // 
            this.chkoldSlipnumber.AutoSize = true;
            this.chkoldSlipnumber.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold);
            this.chkoldSlipnumber.ForeColor = System.Drawing.Color.White;
            this.chkoldSlipnumber.Location = new System.Drawing.Point(22, 7);
            this.chkoldSlipnumber.Name = "chkoldSlipnumber";
            this.chkoldSlipnumber.Size = new System.Drawing.Size(69, 30);
            this.chkoldSlipnumber.TabIndex = 0;
            this.chkoldSlipnumber.Text = "补单";
            this.chkoldSlipnumber.UseVisualStyleBackColor = true;
            this.chkoldSlipnumber.CheckedChanged += new System.EventHandler(this.chkoldSlipnumber_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtAmount);
            this.splitContainer1.Panel1.Controls.Add(this.cmbPointType);
            this.splitContainer1.Panel1.Controls.Add(this.cmbPromotion);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.txtPoints);
            this.splitContainer1.Panel1.Controls.Add(this.txtOriAmount);
            this.splitContainer1.Panel1.Controls.Add(this.txtDiscount);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txtPayAmount);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.cmbPayType);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.txtCharge);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.txtPayBankAmount);
            this.splitContainer1.Size = new System.Drawing.Size(488, 350);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(21, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 26);
            this.label7.TabIndex = 0;
            this.label7.Text = "促销方式";
            // 
            // cmbPointType
            // 
            this.cmbPointType.BackColor = System.Drawing.SystemColors.Info;
            this.cmbPointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointType.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbPointType.FormattingEnabled = true;
            this.cmbPointType.Location = new System.Drawing.Point(107, 249);
            this.cmbPointType.Name = "cmbPointType";
            this.cmbPointType.Size = new System.Drawing.Size(114, 24);
            this.cmbPointType.TabIndex = 0;
            this.cmbPointType.SelectedIndexChanged += new System.EventHandler(this.cmbPointType_SelectedIndexChanged);
            // 
            // cmbPromotion
            // 
            this.cmbPromotion.BackColor = System.Drawing.SystemColors.Info;
            this.cmbPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromotion.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbPromotion.FormattingEnabled = true;
            this.cmbPromotion.Location = new System.Drawing.Point(107, 28);
            this.cmbPromotion.Name = "cmbPromotion";
            this.cmbPromotion.Size = new System.Drawing.Size(114, 24);
            this.cmbPromotion.TabIndex = 0;
            this.cmbPromotion.SelectedIndexChanged += new System.EventHandler(this.cmbPromotion_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label10.Location = new System.Drawing.Point(21, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 26);
            this.label10.TabIndex = 9;
            this.label10.Text = "积分";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(488, 2);
            this.panel2.TabIndex = 19;
            // 
            // FrmPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(488, 390);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账单结算";
            this.Load += new System.EventHandler(this.FrmPay_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPayAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCharge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPayType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOriAmount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtPayBankAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateoldTime;
        private System.Windows.Forms.CheckBox chkoldSlipnumber;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPromotion;
        private System.Windows.Forms.ComboBox cmbPointType;
        private System.Windows.Forms.Label label10;
    }
}
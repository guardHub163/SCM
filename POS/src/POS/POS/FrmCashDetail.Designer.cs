namespace POS
{
    partial class FrmCashDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCashDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBalanceCash = new System.Windows.Forms.TextBox();
            this.rdoSet = new System.Windows.Forms.RadioButton();
            this.rdoGet = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSurplus = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cboBack = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "钱箱资金:";
            // 
            // txtBalanceCash
            // 
            this.txtBalanceCash.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBalanceCash.Enabled = false;
            this.txtBalanceCash.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtBalanceCash.ForeColor = System.Drawing.Color.Black;
            this.txtBalanceCash.Location = new System.Drawing.Point(103, 25);
            this.txtBalanceCash.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBalanceCash.Name = "txtBalanceCash";
            this.txtBalanceCash.Size = new System.Drawing.Size(228, 26);
            this.txtBalanceCash.TabIndex = 2;
            this.txtBalanceCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rdoSet
            // 
            this.rdoSet.AutoSize = true;
            this.rdoSet.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.rdoSet.ForeColor = System.Drawing.Color.White;
            this.rdoSet.Location = new System.Drawing.Point(185, 85);
            this.rdoSet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdoSet.Name = "rdoSet";
            this.rdoSet.Size = new System.Drawing.Size(60, 20);
            this.rdoSet.TabIndex = 0;
            this.rdoSet.Text = "存款";
            this.rdoSet.UseVisualStyleBackColor = true;
            this.rdoSet.CheckedChanged += new System.EventHandler(this.Rdo_Changed);
            // 
            // rdoGet
            // 
            this.rdoGet.AutoSize = true;
            this.rdoGet.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.rdoGet.ForeColor = System.Drawing.Color.White;
            this.rdoGet.Location = new System.Drawing.Point(103, 85);
            this.rdoGet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdoGet.Name = "rdoGet";
            this.rdoGet.Size = new System.Drawing.Size(60, 20);
            this.rdoGet.TabIndex = 0;
            this.rdoGet.Text = "取款";
            this.rdoGet.UseVisualStyleBackColor = true;
            this.rdoGet.CheckedChanged += new System.EventHandler(this.Rdo_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "操作选择:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "存取金额:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "剩余金额:";
            // 
            // txtSurplus
            // 
            this.txtSurplus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSurplus.Enabled = false;
            this.txtSurplus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtSurplus.ForeColor = System.Drawing.Color.Black;
            this.txtSurplus.Location = new System.Drawing.Point(103, 193);
            this.txtSurplus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSurplus.Name = "txtSurplus";
            this.txtSurplus.Size = new System.Drawing.Size(228, 26);
            this.txtSurplus.TabIndex = 2;
            this.txtSurplus.Text = "0";
            this.txtSurplus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtAmount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(103, 136);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(228, 26);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "0";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "备注:";
            // 
            // txtMemo
            // 
            this.txtMemo.BackColor = System.Drawing.SystemColors.Info;
            this.txtMemo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtMemo.ForeColor = System.Drawing.Color.Black;
            this.txtMemo.Location = new System.Drawing.Point(103, 307);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(228, 26);
            this.txtMemo.TabIndex = 4;
            this.txtMemo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(103, 353);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确认(&OK)";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(206, 353);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消(&ESC)";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "存入银行:";
            // 
            // cboBack
            // 
            this.cboBack.BackColor = System.Drawing.SystemColors.Info;
            this.cboBack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBack.Font = new System.Drawing.Font("宋体", 12F);
            this.cboBack.FormattingEnabled = true;
            this.cboBack.Location = new System.Drawing.Point(103, 249);
            this.cboBack.Name = "cboBack";
            this.cboBack.Size = new System.Drawing.Size(228, 24);
            this.cboBack.TabIndex = 3;
            // 
            // FrmCashDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(355, 402);
            this.Controls.Add(this.cboBack);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSurplus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rdoGet);
            this.Controls.Add(this.rdoSet);
            this.Controls.Add(this.txtBalanceCash);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCashDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "钱箱操作";
            this.Load += new System.EventHandler(this.FrmCashDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBalanceCash;
        private System.Windows.Forms.RadioButton rdoSet;
        private System.Windows.Forms.RadioButton rdoGet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSurplus;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboBack;
    }
}
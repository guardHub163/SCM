namespace POS
{
    partial class FrmCash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCash));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblBalanceCash = new System.Windows.Forms.Label();
            this.lblProfitCash = new System.Windows.Forms.Label();
            this.lblLastCash = new System.Windows.Forms.Label();
            this.btnBankSlipNumber = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCash = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cashGridView = new POS.MyDataGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cashGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(694, 25);
            this.panel1.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(338, 14);
            this.label11.TabIndex = 1;
            this.label11.Text = "钱箱管理--商品销售的现金部分，均累积到钱箱中";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(0, 66);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(683, 261);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel2.Controls.Add(this.lblBalanceCash);
            this.panel2.Controls.Add(this.lblProfitCash);
            this.panel2.Controls.Add(this.lblLastCash);
            this.panel2.Controls.Add(this.btnBankSlipNumber);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnCash);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 392);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(694, 80);
            this.panel2.TabIndex = 1;
            // 
            // lblBalanceCash
            // 
            this.lblBalanceCash.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBalanceCash.ForeColor = System.Drawing.Color.White;
            this.lblBalanceCash.Location = new System.Drawing.Point(305, 41);
            this.lblBalanceCash.Name = "lblBalanceCash";
            this.lblBalanceCash.Size = new System.Drawing.Size(140, 33);
            this.lblBalanceCash.TabIndex = 16;
            this.lblBalanceCash.Text = "0";
            this.lblBalanceCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProfitCash
            // 
            this.lblProfitCash.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProfitCash.ForeColor = System.Drawing.Color.White;
            this.lblProfitCash.Location = new System.Drawing.Point(5, 41);
            this.lblProfitCash.Name = "lblProfitCash";
            this.lblProfitCash.Size = new System.Drawing.Size(140, 33);
            this.lblProfitCash.TabIndex = 16;
            this.lblProfitCash.Text = "0";
            this.lblProfitCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastCash
            // 
            this.lblLastCash.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLastCash.ForeColor = System.Drawing.Color.White;
            this.lblLastCash.Location = new System.Drawing.Point(155, 41);
            this.lblLastCash.Name = "lblLastCash";
            this.lblLastCash.Size = new System.Drawing.Size(140, 33);
            this.lblLastCash.TabIndex = 16;
            this.lblLastCash.Text = "0";
            this.lblLastCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBankSlipNumber
            // 
            this.btnBankSlipNumber.BackColor = System.Drawing.Color.Transparent;
            this.btnBankSlipNumber.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBankSlipNumber.BackgroundImage")));
            this.btnBankSlipNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBankSlipNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBankSlipNumber.FlatAppearance.BorderSize = 0;
            this.btnBankSlipNumber.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnBankSlipNumber.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBankSlipNumber.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBankSlipNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBankSlipNumber.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBankSlipNumber.ForeColor = System.Drawing.Color.White;
            this.btnBankSlipNumber.Location = new System.Drawing.Point(580, 41);
            this.btnBankSlipNumber.Name = "btnBankSlipNumber";
            this.btnBankSlipNumber.Size = new System.Drawing.Size(80, 35);
            this.btnBankSlipNumber.TabIndex = 21;
            this.btnBankSlipNumber.Text = "F3-存款流水号输入";
            this.btnBankSlipNumber.UseVisualStyleBackColor = false;
            this.btnBankSlipNumber.Click += new System.EventHandler(this.btnBankSlipNumber_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(150, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(2, 80);
            this.label7.TabIndex = 13;
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.Color.Transparent;
            this.btnCash.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCash.BackgroundImage")));
            this.btnCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash.FlatAppearance.BorderSize = 0;
            this.btnCash.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCash.ForeColor = System.Drawing.Color.White;
            this.btnCash.Location = new System.Drawing.Point(478, 41);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(80, 35);
            this.btnCash.TabIndex = 21;
            this.btnCash.Text = "F2-打开钱箱";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(450, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(2, 80);
            this.label2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(300, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 80);
            this.label1.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel5.BackgroundImage = global::POS.Properties.Resources.button_3;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(694, 35);
            this.panel5.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(452, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 22);
            this.label6.TabIndex = 17;
            this.label6.Text = "操作";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(305, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 22);
            this.label5.TabIndex = 16;
            this.label5.Text = "钱箱资金总额";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(155, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 22);
            this.label4.TabIndex = 1;
            this.label4.Text = "上次留存金额";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(5, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "本次收益现金";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.cashGridView);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(694, 367);
            this.panel4.TabIndex = 2;
            // 
            // cashGridView
            // 
            this.cashGridView.AllowUserToAddRows = false;
            this.cashGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.cashGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.cashGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cashGridView.BackgroundColor = System.Drawing.Color.White;
            this.cashGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cashGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.cashGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.cashGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(167)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cashGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.cashGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cashGridView.EnableHeadersVisualStyles = false;
            this.cashGridView.GridColor = System.Drawing.Color.PaleGreen;
            this.cashGridView.Location = new System.Drawing.Point(0, 0);
            this.cashGridView.MultiSelect = false;
            this.cashGridView.Name = "cashGridView";
            this.cashGridView.ReadOnly = true;
            this.cashGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cashGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.cashGridView.RowHeadersVisible = false;
            this.cashGridView.RowTemplate.Height = 23;
            this.cashGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cashGridView.Size = new System.Drawing.Size(692, 365);
            this.cashGridView.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CASH_DATE";
            this.Column1.HeaderText = "开始时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "LAST_UPDATE_TIME";
            this.Column8.FillWeight = 75F;
            this.Column8.HeaderText = "存取时间";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "BANK_NAME";
            this.Column9.HeaderText = "存款银行";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "BANK_SLIP_NUMBER";
            this.Column10.FillWeight = 150F;
            this.Column10.HeaderText = "存款流水号";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PROFIT_CASH";
            this.Column2.FillWeight = 75F;
            this.Column2.HeaderText = "本次收益";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "LAST_CASH";
            this.Column3.FillWeight = 75F;
            this.Column3.HeaderText = "上次留存";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "TAKE_CASH";
            this.Column4.FillWeight = 75F;
            this.Column4.HeaderText = "存取资金";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "BALANCE_CASH";
            this.Column5.FillWeight = 75F;
            this.Column5.HeaderText = "剩余资金";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "SALES_SLIP_NUMBER";
            this.Column6.HeaderText = "截止流水号";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "MEMO";
            this.Column7.FillWeight = 75F;
            this.Column7.HeaderText = "备注";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // FrmCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(694, 472);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "钱箱管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCash_Load);
            this.Resize += new System.EventHandler(this.FrmCash_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCash_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cashGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private MyDataGrid cashGridView;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLastCash;
        private System.Windows.Forms.Label lblBalanceCash;
        private System.Windows.Forms.Label lblProfitCash;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnBankSlipNumber;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}
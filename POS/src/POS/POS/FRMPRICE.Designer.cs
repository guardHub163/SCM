namespace POS
{
    partial class FrmPrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrice));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgSalesPrice = new POS.MyDataGrid();
            this.HOT_KEY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORI_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SALES_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISCOUNT_RATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRICE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSalesPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 218);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 35);
            this.panel1.TabIndex = 18;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(272, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定(&OK)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(355, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "商品价格选择";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgSalesPrice);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 193);
            this.panel2.TabIndex = 20;
            // 
            // dgSalesPrice
            // 
            this.dgSalesPrice.AllowUserToAddRows = false;
            this.dgSalesPrice.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgSalesPrice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSalesPrice.BackgroundColor = System.Drawing.Color.White;
            this.dgSalesPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSalesPrice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgSalesPrice.ColumnHeadersHeight = 22;
            this.dgSalesPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgSalesPrice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HOT_KEY,
            this.ORI_PRICE,
            this.SALES_PRICE,
            this.DISCOUNT_RATE,
            this.PRICE_NAME});
            this.dgSalesPrice.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(167)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSalesPrice.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgSalesPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSalesPrice.EnableHeadersVisualStyles = false;
            this.dgSalesPrice.GridColor = System.Drawing.Color.PaleGreen;
            this.dgSalesPrice.Location = new System.Drawing.Point(0, 0);
            this.dgSalesPrice.MultiSelect = false;
            this.dgSalesPrice.Name = "dgSalesPrice";
            this.dgSalesPrice.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSalesPrice.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgSalesPrice.RowHeadersVisible = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgSalesPrice.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgSalesPrice.RowTemplate.Height = 23;
            this.dgSalesPrice.RowTemplate.ReadOnly = true;
            this.dgSalesPrice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgSalesPrice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSalesPrice.Size = new System.Drawing.Size(437, 191);
            this.dgSalesPrice.TabIndex = 1;
            this.dgSalesPrice.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSalesPrice_CellMouseDoubleClick);
            // 
            // HOT_KEY
            // 
            this.HOT_KEY.DataPropertyName = "HOT_KEY";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.HOT_KEY.DefaultCellStyle = dataGridViewCellStyle3;
            this.HOT_KEY.FillWeight = 24.20091F;
            this.HOT_KEY.HeaderText = "快捷键";
            this.HOT_KEY.Name = "HOT_KEY";
            this.HOT_KEY.ReadOnly = true;
            this.HOT_KEY.Width = 50;
            // 
            // ORI_PRICE
            // 
            this.ORI_PRICE.DataPropertyName = "ORI_PRICE";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ORI_PRICE.DefaultCellStyle = dataGridViewCellStyle4;
            this.ORI_PRICE.FillWeight = 22.72524F;
            this.ORI_PRICE.HeaderText = "原价";
            this.ORI_PRICE.Name = "ORI_PRICE";
            this.ORI_PRICE.ReadOnly = true;
            this.ORI_PRICE.Width = 47;
            // 
            // SALES_PRICE
            // 
            this.SALES_PRICE.DataPropertyName = "SALES_PRICE";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SALES_PRICE.DefaultCellStyle = dataGridViewCellStyle5;
            this.SALES_PRICE.FillWeight = 26.56598F;
            this.SALES_PRICE.HeaderText = "售价";
            this.SALES_PRICE.Name = "SALES_PRICE";
            this.SALES_PRICE.ReadOnly = true;
            this.SALES_PRICE.Width = 55;
            // 
            // DISCOUNT_RATE
            // 
            this.DISCOUNT_RATE.DataPropertyName = "DISCOUNT_RATE";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DISCOUNT_RATE.DefaultCellStyle = dataGridViewCellStyle6;
            this.DISCOUNT_RATE.FillWeight = 24.82032F;
            this.DISCOUNT_RATE.HeaderText = "折扣";
            this.DISCOUNT_RATE.Name = "DISCOUNT_RATE";
            this.DISCOUNT_RATE.ReadOnly = true;
            this.DISCOUNT_RATE.Width = 51;
            // 
            // PRICE_NAME
            // 
            this.PRICE_NAME.DataPropertyName = "PRICE_NAME";
            this.PRICE_NAME.FillWeight = 113.6876F;
            this.PRICE_NAME.HeaderText = "价格描述";
            this.PRICE_NAME.Name = "PRICE_NAME";
            this.PRICE_NAME.ReadOnly = true;
            this.PRICE_NAME.Width = 235;
            // 
            // FrmPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(439, 253);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "商品价格选择";
            this.Load += new System.EventHandler(this.FrmPrice_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPrice_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSalesPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyDataGrid dgSalesPrice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn HOT_KEY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORI_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SALES_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DISCOUNT_RATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRICE_NAME;
    }
}
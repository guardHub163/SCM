namespace POS
{
    partial class FrmSuspendList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSuspendList));
            this.dgView = new POS.MyDataGrid();
            this.SLIP_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SALES_EMPLOYEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTOMER_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATE_DATE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEMO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(222)))));
            this.dgView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgView.BackgroundColor = System.Drawing.Color.White;
            this.dgView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLIP_NUMBER,
            this.SALES_EMPLOYEE,
            this.CUSTOMER_CODE,
            this.CREATE_DATE_TIME,
            this.MEMO2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(167)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.EnableHeadersVisualStyles = false;
            this.dgView.GridColor = System.Drawing.Color.PaleGreen;
            this.dgView.Location = new System.Drawing.Point(0, 0);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.RowHeadersVisible = false;
            this.dgView.RowTemplate.Height = 23;
            this.dgView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgView.Size = new System.Drawing.Size(556, 270);
            this.dgView.TabIndex = 0;
            this.dgView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgView_CellDoubleClick);
            this.dgView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgView_KeyDown);
            // 
            // SLIP_NUMBER
            // 
            this.SLIP_NUMBER.DataPropertyName = "SLIP_NUMBER";
            this.SLIP_NUMBER.HeaderText = "临时单号";
            this.SLIP_NUMBER.Name = "SLIP_NUMBER";
            this.SLIP_NUMBER.ReadOnly = true;
            // 
            // SALES_EMPLOYEE
            // 
            this.SALES_EMPLOYEE.DataPropertyName = "SALES_EMPLOYEE";
            this.SALES_EMPLOYEE.HeaderText = "销售员";
            this.SALES_EMPLOYEE.Name = "SALES_EMPLOYEE";
            this.SALES_EMPLOYEE.ReadOnly = true;
            // 
            // CUSTOMER_CODE
            // 
            this.CUSTOMER_CODE.DataPropertyName = "CUSTOMER_CODE";
            this.CUSTOMER_CODE.HeaderText = "客户编号";
            this.CUSTOMER_CODE.Name = "CUSTOMER_CODE";
            this.CUSTOMER_CODE.ReadOnly = true;
            // 
            // CREATE_DATE_TIME
            // 
            this.CREATE_DATE_TIME.DataPropertyName = "CREATE_DATE_TIME";
            this.CREATE_DATE_TIME.HeaderText = "挂起时间";
            this.CREATE_DATE_TIME.Name = "CREATE_DATE_TIME";
            this.CREATE_DATE_TIME.ReadOnly = true;
            // 
            // MEMO2
            // 
            this.MEMO2.DataPropertyName = "MEMO2";
            this.MEMO2.HeaderText = "备注";
            this.MEMO2.Name = "MEMO2";
            this.MEMO2.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 270);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 35);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::POS.Properties.Resources.button_2;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(466, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::POS.Properties.Resources.button_2;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(370, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确认(&OK)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmSuspendList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(556, 305);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSuspendList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "取单选择";
            this.Load += new System.EventHandler(this.FrmSuspendList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyDataGrid dgView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLIP_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn SALES_EMPLOYEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATE_DATE_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEMO2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}
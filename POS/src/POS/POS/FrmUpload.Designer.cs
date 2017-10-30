namespace POS
{
    partial class FrmUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpload));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.picSalesOrder = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.picCash = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.prcCustomer = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSalesOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcCustomer)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 25);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "基础数据上传";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.picSalesOrder);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.picCash);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.prcCustomer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(809, 153);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "销售记录";
            // 
            // picSalesOrder
            // 
            this.picSalesOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picSalesOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSalesOrder.Image = global::POS.Properties.Resources.销售管理;
            this.picSalesOrder.Location = new System.Drawing.Point(314, 16);
            this.picSalesOrder.Name = "picSalesOrder";
            this.picSalesOrder.Padding = new System.Windows.Forms.Padding(5);
            this.picSalesOrder.Size = new System.Drawing.Size(90, 90);
            this.picSalesOrder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSalesOrder.TabIndex = 4;
            this.picSalesOrder.TabStop = false;
            this.picSalesOrder.MouseLeave += new System.EventHandler(this.Pic_MouseLeave);
            this.picSalesOrder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pic_MouseMove);
            this.picSalesOrder.Click += new System.EventHandler(this.picSalesOrder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "钱箱存取记录";
            // 
            // picCash
            // 
            this.picCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCash.Image = global::POS.Properties.Resources.cash;
            this.picCash.Location = new System.Drawing.Point(163, 16);
            this.picCash.Name = "picCash";
            this.picCash.Padding = new System.Windows.Forms.Padding(5);
            this.picCash.Size = new System.Drawing.Size(90, 90);
            this.picCash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCash.TabIndex = 2;
            this.picCash.TabStop = false;
            this.picCash.MouseLeave += new System.EventHandler(this.Pic_MouseLeave);
            this.picCash.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pic_MouseMove);
            this.picCash.Click += new System.EventHandler(this.picCash_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "客户资料";
            // 
            // prcCustomer
            // 
            this.prcCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.prcCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prcCustomer.Image = global::POS.Properties.Resources.customerInfo;
            this.prcCustomer.Location = new System.Drawing.Point(12, 16);
            this.prcCustomer.Name = "prcCustomer";
            this.prcCustomer.Padding = new System.Windows.Forms.Padding(5);
            this.prcCustomer.Size = new System.Drawing.Size(90, 90);
            this.prcCustomer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.prcCustomer.TabIndex = 0;
            this.prcCustomer.TabStop = false;
            this.prcCustomer.MouseLeave += new System.EventHandler(this.Pic_MouseLeave);
            this.prcCustomer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pic_MouseMove);
            this.prcCustomer.Click += new System.EventHandler(this.prcCustomer_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 178);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(809, 25);
            this.panel3.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "上传记录";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(108)))), ((int)(((byte)(150)))));
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 532);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(809, 35);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(720, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(89, 35);
            this.panel5.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::POS.Properties.Resources.button_2;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(5, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // listBox
            // 
            this.listBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 12;
            this.listBox.Location = new System.Drawing.Point(0, 203);
            this.listBox.Margin = new System.Windows.Forms.Padding(0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(809, 324);
            this.listBox.TabIndex = 5;
            // 
            // FrmUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(809, 567);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础数据上传";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSalesOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcCustomer)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picCash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox prcCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picSalesOrder;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel5;
    }
}
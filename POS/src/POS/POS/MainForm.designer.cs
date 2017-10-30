namespace POS
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.navigateBar = new System.Windows.Forms.Panel();
            this.panelBody = new System.Windows.Forms.Panel();
            this.panelFrm = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblMenu1 = new System.Windows.Forms.Label();
            this.lblMenu2 = new System.Windows.Forms.Label();
            this.lblMenu3 = new System.Windows.Forms.Label();
            this.lblMenu4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMenu = new System.Windows.Forms.Label();
            this.TimeShowsystemtime = new System.Windows.Forms.Timer(this.components);
            this.panelTitle = new System.Windows.Forms.Panel();
            this.panelTitleOpater = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelBody.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.panelTitleOpater.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigateBar
            // 
            this.navigateBar.AutoScroll = true;
            this.navigateBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(219)))), ((int)(((byte)(246)))));
            this.navigateBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigateBar.Location = new System.Drawing.Point(5, 0);
            this.navigateBar.Name = "navigateBar";
            this.navigateBar.Size = new System.Drawing.Size(110, 449);
            this.navigateBar.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.Controls.Add(this.panelFrm);
            this.panelBody.Controls.Add(this.panelBottom);
            this.panelBody.Controls.Add(this.panelLeft);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 98);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(794, 474);
            this.panelBody.TabIndex = 13;
            // 
            // panelFrm
            // 
            this.panelFrm.AutoSize = true;
            this.panelFrm.BackgroundImage = global::POS.Properties.Resources.背景图片;
            this.panelFrm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelFrm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFrm.Location = new System.Drawing.Point(120, 0);
            this.panelFrm.Name = "panelFrm";
            this.panelFrm.Size = new System.Drawing.Size(674, 439);
            this.panelFrm.TabIndex = 6;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panelBottom.BackgroundImage = global::POS.Properties.Resources.footer;
            this.panelBottom.Controls.Add(this.label2);
            this.panelBottom.Controls.Add(this.lblTime);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(120, 439);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(674, 35);
            this.panelBottom.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "当前时间:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(91, 10);
            this.lblTime.Margin = new System.Windows.Forms.Padding(0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 16);
            this.lblTime.TabIndex = 0;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(98)))), ((int)(((byte)(142)))));
            this.panelLeft.Controls.Add(this.lblMenu1);
            this.panelLeft.Controls.Add(this.lblMenu2);
            this.panelLeft.Controls.Add(this.lblMenu3);
            this.panelLeft.Controls.Add(this.lblMenu4);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Controls.Add(this.lblMenu);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(120, 474);
            this.panelLeft.TabIndex = 9;
            // 
            // lblMenu1
            // 
            this.lblMenu1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMenu1.ForeColor = System.Drawing.Color.White;
            this.lblMenu1.Image = global::POS.Properties.Resources.nav;
            this.lblMenu1.Location = new System.Drawing.Point(0, 376);
            this.lblMenu1.Name = "lblMenu1";
            this.lblMenu1.Size = new System.Drawing.Size(120, 23);
            this.lblMenu1.TabIndex = 14;
            this.lblMenu1.Tag = "1";
            this.lblMenu1.Text = "日常事务";
            this.lblMenu1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMenu1.Click += new System.EventHandler(this.Menu_Click);
            // 
            // lblMenu2
            // 
            this.lblMenu2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMenu2.ForeColor = System.Drawing.Color.White;
            this.lblMenu2.Image = ((System.Drawing.Image)(resources.GetObject("lblMenu2.Image")));
            this.lblMenu2.Location = new System.Drawing.Point(0, 399);
            this.lblMenu2.Name = "lblMenu2";
            this.lblMenu2.Size = new System.Drawing.Size(120, 25);
            this.lblMenu2.TabIndex = 13;
            this.lblMenu2.Tag = "2";
            this.lblMenu2.Text = "统计分析";
            this.lblMenu2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMenu2.Click += new System.EventHandler(this.Menu_Click);
            // 
            // lblMenu3
            // 
            this.lblMenu3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMenu3.ForeColor = System.Drawing.Color.White;
            this.lblMenu3.Image = ((System.Drawing.Image)(resources.GetObject("lblMenu3.Image")));
            this.lblMenu3.Location = new System.Drawing.Point(0, 424);
            this.lblMenu3.Name = "lblMenu3";
            this.lblMenu3.Size = new System.Drawing.Size(120, 25);
            this.lblMenu3.TabIndex = 13;
            this.lblMenu3.Tag = "3";
            this.lblMenu3.Text = "信息维护";
            this.lblMenu3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMenu3.Click += new System.EventHandler(this.Menu_Click);
            // 
            // lblMenu4
            // 
            this.lblMenu4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMenu4.ForeColor = System.Drawing.Color.White;
            this.lblMenu4.Image = ((System.Drawing.Image)(resources.GetObject("lblMenu4.Image")));
            this.lblMenu4.Location = new System.Drawing.Point(0, 449);
            this.lblMenu4.Name = "lblMenu4";
            this.lblMenu4.Size = new System.Drawing.Size(120, 25);
            this.lblMenu4.TabIndex = 13;
            this.lblMenu4.Tag = "4";
            this.lblMenu4.Text = "帮助及注册";
            this.lblMenu4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMenu4.Click += new System.EventHandler(this.Menu_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.navigateBar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.panel3.Size = new System.Drawing.Size(120, 449);
            this.panel3.TabIndex = 12;
            // 
            // lblMenu
            // 
            this.lblMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenu.ForeColor = System.Drawing.Color.White;
            this.lblMenu.Image = global::POS.Properties.Resources.nav;
            this.lblMenu.Location = new System.Drawing.Point(0, 0);
            this.lblMenu.Margin = new System.Windows.Forms.Padding(0);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(120, 25);
            this.lblMenu.TabIndex = 1;
            this.lblMenu.Tag = "1";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimeShowsystemtime
            // 
            this.TimeShowsystemtime.Enabled = true;
            this.TimeShowsystemtime.Interval = 1000;
            this.TimeShowsystemtime.Tick += new System.EventHandler(this.TimeShowsystemtime_Tick);
            // 
            // panelTitle
            // 
            this.panelTitle.BackgroundImage = global::POS.Properties.Resources.top;
            this.panelTitle.Controls.Add(this.panelTitleOpater);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(794, 98);
            this.panelTitle.TabIndex = 9;
            // 
            // panelTitleOpater
            // 
            this.panelTitleOpater.BackColor = System.Drawing.Color.Transparent;
            this.panelTitleOpater.Controls.Add(this.panel2);
            this.panelTitleOpater.Controls.Add(this.panel1);
            this.panelTitleOpater.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTitleOpater.Location = new System.Drawing.Point(517, 0);
            this.panelTitleOpater.Name = "panelTitleOpater";
            this.panelTitleOpater.Size = new System.Drawing.Size(277, 98);
            this.panelTitleOpater.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblUserName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(3, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 25);
            this.panel2.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(118, 7);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(53, 12);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "UserName";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前登录用户:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::POS.Properties.Resources.userInfo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::POS.Properties.Resources.top_1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnLogOut);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(3, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 25);
            this.panel1.TabIndex = 0;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogOut.BackgroundImage")));
            this.btnLogOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnLogOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("宋体", 8.5F);
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(191, 4);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(80, 20);
            this.btnLogOut.TabIndex = 3;
            this.btnLogOut.Text = "退出系统(&Q)";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("宋体", 8.5F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(109, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 20);
            this.button2.TabIndex = 2;
            this.button2.Text = "用户信息";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::POS.Properties.Resources.top_2;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("宋体", 8.5F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(27, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "修改密码";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "门店销售管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelTitle.ResumeLayout(false);
            this.panelTitleOpater.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelFrm;
        private System.Windows.Forms.Panel navigateBar;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelTitleOpater;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer TimeShowsystemtime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label lblMenu2;
        private System.Windows.Forms.Label lblMenu3;
        private System.Windows.Forms.Label lblMenu4;
        private System.Windows.Forms.Label lblMenu1;
    }
}




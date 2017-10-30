namespace POS.AutoUpdater
{
    partial class DownloadProgress
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
            this.labelCurrentItem = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBarCurrent = new System.Windows.Forms.ProgressBar();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCurrentItem
            // 
            this.labelCurrentItem.AutoSize = true;
            this.labelCurrentItem.Location = new System.Drawing.Point(84, 8);
            this.labelCurrentItem.Name = "labelCurrentItem";
            this.labelCurrentItem.Size = new System.Drawing.Size(0, 12);
            this.labelCurrentItem.TabIndex = 6;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(377, 85);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "取消";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.OnCancel);
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(15, 67);
            this.progressBarTotal.Maximum = 1000000;
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(438, 12);
            this.progressBarTotal.Step = 1;
            this.progressBarTotal.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "总进度：";
            // 
            // progressBarCurrent
            // 
            this.progressBarCurrent.Location = new System.Drawing.Point(14, 24);
            this.progressBarCurrent.Maximum = 1000000;
            this.progressBarCurrent.Name = "progressBarCurrent";
            this.progressBarCurrent.Size = new System.Drawing.Size(438, 12);
            this.progressBarCurrent.Step = 1;
            this.progressBarCurrent.TabIndex = 7;
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Location = new System.Drawing.Point(12, 9);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(65, 12);
            this.labelCurrent.TabIndex = 5;
            this.labelCurrent.Text = "正在下载：";
            // 
            // DownloadProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 114);
            this.ControlBox = false;
            this.Controls.Add(this.labelCurrentItem);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarCurrent);
            this.Controls.Add(this.labelCurrent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下载进度";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCurrentItem;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ProgressBar progressBarTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBarCurrent;
        private System.Windows.Forms.Label labelCurrent;
    }
}
namespace pillindl {
    partial class frmAbout {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblAppDescr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWebpage = new System.Windows.Forms.LinkLabel();
            this.btnOk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppName.Location = new System.Drawing.Point(69, 18);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(143, 13);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "Pillín Video Downloader";
            // 
            // lblAppDescr
            // 
            this.lblAppDescr.AutoSize = true;
            this.lblAppDescr.Location = new System.Drawing.Point(69, 42);
            this.lblAppDescr.Name = "lblAppDescr";
            this.lblAppDescr.Size = new System.Drawing.Size(255, 13);
            this.lblAppDescr.TabIndex = 1;
            this.lblAppDescr.Text = "Video downloader for Vimeo, Dailymotion and Twitter";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(255, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "v0.1a \'akkyan\'";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblWebpage
            // 
            this.lblWebpage.AutoSize = true;
            this.lblWebpage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebpage.Location = new System.Drawing.Point(69, 86);
            this.lblWebpage.Name = "lblWebpage";
            this.lblWebpage.Size = new System.Drawing.Size(143, 13);
            this.lblWebpage.TabIndex = 3;
            this.lblWebpage.TabStop = true;
            this.lblWebpage.Text = "Jonatan Huenupil-Fernandez";
            this.lblWebpage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblWebpage_LinkClicked);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(321, 142);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Bien";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::pillindl.Properties.Resources.flowRoot4174_6_32;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 43);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(408, 177);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblWebpage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAppDescr);
            this.Controls.Add(this.lblAppName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblAppDescr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lblWebpage;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
namespace pillindl {
    partial class frmMain {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.appToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAmeblo = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheckNewVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.cbListQuality = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtVideoTitle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblVideoDuration = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblVideoSize = new System.Windows.Forms.Label();
            this.lblSave = new System.Windows.Forms.Label();
            this.lblSaveDirectory = new System.Windows.Forms.Label();
            this.btnMenuSaveFolder = new System.Windows.Forms.Button();
            this.btnResetTitle = new System.Windows.Forms.Button();
            this.mnuSaveFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownload = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stlblPercentProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.stpbProgressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblInfo3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.fbFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuMain.SuspendLayout();
            this.mnuSaveFolder.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList1
            // 
            this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
            this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList1.Images.SetKeyName(0, "icon.ico");
            // 
            // lblURL
            // 
            this.lblURL.Location = new System.Drawing.Point(12, 52);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(67, 22);
            this.lblURL.TabIndex = 0;
            this.lblURL.Text = "URL";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(88, 52);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(314, 22);
            this.txtURL.TabIndex = 1;
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(421, 52);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(87, 22);
            this.btnGetInfo.TabIndex = 2;
            this.btnGetInfo.Text = "Get";
            this.btnGetInfo.UseVisualStyleBackColor = true;
            this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(537, 24);
            this.mnuMain.TabIndex = 3;
            this.mnuMain.Text = "MainMenu";
            // 
            // appToolStripMenuItem
            // 
            this.appToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAmeblo,
            this.exitToolStripMenuItem});
            this.appToolStripMenuItem.Name = "appToolStripMenuItem";
            this.appToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.appToolStripMenuItem.Text = "App";
            // 
            // mnuAmeblo
            // 
            this.mnuAmeblo.Name = "mnuAmeblo";
            this.mnuAmeblo.Size = new System.Drawing.Size(125, 22);
            this.mnuAmeblo.Text = "Ameblo...";
            this.mnuAmeblo.Click += new System.EventHandler(this.mnuAmeblo_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCheckNewVersion,
            this.mnuAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mnuCheckNewVersion
            // 
            this.mnuCheckNewVersion.Name = "mnuCheckNewVersion";
            this.mnuCheckNewVersion.Size = new System.Drawing.Size(173, 22);
            this.mnuCheckNewVersion.Text = "Check new version";
            this.mnuCheckNewVersion.Click += new System.EventHandler(this.mnuCheckNewVersion_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(173, 22);
            this.mnuAbout.Text = "About...";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // lblInfo1
            // 
            this.lblInfo1.AutoSize = true;
            this.lblInfo1.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo1.Location = new System.Drawing.Point(12, 32);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(162, 14);
            this.lblInfo1.TabIndex = 4;
            this.lblInfo1.Text = "1. Add URL for to get video info";
            // 
            // lblInfo2
            // 
            this.lblInfo2.AutoSize = true;
            this.lblInfo2.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo2.Location = new System.Drawing.Point(12, 102);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(86, 14);
            this.lblInfo2.TabIndex = 5;
            this.lblInfo2.Text = "2. Select quality";
            // 
            // cbListQuality
            // 
            this.cbListQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbListQuality.Location = new System.Drawing.Point(88, 171);
            this.cbListQuality.Name = "cbListQuality";
            this.cbListQuality.Size = new System.Drawing.Size(87, 22);
            this.cbListQuality.TabIndex = 6;
            this.cbListQuality.SelectedIndexChanged += new System.EventHandler(this.cbListQuality_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Quality:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 129);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(67, 15);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Title:";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVideoTitle
            // 
            this.txtVideoTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtVideoTitle.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVideoTitle.Location = new System.Drawing.Point(115, 126);
            this.txtVideoTitle.Name = "txtVideoTitle";
            this.txtVideoTitle.Size = new System.Drawing.Size(393, 22);
            this.txtVideoTitle.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(373, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "Duration:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVideoDuration
            // 
            this.lblVideoDuration.AutoSize = true;
            this.lblVideoDuration.Location = new System.Drawing.Point(448, 174);
            this.lblVideoDuration.Name = "lblVideoDuration";
            this.lblVideoDuration.Size = new System.Drawing.Size(0, 14);
            this.lblVideoDuration.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(181, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 14);
            this.label9.TabIndex = 14;
            this.label9.Text = "Size:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblVideoSize
            // 
            this.lblVideoSize.AutoSize = true;
            this.lblVideoSize.Location = new System.Drawing.Point(245, 174);
            this.lblVideoSize.Name = "lblVideoSize";
            this.lblVideoSize.Size = new System.Drawing.Size(0, 14);
            this.lblVideoSize.TabIndex = 15;
            // 
            // lblSave
            // 
            this.lblSave.Location = new System.Drawing.Point(12, 205);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(67, 17);
            this.lblSave.TabIndex = 16;
            this.lblSave.Text = "Save in:";
            this.lblSave.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSaveDirectory
            // 
            this.lblSaveDirectory.AutoSize = true;
            this.lblSaveDirectory.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveDirectory.Location = new System.Drawing.Point(112, 205);
            this.lblSaveDirectory.Name = "lblSaveDirectory";
            this.lblSaveDirectory.Size = new System.Drawing.Size(142, 14);
            this.lblSaveDirectory.TabIndex = 17;
            this.lblSaveDirectory.Text = "C:\\Users\\wenupix\\Desktop";
            // 
            // btnMenuSaveFolder
            // 
            this.btnMenuSaveFolder.Location = new System.Drawing.Point(88, 201);
            this.btnMenuSaveFolder.Name = "btnMenuSaveFolder";
            this.btnMenuSaveFolder.Size = new System.Drawing.Size(17, 19);
            this.btnMenuSaveFolder.TabIndex = 19;
            this.btnMenuSaveFolder.Text = "=";
            this.btnMenuSaveFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMenuSaveFolder.UseVisualStyleBackColor = true;
            this.btnMenuSaveFolder.Click += new System.EventHandler(this.btnMenuSaveFolder_Click);
            // 
            // btnResetTitle
            // 
            this.btnResetTitle.Location = new System.Drawing.Point(88, 127);
            this.btnResetTitle.Name = "btnResetTitle";
            this.btnResetTitle.Size = new System.Drawing.Size(17, 19);
            this.btnResetTitle.TabIndex = 20;
            this.btnResetTitle.Text = "!";
            this.btnResetTitle.UseVisualStyleBackColor = true;
            this.btnResetTitle.Click += new System.EventHandler(this.btnResetTitle_Click);
            // 
            // mnuSaveFolder
            // 
            this.mnuSaveFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDirectoryToolStripMenuItem,
            this.openDirectoryToolStripMenuItem});
            this.mnuSaveFolder.Name = "mnuSaveFolder";
            this.mnuSaveFolder.Size = new System.Drawing.Size(165, 48);
            // 
            // selectDirectoryToolStripMenuItem
            // 
            this.selectDirectoryToolStripMenuItem.Name = "selectDirectoryToolStripMenuItem";
            this.selectDirectoryToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.selectDirectoryToolStripMenuItem.Text = "Select directory...";
            this.selectDirectoryToolStripMenuItem.Click += new System.EventHandler(this.selectDirectoryToolStripMenuItem_Click);
            // 
            // openDirectoryToolStripMenuItem
            // 
            this.openDirectoryToolStripMenuItem.Name = "openDirectoryToolStripMenuItem";
            this.openDirectoryToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openDirectoryToolStripMenuItem.Text = "Open directory";
            this.openDirectoryToolStripMenuItem.Click += new System.EventHandler(this.openDirectoryToolStripMenuItem_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(184, 251);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(142, 36);
            this.btnDownload.TabIndex = 22;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlblPercentProgress,
            this.stpbProgressbar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 304);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(537, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stlblPercentProgress
            // 
            this.stlblPercentProgress.AutoSize = false;
            this.stlblPercentProgress.Name = "stlblPercentProgress";
            this.stlblPercentProgress.Size = new System.Drawing.Size(40, 17);
            this.stlblPercentProgress.Text = "0%";
            this.stlblPercentProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stpbProgressbar
            // 
            this.stpbProgressbar.Name = "stpbProgressbar";
            this.stpbProgressbar.Size = new System.Drawing.Size(200, 16);
            this.stpbProgressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblInfo3
            // 
            this.lblInfo3.AutoSize = true;
            this.lblInfo3.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo3.Location = new System.Drawing.Point(12, 248);
            this.lblInfo3.Name = "lblInfo3";
            this.lblInfo3.Size = new System.Drawing.Size(99, 14);
            this.lblInfo3.TabIndex = 24;
            this.lblInfo3.Text = "3. Download video";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(245, 308);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(29, 14);
            this.lblStatus.TabIndex = 25;
            this.lblStatus.Text = "Idle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Title will be used as filename. You can modify title. To reset to original title," +
    " press \"!\" button.";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 326);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblInfo3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnResetTitle);
            this.Controls.Add(this.btnMenuSaveFolder);
            this.Controls.Add(this.lblSaveDirectory);
            this.Controls.Add(this.lblSave);
            this.Controls.Add(this.lblVideoSize);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblVideoDuration);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtVideoTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbListQuality);
            this.Controls.Add(this.lblInfo2);
            this.Controls.Add(this.lblInfo1);
            this.Controls.Add(this.btnGetInfo);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.mnuMain);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Pillin Video Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.mnuSaveFolder.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imgList1;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem appToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAmeblo;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.Label lblInfo1;
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.ComboBox cbListQuality;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtVideoTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblVideoDuration;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblVideoSize;
        private System.Windows.Forms.Label lblSave;
        private System.Windows.Forms.Label lblSaveDirectory;
        private System.Windows.Forms.Button btnMenuSaveFolder;
        private System.Windows.Forms.Button btnResetTitle;
        private System.Windows.Forms.ContextMenuStrip mnuSaveFolder;
        private System.Windows.Forms.ToolStripMenuItem selectDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDirectoryToolStripMenuItem;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stlblPercentProgress;
        private System.Windows.Forms.ToolStripProgressBar stpbProgressbar;
        private System.Windows.Forms.Label lblInfo3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.FolderBrowserDialog fbFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckNewVersion;
    }
}


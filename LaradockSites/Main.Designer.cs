namespace LaradockSites
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbSiteler = new System.Windows.Forms.ListBox();
            this.btnSiteEkle = new System.Windows.Forms.Button();
            this.btnSiteSil = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenSite = new System.Windows.Forms.Button();
            this.btnOpenDirectory = new System.Windows.Forms.Button();
            this.lbLocation = new System.Windows.Forms.Label();
            this.lbServerName = new System.Windows.Forms.Label();
            this.lbSiteName = new System.Windows.Forms.Label();
            this.btnStartLD = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnStopLD = new System.Windows.Forms.Button();
            this.btnAccessCMD = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnChangePath = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSiteler
            // 
            this.lbSiteler.FormattingEnabled = true;
            this.lbSiteler.ItemHeight = 20;
            this.lbSiteler.Location = new System.Drawing.Point(12, 12);
            this.lbSiteler.Name = "lbSiteler";
            this.lbSiteler.Size = new System.Drawing.Size(301, 204);
            this.lbSiteler.TabIndex = 0;
            this.lbSiteler.SelectedIndexChanged += new System.EventHandler(this.lbSiteler_SelectedIndexChanged);
            this.lbSiteler.DoubleClick += new System.EventHandler(this.lbSiteler_DoubleClick);
            // 
            // btnSiteEkle
            // 
            this.btnSiteEkle.Location = new System.Drawing.Point(12, 222);
            this.btnSiteEkle.Name = "btnSiteEkle";
            this.btnSiteEkle.Size = new System.Drawing.Size(94, 29);
            this.btnSiteEkle.TabIndex = 1;
            this.btnSiteEkle.Text = "Site Ekle";
            this.btnSiteEkle.UseVisualStyleBackColor = true;
            this.btnSiteEkle.Click += new System.EventHandler(this.btnSiteEkle_Click);
            // 
            // btnSiteSil
            // 
            this.btnSiteSil.Location = new System.Drawing.Point(112, 222);
            this.btnSiteSil.Name = "btnSiteSil";
            this.btnSiteSil.Size = new System.Drawing.Size(94, 29);
            this.btnSiteSil.TabIndex = 2;
            this.btnSiteSil.Text = "Site Sil";
            this.btnSiteSil.UseVisualStyleBackColor = true;
            this.btnSiteSil.Click += new System.EventHandler(this.btnSiteSil_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenSite);
            this.groupBox1.Controls.Add(this.btnOpenDirectory);
            this.groupBox1.Controls.Add(this.lbLocation);
            this.groupBox1.Controls.Add(this.lbServerName);
            this.groupBox1.Controls.Add(this.lbSiteName);
            this.groupBox1.Location = new System.Drawing.Point(319, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 204);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site Bilgileri";
            // 
            // btnOpenSite
            // 
            this.btnOpenSite.Enabled = false;
            this.btnOpenSite.Location = new System.Drawing.Point(146, 154);
            this.btnOpenSite.Name = "btnOpenSite";
            this.btnOpenSite.Size = new System.Drawing.Size(118, 29);
            this.btnOpenSite.TabIndex = 4;
            this.btnOpenSite.Text = "Siteyi Aç";
            this.btnOpenSite.UseVisualStyleBackColor = true;
            this.btnOpenSite.Click += new System.EventHandler(this.btnOpenSite_Click);
            // 
            // btnOpenDirectory
            // 
            this.btnOpenDirectory.Enabled = false;
            this.btnOpenDirectory.Location = new System.Drawing.Point(22, 154);
            this.btnOpenDirectory.Name = "btnOpenDirectory";
            this.btnOpenDirectory.Size = new System.Drawing.Size(118, 29);
            this.btnOpenDirectory.TabIndex = 3;
            this.btnOpenDirectory.Text = "Klasörü Aç";
            this.btnOpenDirectory.UseVisualStyleBackColor = true;
            this.btnOpenDirectory.Click += new System.EventHandler(this.btnOpenDirectory_Click);
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(22, 101);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(79, 20);
            this.lbLocation.TabIndex = 2;
            this.lbLocation.Text = "lbLocation";
            // 
            // lbServerName
            // 
            this.lbServerName.AutoSize = true;
            this.lbServerName.Location = new System.Drawing.Point(22, 66);
            this.lbServerName.Name = "lbServerName";
            this.lbServerName.Size = new System.Drawing.Size(50, 20);
            this.lbServerName.TabIndex = 1;
            this.lbServerName.Text = "label2";
            // 
            // lbSiteName
            // 
            this.lbSiteName.AutoSize = true;
            this.lbSiteName.Location = new System.Drawing.Point(22, 33);
            this.lbSiteName.Name = "lbSiteName";
            this.lbSiteName.Size = new System.Drawing.Size(87, 20);
            this.lbSiteName.TabIndex = 0;
            this.lbSiteName.Text = "lbSiteName";
            // 
            // btnStartLD
            // 
            this.btnStartLD.Location = new System.Drawing.Point(6, 127);
            this.btnStartLD.Name = "btnStartLD";
            this.btnStartLD.Size = new System.Drawing.Size(170, 29);
            this.btnStartLD.TabIndex = 4;
            this.btnStartLD.Text = "Start Laradock";
            this.btnStartLD.UseVisualStyleBackColor = true;
            this.btnStartLD.Click += new System.EventHandler(this.btnStartLD_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(17, 38);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(50, 20);
            this.lbStatus.TabIndex = 5;
            this.lbStatus.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbStatus);
            this.groupBox2.Controls.Add(this.btnInstall);
            this.groupBox2.Controls.Add(this.btnStopLD);
            this.groupBox2.Controls.Add(this.btnAccessCMD);
            this.groupBox2.Controls.Add(this.btnStartLD);
            this.groupBox2.Location = new System.Drawing.Point(12, 257);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 162);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Laradock";
            // 
            // btnInstall
            // 
            this.btnInstall.Enabled = false;
            this.btnInstall.Location = new System.Drawing.Point(358, 127);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(170, 29);
            this.btnInstall.TabIndex = 4;
            this.btnInstall.Text = "Install Laradock";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnStopLD
            // 
            this.btnStopLD.Enabled = false;
            this.btnStopLD.Location = new System.Drawing.Point(182, 127);
            this.btnStopLD.Name = "btnStopLD";
            this.btnStopLD.Size = new System.Drawing.Size(170, 29);
            this.btnStopLD.TabIndex = 4;
            this.btnStopLD.Text = "Stop Laradock";
            this.btnStopLD.UseVisualStyleBackColor = true;
            this.btnStopLD.Click += new System.EventHandler(this.btnStopLD_Click);
            // 
            // btnAccessCMD
            // 
            this.btnAccessCMD.Location = new System.Drawing.Point(6, 92);
            this.btnAccessCMD.Name = "btnAccessCMD";
            this.btnAccessCMD.Size = new System.Drawing.Size(170, 29);
            this.btnAccessCMD.TabIndex = 4;
            this.btnAccessCMD.Text = "Access Console";
            this.btnAccessCMD.UseVisualStyleBackColor = true;
            this.btnAccessCMD.Click += new System.EventHandler(this.btnAccessCMD_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnChangePath
            // 
            this.btnChangePath.Location = new System.Drawing.Point(212, 222);
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.Size = new System.Drawing.Size(101, 29);
            this.btnChangePath.TabIndex = 2;
            this.btnChangePath.Text = "Path Değiştir";
            this.btnChangePath.UseVisualStyleBackColor = true;
            this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 431);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnChangePath);
            this.Controls.Add(this.btnSiteSil);
            this.Controls.Add(this.btnSiteEkle);
            this.Controls.Add(this.lbSiteler);
            this.Name = "Main";
            this.Text = "Laradock Control Panel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox lbSiteler;
        private Button btnSiteEkle;
        private Button btnSiteSil;
        private GroupBox groupBox1;
        private Button btnOpenDirectory;
        private Label lbLocation;
        private Label lbServerName;
        private Label lbSiteName;
        private Button btnOpenSite;
        private Button btnStartLD;
        private Label lbStatus;
        private GroupBox groupBox2;
        private Button btnStopLD;
        private Button btnInstall;
        private Button btnAccessCMD;
        private Button btnChangePath;
        public System.Windows.Forms.Timer timer1;
    }
}
namespace LaradockSites
{
    partial class AddSite
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSiteAdi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbKlasorAdi = new System.Windows.Forms.TextBox();
            this.cbPackage = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(284, 142);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 29);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Site Name";
            // 
            // tbSiteAdi
            // 
            this.tbSiteAdi.Location = new System.Drawing.Point(110, 9);
            this.tbSiteAdi.Name = "tbSiteAdi";
            this.tbSiteAdi.Size = new System.Drawing.Size(268, 27);
            this.tbSiteAdi.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Url";
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(110, 42);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(268, 27);
            this.tbUrl.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Folder Name";
            // 
            // tbKlasorAdi
            // 
            this.tbKlasorAdi.Location = new System.Drawing.Point(110, 75);
            this.tbKlasorAdi.Name = "tbKlasorAdi";
            this.tbKlasorAdi.Size = new System.Drawing.Size(268, 27);
            this.tbKlasorAdi.TabIndex = 2;
            // 
            // cbPackage
            // 
            this.cbPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPackage.FormattingEnabled = true;
            this.cbPackage.Items.AddRange(new object[] {
            "Laravel",
            "Wordpress"});
            this.cbPackage.Location = new System.Drawing.Point(110, 108);
            this.cbPackage.Name = "cbPackage";
            this.cbPackage.Size = new System.Drawing.Size(268, 28);
            this.cbPackage.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Package";
            // 
            // AddSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 186);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPackage);
            this.Controls.Add(this.tbKlasorAdi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSiteAdi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSite";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddSite";
            this.Load += new System.EventHandler(this.AddSite_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnAdd;
        private Label label1;
        private TextBox tbSiteAdi;
        private Label label2;
        private TextBox tbUrl;
        private Label label3;
        private TextBox tbKlasorAdi;
        private ComboBox cbPackage;
        private Label label4;
    }
}
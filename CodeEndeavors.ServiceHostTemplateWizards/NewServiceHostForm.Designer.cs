namespace CodeEndeavors.ServiceHostTemplateWizards
{
    partial class NewServiceHostForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewServiceHostForm));
            this.lblVidereDir = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblServiceHostUrl = new System.Windows.Forms.Label();
            this.txtServiceHostDir = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtServiceHostUrl = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVidereDir
            // 
            this.lblVidereDir.AutoSize = true;
            this.lblVidereDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVidereDir.Location = new System.Drawing.Point(3, 0);
            this.lblVidereDir.Name = "lblVidereDir";
            this.lblVidereDir.Size = new System.Drawing.Size(145, 26);
            this.lblVidereDir.TabIndex = 29;
            this.lblVidereDir.Text = "Service Host Directory";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Location = new System.Drawing.Point(0, 263);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 45);
            this.panel1.TabIndex = 24;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::CodeEndeavors.ServiceHostTemplateWizards.Properties.Resources.CodeEndeavorsIncLogo;
            this.picLogo.Location = new System.Drawing.Point(12, 7);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(99, 26);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLogo.TabIndex = 7;
            this.picLogo.TabStop = false;
            this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(327, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(247, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.64641F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.35359F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Controls.Add(this.lblServiceHostUrl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtServiceHostDir, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblVidereDir, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBrowse, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtServiceHostUrl, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 93);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(402, 164);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // lblServiceHostUrl
            // 
            this.lblServiceHostUrl.AutoSize = true;
            this.lblServiceHostUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceHostUrl.Location = new System.Drawing.Point(3, 26);
            this.lblServiceHostUrl.Name = "lblServiceHostUrl";
            this.lblServiceHostUrl.Size = new System.Drawing.Size(145, 138);
            this.lblServiceHostUrl.TabIndex = 31;
            this.lblServiceHostUrl.Text = "Service Host Url";
            // 
            // txtServiceHostDir
            // 
            this.txtServiceHostDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServiceHostDir.Location = new System.Drawing.Point(154, 3);
            this.txtServiceHostDir.Name = "txtServiceHostDir";
            this.txtServiceHostDir.Size = new System.Drawing.Size(189, 20);
            this.txtServiceHostDir.TabIndex = 1;
            this.txtServiceHostDir.Text = "c:\\ServiceHost";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(349, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(28, 20);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtServiceHostUrl
            // 
            this.txtServiceHostUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServiceHostUrl.Location = new System.Drawing.Point(154, 29);
            this.txtServiceHostUrl.Name = "txtServiceHostUrl";
            this.txtServiceHostUrl.Size = new System.Drawing.Size(189, 20);
            this.txtServiceHostUrl.TabIndex = 32;
            // 
            // lblInstructions
            // 
            this.lblInstructions.Location = new System.Drawing.Point(103, 8);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(288, 54);
            this.lblInstructions.TabIndex = 25;
            this.lblInstructions.Text = "Please choose the location of your service host folder.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CodeEndeavors.ServiceHostTemplateWizards.Properties.Resources.CodeEndeavors;
            this.pictureBox1.Location = new System.Drawing.Point(12, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // NewServiceHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 308);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblInstructions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewServiceHostForm";
            this.Text = "Service Host";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVidereDir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblServiceHostUrl;
        private System.Windows.Forms.TextBox txtServiceHostDir;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtServiceHostUrl;
    }
}
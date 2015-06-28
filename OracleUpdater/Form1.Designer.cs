namespace OracleUpdater
{
    partial class Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.userTxt = new System.Windows.Forms.TextBox();
            this.passTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loginBTN = new System.Windows.Forms.Button();
            this.registerBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.upToDateLbl = new System.Windows.Forms.Label();
            this.updateBtn = new System.Windows.Forms.Button();
            this.serverSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.firewallLabel = new System.Windows.Forms.Label();
            this.servicesLabel = new System.Windows.Forms.Label();
            this.disconnectBtn = new System.Windows.Forms.Button();
            this.connectionErrorLabel = new System.Windows.Forms.Label();
            this.portsLabel = new System.Windows.Forms.Label();
            this.storeCredsCheckBox = new System.Windows.Forms.CheckBox();
            this.legacyCheck = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userTxt
            // 
            this.userTxt.AcceptsTab = true;
            this.userTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.userTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTxt.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.userTxt.Location = new System.Drawing.Point(463, 85);
            this.userTxt.Name = "userTxt";
            this.userTxt.Size = new System.Drawing.Size(295, 28);
            this.userTxt.TabIndex = 0;
            this.userTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.userTxt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // passTxt
            // 
            this.passTxt.AcceptsTab = true;
            this.passTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.passTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passTxt.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.passTxt.Location = new System.Drawing.Point(463, 163);
            this.passTxt.Name = "passTxt";
            this.passTxt.Size = new System.Drawing.Size(295, 28);
            this.passTxt.TabIndex = 1;
            this.passTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passTxt.UseSystemPasswordChar = true;
            this.passTxt.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(551, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(551, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // loginBTN
            // 
            this.loginBTN.BackColor = System.Drawing.Color.Transparent;
            this.loginBTN.FlatAppearance.BorderSize = 4;
            this.loginBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBTN.Location = new System.Drawing.Point(463, 357);
            this.loginBTN.Name = "loginBTN";
            this.loginBTN.Size = new System.Drawing.Size(295, 45);
            this.loginBTN.TabIndex = 4;
            this.loginBTN.Text = "Login";
            this.loginBTN.UseVisualStyleBackColor = false;
            this.loginBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // registerBtn
            // 
            this.registerBtn.BackColor = System.Drawing.Color.Transparent;
            this.registerBtn.FlatAppearance.BorderSize = 4;
            this.registerBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerBtn.Location = new System.Drawing.Point(649, 417);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(109, 33);
            this.registerBtn.TabIndex = 5;
            this.registerBtn.Text = "Register";
            this.registerBtn.UseVisualStyleBackColor = false;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.GreenYellow;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Version: ";
            // 
            // upToDateLbl
            // 
            this.upToDateLbl.AutoSize = true;
            this.upToDateLbl.BackColor = System.Drawing.Color.Transparent;
            this.upToDateLbl.ForeColor = System.Drawing.Color.Cyan;
            this.upToDateLbl.Location = new System.Drawing.Point(13, 26);
            this.upToDateLbl.Name = "upToDateLbl";
            this.upToDateLbl.Size = new System.Drawing.Size(35, 13);
            this.upToDateLbl.TabIndex = 8;
            this.upToDateLbl.Text = "label4";
            // 
            // updateBtn
            // 
            this.updateBtn.BackColor = System.Drawing.Color.Transparent;
            this.updateBtn.FlatAppearance.BorderSize = 4;
            this.updateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBtn.Location = new System.Drawing.Point(463, 417);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(109, 33);
            this.updateBtn.TabIndex = 9;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = false;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // serverSelectionComboBox
            // 
            this.serverSelectionComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.serverSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serverSelectionComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverSelectionComboBox.FormattingEnabled = true;
            this.serverSelectionComboBox.Items.AddRange(new object[] {
            "US East 2"});
            this.serverSelectionComboBox.Location = new System.Drawing.Point(463, 241);
            this.serverSelectionComboBox.Name = "serverSelectionComboBox";
            this.serverSelectionComboBox.Size = new System.Drawing.Size(295, 25);
            this.serverSelectionComboBox.TabIndex = 10;
            // 
            // firewallLabel
            // 
            this.firewallLabel.AutoSize = true;
            this.firewallLabel.BackColor = System.Drawing.Color.Transparent;
            this.firewallLabel.Location = new System.Drawing.Point(12, 100);
            this.firewallLabel.Name = "firewallLabel";
            this.firewallLabel.Size = new System.Drawing.Size(0, 13);
            this.firewallLabel.TabIndex = 11;
            // 
            // servicesLabel
            // 
            this.servicesLabel.AutoSize = true;
            this.servicesLabel.BackColor = System.Drawing.Color.Transparent;
            this.servicesLabel.Location = new System.Drawing.Point(12, 120);
            this.servicesLabel.Name = "servicesLabel";
            this.servicesLabel.Size = new System.Drawing.Size(0, 13);
            this.servicesLabel.TabIndex = 13;
            // 
            // disconnectBtn
            // 
            this.disconnectBtn.BackColor = System.Drawing.Color.Transparent;
            this.disconnectBtn.Enabled = false;
            this.disconnectBtn.FlatAppearance.BorderSize = 4;
            this.disconnectBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disconnectBtn.Location = new System.Drawing.Point(463, 357);
            this.disconnectBtn.Name = "disconnectBtn";
            this.disconnectBtn.Size = new System.Drawing.Size(295, 45);
            this.disconnectBtn.TabIndex = 15;
            this.disconnectBtn.Text = "Disconnect";
            this.disconnectBtn.UseVisualStyleBackColor = false;
            this.disconnectBtn.Visible = false;
            this.disconnectBtn.Click += new System.EventHandler(this.disconnectBtn_Click);
            // 
            // connectionErrorLabel
            // 
            this.connectionErrorLabel.AutoSize = true;
            this.connectionErrorLabel.BackColor = System.Drawing.Color.Transparent;
            this.connectionErrorLabel.Location = new System.Drawing.Point(603, 332);
            this.connectionErrorLabel.Name = "connectionErrorLabel";
            this.connectionErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.connectionErrorLabel.TabIndex = 14;
            this.connectionErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // portsLabel
            // 
            this.portsLabel.AutoSize = true;
            this.portsLabel.BackColor = System.Drawing.Color.Transparent;
            this.portsLabel.Location = new System.Drawing.Point(12, 140);
            this.portsLabel.Name = "portsLabel";
            this.portsLabel.Size = new System.Drawing.Size(0, 13);
            this.portsLabel.TabIndex = 16;
            // 
            // storeCredsCheckBox
            // 
            this.storeCredsCheckBox.AutoSize = true;
            this.storeCredsCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.storeCredsCheckBox.ForeColor = System.Drawing.Color.White;
            this.storeCredsCheckBox.Location = new System.Drawing.Point(463, 289);
            this.storeCredsCheckBox.Name = "storeCredsCheckBox";
            this.storeCredsCheckBox.Size = new System.Drawing.Size(112, 17);
            this.storeCredsCheckBox.TabIndex = 17;
            this.storeCredsCheckBox.Text = "Remember Login?";
            this.storeCredsCheckBox.UseVisualStyleBackColor = false;
            // 
            // legacyCheck
            // 
            this.legacyCheck.AutoSize = true;
            this.legacyCheck.BackColor = System.Drawing.Color.Transparent;
            this.legacyCheck.ForeColor = System.Drawing.Color.White;
            this.legacyCheck.Location = new System.Drawing.Point(667, 289);
            this.legacyCheck.Name = "legacyCheck";
            this.legacyCheck.Size = new System.Drawing.Size(91, 17);
            this.legacyCheck.TabIndex = 18;
            this.legacyCheck.Text = "Legacy Mode";
            this.legacyCheck.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.GreenYellow;
            this.label4.Location = new System.Drawing.Point(13, 427);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Services provided by The Catering Guild";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.legacyCheck);
            this.Controls.Add(this.storeCredsCheckBox);
            this.Controls.Add(this.portsLabel);
            this.Controls.Add(this.disconnectBtn);
            this.Controls.Add(this.connectionErrorLabel);
            this.Controls.Add(this.servicesLabel);
            this.Controls.Add(this.firewallLabel);
            this.Controls.Add(this.serverSelectionComboBox);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.upToDateLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.loginBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passTxt);
            this.Controls.Add(this.userTxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Client";
            this.Text = "nofla.me VPN Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userTxt;
        private System.Windows.Forms.TextBox passTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loginBTN;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label upToDateLbl;
        private System.Windows.Forms.Button updateBtn;
        private System.Windows.Forms.ComboBox serverSelectionComboBox;
        private System.Windows.Forms.Label firewallLabel;
        private System.Windows.Forms.Label servicesLabel;
        private System.Windows.Forms.Button disconnectBtn;
        private System.Windows.Forms.Label connectionErrorLabel;
        private System.Windows.Forms.Label portsLabel;
        private System.Windows.Forms.CheckBox storeCredsCheckBox;
        private System.Windows.Forms.CheckBox legacyCheck;
        private System.Windows.Forms.Label label4;
    }
}


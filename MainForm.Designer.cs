namespace ADSyncManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.rbFullSync = new System.Windows.Forms.RadioButton();
            this.rbDeltaSync = new System.Windows.Forms.RadioButton();
            this.btnSetCredentials = new System.Windows.Forms.Button();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblDomainName = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnSetServer = new System.Windows.Forms.Button();
            this.groupBoxActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.rbFullSync);
            this.groupBoxActions.Controls.Add(this.rbDeltaSync);
            this.groupBoxActions.Controls.Add(this.btnSetCredentials);
            this.groupBoxActions.Controls.Add(this.btnViewLogs);
            this.groupBoxActions.Controls.Add(this.btnSync);
            this.groupBoxActions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxActions.Location = new System.Drawing.Point(12, 80);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(460, 120);
            this.groupBoxActions.TabIndex = 3;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // rbFullSync
            // 
            this.rbFullSync.AutoSize = true;
            this.rbFullSync.Location = new System.Drawing.Point(168, 30);
            this.rbFullSync.Name = "rbFullSync";
            this.rbFullSync.Size = new System.Drawing.Size(80, 21);
            this.rbFullSync.TabIndex = 4;
            this.rbFullSync.TabStop = true;
            this.rbFullSync.Text = "Full Sync";
            this.rbFullSync.UseVisualStyleBackColor = true;
            // 
            // rbDeltaSync
            // 
            this.rbDeltaSync.AutoSize = true;
            this.rbDeltaSync.Checked = true;
            this.rbDeltaSync.Location = new System.Drawing.Point(12, 30);
            this.rbDeltaSync.Name = "rbDeltaSync";
            this.rbDeltaSync.Size = new System.Drawing.Size(88, 21);
            this.rbDeltaSync.TabIndex = 3;
            this.rbDeltaSync.TabStop = true;
            this.rbDeltaSync.Text = "Delta Sync";
            this.rbDeltaSync.UseVisualStyleBackColor = true;
            // 
            // btnSetCredentials
            // 
            this.btnSetCredentials.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSetCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetCredentials.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetCredentials.ForeColor = System.Drawing.Color.White;
            this.btnSetCredentials.Location = new System.Drawing.Point(324, 70);
            this.btnSetCredentials.Name = "btnSetCredentials";
            this.btnSetCredentials.Size = new System.Drawing.Size(120, 30);
            this.btnSetCredentials.TabIndex = 2;
            this.btnSetCredentials.Text = "Set Credentials";
            this.btnSetCredentials.UseVisualStyleBackColor = false;
            this.btnSetCredentials.Click += new System.EventHandler(this.btnSetCredentials_Click);
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnViewLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewLogs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewLogs.ForeColor = System.Drawing.Color.White;
            this.btnViewLogs.Location = new System.Drawing.Point(168, 70);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(150, 30);
            this.btnViewLogs.TabIndex = 1;
            this.btnViewLogs.Text = "View AD Sync Logs";
            this.btnViewLogs.UseVisualStyleBackColor = false;
            this.btnViewLogs.Click += new System.EventHandler(this.btnViewLogs_Click);
            // 
            // btnSync
            // 
            this.btnSync.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSync.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSync.ForeColor = System.Drawing.Color.White;
            this.btnSync.Location = new System.Drawing.Point(12, 70);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(150, 30);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "Run Sync";
            this.btnSync.UseVisualStyleBackColor = false;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.White;
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(12, 210);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(460, 200);
            this.txtStatus.TabIndex = 4;
            // 
            // lblDomainName
            // 
            this.lblDomainName.AutoSize = true;
            this.lblDomainName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDomainName.Location = new System.Drawing.Point(12, 20);
            this.lblDomainName.Name = "lblDomainName";
            this.lblDomainName.Size = new System.Drawing.Size(95, 17);
            this.lblDomainName.TabIndex = 5;
            this.lblDomainName.Text = "Domain Name:";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(12, 50);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(104, 17);
            this.lblServer.TabIndex = 6;
            this.lblServer.Text = "AD Sync Server:";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(122, 47);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(200, 25);
            this.txtServer.TabIndex = 7;
            // 
            // btnSetServer
            // 
            this.btnSetServer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSetServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetServer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetServer.ForeColor = System.Drawing.Color.White;
            this.btnSetServer.Location = new System.Drawing.Point(328, 45);
            this.btnSetServer.Name = "btnSetServer";
            this.btnSetServer.Size = new System.Drawing.Size(100, 30);
            this.btnSetServer.TabIndex = 8;
            this.btnSetServer.Text = "Set Server";
            this.btnSetServer.UseVisualStyleBackColor = false;
            this.btnSetServer.Click += new System.EventHandler(this.btnSetServer_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 422);
            this.Controls.Add(this.btnSetServer);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblDomainName);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.groupBoxActions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AD Sync Manager";
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button btnSetCredentials;
        private System.Windows.Forms.Button btnViewLogs;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblDomainName;
        private System.Windows.Forms.RadioButton rbFullSync;
        private System.Windows.Forms.RadioButton rbDeltaSync;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btnSetServer;
    }
}
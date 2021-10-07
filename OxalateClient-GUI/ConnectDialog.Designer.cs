
namespace OxalateClient_GUI
{
    partial class ConnectDialog
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
            this.endPointInput = new System.Windows.Forms.TextBox();
            this.Hind = new System.Windows.Forms.Label();
            this.connectLabel = new System.Windows.Forms.Label();
            this.registerLabel = new System.Windows.Forms.Label();
            this.pingLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // endPointInput
            // 
            this.endPointInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.endPointInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.endPointInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.endPointInput.ForeColor = System.Drawing.Color.White;
            this.endPointInput.Location = new System.Drawing.Point(12, 81);
            this.endPointInput.Name = "endPointInput";
            this.endPointInput.Size = new System.Drawing.Size(384, 34);
            this.endPointInput.TabIndex = 0;
            this.endPointInput.WordWrap = false;
            // 
            // Hind
            // 
            this.Hind.AutoSize = true;
            this.Hind.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.Hind.Location = new System.Drawing.Point(12, 58);
            this.Hind.Name = "Hind";
            this.Hind.Size = new System.Drawing.Size(323, 20);
            this.Hind.TabIndex = 1;
            this.Hind.Text = "IP Address, Domain and port number(optional):";
            // 
            // connectLabel
            // 
            this.connectLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.connectLabel.Location = new System.Drawing.Point(420, 15);
            this.connectLabel.Name = "connectLabel";
            this.connectLabel.Size = new System.Drawing.Size(100, 30);
            this.connectLabel.TabIndex = 2;
            this.connectLabel.Text = "Connect";
            this.connectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.connectLabel.Click += new System.EventHandler(this.OnConnectButton);
            this.connectLabel.MouseEnter += new System.EventHandler(this.LabelButtonEnter);
            this.connectLabel.MouseLeave += new System.EventHandler(this.LabelButtonLeave);
            // 
            // registerLabel
            // 
            this.registerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.registerLabel.Location = new System.Drawing.Point(420, 50);
            this.registerLabel.Name = "registerLabel";
            this.registerLabel.Size = new System.Drawing.Size(100, 30);
            this.registerLabel.TabIndex = 3;
            this.registerLabel.Text = "Register";
            this.registerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.registerLabel.Click += new System.EventHandler(this.OnRegisterButton);
            this.registerLabel.MouseEnter += new System.EventHandler(this.LabelButtonEnter);
            this.registerLabel.MouseLeave += new System.EventHandler(this.LabelButtonLeave);
            // 
            // pingLabel
            // 
            this.pingLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.pingLabel.Location = new System.Drawing.Point(420, 85);
            this.pingLabel.Name = "pingLabel";
            this.pingLabel.Size = new System.Drawing.Size(100, 30);
            this.pingLabel.TabIndex = 4;
            this.pingLabel.Text = "Ping";
            this.pingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pingLabel.Click += new System.EventHandler(this.OnPingButton);
            this.pingLabel.MouseEnter += new System.EventHandler(this.LabelButtonEnter);
            this.pingLabel.MouseLeave += new System.EventHandler(this.LabelButtonLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 41);
            this.label1.TabIndex = 5;
            this.label1.Text = "Connect To...";
            // 
            // ConnectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(528, 130);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pingLabel);
            this.Controls.Add(this.registerLabel);
            this.Controls.Add(this.connectLabel);
            this.Controls.Add(this.Hind);
            this.Controls.Add(this.endPointInput);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConnectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect To...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox endPointInput;
        private System.Windows.Forms.Label Hind;
        private System.Windows.Forms.Label connectLabel;
        private System.Windows.Forms.Label registerLabel;
        private System.Windows.Forms.Label pingLabel;
        private System.Windows.Forms.Label label1;
    }
}
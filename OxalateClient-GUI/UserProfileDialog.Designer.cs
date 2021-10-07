
namespace OxalateClient_GUI
{
    partial class UserProfileDialog
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
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(12, 62);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(78, 20);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username:";
            // 
            // usernameBox
            // 
            this.usernameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.usernameBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.usernameBox.ForeColor = System.Drawing.Color.White;
            this.usernameBox.Location = new System.Drawing.Point(96, 60);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(241, 27);
            this.usernameBox.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.Location = new System.Drawing.Point(12, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(170, 41);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "User Profile";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(17, 97);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(73, 20);
            this.passwordLabel.TabIndex = 0;
            this.passwordLabel.Text = "Password:";
            // 
            // passwordBox
            // 
            this.passwordBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.passwordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passwordBox.ForeColor = System.Drawing.Color.White;
            this.passwordBox.Location = new System.Drawing.Point(96, 95);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(241, 27);
            this.passwordBox.TabIndex = 2;
            // 
            // UserProfileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(356, 140);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.usernameLabel);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UserProfileDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Profile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveUserProfile);
            this.Shown += new System.EventHandler(this.LoadData);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.MaskedTextBox passwordBox;
    }
}

namespace OxalateClient_GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.connectLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.verLabel = new System.Windows.Forms.Label();
            this.recvContainer = new System.Windows.Forms.GroupBox();
            this.receiveBox = new System.Windows.Forms.RichTextBox();
            this.inputContainer = new System.Windows.Forms.GroupBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.disconnectLabel = new System.Windows.Forms.Label();
            this.recvContainer.SuspendLayout();
            this.inputContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectLabel
            // 
            this.connectLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            resources.ApplyResources(this.connectLabel, "connectLabel");
            this.connectLabel.Name = "connectLabel";
            this.connectLabel.Click += new System.EventHandler(this.ShowConnectDialog);
            this.connectLabel.MouseEnter += new System.EventHandler(this.LabelButtonEnter);
            this.connectLabel.MouseLeave += new System.EventHandler(this.LabelButtonLeave);
            // 
            // userLabel
            // 
            this.userLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            resources.ApplyResources(this.userLabel, "userLabel");
            this.userLabel.Name = "userLabel";
            this.userLabel.Click += new System.EventHandler(this.ShowUserProfileDialog);
            this.userLabel.MouseEnter += new System.EventHandler(this.LabelButtonEnter);
            this.userLabel.MouseLeave += new System.EventHandler(this.LabelButtonLeave);
            // 
            // verLabel
            // 
            this.verLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.verLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            resources.ApplyResources(this.verLabel, "verLabel");
            this.verLabel.Name = "verLabel";
            this.verLabel.Click += new System.EventHandler(this.ShowAboutScreen);
            // 
            // recvContainer
            // 
            this.recvContainer.Controls.Add(this.receiveBox);
            this.recvContainer.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.recvContainer, "recvContainer");
            this.recvContainer.Name = "recvContainer";
            this.recvContainer.TabStop = false;
            // 
            // receiveBox
            // 
            this.receiveBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.receiveBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.receiveBox.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.receiveBox, "receiveBox");
            this.receiveBox.ForeColor = System.Drawing.Color.White;
            this.receiveBox.Name = "receiveBox";
            this.receiveBox.ReadOnly = true;
            // 
            // inputContainer
            // 
            this.inputContainer.Controls.Add(this.inputBox);
            this.inputContainer.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.inputContainer, "inputContainer");
            this.inputContainer.Name = "inputContainer";
            this.inputContainer.TabStop = false;
            // 
            // inputBox
            // 
            this.inputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.inputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.inputBox, "inputBox");
            this.inputBox.ForeColor = System.Drawing.Color.White;
            this.inputBox.Name = "inputBox";
            this.inputBox.TextChanged += new System.EventHandler(this.CheckInputBox);
            // 
            // disconnectLabel
            // 
            this.disconnectLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            resources.ApplyResources(this.disconnectLabel, "disconnectLabel");
            this.disconnectLabel.Name = "disconnectLabel";
            this.disconnectLabel.Click += new System.EventHandler(this.Disconnect);
            this.disconnectLabel.MouseEnter += new System.EventHandler(this.LabelButtonEnter);
            this.disconnectLabel.MouseLeave += new System.EventHandler(this.LabelButtonLeave);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.inputContainer);
            this.Controls.Add(this.recvContainer);
            this.Controls.Add(this.verLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.connectLabel);
            this.Controls.Add(this.disconnectLabel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Resize += new System.EventHandler(this.AdjustForm);
            this.recvContainer.ResumeLayout(false);
            this.inputContainer.ResumeLayout(false);
            this.inputContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label verLabel;
        private System.Windows.Forms.GroupBox recvContainer;
        private System.Windows.Forms.GroupBox inputContainer;
        private System.Windows.Forms.TextBox inputBox;
        public System.Windows.Forms.RichTextBox receiveBox;
        private System.Windows.Forms.Label disconnectLabel;
        public System.Windows.Forms.Label connectLabel;
    }
}


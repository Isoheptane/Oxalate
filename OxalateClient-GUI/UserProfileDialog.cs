using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oxalate.Standard;

namespace OxalateClient_GUI
{
    public partial class UserProfileDialog : Form
    {
        MainForm mainForm;
        public UserProfileDialog(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void SaveUserProfile(object sender, FormClosingEventArgs e)
        {
            if (!UsernameCheck.IsLegalUsername(usernameBox.Text))
            {
                MessageBox.Show("Username is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }

            mainForm.preference.Username = usernameBox.Text;
            mainForm.preference.Password = passwordBox.Text;
            mainForm.ClientLoadUserProfile();
        }

        private void LoadData(object sender, EventArgs e)
        {
            usernameBox.Text = mainForm.preference.Username;
            passwordBox.Text = mainForm.preference.Password;
        }
    }
}

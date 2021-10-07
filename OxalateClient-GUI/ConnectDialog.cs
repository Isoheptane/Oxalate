using System;
using System.Net;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oxalate.Standard;

namespace OxalateClient_GUI
{
    public partial class ConnectDialog : Form
    {
        MainForm parentForm;
        Preference preference;
        public ConnectDialog(MainForm parentForm, Preference preference)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.preference = preference;
        }

        private void LabelButtonEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(63, 63, 63);
        }

        private void LabelButtonLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(31, 31, 31);
        }

        private IPEndPoint ParseIPEndPoint(string str)
        {
            IPAddress address;
            if (IPAddress.TryParse(str, out address))
            {
                return new IPEndPoint(address, 7376);
            }
            IPEndPoint endPoint;
            if (IPEndPoint.TryParse(str, out endPoint))
            {
                return endPoint;
            }
            if (!str.Contains(':'))
            {
                return new IPEndPoint(Dns.GetHostEntry(str).AddressList[0], 7376);
            }
            if (str.Contains(':'))
            {
                int spliter = str.LastIndexOf(':');
                string domain = str.Substring(0, spliter);
                int port = int.Parse(str.Substring(spliter + 1));
                return new IPEndPoint(Dns.GetHostEntry(domain).AddressList[0], port);
            }
            return null;
        }

        private void OnRegisterButton(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint endPoint = ParseIPEndPoint(endPointInput.Text);
                Packet response = parentForm.client.Register(endPoint);
                if (response["accepted"])
                {
                    TextBoxIO.Print(parentForm.receiveBox, response["message"], preference.ColorTheme);
                    parentForm.receiveBox.AppendText("\n");
                    parentForm.connectLabel.Visible = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Server: {response["message"]}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnConnectButton(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint endPoint = ParseIPEndPoint(endPointInput.Text);
                Packet response = parentForm.client.Connect(endPoint);
                if (response["accepted"])
                {
                    TextBoxIO.Print(parentForm.receiveBox, response["message"], preference.ColorTheme);
                    parentForm.receiveBox.AppendText("\n");
                    parentForm.connectLabel.Visible = false;
                    this.Close();
                } 
                else
                {
                    MessageBox.Show($"Server: {response["message"]}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnPingButton(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint endPoint = ParseIPEndPoint(endPointInput.Text);
                Packet info = parentForm.client.Ping(endPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

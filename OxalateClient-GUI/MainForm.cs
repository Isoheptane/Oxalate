using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OxalateClientTcp;
using Oxalate.Standard;

namespace OxalateClient_GUI
{
    public partial class MainForm : Form
    {

        public ClientTcp client;
        public Preference preference;

        ConnectDialog connectDialog;
        UserProfileDialog profileDialog;

        public MainForm(Preference preference)
        {
            InitializeComponent();
            this.preference = preference;

            client = new ClientTcp("", "");
            connectDialog = new ConnectDialog(this, preference);
            profileDialog = new UserProfileDialog(this);
            client.ReceivedMessage += OnMessageReceive;
            client.ErrorOccured += OnErrorOccur;
            client.ExceptionOccured += OnExceptionOccur;

            ClientLoadUserProfile();
        }

        private void LabelButtonEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(63, 63, 63);
        }

        private void LabelButtonLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(31, 31, 31);
        }

        private void AdjustForm(object sender, EventArgs e)
        {
            verLabel.Width = Width - 278;
            recvContainer.Width = Width - 28;
            recvContainer.Height = Height - 182;
            inputContainer.Width = Width - 28;
            inputContainer.Location = new Point(inputContainer.Location.X, Height - 147);
        }

        private void ShowConnectDialog(object sender, EventArgs e)
        {
            connectDialog.ShowDialog();
        }
        private void ShowUserProfileDialog(object sender, EventArgs e)
        {
            profileDialog.ShowDialog();
        }
        public void ClientLoadUserProfile()
        {
            client.Username = preference.Username;
            var sha256 = System.Security.Cryptography.SHA256.Create();

            byte[] hash = Encoding.UTF8.GetBytes(preference.Password);
            for (int cycles = 0; cycles < 7; cycles++)
                hash = sha256.ComputeHash(hash);
            sha256.Dispose();

            client.Password = Convert.ToBase64String(hash);
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            receiveBox.Font = preference.ChatFont;
            receiveBox.SelectionFont = preference.ChatFont;

            receiveBox.ForeColor = preference.ColorTheme.CodedColor[7];
            receiveBox.SelectionColor = preference.ColorTheme.CodedColor[7];
            receiveBox.SelectionBackColor = preference.ColorTheme.CodedColor[0];
        }

        private void CheckInputBox(object sender, EventArgs e)
        {
            if (inputBox.Text.Contains('\n'))
            {
                string rawInstruction = inputBox.Text.Replace("\n", "").Trim();
                inputBox.Text = "";

                if (rawInstruction == "")
                {
                    return;
                }
                if (rawInstruction[0] == '/' || rawInstruction[0] == '!')
                {
                    CommandCall commandCall = CommandCall.Parse(rawInstruction.Substring(1));
                    if (rawInstruction[0] == '/')
                    {
                        if (client.Connected)
                        {
                            client.Send(commandCall.ToPacket());
                        }
                        else
                        {
                            TextBoxIO.Print(receiveBox, "\\crServer is not currently connected.\n", preference.ColorTheme);
                        }
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    if (client.Connected)
                    {
                        client.Send(new CommandCall("say", rawInstruction).ToPacket());
                    }
                    else
                    {
                        TextBoxIO.Print(receiveBox, "\\crServer is not currently connected.\n", preference.ColorTheme);
                    }
                }
            }
        }

        void Disconnect()
        {
            try
            {
                if (client.Connected)
                {
                    client.Send(new CommandCall("disconnect").ToPacket());
                }
            }
            finally
            {
                if (client.Connected)
                {
                    client.Disconnect();
                }
            }
        }

        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            Disconnect();
            File.WriteAllText("config.json", preference.ToJsonObject().Serialize("", "  "));
        }

        private void Disconnect(object sender, EventArgs e)
        {
            Disconnect();
            connectLabel.Visible = true;
            TextBoxIO.Print(receiveBox, "\\arDisconnected.\n", preference.ColorTheme);
        }
    }
}

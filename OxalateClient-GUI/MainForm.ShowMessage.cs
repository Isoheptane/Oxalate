using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using OxalateClientTcp;
using Oxalate.Standard;

namespace OxalateClient_GUI
{
    public partial class MainForm : Form
    {
        void OnErrorOccur(object sender, ErrorOccurEventArgs e)
        {
            receiveBox.BeginInvoke(new Action(() =>
            {
                TextBoxIO.Print(receiveBox, $"\\crError occured: {ScreenIO.Escape(e.Message)}\n", preference.ColorTheme);
            }));
        }

        void OnExceptionOccur(object sender, ExceptionOccurEventArgs e)
        {
            receiveBox.BeginInvoke(new Action(() =>
            {
                TextBoxIO.Print(receiveBox, $"\\crUnexpected excetion: {ScreenIO.Escape(e.Exception.ToString())}\n", preference.ColorTheme);
            }));
        }

        void ShowChatMessage(Packet packet)
        {
            TextBoxIO.Print(
                receiveBox,
                preference.ChatMessage
                    .Replace("$USER", packet["username"])
                    .Replace("$NICK", packet["nickname"])
                    .Replace("$MESSAGE", packet["message"])
                ,
                preference.ColorTheme
            );
        }

        void ShowWhisperMessage(Packet packet)
        {
            TextBoxIO.Print(
                receiveBox,
                preference.WhisperMessage
                    .Replace("$USER", packet["username"])
                    .Replace("$NICK", packet["nickname"])
                    .Replace("$MESSAGE", packet["message"])
                ,
                preference.ColorTheme
            );
        }

        void ShowActionMessage(Packet packet)
        {
            TextBoxIO.Print(
                receiveBox,
                preference.ActionMessage
                    .Replace("$USER", packet["username"])
                    .Replace("$NICK", packet["nickname"])
                    .Replace("$MESSAGE", packet["message"])
                ,
                preference.ColorTheme
            );
        }

        void ShowBroadcastMessage(Packet packet)
        {
            TextBoxIO.Print(
                receiveBox,
                preference.BroadcastMessage
                    .Replace("$MESSAGE", packet["message"])
                ,
                preference.ColorTheme
            );
        }

        void ShowServerMessage(Packet packet)
        {
            TextBoxIO.Print(
                receiveBox,
                preference.ServerMessage
                    .Replace("$MESSAGE", packet["message"])
                ,
                preference.ColorTheme
            );
        }

        void OnMessageReceive(object sender, ReceiveMessageEventArgs e)
        {
            Packet packet = e.ReceivedPacket;
            string type = packet["messageType"];
            switch (type)
            {
                case "chat":
                    receiveBox.BeginInvoke(new Action(() => { ShowChatMessage(packet); }));
                    break;
                case "whisper":
                    receiveBox.BeginInvoke(new Action(() => { ShowWhisperMessage(packet); }));
                    break;
                case "action":
                    receiveBox.BeginInvoke(new Action(() => { ShowActionMessage(packet); }));
                    break;
                case "broadcast":
                    receiveBox.BeginInvoke(new Action(() => { ShowBroadcastMessage(packet); }));
                    break;
                case "server":
                    receiveBox.BeginInvoke(new Action(() => { ShowServerMessage(packet); }));
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using JsonSharp;

namespace OxalateClient_GUI
{
    public class ColorTheme
    {
        public Color[] CodedColor { get; set; }
        public ColorTheme()
        {
            CodedColor = new Color[16];
            CodedColor[0] = Color.FromArgb(15, 15, 15);
            CodedColor[1] = Color.FromArgb(0, 55, 218);
            CodedColor[2] = Color.FromArgb(19, 161, 14);
            CodedColor[3] = Color.FromArgb(58, 150, 221);
            CodedColor[4] = Color.FromArgb(197, 15, 31);
            CodedColor[5] = Color.FromArgb(136, 23, 152);
            CodedColor[6] = Color.FromArgb(193, 156, 0);
            CodedColor[7] = Color.FromArgb(204, 204, 204);
            CodedColor[8] = Color.FromArgb(118, 118, 118);
            CodedColor[9] = Color.FromArgb(59, 120, 255);
            CodedColor[10] = Color.FromArgb(22, 198, 12);
            CodedColor[11] = Color.FromArgb(97, 214, 214);
            CodedColor[12] = Color.FromArgb(231, 72, 86);
            CodedColor[13] = Color.FromArgb(180, 0, 158);
            CodedColor[14] = Color.FromArgb(249, 241, 165);
            CodedColor[15] = Color.FromArgb(242, 242, 242);
        }
        public ColorTheme(JsonArray colors)
        {
            CodedColor = new Color[16];
            for (int i = 0; i < 16; i++)
                CodedColor[i] = Color.FromArgb(colors[i][0], colors[i][1], colors[i][2]);
        }
        public JsonArray ToJsonArray()
        {
            JsonArray colors = new JsonArray();
            for (int i = 0; i < 16; i++)
            {
                JsonArray subArray = new JsonArray();
                subArray.elements.Add((int)CodedColor[i].R);
                subArray.elements.Add((int)CodedColor[i].G);
                subArray.elements.Add((int)CodedColor[i].B);
                colors.elements.Add(subArray);
            }
            return colors;
        }
    }
    public class Preference
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ColorTheme ColorTheme { get; set; }
        public Font ChatFont { get; set; }
        public string ChatMessage { get; set; }
        public string ActionMessage { get; set; }
        public string WhisperMessage { get; set; }
        public string BroadcastMessage { get; set; }
        public string ServerMessage { get; set; }

        /// <summary>
        /// Default preference
        /// </summary>
        public Preference()
        {
            Username = "username";
            Password = "password";
            ColorTheme = new ColorTheme();
            ChatFont = new Font("Microsoft YaHei", 9.0f);
            ChatMessage         = "<$NICK\\rr> $MESSAGE\n";
            ActionMessage       = "* $NICK\\rr $MESSAGE\n";
            WhisperMessage      = "\\8r$NICK\\8r -> $MESSAGE\n";
            BroadcastMessage    = "[Broadcast] $MESSAGE\n";
            ServerMessage       = "$MESSAGE\n";
        }
        public Preference(JsonObject profile)
        {
            Username = profile["username"];
            Password = profile["password"];
            ColorTheme = new ColorTheme(profile["colors"]);
            string fontFamily = profile["font"]["fontFamily"];
            float fontSize = (float)(decimal)profile["font"]["fontSize"];
            try
            {
                ChatFont = new Font(fontFamily, fontSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ChatMessage         = profile["chatMessage"];
            ActionMessage       = profile["actionMessage"];
            WhisperMessage      = profile["whisperMessage"];
            BroadcastMessage    = profile["broadcastMessage"];
            ServerMessage       = profile["serverMessage"];
        }

        public JsonObject ToJsonObject()
        {
            JsonObject jsonObject = new JsonObject();

            jsonObject["username"] = Username;
            jsonObject["password"] = Password;
            jsonObject["colors"] = ColorTheme.ToJsonArray();

            JsonObject fontObject = new JsonObject();
            fontObject["fontFamily"] = ChatFont.Name;
            fontObject["fontSize"] = (decimal)ChatFont.Size;
            jsonObject["font"] = fontObject;

            jsonObject["chatMessage"]       = ChatMessage;
            jsonObject["actionMessage"]     = ActionMessage;
            jsonObject["whisperMessage"]    = WhisperMessage;
            jsonObject["broadcastMessage"]  = BroadcastMessage;
            jsonObject["serverMessage"]     = ServerMessage;

            return jsonObject;
        }
    }
}

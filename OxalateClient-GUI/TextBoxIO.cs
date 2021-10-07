using System;
using System.Drawing;
using System.Windows.Forms;
using Oxalate.Standard;

namespace OxalateClient_GUI
{
    public static class TextBoxIO
    {

        public static Color ConvertColor(char ch, ColorTheme theme, Color resetColor)
        {
            if (ch >= '0' && ch <= '9')
                return theme.CodedColor[ch - '0'];
            if (ch >= 'a' && ch <= 'f')
                return theme.CodedColor[ch - 'a' + 10];
            if (ch >= 'A' && ch <= 'F')
                return theme.CodedColor[ch - 'F' + 10];
            if (ch == 'r' || ch == 'R')
                return resetColor;
            return Color.Transparent;
        }

        public static void Print(RichTextBox textbox, string message, ColorTheme theme)
        {

            lock (textbox)
            {
                Color foregroundColor = textbox.SelectionColor;
                Color backgroundColor = textbox.SelectionBackColor;

                int length = message.Length;
                for (int i = 0; i < length; i++)
                {
                    if (message[i] == '\\')
                    {
                        if (i + 1 < length && message[i + 1] == '\\')
                        {
                            textbox.AppendText(message[i].ToString());
                            i += 1;
                        }
                        else if (
                            i + 2 < length &&
                            ScreenIO.IsLegalColorFormatter(message[i + 1]) &&
                            ScreenIO.IsLegalColorFormatter(message[i + 2])
                        )
                        {
                            textbox.SelectionColor = ConvertColor(message[i + 1], theme, foregroundColor);
                            textbox.SelectionBackColor = ConvertColor(message[i + 2], theme, backgroundColor);
                            i += 2;
                        }
                        else if (
                            i + 1 < length &&
                            ScreenIO.IsLegalColorFormatter(message[i + 1])
                        )
                        {
                            textbox.SelectionColor = ConvertColor(message[i + 1], theme, foregroundColor);
                            i += 1;
                        }
                        else
                        {
                            textbox.AppendText(message[i].ToString());
                        }
                    }
                    else
                    {
                        if (message[i] != '\r')
                            textbox.AppendText(message[i].ToString());
                    }
                }
                textbox.SelectionColor = foregroundColor;
                textbox.SelectionBackColor = backgroundColor;
            }

        }
    }
}

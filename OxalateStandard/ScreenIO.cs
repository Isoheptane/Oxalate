using System;
using System.Collections.Generic;
using System.Text;

namespace Oxalate.Standard
{
    public static class ScreenIO
    {
        public static string Escape(string str)
        {
            return str.Replace("\\", "\\\\");
        }

        public static bool IsLegalColorFormatter(char ch)
        {
            return
                (ch >= '0' && ch <= '9') ||
                (ch >= 'a' && ch <= 'f') ||
                (ch >= 'A' && ch <= 'F') ||
                (ch == 'r' || ch == 'R');
        }

        public static ConsoleColor ConvertColor(char ch, ConsoleColor resetColor)
        {
            if (ch >= '0' && ch <= '9')
                return (ConsoleColor)(ch - '0');
            if (ch >= 'a' && ch <= 'f')
                return (ConsoleColor)(ch - 'a' + 10);
            if (ch >= 'A' && ch <= 'F')
                return (ConsoleColor)(ch - 'F' + 10);
            if (ch == 'r' || ch == 'R')
                return resetColor;
            return (ConsoleColor)(-1);
        }

        static string CurrentTimeString
        {
            get { return DateTime.Now.ToString("HH:mm:ss"); }
        }

        static object printLock = new object();

        /// <summary>
        /// Print a color formatted message to the screen.
        /// </summary>
        /// <param name="message">Color formatted message</param>
        public static void Print(string message)
        {
            lock (printLock)
            {
                var foregroundColor = Console.ForegroundColor;
                var backgroundColor = Console.BackgroundColor;

                int length = message.Length;
                for (int i = 0; i < length; i++)
                {
                    if (message[i] == '\\')
                    {
                        if (i + 1 < length && message[i + 1] == '\\')
                        {
                            Console.Write(message[i]);
                            i += 1;
                        }
                        else if (
                            i + 2 < length &&
                            IsLegalColorFormatter(message[i + 1]) &&
                            IsLegalColorFormatter(message[i + 2])
                        )
                        {
                            Console.ForegroundColor = ConvertColor(message[i + 1], foregroundColor);
                            Console.BackgroundColor = ConvertColor(message[i + 2], backgroundColor);
                            i += 2;
                        }
                        else if (
                            i + 1 < length &&
                            IsLegalColorFormatter(message[i + 1])
                        )
                        {
                            Console.ForegroundColor = ConvertColor(message[i + 1], foregroundColor);
                            i += 1;
                        }
                        else
                        {
                            Console.Write(message[i]);
                        }
                    }
                    else
                    {
                        Console.Write(message[i]);
                    }
                }

                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
            }
        }

        /// <summary>
        /// Write a info message.
        /// </summary>
        public static void Info(string message) => Print($"\\rr[{CurrentTimeString} INFO]\\rr {message}\\rr\n");

        /// <summary>
        /// Write a warning message.
        /// </summary>
        public static void Warn(string message) => Print($"\\er[{CurrentTimeString} WARN]\\rr {message}\\rr\n");

        /// <summary>
        /// Write a error message.
        /// </summary>
        public static void Error(string message) => Print($"\\cr[{CurrentTimeString} ERROR]\\rr {message}\\rr\n");

        /// <summary>
        /// Write a debug message.
        /// </summary>
        public static void Debug(string message) => Print($"\\rr[{CurrentTimeString} DEBUG]\\rr {message}\\rr\n");

    }
}

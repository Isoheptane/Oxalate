using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using JsonSharp;
using Oxalate.Standard;

namespace Oxalate.Server
{
    class Program
    {
        static Server server;

        static void OnCancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            server.Stop();
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            ScreenIO.Warn("Oxalate is still under development, use it at your own risk.");

            JsonObject configFile = JsonObject.Parse(File.ReadAllText("config.json"));

            server = new Server();

            server.Name = configFile["name"];
            List<string> descriptionLines = new List<string>();
            foreach (JsonValue line in ((JsonArray)configFile["description"]).elements)
                descriptionLines.Add(line);
            server.Description = descriptionLines.ToArray();
            server.MaxOnline = configFile["maxOnline"];
            server.Language = configFile["language"];
            server.Timeout = configFile["timeout"];

            server.Start();

            Console.CancelKeyPress += OnCancelKeyPressed;

            while (true)
                Thread.Sleep(100);
        }
    }
}

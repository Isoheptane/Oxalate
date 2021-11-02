using System;
using System.IO;
using System.Reflection;
using JsonSharp;
using Oxalate.Server;
using Oxalate.Standard;

namespace OxalateTcpInterface
{
    public class TcpInterface : IPlugin
    {
        public string PluginName
        {
            get { return "OxalateTCPInterface"; }
        }

        public string PluginFullname
        {
            get { return "Oxalate TCP Server Interface"; }
        }

        public string PluginVersion
        {
            get { return "1.0"; }
        }

        public string PluginDescription
        {
            get {
                return "Provides TCP server support.";
            }
        }

        public PluginAPI API { get; set; }

        public Translation Translation { get; set; }

        public JsonObject Config { get; set; }

        TcpServer server;

        void LoadLanguageFile(string language)
        {
            Translation newTranslation = new Translation();
            JsonObject langFile = API.LoadConfigFile($"{language}.lang.json");
            if (langFile == null)
                langFile = API.LoadConfigFile("default.lang.json");
            newTranslation.LoadLangFile(langFile);
            Translation = newTranslation;
        }

        public void EnablePlugin(PluginAPI api)
        {
            API = api;

            LoadLanguageFile(API.ServerLanguage);
            Config = API.LoadConfigFile("config.json");

            if (Config["tcpServer"]["enabled"])
            {
                try
                {
                    server = new TcpServer(this, Config["tcpServer"]["port"]);
                    server.AllowRegister = Config["tcpServer"]["allowRegister"];
                    server.StartServer();
                }
                catch (Exception ex)
                {
                    ScreenIO.Error(ScreenIO.Escape(Translation["listener.failStart"].Replace("$MESSAGE", ex.ToString())));
                } 
            }
        }

        public void ReloadPlugin()
        {
            
        }

        public void DisablePlugin()
        {
            server.StopServer();
        }
    }
}

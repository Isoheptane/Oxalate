using System;
using System.IO;
using System.Reflection;
using JsonSharp;
using Oxalate.Server;
using Oxalate.Standard;

namespace OxalateCorePlugin
{
    public class OxalateCore : IPlugin
    {
        public string PluginName
        {
            get { return "oxalateCore"; }
        }

        public string PluginFullname
        {
            get { return "Oxalate Core Function Collection"; }
        }

        public string PluginVersion
        {
            get { return "b0.0.1"; }
        }

        public string PluginDescription
        {
            get {
                return "A plugin that provides the basic functions.";
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

            API.RegisterCommand(new SayCommand(this, "say", Config["commands"]["say"]["usage"], Config["commands"]["say"]));
            API.RegisterCommand(new TellCommand(this, "tell", Config["commands"]["tell"]["usage"], Config["commands"]["tell"]));
            API.RegisterCommand(new ActionCommand(this, "action", Config["commands"]["action"]["usage"], Config["commands"]["action"]));

            if (Config["tcpServer"]["enabled"])
            {
                server = new TcpServer(this, Config["tcpServer"]["port"]);
                server.AllowRegister = Config["tcpServer"]["allowRegister"];
                server.StartServer();
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

using System;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;
using JsonSharp;
using Oxalate.Standard;
using static Oxalate.Standard.ScreenIO;

namespace Oxalate.Server
{
    public partial class Server
    {
        bool serverOn;
        string name;
        string[] description;

        int maxOnline;

        string language;
        Translation translation;

        PluginManager pluginManager;

        int timeout;

        public bool IsRunning
        {
            get { return serverOn; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string[] Description
        {
            get { return description; }
            set { description = value; }
        }

        public int MaxOnline
        {
            get { return maxOnline; }
            set { maxOnline = value; }
        }

        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        public Translation Translation 
        {
            get { return translation; }
        }

        public PluginManager PluginManager
        {
            get { return pluginManager; }
        }

        /// <summary>
        /// Keep-Alive timeout.
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        public Server()
        {
            serverOn = false;
        }

        /// <summary>
        /// Load indicated ""
        /// </summary>
        /// <param name="path"></param>
        public void LoadLanguageFile(string path)
        => Translation.LoadLangFile(JsonObject.Parse(File.ReadAllText(path)));

        /// <summary>
        /// Start server
        /// </summary>
        public void Start()
        {
            Info($"\\erServer name:\\rr {Name}");
            Info("\\erServer description:");
            foreach (string line in description)
                Info($" - {line}");
            Info($"\\erMax Online Users:\\rr {MaxOnline}");
            Info($"\\erServer Language:\\rr {Language}");

            //  Load translation
            translation = new Translation();
            LoadLanguageFile($"{Language}.lang.json");

            //  Load user profile
            users = new ConcurrentDictionary<string, User>();
            onlineUsers = new ConcurrentDictionary<string, OnlineUser>();

            Info(Translation["server.loadUserProfile"].Replace("$COUNT", LoadUsersProfile().ToString()));

            //  Load plugins
            registeredCommands = new ConcurrentDictionary<string, Command>();
            pluginManager = new PluginManager(this);
            string[] pluginPaths = Directory.GetFiles("plugins/");
            foreach (string path in pluginPaths)
                if (path.ToLower().EndsWith(".dll"))
                    PluginManager.LoadPlugin(path);

            foreach (var plugin in PluginManager.PluginList)
                PluginManager.EnablePlugin(plugin.Key);
            
            Info(Translation["server.loadPlugins"].Replace("$COUNT", PluginManager.PluginList.Count.ToString()));

            serverOn = true;
        }

        public void Stop()
        {
            serverOn = false;
            foreach (var plugin in PluginManager.PluginList)
            {
                plugin.Value.Disable();
            }
            Info(Translation["server.saveUserProfile"].Replace("$COUNT", SaveUsersProfile().ToString()));
            Info(Translation["server.stop"]);
        }

    }
}

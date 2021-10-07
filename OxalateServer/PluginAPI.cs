using System;
using System.IO;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;
using JsonSharp;

namespace Oxalate.Server
{
    public class PluginAPI
    {
        Server server;
        string pluginName;
        
        /// <summary>
        /// Initialize a Plugin API instance.
        /// </summary>
        public PluginAPI(Server server, string pluginName)
        {
            this.server = server;
            this.pluginName = pluginName;
        }

        /// <summary>
        /// Get a clone of the registered command list of the server.
        /// </summary>
        public Dictionary<string, Command> Commands
        {
            get
            {
                return server.Commands.ToDictionary(entry => entry.Key, entry => entry.Value);
            }
        }

        /// <summary>
        /// Register a new command.
        /// </summary>
        public void RegisterCommand(Command command)
        {
            server.Commands.TryAdd(command.Name, command);
        }

        /// <summary>
        /// Delete a registered command.
        /// </summary>
        public void UnregisterCommand(string commandName) 
        {
            Command command;
            server.Commands.TryRemove(commandName, out command);
        }

        /// <summary>
        /// Get a registered command.
        /// </summary>
        public Command GetCommand(string commandName)
        {
            return server.Commands[commandName];
        }
        
        public string ServerName
        {
            get { return server.Name; }
            set { server.Name = value; }
        }

        public string[] Description
        {
            get { return server.Description; }
            set { server.Description = value; }
        }

        /// <summary>
        /// Max online user count.
        /// </summary>
        public int OnlineCount
        {
            get { return server.OnlineUsers.Count; }
        }

        /// <summary>
        /// Max online user count.
        /// </summary>
        public int MaxOnlineCount
        {
            get { return server.MaxOnline; }
        }

        /// <summary>
        /// Server language setting.
        /// </summary>
        public string ServerLanguage
        {
            get { return server.Language; }
        }

        public ConcurrentDictionary<string, User> Users
        {
            get { return server.Users; }
        }

        public ConcurrentDictionary<string, OnlineUser> OnlineUsers
        {
            get { return server.OnlineUsers; }
        }

        /// <summary>
        /// Register a user on the server.
        /// </summary>
        public void RegisterUser(string username, string password)
        {
            server.RegisterUser(username, password);
        }

        /// <summary>
        /// Connect a user to the sever.
        /// </summary>
        public void ConnectUser(string username, OnlineUser user)
        {
            server.ConnectUser(username, user);
        }

        /// <summary>
        /// Force disconnect a user.
        /// </summary>
        public void DisconnectUser(string username)
        {
            server.DisconnectUser(username);
        }

        /// <summary>
        /// Execute command as user.
        /// </summary>
        public void ExecuteCommand(OnlineUser user, CommandCall command)
        {
            server.ExecuteCommand(user, command);
        }

        /// <summary>
        /// Read configure file from plugin's directory.
        /// </summary>
        public JsonObject LoadConfigFile(string fileName)
        {
            if (!File.Exists($"plugins/{pluginName}/{fileName}"))
                return null;
            return JsonObject.Parse(File.ReadAllText($"plugins/{pluginName}/{fileName}"));
        }

        /// <summary>
        /// Write configure file from plugin's directory.
        /// </summary>
        public void SaveConfigFile(string fileName, JsonObject config)
        {
            File.WriteAllText(fileName, config.Serialize("", "  "));
        }

        /// <summary>
        /// Send a packet to indicated user.
        /// </summary>
        public void SendPacket(Packet packet, OnlineUser user) => server.SendPacket(packet, user);

        /// <summary>
        /// Send a packet to all users.
        /// </summary>
        public void BroadcastPacket(Packet packet) => server.BroadcastPacket(packet);

        /// <summary>
        /// Send a server message to user.
        /// </summary>
        public void SendServerMessage(string message, OnlineUser user) => server.SendServerMessage(message, user);

        /// <summary>
        /// Broadcast a message to all users.
        /// </summary>
        /// <param name="message"></param>
        public void BroadcastMessage(string message) => server.BroadcastMessage(message);

    }
}

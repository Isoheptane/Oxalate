using System;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;

namespace Oxalate.Server
{
    public abstract class OnlineUser
    {
        protected bool connected;
        string username;
        string nickname;

        User backendUser;

        IPlugin plugin;

        public bool Connected
        {
            get { return connected; }
        }

        public string Username
        {
            get { return username; }
        }

        public string Nickname
        {
            get { return nickname; }
        }

        public User BackendUser
        {
            get { return backendUser; }
        }

        public IPlugin Plugin
        {
            get { return plugin; }
        }

        /// <summary>
        /// Create a OnlineUser instance with indicated username and Server Plugin API instance.
        /// </summary>
        public OnlineUser(string username, IPlugin plugin)
        {
            connected = true;
            this.username = username;
            this.plugin = plugin;
            nickname = Plugin.API.Users[username].Nickname;
            backendUser = Plugin.API.Users[username];
        }

        /// <summary>
        /// Disconnect the user.
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Send a packet to the user.
        /// </summary>
        public abstract void SendPacket(Packet packet);

        /// <summary>
        /// Receive a packet from the 
        /// </summary>
        /// <returns></returns>
        public abstract Packet ReceivePacket();
    }
}

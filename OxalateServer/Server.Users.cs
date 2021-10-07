using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using JsonSharp;
using Oxalate.Standard;

namespace Oxalate.Server
{
    public partial class Server
    {
        ConcurrentDictionary<string, User> users;

        ConcurrentDictionary<string, OnlineUser> onlineUsers;

        int defaultPermissionLevel;

        public int DefaultPermissionLevel
        {
            get { return defaultPermissionLevel; }
            set { defaultPermissionLevel = value; }
        }

        public ConcurrentDictionary<string, User> Users
        {
            get { return users; }
        }

        public ConcurrentDictionary<string, OnlineUser> OnlineUsers
        {
            get { return onlineUsers; }
        }

        /// <summary>
        /// Connect user to the server.
        /// </summary>
        /// <param name="client">Connection TcpClient instance</param>
        public void ConnectUser(string username, OnlineUser onlineUser)
        {
            OnlineUsers.TryAdd(username, onlineUser);
            Users[username].LastLoginTime = DateTime.Now;
        }

        /// <summary>
        /// Disconnect a connected user.
        /// </summary>
        public void DisconnectUser(string username)
        {
            OnlineUser user;
            OnlineUsers.TryRemove(username, out user);
            user.Disconnect();
        }
        
        /// <summary>
        /// Register a new user.
        /// </summary>
        public void RegisterUser(string username, string password)
        {
            User user = new User(username, password);
            Users.TryAdd(username, user);
        }

        public int LoadUsersProfile()
        {
            int count = 0;
            foreach (string path in Directory.GetFiles("userdata/"))
            {
                try
                {
                    User user = User.CreateFromProfile(JsonObject.Parse(File.ReadAllText(path)));
                    Users.TryAdd(user.Username, user);
                    count++;
                }
                catch (Exception ex)
                {
                    ScreenIO.Error(
                        Translation["server.failLoad"]
                        .Replace("$PATH", path)
                        .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                    );
                }
            }
            return count;
        }

        public int SaveUsersProfile()
        {
            int count = 0;
            foreach (var user in Users)
            {
                try
                {
                    File.WriteAllText($"userdata/{user.Key}.json", user.Value.ToJsonObject().Serialize("", "  "));
                    count++;
                }
                catch (Exception ex)
                {
                    ScreenIO.Error(
                        Translation["server.failSave"]
                        .Replace("$USER", user.Key)
                        .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                    );
                }
            }
            return count;
        }

        /// <summary>
        /// Check if the username is legal.
        /// </summary>
        public bool IsLegalUsername(string username)
        {
            foreach (char i in username)
                if (i <= 32 || i >= 127 || i == '\\')
                    return false;
            return true;
        }

        /// <summary>
        /// Check if the nickname is legal.
        /// </summary>
        public bool IsLegalNickname(string nickname)
        {
            foreach (char i in nickname)
                if (i <= 32)
                    return false;
            return true;
        }
        
    }
}

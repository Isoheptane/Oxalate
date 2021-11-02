using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Oxalate.Standard;
using JsonSharp;

namespace OxalateTcpInterface
{
    public partial class TcpServer
    {
        bool serverOn;
        TcpListener listener;
        public TcpInterface Plugin { get; }
        public bool IsRunning
        {
            get { return serverOn; }
        }
        public int Port { get; }
        public bool AllowRegister { get; set; }

        Thread listenerThread;

        public TcpServer(TcpInterface plugin, int port)
        {
            serverOn = false;
            Plugin = plugin;
            Port = port;
        }

        public void StartServer()
        {
            listener = new TcpListener(IPAddress.IPv6Any, Port);
            listener.Server.DualMode = true;
            listener.Start();
            ScreenIO.Info(Plugin.Translation["listener.start"].Replace("$PORT", Port.ToString()));

            listenerThread = new Thread(StartListener);
            listenerThread.Start();
            serverOn = true;
        }

        public void StopServer()
        {
            if (listener != null)
            {
                listener.Stop();
            }
            serverOn = false;
        }

        void StartListener()
        {
            while (IsRunning)
            {
                try
                {
                    if (!listener.Pending())
                    {
                        Thread.Sleep(1);
                        continue;
                    }
                    TcpClient client = listener.AcceptTcpClient();
                    Task.Run(() => { ProcessRequest(client); });
                }
                catch (Exception ex)
                {
                    ScreenIO.Error(
                        Plugin.Translation["listener.error"]
                        .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                    );
                }
            }
            listener.Stop();
            ScreenIO.Info(Plugin.Translation["listener.stop"]);
        }

        void ProcessRequest(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            
            Packet request = TcpPacket.ReceivePacket(stream);
            string requestType = request["requestType"];
            switch (requestType)
            {
                case "ping":
                    PingRequest(client);
                    break;
                case "login":
                    LoginRequest(client, request);
                    break;
                case "register":
                    RegisterRequest(client, request);
                    break;
            }
        }

        void PingRequest(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            Packet response = new Packet();
            response["name"] = Plugin.API.ServerName;
            JsonArray description = new JsonArray();
            foreach (string line in Plugin.API.Description)
                description.elements.Add(line);
            response["description"] = description;
            response["maxOnline"]   = Plugin.API.MaxOnlineCount;
            response["language"]    = Plugin.API.ServerLanguage;
            TcpPacket.SendPacket(response, stream);
            stream.Close();
            client.Close();
            client.Dispose();
        }

        void LoginRequest(TcpClient client, Packet request)
        {
            NetworkStream stream = client.GetStream();
            Packet response = new Packet();
            response["accepted"] = false;
            string username = request["username"];
            string password = request["password"];
            bool accepted = false;
            if (!Plugin.API.Users.ContainsKey(username))
            {
                response["message"] = Plugin.Translation["auth.userNotExist"];
            }
            else if (password != Plugin.API.Users[username].Password)
            {
                response["message"] = Plugin.Translation["auth.passwordIncorrect"];
            }
            else if (DateTime.Now < Plugin.API.Users[username].BanTime)
            {
                response["message"] =
                    Plugin.Translation["auth.banned"]
                    .Replace("$TIME", Plugin.API.Users[username].BanTime.ToString("yyyy/MM/dd HH:mm"));
            }
            else if (Plugin.API.OnlineUsers.ContainsKey(username))
            {
                response["message"] = Plugin.Translation["auth.alreadyLogin"];
            }
            else if (Plugin.API.OnlineUsers.Count >= Plugin.API.MaxOnlineCount)
            {
                response["message"] = Plugin.Translation["auth.serverFull"];
            }
            else
            {
                response["accepted"] = true;
                accepted = true;
                response["message"] = Plugin.Translation["auth.welcome"];
            }
            TcpPacket.SendPacket(response, stream);
            if (accepted)
            {
                Plugin.API.ConnectUser(username, new TcpOnlineUser(username, Plugin, client));
                ScreenIO.Info(
                    Plugin.Translation["listener.connected"]
                    .Replace("$USER", username)
                    .Replace("$ADDR", client.Client.RemoteEndPoint.ToString())
                );
            }
            else
            {
                ScreenIO.Info(
                    Plugin.Translation["listener.failConnect"]
                    .Replace("$USER", username)
                    .Replace("$ADDR", client.Client.RemoteEndPoint.ToString())
                    .Replace("$MESSAGE", response["message"])
                );
                stream.Close();
                client.Close();
                client.Dispose();
            }
        }

        void RegisterRequest(TcpClient client, Packet request)
        {
            NetworkStream stream = client.GetStream();
            Packet response = new Packet();
            response["accepted"] = false;
            string username = request["username"];
            string password = request["password"];
            bool accepted = false;
            if (!AllowRegister)
            {
                response["message"] = Plugin.Translation["auth.registerForbidden"];
            }
            else if (Plugin.API.Users.ContainsKey(username))
            {
                response["message"] = Plugin.Translation["auth.userAlreadyExist"];
            }
            else if (!UsernameCheck.IsLegalUsername(username))
            {
                response["message"] = Plugin.Translation["auth.invalidUsername"];
            }
            else
            {
                response["accepted"] = true;
                accepted = true;
                response["message"] = Plugin.Translation["auth.newWelcome"];
            }
            TcpPacket.SendPacket(response, stream);
            if (accepted)
            {
                Plugin.API.RegisterUser(username, password);
                Plugin.API.ConnectUser(username, new TcpOnlineUser(username, Plugin, client));
                ScreenIO.Info(
                    Plugin.Translation["listener.registered"]
                    .Replace("$USER", username)
                    .Replace("$ADDR", client.Client.RemoteEndPoint.ToString())
                );
            }
            else
            {
                ScreenIO.Info(
                    Plugin.Translation["listener.failConnect"]
                    .Replace("$USER", username)
                    .Replace("$ADDR", client.Client.RemoteEndPoint.ToString())
                    .Replace("$MESSAGE", response["message"])
                );
                stream.Close();
                client.Close();
                client.Dispose();
            }
        }
    }
}

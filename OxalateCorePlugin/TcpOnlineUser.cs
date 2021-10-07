using System;
using System.Net.Sockets;
using System.Threading;
using Oxalate.Server;
using Oxalate.Standard;
using JsonSharp;

namespace OxalateCorePlugin
{
    public class TcpOnlineUser : OnlineUser
    {

        TcpClient client;
        NetworkStream stream;

        DateTime expireTime;
        Thread daemonThread;

        public TcpOnlineUser(string username, IPlugin plugin, TcpClient client) : base(username, plugin)
        {
            this.client = client;
            stream = client.GetStream();
            KeepAlive();
            daemonThread = new Thread(StartDaemon);
            daemonThread.Start();
        }

        void KeepAlive()
        {
            expireTime = DateTime.Now.AddMilliseconds(5000);
        }

        public override void Disconnect()
        {
            stream.Close();
            client.Close();
            client.Dispose();
            connected = false;
        }

        public override Packet ReceivePacket()
        {
            return TcpPacket.ReceivePacket(stream);
        }

        public override void SendPacket(Packet packet)
        {
            lock (stream)
            {
                TcpPacket.SendPacket(packet, stream);
            }
        }

        public void StartDaemon()
        {
            while (Connected)
            {
                Thread.Sleep(1);
                if (!Connected)
                {
                    break;
                }
                if (DateTime.Now > expireTime)
                {
                    ScreenIO.Warn(
                        Plugin.Translation["listener.keepalive"]
                        .Replace("$USER", Username)
                    );
                    Plugin.API.DisconnectUser(Username);
                    break;
                }
                if (!stream.DataAvailable)
                {
                    continue;
                }
                try
                {
                    CommandCall call = CommandCall.Parse(ReceivePacket());
                    if (call.CommandName == "disconnect")
                    {
                        ScreenIO.Info(
                            Plugin.Translation["listener.disconnected"]
                            .Replace("$ADDR", client.Client.RemoteEndPoint.ToString())
                            .Replace("$USER", Username)
                        );
                        Plugin.API.DisconnectUser(Username);
                        break;
                    }
                    if (call.CommandName == "keep-alive")
                    {
                        KeepAlive();
                        Packet kaPacket = new Packet();
                        kaPacket["messageType"] = "keep-alive";
                        SendPacket(kaPacket);
                    }
                    else
                    {
                        Plugin.API.ExecuteCommand(this, call);
                    }
                }
                catch (Exception ex)
                {
                    ScreenIO.Error(
                        Plugin.Translation["listener.daemonError"]
                        .Replace("$USER", Username)
                        .Replace("$MESSAGE", ScreenIO.Escape(ex.ToString()))
                    );
                }
            }
        }
    }
}

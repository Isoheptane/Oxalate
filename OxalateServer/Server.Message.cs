using System;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;

namespace Oxalate.Server
{
    public partial class Server
    {
        /// <summary>
        /// Send a packet to the indicated user.
        /// </summary>
        public void SendPacket(Packet packet, OnlineUser user)
        {
            try
            {
                user.SendPacket(packet);
            }
            catch (Exception ex)
            {
                ScreenIO.Error(
                    Translation["server.sendPacketError"]
                    .Replace("$USER", user.Username)
                    .Replace("$MESSAGE", ex.Message)
                );
            }
        }

        /// <summary>
        /// Broadcast a packet to all online user.
        /// </summary>
        public void BroadcastPacket(Packet packet)
        {
            foreach (var user in OnlineUsers)
                SendPacket(packet, user.Value);
        }

        /// <summary>
        /// Send a server message to the user.
        /// </summary>
        public void SendServerMessage(string message, OnlineUser user)
        {
            Packet packet = new Packet();
            packet["messageType"] = "server";
            packet["message"] = message;
            SendPacket(packet, user);
        }

        /// <summary>
        /// Send a server message to the user.
        /// </summary>
        public void BroadcastMessage(string message)
        {
            Packet packet = new Packet();
            packet["messageType"] = "server";
            packet["message"] = message;
            BroadcastPacket(packet);
            ScreenIO.Info(
                Translation["server.broadcastDisplay"]
                .Replace("$MESSAGE", message)
            );
        }
    }
}

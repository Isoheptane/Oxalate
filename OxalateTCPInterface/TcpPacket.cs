using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using JsonSharp;
using Oxalate.Standard;

namespace OxalateTcpInterface
{
    public static class TcpPacket
    {

        /// <summary>
        /// Send a packet to the indicated stream.
        /// </summary>
        public static void SendPacket(Packet packet, NetworkStream stream)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(packet.ToString());
            byte[] endBytes = new byte[bytes.Length + 1];
            Array.Copy(bytes, endBytes, bytes.Length);
            endBytes[endBytes.Length - 1] = 0;
            stream.Write(endBytes);
        }

        /// <summary>
        /// Receive a packet from indicated stream.
        /// </summary>
        /// <param name="timeout">Timeout in milliseconds</param>
        public static Packet ReceivePacket(NetworkStream stream, int timeout = 5000)
        {
            List<byte> bytes = new List<byte>();
            while (stream.DataAvailable)
            {
                byte currentByte = (byte)stream.ReadByte();
                if (currentByte == 0)
                    break;
                bytes.Add(currentByte);
            }
            Packet received = new Packet(JsonObject.Parse(Encoding.UTF8.GetString(bytes.ToArray())));
            bytes = null;
            return received;
        }
    }
}

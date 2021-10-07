using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;
using Oxalate.Standard;

namespace OxalateClientTcp
{
    public partial class ClientTcp
    {
        bool connected = false;
        /// <summary>
        /// Returns the status of connection.
        /// </summary>
        public bool Connected
        {
            get { return connected; }
        }
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Interval of sending keep-alive packet in milliseconds.
        /// </summary>
        public int SendInterval { get; set; }
        /// <summary>
        /// Keep-Alive timeout in milliseconds. 
        /// </summary>
        public int Timeout { get; set; }

        TcpClient client;
        NetworkStream stream;

        public Translation Translation { get; set; }

        /// <summary>
        /// Create a client object from indicated user informations
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Hashed password</param>
        public ClientTcp(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            SendInterval = 2000;
            Timeout = 5000;
        }

        /// <summary>
        /// Create TcpClient object
        /// </summary>
        TcpClient CreateClient()
        {
            TcpClient client = new TcpClient();
            client.Client.DualMode = true;
            return client;
        }

        /// <summary>
        /// Dispose TcpClient object
        /// </summary>
        void DisposeClient()
        {
            if (stream != null)
                stream.Close();
            client.Close();
            client.Dispose();
            stream = null;
            client = null;
        }

        public bool DataAvailable
        {
            get { return stream.DataAvailable; }
        }

        public void Send(Packet packet)
        {
            lock (stream)
            {
                TcpPacket.SendPacket(packet, stream);
            }
        }

        public Packet Receive()
        {
            lock (stream)
            {
                return TcpPacket.ReceivePacket(stream);
            }
        }

        /// <summary>
        /// Connect to the indicated server.
        /// </summary>
        /// <param name="remote">Remote server</param>
        public Packet Connect(IPEndPoint remote)
        {
            if (Connected)
            {
                Send(new CommandCall("disconnect").ToPacket());
                Disconnect();
            }
            client = CreateClient();
            client.Connect(remote);
            stream = client.GetStream();

            Packet request = new Packet();
            request["requestType"] = "login";
            request["username"] = Username;
            request["password"] = Password;
            Send(request);
            Packet response = Receive();
            if(response["accepted"] == true) 
            {
                connected = true;
                StartKeepAlive();
                StartListener();
            }
            else
            {
                DisposeClient();
            }
            return response;
        }

        /// <summary>
        /// Register at the indicated server.
        /// </summary>
        /// <param name="remote">Remote server</param>
        public Packet Register(IPEndPoint remote)
        {
            if (Connected)
            {
                Send(new CommandCall("disconnect").ToPacket());
                Disconnect();
            }
            client = CreateClient();
            client.Connect(remote);
            stream = client.GetStream();

            Packet request = new Packet();
            request["requestType"] = "register";
            request["username"] = Username;
            request["password"] = Password;
            Send(request);
            Packet response = Receive();
            if(response["accepted"] == true) 
            {
                connected = true;
                StartKeepAlive();
                StartListener();
            }
            else
            {
                DisposeClient();
            }
            return response;
        }

        /// <summary>
        /// Ping remote server
        /// </summary>
        /// <param name="remote">Remote server</param>
        public Packet Ping(IPEndPoint remote)
        {
            TcpClient pingClient = CreateClient();
            pingClient.Connect(remote);
            NetworkStream pingStream = pingClient.GetStream();

            Packet request = new Packet();
            request["requestType"] = "ping";

            TcpPacket.SendPacket(request, pingStream);
            Packet result = TcpPacket.ReceivePacket(pingStream);

            pingStream.Close();
            pingClient.Close();
            pingClient.Dispose();
            return result;
        }

        /// <summary>
        /// Disconnect from the server
        /// </summary>
        public void Disconnect()
        {
            DisposeClient();
            connected = false;
        }
    }
}

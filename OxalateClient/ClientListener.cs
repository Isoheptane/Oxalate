using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using Oxalate.Standard;

namespace OxalateClientTcp
{
    public partial class ClientTcp
    {
        Thread listenerThread;

        void StartListener()
        {
            listenerThread = new Thread(ListenMessage);
            listenerThread.Start();
        }

        void ListenMessage()
        {
            while (Connected)
            {
                Thread.Sleep(1);
                if (DateTime.Now > expireTime)
                {
                    Disconnect();
                    RaiseErrorOccuredEvent("Disconnected because the server does not respond.");
                    break;
                }
                try
                {
                    if (!Connected)
                    {
                        break;
                    }
                    if (!DataAvailable)
                    {
                        continue;
                    }
                    Packet received = Receive();
                    if (received["messageType"] == "keep-alive")
                    {
                        KeepAlive();
                    }
                    else 
                    {
                        RaiseReceivedMessageEvent(received);
                    }
                }
                catch (Exception ex)
                {
                    RaiseExceptionOccuredEvent(ex);
                }
            }
        }
    }
}

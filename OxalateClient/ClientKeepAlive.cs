using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Oxalate.Standard;

namespace OxalateClientTcp {
    public partial class ClientTcp
    {
        Thread keepAliveSenderThread;
        DateTime expireTime;
        
        void KeepAlive()
        {
            expireTime = DateTime.Now.AddMilliseconds(Timeout);
        }
        void StartKeepAlive()
        {
            KeepAlive();
            keepAliveSenderThread = new Thread(() =>
            {
                expireTime = DateTime.Now.AddMilliseconds(Timeout);
                while (Connected)
                {
                    try
                    {
                        Thread.Sleep(SendInterval);
                        if (!Connected)
                        {
                            break;
                        }
                        Send(new CommandCall("keep-alive").ToPacket());
                    }
                    catch (Exception ex)
                    {
                        RaiseExceptionOccuredEvent(ex);
                    }
                }
            });
            keepAliveSenderThread.Start();
        }
    }
}

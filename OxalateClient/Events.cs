using System;
using System.Threading;
using Oxalate.Standard;

namespace OxalateClientTcp
{
    public class ReceiveMessageEventArgs : EventArgs
    {
        public Packet ReceivedPacket { get; set; }
        public ReceiveMessageEventArgs(Packet packet) 
        {
            ReceivedPacket = packet;
        }
    }

    public class ExceptionOccurEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
        public ExceptionOccurEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }

    public class ErrorOccurEventArgs : EventArgs
    {
        public string Message { get; set;}
        public ErrorOccurEventArgs(string message)
        {
            Message = message;
        }
    }

    public partial class ClientTcp
    {
        public event EventHandler<ReceiveMessageEventArgs> ReceivedMessage;
        public event EventHandler<ExceptionOccurEventArgs> ExceptionOccured;
        public event EventHandler<ErrorOccurEventArgs> ErrorOccured;

        void RaiseReceivedMessageEvent(Packet packet)
        {
            ReceivedMessage(this, new ReceiveMessageEventArgs(packet));
        }

        void RaiseExceptionOccuredEvent(Exception exception)
        {
            ExceptionOccured(this, new ExceptionOccurEventArgs(exception));
        }

        void RaiseErrorOccuredEvent(string message)
        {
            ErrorOccured(this, new ErrorOccurEventArgs(message));
        }
    }
}
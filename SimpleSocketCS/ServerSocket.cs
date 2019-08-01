using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Threading;

namespace CM.SimpleSocketCS
{
    public class ServerSocket : BaseSocket
    {
        public Dictionary<string, Socket> m_clientSockets = new Dictionary<string, Socket>();
        private int n = 10;

        public ServerSocket(string host, int port) : base(host, port, true)
        {
            Thread th = new Thread(new ThreadStart(Processing));
            th.IsBackground = true;
            th.Start();
        }

        protected override void Processing()
        {
            byte[] bytes = new byte[1024 * 1024];
            while (n>0)
            {
                n--;

                try
                {
                    // Create Socket for new Connection.
                    Socket clientSocket = this.m_socket.Accept();

                    this.m_clientSockets.Add(clientSocket.RemoteEndPoint.ToString(), clientSocket);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void Send(byte[] bytes, int index)
        {
            this.m_clientSockets.Values.ToList()[index].Send(bytes);
        }

        public void Broadcast(byte[] bytes)
        {
            foreach (Socket socket in this.m_clientSockets.Values)
            {
                if(socket != null && socket.Connected)
                {
                    socket.Send(bytes);
                }
            }
        }

        public void Broadcast(object obj)
        {
            if(obj == null || !(obj is ISerializable))
            {
                throw (new ArgumentException());
            }

            Broadcast(Utils.ObjectToBytes(obj));
        }
    }
}

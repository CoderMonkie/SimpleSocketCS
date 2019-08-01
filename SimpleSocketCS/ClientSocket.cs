using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace CM.SimpleSocketCS
{
    public class ClientSocket : BaseSocket
    {
        public ClientSocket(string host, int port):base(host, port)
        {
            Thread th = new Thread(new ThreadStart(Processing));
            th.Start();
        }

        protected override void Processing()
        {
            byte[] bytes = new byte[1024 * 1024];

            while (true)
            {
                if (!this.m_socket.Connected)
                {
                    return;
                }

                try
                {
                    // Wait to Receive data from Server.
                    int bytesRec = this.m_socket.Receive(bytes);

                    if (bytesRec == 0)
                    {
                        Console.WriteLine("Client[" + this.m_socket.RemoteEndPoint.ToString() + "] is disconnected...");
                        break;
                    }

                    RaiseReceiveData(bytes.Take(bytesRec).ToArray());
                }
                catch (SocketException ex)
                {
                    throw ex;
                }
            }
        }

        public void Send(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }

            Console.WriteLine("From Cient To Server...");

            this.m_socket.Send(Encoding.UTF8.GetBytes(msg));
        }

        public void Send(object obj)
        {
            if (obj == null || !(obj is ISerializable))
            {
                throw (new ArgumentException());
            }

            Console.WriteLine("From Cient To Server...");

            this.m_socket.Send(Utils.ObjectToBytes(obj));
        }
    
        public void Close()
        {
            this.m_socket.Shutdown(SocketShutdown.Both);
            this.m_socket.Close(500);
        }
    }
}

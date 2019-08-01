using System;
using System.Net;
using System.Net.Sockets;

namespace CM.SimpleSocketCS
{
    public class BaseSocket
    {
        public Socket m_socket = null;

        private bool m_bIsServer = false;

        public delegate void ReceiveHandler(byte[] bytes);
        public event ReceiveHandler onReceiveData;

        public BaseSocket(string host, int port, bool isServer = false)
        {
            try
            {

                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);

                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_bIsServer = isServer;

                if (isServer)
                {
                    m_socket.Bind(ipe);

                    m_socket.Listen(0);

                    Console.WriteLine(string.Format("Server is Listenning on [{0}:{1}]", host, port));
                }
                else
                {
                    m_socket.Connect(ipe);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void RaiseReceiveData(byte[] bytes)
        {
            if(onReceiveData != null)
            {
                onReceiveData.Invoke(bytes);
            }
        }

        protected virtual void Processing()
        {
            throw new NotImplementedException();
        }
    }
}

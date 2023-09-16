using System.Net.Sockets;
using System.Net;

namespace Diplom.Client.Utilities
{
    public class ServerSettings
    {
        private readonly string _ip;
        private readonly int _port;
        private readonly IPEndPoint _tcpEndPoint;
        private readonly Socket _tcpSocket;

        public string IP
        {
            get { return _ip; }
        }

        public int Port
        {
            get { return _port; } 
        }

        public IPEndPoint EndPoint
        { get 
            { return _tcpEndPoint; } 
        }

        public Socket Socket
        { 
            get { return _tcpSocket; } 
        }

        public ServerSettings()
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            _ip = ip;
            _port = port;

            _tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            _tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //_tcpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, false);
            _tcpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //_tcpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);
        }
    }
}

using System.Net;
using System.Net.Sockets;

namespace CheckersClient.Main
{
    public class ConnectionEstablisher
    {
        public static int FindFreePort()
        {
            var port = 0;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                var localEP = new IPEndPoint(IPAddress.Any, 0);
                socket.Bind(localEP);
                localEP = (IPEndPoint)socket.LocalEndPoint;
                port = localEP.Port;
            }
            finally
            {
                socket.Close();
            }

            return port;
        }
    }
}
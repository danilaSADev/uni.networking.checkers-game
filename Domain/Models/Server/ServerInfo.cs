using System.Net.Sockets;

namespace Domain.Models.Server
{
    public static class ServerInfo
    {
        public static readonly string IpAddress = "127.0.0.1";
        public static readonly int Port = 8080;

        public static Socket SharedSocket =>
            new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
}
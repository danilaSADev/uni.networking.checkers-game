using System.Net.Sockets;

namespace Domain.Models.Server
{
    public static class ServerInfo
    {
        public static readonly string IpAddress = "0.0.0.0";
        public static readonly int Port = 8080;

        public static readonly int MaxClientResponseTime = 10000; // 10 seconds
        public static Socket SharedSocket =>
            new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
}
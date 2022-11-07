using System;
using System.Net;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Payloads.Client;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Main
{
    public class ConnectionEstablisher
    {
        public ConnectionEstablisher()
        {
        }

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
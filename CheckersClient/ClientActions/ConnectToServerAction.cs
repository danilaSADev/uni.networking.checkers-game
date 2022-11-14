using System;
using System.Net;
using System.Net.Sockets;
using CheckersClient.Main;
using Domain.Converters;
using Domain.Models;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersClient.ClientActions
{
    public class ConnectToServerAction : AbstractAction
    {
        private readonly string _password;
        private readonly string _username;

        public ConnectToServerAction(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public override ServerResponse Request()
        {
            var payload = new EstablishConnectionPayload
            {
                Username = _username,
                Password = _password,
                IpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                Port = ConnectionEstablisher.FindFreePort()
            };
            return ExecuteAction(ClientCommands.ConnectToServer, payload);
        }
    }
}
using System.Net;
using CheckersClient.Main;
using Domain.Models;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;

namespace CheckersClient.Actions
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

        public override Response Request()
        {
            var payload = new EstablishConnectionPayload
            {
                Username = _username,
                Password = _password,
                IpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                Port = ConnectionEstablisher.Port
            };
            return ExecuteAction(ClientCommands.ConnectToServer, payload);
        }
    }
}
using System;
using System.Net.Sockets;
using CheckersClient.Actions;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersClient.ClientActions
{
    public class GetLobbiesAction : AbstractAction
    {
        private readonly string _identifier;

        public GetLobbiesAction(string identifier)
        {
            _identifier = identifier;
        }
        
        public override Response Request()
        {
            var payload = new RequestLobbiesPayload()
            {
                UserIdentifier = _identifier
            };
            return ExecuteAction(ClientCommands.RetrieveLobbies, payload);
        }
    }
}
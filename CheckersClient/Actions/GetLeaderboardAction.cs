using System;
using System.Net.Sockets;
using CheckersClient.Actions;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersClient.ClientActions
{
    public class GetLeaderboardAction : AbstractAction
    {
        private readonly string _identifier;

        public GetLeaderboardAction(string identifier)
        {
            _identifier = identifier;
        }
        
        public override Response Request()
        {
            var payload = new RequestLeaderboardPayload
            {
                UserIdentifier = _identifier
            };
            return ExecuteAction(ClientCommands.RetrieveLeaderboard, payload);
        }
    }
}
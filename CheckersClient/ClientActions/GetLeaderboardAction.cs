using System;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
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
        
        public override ServerResponse Request()
        {
            var payload = new RequestLeaderboardPayload
            {
                UserIdentifier = _identifier
            };
            return ExecuteAction(ClientCommands.RetrieveLeaderboard, payload);
        }
    }
}
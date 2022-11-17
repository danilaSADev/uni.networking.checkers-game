using System;
using System.Net.Sockets;
using CheckersClient.Actions;
using CheckersClient.Main;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
using Domain.Models.Shared;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersClient.ClientActions
{
    public class CreateLobbyAction : AbstractAction
    {
        private readonly string _userIdentifier;
        private readonly GameSettings _settings;

        public CreateLobbyAction(string userIdentifier, GameSettings settings)
        {
            _userIdentifier = userIdentifier;
            _settings = settings;
        }
        
        public override Response Request()
        {
            var payload = new CreateLobbyPayload
            {
                HostIdentifier = _userIdentifier,
                Settings = _settings
            };
            return ExecuteAction(ClientCommands.CreateLobby, payload);
        }
    }
}
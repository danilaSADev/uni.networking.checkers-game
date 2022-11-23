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
    public class ConnectToLobbyAction : AbstractAction
    {
        private readonly string _userId;
        private readonly string _lobbyId;

        public ConnectToLobbyAction(string userId, string lobbyId)
        {
            _userId = userId;
            _lobbyId = lobbyId;
        }

        public override Response Request()
        {
            var payload = new ConnectToLobbyPayload
            {
                UserIdentifier = _userId,
                LobbyIdentifier = _lobbyId
            };
            return ExecuteAction(ClientCommands.ConnectToLobby, payload);
        }
    }
}
using System;
using System.Net.Sockets;
using Domain.Converters;
using Domain.Models;
using Domain.Models.Server;
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

        public override ServerResponse Request()
        {
            var payload = new ConnectToLobbyPayload
            {
                UserIdentifier = _userId,
                LobbyIdentifier = _lobbyId
            };
            return ExecuteAction(ClientCommands.ConnectToLobby, payload);;
        }
    }
}
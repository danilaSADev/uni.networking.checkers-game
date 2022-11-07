﻿using Domain.Models;
using Domain.Payloads.Client;

namespace CheckersClient.ClientActions
{
    public class DisconnectFromLobbyAction : AbstractAction
    {
        private readonly string _userId;
        private readonly string _lobbyId;

        public DisconnectFromLobbyAction(string userId, string lobbyId)
        {
            _userId = userId;
            _lobbyId = lobbyId;
        }
        
        public override ServerResponse Request()
        {
            var payload = new DisconnectFromLobbyPayload()
            {
                UserIdentifier = _userId,
                LobbyIdentifier = _lobbyId
            };
            return ExecuteAction(ClientCommands.DisconnectFromLobby, payload);
        }
    }
}
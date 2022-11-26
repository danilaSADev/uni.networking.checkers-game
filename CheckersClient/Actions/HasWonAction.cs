using System;
using Domain.Models;
using Domain.Models.Shared;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;

namespace CheckersClient.Actions
{
    public class HasWonAction : AbstractAction
    {
        private readonly string _userId;
        private readonly string _lobbyId;

        public HasWonAction(string userId, string lobbyId)
        {
            _userId = userId;
            _lobbyId = lobbyId;
        }
        
        public override Response Request()
        {
            var payload = new GameStatePayload()
            {
                LobbyId = _lobbyId,
                UserId = _userId,
                State = GameState.Winner
            };
            return ExecuteAction(ClientCommands.ChangeGameStats, payload);
        }
    }
}
using CheckersClient.Actions;
using Domain.Models;
using Domain.Models.Server;
using Domain.Networking.Handlers.Models;
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
        
        public override Response Request()
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
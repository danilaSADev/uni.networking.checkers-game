using CheckersClient.Forms;
using Domain.Models;
using Domain.Models.Shared;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;

namespace CheckersClient.Actions
{
    public class HasNoTurnsAction : AbstractAction
    {
        private readonly string _lobbyId;
        private readonly string _userId;

        public HasNoTurnsAction(string userId, string lobbyId)
        {
            _lobbyId = lobbyId;
            _userId = userId;
        }

        public override Response Request()
        {
            var payload = new GameStatePayload()
            {
                UserId= _userId,
                LobbyId = _lobbyId,
                State = GameState.NoTurns,
            };
            return ExecuteAction(ClientCommands.ChangeGameStats, payload);
        }
    }
}
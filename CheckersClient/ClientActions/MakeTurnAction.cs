using Domain.Models;
using Domain.Models.Server;
using Domain.Models.Shared;
using Domain.Payloads.Client;

namespace CheckersClient.ClientActions
{
    public class MakeTurnAction : AbstractAction
    {
        private readonly string _userId;
        private readonly string _lobbyId;
        private readonly Vector _fromPosition;
        private readonly Vector _toPosition;
        private readonly Side _side;

        public MakeTurnAction(string userId, string lobbyId, Vector fromPosition, Vector toPosition, Side side)
        {
            _userId = userId;
            _lobbyId = lobbyId;
            _fromPosition = fromPosition;
            _toPosition = toPosition;
            _side = side;
        }
        
        public override ServerResponse Request()
        {
            var payload = new MakeTurnPayload
            {
                FromPosition = _fromPosition,
                ToPosition = _toPosition,
                TurnSide = _side
            };
            return ExecuteAction(ClientCommands.MakeTurn, payload);
        }
    }
}
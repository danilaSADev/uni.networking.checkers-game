using CheckersClient.Actions;
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
        private bool _finishedTurn;
        private readonly TurnType _type;

        public MakeTurnAction(TurnInformation info)
        {
            _userId = info.UserId;
            _lobbyId = info.LobbyId;
            _fromPosition = info.FromPosition;
            _toPosition = info.ToPosition;
            _side = info.TurnSide;
            _finishedTurn = info.FinishedTurn;
            _type = info.Type;
        }
        
        public override Response Request()
        {
            var payload = new MakeTurnPayload
            {
                LobbyId = _lobbyId,
                UserId = _userId,
                FromPosition = _fromPosition,
                ToPosition = _toPosition,
                TurnSide = _side,
                FinishedTurn = _finishedTurn
            };
            return ExecuteAction(ClientCommands.MakeTurn, payload);
        }
    }
}
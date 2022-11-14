using Domain.Models;
using Domain.Models.Shared;
using Domain.Payloads.Client;

namespace CheckersClient.ClientActions
{
    public class MakeTurnAction : AbstractAction
    {
        private readonly Vector _fromPosition;
        private readonly Vector _toPosition;
        private readonly Side _side;

        public MakeTurnAction(Vector fromPosition, Vector toPosition, Side side)
        {
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
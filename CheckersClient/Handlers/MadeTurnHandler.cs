using CheckersClient.Services;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersClient.Handlers
{
    public class MadeTurnHandler : ICommandHandler
    {
        private Board _board;

        public MadeTurnHandler(Board board)
        {
            _board = board;
        }
        
        public Response Handle(string payload)
        {
            var unpackedPayload = JsonConvert.DeserializeObject<MakeTurnPayload>(payload);
            _board.MakeOpponentTurn(unpackedPayload);

            return Response.Ok;
        }
    }
}
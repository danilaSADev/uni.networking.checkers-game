using CheckersServer.Interfaces;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Client;
using Newtonsoft.Json;

namespace CheckersServer.Handlers
{
    public class GameStateHandler : ICommandHandler
    {
        private readonly IMultiplayerService _service;

        public GameStateHandler(IMultiplayerService service)
        {
            _service = service;
        }
        
        public Response Handle(string payload)
        {
            var unpackedPayload = JsonConvert.DeserializeObject<GameStatePayload>(payload);
            
            if (unpackedPayload == null)
                return Response.Failed;
            
            _service.ChangeLobbyState(unpackedPayload.UserId, unpackedPayload.LobbyId, unpackedPayload.State);
            return Response.Ok;
        }
    }
}


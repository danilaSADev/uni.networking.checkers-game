using Domain.Models.Server;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Handlers
{
    public class ServerMessageHandler : ICommandHandler
    {
        private readonly IGameForm _gameForm;

        public ServerMessageHandler(IGameForm gameForm)
        {
            _gameForm = gameForm;
        }
        
        public Response Handle(string payload)
        {
            var unpacked = JsonConvert.DeserializeObject<ServerMessagePayload>(payload);
            
            if (unpacked == null)
                return Response.Unknown;
            
            if (unpacked.Type == ServerMessageType.Notification)
                _gameForm.ShowMessage(unpacked.Message);
            else if (unpacked.Type == ServerMessageType.GameTermination)
                _gameForm.ShowMessageAndDisconnect(unpacked.Message);
            
            return Response.Ok;
        }
    }

    public interface IGameForm
    {
        void ShowMessage(string message);
        void ShowMessageAndDisconnect(string message);
    }
}
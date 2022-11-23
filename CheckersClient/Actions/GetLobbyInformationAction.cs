using System;
using Domain.Models.Server;
using Domain.Networking.Handlers.Models;

namespace CheckersClient.Actions
{
    public class GetLobbyInformationAction : AbstractAction
    {
        public GetLobbyInformationAction(string lobbyId, string userId)
        {
            
        }
        
        public override Response Request()
        {
            throw new NotImplementedException();
        }
    }
}
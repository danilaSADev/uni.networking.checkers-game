using System;
using Domain.Models.Shared;

namespace Domain.Payloads.Client
{
    [Serializable]
    public class GameStatePayload
    {
        public string UserId { get; set; }
        public string LobbyId { get; set; }
        public GameState State { get; set; }
    }   
}
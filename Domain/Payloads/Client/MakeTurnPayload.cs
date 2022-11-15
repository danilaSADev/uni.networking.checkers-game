using System.Dynamic;
using Domain.Models;
using Domain.Models.Shared;

namespace Domain.Payloads.Client
{
    public class MakeTurnPayload
    {
        public string UserId { get; set; }
        public string LobbyId { get; set; }
        public Vector FromPosition { get; set; }
        public Vector ToPosition { get; set; }
        public Side TurnSide { get; set; }
        public TurnType Type { get; set; }
    }

    
}
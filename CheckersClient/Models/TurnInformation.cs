using Domain.Models.Shared;

namespace CheckersClient
{
    public class TurnInformation
    {
        public string UserId { get; set; }
        public Vector FromPosition { get; set; }
        public string LobbyId { get; set; }
        public Vector ToPosition { get; set; }
        public Side TurnSide { get; set; }
        public TurnType Type { get; set; }
        public bool FinishedTurn { get; set; }
    }
}
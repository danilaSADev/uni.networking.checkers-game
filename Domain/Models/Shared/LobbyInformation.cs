namespace Domain.Models.Shared
{
    public class LobbyInformation
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public bool IsTournament { get; set; }
        public int TimeToMakeTurn { get; set; }
    }
}
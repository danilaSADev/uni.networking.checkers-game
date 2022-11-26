namespace Domain.Models.Shared
{
    // TODO : compare with requirements 
    public class GameSettings
    {
        public string RoomName { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public string LobbyId { get; set; }
        public bool IsTournament { get; set; }
        public int TimeOut { get; set; } = 30000;
    }

    public enum GameState
    {
        Winner, 
        NoTurns
    }
    
    public enum GameDifficulty
    {
        Normal, 
        Hard
    }
    
    public enum Side
    {
        White,
        Black
    }
    public enum TurnType
    {
        Movement,
        Beating
    }
}
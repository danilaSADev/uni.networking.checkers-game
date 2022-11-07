﻿namespace Domain.Models
{
    // TODO : compare with requirements 
    public class GameSettings
    {
        public string RoomName { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public bool IsTournament { get; set; }
    }

    public enum GameDifficulty
    {
        Normal, 
        Hard
    }
}
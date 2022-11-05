namespace Domain.Models
{
    public static class ClientCommands
    {
        public static readonly string ConnectToServer = "CONNECT_TO_SERVER";
        public static readonly string DisconnectFromServer = "DISCONNECT_FROM_SERVER";
        
        public static readonly string CreateLobby = "CREATE_LOBBY";
        public static readonly string StartGame = "START_GAME";
        public static readonly string ConnectToLobby = "CONNECT_TO_LOBBY";
        public static readonly string CheckReadyInLobby = "CHECK_READY";
        public static readonly string MakeTurn = "MAKE_TURN";
        public static readonly string ChangeLobbySettings = "CHANGE_DIFFICULTY";

        public static readonly string RetrieveLeaderboard = "RETRIEVE_LEADERBOARD";
    }
}
using System;

namespace Domain.Payloads.Client
{
    [Serializable]
    public class ConnectToLobbyPayload
    {
        public string UserIdentifier { get; set; } 
        public string LobbyIdentifier { get; set; }
    }
}
using System;

namespace Domain.Payloads.Client
{
    [Serializable]
    public class DisconnectFromLobbyPayload
    {
        public string UserIdentifier { get; set; }
        public string LobbyIdentifier { get; set; }
    }
}
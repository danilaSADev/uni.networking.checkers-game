using System;
using Domain.Models;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class ConnectedToLobbyPayload
    {
        public LobbyInformation Information { get; set; }
    }
}
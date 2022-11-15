using System;
using Domain.Models;
using Domain.Models.Shared;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class ConnectedToLobbyPayload
    {
        public LobbyInformation Information { get; set; }
    }
}
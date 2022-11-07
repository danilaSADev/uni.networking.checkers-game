using System;
using Domain.Models;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class CreatedLobbyPayload
    {
        public LobbyInformation Information { get; set; }
    }
}
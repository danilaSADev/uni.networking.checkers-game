using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.Models.Shared;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class FetchedLobbiesPayload
    {
        public List<LobbyInformation> Lobbies { get; set; }
    }
}
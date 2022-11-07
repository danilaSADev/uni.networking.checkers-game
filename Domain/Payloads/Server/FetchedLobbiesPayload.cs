using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class FetchedLobbiesPayload
    {
        public List<LobbyInformation> Lobbies { get; set; }
    }
}
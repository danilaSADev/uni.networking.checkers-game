using System;
using Domain.Models;
using Domain.Models.Shared;

namespace Domain.Payloads.Client
{
    [Serializable]
    public class CreateLobbyPayload
    {
        public GameSettings Settings { get; set; }
        public string HostIdentifier { get; set; }
    }
}
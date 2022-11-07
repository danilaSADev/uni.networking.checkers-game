using System;
using Domain.Models;

namespace Domain.Payloads.Client
{
    [Serializable]
    public class CreateLobbyPayload
    {
        public GameSettings Settings { get; set; }
        public string HostIdentifier { get; set; }
    }
}
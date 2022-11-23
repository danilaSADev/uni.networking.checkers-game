using System;
using Domain.Models.Shared;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class GameStartedPayload
    {
        public Side PlayerSide { get; set; }
    }
}
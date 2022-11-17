using System.Collections.Generic;
using Domain.Models.Shared;

namespace Domain.Payloads.Server
{
    public class GameStartedPayload
    {
        public Side PlayerSide { get; set; }
    }
}
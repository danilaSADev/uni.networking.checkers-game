
using System;
using Domain.Models.Server;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class ServerMessagePayload
    {
        public ServerMessageType Type { get; set; }
        public string Message { get; set; }
    }
}

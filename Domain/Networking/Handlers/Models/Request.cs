using System;

namespace Domain.Networking.Handlers.Models
{
    [Serializable]
    public class Request
    {
        public string Command { get; set; }
        public string Payload { get; set; }
    }
}
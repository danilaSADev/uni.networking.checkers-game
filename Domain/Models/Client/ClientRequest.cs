using System;

namespace Domain.Models
{
    [Serializable]
    public class ClientRequest
    {
        public string Command { get; set; }
        public string Payload { get; set; }
    }
}
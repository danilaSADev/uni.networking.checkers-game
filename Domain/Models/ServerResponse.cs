using System;

namespace Domain.Models
{
    [Serializable]
    public class ServerResponse
    {
        public string Status { get; set; }
        public string Payload { get; set; }
    }
}
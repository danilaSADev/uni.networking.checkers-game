using System;

namespace Domain.Models
{
    [Serializable]
    public class Request
    {
        public string Command { get; set; }
        public string Payload { get; set; }
    }
}
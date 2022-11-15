using System;

namespace Domain.Models.Server
{
    [Serializable]
    public class ServerResponse
    {
        public static ServerResponse FailedResponse => new ServerResponse() { Status = "FAILED", Payload = "" };
        public static ServerResponse Unknown => new ServerResponse() { Status = "UNKNOWN", Payload = "" };
        
        public string Status { get; set; }
        public string Payload { get; set; }
    }
}
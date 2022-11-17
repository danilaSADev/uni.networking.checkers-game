using System;

namespace Domain.Models.Server
{
    [Serializable]
    public class Response
    {
        public static Response FailedResponse => new Response() { Status = "FAILED", Payload = "" };
        public static Response Unknown => new Response() { Status = "UNKNOWN", Payload = "" };
        
        public string Status { get; set; }
        public string Payload { get; set; }
    }
}
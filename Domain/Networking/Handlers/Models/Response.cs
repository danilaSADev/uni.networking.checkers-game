using System;

namespace Domain.Networking.Handlers.Models
{
    [Serializable]
    public class Response
    {
        public static Response Ok => new() { Status = "OK", Payload = string.Empty };
        public static Response Empty => new() { Status = "EMPTY", Payload = string.Empty };
        public static Response Failed => new()  { Status = "FAILED", Payload = string.Empty };
        public static Response Unknown => new() { Status = "UNKNOWN", Payload = string.Empty };
        
        public string Status { get; set; }
        public string Payload { get; set; }
    }
}
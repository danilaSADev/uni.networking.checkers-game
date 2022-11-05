using System;

namespace Domain.Payloads
{
    [Serializable]
    public class EstablishConnectionPayload
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string IpAddress { get; set; }
        
        public int Port { get; set; }
    }
}
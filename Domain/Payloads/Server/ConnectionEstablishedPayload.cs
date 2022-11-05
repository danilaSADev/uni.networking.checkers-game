using System;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class ConnectionEstablishedPayload
    {
        public string UserIdentifier { get; set; }
    }
}
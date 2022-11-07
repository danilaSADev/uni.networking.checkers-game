using System;

namespace Domain.Payloads.Client
{
    [Serializable]
    public class RequestLobbiesPayload
    {
        public string UserIdentifier { get; set; }
    }
}
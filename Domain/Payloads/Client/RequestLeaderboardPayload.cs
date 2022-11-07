using System;

namespace Domain.Payloads.Client
{
    [Serializable]
    public class RequestLeaderboardPayload
    {
        public string UserIdentifier { get; set; }
    }
}
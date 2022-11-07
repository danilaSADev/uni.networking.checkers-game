using System;
using System.Collections.Generic;

namespace Domain.Payloads.Server
{
    [Serializable]
    public class FetchedLeaderboardPayload
    {
        public Dictionary<string, int> Leaderboard { get; set; }
    }
}
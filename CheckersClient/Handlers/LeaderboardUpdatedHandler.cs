using System.Linq;
using System.Windows.Forms;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Handlers
{
    public class LeaderboardUpdatedHandler : ICommandHandler
    {
        private readonly BindingSource _leaderboardSource;

        public BindingSource LeaderboardSource => _leaderboardSource;

        public LeaderboardUpdatedHandler(BindingSource leaderboardSource)
        {
            _leaderboardSource = leaderboardSource;
        }
        
        public Response Handle(string payload)
        {
            var dictionary = JsonConvert.DeserializeObject<FetchedLeaderboardPayload>(payload).Leaderboard;
            _leaderboardSource.DataSource = dictionary.ToList().Select(s =>
                new
                {
                    Nickname = s.Key,
                    Score = s.Value
                });
            return Response.Ok;
        }
    }
}
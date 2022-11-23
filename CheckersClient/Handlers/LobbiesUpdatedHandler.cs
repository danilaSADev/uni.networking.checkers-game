using System.Linq;
using System.Windows.Forms;
using Domain.Networking.Handlers.Interfaces;
using Domain.Networking.Handlers.Models;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Handlers
{
    public class LobbiesUpdatedHandler : ICommandHandler
    {
        private readonly BindingSource _lobbiesSource;

        public LobbiesUpdatedHandler(BindingSource lobbiesSource)
        {
            _lobbiesSource = lobbiesSource;
        }
        public Response Handle(string payload)
        {
            var dictionary = JsonConvert.DeserializeObject<FetchedLobbiesPayload>(payload).Lobbies;
            _lobbiesSource.DataSource = dictionary.ToList();
            return Response.Ok;
        }
    }
}
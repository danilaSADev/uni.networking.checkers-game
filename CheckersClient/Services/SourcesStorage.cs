using System.Windows.Forms;

namespace CheckersClient.Services
{
    public class SourcesStorage
    {
        public BindingSource LeaderboardSource { get; } = new BindingSource();
        public BindingSource LobbiesSource { get; } = new BindingSource();
    }
}
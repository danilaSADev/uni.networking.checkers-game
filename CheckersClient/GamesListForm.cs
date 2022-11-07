using System;
using System.Linq;
using System.Windows.Forms;
using CheckersClient.ClientActions;
using CheckersClient.GameGraphics;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient
{
    public partial class GamesListForm : Form
    {
        private readonly string _userIdentifier;

        private readonly BindingSource _leaderboardDataSource;
        private readonly BindingSource _lobbiesDataSource;

        public GamesListForm(string userIdentifier)
        {
            _leaderboardDataSource = new BindingSource();
            _lobbiesDataSource = new BindingSource();
            _userIdentifier = userIdentifier;
            InitializeComponent();
        }

        private void OnCreateLobby(object sender, EventArgs e)
        {
            var form = new GameForm();

            form.Show();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = this.Location;
            this.Hide();

            form.FormClosed += (o, args) =>
            {
                this.DesktopLocation = form.Location;
                this.Show();
            };
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            var action = new DisconnectFromServerAction(_userIdentifier);
            action.Request();
        }

        private void OnRefreshLeaderboard(object sender, EventArgs e)
        {
            var action = new GetLeaderboardAction(_userIdentifier);

            var response = action.Request();

            if (response.Payload.Equals(string.Empty))
            {
                // TODO : payload is empty 
                return;
            }

            var dictionary = JsonConvert.DeserializeObject<FetchedLeaderboardPayload>(response.Payload).Leaderboard;

            _leaderboardDataSource.DataSource = dictionary.ToList().Select(s =>
                new
                {
                    Nickname = s.Key,
                    Score = s.Value
                });

        }

        private void OnFormLoaded(object sender, EventArgs e)
        {
            OnRefreshLeaderboard(sender, e);
            leaderboard.DataSource = _leaderboardDataSource;
            roomsList.DataSource = _lobbiesDataSource;
        }

        private void OnRefreshLobbies(object sender, EventArgs e)
        {
            var action = new GetLobbiesAction(_userIdentifier);

            var response = action.Request();

            if (response.Payload.Equals(string.Empty))
            {
                // TODO : payload is empty 
                return;
            }

            var list = JsonConvert.DeserializeObject<FetchedLobbiesPayload>(response.Payload).Lobbies;

            _lobbiesDataSource.DataSource = list;
        }
    }
}
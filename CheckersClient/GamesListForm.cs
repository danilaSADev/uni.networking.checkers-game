using System;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;
using CheckersClient.ClientActions;
using CheckersClient.GameGraphics;
using Domain.Models;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient
{
    public partial class GamesListForm : Form
    {
        private readonly string _userIdentifier;
        private string _selectedLobbyIdentifier;

        private readonly BindingSource _leaderboardDataSource;
        private readonly BindingSource _lobbiesDataSource;

        public GamesListForm(string userIdentifier)
        {
            _leaderboardDataSource = new BindingSource();
            _lobbiesDataSource = new BindingSource();
            _userIdentifier = userIdentifier;
            InitializeComponent();
        }

        private GameSettings GenerateGameSettings()
        {
            var gameSettings = new GameSettings();
            gameSettings.RoomName = roomNameBox.Text;
            gameSettings.Difficulty = (GameDifficulty) difficultyComboBox.SelectedItem;
            gameSettings.IsTournament = tournamentBox.Checked;
            return gameSettings;
        }

        private void GoToLobby(LobbyInformation information)
        {
            var form = new GameForm( _userIdentifier, information);
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
        
        private void OnCreateLobby(object sender, EventArgs e)
        {
            var action = new CreateLobbyAction(_userIdentifier, GenerateGameSettings());
            
            var response = action.Request();

            if (response.Status.Equals("FAILED") || response.Payload.Equals(string.Empty))
            {
                // TODO : dialog on payload is empty 
                return;
            }

            var unpackedPayload = JsonConvert.DeserializeObject<CreatedLobbyPayload>(response.Payload);
            
            GoToLobby(unpackedPayload.Information);
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
                // TODO : dialog on payload is empty 
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

        private void OnRefreshLobbies(object sender, EventArgs e)
        {
            var action = new GetLobbiesAction(_userIdentifier);

            var response = action.Request();

            if (response.Payload.Equals(string.Empty))
            {
                // TODO : dialog on payload is empty 
                return;
            }

            var list = JsonConvert.DeserializeObject<FetchedLobbiesPayload>(response.Payload).Lobbies;

            _lobbiesDataSource.DataSource = list.Select(s =>
                new {
                    ID = s.Identifier,
                    Name = s.Name,
                    IsTournament = s.IsTournament
                });
        }
        
        private void OnFormLoaded(object sender, EventArgs e)
        {
            OnRefreshLeaderboard(sender, e);
            leaderboard.DataSource = _leaderboardDataSource;
            roomsList.DataSource = _lobbiesDataSource;
            difficultyComboBox.DataSource = Enum.GetValues(typeof(GameDifficulty));
        }

        private void OnRoomSelection(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var lobbyIdentifier = (string) roomsList.Rows[rowIndex].Cells[0].Value;
            _selectedLobbyIdentifier = lobbyIdentifier;
        }

        private void OnConnectToGame(object sender, EventArgs e)
        {
            var action = new ConnectToLobbyAction(_userIdentifier, _selectedLobbyIdentifier);
            var response = action.Request();
            
            
            if (response.Status.Equals("FAILED") || response.Payload.Equals(string.Empty))
            {
                // TODO : dialog on payload is empty 
                return;
            }

            var unpackedPayload = JsonConvert.DeserializeObject<ConnectedToLobbyPayload>(response.Payload);
            
            GoToLobby(unpackedPayload.Information);
        }
    }
}
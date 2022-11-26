using System;
using System.Linq;
using System.Windows.Forms;
using CheckersClient.ClientActions;
using CheckersClient.Handlers;
using CheckersClient.Services;
using Domain.Models.Server;
using Domain.Models.Shared;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Forms
{
    public partial class GamesListForm : Form
    {
        private readonly string _userIdentifier;
        private string _selectedLobbyIdentifier;

        private readonly SourcesStorage _storage;
        
        private readonly BindingSource _leaderboardDataSource;
        private readonly BindingSource _lobbiesDataSource;
        private readonly ClientSocketListener _clientSocketListener;
        
        public GamesListForm(ClientSocketListener listener, string userIdentifier, SourcesStorage storage)
        {
            _clientSocketListener = listener;
            _leaderboardDataSource = storage.LeaderboardSource;
            _lobbiesDataSource = storage.LobbiesSource;
            _userIdentifier = userIdentifier;
            _storage = storage;
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
            var form = new GameForm(_userIdentifier, information, _clientSocketListener.GameBoard);
            _clientSocketListener.GameBoard.SetLobbyInformation(information);
            _clientSocketListener.Bind(ServerCommands.ServerMessage, new ServerMessageHandler(form));
            form.Show();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = Location;

            Hide();

            form.FormClosed += (o, args) =>
            {
                DesktopLocation = form.Location;
                _clientSocketListener.Unbind(ServerCommands.ServerMessage);
                Show();
            };
        }
        
        private void OnCreateLobby(object sender, EventArgs e)
        {
            var action = new CreateLobbyAction(_userIdentifier, GenerateGameSettings());
            var response = action.Request();

            if (response == null || response.Status.Equals("FAILED") || response.Payload.Equals(string.Empty))
            {
                MessageBox.Show("Failed to create lobby!", "Error", MessageBoxButtons.OK);
                return;
            }
            
            var unpackedPayload = JsonConvert.DeserializeObject<CreatedLobbyPayload>(response.Payload);
            
            if (unpackedPayload != null) 
                GoToLobby(unpackedPayload.Information);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _clientSocketListener.StopListeningToServer();
            var action = new DisconnectFromServerAction(_userIdentifier);
            action.Request();
        }

        private void RefreshLeaderboard(object sender, EventArgs e)
        {
            var action = new GetLeaderboardAction(_userIdentifier);

            var response = action.Request();

            if (response.Payload.Equals(string.Empty))
            {
                MessageBox.Show("Failed to get leaderboard", "Error", MessageBoxButtons.OK);
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

        private void RefreshLobbies(object sender, EventArgs e)
        {
            var action = new GetLobbiesAction(_userIdentifier);

            var response = action.Request();

            if (response.Payload.Equals(string.Empty))
            {
                MessageBox.Show("Failed to get lobbies", "Error", MessageBoxButtons.OK);
                return;
            }

            var list = JsonConvert.DeserializeObject<FetchedLobbiesPayload>(response.Payload).Lobbies;

            _lobbiesDataSource.DataSource = list.Select(s =>
                new {
                    ID = s.Identifier,
                    s.Name,
                    s.IsTournament
                });
        }
        
        private void OnFormLoaded(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            _clientSocketListener.TryStartListeningToServer();
            RefreshLeaderboard(sender, e);
            leaderboard.DataSource = _leaderboardDataSource;
            roomsList.DataSource = _lobbiesDataSource;
            difficultyComboBox.DataSource = Enum.GetValues(typeof(GameDifficulty));
        }
        
        private void SelectRoom(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var lobbyIdentifier = (string) roomsList.Rows[rowIndex].Cells[0].Value;
            _selectedLobbyIdentifier = lobbyIdentifier;
        }

        private void ConnectToGame(object sender, EventArgs e)
        {
            var action = new ConnectToLobbyAction(_userIdentifier, _selectedLobbyIdentifier);
            var response = action.Request();
            
            if (response.Status.Equals("FAILED") || response.Payload.Equals(string.Empty))
            {
                MessageBox.Show("Failed to connect to the lobby", "Error", MessageBoxButtons.OK);
                return;
            }

            var unpackedPayload = JsonConvert.DeserializeObject<ConnectedToLobbyPayload>(response.Payload);
            
            if (unpackedPayload != null)
                GoToLobby(unpackedPayload.Information);
        }
    }
}
using System;
using System.Windows.Forms;
using CheckersClient.Handlers;
using CheckersClient.Services;
using CheckersClient.ClientActions;
using Domain.Models.Server;
using Domain.Networking.Handlers;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.Forms
{
    public partial class IndexForm : Form
    {
        private ClientSocketListener _clientSocketListener;

        public IndexForm()
        {
            InitializeComponent();
        }

        private void InitializeNextScene(string userId, SourcesStorage storage)
        {
            _clientSocketListener.UserId = userId;
             
            var form = new GamesListForm( _clientSocketListener, userId, storage);
            form.StartPosition = FormStartPosition.Manual;         
            form.Location = this.Location;
            form.Show();
            Hide();

            form.FormClosed += (o, args) =>
            {
                this.Location = form.Location;
                Show();
            };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        { }

        private void OnAuthorize(object sender, EventArgs e)
        {
            var username = usernameBox.Text;
            var password = passwordBox.Text;

            if (username.Equals(string.Empty) || password.Equals(string.Empty))
                return;

            var connectAction = new ConnectToServerAction(username, password);
            
            var binder = new HandlerBinder();
            var storage = new SourcesStorage();
            
            _clientSocketListener = new ClientSocketListener(binder);
            binder.Bind(ServerCommands.LeaderboardUpdated, new LeaderboardUpdatedHandler(storage.LeaderboardSource));
            binder.Bind(ServerCommands.LobbiesUpdated, new LobbiesUpdatedHandler(storage.LobbiesSource));
            binder.Bind(ServerCommands.GameStarted, new GameStartedHandler(_clientSocketListener.GameBoard));
            binder.Bind(ServerCommands.MakeTurn, new MadeTurnHandler(_clientSocketListener.GameBoard));
            
            _clientSocketListener.TryStartListeningToServer();
            
            var response = connectAction.Request();
            if (response == null || response.Payload.Equals(string.Empty))
            {
                MessageBox.Show("Failed to establish connection", "Error", MessageBoxButtons.OK);
                return;
            }

            var payload = JsonConvert.DeserializeObject<ConnectionEstablishedPayload>(response.Payload);
            InitializeNextScene(payload.UserIdentifier, storage);
        }
    }
}
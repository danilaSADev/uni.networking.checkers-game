using System;
using System.Windows.Forms;
using CheckersClient.ClientActions;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient
{
    public partial class IndexForm : Form
    {
        public IndexForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void OnAuthorize(object sender, EventArgs e)
        {
            var username = usernameBox.Text;
            var password = passwordBox.Text;

            if (username.Equals(string.Empty) || password.Equals(string.Empty))
                return;

            var connectAction = new ConnectToServerAction(username, password);
            var response = connectAction.Request();

            var payload = JsonConvert.DeserializeObject<ConnectionEstablishedPayload>(response.Payload);

            if (payload == null)
                throw new Exception("Failed to establish connection: payload was null.");

            var form = new GamesListForm(payload.UserIdentifier);
            form.Show();
            Hide();

            form.FormClosed += (o, args) => { Show(); };
        }
    }
}
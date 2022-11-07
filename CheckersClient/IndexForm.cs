using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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

            if (response == null || response.Payload.Equals(string.Empty))
            {
                MessageBox.Show("Failed to establish connection", "Error", MessageBoxButtons.OK);
                return;
            }

            var payload = JsonConvert.DeserializeObject<ConnectionEstablishedPayload>(response.Payload);
            
            var form = new GamesListForm(payload.UserIdentifier);
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
    }
}
using System;
using System.Windows.Forms;
using CheckersClient.GameGraphics;
using CheckersClient.Main;

namespace CheckersClient
{
    public partial class CheckersForm : Form
    {

        private ConnectionEstablisher _connectionEstablisher;
        public CheckersForm()
        {
            _connectionEstablisher = new ConnectionEstablisher();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var form = new GameForm();
            form.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
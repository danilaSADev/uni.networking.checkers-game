using System;
using System.Drawing;
using System.Windows.Forms;
using Domain.Models.Shared;
using CheckersClient.ClientActions;
using CheckersClient.Handlers;
using CheckersClient.Properties;
using CheckersClient.Services;

namespace CheckersClient.Forms
{
    public partial class GameForm : Form, IGameForm
    {
        private readonly string _userId;
        private readonly LobbyInformation _information;
        private readonly Board _board;
        private Graphics _graphics;
        private bool _isPainted = false;

        public GameForm(string userId, LobbyInformation information, Board board)
        {
            _userId = userId;
            _information = information;
            _board = board;
            InitializeComponent();
        }
        
        private void GameForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            _board.InitializeBoard();
            _board.StateChanged += OnStatsChanged;
        }
        
        private Image GetCheckImage(Checker checker)
        {
            Image img = checker.Side.Equals(Side.White)
                ? Resources.check_white
                : Resources.check_black;
            
            if (checker.IsKing)
            {
                img = checker.Side == Side.White ? Resources.damka_white : Resources.damka_black;
            }

            return img;
        }

        private void DrawBoard()
        {
            panel1.Refresh();
            _graphics = panel1.CreateGraphics();
            var collection = _board.GetAllCheckers();
            
            foreach (var check in collection)
            {
                Image img = GetCheckImage(check);

                var x = 32 * (check.Position.X + 1);
                var y = 320 - 32 * (check.Position.Y + 2);
                _graphics.DrawImage(img, x, y);
            }

            if (_board.Selected != null)
            {
                Image img = GetCheckImage(_board.Selected);
                
                var x = 32 * (_board.Selected.Position.X + 1);
                var y = 320 - 32 * (_board.Selected.Position.Y + 2);
                
                _graphics.DrawImage(Resources.selected, x - 7, y + 2);
                _graphics.DrawImage(img, x, y);

                if (_board.Selected.MovementOptions == null)
                    return;

                foreach (var direction in _board.Selected.MovementOptions)
                {
                    _graphics.DrawImage(
                        Resources.possible_move2, 
                        32 * (direction.X + 1),
                        320 - 32 * (direction.Y + 2));
                }
            }
        }

        private void OnStatsChanged(object sender, GameStateChangedArgs args)
        {
            DrawBoard();
            DrawGameStats();
        }

        private void DrawGameStats()
        {
            // tODO : drawing all game stats
            playerSide.Text = _board.PlayerSide.ToString();
        }
        
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            var mouseX = e.X;
            var mouseY = e.Y;

            var panelWidth = panel1.Bounds.Width;
            var panelHeight = panel1.Bounds.Height;

            if (mouseX > 32
                && mouseX < panelWidth - 32
                && mouseY > 32
                && mouseY < panelHeight - 32)
            {
                var pos = new Vector(
                    (int)Math.Floor(mouseX / 32.0) - 1,
                    8 - (int)Math.Floor(mouseY / 32.0));

                label1.Text = $"{pos.X} {pos.Y}";

                _board.ClickedAt(pos);
                DrawBoard();
            }
        }

        private void OnLeavingLobby(object sender, FormClosedEventArgs e)
        {
            _board.StateChanged -= OnStatsChanged;
            _board.Reinitialize();
            var action = new DisconnectFromLobbyAction(_userId, _information.Identifier);
            action.Request();
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            if (!_isPainted)
            {
                _isPainted = true;
                DrawBoard();
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show("Your opponent decided to leave the game!", "Oooops!", MessageBoxButtons.OK);
        }

        public void ShowMessageAndDisconnect(string message)
        {
            ShowMessage(message);
            Close();
        }
    }
}
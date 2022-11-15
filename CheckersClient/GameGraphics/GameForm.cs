using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CheckersClient.ClientActions;
using CheckersClient.Main;
using CheckersClient.Properties;
using Domain.Models;
using Domain.Models.Shared;
using Domain.Payloads.Server;
using Newtonsoft.Json;

namespace CheckersClient.GameGraphics
{
    public partial class GameForm : Form
    {
        private readonly string _userId;
        private readonly LobbyInformation _information;
        private readonly Board _board;
        private Graphics _graphics;

        public GameForm(string userId, LobbyInformation information)
        {
            _userId = userId;
            _information = information;
            // TODO : create on game session initialization
            _board = new Board();
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            _board.InitializeBoard();
            DrawBoard();
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
                
                // var directions = _board.CalculatePossibleDirections(_board.Selected);
                // Console.WriteLine(directions.Count());
                
                foreach (var direction in _board.Selected.MovementOptions)
                {
                    _graphics.DrawImage(
                        Resources.possible_move2, 
                        32 * (direction.X + 1),
                        320 - 32 * (direction.Y + 2));
                }
            }
        }
        
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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
            var action = new DisconnectFromLobbyAction(_userId, _information.Identifier);
            var response = action.Request();

            if (response.Equals(string.Empty))
            {
                return;
            }

            var unpackedResponse = JsonConvert.DeserializeObject<DisconnectedFromLobbyPayload>(response.Payload);
            
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CheckersClient.GameGraphics {
    public partial class GameForm : Form
    {
        private Board _board;
        private Graphics _graphics;
        public GameForm()
        {
            _board = new Board();
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            _board.InitializeBoard();
            DrawBoard();
        }

        private void DrawBoard()
        {
            panel1.Refresh();
            _graphics = panel1.CreateGraphics();
            var collection = _board.GetAllCheckers();

            foreach (var check in collection)
            {
                Image img = check.Side.Equals(Side.White) 
                    ? Properties.Resources.check_white
                    : Properties.Resources.check_black;

                var x = 32 * (check.Position.X + 1);
                var y = 320 - 32 * (check.Position.Y + 2);
                _graphics.DrawImage(img, x, y);
            }

            if (_board.Selected != null)
            {
                Image img = _board.Selected.Side.Equals(Side.White) 
                    ? Properties.Resources.check_white
                    : Properties.Resources.check_black;
                var x = 32 * (_board.Selected.Position.X + 1);
                var y = 320 - 32 * (_board.Selected.Position.Y + 2);
                
                _graphics.DrawImage(Properties.Resources.selected, x - 7, y + 2);
                _graphics.DrawImage(img, x, y);
            }
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        { }
        
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
                Vector pos = new Vector(
                    (int)Math.Floor(mouseX / 32.0) - 1, 
                    8 - (int)Math.Floor(mouseY / 32.0));
                
                label1.Text = $"{pos.X} {pos.Y}";
                
                _board.ClickedAt(pos);
                DrawBoard();
            }
        }
    }
}
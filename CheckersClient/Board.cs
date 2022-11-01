using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CheckersClient
{
    public class Board
    {
        private Check _selected;
        private List<Check> _checkers;

        public Check Selected => _selected;
        
        public Board()
        {
            _checkers = new List<Check>();
        }

        public void InitializeBoard()
        {
            for (int y = 0; y < 3; y ++)
            {
                for (int x = 0; x < 7; x += 2)
                {
                    var check = new Check(Side.White, new Vector(x + y % 2, y));
                    _checkers.Add(check);
                }
            }
            
            for (int y = 0; y < 3; y ++)
            {
                for (int x = 0; x < 7; x += 2)
                {
                    var check = new Check(Side.Black, new Vector(x + (y + 1) % 2, 7 - y));
                    _checkers.Add(check);
                }
            }
        }

        public void ClickedAt(Vector pos)
        {
            if (_selected == null)
            {
                _selected = _checkers.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);
                return;
            }
            
            if (pos.X == Selected.Position.X && pos.Y == Selected.Position.Y)
            {
                _selected = null;
                return;
            }
            
            var any = _checkers.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);
            if (any == null)
            {
                _selected.Move(pos);
            }
            _selected = null;
        }
        
        public IEnumerable<Check> GetAllCheckers()
        {
            return _checkers.Except(new [] { Selected }).ToList();
        }

    }

    public class Damka : Check
    {
        public Damka(Side side, Vector position) : base(side, position)
        {}
    }
    
    public class Check
    {
        private Side _side;
        private Vector _position;

        public Side Side => _side;
        public Vector Position => _position;

        public Check(Side side, Vector position)
        {
            _side = side;
            _position = position;
        }

        public void Move(Vector newPosition)
        {
            _position = newPosition;
        }
    }

    public class Vector
    {
        public static Vector Zero => new Vector();
        
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        
        public Vector() {}

        public Vector(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point(Vector v) => new Point(v.X, v.Y);
        public static explicit operator Vector(Point p) => new Vector(p);
    }

    public enum Side
    {
        Black,
        White
    }
    
}
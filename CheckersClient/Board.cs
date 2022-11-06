using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CheckersClient
{
    public class Board
    {
        private readonly List<Check> _checkers;

        public Board()
        {
            _checkers = new List<Check>();
        }

        public Check Selected { get; private set; }

        public void InitializeBoard()
        {
            for (var y = 0; y < 3; y++)
            for (var x = 0; x < 7; x += 2)
            {
                var check = new Check(Side.White, new Vector(x + y % 2, y));
                _checkers.Add(check);
            }

            for (var y = 0; y < 3; y++)
            for (var x = 0; x < 7; x += 2)
            {
                var check = new Check(Side.Black, new Vector(x + (y + 1) % 2, 7 - y));
                _checkers.Add(check);
            }
        }

        public void ClickedAt(Vector pos)
        {
            if (Selected == null)
            {
                Selected = _checkers.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);
                return;
            }

            if (pos.X == Selected.Position.X && pos.Y == Selected.Position.Y)
            {
                Selected = null;
                return;
            }

            var any = _checkers.FirstOrDefault(x => x.Position.X == pos.X && x.Position.Y == pos.Y);
            if (any == null) Selected.Move(pos);
            Selected = null;
        }

        public IEnumerable<Check> GetAllCheckers()
        {
            return _checkers.Except(new[] { Selected }).ToList();
        }
    }

    public class Damka : Check
    {
        public Damka(Side side, Vector position) : base(side, position)
        {
        }
    }

    public class Check
    {
        public Check(Side side, Vector position)
        {
            Side = side;
            Position = position;
        }

        public Side Side { get; }

        public Vector Position { get; private set; }

        public void Move(Vector newPosition)
        {
            Position = newPosition;
        }
    }

    public class Vector
    {
        public Vector()
        {
        }

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

        public static Vector Zero => new Vector();

        public int X { get; set; }
        public int Y { get; set; }

        public static implicit operator Point(Vector v)
        {
            return new Point(v.X, v.Y);
        }

        public static explicit operator Vector(Point p)
        {
            return new Vector(p);
        }
    }

    public enum Side
    {
        Black,
        White
    }
}
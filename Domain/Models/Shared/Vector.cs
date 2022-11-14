using System.Drawing;

namespace Domain.Models.Shared
{
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

        public static Vector operator +(Vector v) => v;
        public static Vector operator -(Vector v) => new Vector(-v.X, v.Y);
        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector operator -(Vector v1, Vector v2) => new Vector(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector operator *(Vector v, int scalar) => new Vector(v.X * scalar, v.Y * scalar);

        public override bool Equals(object obj)
        {
            var v = obj as Vector;
            if (v == null)
                return false;
            return X == v.X && Y == v.Y;
        }

        public static implicit operator Point(Vector v)
        {
            return new Point(v.X, v.Y);
        }

        public static explicit operator Vector(Point p)
        {
            return new Vector(p);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Integer
{
    public readonly struct Point : IEquatable<Point>, IFormattable
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point With(int? x = null, int? y = null)
        {
            return new Point(x ?? X, y ?? Y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object obj)
            => obj is Point point && Equals(point);

        public bool Equals(Point other)
            => X == other.X && Y == other.Y;

        public override int GetHashCode()
            => (245009355 + X) * -1521134295 + Y;

        public static bool operator ==(Point left, Point right) => left.Equals(right);
        public static bool operator !=(Point left, Point right) => !left.Equals(right);

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "{0},{1}", X, Y);
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) => ToString(formatProvider);
        public override string ToString() => ToString(System.Globalization.CultureInfo.CurrentCulture);
    }
}

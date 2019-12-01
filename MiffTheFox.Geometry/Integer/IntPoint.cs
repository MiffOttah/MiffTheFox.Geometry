using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Integer
{
    public readonly struct IntPoint : IEquatable<IntPoint>, IFormattable
    {
        public static IntPoint Empty { get; } = new IntPoint();

        public int X { get; }
        public int Y { get; }

        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IntPoint With(int? x = null, int? y = null)
        {
            return new IntPoint(x ?? X, y ?? Y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object obj)
            => obj is IntPoint point && Equals(point);

        public bool Equals(IntPoint other)
            => X == other.X && Y == other.Y;

        public override int GetHashCode()
            => (245009355 + X) * -1521134295 + Y;

        public static bool operator ==(IntPoint left, IntPoint right) => left.Equals(right);
        public static bool operator !=(IntPoint left, IntPoint right) => !left.Equals(right);

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "{0},{1}", X, Y);
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) => ToString(formatProvider);
        public override string ToString() => ToString(System.Globalization.CultureInfo.CurrentCulture);

        public static IntPoint operator +(IntPoint p1, IntPoint p2)
        {
            return new IntPoint(p1.X + p2.X, p1.Y + p2.Y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    public readonly struct Point : IEquatable<Point>, IFormattable
    {
        public static Point Empty { get; } = new Point();

        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            _InternalUtility.ValidateDouble(x, nameof(x));
            _InternalUtility.ValidateDouble(y, nameof(y));

            X = x;
            Y = y;
        }

        public Point With(double? x = null, double? y = null)
        {
            return new Point(x ?? X, y ?? Y);
        }

        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object obj)
            => obj is Point point && Equals(point);

        public bool Equals(Point other)
            => X == other.X && Y == other.Y;

        public static bool operator ==(Point left, Point right) => left.Equals(right);
        public static bool operator !=(Point left, Point right) => !left.Equals(right);


        public string ToString(string format, IFormatProvider formatProvider)
        {
            return X.ToString(format, formatProvider) + "," + Y.ToString(format, formatProvider);
        }

        public string ToString(IFormatProvider formatProvider) => ToString("g", formatProvider);
        public override string ToString() => ToString("g", System.Globalization.CultureInfo.CurrentCulture);

        public override int GetHashCode() => (245009355 + X.GetHashCode()) * -1521134295 + Y.GetHashCode();

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static implicit operator Point(Integer.IntPoint p) => new Point(p.X, p.Y);
        public static explicit operator Integer.IntPoint(Point p) => p.ToIntPoint();

        public Integer.IntPoint ToIntPoint(MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return new Integer.IntPoint((int)Math.Round(X, rounding), (int)Math.Round(Y, rounding));
        }
    }
}

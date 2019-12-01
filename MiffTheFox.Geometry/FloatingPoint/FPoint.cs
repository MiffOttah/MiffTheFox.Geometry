using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.FloatingPoint
{
    public readonly struct FPoint : IEquatable<FPoint>, IFormattable
    {
        public static FPoint Empty { get; } = new FPoint();

        public double X { get; }
        public double Y { get; }

        public FPoint(double x, double y)
        {
            if (double.IsInfinity(x) || double.IsNaN(x)) throw new ArgumentException("Parameter cannot be infinity or NaN.", nameof(x));
            if (double.IsInfinity(y) || double.IsNaN(y)) throw new ArgumentException("Parameter cannot be infinity or NaN.", nameof(x));

            X = x;
            Y = y;
        }

        public FPoint With(double? x = null, double? y = null)
        {
            return new FPoint(x ?? X, y ?? Y);
        }

        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object obj)
            => obj is FPoint point && Equals(point);

        public bool Equals(FPoint other)
            => X == other.X && Y == other.Y;

        public static bool operator ==(FPoint left, FPoint right) => left.Equals(right);
        public static bool operator !=(FPoint left, FPoint right) => !left.Equals(right);


        public string ToString(string format, IFormatProvider formatProvider)
        {
            return X.ToString(format, formatProvider) + "," + Y.ToString(format, formatProvider);
        }

        public string ToString(IFormatProvider formatProvider) => ToString("g", formatProvider);
        public override string ToString() => ToString("g", System.Globalization.CultureInfo.CurrentCulture);

        public override int GetHashCode() => (245009355 + X.GetHashCode()) * -1521134295 + Y.GetHashCode();

        public static FPoint operator +(FPoint p1, FPoint p2)
        {
            return new FPoint(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static implicit operator FPoint(Integer.Point p) => new FPoint(p.X, p.Y);
        public static explicit operator Integer.Point(FPoint p) => p.ToIntPoint();

        public Integer.Point ToIntPoint(MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return new Integer.Point((int)Math.Round(X, rounding), (int)Math.Round(Y, rounding));

        }
    }
}

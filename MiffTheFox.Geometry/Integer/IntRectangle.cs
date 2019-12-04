using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Integer
{
    public readonly struct IntRectangle : IEquatable<IntRectangle>, IFormattable
    {
        public static readonly IntRectangle Empty = new IntRectangle();

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public IntPoint Position => new IntPoint(X, Y);
        public IntSize Size => new IntSize(Width, Height);

        public IntRectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public IntRectangle(IntPoint position, IntSize size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public IntRectangle With(int? x = null, int? y = null, int? width = null, int? height = null)
        {
            return new IntRectangle(x ?? X, y ?? Y, width ?? Width, height ?? Height);
        }

        public override bool Equals(object obj)
        {
            return obj is IntRectangle r && Equals(r);
        }

        public bool Equals(IntRectangle other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height;
        }

        public override int GetHashCode()
        {
            var hashCode = 466501756;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(IntRectangle left, IntRectangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IntRectangle left, IntRectangle right)
        {
            return !left.Equals(right);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var b = new StringBuilder();
            b.Append(X.ToString(format, formatProvider));
            b.Append(',');
            b.Append(Y.ToString(format, formatProvider));
            b.Append(';');
            b.Append(Width.ToString(format, formatProvider));
            b.Append('x');
            b.Append(Height.ToString(format, formatProvider));
            return b.ToString();
        }
        public override string ToString() => ToString(null, null);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Integer
{
    public readonly struct IntSize : IEquatable<IntSize>, IFormattable
    {
        public static IntSize Empty { get; } = new IntSize();

        public int Width { get; }
        public int Height { get; }

        public IntSize(int w, int h)
        {
            Width = w;
            Height = h;
        }

        public IntSize With(int? w = null, int? h = null)
        {
            return new IntSize(w ?? Width, h ?? Height);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = Width;
            y = Height;
        }

        public override bool Equals(object obj)
            => obj is IntSize point && Equals(point);

        public bool Equals(IntSize other)
            => Width == other.Width && Height == other.Height;

        public override int GetHashCode() =>
            ((245009355 + Width) * -1521134295) + Height;

        public static bool operator ==(IntSize left, IntSize right) => left.Equals(right);
        public static bool operator !=(IntSize left, IntSize right) => !left.Equals(right);

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "{0}x{1}", Width, Height);
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) => ToString(formatProvider);
        public override string ToString() => ToString(System.Globalization.CultureInfo.CurrentCulture);

        public static IntSize operator +(IntSize p1, IntSize p2)
        {
            return new IntSize(p1.Width + p2.Width, p1.Height + p2.Height);
        }
    }
}

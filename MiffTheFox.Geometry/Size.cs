using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    /// <summary>
    /// Represents a rectangular area in 2D space with no specific location.
    /// </summary>
    public readonly struct Size : IEquatable<Size>, IFormattable
    {
        public static Size Empty { get; } = new Size();

        public double Width { get; }
        public double Height { get; }

        public Size(double w, double h)
        {
            _InternalUtility.ValidateDouble(w, nameof(w));
            _InternalUtility.ValidateDouble(h, nameof(h));

            Width = w;
            Height = h;
        }

        public Size With(double? w = null, double? h = null)
        {
            return new Size(w ?? Width, h ?? Height);
        }

        public void Deconstruct(out double w, out double h)
        {
            w = Width;
            h = Height;
        }

        public override bool Equals(object obj)
            => obj is Size Size && Equals(Size);

        public bool Equals(Size other)
            => Width == other.Width && Height == other.Height;

        public static bool operator ==(Size left, Size right) => left.Equals(right);
        public static bool operator !=(Size left, Size right) => !left.Equals(right);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Width.ToString(format, formatProvider) + "x" + Height.ToString(format, formatProvider);
        }

        public string ToString(IFormatProvider formatProvider) => ToString("g", formatProvider);
        public override string ToString() => ToString("g", System.Globalization.CultureInfo.CurrentCulture);

        public static Size operator +(Size p1, Size p2)
        {
            return new Size(p1.Width + p2.Width, p1.Height + p2.Height);
        }

        public static implicit operator Size(Integer.IntSize s) => new Size(s.Width, s.Height);
        public static explicit operator Integer.IntSize(Size s) => s.ToIntSize();

        public Integer.IntSize ToIntSize(MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return new Integer.IntSize((int)Math.Round(Width, rounding), (int)Math.Round(Height, rounding));
        }

        public override int GetHashCode()
            => (21801 + Width.GetHashCode()) * -1521134295 + Height.GetHashCode();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    public readonly struct Rectangle : IEquatable<Rectangle>, IFormattable
    {
        public static readonly Rectangle Empty = new Rectangle();

        public double X { get; } 
        public double Y { get; } 
        public double Width { get; }
        public double Height { get; }

        public Point Position => new Point(X, Y);
        public Size Size => new Size(Width, Height);

        public double Right => X + Width;
        public double Bottom => Y + Height;

        /// <summary>
        /// If the Rectangle's size is negative, get a rectangle
        /// compensating for the negative size by projecting backwards.
        /// Otherwise returns the same rectangle.
        /// </summary>
        public Rectangle Canonical
        {
            get
            {
                (double x, double y, double width, double height) = this;
                if (width < 0)
                {
                    x -= width;
                    width = -width;
                }
                if (height < 0)
                {
                    y -= height;
                    height = -height;
                }
                return new Rectangle(x, y, width, height);
            }
        }

        public Rectangle(double x, double y, double width, double height)
        {
            _InternalUtility.ValidateDouble(x, nameof(x));
            _InternalUtility.ValidateDouble(y, nameof(y));
            _InternalUtility.ValidateDouble(width, nameof(width));
            _InternalUtility.ValidateDouble(height, nameof(height));

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectangle(Point position, Size size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public static Rectangle FromLTRB(double left, double top, double right, double bottom)
            => new Rectangle(left, top, right - left, bottom - top);

        public void Deconstruct(out double x, out double y, out double width, out double height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        public Rectangle With(double? x = null, double? y = null, double? width = null, double? height = null)
        {
            return new Rectangle(x ?? X, y ?? Y, width ?? Width, height ?? Height);
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle r && Equals(r);
        }

        public bool Equals(Rectangle other)
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

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Rectangle left, Rectangle right)
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

        public static implicit operator Rectangle(Integer.IntRectangle r) => new Rectangle(r.X, r.Y, r.Width, r.Height);
        public static explicit operator Integer.IntRectangle(Rectangle r) => r.ToIntRectangle();

        public Integer.IntRectangle ToIntRectangle(MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return new Integer.IntRectangle(
                (int)Math.Round(X, rounding),
                (int)Math.Round(Y, rounding),
                (int)Math.Round(Width, rounding),
                (int)Math.Round(Height, rounding)
            );
        }
    }
}

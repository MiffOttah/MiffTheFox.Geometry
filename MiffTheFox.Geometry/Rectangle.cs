using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MiffTheFox.Geometry
{
    /// <summary>
    /// Represents a specific rectangular area in 2D space.
    /// </summary>
    public readonly struct Rectangle : IEquatable<Rectangle>, IFormattable
    {
        /// <summary>
        /// An area centered on 0,0 with a size of 0x0
        /// </summary>
        public static readonly Rectangle Empty = new Rectangle();

        /// <summary>
        /// The X coordinate of the left edge of the rectangle.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The Y coordinate of the top edge of the rectangle.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public double Height { get; }

        /// <summary>
        /// The position of the top-left corner of the rectangle.
        /// </summary>
        public Point Position => new Point(X, Y);

        /// <summary>
        /// The size of the rectangle.
        /// </summary>
        public Size Size => new Size(Width, Height);

        /// <summary>
        /// The X coordiate of the right edge of the rectangle.
        /// </summary>
        public double Right => X + Width;

        /// <summary>
        /// The Y cooridate of the bottom edge of the rectangle.
        /// </summary>
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

        /// <summary>
        /// Gets the point in the center of the rectangle.
        /// </summary>
        public Point Midpoint => new Point(X + Width / 2, Y + Height / 2);

        /// <summary>
        /// Create a rectangle at the specified location with the specified size.
        /// </summary>
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

        /// <summary>
        /// Create a rectangle at the specified location with the specified size.
        /// </summary>
        public Rectangle(Point position, Size size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Creates a rectangle where the edges are at specific coordinates.
        /// </summary>
        public static Rectangle FromLTRB(double left, double top, double right, double bottom)
            => new Rectangle(left, top, right - left, bottom - top);

        /// <summary>
        /// Creates a rectangle representing the area between two points.
        /// </summary>
        public static Rectangle Between(Point point1, Point point2)
        {
            double l, r, t, b;
            (l, r) = _InternalUtility.MinMax(point1.X, point2.X);
            (t, b) = _InternalUtility.MinMax(point1.Y, point2.Y);
            return FromLTRB(l, t, r, b);
        }

        /// <summary>
        /// Creates a rectangle with the specified size with a midpoint at the specified point.
        /// </summary>
        public static Rectangle Around(Point midpoint, Size size)
        {
            return new Rectangle(
                midpoint.X - (size.Width / 2),
                midpoint.Y - (size.Height / 2),
                size.Width,
                size.Height
            );
        }
        /// <summary>
        /// Creates a rectangle with the specified size with a midpoint at the specified point.
        /// </summary>
        public static Rectangle Around(double x, double y, double width, double right)
            => Around(new Point(x, y), new Size(width, right));

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

        public string Serialize()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:R},{1:R};{2:R}x{3:R}", X, Y, Width, Height);
        }
        public static Rectangle DeSerialize(string serialized)
        {
            double to(char c)
            {
                int ix = serialized.IndexOf(c);
                if (ix == -1) throw new FormatException();
                string r = serialized.Remove(ix);
                serialized = serialized.Substring(ix + 1);
                return double.Parse(r, CultureInfo.InvariantCulture);
            }

            double x = to(',');
            double y = to(';');
            double w = to('x');
            double h = double.Parse(serialized, CultureInfo.InvariantCulture);

            return new Rectangle(x, y, w, h);
        }

        /// <summary>
        /// Creates a rectangle where each edge is moved by the specified values away from the midpoint of the rectangle.
        /// </summary>
        /// <remarks>Negative values will move the edges closer to the midpoint.</remarks>
        public Rectangle Inflate(double left, double top, double right, double bottom)
        {
            return new Rectangle(
                X - left,
                Y - top,
                Width + left + right,
                Height + top + bottom
            );
        }

        /// <summary>
        /// Creates a rectangle where each edge is moved by the specified values away from the midpoint of the rectangle.
        /// </summary>
        /// <remarks>Negative values will move the edges closer to the midpoint.</remarks>
        public Rectangle Inflate(double x, double y)
        {
            return new Rectangle(
                X - x,
                Y - y,
                Width + x * 2,
                Height + y * 2
            );
        }

        /// <summary>
        /// Creates a rectangle where each edge is moved by the specified value away from the midpoint of the rectangle.
        /// </summary>
        /// <remarks>A negative value will move the edges closer to the midpoint.</remarks>
        public Rectangle Inflate(double amount) => Inflate(amount, amount);
    }
}

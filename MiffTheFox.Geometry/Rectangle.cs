using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    public readonly struct Rectangle
    {
        public static readonly Rectangle Empty = new Rectangle();

        public double X { get; } 
        public double Y { get; } 
        public double Width { get; }
        public double Height { get; }

        public Point Position => new Point(X, Y);
        public Size Size => new Size(Width, Height);

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
    }
}

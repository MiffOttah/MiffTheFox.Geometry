using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    public readonly struct Portion
    {
        public double Value { get; }

        public Portion(double value)
        {
            if (double.IsInfinity(value) || double.IsNaN(value)) throw new ArgumentException("Value cannot be infinity or NaN.", nameof(value));
            if (value < 0.0 || value > 1.0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0.0 and 1.0.");

            Value = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    /// <summary>
    /// Repersents a portion of a whole as a real value between 0 and 1, inclusive.
    /// </summary>
    public readonly struct Portion : IFormattable, IEquatable<Portion>, IComparable<Portion>
    {
        /// <summary>
        /// The value of this portion, between 0 and 1, inclusive
        /// </summary>
        public double Value { get; }

        /// <summary>An empty portion with a value of 0, representing nothing from the whole.</summary>
        public static Portion Empty { get; } = new Portion(0);
        /// <summary>An full portion with a value of 1, representing the entire whole.</summary>
        public static Portion Full { get; } = new Portion(1);

        /// <summary>A portion that, together with this portion, represent a whole (adding to 1).</summary>
        public Portion Complement => new Portion(1.0 - Value);

        /// <summary>
        /// Creates a new portion with a specified value.
        /// </summary>
        /// <param name="value">The value of the portion, between 0 and 1 inclusive.</param>
        /// <exception cref="ArgumentException">The value provided was not a real number.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The value provided was less than 0 or greater than 1.</exception>
        public Portion(double value)
        {
            if (double.IsInfinity(value) || double.IsNaN(value)) throw new ArgumentException("Value cannot be infinity or NaN.", nameof(value));
            if (value < 0.0 || value > 1.0) throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0.0 and 1.0.");

            Value = value;
        }

        public override int GetHashCode() => -1937169414 + Value.GetHashCode();

        public override bool Equals(object obj) => obj is Portion portion && Equals(portion);
        public bool Equals(Portion other) => Value == other.Value;
        public int CompareTo(Portion other) => Value.CompareTo(other.Value);

        public static bool operator ==(Portion left, Portion right) => left.Value == right.Value;
        public static bool operator !=(Portion left, Portion right) => left.Value != right.Value;
        public static bool operator >(Portion left, Portion right) => left.Value > right.Value;
        public static bool operator >=(Portion left, Portion right) => left.Value >= right.Value;
        public static bool operator <(Portion left, Portion right) => left.Value < right.Value;
        public static bool operator <=(Portion left, Portion right) => left.Value <= right.Value;

        public override string ToString() => Value.ToString("G", null);
        public string ToString(string format) => Value.ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        /// <summary>
        /// Find the value between 0 and <paramref name="max"/> corresponding to this portion.
        /// </summary>
        /// <param name="max">The maximum value that represents the full portion.</param>
        /// <returns>A value between 0 and <paramref name="max"/> corresponding to this portion. A portion of 0 always returns 0; a portion of 1 always returns <paramref name="max"/>.</returns>
        public double Lerp(double max) => Value * max;

        /// <summary>
        /// Find the value between <paramref name="min"/> and <paramref name="max"/> corresponding to this portion.
        /// </summary>
        /// <param name="min">The minimum value that represents the empty portion.</param>
        /// <param name="max">The maximum value that represents the full portion.</param>
        /// <returns>A value between <paramref name="min"/> and <paramref name="max"/> corresponding to this portion. A portion of 0 always returns <paramref name="min"/>, a portion of 1 always returns <paramref name="max"/></returns>
        /// <remarks>If <paramref name="min"/> is greater than <paramref name="max"/>, greater portions will result in a lower value and vice versa.</remarks>
        public double Lerp(double min, double max) => Value * (max - min) + min;

        /// <summary>
        /// Find the integral value between 0 and <paramref name="max"/> corresponding to this portion.
        /// </summary>
        /// <param name="max">The maximum value that represents the full portion.</param>
        /// <param name="midpointRounding">The midpoint rounding method to use when the result is not an even integer.</param>
        /// <returns>An integral value between 0 and <paramref name="max"/> corresponding to this portion. A portion of 0 always returns 0; a portion of 1 always returns <paramref name="max"/>.</returns>
        public int Lerp(int max, MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
            => (int)Math.Round(Lerp((double) max), midpointRounding);

        /// <summary>
        /// Find the integral value between <paramref name="min"/> and <paramref name="max"/> corresponding to this portion.
        /// </summary>
        /// <param name="min">The minimum value that represents the empty portion.</param>
        /// <param name="max">The maximum value that represents the full portion.</param>
        /// <param name="midpointRounding">The midpoint rounding method to use when the result is not an even integer.</param>
        /// <returns>A integral value between <paramref name="min"/> and <paramref name="max"/> corresponding to this portion. A portion of 0 always returns <paramref name="min"/>, a portion of 1 always returns <paramref name="max"/></returns>
        /// <remarks>If <paramref name="min"/> is greater than <paramref name="max"/>, greater portions will result in a lower value and vice versa.</remarks>
        public int Lerp(int min, int max, MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
            => (int)Math.Round(Lerp((double) min, max), midpointRounding);

        public static double operator *(Portion portion, double max) => portion.Lerp(max);
        public static double operator *(double max, Portion portion) => portion.Lerp(max);
        public static int operator *(Portion portion, int max) => portion.Lerp(max);
        public static int operator *(int max, Portion portion) => portion.Lerp(max);

        /// <summary>
        /// Repersents the portion that is the fraction of <paramref name="numerator"/> to <paramref name="denominator"/>.
        /// </summary>
        /// <param name="numerator">The numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <returns>A portion equal to the fraction of <paramref name="numerator"/> to <paramref name="denominator"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="denominator"/> is less than or equal to zero, or <paramref name="numerator"/> is less than zero or greater than <paramref name="denominator"/>.</exception>
        public static Portion Fraction(int numerator, int denominator)
        {
            if (numerator < 0) throw new ArgumentOutOfRangeException(nameof(numerator), "Numerator cannot be negative.");
            if (denominator <= 0) throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be negative or zero.");
            if (numerator > denominator) throw new ArgumentOutOfRangeException(nameof(numerator), "Numerator cannot be greater than denominator.");

            return new Portion((double)numerator / denominator);
        }

        /// <summary>
        /// Represents the porition that is equal to the fraction of 1 to <paramref name="denominator"/>
        /// </summary>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <returns>A portion equal to the fraction of 1 to <paramref name="denominator"/></returns>
        public static Portion Fraction(int denominator) => Fraction(1, denominator);

        private static Portion _CastIn(double value)
        {
            // helper method to throw InvalidCastException rather than ArgumentException
            if (double.IsInfinity(value) || double.IsNaN(value) || value < 0 || value > 1) throw new InvalidCastException();
            return new Portion(value);
        }

        public static explicit operator double(Portion v) => v.Value;
        public static explicit operator float(Portion v) => Convert.ToSingle(v.Value);
        public static explicit operator decimal(Portion v) => Convert.ToDecimal(v.Value);
        public static explicit operator byte(Portion v) => Convert.ToByte(v.Lerp(255));

        public static explicit operator Portion(double v) => _CastIn(v);
        public static explicit operator Portion(float v) => _CastIn(v);
        public static explicit operator Portion(decimal v) => _CastIn(Convert.ToDouble(v));
        public static explicit operator Portion(byte v) => Fraction(v, 255);
    }
}

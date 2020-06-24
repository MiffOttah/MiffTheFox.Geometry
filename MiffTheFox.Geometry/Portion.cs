using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    public readonly struct Portion : IEquatable<Portion>, IComparable<Portion>
    {
        public double Value { get; }

        public static Portion Empty { get; } = new Portion(0);
        public static Portion Full { get; } = new Portion(1);

        public Portion Complement => new Portion(1.0 - Value);

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

        public double Lerp(double max) => Value * max;
        public double Lerp(double min, double max) => Value * (max - min) + min;

        public int Lerp(int max, MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
            => (int)Math.Round(Lerp((double) max), midpointRounding);
        public int Lerp(int min, int max, MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
            => (int)Math.Round(Lerp((double) min, max), midpointRounding);

        public static double operator *(Portion portion, double max) => portion.Lerp(max);
        public static double operator *(double max, Portion portion) => portion.Lerp(max);
        public static int operator *(Portion portion, int max) => portion.Lerp(max);
        public static int operator *(int max, Portion portion) => portion.Lerp(max);

        public static Portion Fraction(int numerator, int denominator)
        {
            if (numerator < 0) throw new ArgumentOutOfRangeException(nameof(numerator), "Numerator cannot be negative.");
            if (denominator <= 0) throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be negative or zero.");
            if (numerator > denominator) throw new ArgumentOutOfRangeException(nameof(numerator), "Numerator cannot be greater than denominator.");

            return new Portion((double)numerator / denominator);
        }
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

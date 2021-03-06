﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    public readonly struct Angle : IFormattable, IEquatable<Angle>, IComparable<Angle>
    {
        public static readonly Angle Zero = new Angle(0);

        public double Turns { get; }
        public double Radians => ToUnit(AngleUnit.Radians);

        /// <summary>
        /// Returns an angle greater than or equal to 0τ but less than 1τ that is coterminal with this angle.
        /// </summary>
        public Angle Canonical => new Angle(Turns - Math.Floor(Turns));

        public Angle(double value, AngleUnit unit)
            => Turns = _CheckValue(value, nameof(value)) / _GetConstantForAngleUnit(unit);

        public Angle(double turns)
            => Turns = _CheckValue(turns, nameof(turns));

        public Angle(Portion turns)
            => Turns = turns.Value; // Portions can be trusted to not be infinity or NaN

        private static double _CheckValue(double value, string parameterName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentException("Value cannot be NaN or infinity.", parameterName);
            return value;
        }

        public double ToUnit(AngleUnit unit) => Turns * _GetConstantForAngleUnit(unit);

        private static double _GetConstantForAngleUnit(AngleUnit unit)
        {
            const double tau = Math.PI * 2;
            switch (unit)
            {
                case AngleUnit.Turns: return 1;
                case AngleUnit.Degrees: return 360;
                case AngleUnit.Radians: return tau;
                case AngleUnit.PiRadians: return 2;
                case AngleUnit.Gradians: return 400;
                case AngleUnit.Percent: return 100;
                default: throw new ArgumentException("Unknown angle unit.", nameof(unit));
            }
        }

        public override string ToString() => ToString(null, null);
        public string ToString(string format) => ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var unit = AngleUnit.Radians;
            if (!string.IsNullOrEmpty(format))
            {
                if (format == "P" || format == "p")
                {
                    format = null;
                    unit = AngleUnit.Percent;
                }
                else
                {
                    switch (format[format.Length - 1])
                    {
                        case 'τ': unit = AngleUnit.Turns; format = format.Remove(format.Length - 1); break;
                        case 'π': unit = AngleUnit.PiRadians; format = format.Remove(format.Length - 1); break;
                        case '°': unit = AngleUnit.Degrees; format = format.Remove(format.Length - 1); break;
                        case '%': unit = AngleUnit.Percent; format = format.Remove(format.Length - 1); break;
                        default:

                            if (format.EndsWith(" gon", StringComparison.InvariantCultureIgnoreCase))
                            {
                                format = format.Remove(format.Length - 4);
                                unit = AngleUnit.Gradians;
                            }
                            else if (format == "gon")
                            {
                                format = null;
                                unit = AngleUnit.Gradians;
                            }

                            break;
                    }
                }
            }
            return ToString(unit, format, formatProvider);
        }
        public string ToString(AngleUnit unit, string decimalFormat, IFormatProvider formatProvider)
        {
            string valueString = ToUnit(unit).ToString(decimalFormat, formatProvider);

            switch (unit)
            {
                case AngleUnit.Turns: return valueString + "τ";
                case AngleUnit.Degrees: return valueString + "°";
                case AngleUnit.Radians: return valueString;
                case AngleUnit.PiRadians: return valueString + "π";
                case AngleUnit.Gradians: return valueString + " gon";
                case AngleUnit.Percent: return valueString + "%";
                default: throw new ArgumentException("Unknown angle unit.", nameof(unit));
            }
        }

        public override bool Equals(object obj) => obj is Angle angle && Equals(angle);
        public bool Equals(Angle other) => Turns == other.Turns;
        public override int GetHashCode() => 1224962691 + Turns.GetHashCode();

        public int CompareTo(Angle other)
        {
            return Turns.CompareTo(other.Turns);
        }

        public static bool operator ==(Angle left, Angle right) => left.Turns == right.Turns;
        public static bool operator !=(Angle left, Angle right) => left.Turns != right.Turns;
        public static bool operator >(Angle x, Angle y) => x.Turns > y.Turns;
        public static bool operator >=(Angle x, Angle y) => x.Turns >= y.Turns;
        public static bool operator <(Angle x, Angle y) => x.Turns < y.Turns;
        public static bool operator <=(Angle x, Angle y) => x.Turns <= y.Turns;

        public static Angle operator +(Angle x, Angle y) => new Angle(x.Turns + y.Turns);
        public static Angle operator -(Angle x, Angle y) => new Angle(x.Turns - y.Turns);
        public static Angle operator *(Angle portion, double multiple) => new Angle(portion.Turns * multiple);
        public static Angle operator *(double multiple, Angle portion) => new Angle(portion.Turns * multiple);
        public static Angle operator /(Angle portion, double divisor) => new Angle(portion.Turns / divisor);

        public static Angle ArcSin(double sine) => new Angle(Math.Asin(sine), AngleUnit.Radians);
        public static Angle ArcCos(double cosine) => new Angle(Math.Acos(cosine), AngleUnit.Radians);
        public static Angle ArcTan(double tangent) => new Angle(Math.Atan(tangent), AngleUnit.Radians);
        public static Angle ArcTan2(double y, double x) => new Angle(Math.Atan2(y, x), AngleUnit.Radians);

        public double Cos() => Math.Cos(Radians);
        public double Sin() => Math.Sin(Radians);
        public double Tan() => Math.Tan(Radians);
        public double Cosh() => Math.Cosh(Radians);
        public double Sinh() => Math.Sinh(Radians);
        public double Tanh() => Math.Tanh(Radians);

        public Point ToPoint()
        {
            double theta = Radians; // prefetch radians value
            return new Point(Math.Cos(theta), Math.Sin(theta));
        }
        public Point ToPoint(double radius)
        {
            double theta = Radians; // prefetch radians value
            return new Point(Math.Cos(theta) * radius, Math.Sin(theta) * radius);
        }
        public Point ToPoint(Point center, double radius)
        {
            return ToPoint(radius) + center;
        }
    }

    public enum AngleUnit : byte
    {
        /// <summary>Turns, where a complete circle is 1. Also the unit in radians as a multiple of τ (2π).</summary>
        Turns = 0,
        /// <summary>Degrees, where a complete circle is 360°.</summary>
        Degrees = 1,
        /// <summary>Radians, where a complete circle is 2π.</summary>
        Radians = 2,
        /// <summary>Radians as a multiple of π, where a complete circle is 2.</summary>
        PiRadians = 3,
        /// <summary>Gradians, where a complete circle is 400.</summary>
        Gradians = 4,
        /// <summary>Percent, where a complete circle is 100.</summary>
        Percent = 5
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    public readonly struct Angle
    {
        public double Turns { get; }
        public double Radians => ToUnit(AngleUnit.Radians);

        public Angle(double value, AngleUnit unit)
            => Turns = value / _GetConstantForAngleUnit(unit);

        public Angle(double turns)
            => Turns = turns;

        public Angle(Portion turns)
            => Turns = turns.Value;

        public double ToUnit(AngleUnit unit) => Turns * _GetConstantForAngleUnit(unit);
        
        private static double _ConvertUnit(double value, AngleUnit from, AngleUnit to)
        {
            double factor = _GetConstantForAngleUnit(to) / _GetConstantForAngleUnit(from);
            return value * factor;
        }

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

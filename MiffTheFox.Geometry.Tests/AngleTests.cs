using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class AngleTests
    {
        const double DELTA = 1e-8; // good ol' floating-points
        
        [TestMethod]
        [DataRow(0.5, AngleUnit.Turns, 0.5)]
        [DataRow(90, AngleUnit.Degrees, 0.25)]
        [DataRow(270, AngleUnit.Degrees, 0.75)]
        [DataRow(Math.PI * 0.3, AngleUnit.Radians, 0.15)]
        [DataRow(0.8, AngleUnit.PiRadians, 0.4)]
        [DataRow(250, AngleUnit.Gradians, 0.625)]
        [DataRow(80, AngleUnit.Percent, 0.8)]
        [DataRow(0, AngleUnit.Radians, 0)] // zero angle
        [DataRow(400, AngleUnit.Gradians, 1)] // complete angle
        [DataRow(540, AngleUnit.Degrees, 1.5)] // over-extended angle
        [DataRow(Math.PI * 6.6, AngleUnit.Radians, 3.3)] // over-extended angle
        [DataRow(-90, AngleUnit.Degrees, -0.25)] // negative angle
        public void TestCreationFromUnits(double value, AngleUnit unit, double expected)
        {
            var angle = new Angle(value, unit);
            Assert.AreEqual(expected, angle.Turns);
        }

        [TestMethod]
        [DataRow(double.NaN)]
        [DataRow(double.NegativeInfinity)]
        [DataRow(double.PositiveInfinity)]
        public void TestFailedCreation(double value)
        {
            Assert.ThrowsException<ArgumentException>(() => new Angle(value));
            Assert.ThrowsException<ArgumentException>(() => new Angle(value, AngleUnit.Radians));
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(0.25, Math.PI * 0.5)]
        [DataRow(0.5, Math.PI)]
        [DataRow(0.75, Math.PI * 1.5)]
        [DataRow(1, Math.PI * 2)]
        [DataRow(1.5, Math.PI * 3)]
        public void TestRadiansProperty(double turns, double expectedRadians)
        {
            var angle = new Angle(turns);
            Assert.AreEqual(expectedRadians, angle.Radians);
        }

        [TestMethod]
        // put the expected first and turns last so that the test can reuse the
        // test data for TestCreationFromUnits.
        [DataRow(0.5, AngleUnit.Turns, 0.5)]
        [DataRow(90, AngleUnit.Degrees, 0.25)]
        [DataRow(270, AngleUnit.Degrees, 0.75)]
        [DataRow(Math.PI * 0.3, AngleUnit.Radians, 0.15)]
        [DataRow(0.8, AngleUnit.PiRadians, 0.4)]
        [DataRow(250, AngleUnit.Gradians, 0.625)]
        [DataRow(80, AngleUnit.Percent, 0.8)]
        [DataRow(0, AngleUnit.Radians, 0)] // zero angle
        [DataRow(400, AngleUnit.Gradians, 1)] // complete angle
        [DataRow(540, AngleUnit.Degrees, 1.5)] // over-extended angle
        [DataRow(Math.PI * 6.6, AngleUnit.Radians, 3.3)] // over-extended angle
        [DataRow(-90, AngleUnit.Degrees, -0.25)] // negative angle
        public void TestToUnit(double expected, AngleUnit toUnit, double turns)
        {
            var angle = new Angle(turns);
            Assert.AreEqual(expected, angle.ToUnit(toUnit));
        }

        [TestMethod]
        [DataRow(0, null, "0", DisplayName = "Null format string")]
        [DataRow(1, "F2", "6.28", DisplayName = "Radians F2")]
        [DataRow(0.5, "F2τ", "0.50τ", DisplayName = "Turns F2")]
        [DataRow(0.25, "F1°", "90.0°", DisplayName = "Degrees F1")]
        [DataRow(0.25, "F1%", "25.0%", DisplayName = "Percent F1")]
        [DataRow(0.25, "F3π", "0.500π", DisplayName = "PiRadians F3")]
        [DataRow(0.25, "F1 gon", "100.0 gon", DisplayName = "Gradians F1")]
        [DataRow(0.25, "°", "90°", DisplayName = "Degrees unit only")]
        [DataRow(0.25, "gon", "100 gon", DisplayName = "Gradians unit only")]
        public void TestToString(double turns, string formatString, string expected)
        {
            var angle = new Angle(turns);
            string angleStr = angle.ToString(formatString, CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, angleStr);
        }

        [TestMethod]
        public void TestEquality()
        {
            Angle a = new Angle(0.45);
            Angle b = new Angle(0.45);
            Angle c = new Angle(0.55);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
            Assert.IsFalse(a != b);

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsTrue(a.Equals((object)b));
            Assert.IsFalse(a.Equals((object)c));
        }

        [TestMethod]
        public void TestCompare()
        {
            Angle a = new Angle(0.45);
            Angle b = new Angle(0.50);
            Angle c = new Angle(0.55);
            Angle b2 = new Angle(0.50);

            Assert.IsTrue(a.CompareTo(c) < 0);
            Assert.IsTrue(c.CompareTo(a) > 0);
            Assert.IsTrue(b.CompareTo(b2) == 0);

            Assert.IsFalse(b < a);
            Assert.IsFalse(b < b2);
            Assert.IsTrue(b < c);

            Assert.IsFalse(b <= a);
            Assert.IsTrue(b <= b2);
            Assert.IsTrue(b <= c);

            Assert.IsTrue(b > a);
            Assert.IsFalse(b > b2);
            Assert.IsFalse(b > c);

            Assert.IsTrue(b >= a);
            Assert.IsTrue(b >= b2);
            Assert.IsFalse(b >= c);
        }
    }
}

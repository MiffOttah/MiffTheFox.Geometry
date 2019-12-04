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
        public void TestStaticAngles()
        {
            Assert.AreEqual(0.0, Angle.Zero.Turns);
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
        [DataRow(0, 0)]
        [DataRow(1, 0)]
        [DataRow(1.5, 0.5)]
        [DataRow(-.75, 0.25)]
        [DataRow(-2, 0)]
        [DataRow(-2.5, 0.5)]
        [DataRow(-3.1, 0.9)]
        [DataRow(300, 0)]
        public void TestCanonicalProperty(double turns, double expected)
        {
            var angle = new Angle(turns);
            Assert.AreEqual(expected, angle.Canonical.Turns, DELTA);
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

        [TestMethod]
        public void TestOperators()
        {
            Assert.AreEqual(1.0, (new Angle(0.4) + new Angle(0.6)).Turns, DELTA);
            Assert.AreEqual(-0.2, (new Angle(0.4) - new Angle(0.6)).Turns, DELTA);
            Assert.AreEqual(0.8, (new Angle(0.4) * 2).Turns, DELTA);
            Assert.AreEqual(1.2, (3 * new Angle(0.4)).Turns, DELTA);
            Assert.AreEqual(0.25, (new Angle(0.5) / 2).Turns, DELTA);
        }

        [TestMethod]
        public void TestTrigCreation()
        {
            const double sqrt2over3 = 0.47140452079;

            Assert.AreEqual(0.490882678, Angle.ArcSin(sqrt2over3).Radians, DELTA);
            Assert.AreEqual(1.07991365, Angle.ArcCos(sqrt2over3).Radians, DELTA);
            Assert.AreEqual(0.982793723, Angle.ArcTan(1.5).Radians, DELTA);
            Assert.AreEqual(0.982793723, Angle.ArcTan2(3, 2).Radians, DELTA);
        }

        [TestMethod]
        public void TestTrigMethods()
        {
            var angle = new Angle(Math.PI / 6, AngleUnit.Radians);

            Assert.AreEqual(0.86602540378, angle.Cos(), DELTA);
            Assert.AreEqual(0.5, angle.Sin(), DELTA);
            Assert.AreEqual(0.57735026919, angle.Tan(), DELTA);
            Assert.AreEqual(1.14023832108, angle.Cosh(), DELTA);
            Assert.AreEqual(0.54785347388, angle.Sinh(), DELTA);
            Assert.AreEqual(0.48047277815, angle.Tanh(), DELTA);
        }

        [TestMethod]
        [DataRow(0, 1, 0)]
        [DataRow(0.2, 0.309016994, 0.951056516)]
        [DataRow(0.25, 0, 1)]
        [DataRow(0.5, -1, 0)]
        [DataRow(0.6666, -0.500362716, -0.865815888)]
        [DataRow(0.75, 0, -1)]
        [DataRow(0.9, 0.809016994, -0.587785252)]
        [DataRow(1, 1, 0)]
        public void TestToPoint(double turns, double expectedX, double expectedY)
        {
            var angle = new Angle(turns);
            (double x, double y) = angle.ToPoint();
            Assert.AreEqual(expectedX, x, DELTA);
            Assert.AreEqual(expectedY, y, DELTA);
        }

        [TestMethod]
        [DataRow(0, 2.5, 2.5, 0)]
        [DataRow(0.2, 2.5, 0.772542486, 2.377641291)]
        [DataRow(0.25, 2.5, 0, 2.5)]
        [DataRow(0.5, 2.5, -2.5, 0)]
        [DataRow(0.6666, 2.5, -1.25090679, -2.164539721)]
        [DataRow(0.75, 2.5, 0, -2.5)]
        [DataRow(0.9, 2.5, 2.022542486, -1.469463131)]
        [DataRow(1, 2.5, 2.5, 0)]
        public void TestToPointWithRadius(double turns, double radius, double expectedX, double expectedY)
        {
            var angle = new Angle(turns);
            (double x, double y) = angle.ToPoint(radius);
            Assert.AreEqual(expectedX, x, DELTA);
            Assert.AreEqual(expectedY, y, DELTA);
        }
    }
}

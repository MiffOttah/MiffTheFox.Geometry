using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class AngleTests
    {
        const double DELTA = 1e-8; // good ol' floating-points
        
        [TestMethod]
        [DataRow(0.5, AngleUnit.Turns, 0.5)]
        // TODO: test other units
        public void TestCreationFromUnits(double value, AngleUnit unit, double expected)
        {
            var angle = new Angle(value, unit);
            Assert.AreEqual(expected, angle.Turns);
        }

        [TestMethod]
        [DataRow(0.5, Math.PI)]
        [DataRow(0.75, Math.PI * 1.5)]
        // TODO: more test cases
        public void TestRadiansProperty(double turns, double expectedRadians)
        {
            var angle = new Angle(turns);
            Assert.AreEqual(expectedRadians, angle.Radians);
        }

        [TestMethod]
        [DataRow(0.0, AngleUnit.Degrees, 0.0)]
        [DataRow(0.25, AngleUnit.Degrees, 90.0)]
        [DataRow(0.5, AngleUnit.Degrees, 180.0)]
        [DataRow(1.0, AngleUnit.Degrees, 360.0)]
        // TODO: test cases for each other unit
        public void TestToUnit(double turns, AngleUnit toUnit, double expected)
        {
            var angle = new Angle(turns);
            Assert.AreEqual(expected, angle.ToUnit(toUnit));
        }
    }
}

﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}

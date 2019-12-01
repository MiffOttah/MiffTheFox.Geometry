using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class PortionTests
    {
        const double DELTA = 0.000001; // good ol' floating-points

        [TestMethod]
        [DataRow(0)]
        [DataRow(0.25)]
        [DataRow(0.5)]
        [DataRow(0.75)]
        [DataRow(1)]
        public void TestCreation(double value)
        {
            var por = new Portion(value);
            Assert.AreEqual(value, por.Value);
        }

        [TestMethod]
        [DataRow(-0.4)]
        [DataRow(-10)]
        [DataRow(1.05)]
        [DataRow(13)]
        public void TestFailedCreationFromValueOutOfRange(double value)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Portion(value));
        }

        [TestMethod]
        [DataRow(double.NaN)]
        [DataRow(double.NegativeInfinity)]
        [DataRow(double.PositiveInfinity)]
        public void TestFailedCreationFromInvalidDouble(double value)
        {
            Assert.ThrowsException<ArgumentException>(() => new Portion(value));
        }

        [TestMethod]
        public void TestStaticPortions()
        {
            Assert.AreEqual(0.0, Portion.Empty.Value);
            Assert.AreEqual(1.0, Portion.Full.Value);
        }

        [TestMethod]
        [DataRow(0, 1)]
        [DataRow(0.2, 0.8)]
        [DataRow(0.5, 0.5)]
        [DataRow(0.7, 0.3)]
        [DataRow(1, 0)]
        public void TestComplement(double value, double complement)
        {
            var por = new Portion(value);
            Assert.AreEqual(complement, por.Complement.Value, DELTA);
        }
    }
}

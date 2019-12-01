using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class PortionTests
    {
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
    }
}

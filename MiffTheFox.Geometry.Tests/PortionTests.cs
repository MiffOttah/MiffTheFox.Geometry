using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class PortionTests
    {
        const double DELTA = 1e-8; // good ol' floating-points

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

        [TestMethod]
        public void TestEquals()
        {
            var a = new Portion(0.2);
            var b = new Portion(0.2);
            var c = new Portion(0.8);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
            Assert.IsFalse(b.Equals(c));

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);

            Assert.IsTrue(a != c);
            Assert.IsFalse(a != b);

            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [TestMethod]
        public void TestCompare()
        {
            var a = new Portion(0.2);
            var b = new Portion(0.2);
            var c = new Portion(0.8);

            Assert.AreEqual(1, c.CompareTo(a));
            Assert.AreEqual(-1, a.CompareTo(c));
            Assert.AreEqual(0, a.CompareTo(b));

            Assert.IsTrue(a < c);
            Assert.IsFalse(c < a);
            Assert.IsFalse(b < a);

            Assert.IsTrue(c > a);
            Assert.IsFalse(a > c);
            Assert.IsFalse(b > a);

            Assert.IsTrue(a <= c);
            Assert.IsFalse(c <= a);
            Assert.IsTrue(b <= a);

            Assert.IsTrue(c >= a);
            Assert.IsFalse(a >= c);
            Assert.IsTrue(b >= a);
        }

        [TestMethod]
        [DataRow(0.1, 10, 1)]
        [DataRow(0.5, -5, -2.5)]
        [DataRow(0, -5, 0)]
        [DataRow(1, -5, -5)]
        [DataRow(0.8, 0, 0)]
        public void TestFloatingPointLerp(double portion, double max, double expected)
        {
            var por = new Portion(portion);

            Assert.AreEqual(expected, por.Lerp(max), DELTA);
            Assert.AreEqual(expected + 1.0, por.Lerp(1.0, max), DELTA);
            Assert.AreEqual(expected, por * max, DELTA);
            Assert.AreEqual(expected, max * por, DELTA);
        }

        [TestMethod]
        [DataRow(0.1, 10, 1)]
        [DataRow(0.5, -5, -3)]
        [DataRow(0, -5, 0)]
        [DataRow(1, -5, -5)]
        [DataRow(0.8, 0, 0)]
        public void TestIntegerLerp(double portion, int max, int expected)
        {
            var por = new Portion(portion);

            Assert.AreEqual(expected, por.Lerp(max), DELTA);
            Assert.AreEqual(expected + 1, por.Lerp(1, max), DELTA);
            Assert.AreEqual(expected, por * max, DELTA);
            Assert.AreEqual(expected, max * por, DELTA);
        }
    }
}

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
            Assert.AreEqual(expected + 1.5, por.Lerp(1.5, max + 1.5), DELTA);
            Assert.AreEqual(expected, por * max, DELTA);
            Assert.AreEqual(expected, max * por, DELTA);
        }

        [TestMethod]
        [DataRow(0.1, 10, 1)]
        [DataRow(0.5, -5, -3)]
        [DataRow(0, -5, 0)]
        [DataRow(1, -5, -5)]
        [DataRow(0.8, 0, 0)]
        [DataRow(0.32, 10, 3)]
        [DataRow(0.36, 10, 4)]
        public void TestIntegerLerp(double portion, int max, int expected)
        {
            var por = new Portion(portion);

            Assert.AreEqual(expected, por.Lerp(max));
            Assert.AreEqual(expected + 2, por.Lerp(2, max + 2));
            Assert.AreEqual(expected, por * max);
            Assert.AreEqual(expected, max * por);
        }

        [TestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(1, 2, 0.5)]
        [DataRow(3, 4, 0.75)]
        [DataRow(0, 2, 0)]
        public void TestPortion(int num, int denom, double expected)
        {
            var por = Portion.Fraction(num, denom);
            Assert.AreEqual(expected, por.Value, DELTA);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 0.5)]
        [DataRow(4, 0.25)]
        [DataRow(5, 0.2)]
        public void TestPortionWithoutNumerator(int denom, double expected)
        {
            var por = Portion.Fraction(denom);
            Assert.AreEqual(expected, por.Value, DELTA);
        }

        [TestMethod]
        [DataRow(-1, 2, DisplayName = "Numerator negative")]
        [DataRow(2, 0, DisplayName = "Denominator zero")]
        [DataRow(2, -1, DisplayName = "Denominator negative")]
        [DataRow(2, 1, DisplayName = "Numerator greater than denominator")]
        public void TestPortionOutOfRange(int num, int denom)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Portion.Fraction(num, denom));
        }

        [TestMethod]
        [DataRow(0, DisplayName = "Denominator zero")]
        [DataRow(-1, DisplayName = "Denominator negative")]
        public void TestPortionWithoutNumeratorOutOfRange(int denom)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Portion.Fraction(denom));
        }

        [TestMethod]
        public void TestCast()
        {
            var por = new Portion(0.4);
            const byte byteValue = 102;

            Assert.AreEqual(0.4, (double)por, DELTA);
            Assert.AreEqual(0.4f, (float)por, 1e-8f);
            Assert.AreEqual(0.4M, (decimal)por);
            Assert.AreEqual(byteValue, (byte)por);

            Assert.AreEqual(por.Value, ((Portion)0.4).Value, DELTA);
            Assert.AreEqual(por.Value, ((Portion)0.4f).Value, DELTA);
            Assert.AreEqual(por.Value, ((Portion)0.4M).Value, DELTA);
            Assert.AreEqual(por.Value, ((Portion)byteValue).Value, DELTA);
        }

        [TestMethod]
        [DataRow(1.1, DisplayName = "Double >1")]
        [DataRow(-2, DisplayName = "Double <0")]
        [DataRow(double.NaN, DisplayName = "Double NaN")]
        [DataRow(double.PositiveInfinity, DisplayName = "Double Infinity")]
        [DataRow(1.1f, DisplayName = "Float >1")]
        [DataRow(-2f, DisplayName = "Float <0")]
        [DataRow(float.NaN, DisplayName = "Float NaN")]
        [DataRow(float.PositiveInfinity, DisplayName = "Float Infinity")]
        // cast from decimal isn't tested becuase it can't be in a DataRow
        // however, we can assume that it will follow the double/single cases
        // due to the same _CastIn method being called after casting the decimal
        // to a double.
        public void TestFailedCastIn(object source)
        {
            Assert.ThrowsException<InvalidCastException>(() => (Portion)source);
        }
    }
}

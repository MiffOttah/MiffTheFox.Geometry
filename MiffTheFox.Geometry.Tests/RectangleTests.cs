using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiffTheFox.Geometry.Integer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class RectangleTests
    {
        const double DELTA = 1e-8; // good ol' floating-points

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(10, 10, 20, 20)]
        [DataRow(-5, -6, 20, 21)]
        [DataRow(8.2, 8.3, 4.1, 4.2)]
        [DataRow(12, 12, -6, -6)]
        public void TestCreation(double x, double y, double w, double h)
        {
            var r1 = new Rectangle(x, y, w, h);
            Assert.AreEqual(r1.X, x);
            Assert.AreEqual(r1.Y, y);
            Assert.AreEqual(r1.Width, w);
            Assert.AreEqual(r1.Height, h);

            var r2 = new Rectangle(new Point(x, y), new Size(w, h));
            Assert.AreEqual(r2.X, x);
            Assert.AreEqual(r2.Y, y);
            Assert.AreEqual(r2.Width, w);
            Assert.AreEqual(r2.Height, h);
        }

        [TestMethod]
        [DataRow(double.PositiveInfinity, 20, 30, 40)]
        [DataRow(double.NegativeInfinity, 20, 30, 40)]
        [DataRow(double.NaN, 20, 30, 40)]
        [DataRow(20, double.PositiveInfinity, 30, 40)]
        [DataRow(20, double.NegativeInfinity, 30, 40)]
        [DataRow(20, double.NaN, 30, 40)]
        [DataRow(20, 30, double.PositiveInfinity, 40)]
        [DataRow(20, 30, double.NegativeInfinity, 40)]
        [DataRow(20, 30, double.NaN, 40)]
        [DataRow(20, 30, 40, double.PositiveInfinity)]
        [DataRow(20, 30, 40, double.NegativeInfinity)]
        [DataRow(20, 30, 40, double.NaN)]
        public void TestFailedCreation(double x, double y, double w, double h)
        {
            Assert.ThrowsException<ArgumentException>(() => new Rectangle(x, y, w, h));
        }

        [TestMethod]
        public void TestEmpty()
        {
            Assert.AreEqual(0, Rectangle.Empty.X);
            Assert.AreEqual(0, Rectangle.Empty.Y);
            Assert.AreEqual(0, Rectangle.Empty.Height);
            Assert.AreEqual(0, Rectangle.Empty.Width);
        }

        [TestMethod]
        [DataRow(10, 12, 30, 34, 10, 12, 20, 22)]
        [DataRow(-50, -45, -21, -22, -50, -45, 29, 23)]
        [DataRow(-10, 0, 22, 13, -10, 0, 32, 13)]
        [DataRow(4, 5, -11, -12, 4, 5, -15, -17)]
        [DataRow(-5, -8, 0, 0, -5, -8, 5, 8)]
        [DataRow(0, 0, 4, 8, 0, 0, 4, 8)]
        public void TestFromLTRB(double l, double t, double r, double b, double x, double y, double w, double h)
        {
            var rect = Rectangle.FromLTRB(l, t, r, b);
            var expected = new Rectangle(x, y, w, h);
            Assert.AreEqual(expected, rect);
        }

        [TestMethod]
        [DataRow(-1, -1, 1, 1, -1, -1, 2, 2)]
        [DataRow(2, 0, 0, 3, 0, 0, 2, 3)]
        [DataRow(4, 4, 4, 4, 4, 4, 0, 0)]
        [DataRow(1, 1, -1, -1, -1, -1, 2, 2)]
        [DataRow(0, 0, 0, 0, 0, 0, 0, 0)]
        public void TestBetween(double x1, double y1, double x2, double y2, double xExpected, double yExpected, double wExpected, double hExpected)
        {
            var expected = new Rectangle(xExpected, yExpected, wExpected, hExpected);
            var actual = Rectangle.Between(new Point(x1, y1), new Point(x2, y2));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(10, 10, 20, 20)]
        [DataRow(-5, -6, 20, 21)]
        [DataRow(8.2, 8.3, 4.1, 4.2)]
        [DataRow(12, 12, -6, -6)]
        public void TestDeconstruction(double x, double y, double w, double h)
        {
            var r = new Rectangle(x, y, w, h);
            (double x2, double y2, double w2, double h2) = r;
            Assert.AreEqual(x, x2);
            Assert.AreEqual(y, y2);
            Assert.AreEqual(w, w2);
            Assert.AreEqual(h, h2);
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(10, 10, 20, 20)]
        [DataRow(-5, -6, 20, 21)]
        [DataRow(8.2, 8.3, 4.1, 4.2)]
        [DataRow(12, 12, -6, -6)]
        public void TestSizeAndPositionProperties(double x, double y, double w, double h)
        {
            var r = new Rectangle(x, y, w, h);
            Assert.AreEqual(new Point(x, y), r.Position);
            Assert.AreEqual(new Size(w, h), r.Size);
        }

        [TestMethod]
        public void TestEquality()
        {
            var r1 = new Rectangle(10, 11, 12, 13);
            var r2 = new Rectangle(13, 12, 11, 10);
            var r3 = new Rectangle(10, 11, 12, 13);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsTrue(r1.Equals(r3));

            Assert.IsTrue(r1.Equals((object)r3));
            Assert.IsFalse(r1.Equals((object)r2));
            Assert.IsFalse(r1.Equals(null));
            Assert.IsFalse(r1.Equals(new DateTime()));

            Assert.IsTrue(r1 == r3);
            Assert.IsTrue(r1 != r2);
            Assert.IsFalse(r1 == r2);
            Assert.IsFalse(r1 != r3);
        }

        [TestMethod]
        public void TestWith()
        {
            var r = new Rectangle(10, 11, 12, 13);

            Assert.AreEqual(new Rectangle(10, 11, 12, 13), r.With());
            Assert.AreEqual(new Rectangle(20, 11, 12, 13), r.With(x: 20));
            Assert.AreEqual(new Rectangle(10, 20, 12, 13), r.With(y: 20));
            Assert.AreEqual(new Rectangle(10, 11, 20, 13), r.With(width: 20));
            Assert.AreEqual(new Rectangle(10, 11, 12, 20), r.With(height: 20));
        }

        [TestMethod]
        [DataRow(2, -2, 2, -2, MidpointRounding.AwayFromZero, DisplayName = "Whole numbers")]
        [DataRow(5.8, -1.1, 6, -1, MidpointRounding.AwayFromZero, DisplayName = "Rounding AFZ without midpoint")]
        [DataRow(5.5, 4.5, 6, 5, MidpointRounding.AwayFromZero, DisplayName = "Round positive midpoint")]
        [DataRow(-5.5, -4.5, -6, -5, MidpointRounding.AwayFromZero, DisplayName = "Round negative midpoint")]
        [DataRow(5.5, 4.5, 6, 4, MidpointRounding.ToEven, DisplayName = "Even rounding")]
        public void TestCast(double x, double y, int xR, int yR, MidpointRounding rounding)
        {
            var r1Expected = new IntRectangle(xR, yR, 10, 10);
            var r1Actual = new Rectangle(x, y, 10, 10);
            Assert.AreEqual(r1Expected, r1Actual.ToIntRectangle(rounding));

            var r2Expected = new IntRectangle(10, 10, xR, yR);
            var r2Actual = new Rectangle(10, 10, x, y);
            Assert.AreEqual(r2Expected, r2Actual.ToIntRectangle(rounding));
        }

        [TestMethod]
        [DataRow(5, 10, 2, 8, 7, 18)]
        [DataRow(-10, -20, 25, 15, 15, -5)]
        [DataRow(40, -3, 10, 10, 50, 7)]
        [DataRow(10, 10, -2, -2, 8, 8)]
        public void TestRightBottom(double x, double y, double w, double h, double expectedRight, double expectedBottom)
        {
            var r = new Rectangle(x, y, w, h);
            Assert.AreEqual(r.Right, expectedRight);
            Assert.AreEqual(r.Bottom, expectedBottom);
        }

        [TestMethod]
        [DataRow(5, 10, 2, 8, 5, 10, 2, 8)]
        [DataRow(4, 11, -2, -8, 6, 19, 2, 8)]
        [DataRow(-8, -15, 3, 3, -8, -15, 3, 3)]
        [DataRow(-10, -50, -4, -1, -6, -49, 4, 1)]
        [DataRow(0, 0, -4, 3, 4, 0, 4, 3)]
        [DataRow(0, 0, 4, -3, 0, 3, 4, 3)]
        [DataRow(5, 5, 0, 0, 5, 5, 0, 0)]
        [DataRow(0, 0, 0, 0, 0, 0, 0, 0)]
        public void TestCanonical(double x, double y, double w, double h, double xExpected, double yExpected, double wExpected, double hExpected)
        {
            var r = new Rectangle(x, y, w, h);
            Assert.AreEqual(new Rectangle(xExpected, yExpected, wExpected, hExpected), r.Canonical);
        }

        [TestMethod]
        [DataRow(4, 5, 10, 13, 9, 11.5)]
        [DataRow(0, 0, 30, 25, 15, 12.5)]
        [DataRow(-10, -12, 2, 3, -9, -10.5)]
        [DataRow(0, 0, -5, -6, -2.5, -3)]
        [DataRow(4, -3, 10, 5, 9, -0.5)]
        public void TestMidpoint(double x, double y, double w, double h, double midX, double midY)
        {
            var r = new Rectangle(x, y, w, h);
            Assert.AreEqual(new Point(midX, midY), r.Midpoint);
        }

        [TestMethod]
        [DataRow(0, 0, 10, 20, -5, -10, 10, 20)]
        [DataRow(5, 10, 15, 20, -2.5, 0, 15, 20)]
        [DataRow(-2, -2, 5, 5, -4.5, -4.5, 5, 5)]
        [DataRow(11, 12, -1, -1, 11.5, 12.5, -1, -1)]
        [DataRow(-10, 5, 10, 12, -15, -1, 10, 12)]
        public void TestAround(double x, double y, double w, double h, double xExpected, double yExpected, double wExpected, double hExpected)
        {
            var expected = new Rectangle(xExpected, yExpected, wExpected, hExpected);

            Assert.AreEqual(expected, Rectangle.Around(x, y, w, h));
            Assert.AreEqual(expected, Rectangle.Around(new Point(x, y), new Size(w, h)));
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0, "0,0;0x0")]
        [DataRow(4, 14, 2, 12, "4,14;2x12")]
        [DataRow(-2, -3, 10, 11, "-2,-3;10x11")]
        [DataRow(-2, 3, -10, -11, "-2,3;-10x-11")]
        [DataRow(0.5, 0.5, 4.5, 4.5, "0.5,0.5;4.5x4.5")]
        [DataRow(95.3, -102, 40.2, 44.9, "95.3,-102;40.2x44.9")]
        public void TestSerialize(double x, double y, double w, double h, string serialized)
        {
            var r = new Rectangle(x, y, w, h);
            Assert.AreEqual(serialized, r.Serialize());
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0, "0,0;0x0")]
        [DataRow(4, 14, 2, 12, "4,14;2x12")]
        [DataRow(-2, -3, 10, 11, "-2,-3;10x11")]
        [DataRow(-2, 3, -10, -11, "-2,3;-10x-11")]
        [DataRow(0.5, 0.5, 4.5, 4.5, "0.5,0.5;4.5x4.5")]
        [DataRow(95.3, -102, 40.2, 44.9, "95.3,-102;40.2x44.9")]
        public void TestDeSerialize(double x, double y, double w, double h, string serialized)
        {
            var r = new Rectangle(x, y, w, h);
            Assert.AreEqual(r, Rectangle.DeSerialize(serialized));
        }

        [TestMethod]
        [DataRow(2, 3, "4,5;6x7", "2,2;10x13")]
        [DataRow(5, 6, "-2,-2;2x2", "-7,-8;12x14")]
        [DataRow(-2, -3, "0,0;10x10", "2,3;6x4")]
        public void TestInflate2(double w, double h, string baseRect, string expected)
        {
            var r = Rectangle.DeSerialize(baseRect);
            Assert.AreEqual(Rectangle.DeSerialize(expected), r.Inflate(w, h));
        }

        [TestMethod]
        public void TestInflate4()
        {
            // TODO: Write test
            throw new NotImplementedException();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiffTheFox.Geometry.Integer;
using MiffTheFox.Geometry.FloatingPoint;
using System;

namespace MiffTheFox.Geometry.Tests.FloatingPoint
{
    [TestClass]
    public class FPointTests
    {
        [DataRow(0, 0, DisplayName = "Empty point")]
        [DataRow(1.5, 3, DisplayName = "1.5,3")]
        [DataRow(3, -5.8, DisplayName = "3,-5.8")]
        [DataRow(-4, -1, DisplayName = "-4,-1")]
        [DataRow(-5, 0, DisplayName = "-5,0")]
        [TestMethod]
        public void TestCreation(double x, double y)
        {
            var point = new FPoint(x, y);
            Assert.AreEqual(x, point.X);
            Assert.AreEqual(y, point.Y);
        }

        [DataRow(double.NaN, 0, DisplayName = "X = NaN")]
        [DataRow(double.PositiveInfinity, 0, DisplayName = "X = +Inf")]
        [DataRow(double.NegativeInfinity, 0, DisplayName = "X = +Inf")]
        [DataRow(0, double.NaN, DisplayName = "Y = NaN")]
        [DataRow(0, double.PositiveInfinity, DisplayName = "Y = +Inf")]
        [DataRow(0, double.NegativeInfinity, DisplayName = "Y = +Inf")]
        [TestMethod]
        public void TestInvalidCreation(double x, double y)
        {
            Assert.ThrowsException<ArgumentException>(() => new FPoint(x, y));
        }


        [TestMethod]
        public void TestEmpty()
        {
            var point = FPoint.Empty;
            Assert.AreEqual(0.0, point.X);
            Assert.AreEqual(0.0, point.Y);
        }

        [TestMethod]
        public void TestWith()
        {
            var point = new FPoint(0.3, 0.2);
            var point2 = point.With(y: 2.0);
            var point3 = point.With(x: -1.1);
            var point4 = point.With(-5, -5);

            Assert.AreEqual(0.3, point2.X);
            Assert.AreEqual(2.0, point2.Y);

            Assert.AreEqual(-1.1, point3.X);
            Assert.AreEqual(0.2, point3.Y);

            Assert.AreEqual(-5, point4.X);
            Assert.AreEqual(-5, point4.Y);
        }

        [DataRow(0, 0, DisplayName = "Empty point")]
        [DataRow(-2.5, 3, DisplayName = "-2.5,3")]
        [DataRow(4.5, -1.1, DisplayName = "4.5,-1.1")]
        [TestMethod]
        public void TestDeconstruct(double x, double y)
        {
            var point = new FPoint(x, y);
            double x2, y2;

            (x2, y2) = point;

            Assert.AreEqual(x, x2);
            Assert.AreEqual(y, y2);
        }


        [TestMethod]
        public void TestCompare()
        {
            var a = new FPoint(1, 2);
            var b = new FPoint(50, -10);
            var c = new FPoint(11, 15);
            var d = new FPoint(1, 2);

            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a.Equals(d));

            Assert.IsTrue(c.Equals((object)c));
            Assert.IsFalse(c.Equals((object)null));

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());
            Assert.AreEqual(a.GetHashCode(), d.GetHashCode());

            Assert.IsTrue(a == d);
            Assert.IsFalse(a == b);

            Assert.IsTrue(a != c);
            Assert.IsFalse(a != d);

            Assert.AreEqual(a, d);
            Assert.AreNotEqual(a, b);
        }

        [TestMethod]
        [DataRow(1, 2.5, 11, 12, 12, 14.5)]
        [DataRow(-1, 2, 11, -12.8, 10, -10.8)]
        [DataRow(0, 0, 1, 0, 1, 0)]
        public void TestAdd(double x1, double y1, double x2, double y2, double xR, double yR)
        {
            var p1 = new FPoint(x1, y1);
            var p2 = new FPoint(x2, y2);
            Assert.AreEqual(new FPoint(xR, yR), p1 + p2);
        }

        [TestMethod]
        [DataRow(2, -2, 2, -2, MidpointRounding.AwayFromZero, DisplayName = "Whole numbers")]
        [DataRow(5.8, -1.1, 6, -1, MidpointRounding.AwayFromZero, DisplayName = "Rounding AFZ without midpoint")]
        [DataRow(5.5, 4.5, 6, 5, MidpointRounding.AwayFromZero, DisplayName = "Round positive midpoint")]
        [DataRow(-5.5, -4.5, -6, -5, MidpointRounding.AwayFromZero, DisplayName = "Round negative midpoint")]
        [DataRow(5.5, 4.5, 6, 4, MidpointRounding.ToEven, DisplayName = "Even rounding")]
        public void TestCast(double x, double y, int xR, int yR, MidpointRounding rounding)
        {
            var p = new FPoint(x, y);
            var pR = new Point(xR, yR);
            Assert.AreEqual(pR, p.ToIntPoint(rounding));
        }
    }
}

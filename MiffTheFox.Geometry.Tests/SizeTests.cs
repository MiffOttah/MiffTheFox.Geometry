using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiffTheFox.Geometry.Integer;
using System;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class SizeTests
    {
        [DataRow(0, 0, DisplayName = "Empty size")]
        [DataRow(10.5, 3.2)]
        [DataRow(3, -5.8)]
        [DataRow(40, 12.55)]
        [DataRow(-50, 0)]
        [TestMethod]
        public void TestCreation(double w, double h)
        {
            var point = new Size(w, h);
            Assert.AreEqual(w, point.Width);
            Assert.AreEqual(h, point.Height);
        }

        [DataRow(double.NaN, 0, DisplayName = "Width = NaN")]
        [DataRow(double.PositiveInfinity, 0, DisplayName = "Width = +Inf")]
        [DataRow(double.NegativeInfinity, 0, DisplayName = "Width = +Inf")]
        [DataRow(0, double.NaN, DisplayName = "Height = NaN")]
        [DataRow(0, double.PositiveInfinity, DisplayName = "Height = +Inf")]
        [DataRow(0, double.NegativeInfinity, DisplayName = "Height = +Inf")]
        [TestMethod]
        public void TestInvalidCreation(double x, double y)
        {
            Assert.ThrowsException<ArgumentException>(() => new Size(x, y));
        }


        [TestMethod]
        public void TestEmpty()
        {
            var point = Point.Empty;
            Assert.AreEqual(0.0, point.X);
            Assert.AreEqual(0.0, point.Y);
        }

        [TestMethod]
        public void TestWith()
        {
            var size = new Size(0.3, 0.2);
            var size2 = size.With(h: 2.0);
            var size3 = size.With(w: -1.1);
            var size4 = size.With(-5, -5);

            Assert.AreEqual(0.3, size2.Width);
            Assert.AreEqual(2.0, size2.Height);

            Assert.AreEqual(-1.1, size3.Width);
            Assert.AreEqual(0.2, size3.Height);

            Assert.AreEqual(-5, size4.Width);
            Assert.AreEqual(-5, size4.Height);
        }

        [DataRow(0, 0, DisplayName = "Empty point")]
        [DataRow(25, 30.2)]
        [DataRow(40.8, -12)]
        [DataRow(33, 33)]
        [TestMethod]
        public void TestDeconstruct(double w, double h)
        {
            var point = new Size(w, h);
            double w2, h2;

            (w2, h2) = point;

            Assert.AreEqual(w, w2);
            Assert.AreEqual(h, h2);
        }


        [TestMethod]
        public void TestCompare()
        {
            var a = new Size(1, 2);
            var b = new Size(50, -10);
            var c = new Size(11, 15);
            var d = new Size(1, 2);

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
            var p1 = new Size(x1, y1);
            var p2 = new Size(x2, y2);
            Assert.AreEqual(new Size(xR, yR), p1 + p2);
        }

        [TestMethod]
        [DataRow(2, -2, 2, -2, MidpointRounding.AwayFromZero, DisplayName = "Whole numbers")]
        [DataRow(5.8, -1.1, 6, -1, MidpointRounding.AwayFromZero, DisplayName = "Rounding AFZ without midpoint")]
        [DataRow(5.5, 4.5, 6, 5, MidpointRounding.AwayFromZero, DisplayName = "Round positive midpoint")]
        [DataRow(-5.5, -4.5, -6, -5, MidpointRounding.AwayFromZero, DisplayName = "Round negative midpoint")]
        [DataRow(5.5, 4.5, 6, 4, MidpointRounding.ToEven, DisplayName = "Even rounding")]
        public void TestCast(double x, double y, int xR, int yR, MidpointRounding rounding)
        {
            var p = new Size(x, y);
            var pR = new IntSize(xR, yR);
            Assert.AreEqual(pR, p.ToIntSize(rounding));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiffTheFox.Geometry.Integer;

namespace MiffTheFox.Geometry.Tests.Integer
{
    [TestClass]
    public class PointTests
    {
        [DataRow(0, 0, DisplayName = "Empty point")]
        [DataRow(1, 3, DisplayName = "1,3")]
        [DataRow(3, -5, DisplayName = "3,-5")]
        [DataRow(-4, -1, DisplayName = "-4,-1")]
        [DataRow(-5, 0, DisplayName = "-5,0")]
        [TestMethod]
        public void TestCreation(int x, int y)
        {
            var point = new Point(x, y);
            Assert.AreEqual(x, point.X);
            Assert.AreEqual(y, point.Y);
        }

        [TestMethod]
        public void TestWith()
        {
            var point = new Point(1, 2);
            var point2 = point.With(y: 3);
            var point3 = point.With(x: -1);
            var point4 = point.With(-5, -5);

            Assert.AreEqual(1, point2.X);
            Assert.AreEqual(3, point2.Y);

            Assert.AreEqual(-1, point3.X);
            Assert.AreEqual(2, point3.Y);

            Assert.AreEqual(-5, point4.X);
            Assert.AreEqual(-5, point4.Y);
        }

        [DataRow(0, 0, DisplayName = "Empty point")]
        [DataRow(-2, 3, DisplayName = "-2,3")]
        [DataRow(4, -1, DisplayName = "4,-1")]
        [TestMethod]
        public void TestDeconstruct(int x, int y)
        {
            var point = new Point(x, y);
            int x2, y2;

            (x2, y2) = point;

            Assert.AreEqual(x, x2);
            Assert.AreEqual(y, y2);
        }


        [TestMethod]
        public void TestCompare()
        {
            var a = new Point(1, 2);
            var b = new Point(50, -10);
            var c = new Point(11, 15);
            var d = new Point(1, 2);

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
        }
    }
}

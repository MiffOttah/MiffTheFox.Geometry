using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiffTheFox.Geometry.Integer;

namespace MiffTheFox.Geometry.Tests.Integer
{
    [TestClass]
    public class IntSizeTests
    {
        [DataRow(0, 0, DisplayName = "Empty size")]
        [DataRow(1, 3, DisplayName = "1x3")]
        [DataRow(3, -5, DisplayName = "3x-5")]
        [DataRow(-4, -1, DisplayName = "-4x-1")]
        [DataRow(-5, 0, DisplayName = "-5x0")]
        [TestMethod]
        public void TestCreation(int w, int h)
        {
            var point = new IntSize(w, h);
            Assert.AreEqual(w, point.Width);
            Assert.AreEqual(h, point.Height);
        }

        [TestMethod]
        public void TestEmpty()
        {
            var point = IntSize.Empty;
            Assert.AreEqual(0, point.Width);
            Assert.AreEqual(0, point.Height);
        }

        [TestMethod]
        public void TestWith()
        {
            var point = new IntSize(1, 2);
            var point2 = point.With(h: 3);
            var point3 = point.With(w: -1);
            var point4 = point.With(-5, -5);

            Assert.AreEqual(1, point2.Width);
            Assert.AreEqual(3, point2.Height);

            Assert.AreEqual(-1, point3.Width);
            Assert.AreEqual(2, point3.Height);

            Assert.AreEqual(-5, point4.Width);
            Assert.AreEqual(-5, point4.Height);
        }

        [DataRow(0, 0, DisplayName = "Empty size")]
        [DataRow(20, 30)]
        [DataRow(45, 15)]
        [TestMethod]
        public void TestDeconstruct(int w, int h)
        {
            var point = new IntSize(w, h);
            int w2, h2;
            (w2, h2) = point;
            Assert.AreEqual(w, w2);
            Assert.AreEqual(h, h2);
        }


        [TestMethod]
        public void TestCompare()
        {
            var a = new IntSize(1, 2);
            var b = new IntSize(50, -10);
            var c = new IntSize(11, 15);
            var d = new IntSize(1, 2);

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
        [DataRow(1, 2, 11, 12, 12, 14)]
        [DataRow(-1, 2, 11, -12, 10, -10)]
        [DataRow(0, 0, 1, 0, 1, 0)]
        public void TestAdd(int x1, int y1, int x2, int y2, int xR, int yR)
        {
            var p1 = new IntSize(x1, y1);
            var p2 = new IntSize(x2, y2);
            Assert.AreEqual(new IntSize(xR, yR), p1 + p2);
        }
    }
}

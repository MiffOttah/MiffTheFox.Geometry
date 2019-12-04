using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MiffTheFox.Geometry.Integer;


namespace MiffTheFox.Geometry.Tests.Integer
{
    [TestClass]
    public class IntRectangleTests
    {
        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(10, 10, 20, 20)]
        [DataRow(-5, -6, 20, 21)]
        [DataRow(82, 83, 41, 42)]
        [DataRow(12, 12, -6, -6)]
        public void TestCreation(int x, int y, int w, int h)
        {
            var r1 = new IntRectangle(x, y, w, h);
            Assert.AreEqual(r1.X, x);
            Assert.AreEqual(r1.Y, y);
            Assert.AreEqual(r1.Width, w);
            Assert.AreEqual(r1.Height, h);

            var r2 = new IntRectangle(new IntPoint(x, y), new IntSize(w, h));
            Assert.AreEqual(r2.X, x);
            Assert.AreEqual(r2.Y, y);
            Assert.AreEqual(r2.Width, w);
            Assert.AreEqual(r2.Height, h);
        }

        [TestMethod]
        public void TestEmpty()
        {
            Assert.AreEqual(0, IntRectangle.Empty.X);
            Assert.AreEqual(0, IntRectangle.Empty.Y);
            Assert.AreEqual(0, IntRectangle.Empty.Height);
            Assert.AreEqual(0, IntRectangle.Empty.Width);
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(10, 10, 20, 20)]
        [DataRow(-5, -6, 20, 21)]
        [DataRow(82, 83, 41, 42)]
        [DataRow(12, 12, -6, -6)]
        public void TestDeconstruction(int x, int y, int w, int h)
        {
            var r = new IntRectangle(x, y, w, h);
            (int x2, int y2, int w2, int h2) = r;
            Assert.AreEqual(x, x2);
            Assert.AreEqual(y, y2);
            Assert.AreEqual(w, w2);
            Assert.AreEqual(h, h2);
        }

        [TestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(10, 10, 20, 20)]
        [DataRow(-5, -6, 20, 21)]
        [DataRow(82, 83, 41, 42)]
        [DataRow(12, 12, -6, -6)]
        public void TestSizeAndPositionProperties(int x, int y, int w, int h)
        {
            var r = new IntRectangle(x, y, w, h);
            Assert.AreEqual(new IntPoint(x, y), r.Position);
            Assert.AreEqual(new IntSize(w, h), r.Size);
        }

        [TestMethod]
        public void TestEquality()
        {
            var r1 = new IntRectangle(10, 11, 12, 13);
            var r2 = new IntRectangle(13, 12, 11, 10);
            var r3 = new IntRectangle(10, 11, 12, 13);

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
            var r = new IntRectangle(10, 11, 12, 13);

            Assert.AreEqual(new IntRectangle(10, 11, 12, 13), r.With());
            Assert.AreEqual(new IntRectangle(20, 11, 12, 13), r.With(x: 20));
            Assert.AreEqual(new IntRectangle(10, 20, 12, 13), r.With(y: 20));
            Assert.AreEqual(new IntRectangle(10, 11, 20, 13), r.With(width: 20));
            Assert.AreEqual(new IntRectangle(10, 11, 12, 20), r.With(height: 20));
        }
    }
}

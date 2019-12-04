﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry.Tests
{
    [TestClass]
    public class RectangleTests
    {
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
    }
}

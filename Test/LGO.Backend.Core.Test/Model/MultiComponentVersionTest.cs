using System;
using LGO.Backend.Core.Model;
using NUnit.Framework;

namespace LGO.Backend.Core.Test.Model
{
    [TestFixture]
    public class MultiComponentVersionTest
    {
        [Test]
        public void TestParse()
        {
            var parsedVersion = MultiComponentVersion.Parse("13.37.0");
            Assert.IsNotNull(parsedVersion);
            
            Assert.Throws<ArgumentException>(() => MultiComponentVersion.Parse("Not a valid version"));
        }

        [Test]
        public void TestTryParse()
        {
            Assert.IsTrue(MultiComponentVersion.TryParse("13.37.0", out _));
            Assert.IsFalse(MultiComponentVersion.TryParse("Not a valid version", out _));
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual("13.37.0", new MultiComponentVersion(13, 37, 0).ToString());
        }

        [Test]
        public void TestEquals()
        {
            var versionA = new MultiComponentVersion(13, 37, 0);
            var versionB = new MultiComponentVersion(13, 37);
            var versionC = new MultiComponentVersion(37, 13, 1);
            
            Assert.IsTrue(versionA.Equals(versionA));
            Assert.IsTrue(versionB.Equals(versionB));
            Assert.IsTrue(versionC.Equals(versionC));
            
            Assert.IsTrue(versionA.Equals(versionB));
            Assert.IsFalse(versionA.Equals(versionC));
            
            Assert.IsTrue(versionB.Equals(versionA));
            Assert.IsFalse(versionB.Equals(versionC));
            
            Assert.IsFalse(versionC.Equals(versionA));
            Assert.IsFalse(versionC.Equals(versionB));
        }

        [Test]
        public void TestCompare()
        {
            var versionA = new MultiComponentVersion(0);
            var versionB = new MultiComponentVersion(1);
            var versionC = new MultiComponentVersion(2);
            
            Assert.Less(versionA, versionB);
            Assert.Less(versionB, versionC);
        }
    }
}
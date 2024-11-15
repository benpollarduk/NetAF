using NetAF.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class Point2D_Tests
    {
        [TestMethod]
        public void Given2PointsThatAreTheSameInXAndY_WhenEquals_ThenReturnTrue()
        {
            var one = new Point2D(0, 0);
            var two = new Point2D(0, 0);

            var result = one.Equals(two);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given2PointsThatAreDifferentInX_WhenEquals_ThenReturnTrue()
        {
            var one = new Point2D(0, 0);
            var two = new Point2D(1, 0);

            var result = one.Equals(two);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given2PointsThatAreDifferentInY_WhenEquals_ThenReturnTrue()
        {
            var one = new Point2D(0, 0);
            var two = new Point2D(0, 1);

            var result = one.Equals(two);

            Assert.IsFalse(result);
        }
    }
}

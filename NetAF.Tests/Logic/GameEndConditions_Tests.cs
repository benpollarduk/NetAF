using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class GameEndConditions_Tests
    {
        [TestMethod]
        public void GivenNotEnded_WhenInvoked_ThenReturnsNotEnded()
        {
            var result = GameEndConditions.NotEnded(null);

            Assert.AreEqual(EndCheckResult.NotEnded, result);
        }

        [TestMethod]
        public void GivenNoEnd_WhenCompletionCondition_ThenReturnsNotEnded()
        {
            var result = GameEndConditions.NoEnd.CompletionCondition(null);

            Assert.IsFalse(result.HasEnded);
        }

        [TestMethod]
        public void GivenNoEnd_WhenGameOverCondition_ThenReturnsNotEnded()
        {
            var result = GameEndConditions.NoEnd.GameOverCondition(null);

            Assert.IsFalse(result.HasEnded);
        }

        [TestMethod]
        public void GivenCustomConditions_WhenConstructed_ThenCompletionConditionIsSet()
        {
            EndCheck completion = g => new(true, "DONE", string.Empty);
            EndCheck gameOver = g => new(true, "OVER", string.Empty);

            GameEndConditions result = new(completion, gameOver);

            Assert.AreEqual(completion, result.CompletionCondition);
        }

        [TestMethod]
        public void GivenCustomConditions_WhenConstructed_ThenGameOverConditionIsSet()
        {
            EndCheck completion = g => new(true, "DONE", string.Empty);
            EndCheck gameOver = g => new(true, "OVER", string.Empty);

            GameEndConditions result = new(completion, gameOver);

            Assert.AreEqual(gameOver, result.GameOverCondition);
        }
    }
}

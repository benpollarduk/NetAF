﻿using NetAF.Assets.Interaction;
using NetAF.Commands.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Commands.Global
{
    [TestClass]
    public class New_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new New();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenOK()
        {
            var game = NetAF.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var command = new New();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }
    }
}

﻿using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class GlobalCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.North)), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.South)), 0, 1, 0);
            overworld.AddRegion(region);
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenEmptyString_WhenInterpret_ThenReturnFalse()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNew_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.Interpret(GlobalCommandInterpreter.New, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenAbout_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.Interpret(GlobalCommandInterpreter.About, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExit_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.Interpret(GlobalCommandInterpreter.Exit, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHelp_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.Interpret(GlobalCommandInterpreter.Help, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenMap_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => new PlayableCharacter(Identifier.Empty, Description.Empty), _ => EndCheckResult.NotEnded, _ => EndCheckResult.NotEnded).Invoke();

            var result = interpreter.Interpret(GlobalCommandInterpreter.Map, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}

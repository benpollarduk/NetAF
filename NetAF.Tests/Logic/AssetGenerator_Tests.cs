using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class AssetGenerator_Tests
    {
        private class TemplatePlayer : IAssetTemplate<PlayableCharacter>
        {
            public PlayableCharacter Instantiate()
            {
                return new PlayableCharacter(string.Empty, string.Empty);
            }
        }

        private class TemplateOverworld : IAssetTemplate<Overworld>
        {
            public Overworld Instantiate()
            {
                return new Overworld(string.Empty, string.Empty);
            }
        }

        [TestMethod]
        public void GivenRetained_WhenGetOverworld_ThenSameInstanceOfOverworldReturned()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var player = new PlayableCharacter(string.Empty, string.Empty);
            var generator = AssetGenerator.Retained(overworld, player);

            var result = generator.GetOverworld();

            Assert.AreEqual(overworld, result);
        }

        [TestMethod]
        public void GivenRetained_WhenGetPlayer_ThenSameInstanceOfPlayerReturned()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var player = new PlayableCharacter(string.Empty, string.Empty);
            var generator = AssetGenerator.Retained(overworld, player);

            var result = generator.GetPlayer();

            Assert.AreEqual(player, result);
        }

        [TestMethod]
        public void GivenNewWithOverworldTemplate_WhenGetOverworld_ThenDifferentInstanceOfOverworldReturned()
        {
            var generator = AssetGenerator.New(new TemplateOverworld(), new TemplatePlayer());

            var result1 = generator.GetOverworld();
            var result2 = generator.GetOverworld();

            Assert.AreNotEqual(result1, result2);
        }

        [TestMethod]
        public void GivenNew_WhenGetPlayer_ThenDifferentInstanceOfPlayerReturned()
        {
            var generator = AssetGenerator.New(new TemplateOverworld(), new TemplatePlayer());

            var result1 = generator.GetPlayer();
            var result2 = generator.GetPlayer();

            Assert.AreNotEqual(result1, result2);
        }

        [TestMethod]
        public void GivenNewWithOverwoldMaker_WhenGetOverworld_ThenDifferentInstanceOfOverworldReturned()
        {
            var maker = new OverworldMaker(string.Empty, string.Empty);
            var generator = AssetGenerator.New(maker, new TemplatePlayer());

            var result1 = generator.GetOverworld();
            var result2 = generator.GetOverworld();

            Assert.AreNotEqual(result1, result2);
        }

        [TestMethod]
        public void GivenCustom_WhenGetOverworld_ThenSameInstanceOfOverworldReturned()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var player = new PlayableCharacter(string.Empty, string.Empty);
            var generator = AssetGenerator.Custom(() => overworld, () => player);

            var result = generator.GetOverworld();

            Assert.AreEqual(overworld, result);
        }

        [TestMethod]
        public void GivenCustom_WhenGetPlayer_ThenSameInstanceOfPlayerReturned()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var player = new PlayableCharacter(string.Empty, string.Empty);
            var generator = AssetGenerator.Custom(() => overworld, () => player);

            var result = generator.GetPlayer();

            Assert.AreEqual(player, result);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Rendering;
using NetAF.Utilities;

namespace NetAF.Tests.Logic.Modes
{
    [TestClass]
    public class SceneMode_Tests
    {
        [TestMethod]
        public void GivenNew_WhenRender_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                FrameProperties.CommandListType = CommandListType.All;
                FrameProperties.KeyType = KeyType.Dynamic;
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
                game.Overworld.CurrentRegion.Enter();
                var mode = new SceneMode(new SceneCommandInterpreter());

                mode.Render(game);
            });
        }

        [TestMethod]
        public void GivenNewAndDisplayingNoCommandsOrKey_WhenRender_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                FrameProperties.CommandListType = CommandListType.None;
                FrameProperties.KeyType = KeyType.None;
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
                game.Overworld.CurrentRegion.Enter();
                var mode = new SceneMode(new SceneCommandInterpreter());

                mode.Render(game);
            });
        }

        [TestMethod]
        public void GivenNewAndDisplayingMinimalCommandsAndKey_WhenRender_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                FrameProperties.CommandListType = CommandListType.Minimal;
                FrameProperties.KeyType = KeyType.Dynamic;
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
                game.Overworld.CurrentRegion.Enter();
                var mode = new SceneMode(new SceneCommandInterpreter());

                mode.Render(game);
            });
        }
    }
}

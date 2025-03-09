using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console;

namespace NetAF.Tests.Targets.Console
{
    [TestClass]
    public class ConsoleExecutionController_Tests
    {
        [TestMethod]
        public void GivenGame_WhenCancel_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var controller = new ConsoleExecutionController();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = new Room("Room A", string.Empty, [new Exit(Direction.North)]);
                regionMaker[0, 1, 0] = new Room("Room B", string.Empty, [new Exit(Direction.South)]);
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                PlayableCharacter player1 = new("A", string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), player1), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
                game.Update();

                _ = controller.BeginAsync(game);
                controller.CancelAsync().Wait();
            });
        }
    }
}
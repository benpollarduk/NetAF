using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands.RegionMap;
using NetAF.Utilities;
using NetAF.Logic.Modes;
using NetAF.Rendering;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class RegionMapCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenInterpreter_WhenGetSupportedCommands_ThenReturnArrayWithSomeItems()
        {
            var interpreter = new RegionMapCommandInterpreter();

            var result = interpreter.SupportedCommands;

            Assert.IsNotEmpty(result);
        }

        [TestMethod]
        public void GivenCanPanAnyDirectionAndZoomedIsMaximal_WhenGetContextualCommands_ThenReturn8Commands()
        {
            FrameProperties.MapDetail = RegionMapDetail.Maximal;
            var interpreter = new RegionMapCommandInterpreter();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room bottom = new(string.Empty, string.Empty, [new Exit(Direction.Up)]);
            Room top = new(string.Empty, string.Empty, [new Exit(Direction.Down)]);
            Room south = new(string.Empty, string.Empty, [new Exit(Direction.North)]);
            Room north = new(string.Empty, string.Empty, [new Exit(Direction.South)]);
            Room west = new(string.Empty, string.Empty, [new Exit(Direction.East)]);
            Room east = new(string.Empty, string.Empty, [new Exit(Direction.West)]);
            Room center = new(string.Empty, string.Empty, [new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.Up), new Exit(Direction.Down)]);
            regionMaker[1, 1, 1] = center;
            regionMaker[0, 1, 1] = west;
            regionMaker[2, 1, 1] = east;
            regionMaker[1, 2, 1] = north;
            regionMaker[1, 0, 1] = south;
            regionMaker[1, 1, 2] = top;
            regionMaker[1, 1, 0] = bottom;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.IsVisibleWithoutDiscovery = true;
            game.Overworld.CurrentRegion.Enter();
            game.ChangeMode(new RegionMapMode(new(1, 1, 1), FrameProperties.MapDetail, new RegionMapCommandInterpreter()));

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.HasCount(8, result);
        }

        [TestMethod]
        public void GivenCanPanAnyDirectionAndZoomIsNormal_WhenGetContextualCommands_ThenReturn9Commands()
        {
            FrameProperties.MapDetail = RegionMapDetail.Normal;
            var interpreter = new RegionMapCommandInterpreter();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room bottom = new(string.Empty, string.Empty, [new Exit(Direction.Up)]);
            Room top = new(string.Empty, string.Empty, [new Exit(Direction.Down)]);
            Room south = new(string.Empty, string.Empty, [new Exit(Direction.North)]);
            Room north = new(string.Empty, string.Empty, [new Exit(Direction.South)]);
            Room west = new(string.Empty, string.Empty, [new Exit(Direction.East)]);
            Room east = new(string.Empty, string.Empty, [new Exit(Direction.West)]);
            Room center = new(string.Empty, string.Empty, [new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.Up), new Exit(Direction.Down)]);
            regionMaker[1, 1, 1] = center;
            regionMaker[0, 1, 1] = west;
            regionMaker[2, 1, 1] = east;
            regionMaker[1, 2, 1] = north;
            regionMaker[1, 0, 1] = south;
            regionMaker[1, 1, 2] = top;
            regionMaker[1, 1, 0] = bottom;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.IsVisibleWithoutDiscovery = true;
            game.Overworld.CurrentRegion.Enter();
            game.ChangeMode(new RegionMapMode(new(1, 1, 1), FrameProperties.MapDetail, new RegionMapCommandInterpreter()));

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.HasCount(9, result);
        }

        [TestMethod]
        public void GivenCanPanAnyDirectionAndZoomIsMinimal_WhenGetContextualCommands_ThenReturn8Commands()
        {
            FrameProperties.MapDetail = RegionMapDetail.Minimal;
            var interpreter = new RegionMapCommandInterpreter();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room bottom = new(string.Empty, string.Empty, [new Exit(Direction.Up)]);
            Room top = new(string.Empty, string.Empty, [new Exit(Direction.Down)]);
            Room south = new(string.Empty, string.Empty, [new Exit(Direction.North)]);
            Room north = new(string.Empty, string.Empty, [new Exit(Direction.South)]);
            Room west = new(string.Empty, string.Empty, [new Exit(Direction.East)]);
            Room east = new(string.Empty, string.Empty, [new Exit(Direction.West)]);
            Room center = new(string.Empty, string.Empty, [new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.Up), new Exit(Direction.Down)]);
            regionMaker[1, 1, 1] = center;
            regionMaker[0, 1, 1] = west;
            regionMaker[2, 1, 1] = east;
            regionMaker[1, 2, 1] = north;
            regionMaker[1, 0, 1] = south;
            regionMaker[1, 1, 2] = top;
            regionMaker[1, 1, 0] = bottom;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.IsVisibleWithoutDiscovery = true;
            game.Overworld.CurrentRegion.Enter();
            game.ChangeMode(new RegionMapMode(new(1, 1, 1), FrameProperties.MapDetail, new RegionMapCommandInterpreter()));

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.HasCount(8, result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenInterpret_ThenReturnFalse()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenPanNorth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Pan.NorthCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenPanSouth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Pan.SouthCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenPanEast_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Pan.EastCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenPanWest_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Pan.WestCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenPanUp_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Pan.UpCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenPanDown_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Pan.DownCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenReset_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new RegionMapCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(PanReset.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}

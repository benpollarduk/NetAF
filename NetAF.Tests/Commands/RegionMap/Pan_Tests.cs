using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.RegionMap;
using NetAF.Logic.Modes;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Commands;
using NetAF.Rendering;

namespace NetAF.Tests.Commands.RegionMap
{
    [TestClass]
    public class Pan_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new Pan(Direction.West);

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvokeAndNotInRegionMapMode_ThenError()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Pan(Direction.West);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGameAndCantPan_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Pan(Direction.West);
            game.ChangeMode(new RegionMapMode(RegionMapMode.Player, RegionMapDetail.Basic));

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenValidGameAndCanPanNorth_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.South)]);
            Room room2 = new(string.Empty, string.Empty, [new Exit(Direction.North)]);
            regionMaker[0, 1, 0] = room;
            regionMaker[0, 0, 0] = room2;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.North);
            game.Overworld.CurrentRegion.Move(Direction.South);
            var command = new Pan(Direction.North);
            var mode = new RegionMapMode(RegionMapMode.Player, RegionMapDetail.Basic) { FocusPosition = new Point3D(0, 0, 0) };
            game.ChangeMode(mode);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenValidGameAndCanPanSouth_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.South)]);
            Room room2 = new(string.Empty, string.Empty, [new Exit(Direction.North)]);
            regionMaker[0, 1, 0] = room;
            regionMaker[0, 0, 0] = room2;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.South);
            game.Overworld.CurrentRegion.Move(Direction.North);
            var command = new Pan(Direction.South);
            var mode = new RegionMapMode(RegionMapMode.Player, RegionMapDetail.Basic) { FocusPosition = new Point3D(0, 1, 0) };
            game.ChangeMode(mode);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenValidGameAndCanPanEast_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.East)]);
            Room room2 = new(string.Empty, string.Empty, [new Exit(Direction.West)]);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 0] = room2;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.East);
            game.Overworld.CurrentRegion.Move(Direction.West);
            var command = new Pan(Direction.East);
            var mode = new RegionMapMode(RegionMapMode.Player, RegionMapDetail.Basic) { FocusPosition = new Point3D(0, 0, 0) };
            game.ChangeMode(mode);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenValidGameAndCanPanWest_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.West)]);
            Room room2 = new(string.Empty, string.Empty, [new Exit(Direction.East)]);
            regionMaker[1, 0, 0] = room;
            regionMaker[0, 0, 0] = room2;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.West);
            game.Overworld.CurrentRegion.Move(Direction.East);
            var command = new Pan(Direction.West);
            var mode = new RegionMapMode(RegionMapMode.Player, RegionMapDetail.Basic) { FocusPosition = new Point3D(1, 0, 0) };
            game.ChangeMode(mode);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenValidGameAndCanPanUp_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.Down)]);
            Room room2 = new(string.Empty, string.Empty, [new Exit(Direction.Up)]);
            regionMaker[0, 0, 1] = room;
            regionMaker[0, 0, 0] = room2;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.Up);
            game.Overworld.CurrentRegion.Move(Direction.Down);
            var command = new Pan(Direction.Up);
            var mode = new RegionMapMode(RegionMapMode.Player, RegionMapDetail.Basic) { FocusPosition = new Point3D(0, 0, 0) };
            game.ChangeMode(mode);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenValidGameAndCanPanDown_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.Down)]);
            Room room2 = new(string.Empty, string.Empty, [new Exit(Direction.Up)]);
            regionMaker[0, 0, 1] = room;
            regionMaker[0, 0, 0] = room2;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.Down);
            game.Overworld.CurrentRegion.Move(Direction.Up);
            var command = new Pan(Direction.Down);
            var mode = new RegionMapMode(RegionMapMode.Player, RegionMapDetail.Basic) { FocusPosition = new Point3D(0, 0, 1) };
            game.ChangeMode(mode);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenFocusPositionOf000_WhenGetPanPosition_ThenReturnMinus100()
        {
            var result = Pan.GetPanPosition(new Point3D(0, 0, 0), Direction.West);

            Assert.AreEqual(-1, result.X);
            Assert.AreEqual(0, result.Y);
            Assert.AreEqual(0, result.Z);
        }

        [TestMethod]
        public void GivenGame_WhenGetPrompts_ThenEmptyArray()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Pan(Direction.East);

            var result = command.GetPrompts(game);

            Assert.AreEqual([], result);
        }
    }
}

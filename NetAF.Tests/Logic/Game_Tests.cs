using System;
using System.IO;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Conversations;
using NetAF.Logic;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class Game_Tests
    {
        [TestMethod]
        public void GivenEmptyRoom_WhenGetAllPlayerVisibleExaminables_ThenNotNull()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenEmptyRoom_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleItem_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = true };
            var room = new Room(string.Empty, string.Empty,null, item);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleItem_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, item);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleExit_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down) { IsPlayerVisible = true };
            var room = new Room(string.Empty, string.Empty, exit);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleExit_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, exit);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleNPC_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { IsPlayerVisible = true };
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(npc);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleNPC_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(npc);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenDisplayTransition_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

                game.DisplayTransition("Test", "Test");
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenDisplayAbout_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

                game.DisplayAbout();
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenDisplayHelp_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

                game.DisplayHelp();
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenDisplayMap_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

                game.DisplayMap();
            });
        }

        [TestMethod]
        public void GivenPlayer_WhenFindInteractionTarget_ThenReturnPlayer()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter("Player", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.FindInteractionTarget("Player");

            Assert.AreEqual(game.Player, result);
        }

        [TestMethod]
        public void GivenRoom_WhenFindInteractionTarget_ThenReturnRoom()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.FindInteractionTarget("Room");

            Assert.AreEqual(game.Overworld.CurrentRegion.CurrentRoom, result);
        }

        [TestMethod]
        public void GivenPlayerItem_WhenFindInteractionTarget_ThenReturnPlayerItem()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty, new Item("Item", string.Empty))), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.FindInteractionTarget("Item");

            Assert.AreEqual(game.Player.Items[0], result);
        }

        [TestMethod]
        public void GivenRoomItem_WhenFindInteractionTarget_ThenReturnRoomItem()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room("Room", string.Empty);
            room.AddItem(new Item("Item", string.Empty));
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.FindInteractionTarget("Item");

            Assert.AreEqual(game.Overworld.CurrentRegion.CurrentRoom.Items[0], result);
        }

        [TestMethod]
        public void GivenUnknown_WhenFindInteractionTarget_ThenReturnNull()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = game.FindInteractionTarget("ABC");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenSimpleGameWithNoConsoleAccess_WhenExecute_ThenIOExceptionThrown()
        {
            Assert.ThrowsException<IOException>(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default);

                Game.Execute(game);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndCompletionConditionReached_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(_ => new EndCheckResult(true, string.Empty, string.Empty), GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
                game.Adapter = new TestConsoleAdapter();

                game.Execute();
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndGameOverConditionReached_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(GameEndConditions.NotEnded, _ => new EndCheckResult(true, string.Empty, string.Empty)), GameConfiguration.Default).Invoke();
                game.Adapter = new TestConsoleAdapter();

                game.Execute();
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndConverser_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
                game.Adapter = new TestConsoleAdapter();
                var npc = new NonPlayableCharacter("", "") { Conversation = new Conversation(new Paragraph("Test", g => g.End())) };
                game.StartConversation(npc);

                game.Execute();
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccess_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty);
                var room = new Room("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
                game.Adapter = new TestConsoleAdapter();

                game.Execute();
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangePlayer_ThenPlayerIsNewPlayer()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var player2 = new PlayableCharacter(string.Empty, string.Empty);

            game.ChangePlayer(player2);

            Assert.AreEqual(player2, game.Player);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenSetupProvided_ThenEnsureSetupInvoked()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var result = false;
            void setup(Game _) { result = true; }

            Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default, setup).Invoke();

            Assert.IsTrue(result);
        }
    }
}
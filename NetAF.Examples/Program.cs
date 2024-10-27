using System;
using System.Collections.Generic;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Examples.Assets.Player;
using NetAF.Examples.Assets.Regions.Everglades;
using NetAF.Examples.Assets.Regions.Flat;
using NetAF.Examples.Assets.Regions.Hub;
using NetAF.Examples.Assets.Regions.Zelda;
using NetAF.Examples.Assets.Regions.Zelda.Rooms;
using NetAF.Extensions;
using NetAF.Interpretation;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Examples
{
    internal static class Program
    {
        private static EndCheckResult DetermineIfGameHasCompleted(Game game)
        {
            var atDestination = TailCave.Name.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom);

            if (!atDestination)
                return EndCheckResult.NotEnded;

            return new EndCheckResult(true, "Game Over", "You have reached the end of the game, thanks for playing!");
        }

        private static EndCheckResult DetermineIfGameOver(Game game)
        {
            if (game.Player.IsAlive)
                return EndCheckResult.NotEnded;

            return new EndCheckResult(true, "Game Over", "You are dead!");
        }

        private static void PopulateHub(Region hub, Overworld overworld, Region[] otherRegions)
        {
            var room = hub.CurrentRoom;

            foreach (var otherRegion in otherRegions)
            {
                room.AddItem(new Item($"{otherRegion.Identifier.Name} Sphere", "A glass sphere, about the size of a snooker ball. Inside you can see a swirling mist.", true)
                {
                    Commands =
                    [
                        new CustomCommand(new CommandHelp($"Warp {otherRegion.Identifier.Name}", $"Use the {otherRegion.Identifier.Name} Sphere to warp to the {otherRegion.Identifier.Name}."), true, (g, _) =>
                        {
                            var move = overworld?.Move(otherRegion) ?? false;

                            if (!move)
                                return new Reaction(ReactionResult.Error, $"Could not move to {otherRegion.Identifier.Name}.");

                            g.DisplayTransition(string.Empty, $"You peer inside the sphere and feel faint. When the sensation passes you open you eyes and have been transported to the {otherRegion.Identifier.Name}.");

                            return new Reaction(ReactionResult.Internal, string.Empty);
                        })
                    ]
                });
            }
        }

        private static void Main(string[] args)
        {
            try
            {
                static Overworld overworldCreator()
                {
                    var regions = new List<Region>
                    {
                        new Everglades().Instantiate(),
                        new Flat().Instantiate(),
                        new Zelda().Instantiate()
                    };

                    var overworld = new Overworld("Demo", "A demo of NetAF.");

                    var hub = new Hub().Instantiate();
                    PopulateHub(hub, overworld, [.. regions]);
                    overworld.AddRegion(hub);

                    foreach (var region in regions)
                        overworld.AddRegion(region);

                    overworld.Commands =
                    [
                        // add a hidden custom command to the overworld that allows jumping around a region for debugging purposes
                        new CustomCommand(new CommandHelp("Jump", "Jump to a location in a region."), false, (g, a) =>
                        {
                            var x = 0;
                            var y = 0;
                            var z = 0;

                            if (a?.Length >= 3)
                            {
                                _ = int.TryParse(a[0], out x);
                                _ = int.TryParse(a[1], out y);
                                _ = int.TryParse(a[2], out z);
                            }

                            var result = g.Overworld.CurrentRegion.JumpToRoom(x, y, z);

                            if (!result)
                                return new Reaction(ReactionResult.Error, $"Failed to jump to {x} {y} {z}.");

                            return new Reaction(ReactionResult.OK, $"Jumped to {x} {y} {z}.");
                        })
                    ];

                    return overworld;
                }

                var about = "This is a short demo of NetAF made up from test chunks of games that were build to test different features during development.";
                var creator = Game.Create(new GameInfo("NetAF Demo", about, "NetAF"), about, AssetGenerator.Custom(overworldCreator, new Player().Instantiate), new GameEndConditions(DetermineIfGameHasCompleted, DetermineIfGameOver), GameConfiguration.Default);

                Game.Execute(creator);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught running demo: {e.Message}");
                Console.ReadKey();
            }
        }

        private void d()
        {
            // create the player. this is the character the user plays as
            var player = new PlayableCharacter("Dave", "A young boy on a quest to find the meaning of life.");

            /// create region maker. the region maker simplifies creating in game regions. a region contains a series of rooms
            var regionMaker = new RegionMaker("Mountain", "An imposing volcano just East of town.")
            {
                // add a room to the region at position x 0, y 0, z 0
                [0, 0, 0] = new Room("Cavern", "A dark cavern set in to the base of the mountain.")
            };

            // create overworld maker. the overworld maker simplifies creating in game overworlds. an overworld contains a series or regions
            var overworldMaker = new OverworldMaker("Daves World", "An ancient kingdom.", regionMaker);

            // create the callback for generating new instances of the game
            // - the title of the game
            // - an introduction to the game, displayed at the start
            // - about the game, displayed on the about screen
            // - a callback that provides a new instance of the games overworld
            // - a callback that provides a new instance of the player
            // - a callback that determines if the game is complete, checked every cycle of the game
            // - a callback that determines if it's game over, checked every cycle of the game
            var gameCreator = Game.Create(
                new GameInfo("The Life of Dave", "A very low budget adventure.", "Ben Pollard"),
                "Dave awakes to find himself in a cavern...",
                AssetGenerator.Retained(overworldMaker.Make(), player),
                GameEndConditions.NoEnd,
                GameConfiguration.Default);

            // begin the execution of the game
            Game.Execute(gameCreator);
        }
    }
}

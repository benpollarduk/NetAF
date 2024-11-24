using System;
using System.Collections.Generic;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Persistence;
using NetAF.Examples.Assets.Player;
using NetAF.Examples.Assets.Regions.Everglades;
using NetAF.Examples.Assets.Regions.Flat;
using NetAF.Examples.Assets.Regions.Hub;
using NetAF.Examples.Assets.Regions.Hub.Rooms;
using NetAF.Examples.Assets.Regions.Zelda;
using NetAF.Examples.Assets.Regions.Zelda.Rooms;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Logic.Configuration;

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
            hub.TryFindRoom(Clearing.Name, out var room);

            foreach (var otherRegion in otherRegions)
            {
                CustomCommand[] commands =
                [
                    new CustomCommand(new CommandHelp($"Warp {otherRegion.Identifier.Name}", $"Use the {otherRegion.Identifier.Name} Sphere to warp to the {otherRegion.Identifier.Name}."), true, true, (g, _) =>
                    {
                        var move = overworld?.Move(otherRegion) ?? false;

                        if (!move)
                            return new Reaction(ReactionResult.Error, $"Could not move to {otherRegion.Identifier.Name}.");

                        return new Reaction(ReactionResult.Inform, $"You peer inside the sphere and feel faint. When the sensation passes you open you eyes and have been transported to the {otherRegion.Identifier.Name}.");
                    })
                ];

                room.AddItem(new Item($"{otherRegion.Identifier.Name} Sphere", "A glass sphere, about the size of a snooker ball. Inside you can see a swirling mist.", true, commands));
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

                    CustomCommand[] commands =
                    [
                        // add a hidden custom command to the overworld that allows jumping around a region for debugging purposes
                        new(new("Jump", "Jump to a location in a region."), false, true, (g, a) =>
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

                            var result = g.Overworld.CurrentRegion.JumpToRoom(new Point3D(x, y, z));

                            if (!result)
                                return new(ReactionResult.Error, $"Failed to jump to {x} {y} {z}.");

                            return new(ReactionResult.Inform, $"Jumped to {x} {y} {z}.");
                        }),
                        new Save(),
                        new Load()
                    ];

                    var overworld = new Overworld("Demo", "A demo of NetAF.", commands);

                    var hub = new Hub().Instantiate();
                    PopulateHub(hub, overworld, [.. regions]);
                    overworld.AddRegion(hub);

                    foreach (var region in regions)
                        overworld.AddRegion(region);

                    return overworld;
                }

                var about = "This is a short demo of NetAF made up from test chunks of games that were build to test different features during development.";
                var creator = Game.Create(new("NetAF Demo", about, "NetAF"), about, AssetGenerator.Custom(overworldCreator, new Player().Instantiate), new GameEndConditions(DetermineIfGameHasCompleted, DetermineIfGameOver), ConsoleGameConfiguration.Default);

                Game.Execute(creator);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught running demo: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}

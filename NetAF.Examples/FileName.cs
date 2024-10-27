using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.GettingStarted
{
    internal class Program
    {
        private static EndCheckResult IsGameComplete(Game game)
        {
            if (!game.Player.FindItem("Holy Grail", out _))
                return EndCheckResult.NotEnded;

            return new EndCheckResult(true, "Game Complete", "You have the Holy Grail!");
        }

        private static EndCheckResult IsGameOver(Game game)
        {
            if (game.Player.IsAlive)
                return EndCheckResult.NotEnded;

            return new EndCheckResult(true, "Game Over", "You died!");
        }

        private static PlayableCharacter CreatePlayer()
        {
            return new PlayableCharacter("Dave", "A young boy on a quest to find the meaning of life.");
        }

        private static void Main(string[] args)
        {
            var cavern = new Room("Cavern", "A dark cavern set in to the base of the mountain.", new Exit(Direction.North));

            var tunnel = new Room("Tunnel", "A dark tunnel leading inside the mountain.", new Exit(Direction.South));

            var holyGrail = new Item("Holy Grail", "A dull golden cup, looks pretty old.", true);

            tunnel.AddItem(holyGrail);

            var regionMaker = new RegionMaker("Mountain", "An imposing volcano just East of town.")
            {
                [0, 0, 0] = cavern,
                [0, 1, 0] = tunnel
            };

            var overworldMaker = new OverworldMaker("Daves World", "An ancient kingdom.", regionMaker);

            var gameCreator = Game.Create(
                new GameInfo("The Life of Dave", "A very low budget adventure.", "Ben Pollard"),
                "Dave awakes to find himself in a cavern...",
                AssetGenerator.Custom(overworldMaker.Make, CreatePlayer),
                new GameEndConditions(IsGameComplete, IsGameOver),
                GameConfiguration.Default);

            Game.Execute(gameCreator);
        }
    }
}
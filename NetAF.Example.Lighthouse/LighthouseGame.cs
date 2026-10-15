using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Example.Lighthouse.Assets;
using NetAF.Example.Lighthouse.Assets.Regions;
using NetAF.Example.Lighthouse.Assets.Player;

namespace NetAF.Example.Lighthouse
{
    /// <summary>
    /// Assembles "The Lighthouse Keeper's Last Night" - an original NetAF text adventure.
    /// </summary>
    public static class LighthouseGame
    {
        private static EndCheckResult DetermineIfGameHasCompleted(Game game)
        {
            if (GameState.IsSet(GameState.LampLit))
            {
                return new EndCheckResult(true, "The Light Returns", "As the lamp turns, a ship's bell answers from somewhere out in the dark - safe water found. You slump against the lens, exhausted, as the first grey of dawn creeps over the sound. You kept the light. Thanks for playing!");
            }

            return EndCheckResult.NotEnded;
        }

        private static EndCheckResult DetermineIfGameOver(Game game)
        {
            if (game.Player.IsAlive)
                return EndCheckResult.NotEnded;

            return new EndCheckResult(true, "Lost to the Sea", "You are dead!");
        }

        public static GameCreator Create(GameConfiguration configuration)
        {
            static Overworld overworldCreator()
            {
                var overworld = new Overworld("Gull Rock", "A lonely rock in the storm-wracked sound.");
                overworld.AddRegion(new GullRock().Instantiate());
                return overworld;
            }

            var introduction = "The crossing was hell, and you nearly didn't make it. But the great lamp of Gull Rock is dark, and ships are running blind onto the rocks tonight. You have until dawn to bring the light back. Examine everything, read what you find, and use one thing upon another to solve your way to the top of the tower.";
            var info = new GameInfo("The Lighthouse Keeper's Last Night", introduction, "An original NetAF adventure.");

            return Game.Create(info, introduction, AssetGenerator.Custom(overworldCreator, new Keeper().Instantiate), new GameEndConditions(DetermineIfGameHasCompleted, DetermineIfGameOver), configuration);
        }
    }
}

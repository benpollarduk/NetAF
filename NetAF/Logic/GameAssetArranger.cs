using NetAF.Serialization;

namespace NetAF.Logic
{
    internal static class GameAssetArranger
    {
        public static void Arrange(Game game, GameSerialization serialization)
        {
            GameSerialization currentState = new(game);
            
            /* need to find where all the assets have moved between:
               - player - > npc
               - npc -> player
               - player -> room
               - room -> player
               - room -> room
            */

            // then move the assets to the new locations

        }
    }
}

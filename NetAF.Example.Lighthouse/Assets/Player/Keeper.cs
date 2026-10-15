using NetAF.Assets.Characters;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Player
{
    /// <summary>
    /// Provides the playable character for the lighthouse adventure.
    /// </summary>
    public class Keeper : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        private const string Name = "Relief Keeper";
        private const string Description = "You are the relief keeper, sent across the sound to Gull Rock when the regular man failed to signal. Your oilskins are soaked through and your lantern guttered out on the crossing.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        /// <inheritdoc/>
        public PlayableCharacter Instantiate()
        {
            return new PlayableCharacter(Name, Description);
        }

        #endregion
    }
}

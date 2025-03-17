using NetAF.Assets.Characters;
using NetAF.Conversations.Instructions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.NPCs
{
    public class Beth : IAssetTemplate<NonPlayableCharacter>
    {
        #region Constants

        internal const string Name = "Beth";
        private const string Description = "Beth is very pretty, she has blue eyes and ginger hair. She is sat on the sofa watching the TV.";

        #endregion

        #region Overrides of NonIAssetTemplate<PlayableCharacter>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public NonPlayableCharacter Instantiate()
        {
            return new(Name, Description, conversation: new(new("Hello Ben."), new("How are you?", null, new First())));
        }

        #endregion
    }
}

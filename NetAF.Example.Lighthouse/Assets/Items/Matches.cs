using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// A box of matches. Starts damp and useless, and must be dried on the stove.
    /// </summary>
    public class Matches : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Matches";
        private const string DryDescription = "A box of matches, now warm and bone dry. These will strike first time.";
        private const string DampDescription = "A box of matches, soft and damp from the crossing. Struck now they would only crumble - they must be dried out somewhere warm.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            var description = new ConditionalDescription(DryDescription, DampDescription, () => GameState.IsSet(GameState.MatchesDry));
            return new Item(new Identifier(Name), description, true);
        }

        #endregion
    }
}

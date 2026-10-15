using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The keeper's logbook. Reading it reveals clues about the lighthouse puzzles.
    /// </summary>
    public class Logbook : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Logbook";
        private const string Description = "The station logbook, its pages warped with damp. The final entry reads: \"Lamp out. Clockwork seized - the winding handle is gone, I keep it by my chair. Oil store locked, key under the old mooring rope. Matches ruined in the crossing - dry a fresh box on the stove before you trust them.\"";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}

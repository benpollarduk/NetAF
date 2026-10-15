using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The winding handle used to restore the seized clockwork in the stairwell.
    /// </summary>
    public class WindingHandle : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Winding Handle";
        private const string Description = "A heavy brass crank with a squared socket on one end. It is the sort of thing that fits a clockwork drum.";

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

using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// A coiled rope lying on the jetty. The rusted key is hidden beneath it.
    /// </summary>
    public class CoiledRope : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Coiled Rope";
        private const string Description = "A heavy mooring rope, coiled into a sodden heap on the boards. It looks as though it has not been moved in weeks.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return new Item(Name, Description);
        }

        #endregion
    }
}

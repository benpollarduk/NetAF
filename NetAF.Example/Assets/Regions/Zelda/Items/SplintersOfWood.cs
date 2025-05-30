﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Items
{
    public class SplintersOfWood : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Splinters Of Wood";
        private const string Description = "Some splinters of wood left from your chopping frenzy on the stump.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description);
        }

        #endregion
    }
}

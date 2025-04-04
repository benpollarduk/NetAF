﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class LoungeTV : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "TV";
        private const string Description = "The TV is large, and is playing some program with a Chinese looking man dressing a half naked middle aged woman.";

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

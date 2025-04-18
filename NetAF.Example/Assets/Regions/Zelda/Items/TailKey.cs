﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Items
{
    public class TailKey : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Tail Key";
        private const string Description = "A small key, with a complex handle in the shape of a worm like creature.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description, true);
        }

        #endregion
    }
}

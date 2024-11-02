using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Flat.Items
{
    public class Kettle : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Kettle";
        private const string Description = "The kettle has just boiled, you can tell because it is lightly steaming.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description)
            {
                Interaction = item =>
                {
                    if (item != null && CoffeeMug.Name.EqualsIdentifier(item.Identifier))
                    {
                        return new(InteractionEffect.SelfContained, item, "You put some instant coffee granuals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                    }

                    return new(InteractionEffect.NoEffect, item);
                }
            };
        }

        #endregion
    }
}

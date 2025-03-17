using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class CoffeeMug : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Coffee Mug";
        private const string Description = "A coffee mug. It has an ugly hand painted picture of a man with green hair and enormous sideburns painted on the side of it. Underneath it says 'The Sideburn Monster Rides again'. Strange.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description, true, interaction: item =>
            {
                if (Kettle.Name.EqualsIdentifier(item.Identifier))
                {
                    return new(InteractionResult.NoChange, item, "You put some instant coffee graduals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                }

                return new(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}

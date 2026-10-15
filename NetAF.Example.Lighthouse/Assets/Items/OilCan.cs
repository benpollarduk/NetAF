using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The oil can. Starts empty and can be filled from the whale oil cask.
    /// </summary>
    public class OilCan : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Oil Can";
        private const string FullDescription = "A long-spouted copper oil can, now heavy and sloshing with whale oil.";
        private const string EmptyDescription = "A long-spouted copper oil can. It is bone dry - there is nothing left to burn in it.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            var description = new ConditionalDescription(FullDescription, EmptyDescription, () => GameState.IsSet(GameState.OilCanFilled));
            return new Item(new Identifier(Name), description, true);
        }

        #endregion
    }
}

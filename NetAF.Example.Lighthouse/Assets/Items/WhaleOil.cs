using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The barrel of whale oil in the boathouse, used to fill the oil can.
    /// </summary>
    public class WhaleOil : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Whale Oil";
        private const string Description = "A squat cask of whale oil, thick and amber. Too heavy to carry, but a tap on its side could fill a smaller vessel.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return new Item(Name, Description, false, interaction: item =>
            {
                if (item != null && OilCan.Name.EqualsExaminable(item))
                {
                    if (GameState.IsSet(GameState.OilCanFilled))
                        return new Interaction(InteractionResult.NoChange, item, "The oil can is already full to the brim.");

                    GameState.Set(GameState.OilCanFilled);
                    return new Interaction(InteractionResult.NoChange, item, "You turn the little brass tap and let the thick oil glug into the can until it is full. The tap splutters dry.");
                }

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}


using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The lamp room stove. Used to dry the damp matches.
    /// </summary>
    public class Stove : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Stove";
        private const string Description = "A small cast-iron stove in the corner of the lamp room, its embers still glowing faintly. Warm enough to dry out something small.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return new Item(Name, Description, false, interaction: item =>
            {
                if (item != null && Matches.Name.EqualsExaminable(item))
                {
                    if (GameState.IsSet(GameState.MatchesDry))
                        return new Interaction(InteractionResult.NoChange, item, "The matches are already dry.");

                    GameState.Set(GameState.MatchesDry);
                    return new Interaction(InteractionResult.NoChange, item, "You lay the box on the warm iron and wait. Before long the matches are dry and crisp - they will strike now.");
                }

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}

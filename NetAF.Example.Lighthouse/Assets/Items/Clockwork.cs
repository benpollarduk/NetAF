using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The seized clockwork in the stairwell.
    /// unlocks the hatch up to the lamp room.
    /// </summary>
    public class Clockwork : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Clockwork";
        private const string WoundDescription = "The great brass drum turns steadily once more, its governor spinning. High above, the hatch to the lamp room stands unlatched.";
        private const string SeizedDescription = "A great brass drum and a train of gears - the mechanism that once turned the lamp. It is wound down and dead still, and the hatch above it is latched shut. There is a squared spindle where a handle should fit.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public static Item Create(System.Action onWound)
        {
            var description = new ConditionalDescription(WoundDescription, SeizedDescription, () => GameState.IsSet(GameState.ClockworkWound));

            return new Item(new Identifier(Name), description, false, interaction: item =>
            {
                if (item != null && WindingHandle.Name.EqualsExaminable(item))
                {
                    if (GameState.IsSet(GameState.ClockworkWound))
                        return new Interaction(InteractionResult.NoChange, item, "The clockwork is already wound and turning.");

                    GameState.Set(GameState.ClockworkWound);
                    onWound?.Invoke();
                    return new Interaction(InteractionResult.NoChange, item, "You seat the handle on the spindle and heave. With a shuddering clack the gears take up, the drum begins to turn, and the hatch to the lamp room springs its latch.");
                }

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        /// <inheritdoc/>
        public Item Instantiate()
        {
            return Create(null);
        }

        #endregion
    }
}

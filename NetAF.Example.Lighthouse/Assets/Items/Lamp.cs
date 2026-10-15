using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Items
{
    /// <summary>
    /// The great lamp. The final puzzle - it must be filled with oil and lit with dry
    /// matches to win the game.
    /// </summary>
    public class Lamp : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Lamp";
        private const string LitDescription = "The great lamp blazes, its lens throwing a turning blade of light far out across the black water. Gull Rock is a lighthouse again.";
        private const string DarkDescription = "The great lamp sits cold and dark at the heart of the lens. Its reservoir is dry, and there is nothing here to strike a flame with.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <inheritdoc/>
        public Item Instantiate()
        {
            var description = new ConditionalDescription(LitDescription, DarkDescription, () => GameState.IsSet(GameState.LampLit));

            return new Item(new Identifier(Name), description, false, interaction: item =>
            {
                if (item == null)
                    return new Interaction(InteractionResult.NoChange, item);

                if (GameState.IsSet(GameState.LampLit))
                    return new Interaction(InteractionResult.NoChange, item, "The lamp is already lit and turning.");

                if (OilCan.Name.EqualsExaminable(item))
                {
                    if (!GameState.IsSet(GameState.OilCanFilled))
                        return new Interaction(InteractionResult.NoChange, item, "You tip the oil can over the reservoir, but it is empty. You need to fill it first.");

                    if (GameState.IsSet(GameState.LampReservoirFilled))
                        return new Interaction(InteractionResult.NoChange, item, "The reservoir is already full of oil.");

                    GameState.Set(GameState.LampReservoirFilled);
                    return new Interaction(InteractionResult.ItemExpires, item, "You pour the whale oil into the lamp's reservoir until it brims. Now it only needs a flame.");
                }

                if (Matches.Name.EqualsExaminable(item))
                {
                    if (!GameState.IsSet(GameState.LampReservoirFilled))
                        return new Interaction(InteractionResult.NoChange, item, "You strike a match, but the dry reservoir catches nothing. The lamp needs oil first.");

                    if (!GameState.IsSet(GameState.MatchesDry))
                        return new Interaction(InteractionResult.NoChange, item, "The damp match crumbles against the striker without catching. These matches must be dried out first.");

                    GameState.Set(GameState.LampLit);
                    return new Interaction(InteractionResult.ItemExpires, item, "You strike a match, touch it to the oil-soaked wick, and the lamp roars into life. Light sweeps out across the sound - the ships are safe.");
                }

                return new Interaction(InteractionResult.NoChange, item);
            });
        }

        #endregion
    }
}

using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Example.Assets.Items;
using NetAF.Example.Assets.Regions.Flat.Items;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Player
{
    public class Player : IAssetTemplate<PlayableCharacter>
    {
        #region Constants

        private const string Name = "Ben";
        private const string Description = "You are a 25 year old man, dressed in shorts, a t-shirt and flip-flops.";

        #endregion

        #region Implementation of IAssetTemplate<PlayableCharacter>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public PlayableCharacter Instantiate()
        {
            var player = new PlayableCharacter(Name, Description, [new Knife().Instantiate()], interaction: i =>
            {
                if (i == null)
                    return new(InteractionResult.NoChange, null);

                if (Knife.Name.EqualsExaminable(i))
                    return new(InteractionResult.TargetExpires, i, "You slash wildly at your own throat. Your jugular opens spilling blood everywhere. As you loose consciousness you are filled with a deep sense of regret.");

                if (CoffeeMug.Name.EqualsIdentifier(i.Identifier))
                    return new(InteractionResult.NoChange, i, "If there was some coffee in the mug you could drink it.");

                if (Guitar.Name.EqualsIdentifier(i.Identifier))
                    return new(InteractionResult.NoChange, i, "You bust out some Bad Religion. Cracking, shame the guitar isn't plugged in to an amplified though...");

                return new(InteractionResult.NoChange, i);
            });

            return player;
        }

        #endregion
    }
}

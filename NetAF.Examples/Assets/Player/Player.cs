using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Examples.Assets.Items;
using NetAF.Examples.Assets.Regions.Flat.Items;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Player
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
                    return new(InteractionEffect.NeitherItemOrTargetExpired, null);

                if (Knife.Name.EqualsExaminable(i))
                    return new(InteractionEffect.TargetExpired, i, "You slash wildly at your own throat. You are dead.");

                if (CoffeeMug.Name.EqualsIdentifier(i.Identifier))
                    return new(InteractionEffect.NeitherItemOrTargetExpired, i, "If there was some coffee in the mug you could drink it.");

                if (Guitar.Name.EqualsIdentifier(i.Identifier))
                    return new(InteractionEffect.NeitherItemOrTargetExpired, i, "You bust out some Bad Religion. Cracking, shame the guitar isn't plugged in to an amplified though...");

                return new(InteractionEffect.NeitherItemOrTargetExpired, i);
            });

            return player;
        }

        #endregion
    }
}

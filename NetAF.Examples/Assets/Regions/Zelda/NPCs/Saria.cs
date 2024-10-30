using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using NetAF.Examples.Assets.Regions.Zelda.Items;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Zelda.NPCs
{
    internal class Saria : IAssetTemplate<NonPlayableCharacter>
    {
        #region Constants

        internal const string Name = "Saria";
        private const string Description = "A pretty Kokiri elf, dresse.";

        #endregion

        #region Fields

        private readonly Room room;

        #endregion

        #region Constructors

        internal Saria(Room room)
        {
            this.room = room;
        }

        #endregion

        #region Overrides of NonIAssetTemplate<PlayableCharacter>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public NonPlayableCharacter Instantiate()
        {
            var saria = new NonPlayableCharacter(Name, Description);

            saria.AcquireItem(new TailKey().Instantiate());

            saria.Conversation = new Conversation
            (
                new("Hi Link, how's it going?"),
                new("I lost my red rupee, if you find it will you please bring it to me?"),
                new("Oh Link you are so adorable."),
                new("OK Link your annoying me now, I'm just going to ignore you.", new First())
            );

            saria.Interaction = item =>
            {
                saria.FindItem(TailKey.Name, out var key);

                if (Rupee.Name.EqualsIdentifier(item.Identifier) && key != null)
                {
                    saria.DequireItem(key);
                    room.AddItem(key);
                    item.IsPlayerVisible = false;
                    return new(InteractionEffect.SelfContained, item, $"{saria.Identifier.Name} looks excited! \"Thanks Link, here take the Tail Key!\" Saria put the Tail Key down, awesome!");
                }

                if (Shield.Name.EqualsIdentifier(item.Identifier))
                {
                    return new(InteractionEffect.NoEffect, item, $"{saria.Identifier.Name} looks at your shield, but seems pretty unimpressed.");
                }

                if (Sword.Name.EqualsIdentifier(item.Identifier) && saria.IsAlive)
                {
                    saria.Kill();

                    if (!saria.HasItem(key))
                        return new(InteractionEffect.SelfContained, item, $"You strike {saria.Identifier.Name} in the face with the sword and she falls down dead.");

                    saria.DequireItem(key);
                    room.AddItem(key);

                    return new(InteractionEffect.SelfContained, item, $"You strike {saria.Identifier.Name} in the face with the sword and she falls down dead. When she fell you saw something drop to out of her hand, it looked like a key...");
                }

                return new(InteractionEffect.NoEffect, item);
            };

            return saria;
        }

        #endregion
    }
}

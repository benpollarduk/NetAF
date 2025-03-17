using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using NetAF.Example.Assets.Regions.Zelda.Items;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.NPCs
{
    public class Saria : IAssetTemplate<NonPlayableCharacter>
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
            NonPlayableCharacter saria = null;

            Conversation conversation = new
            (
                new("Hi Link, how's it going?"),
                new("I lost my red rupee, if you find it will you please bring it to me?"),
                new("Oh Link you are so adorable."),
                new("OK Link your annoying me now, I'm just going to ignore you.", new First())
            );

            saria = new NonPlayableCharacter(Name, Description, conversation: conversation, interaction: item =>
            {
                saria.FindItem(TailKey.Name, out var key);

                if (Rupee.Name.EqualsIdentifier(item.Identifier) && key != null)
                {
                    saria.RemoveItem(key);
                    room.AddItem(key);
                    return new(InteractionResult.ItemExpires, item, $"{saria.Identifier.Name} looks excited! \"Thanks Link, here take the Tail Key!\" Saria put the Tail Key down, awesome!");
                }

                if (Shield.Name.EqualsIdentifier(item.Identifier))
                {
                    return new(InteractionResult.NoChange, item, $"{saria.Identifier.Name} looks at your shield, but seems pretty unimpressed.");
                }

                if (Sword.Name.EqualsIdentifier(item.Identifier) && saria.IsAlive)
                {
                    saria.Kill();

                    if (!saria.HasItem(key))
                        return new(InteractionResult.NoChange, item, $"You strike {saria.Identifier.Name} in the face with the sword and she falls down dead.");

                    saria.RemoveItem(key);
                    room.AddItem(key);

                    return new(InteractionResult.NoChange, item, $"You strike {saria.Identifier.Name} in the face with the sword and she falls down dead. When she fell you saw something drop to out of her hand, it looked like a key...");
                }

                return new(InteractionResult.NoChange, item);
            });

            saria.AddItem(new TailKey().Instantiate());

            return saria;
        }

        #endregion
    }
}

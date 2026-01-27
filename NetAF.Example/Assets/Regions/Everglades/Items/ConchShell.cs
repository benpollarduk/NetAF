using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Example.Assets.Items;
using NetAF.Example.Assets.Regions.Everglades.Rooms;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Everglades.Items
{
    public class ConchShell : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Conch Shell";
        private const string Description = "A pretty conch shell, it is about the size of a coconut";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            var conchShell = new Item(Name, Description, true, interaction: item =>
            {
                if (item != null && item.Identifier.Equals(Knife.Name))
                    return new(InteractionResult.TargetExpires, item, "You slash at the conch shell and it shatters into tiny pieces. Without the conch shell you are well and truly in trouble.");

                var currentRoom = GameExecutor.ExecutingGame?.Overworld?.CurrentRegion?.CurrentRoom;

                if (currentRoom != null && currentRoom.Identifier.Equals(InnerCave.Name))
                {
                    GameExecutor.ExecutingGame?.NoteManager.Expire(InnerCave.BlowNoteKey);
                    currentRoom[Direction.North].Unlock();
                    return new(InteractionResult.ItemExpires, item, "You blow into the Conch Shell. The Conch Shell howls, the  bats leave! Conch shell crumbles to pieces.");
                }

                return new(InteractionResult.NoChange, item);
            });

            return conchShell;
        }

        #endregion
    }
}

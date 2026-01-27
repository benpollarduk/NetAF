using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Example.Assets.Items;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Everglades.Rooms
{
    public class InnerCave : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Inner Cave";
        internal const string BlowNoteKey = "BLOW";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            Room room = null;

            var description = new ConditionalDescription("With the bats gone there is daylight to the north. To the west is the cave entrance", "As you enter the inner cave the screeching gets louder, and in the gloom you can make out what looks like a million sets of eyes looking back at you. Bats! You can just make out a few rays of light coming from the north, but the bats are blocking your way.", () => !room[Direction.North].IsLocked);

            ExaminationCallback examination = r =>
            {
                GameExecutor.ExecutingGame?.NoteManager.Add(BlowNoteKey, "Perhaps a noise could scare the bats.");
                return ExaminableObject.DefaultExamination.Invoke(r);
            };

            room = new Room(new(Name), description, [new Exit(Direction.West), new Exit(Direction.North, true)], examination: examination, interaction: item =>
            {
                if (item != null && Knife.Name.EqualsExaminable(item))
                    return new(InteractionResult.NoChange, item, "You slash wildly at the bats, but there are too many. Don't aggravate them!");

                return new(InteractionResult.NoChange, item);
            });

            return room;

        }

        #endregion
    }
}

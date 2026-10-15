using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using NetAF.Utilities;
using NetAF.Example.Lighthouse.Assets.Items;

namespace NetAF.Example.Lighthouse.Assets.Rooms
{
    /// <summary>
    /// The starting room. The player arrives here; the rusted key is hidden beneath the
    /// coiled rope and revealed by examining it.
    /// </summary>
    public class LandingJetty : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Landing Jetty";
        private const string Description = "Spray breaks over the slick timbers of the landing jetty. Your little boat knocks against the piles below. The cottage waits to the north, its one window dark, and the squat shape of the boathouse stands to the east. An iron stair climbs up into the great tower, whose lamp hangs lifeless against the storm.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <inheritdoc/>
        public Room Instantiate()
        {
            Room room = null;

            ExaminationCallback ropeExamination = request =>
            {
                if (room != null && !room.FindItem(RustedKey.Name, out _))
                {
                    room.AddItem(new RustedKey().Instantiate());
                    return new Examination("You heave the sodden coil aside. Beneath it, pressed into a gap between the boards, lies a rusted key.");
                }

                return ExaminableObject.DefaultExamination.Invoke(request);
            };

            var rope = new Item(new Identifier(CoiledRope.Name), new Description("A heavy mooring rope, coiled into a sodden heap on the boards. It looks as though it has not been moved in weeks."), false, examination: ropeExamination);

            room = new Room(Name, Description, [new Exit(Direction.North), new Exit(Direction.East, true), new Exit(Direction.Up)]);
            room.AddItem(rope);

            Item door = null;
            door = new Item(new Identifier("Boathouse Door"), new Description("The boathouse door, its planks silvered by salt. A heavy iron padlock holds it fast."), false, interaction: item =>
            {
                if (item != null && RustedKey.Name.EqualsExaminable(item))
                {
                    if (room.FindExit(Direction.East, true, out var exit) && exit.IsLocked)
                    {
                        exit.Unlock();
                        room.RemoveItem(door);
                        return new Interaction(InteractionResult.ItemExpires, item, "The rusted key grinds in the padlock, then gives. The shackle springs open and the boathouse door swings wide to the east.");
                    }

                    return new Interaction(InteractionResult.NoChange, item, "The padlock is already open.");
                }

                return new Interaction(InteractionResult.NoChange, item);
            });

            room.AddItem(door);

            return room;
        }

        #endregion
    }
}

using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Example.Assets.Regions.Everglades.Visuals;
using NetAF.Logic.Modes;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Everglades.Rooms
{
    public class ForestEntrance : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Forest Entrance";
        private const string Description = "You are standing on the edge of a beautiful forest. There is a parting in the trees to the north.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new(Name, Description, [new Exit(Direction.North)], commands:
            [
                new(new("Look", "Look around the area."), true, true, (g, a) =>
                {
                    var frame = new ForestEntranceVisualFrame(Name, g.Configuration.DisplaySize).Instantiate();
                    g.ChangeMode(new VisualMode(frame));
                    return new(ReactionResult.GameModeChanged, string.Empty);
                })
             ]);

        }

        #endregion
    }
}

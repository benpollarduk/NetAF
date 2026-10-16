using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a scene that an examination occurs in.
    /// </summary>
    /// <param name="examiner">The character who is examining the object.</param>
    /// <param name="room">The room the examinable is being examined from.</param>
    /// <param name="viewPoint">The viewpoint from the room.</param>
    public class ExaminationScene(Character examiner, Room room, ViewPoint viewPoint)
    {
        #region StaticProperties

        /// <summary>
        /// Get a default value for when there is no scene.
        /// </summary>
        public static ExaminationScene NoScene { get; } = new(null, null, null);

        #endregion

        #region Properties

        /// <summary>
        /// Get the examiner.
        /// </summary>
        public Character Examiner { get; private set; } = examiner;

        /// <summary>
        /// Get the room the examinable is being examined from.
        /// </summary>
        public Room Room { get; private set; } = room;

        /// <summary>
        /// Get the viewpoint from the room.
        /// </summary>
        public ViewPoint ViewPoint { get; private set; } = viewPoint;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExaminationScene class.
        /// </summary>
        /// <param name="game">The executing game.</param>
        public ExaminationScene(Game game) : this(game?.Player, game?.Overworld?.CurrentRegion?.CurrentRoom, ViewPoint.Create(game?.Overworld?.CurrentRegion))
        {
        }

        #endregion
    }
}

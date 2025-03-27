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
    public class ExaminationScene(Character examiner, Room room)
    {
        #region StaticProperties

        /// <summary>
        /// Get a default value for when there is no scene.
        /// </summary>
        public static ExaminationScene NoScene { get; } = new(null, null);

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExaminationScene class.
        /// </summary>
        /// <param name="game">The executing game.</param>
        public ExaminationScene(Game game) : this(game?.Player, game?.Overworld?.CurrentRegion?.CurrentRoom)
        {
        }

        #endregion
    }
}

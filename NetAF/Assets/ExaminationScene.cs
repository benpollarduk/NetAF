using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a scene that an examination occurs in.
    /// </summary>
    public class ExaminationScene
    {
        #region StaticProperties

        /// <summary>
        /// Get a default value for when there is no scene.
        /// </summary>
        public static ExaminationScene NoScene { get; } = new ExaminationScene(null, null);

        #endregion

        #region Properties

        /// <summary>
        /// Get the examiner.
        /// </summary>
        public Character Examiner { get; private set; }

        /// <summary>
        /// Get the room the examinable is being examined from.
        /// </summary>
        public Room Room { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ExaminationScene class.
        /// </summary>
        /// <param name="examiner">The character who is examining the object.</param>
        /// <param name="room">The room the examinable is being examined from.</param>
        public ExaminationScene(Character examiner, Room room)
        {
            Examiner = examiner;
            Room = room;
        }

        /// <summary>
        /// Initializes a new instance of the ExaminationScene class.
        /// </summary>
        /// <param name="game">The executing game.</param>
        public ExaminationScene(Game game) : this(game.Player, game.Overworld?.CurrentRegion?.CurrentRoom)
        {
        }

        #endregion
    }
}

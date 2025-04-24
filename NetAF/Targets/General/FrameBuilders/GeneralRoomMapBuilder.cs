using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using System;

namespace NetAF.Targets.General.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    public abstract class GeneralRoomMapBuilder : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing there is an item or a character in the room.
        /// </summary>
        public char ItemOrCharacterInRoom { get; set; } = '!';

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = '-';

        /// <summary>
        /// Get or set the character to use for vertical exit borders.
        /// </summary>
        public char VerticalExitBorder { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal exit borders.
        /// </summary>
        public char HorizontalExitBorder { get; set; } = '-';

        /// <summary>
        /// Get or set the character to use for corners.
        /// </summary>
        public char Corner { get; set; } = '+';

        /// <summary>
        /// Get or set the padding between the key and the map.
        /// </summary>
        public int KeyPadding { get; set; } = 6;

        /// <summary>
        /// Get or set the maximum size.
        /// </summary>
        public Size MaxSize { get; set; } = new Size(80, 40);

        #endregion

        #region Methods

        /// <summary>
        /// Adapt the room map for the target.
        /// </summary>
        /// <param name="roomMapBuilder">The room map builder.</param>
        protected virtual void Adapt(GridStringBuilder roomMapBuilder)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, KeyType key)
        {
            /*
                * *-| N |-*
                * |       |
                * - U   D -
                * W   ?   E
                * -       -
                * |       |
                * *-| S |-*
                */

            // for now, cheat and use the ANSI builder then convert to string

            // create an ANSI grid string builder just for this map
            GridStringBuilder ansiGridStringBuilder = new();
            ansiGridStringBuilder.Resize(MaxSize);

            var ansiRoomBuilder = new ConsoleRoomMapBuilder(ansiGridStringBuilder)
            {
                LockedExit = LockedExit,
                ItemOrCharacterInRoom = ItemOrCharacterInRoom,
                VerticalBoundary = VerticalBoundary,
                HorizontalBoundary = HorizontalBoundary,
                VerticalExitBorder = VerticalExitBorder,
                HorizontalExitBorder = HorizontalExitBorder,
                Corner = Corner,
                KeyPadding = KeyPadding
            };

            ansiRoomBuilder.BuildRoomMap(room, viewPoint, key);
            Adapt(ansiGridStringBuilder);
        }

        #endregion
    }
}

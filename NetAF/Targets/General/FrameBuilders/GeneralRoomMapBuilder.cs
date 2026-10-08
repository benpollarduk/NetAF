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
    /// Provides a general builder for room maps.
    /// </summary>
    public abstract class GeneralRoomMapBuilder : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing a point of interest in the room.
        /// </summary>
        public char PointOfInterest { get; set; } = '!';

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

        private IRoomMapBuilder CreateRoomMapBuilder(GridStringBuilder ansiGridStringBuilder)
        {
            return new ConsoleHighDetailRoomMapBuilder(ansiGridStringBuilder)
            {
                LockedExit = LockedExit,
                PointOfInterest = PointOfInterest,
                VerticalBoundary = VerticalBoundary,
                HorizontalBoundary = HorizontalBoundary,
                VerticalExitBorder = VerticalExitBorder,
                HorizontalExitBorder = HorizontalExitBorder,
                Corner = Corner,
                KeyPadding = KeyPadding
            };
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <inheritdoc/>
        public Size RenderedSize => new(9, 7);

        /// <inheritdoc/>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            /*
                * *-| N |-*
                * |       |
                * - U   D -
                * W   !   E
                * -       -
                * |       |
                * *-| S |-*
                */

            // for now, cheat and use the ANSI builder then convert to string

            // create an ANSI grid string builder just for this map
            GridStringBuilder ansiGridStringBuilder = new();

            var ansiRoomBuilder = CreateRoomMapBuilder(ansiGridStringBuilder);
            var renderedSize = Measure(room, viewPoint, options);
            ansiGridStringBuilder.Resize(renderedSize);

            ansiRoomBuilder.BuildRoomMap(room, viewPoint, options);
            Adapt(ansiGridStringBuilder);
        }

        /// <inheritdoc/>
        public Size Measure(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            // determine the required size
            var ansiRoomBuilder = CreateRoomMapBuilder(null);
            return ansiRoomBuilder.Measure(room, viewPoint, options);
        }

        #endregion
    }
}

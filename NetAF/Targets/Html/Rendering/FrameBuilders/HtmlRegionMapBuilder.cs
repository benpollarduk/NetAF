using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder for region maps.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlRegionMapBuilder(HtmlBuilder builder) : IRegionMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing an unlocked exit.
        /// </summary>
        public char UnLockedExit { get; set; } = ' ';

        /// <summary>
        /// Get or set the character used for representing an empty space.
        /// </summary>
        public char EmptySpace { get; set; } = ' ';

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = '-';

        /// <summary>
        /// Get or set the character to use for lower levels.
        /// </summary>
        public char LowerLevel { get; set; } = '.';

        /// <summary>
        /// Get or set the character to use for indicating the player.
        /// </summary>
        public char Player { get; set; } = 'O';

        /// <summary>
        /// Get or set the character to use for indicating the focus.
        /// </summary>
        public char Focus { get; set; } = '+';

        /// <summary>
        /// Get or set the character to use for the current floor.
        /// </summary>
        public char CurrentFloorIndicator { get; set; } = '*';

        /// <summary>
        /// Get or set if lower floors should be shown.
        /// </summary>
        public bool ShowLowerFloors { get; set; } = true;

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="startPosition">The position to start building at.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        public void BuildRegionMap(Region region, Point2D startPosition, Point3D focusPosition, Size maxSize)
        {
            
        }

        #endregion
    }
}

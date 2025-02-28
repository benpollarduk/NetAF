using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;

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
        /// <param name="focusPosition">The position to focus on.</param>
        public void BuildRegionMap(Region region, Point3D focusPosition)
        {
            // for now, cheat and use the ansi builder then convert to HTML

            // create an ansi grid string builder just for this map, and resize to ample room for the map which has an unknown
            // width and an unknown height
            Size size = new(100, 20);
            GridStringBuilder ansiGridStringBuilder = new();
            ansiGridStringBuilder.Resize(size);

            var ansiRegionBuilder = new ConsoleRegionMapBuilder(ansiGridStringBuilder)
            {
                LockedExit = LockedExit,
                UnLockedExit = UnLockedExit,
                EmptySpace = EmptySpace,
                VerticalBoundary = VerticalBoundary,
                HorizontalBoundary = HorizontalBoundary,
                LowerLevel = LowerLevel,
                Player = Player,
                Focus = Focus,
                CurrentFloorIndicator = CurrentFloorIndicator
            };

            ansiRegionBuilder.BuildRegionMap(region, focusPosition, new(0, 0), size);

            var regionAsString = HtmlHelper.ConvertGridStringBuilderToHtmlString(ansiGridStringBuilder);

            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            builder.Raw($"<pre style=\"font-family: 'Courier New', Courier, monospace;\">{regionAsString}</pre>");
        }

        #endregion
    }
}

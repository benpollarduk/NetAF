using NetAF.Assets.Locations;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders.Color
{
    /// <summary>
    /// Provides a builder of color region map frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="regionMapBuilder">A builder for region maps.</param>
    public sealed class ColorRegionMapFrameBuilder(GridStringBuilder gridStringBuilder, IRegionMapBuilder regionMapBuilder) : IRegionMapFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder = gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        private IRegionMapBuilder RegionMapBuilder { get; } = regionMapBuilder;

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        #endregion

        #region Implementation of IRegionMapFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(Region region, int width, int height)
        {
            gridStringBuilder.Resize(new(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(region.Identifier.Name, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, region.Identifier.Name.Length, TitleColor);

            RegionMapBuilder?.BuildRegionMap(gridStringBuilder, region, leftMargin, lastY + 2, availableWidth, height - 4);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}

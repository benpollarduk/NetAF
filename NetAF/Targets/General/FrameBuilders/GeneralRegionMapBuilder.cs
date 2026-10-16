using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Common;
using System;

namespace NetAF.Targets.General.FrameBuilders
{
    /// <summary>
    /// Provides a general builder for region maps.
    /// </summary>
    /// <param name="properties">The render properties.</param>
    public abstract class GeneralRegionMapBuilder(ConsoleMapRenderProperties properties) : IRegionMapBuilder
    {
        #region Methods

        /// <summary>
        /// Adapt the region map for the target.
        /// </summary>
        /// <param name="regionMapBuilder">The region map builder.</param>
        protected virtual void Adapt(GridStringBuilder regionMapBuilder)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <inheritdoc/>
        public bool SupportsPan => true;

        /// <inheritdoc/>
        public bool SupportsZoom => true;

        /// <inheritdoc/>
        public void BuildRegionMap(Region region, Point3D focusPosition, RegionMapRenderOptions regionMapOptions, RoomMapRenderOptions roomMapOptions, Size maxSize)
        {
            // for now, cheat and use the ANSI builder then convert to string

            // create an ANSI grid string builder just for this map
            GridStringBuilder ansiGridStringBuilder = new();
            ansiGridStringBuilder.Resize(maxSize);

            var ansiRegionBuilder = new ConsoleRegionMapBuilder(ansiGridStringBuilder, properties);
            ansiRegionBuilder.BuildRegionMap(region, focusPosition, regionMapOptions, roomMapOptions, maxSize, new(0, 0));
            Adapt(ansiGridStringBuilder);
        }

        #endregion
    }
}

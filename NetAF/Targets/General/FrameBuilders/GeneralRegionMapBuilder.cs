﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using System;

namespace NetAF.Targets.General.FrameBuilders
{
    /// <summary>
    /// Provides a builder for region maps.
    /// </summary>
    public abstract class GeneralRegionMapBuilder : IRegionMapBuilder
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

        /// <summary>
        /// Get or set the maximum size.
        /// </summary>
        public Size MaxSize { get; set; } = new Size(80, 40);

        #endregion

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

        /// <summary>
        /// Get if this frame builder supports panning.
        /// </summary>
        public bool SupportsPan => true;

        /// <summary>
        /// Get if this frame builder supports zooming.
        /// </summary>
        public bool SupportsZoom => true;

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="detail">The level of detail to use.</param>
        public void BuildRegionMap(Region region, Point3D focusPosition, RegionMapDetail detail)
        {
            // for now, cheat and use the ANSI builder then convert to string

            // create an ANSI grid string builder just for this map
            GridStringBuilder ansiGridStringBuilder = new();
            ansiGridStringBuilder.Resize(MaxSize);

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

            ansiRegionBuilder.BuildRegionMap(region, focusPosition, detail, new(0, 0), MaxSize);
            Adapt(ansiGridStringBuilder);
        }

        #endregion
    }
}

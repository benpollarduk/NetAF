using System;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides properties for rendering a map in a console frame.
    /// </summary>
    public sealed class ConsoleMapRenderProperties : ICloneable
    {
        #region StaticProperties

        /// <summary>
        /// Get the default console render properties.
        /// </summary>
        public static ConsoleMapRenderProperties Default => new()
        {
            LockedExit = 'x',
            PointOfInterest = '!',
            NoPointOfInterest = ' ',
            VerticalBoundary = '|',
            HorizontalBoundary = '-',
            VerticalExitBorder = '|',
            HorizontalExitBorder = '-',
            Corner = '+',
            EmptySpace = ' ',
            UnLockedExit = ' ',
            LowerLevel = '.',
            Player = 'O',
            Focus = '+',
            CurrentFloorIndicator = '*',
            HorizontalKeyPadding = 6,
            VerticalKeyPadding = 1,
            BoundaryColor = AnsiColor.BrightBlack,
            PointOfInterestColor = NetAFPalette.NetAFBlue,
            LockedExitColor = NetAFPalette.NetAFRed,
            VisitedExitColor = NetAFPalette.NetAFYellow,
            UnvisitedExitColor = NetAFPalette.NetAFGreen,
            FocusedBoundaryColor = NetAFPalette.NetAFBlue,
            VisitedBoundaryColor = AnsiColor.White,
            UnvisitedBoundaryColor = AnsiColor.BrightBlack,
            PlayerColor = NetAFPalette.NetAFBlue,
            LowerLevelColor = AnsiColor.BrightBlack,
            DisplayDirections = true,
            ShowLowerFloors = true
        };

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; }

        /// <summary>
        /// Get or set the character used for representing a point of interest.
        /// </summary>
        public char PointOfInterest { get; set; }

        /// <summary>
        /// Get or set the character used for representing no point of interest.
        /// </summary>
        public char NoPointOfInterest { get; set; }

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; }

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; }

        /// <summary>
        /// Get or set the character to use for vertical exit borders.
        /// </summary>
        public char VerticalExitBorder { get; set; }

        /// <summary>
        /// Get or set the character to use for horizontal exit borders.
        /// </summary>
        public char HorizontalExitBorder { get; set; }

        /// <summary>
        /// Get or set the character to use for corners.
        /// </summary>
        public char Corner { get; set; }

        /// <summary>
        /// Get or set the character used for empty space in the map.
        /// </summary>
        public char EmptySpace { get; set; }

        /// <summary>
        /// Get or set the character used for representing an unlocked exit.
        /// </summary>
        public char UnLockedExit { get; set; }

        /// <summary>
        /// Get or set the character to use for lower levels.
        /// </summary>
        public char LowerLevel { get; set; }

        /// <summary>
        /// Get or set the character to use for indicating the player.
        /// </summary>
        public char Player { get; set; }

        /// <summary>
        /// Get or set the character to use for indicating the focus.
        /// </summary>
        public char Focus { get; set; }

        /// <summary>
        /// Get or set the character to use for the current floor.
        /// </summary>
        public char CurrentFloorIndicator { get; set; }

        /// <summary>
        /// Get or set the horizontal padding between the key and the map.
        /// </summary>
        public int HorizontalKeyPadding { get; set; }

        /// <summary>
        /// Get or set the vertical padding between the key and the map.
        /// </summary>
        public int VerticalKeyPadding { get; set; }

        /// <summary>
        /// Get or set the room boundary color.
        /// </summary>
        public AnsiColor BoundaryColor { get; set; }

        /// <summary>
        /// Get or set the point of interest color.
        /// </summary>
        public AnsiColor PointOfInterestColor { get; set; }

        /// <summary>
        /// Get or set the locked exit color.
        /// </summary>
        public AnsiColor LockedExitColor { get; set; }

        /// <summary>
        /// Get or set the visited exit color.
        /// </summary>
        public AnsiColor VisitedExitColor { get; set; }

        /// <summary>
        /// Get or set the unvisited exit color.
        /// </summary>
        public AnsiColor UnvisitedExitColor { get; set; }

        /// <summary>
        /// Get or set the focused room boundary color.
        /// </summary>
        public AnsiColor FocusedBoundaryColor { get; set; }

        /// <summary>
        /// Get or set the visited room boundary color.
        /// </summary>
        public AnsiColor VisitedBoundaryColor { get; set; }

        /// <summary>
        /// Get or set the unvisited room boundary color.
        /// </summary>
        public AnsiColor UnvisitedBoundaryColor { get; set; }

        /// <summary>
        /// Get or set the player color.
        /// </summary>
        public AnsiColor PlayerColor { get; set; }

        /// <summary>
        /// Get or set the lower level color.
        /// </summary>
        public AnsiColor LowerLevelColor { get; set; }

        /// <summary>
        /// Get or set if directions are displayed.
        /// </summary>
        public bool DisplayDirections { get; set; }

        /// <summary>
        /// Get or set if lower floors should be shown.
        /// </summary>
        public bool ShowLowerFloors { get; set; }

        #endregion

        #region Implementation of ICloneable

        /// <inheritdoc/>
        public object Clone()
        {
            return new ConsoleMapRenderProperties()
            {
                LockedExit = LockedExit,
                PointOfInterest = PointOfInterest,
                NoPointOfInterest = NoPointOfInterest,
                VerticalBoundary = VerticalBoundary,
                HorizontalBoundary = HorizontalBoundary,
                VerticalExitBorder = VerticalExitBorder,
                HorizontalExitBorder = HorizontalExitBorder,
                Corner = Corner,
                EmptySpace = EmptySpace,
                UnLockedExit = UnLockedExit,
                LowerLevel = LowerLevel,
                Player = Player,
                Focus = Focus,
                CurrentFloorIndicator = CurrentFloorIndicator,
                HorizontalKeyPadding = HorizontalKeyPadding,
                VerticalKeyPadding = VerticalKeyPadding,
                BoundaryColor = BoundaryColor,
                PointOfInterestColor = PointOfInterestColor,
                LockedExitColor = LockedExitColor,
                VisitedExitColor = VisitedExitColor,
                UnvisitedExitColor = UnvisitedExitColor,
                FocusedBoundaryColor = FocusedBoundaryColor,
                VisitedBoundaryColor = VisitedBoundaryColor,
                UnvisitedBoundaryColor = UnvisitedBoundaryColor,
                PlayerColor = PlayerColor,
                LowerLevelColor = LowerLevelColor,
                DisplayDirections = DisplayDirections,
                ShowLowerFloors = ShowLowerFloors
            };
        }

        #endregion
    }
}

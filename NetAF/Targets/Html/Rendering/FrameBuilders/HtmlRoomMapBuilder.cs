using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlRoomMapBuilder(HtmlBuilder builder) : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing there is an item or a character in the room.
        /// </summary>
        public char ItemOrCharacterInRoom { get; set; } = '?';

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

            // for now, cheat and use the ansi builder then convert to HTML

            // create an ansi grid string builder just for this map
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

            var roomAsString = HtmlAdapter.ConvertGridStringBuilderToHtmlString(ansiGridStringBuilder);

            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            builder.Raw($"<pre style=\"font-family: 'Courier New', Courier, monospace;\">{roomAsString}</pre>");
        }

        #endregion
    }
}

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents any object that is a frame that can display a command based interface.
    /// </summary>
    public interface IConsoleFrame : IAnsiGridFrame
    {
        /// <summary>
        /// Get the cursor left position.
        /// </summary>
        int CursorLeft { get; }
        /// <summary>
        /// Get the cursor top position.
        /// </summary>
        int CursorTop { get; }
        /// <summary>
        /// Get or set if the cursor should be shown.
        /// </summary>
        bool ShowCursor { get; set; }
    }
}

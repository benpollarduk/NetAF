namespace NetAF.Rendering
{
    /// <summary>
    /// Provides options for rendering a frame.
    /// </summary>
    public class RenderProperties
    {
        #region Properties

        /// <summary>
        /// Get or set a value indicating whether keywords should be highlighted.
        /// </summary>
        public bool HighlightKeywords { get; set; }

        /// <summary>
        /// Get or set a value indicating whether conversation history should be rendered.
        /// </summary>
        public bool RenderConversationHistory { get; set; }

        #endregion
    }
}

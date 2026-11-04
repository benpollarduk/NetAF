namespace NetAF.Rendering
{
    /// <summary>
    /// Provides options for rendering a frame.
    /// </summary>
    public class RenderProperties
    {
        #region Properties

        /// <summary>
        /// Get or set a value indicating whether keywords should be highlighted in scenes.
        /// </summary>
        public bool HighlightSceneKeywords { get; set; }

        /// <summary>
        /// Get or set a value indicating whether keywords should be highlighted in reactions.
        /// </summary>
        public bool HighlightReactionKeywords { get; set; }
        
        /// <summary>
        /// Get or set a value indicating whether conversation history should be rendered.
        /// </summary>
        public bool RenderConversationHistory { get; set; }

        #endregion
    }
}

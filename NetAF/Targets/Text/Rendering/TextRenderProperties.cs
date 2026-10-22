using NetAF.Rendering;

namespace NetAF.Targets.Text.Rendering
{
    /// <summary>
    /// Provides options for rendering a text frame.
    /// </summary>
    public sealed class TextRenderProperties : RenderProperties
    {
        #region StaticProperties

        /// <summary>
        /// Get the default text render properties.
        /// </summary>
        public static TextRenderProperties Default => new()
        {
            HighlightKeywords = true,
            RenderConversationHistory = false
        };

        #endregion
    }
}

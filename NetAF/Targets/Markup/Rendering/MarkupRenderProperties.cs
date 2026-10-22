using NetAF.Rendering;

namespace NetAF.Targets.Markup.Rendering
{
    /// <summary>
    /// Provides options for rendering a markup frame.
    /// </summary>
    public sealed class MarkupRenderProperties : RenderProperties
    {
        #region StaticProperties

        /// <summary>
        /// Get the default markup render properties.
        /// </summary>
        public static MarkupRenderProperties Default => new()
        {
            HighlightKeywords = true,
            RenderConversationHistory = false
        };

        #endregion
    }
}

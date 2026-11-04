using NetAF.Rendering;

namespace NetAF.Targets.Html.Rendering
{
    /// <summary>
    /// Provides options for rendering a HTML frame.
    /// </summary>
    public sealed class HtmlRenderProperties : RenderProperties
    {
        #region StaticProperties

        /// <summary>
        /// Get the default HTML render properties.
        /// </summary>
        public static HtmlRenderProperties Default => new()
        {
            HighlightSceneKeywords = true,
            HighlightReactionKeywords = false,
            RenderConversationHistory = false
        };

        #endregion
    }
}

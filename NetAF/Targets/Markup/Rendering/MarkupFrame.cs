using NetAF.Rendering;

namespace NetAF.Targets.Markup.Rendering
{
    /// <summary>
    /// Provides a markup frame for displaying a command based interface.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    public sealed class MarkupFrame(MarkupBuilder builder) : IFrame
    {
        #region Implementation of IFrame

        /// <inheritdoc/>
        public string ToPresentableString()
        {
            return builder.ToString();
        }

        #endregion
    }
}
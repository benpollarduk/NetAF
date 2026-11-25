using NetAF.Rendering;

namespace NetAF.Targets.Markup.Rendering
{
    /// <summary>
    /// Provides a markup frame for displaying a command based interface.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    public sealed class MarkupFrame(MarkupBuilder builder) : IFrame
    {
        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            return builder.ToString();
        }

        #endregion
    }
}
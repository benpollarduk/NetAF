using System.Text;
using NetAF.Rendering;

namespace NetAF.Targets.Text.Rendering
{
    /// <summary>
    /// Provides text frame for displaying a command based interface.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    public sealed class TextFrame(StringBuilder builder) : IFrame
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
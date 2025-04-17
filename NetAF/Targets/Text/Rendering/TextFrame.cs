﻿using System.Text;
using NetAF.Rendering;

namespace NetAF.Targets.Text.Rendering
{
    /// <summary>
    /// Provides text frame for displaying a command based interface.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    public sealed class TextFrame(StringBuilder builder) : IFrame
    {
        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return builder.ToString();
        }

        #endregion

        #region Implementation of IFrame<Inline>

        /// <summary>
        /// Render this frame on a presenter.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        public void Render(IFramePresenter presenter)
        {
            presenter.Present(ToString());
        }

        #endregion
    }
}
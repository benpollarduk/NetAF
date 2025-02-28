using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of help frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlHelpFrameBuilder(HtmlBuilder builder) : IHelpFrameBuilder
    {
        #region Implementation of IHelpFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, CommandHelp commandHelp, Size size)
        {
            builder.Clear();

            builder.H1(title);
            builder.Br();

            builder.P($"Command: {commandHelp.Command}");

            if (!string.IsNullOrEmpty(commandHelp.Shortcut))
                builder.P($"Shortcut: {commandHelp.Shortcut}");

            builder.P($"Description: {commandHelp.Description.EnsureFinishedSentence()}");

            if (!string.IsNullOrEmpty(commandHelp.Instructions))
                builder.P($"Instructions: {commandHelp.Instructions.EnsureFinishedSentence()}");

            if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                builder.P($"Example: {commandHelp.DisplayAs}");

            return new HtmlFrame(builder);
        }

        #endregion
    }
}

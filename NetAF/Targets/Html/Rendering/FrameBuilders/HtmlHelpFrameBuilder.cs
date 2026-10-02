using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of help frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlHelpFrameBuilder(HtmlBuilder builder) : IHelpFrameBuilder
    {
        #region Implementation of IHelpFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, CommandHelp commandHelp, Prompt[] prompts, Size size)
        {
            builder.Clear();

            builder.H1(title);
            builder.Br();

            if (commandHelp != null)
            {
                builder.P($"Command: {commandHelp.Command}");

                if (!string.IsNullOrEmpty(commandHelp.Shortcut))
                    builder.P($"Shortcut: {commandHelp.Shortcut}");

                builder.P($"Description: {commandHelp.Description.EnsureFinishedSentence()}");

                if (!string.IsNullOrEmpty(commandHelp.Instructions))
                    builder.P($"Instructions: {commandHelp.Instructions.EnsureFinishedSentence()}");

                if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                    builder.P($"Example: {commandHelp.DisplayAs}");

                StringBuilder promptBuilder = new();

                foreach (var prompt in prompts ?? [])
                    promptBuilder.Append($"'{prompt.Entry}' ");

                var promptString = promptBuilder.ToString();

                if (!string.IsNullOrEmpty(promptString))
                    builder.P($"Prompts: {promptString}");
            }

            return new HtmlFrame(builder);
        }

        #endregion
    }
}

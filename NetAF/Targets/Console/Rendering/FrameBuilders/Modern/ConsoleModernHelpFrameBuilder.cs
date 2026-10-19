using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using System.Text;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of help frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleModernHelpFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, IHelpFrameBuilder
    {
        #region Implementation of IHelpFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, CommandHelp commandHelp, Prompt[] prompts, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            gridStringBuilder.DrawWrapped(title, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);

            lastY += 3;

            if (commandHelp != null)
            {
                gridStringBuilder.DrawWrapped($"Command: {commandHelp.Command}", xMargin, lastY, constrainedWidth, properties.OptionColor, out _, out lastY);

                if (!string.IsNullOrEmpty(commandHelp.Shortcut))
                    gridStringBuilder.DrawWrapped($"Shortcut: {commandHelp.Shortcut}", xMargin, lastY + 2, constrainedWidth, properties.OptionColor, out _, out lastY);

                gridStringBuilder.DrawWrapped($"Description: {commandHelp.Description.EnsureFinishedSentence()}", xMargin, lastY + 2, constrainedWidth, properties.OptionDescriptionColor, out _, out lastY);

                if (!string.IsNullOrEmpty(commandHelp.Instructions))
                    gridStringBuilder.DrawWrapped($"Instructions: {commandHelp.Instructions.EnsureFinishedSentence()}", xMargin, lastY + 2, constrainedWidth, properties.OptionDescriptionColor, out _, out lastY);

                if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                    gridStringBuilder.DrawWrapped($"Example: {commandHelp.DisplayAs}", xMargin, lastY + 2, constrainedWidth, properties.OptionDescriptionColor, out _, out lastY);

                StringBuilder synonymBuilder = new();

                foreach (var synonym in commandHelp.Synonyms ?? [])
                    synonymBuilder.Append($"'{synonym}' ");

                var synonymString = synonymBuilder.ToString();

                if (!string.IsNullOrEmpty(synonymString))
                    gridStringBuilder.DrawWrapped($"Synonyms: {synonymString}", xMargin, lastY + 2, constrainedWidth, properties.OptionColor, out _, out lastY);

                StringBuilder promptBuilder = new();

                foreach (var prompt in prompts ?? [])
                    promptBuilder.Append($"'{prompt.Entry}' ");

                var promptString = promptBuilder.ToString();

                if (!string.IsNullOrEmpty(promptString))
                    gridStringBuilder.DrawWrapped($"Prompts: {promptString}", xMargin, lastY + 2, constrainedWidth, properties.InputColor, out _, out _);
            }

            return new GridTextFrame(gridStringBuilder, 0, 0, false, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}

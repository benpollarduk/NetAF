using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of about frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupAboutFrameBuilder(MarkupBuilder builder) : IAboutFrameBuilder
    {
        #region Implementation of IAboutFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="game">The game.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, Game game, Size size)
        {
            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();
            builder.Text(game.Info.Name);
            builder.Text(game.Info.Description.EnsureFinishedSentence());

            if (!string.IsNullOrEmpty(game.Info.Author))
                builder.Text($"Created by: {game.Info.Author}".EnsureFinishedSentence());
            else
                builder.Text(Info.AboutNetAF);

            return new MarkupFrame(builder);
        }

        #endregion
    }
}

using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Information
{
    /// <summary>
    /// Represents the History command.
    /// </summary>
    public sealed class History : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("History", "View in-game history", CommandCategory.Global);

        /// <summary>
        /// Get the prompt for clear.
        /// </summary>
        public static Prompt Clear => new("Clear");

        /// <summary>
        /// Get the prompt for show.
        /// </summary>
        public static Prompt Show => new("Show");

        #endregion

        #region Fields

        private string args;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the History command.
        /// </summary>
        public History() : this(Show.Entry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the History command.
        /// </summary>
        /// <param name="args">The arguments for the command.</param>
        public History(string args)
        {
            this.args = args;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => CommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (string.IsNullOrEmpty(args) || args.InsensitiveEquals(Show.Entry))
            {
                game.ChangeMode(new HistoryMode(game.HistoryManager));
                return new(ReactionResult.GameModeChanged, string.Empty);
            }
            else if (args.InsensitiveEquals(Clear.Entry))
            {
                game.HistoryManager.Clear();
                return new(ReactionResult.Inform, "History cleared.");
            }
            else
            {
                return new(ReactionResult.Error, $"Invalid argument '{args}'.");
            }
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [ Show, Clear ];
        }

        #endregion
    }
}

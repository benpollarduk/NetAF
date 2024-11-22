using NetAF.Assets;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Examine command.
    /// </summary>
    /// <param name="examinable">The examinable.</param>
    public sealed class Examine(IExaminable examinable) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Examine", "Examine something", "X");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (examinable == null)
                return new(ReactionResult.Error, "Nothing to examine.");

            return new(ReactionResult.Inform, examinable.Examine(new ExaminationScene(game)).Description);
        }

        #endregion
    }
}
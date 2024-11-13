using NetAF.Assets;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Examine command.
    /// </summary>
    /// <param name="examinable">The examinable.</param>
    public class Examine(IExaminable examinable) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Examine", "Examine anything in the game", "X");

        #endregion

        #region Properties

        /// <summary>
        /// Get the examinable.
        /// </summary>
        public IExaminable Examinable { get; } = examinable;

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (Examinable == null)
                return new(ReactionResult.Error, "Nothing to examine.");

            return new(ReactionResult.OK, Examinable.Examine(new ExaminationScene(game)).Description);
        }

        #endregion
    }
}
using NetAF.Assets;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Game
{
    /// <summary>
    /// Represents the Examine command.
    /// </summary>
    /// <param name="examinable">The examinable.</param>
    internal class Examine(IExaminable examinable) : ICommand
    {
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
using NetAF.Assets;
using NetAF.Logic;
using System.Linq;

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
        public static CommandHelp CommandHelp { get; } = new("Examine", "Examine something", CommandCategory.Scene, "X", displayAs: "Examine/X __", instructions: "Examine any examinable object. This includes characters, exits, items, rooms and regions.");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (examinable == null)
                return new(ReactionResult.Error, "Nothing to examine.");

            return new(ReactionResult.Inform, examinable.Examine(new ExaminationScene(game)).Description);
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [..game?.GetAllPlayerVisibleExaminables()?.Select(x => x.Identifier.Name).Select(x => new Prompt(x))];
        }

        #endregion
    }
}
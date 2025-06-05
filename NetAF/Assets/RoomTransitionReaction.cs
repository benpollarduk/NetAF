using NetAF.Commands;

namespace NetAF.Assets
{
    /// <summary>
    /// Represents a reaction to a room transition.
    /// </summary>
    /// <param name="reaction">The reaction.</param>
    /// <param name="continueWithTransition">If the transition should be continued with.</param>
    public sealed class RoomTransitionReaction(Reaction reaction, bool continueWithTransition)
    {
        #region StaticProperties

        /// <summary>
        /// Provides a default value for Silent.
        /// </summary>
        public static RoomTransitionReaction Silent { get; } = new(Reaction.Silent, true);

        #endregion

        #region Properties

        /// <summary>
        /// Get the reaction.
        /// </summary>
        public Reaction Reaction { get; } = reaction;

        /// <summary>
        /// Get if the transition should be continued with.
        /// </summary>
        public bool ContinueWithTransition { get; } = continueWithTransition;

        #endregion
    }
}

namespace NetAF.Assets
{
    /// <summary>
    /// Represents the callback for handling transitioning between rooms.
    /// </summary>
    /// <param name="transition">The transition.</param>
    /// <returns>The reaction to the transition.</returns>
    public delegate RoomTransitionReaction RoomTransitionCallback(RoomTransition transition);
}
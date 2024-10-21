using NetAF.Assets.Characters;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents a callback for Player creation.
    /// </summary>
    /// <returns>A generated Player.</returns>
    public delegate PlayableCharacter PlayerCreationCallback();
}
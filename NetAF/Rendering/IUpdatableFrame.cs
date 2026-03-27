using System;

namespace NetAF.Rendering
{
    /// <summary>
    /// Represents any object that is an updateable frame for displaying an interface.
    /// </summary>
    public interface IUpdatableFrame : IFrame
    {
        /// <summary>
        /// Occurs when the frame is updated.
        /// </summary>
        event EventHandler<IFrame> Updated;
    }
}

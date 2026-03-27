using NetAF.Logic.Callbacks;
using NetAF.Rendering;

namespace NetAF.Logic
{
    /// <summary>
    /// Provides a manager for updateable frames.
    /// </summary>
    internal static class UpdatableFrameManager
    {
        #region StaticFields

        private static IFrame lastFrame;
        private static FrameUpdateCallback updateCallback;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Manage the transition of the frame.
        /// </summary>
        /// <param name="frame">The incoming frame.</param>
        /// <param name="callback">A callback to invoke when the frame updates.</param>
        public static void ManageFrameTransition(IFrame frame, FrameUpdateCallback callback)
        {
            if (lastFrame is IUpdatableFrame oldUpdateable)
            {
                oldUpdateable.Stop();
                oldUpdateable.Updated -= Updateable_Updated;
            }

            lastFrame = frame;
            updateCallback = callback;

            if (lastFrame is IUpdatableFrame newUpdateable)
            {
                newUpdateable.Updated += Updateable_Updated;
                newUpdateable.Start();
            }
        }

        #endregion

        #region EventHandlers

        private static void Updateable_Updated(object sender, IFrame e)
        {
            updateCallback?.Invoke(e);
        }

        #endregion
    }
}

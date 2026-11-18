namespace NetAF.Rendering.Animation
{
    /// <summary>
    /// Provides options for an animation.
    /// </summary>
    /// <param name="Cycles">The number of times to cycle the animation. For infinite cycles, use AnimationOptions.Forever.</param>
    /// <param name="FramesPerSecond">The number of frames per second to run the animation at.</param>
    public record AnimationOptions(int Cycles = 1, int FramesPerSecond = 20)
    {
        #region Constants

        /// <summary>
        /// Get a value representing forever.
        /// </summary>
        public const int Forever = -1;

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get a value for a single playthrough of the animation.
        /// </summary>
        public static AnimationOptions Single => new(1);

        #endregion
    }
}

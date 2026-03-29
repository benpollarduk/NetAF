using System;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a variation generator that generates by intensity.
    /// </summary>
    /// <param name="maximumSubtraction">The maximum amount that can be subtracted from the intensity.</param>
    /// <param name="maximumAddition">The maximum amount that can be aded from the intensity.</param>
    public class IntensityVariationGenerator(byte maximumSubtraction, byte maximumAddition) : IVariationGenerator
    {
        #region StaticFields

        private static readonly Random Random = new();

        #endregion

        #region Implementation of IVariationGenerator

        /// <summary>
        /// Vary a color.
        /// </summary>
        /// <param name="color">The color to vary.</param>
        /// <returns>The varied color</returns>
        public AnsiColor Vary(AnsiColor color)
        {
            var offset = Random.Next(-maximumSubtraction, maximumAddition);
            return new AnsiColor((byte)int.Clamp(color.R + offset, 0, 255), (byte)int.Clamp(color.G + offset, 0, 255), (byte)int.Clamp(color.B + offset, 0, 255));
        }

        #endregion
    }
}

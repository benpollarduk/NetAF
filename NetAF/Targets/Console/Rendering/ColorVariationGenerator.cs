using System;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a variation generator that generates by intensity.
    /// </summary>
    /// <param name="maximumSubtractionR">The maximum amount that can be subtracted from the intensity, for the red channel.</param>
    /// <param name="maximumAdditionR">The maximum amount that can be aded from the intensity, for the red channel.</param>
    /// <param name="maximumSubtractionG">The maximum amount that can be subtracted from the intensity, for the green channel.</param>
    /// <param name="maximumAdditionG">The maximum amount that can be aded from the intensity, for the green channel.</param>
    /// <param name="maximumSubtractionB">The maximum amount that can be subtracted from the intensity, for the blue channel.</param>
    /// <param name="maximumAdditionB">The maximum amount that can be aded from the intensity, for the blue channel.</param>
    public class ColorVariationGenerator(byte maximumSubtractionR, byte maximumAdditionR, byte maximumSubtractionG, byte maximumAdditionG, byte maximumSubtractionB, byte maximumAdditionB) : IVariationGenerator
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
            var offsetR = Random.Next(-maximumSubtractionR, maximumAdditionR);
            var offsetG = Random.Next(-maximumSubtractionG, maximumAdditionG);
            var offsetB = Random.Next(-maximumSubtractionB, maximumAdditionB);
            return new AnsiColor((byte)int.Clamp(color.R + offsetR, 0, 255), (byte)int.Clamp(color.G + offsetG, 0, 255), (byte)int.Clamp(color.B + offsetB, 0, 255));
        }

        #endregion
    }
}

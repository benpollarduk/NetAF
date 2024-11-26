using System;

namespace NetAF.Rendering.Console
{
    /// <summary>
    /// Represents an ANSI color.
    /// </summary>
    /// <param name="r">The red channel.</param>
    /// <param name="g">The green channel.</param>
    /// <param name="b">The blue channel.</param>
    public struct AnsiColor(byte r, byte g, byte b) : IEquatable<AnsiColor>
    {
        #region Properties

        /// <summary>
        /// Get the red channel.
        /// </summary>
        public readonly byte R => r;

        /// <summary>
        /// Get the green channel.
        /// </summary>
        public readonly byte G => g;

        /// <summary>
        /// Get the blue channel.
        /// </summary>
        public readonly byte B => b;

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="obj" /> parameter; otherwise, <see langword="false" />.</returns>
        public override readonly bool Equals(object obj)
        {
            return obj is AnsiColor color && Equals(color);
        }

        #endregion

        #region Implementation of IEquatable<AnsiColor>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public readonly bool Equals(AnsiColor other)
        {
            return R == other.R && G == other.G && B == other.B;
        }

        #endregion

        #region StaticProperties

        /// <summary>
        /// Black.
        /// </summary>
        public static AnsiColor Black { get; } = new(0, 0, 0);

        /// <summary>
        /// Red.
        /// </summary>
        public static AnsiColor Red { get; } = new(128, 0, 0);

        /// <summary>
        /// Green.
        /// </summary>
        public static AnsiColor Green { get; } = new(0, 128, 0);

        /// <summary>
        /// Yellow
        /// </summary>
        public static AnsiColor Yellow { get; } = new(128, 128, 0);

        /// <summary>
        /// Blue.
        /// </summary>
        public static AnsiColor Blue { get; } = new(0, 0, 128);

        /// <summary>
        /// Magenta.
        /// </summary>
        public static AnsiColor Magenta { get; } = new(128, 0, 128);

        /// <summary>
        /// Cyan.
        /// </summary>
        public static AnsiColor Cyan { get; } = new(0, 128, 128);

        /// <summary>
        /// White.
        /// </summary>
        public static AnsiColor White { get; } = new(192, 192, 192);

        /// <summary>
        /// Bright black.
        /// </summary>
        public static AnsiColor BrightBlack { get; } = new(128, 128, 128);

        /// <summary>
        /// Bright red.
        /// </summary>
        public static AnsiColor BrightRed { get; } = new(255, 0, 0);

        /// <summary>
        /// Bright green.
        /// </summary>
        public static AnsiColor BrightGreen { get; } = new(0, 255, 0);

        /// <summary>
        /// Bright yellow.
        /// </summary>
        public static AnsiColor BrightYellow { get; } = new(255, 255, 0);

        /// <summary>
        /// Bright blue.
        /// </summary>
        public static AnsiColor BrightBlue { get; } = new(0, 0, 255);

        /// <summary>
        /// Bright magenta.
        /// </summary>
        public static AnsiColor BrightMagenta { get; } = new(255, 0, 255);

        /// <summary>
        /// Bright cyan.
        /// </summary>
        public static AnsiColor BrightCyan { get; } = new(0, 255, 255);

        /// <summary>
        /// Bright white.
        /// </summary>
        public static AnsiColor BrightWhite { get; } = new(255, 255, 255);

        #endregion

        #region StaticMethods

        /// <summary>
        /// Determines whether two specified AnsiColors have the same value.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        // <see langword="true" /> if the value of a is the same as the value of b, else <see langword="false" />.</returns>
        public static bool operator ==(AnsiColor left, AnsiColor right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified AnsiColors have different values.
        /// </summary>
        /// <param name="left">The left argument.</param>
        /// <param name="right">The right argument.</param>
        // <see langword="true" /> if the value of a is different to the value of b, else <see langword="false" />.</returns>
        public static bool operator !=(AnsiColor left, AnsiColor right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
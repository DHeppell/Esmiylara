using System;

namespace Esmiylara.Enumerations
{
    /// <summary>
    /// Defines mouse button values.
    /// </summary>
    public enum MouseButton : byte
    {
        /// <summary>
        /// Defines the left mouse button value.
        /// </summary>
        Left = (1 << 0),
        /// <summary>
        /// Defines the right mouse button value.
        /// </summary>
        Right = (1 << 1),
        /// <summary>
        /// Defines the middle mouse button value.
        /// </summary>
        Middle = (1 << 2),
    }

    /// <summary>
    /// Defines extension methods for the enumeration.
    /// </summary>
    public static class MouseButtonExtensions
    {
        /// <summary>
        /// Returns the button value as a byte.
        /// </summary>
        /// <param name="button">The button to convert.</param>
        /// <returns>The byte value.</returns>
        public static byte AsByte(this MouseButton button)
        {
            // Use math magic logarithms to find the value.
            var value = (byte)Math.Log((int)button, 2);

            // Make sure the value is not lower then zero.
            return Math.Max((byte)0, value);
        }
    }
}
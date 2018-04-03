using System;
using UnityEngine;

namespace Esmiylara.Enumerations
{
    /// <summary>
    /// Defines direction facing values.
    /// </summary>
    public enum Direction : byte
    {
        /// <summary>
        /// Defines the value for north.
        /// </summary>
        North = (1 << 0),
        /// <summary>
        /// Defines the value for south.
        /// </summary>
        South = (1 << 1),
        /// <summary>
        /// Defines the value for west.
        /// </summary>
        West = (1 << 2),
        /// <summary>
        /// Defines the value for east.
        /// </summary>
        East = (1 << 3),

        /// <summary>
        /// Defines the value for up.
        /// </summary>
        Up = North,
        /// <summary>
        /// Defines the value for down.
        /// </summary>
        Down = South,
        /// <summary>
        /// Defines the value for left.
        /// </summary>
        Left = West,
        /// <summary>
        /// Defines the value for right.
        /// </summary>
        Right = East,

        /// <summary>
        /// Defines the value for no direction.
        /// </summary>
        None = byte.MaxValue,
    }

    /// <summary>
    /// Defines extension methods for the enumeration.
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Returns the direction value as a byte.
        /// </summary>
        /// <param name="direction">The direction to convert.</param>
        /// <returns>The byte value.</returns>
        public static byte AsByte(this Direction direction)
        {
            // Use math magic logarithms to find the value.
            var value = (byte)Math.Log((int)direction, 2);

            // Make sure the value is not lower then zero.
            return Math.Max((byte)0, value);
        }

        /// <summary>
        /// Gets the value of a vector using the direction.
        /// </summary>
        /// <param name="direction">The direction to use.</param>
        /// <param name="vector">The vector to look in.</param>
        /// <returns>The value of the vector.</returns>
        public static float GetVectorAxis(this Direction direction, Vector2 vector)
        {
            // Select the direction.
            switch (direction)
            {
                // If they pick north, south, up, or down give back the Y axis.
                case Direction.North:
                case Direction.South:
                    return vector.y;

                // If they pick west, east, left, or right give back the X axis.
                case Direction.West:
                case Direction.East:
                    return vector.x;
            }

            // If we hit this, something is wrong.
            throw new NotImplementedException("Error converting vector to direction axis, the direction specified is not handled.");
        }

        /// <summary>
        /// Gets a direction value from a vector.
        /// </summary>
        /// <param name="vector">The vector to evaluate.</param>
        /// <returns>The direction that the vector equals.</returns>
        public static Direction GetDirectionFromVector(Vector2 vector)
        {
            // Convert values to absolute values first.
            var vectorX = Math.Abs(vector.x);
            var vectorY = Math.Abs(vector.y);

            // Check to see which is bigger.
            if(vectorX > vectorY)
            {
                // Check the X axis first.
                if (vector.x > 0) { return Direction.Right; }
                if (vector.x < 0) { return Direction.Left; }

                // Check the Y axis next.
                if (vector.y < 0) { return Direction.Down; }
                if (vector.y > 0) { return Direction.Up; }
            }
            else
            {
                // Check the Y axis first.
                if (vector.y < 0) { return Direction.Down; }
                if (vector.y > 0) { return Direction.Up; }

                // Check the X axis next.
                if (vector.x > 0) { return Direction.Right; }
                if (vector.x < 0) { return Direction.Left; }
            }           

            // No direction was found.
            return Direction.None;
        }
    }
}
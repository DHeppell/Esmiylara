using Esmiylara.Enumerations;
using System;
using System.Runtime.InteropServices;

namespace Esmiylara.Utilities
{
    /// <summary>
    /// Time utilities class, defines time functions.
    /// </summary>
    public static class TimeUtilities
    {
        /// <summary>
        /// High resolution time class.
        /// </summary>
        public static class HighResolution
        {
            /// <summary>
            /// Tells the engine if the class is usable.
            /// </summary>
            public static bool IsAvailable { get; private set; }

            /// <summary>
            /// Magic thing of magicness, seriously dont touch!
            /// </summary>
            /// <param name="filetime">The variable to save the time to.</param>
            [DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
            private static extern void GetSystemTimePreciseAsFileTime(out long filetime);

            /// <summary>
            /// The current utc high resolution time.
            /// </summary>
            public static DateTime UtcNow
            {
                get
                {
                    // Check to see if available.
                    if (!IsAvailable)
                    {
                        // It's not, fall back to the default date time object.
                        return DateTime.UtcNow;
                    }

                    // Get the current time.
                    long filetime;
                    GetSystemTimePreciseAsFileTime(out filetime);

                    // Return the timestamp.
                    return DateTime.FromFileTimeUtc(filetime);
                }
            }

            /// <summary>
            /// The current high resolution time.
            /// </summary>
            public static DateTime Now
            {
                get
                {
                    // Check to see if available.
                    if (!IsAvailable)
                    {
                        // It's not, fall back to the default date time object.
                        return DateTime.UtcNow;
                    }

                    // Get the current time.
                    long filetime = 0;
                    GetSystemTimePreciseAsFileTime(out filetime);

                    // Return the timestamp.
                    return DateTime.FromFileTime(filetime);
                }
            }

            /// <summary>
            /// Constructs the High Resolution Time class.
            /// </summary>
            static HighResolution()
            {
                try
                {
                    // Try to get the current time.
                    long filetime = 0;
                    GetSystemTimePreciseAsFileTime(out filetime);

                    // Nothing went wrong, it's available.
                    IsAvailable = true;
                }
                catch (EntryPointNotFoundException)
                {
                    // Not available on this computer.
                    Std.Out("Unable to load high resolution time, defaulting to low resolution!", Verbosity.Warning);
                    IsAvailable = false;
                }
            }
        }

        /// <summary>
        /// Unix time class.
        /// </summary>
        public static class Unix
        {
            /// <summary>
            /// The unix epoch.
            /// </summary>
            private static DateTime epoch;

            /// <summary>
            /// Constructs the unix time class.
            /// </summary>
            static Unix()
            {
                // Define the epoch.
                epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            }

            /// <summary>
            /// Gets the current unix timestamp.
            /// </summary>
            public static double Now
            {
                get
                {
                    // Calculate and return the value.
                    return (HighResolution.Now - epoch).TotalMilliseconds;
                }
            }

            /// <summary>
            /// Gets the current utc unix timestamp.
            /// </summary>
            public static double UtcNow
            {
                get
                {
                    // Calculate and return the value.
                    return (HighResolution.UtcNow - epoch).TotalMilliseconds;
                }
            }
        }
    }
}
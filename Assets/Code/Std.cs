using Esmiylara.Enumerations;
using Esmiylara.Extensions;
using Esmiylara.Utilities;
using System;
using UnityEngine;

namespace Esmiylara
{
    /// <summary>
    /// Std class, used to read and write console input/output.
    /// </summary>
    public static class Std
    {
#if DEBUG
        /// <summary>
        /// The timestamp format.
        /// </summary>
        public static string DefaultTimestampFormat { get { return @"yyyy-MM-dd HH\:mm\:ss.fff"; } }
#else
        /// <summary>
        /// The timestamp format.
        /// </summary>
        public static string DefaultTimestampFormat { get { return @"yyyy-MM-dd HH\:mm"; } }
#endif

        /// <summary>
        /// The Std input hoook.
        /// </summary>
        public static EventHandler<StdInArgs> InHook;
        /// <summary>
        /// The Std output hook.
        /// </summary>
        public static EventHandler<StdOutArgs> OutHook;

        /// <summary>
        /// Constructs the class and defines console functions.
        /// </summary>
        static Std()
        {
            // Define default console write hook.
            OutHook += (a, b) =>
            {
                Debug.Log(string.Format("[{0}][{1}] {2}",
                    TimeUtilities.HighResolution.Now.ToString(DefaultTimestampFormat),
                    b.Verbosity.GetDescription(), b.Message));
            };

            // Define default console read hook.
            InHook += (a, b) =>
            {
                b.Value = Console.ReadLine();
            };
        }

        /// <summary>
        /// Std input function, handles console input.
        /// </summary>
        /// <typeparam name="Type">The type of object to get.</typeparam>
        /// <returns>The object from the input.</returns>
        public static string In()
        {
            // Define the event args variable.
            var args = new StdInArgs();

            // Invoke the hook and get a return object.
            if (InHook != null)
                InHook.Invoke(null, args);

            // Return the object from the hook.
            return args.Value;
        }

        /// <summary>
        /// Std output funciton, handles console output.
        /// </summary>
        /// <param name="message">The message being output.</param>
        /// <param name="verbosity">The verbosity of the message being output.</param>
        public static void Out(string message, Verbosity verbosity = Verbosity.Info)
        {
            // Invoke the hook.
            if (OutHook != null)
                OutHook.Invoke(null, new StdOutArgs(message, verbosity));
        }

        /// <summary>
        /// Defines event arguments for Std in class events.
        /// </summary>
        public class StdInArgs : EventArgs
        {
            /// <summary>
            /// The return value.
            /// </summary>
            public string Value;

            /// <summary>
            /// Constructs new Std in event args.
            /// </summary>
            public StdInArgs()
            {
                Value = string.Empty;
            }
        }

        /// <summary>
        /// Defines event arguments for Std out class events.
        /// </summary>
        public class StdOutArgs : EventArgs
        {
            /// <summary>
            /// The message.
            /// </summary>
            public string Message;
            /// <summary>
            /// The verbosity.
            /// </summary>
            public Verbosity Verbosity;

            /// <summary>
            /// Constructs new Std out event args.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="verbosity">The verbosity.</param>
            public StdOutArgs(string message, Verbosity verbosity)
            {
                // Save the properties.
                Message = message;
                Verbosity = verbosity;
            }
        }
    }
}
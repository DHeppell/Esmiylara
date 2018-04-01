using Esmiylara.Enumerations;
using System.ComponentModel;

namespace Esmiylara.Extensions
{
    /// <summary>
    /// Verbosity extensions.
    /// </summary>
    public static class VerbosityExtensions
    {
        /// <summary>
        /// Gets the description of the selected verbosity.
        /// </summary>
        /// <param name="verbosity">The selected verbosity.</param>
        /// <returns>The description of the selected verbosity.</returns>
        public static string GetDescription(this Verbosity verbosity)
        {
            // Get the verbosity attributes.
            var fi = verbosity.GetType().GetField(verbosity.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // Check to see if we got one.
            if (attributes != null && attributes.Length > 0)
            {
                // We did, return it.
                return attributes[0].Description;
            }
            else
            {
                // We did not, return the name.
                return verbosity.ToString();
            }
        }
    }
}
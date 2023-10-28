using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    /// <summary>
    /// Provides utility methods to handle and throw exceptions based on certain conditions.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Throws an ArgumentNullException if the provided argument is null.
        /// </summary>
        /// <param name="argumentName">The name of the argument being checked.</param>
        /// <param name="argument">The actual argument value.</param>
        /// <exception cref="ArgumentNullException">Thrown when the argument is null.</exception>
        public static void ThrowIfNull(string argumentName, object argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName, $"Argument '{argumentName}' cannot be null.");
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if the provided string argument is null, 
        /// or an ArgumentException if the string argument is empty.
        /// </summary>
        /// <param name="argumentName">The name of the string argument being checked.</param>
        /// <param name="argument">The actual string argument value.</param>
        /// <exception cref="ArgumentNullException">Thrown when the string argument is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the string argument is empty.</exception>
        public static void ThrowIfNullOrEmpty(string argumentName, string argument)
        {
            // Check if the argument is null
            ThrowIfNull(argumentName, argument);

            // Check if the argument is an empty string
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentException($"Argument '{argumentName}' cannot be empty.", argumentName);
            }
        }
    }
}
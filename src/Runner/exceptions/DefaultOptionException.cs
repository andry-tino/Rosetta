/// <summary>
/// DefaultOptionException.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.Exceptions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Exception thrown when the unnamed option is not used properly.
    /// </summary>
    internal class DefaultOptionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultOptionException"/> class.
        /// </summary>
        public DefaultOptionException(params string[] options)
            : base("Misuse of the default paramter!")
        {
            this.OptionsValues = options;
        }

        /// <summary>
        /// Gets all the names of the options provided without parameter names.
        /// </summary>
        public IEnumerable<string> OptionsValues { get; private set; }
    }
}

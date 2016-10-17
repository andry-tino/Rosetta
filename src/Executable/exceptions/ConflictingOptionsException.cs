/// <summary>
/// ConflictingOptionsException.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Executable.Exceptions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Exception thrown when conflicts occur in provided parameters.
    /// </summary>
    public class ConflictingOptionsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictingOptionsException"/> class.
        /// </summary>
        public ConflictingOptionsException(params string[] options) 
            : base("A conflict involving some options has been detected!")
        {
            this.Options = options;
        }

        /// <summary>
        /// Gets all the names of the conflicting options.
        /// </summary>
        public IEnumerable<string> Options { get; private set; }
    }
}

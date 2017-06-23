/// <summary>
/// AmbiguousEmbeddedResourceException.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;

    /// <summary>
    /// Exception thrown when an embedded resource whose name is ambiguous could be retrieved.
    /// </summary>
    public class AmbiguousEmbeddedResourceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousEmbeddedResourceException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public AmbiguousEmbeddedResourceException(string message)
            : base(message)
        {
        }
    }
}

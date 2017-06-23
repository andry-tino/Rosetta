/// <summary>
/// EmbeddedResourceNotFoundException.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;

    /// <summary>
    /// Exception thrown when an embedded resource could not be retrieved.
    /// </summary>
    public class EmbeddedResourceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceNotFoundException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public EmbeddedResourceNotFoundException(string message) 
            : base(message)
        {
        }
    }
}

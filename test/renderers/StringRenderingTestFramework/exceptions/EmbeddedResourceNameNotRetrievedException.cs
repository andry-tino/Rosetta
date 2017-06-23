/// <summary>
/// EmbeddedResourceNameNotRetrievedException.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;

    /// <summary>
    /// Exception thrown when an embedded resource name could not be found when inspecting the method.
    /// </summary>
    public class EmbeddedResourceNameNotRetrievedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceNameNotRetrievedException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public EmbeddedResourceNameNotRetrievedException(string message)
            : base(message)
        {
        }
    }
}

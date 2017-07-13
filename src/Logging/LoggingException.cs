/// <summary>
/// ILogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;

    /// <summary>
    /// Describes a logging exception.
    /// </summary>
    public class LoggingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public LoggingException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}

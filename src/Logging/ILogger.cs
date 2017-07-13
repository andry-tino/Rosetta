/// <summary>
/// ILogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;

    /// <summary>
    /// Describes loggers.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an information split into different substrings.
        /// </summary>
        /// <param name="messages">The different strings to emphisize separately in the log entry.</param>
        void Log(params string[] messages);
    }
}

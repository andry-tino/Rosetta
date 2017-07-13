/// <summary>
/// ILogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;

    /// <summary>
    /// Describes log path providers.
    /// </summary>
    public interface ILogPathProvider
    {
        /// <summary>
        /// Gets the path to where logging info should be emitted.
        /// </summary>
        string LogPath { get; }
    }
}

/// <summary>
/// ILogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;

    /// <summary>
    /// Utilities for handling errors when logging.
    /// </summary>
    public static class ErrorUtils
    {
        /// <summary>
        /// When an error occurs while logging we cannot interrupt the execution and fail, so we write the event viewer. 
        /// However the logging in the event viewer might fail as well, so in such a terrible case we swallow everything
        /// and nobody will never know some pretty bad shit happened.
        /// </summary>
        /// <param name="message"></param>
        public static void LogLoggingError(string message)
        {
            try
            {
                ILogger eventViewerLogger = new EventViewerLogger(EventViewerLogger.LogEntryLevel.Error, "Rosetta - Logging errors");

                eventViewerLogger.Log("An unhandled exception occurred while Rosetta was logging", message);
            }
            catch (Exception e)
            {
                // Sorry but at this level we don't really know what to do... 
                // Hopefully somebody will notice if something went wrong.
            }
        }

        /// <summary>
        /// When an error occurs while logging we cannot interrupt the execution and fail, so we write the event viewer. 
        /// However the logging in the event viewer might fail as well, so in such a terrible case we swallow everything
        /// and nobody will never know some pretty bad shit happened.
        /// </summary>
        /// <param name="exception"></param>
        public static void LogLoggingError(this Exception exception)
        {
            try
            {
                ILogger eventViewerLogger = new EventViewerLogger(EventViewerLogger.LogEntryLevel.Error, "Rosetta - Logging errors");

                eventViewerLogger.Log("An unhandled exception occurred while Rosetta was logging", exception.Message, exception.StackTrace);
            }
            catch (Exception e)
            {
                // Sorry but at this level we don't really know what to do... 
                // Hopefully somebody will notice if something went wrong.
            }
        }
    }
}

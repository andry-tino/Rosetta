/// <summary>
/// EventViewerLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Logs information into the event viewer.
    /// </summary>
    public class EventViewerLogger : ILogger
    {
        private const string Separator = "\t"; // Tab
        private const string LogSectionName = "Application";

        private readonly LogEntryLevel level;
        private readonly string sourceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="path">The path of the file to create, including the file name.</param>
        public EventViewerLogger(LogEntryLevel level = LogEntryLevel.Information, string sourceName = "Rosetta")
        {
            if (sourceName == null)
            {
                throw new ArgumentNullException(nameof(sourceName));
            }

            this.sourceName = sourceName;
            this.level = level;
        }

        /// <summary>
        /// Logs an information split into different substrings.
        /// </summary>
        /// <param name="messages">The different strings to emphisize separately in the log entry.</param>
        public void Log(params string[] messages)
        {
            if (messages == null)
            {
                throw new ArgumentNullException(nameof(messages));
            }

            string message = messages.Aggregate((s1, s2) => $"{s1}{Separator}{s2}");
            string logEntry = $"{DateTime.Now.ToShortDateString()}-{DateTime.Now.ToShortTimeString()}:{Separator}{message}";

            try
            {
                if (!EventLog.SourceExists(this.sourceName))
                {
                    EventLog.CreateEventSource(this.sourceName, LogSectionName);
                }
            }
            catch (Exception e)
            {
                throw new LoggingException($"An error occurred while creating source {this.sourceName} in {LogSectionName} in the event viewer", e);
            }

            try
            {
                EventLog.WriteEntry(this.sourceName, logEntry, MapToEventViewerEventType(level));
            }
            catch (Exception e)
            {
                throw new LoggingException("An error occurred while logging in the event viewer", e);
            }
        }

        private static EventLogEntryType MapToEventViewerEventType(LogEntryLevel level)
        {
            switch (level)
            {
                case LogEntryLevel.Error: return EventLogEntryType.Error;
                case LogEntryLevel.Information: return EventLogEntryType.Information;
                case LogEntryLevel.Warning: return EventLogEntryType.Warning;
            }

            return EventLogEntryType.Information;
        }

        #region Types

        /// <summary>
        /// The type of event to log.
        /// </summary>
        public enum LogEntryLevel
        {
            Information,
            Warning,
            Error
        }

        #endregion
    }
}

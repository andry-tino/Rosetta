/// <summary>
/// BufferedFileLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Logs information into a file.
    /// </summary>
    public class BufferedFileLogger : FileLogger, IDisposable
    {
        private List<string> logEntries;

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferedFileLogger"/> class.
        /// </summary>
        /// <param name="path">The path of the file to create, including the file name.</param>
        public BufferedFileLogger(string path) 
            : base(path)
        {
            this.logEntries = new List<string>();
        }

        /// <summary>
        /// Disposes the object and forces the buffer to flush.
        /// </summary>
        public void Dispose()
        {
            this.Flush();

            // CLear in-memory entries
            this.logEntries.Clear();
        }

        protected override void WriteEntry(string logEntry)
        {
            this.logEntries.Add(logEntry);
        }

        private void Flush()
        {
            try
            {
                File.WriteAllLines(this.Path, this.logEntries.ToArray());
            }
            catch (Exception e)
            {
                e.LogLoggingError();
            }
        }
    }
}

/// <summary>
/// FileLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Diagnostics.Logging
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Logs information into a file.
    /// </summary>
    public class FileLogger : ILogger
    {
        private const string Separator = "\t"; // Tab

        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="path">The path of the file to create, including the file name.</param>
        public FileLogger(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.path = path;

            try
            {
                this.CreateFile();
            }
            catch (Exception e)
            {
                e.LogLoggingError();
            }
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
            string logEntry = $"[{DateTime.Now.ToShortDateString()}-{DateTime.Now.ToShortTimeString()}]{Separator}{message}";

            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception e)
            {
                e.LogLoggingError();
            }
        }

        private void CreateFile()
        {
            // If it exists already, remove it so we do not append different logging sessions
            if (File.Exists(this.path))
            {
                File.Delete(this.path);
            }

            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("Rosetta logfile");
                fs.Write(info, 0, info.Length);
            }
        }
    }
}

/// <summary>
/// FormatWriter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Helper for generating output.
    /// </summary>
    public class FormatWriter
    {
        private IFormatter formatter;
        private StringWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatWriter"/> class.
        /// </summary>
        public FormatWriter()
        {
            this.formatter = null;
            this.writer = new StringWriter();
        }

        /// <summary>
        /// Gets or sets the formatter being used.
        /// </summary>
        /// <remarks>
        /// This will cause the internal writer to be reset.
        /// </remarks>
        public IFormatter Formatter
        {
            get
            {
                if (this.formatter == null)
                {
                    this.formatter = new WhiteSpaceFormatter();
                    this.writer = new StringWriter();
                }

                return this.formatter;
            }

            set { this.formatter = value; }
        }

        /// <summary>
        /// Writes a line by replacing the syntax for a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="format">The format pattern.</param>
        /// <param name="arg">The replacing arguments.</param>
        public void WriteLine(string format, params string[] arg)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            // Argument `format` will contain a format-pattern
            // thus, it is possible that the formatting fails because of curly brackets
            // like in `public void MyMethod() { /* something */ }`
            //                                 +- This bracket   +
            //                                                   |- And this!
            this.writer.WriteLine(
                this.formatter.FormatLine(
                    string.Format(
                        EscapeInputFormat(format), arg)));
        }

        /// <summary>
        /// Writes a line by replacing the syntax for a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="format">The format pattern.</param>
        /// <param name="postprocessor">The action to perform before writing and after applying placeholders.</param>
        /// <param name="arg">The replacing arguments.</param>
        public void WriteLine(string format, Func<string, string> postprocessor, params string[] arg)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }
            if (postprocessor == null)
            {
                throw new ArgumentNullException(nameof(postprocessor));
            }

            StringWriter writer = new StringWriter();

            // Argument `format` will contain a format-pattern
            // thus, it is possible that the formatting fails because of curly brackets
            // like in `public void MyMethod() { /* something */ }`
            //                                 +- This bracket   +
            //                                                   |- And this!
            writer.Write(
                this.formatter.FormatLine(
                    string.Format(
                        EscapeInputFormat(format), arg)));

            this.writer.WriteLine(postprocessor(writer.ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.writer.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputFormat"></param>
        /// <returns></returns>
        private static string EscapeInputFormat(string inputFormat)
        {
            Regex regex = new Regex(@"\{\d+\}"); // A `{` followed by a number and a `}`
            string result = inputFormat;

            MatchCollection matches = regex.Matches(inputFormat);
            List<string> parts = new List<string>();
            parts.Add(inputFormat);

            foreach (Match match in matches)
            {
                parts.AddRange(parts.Last().Split(new string[] { match.Value }, StringSplitOptions.None));
            }

            // We might have no placeholders, thus parts contains one element only 
            // which we must escape before returning
            if (parts.Count == 1)
            {
                return EscapeCleanString(parts[0]);
            }

            // Remove first and last
            parts.RemoveAt(0);
            parts.RemoveAt(parts.Count - 1);
            // Remove all even positions (in even positions we have correct 
            // placeholders that should not be escaped)
            parts = parts.Where((c, i) => i % 2 == 0).ToList();

            // Escape each element in the sequence
            List<string> escapedParts = new List<string>();

            foreach (string part in parts)
            {
                escapedParts.Add(EscapeCleanString(part));
            }

            // Aggregate back
            string aggregate = escapedParts[0];

            for (int i = 0; i < matches.Count - 1; i++)
            {
                aggregate += matches[i].Value + escapedParts[i + 1];
            }
            aggregate += matches[matches.Count - 1].Value;

            return aggregate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cleanString"></param>
        /// <returns></returns>
        public static string EscapeCleanString(string cleanString)
        {
            return cleanString.Replace("{", "{{").Replace("}", "}}");
        }
    }
}

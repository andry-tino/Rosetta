/// <summary>
/// ParameterUtils.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Utils
{
    using System;

    using Rosetta.Runner;

    /// <summary>
    /// Utils for parameter handling.
    /// </summary>
    internal static class ParameterUtils
    {
        public static string FileArgumentParameter
        {
            get { return string.Format("--{0}", Program.FileArgumentName); }
        }

        public static string FileArgumentShortParameter
        {
            get { return string.Format("-{0}", Program.FileArgumentChar); }
        }

        public static string ProjectArgumentParameter
        {
            get { return string.Format("--{0}", Program.ProjectArgumentName); }
        }

        public static string ProjectArgumentShortParameter
        {
            get { return string.Format("-{0}", Program.ProjectArgumentChar); }
        }

        public static string OutputArgumentParameter
        {
            get { return string.Format("--{0}", Program.OutputArgumentName); }
        }

        public static string OutputArgumentShortParameter
        {
            get { return string.Format("-{0}", Program.OutputArgumentChar); }
        }

        public static string FileNameArgumentParameter
        {
            get { return string.Format("--{0}", Program.FileNameArgumentName); }
        }

        public static string FileNameArgumentShortParameter
        {
            get { return string.Format("-{0}", Program.FileNameArgumentChar); }
        }

        public static string VerboseArgumentParameter
        {
            get { return string.Format("--{0}", Program.VerboseArgumentName); }
        }

        public static string VerboseArgumentShortParameter
        {
            get { return string.Format("-{0}", Program.VerboseArgumentChar); }
        }

        public static string HelpArgumentParameter
        {
            get { return string.Format("--{0}", Program.HelpArgumentName); }
        }

        public static string HelpArgumentShortParameter
        {
            get { return string.Format("-{0}", Program.HelpArgumentChar); }
        }
    }
}

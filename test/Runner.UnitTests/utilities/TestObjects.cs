/// <summary>
/// TestObjects.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Utils
{
    using System;
    using System.IO;

    /// <summary>
    /// Test objects available for these tests.
    /// </summary>
    internal static class TestObjects
    {
        private static string emptyFile = "EmptyFile.testobject.cs";
        private static string notExistingFile = "WrongFile.testobject.cs";

        private static string baseFolder = "test-objects";

        /// <summary>
        /// 
        /// </summary>
        public static string EmptyFile
        {
            get { return Path.Combine(baseFolder, emptyFile); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string NotExistingFile
        {
            get { return Path.Combine(baseFolder, notExistingFile); }
        }
    }
}

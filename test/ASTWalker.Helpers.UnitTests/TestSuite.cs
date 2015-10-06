/// <summary>
/// TestSuite.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ASTWalker.Helpers.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Tests.Data;
    
    public class TestSuite
    {
        /// <summary>
        /// Simple class.
        /// </summary>
        public static string Class1 { get; set; }

        /// <summary>
        /// Class with base class.
        /// </summary>
        public static string Class2 { get; set; }

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Class1 = SourceGenerator.Generate();
            Class2 = SourceGenerator.Generate(SourceOptions.HasInheritance);
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
        }
    }
}

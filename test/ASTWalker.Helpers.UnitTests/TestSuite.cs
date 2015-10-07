/// <summary>
/// TestSuite.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ASTWalker.Helpers.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Tests.Data;
    
    public class TestSuite
    {
        /// <summary>
        /// Simple class.
        /// </summary>
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> Class1 { get; private set; }

        /// <summary>
        /// Class with base class.
        /// </summary>
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> Class2 { get; private set; }

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

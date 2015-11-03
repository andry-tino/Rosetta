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
    
    [TestClass]
    public class TestSuite
    {
        /// <summary>
        /// Simple class.
        /// </summary>
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> Class1 { get; private set; }

        /// <summary>
        /// Simple class with empty methods.
        /// </summary>
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> Class11 { get; private set; }

        /// <summary>
        /// Class with base class.
        /// </summary>
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> Class2 { get; private set; }

        /// <summary>
        /// Class with interface.
        /// </summary>
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> Class3 { get; private set; }

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Class1 = SourceGenerator.Generate();
            Class11 = SourceGenerator.Generate(SourceOptions.EmptyElements);
            Class2 = SourceGenerator.Generate(SourceOptions.HasInheritance);
            Class3 = SourceGenerator.Generate(SourceOptions.ImplementsInterfaces);
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
        }
    }
}

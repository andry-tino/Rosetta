/// <summary>
/// ClassGenerator.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests
{
    using System;

    /// <summary>
    /// Generate classes.
    /// 
    /// TODO: Refactor this whole component in order to use CodeAnalysis 
    /// and Roslyn to generate the C# code!
    /// </summary>
    internal partial class ClassGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        public ClassGenerator()
        {
            this.Name = "ExampleClass";
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}

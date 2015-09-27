/// <summary>
/// SourceGenerator.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests
{
    using System;

    /// <summary>
    /// Utility class for tokens.
    /// </summary>
    public static class SourceGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        public static string SimpleProgram
        {
            get
            {
                return new ClassGenerator()
                {
                    Name = ""
                }.VerySimpleClass;
            }
        }
    }
}

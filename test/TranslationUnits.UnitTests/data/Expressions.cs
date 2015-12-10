/// <summary>
/// Expressions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests.Data
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// 
    /// </summary>
    internal static class Expressions
    {
        /// <summary>
        /// 
        /// </summary>
        public static ITranslationUnit RandomIntegerLiteral
        {
            get { return LiteralTranslationUnit<int>.Create(RandomInteger); }
        }
        
        private static int RandomInteger
        {
            get { return new Random(0).Next(0, 1000); }
        }
    }
}

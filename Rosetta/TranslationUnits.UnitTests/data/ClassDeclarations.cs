/// <summary>
/// ClassDeclarations.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.UnitTests.Data
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// 
    /// </summary>
    internal static class ClassDeclarations
    {
        /// <summary>
        /// 
        /// </summary>
        public static ITranslationUnit SimpleClassDeclaration
        {
            get
            {
                return ClassDeclarationTranslationUnit.Create(VisibilityToken.Public, "SimpleClass", null);
            }
        }
    }
}

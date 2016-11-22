/// <summary>
/// EnumDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.AST.Factories;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class EnumDefinitionTranslationUnitFactory : EnumTranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public EnumDefinitionTranslationUnitFactory(CSharpSyntaxNode node)
            : base(node)
        {
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="EnumTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(VisibilityToken visibility, ITranslationUnit name)
        {
            return EnumDefinitionTranslationUnit.Create(visibility, name);
        }
    }
}

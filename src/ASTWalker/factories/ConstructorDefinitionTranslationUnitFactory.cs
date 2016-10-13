/// <summary>
/// ConstructorDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class ConstructorDefinitionTranslationUnitFactory : ConstructorDeclarationTranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public ConstructorDefinitionTranslationUnitFactory(CSharpSyntaxNode node) 
            : base(node)
        {
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="ConstructorDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(VisibilityToken visibility, ITranslationUnit name)
        {
            return ConstructorDefinitionTranslationUnit.Create(visibility, name);
        }
    }
}

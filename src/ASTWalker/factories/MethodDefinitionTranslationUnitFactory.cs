/// <summary>
/// MethodDefinitionTranslationUnitFactory.cs
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
    public class MethodDefinitionTranslationUnitFactory : MethodDeclarationTranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public MethodDefinitionTranslationUnitFactory(CSharpSyntaxNode node) 
            : base(node)
        {
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="MethodSignatureDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit returnType, ITranslationUnit name)
        {
            return MethodDefinitionTranslationUnit.Create(visibility, returnType, name);
        }
    }
}

/// <summary>
/// InterfaceDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.AST.Factories;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class InterfaceDefinitionTranslationUnitFactory : InterfaceDeclarationTranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public InterfaceDefinitionTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="InterfaceDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public InterfaceDefinitionTranslationUnitFactory(InterfaceDefinitionTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="InterfaceDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(ModifierTokens visibility, ITranslationUnit name)
        {
            return InterfaceDefinitionTranslationUnit.Create(visibility, name);
        }
    }
}

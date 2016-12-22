/// <summary>
/// ModuleDefinitionTranslationUnitFactory.cs
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
    public class ModuleDefinitionTranslationUnitFactory : ModuleTranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public ModuleDefinitionTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ModuleDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ModuleDefinitionTranslationUnitFactory(ModuleDefinitionTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="ModuleTranslationUnit"/>.
        /// </remarks>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(ITranslationUnit name)
        {
            return ModuleDefinitionTranslationUnit.Create(name);
        }
    }
}

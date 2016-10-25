/// <summary>
/// ModuleDefinitionTranslationUnitFactory.cs
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
    public class ModuleDefinitionTranslationUnitFactory : ModuleTranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public ModuleDefinitionTranslationUnitFactory(CSharpSyntaxNode node)
            : base(node)
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

/// <summary>
/// ModuleTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Translation;

    /// <summary>
    /// Factory for <see cref="ProgramTranslationUnit"/>.
    /// </summary>
    public class ProgramTranslationUnitFactory : ITranslationUnitFactory
    {
        // TODO: Create common base class for all translation unit factories

        private readonly CSharpSyntaxNode node;
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public ProgramTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Creates a <see cref="ProgramTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="ProgramTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            return ProgramTranslationUnit.Create();
        }
    }
}

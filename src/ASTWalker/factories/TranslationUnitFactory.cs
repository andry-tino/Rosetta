/// <summary>
/// TranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Factory for <see cref="ClassDeclarationTranslationUnit"/>.
    /// </summary>
    public abstract class TranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="semanticModel">The semantic model.</param>
        public TranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="TranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public TranslationUnitFactory(TranslationUnitFactory other)
        {
            this.node = other.node;
            this.semanticModel = other.semanticModel;
        }

        protected CSharpSyntaxNode Node => this.node;

        protected SemanticModel SemanticModel => this.semanticModel;
    }
}

/// <summary>
/// ModuleTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Factory for <see cref="ProgramTranslationUnit"/>.
    /// </summary>
    public class ProgramTranslationUnitFactory : ITranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public ProgramTranslationUnitFactory(CSharpSyntaxNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
        }

        /// <summary>
        /// Creates a <see cref="ProgramTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="ProgramTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            return ProgramTranslationUnit.Create(); ;
        }
    }
}

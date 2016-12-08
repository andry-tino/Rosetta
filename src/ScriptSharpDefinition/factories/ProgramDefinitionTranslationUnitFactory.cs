/// <summary>
/// ProgramDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.AST.Factories;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class ProgramDefinitionTranslationUnitFactory : ProgramTranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public ProgramDefinitionTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ProgramDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ProgramDefinitionTranslationUnitFactory(ProgramDefinitionTranslationUnitFactory other) 
            : base(other)
        {
        }
    }
}

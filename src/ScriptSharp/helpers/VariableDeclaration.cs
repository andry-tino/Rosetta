/// <summary>
/// VariableDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing interface in AST
    /// </summary>
    public class VariableDeclaration : Rosetta.AST.Helpers.VariableDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public VariableDeclaration(VariableDeclarationSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public VariableDeclaration(VariableDeclarationSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the type of the variable.
        /// </summary>
        /// <remarks>
        /// Might be null.
        /// </remarks>
        public override Rosetta.AST.Helpers.TypeReference Type => new Rosetta.ScriptSharp.AST.Helpers.TypeReference(this.VariableDeclarationSyntaxNode.Type, this.SemanticModel);
    }
}

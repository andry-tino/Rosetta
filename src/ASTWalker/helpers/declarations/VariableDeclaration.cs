/// <summary>
/// VariableDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing interface in AST
    /// </summary>
    public class VariableDeclaration : Helper
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
        /// Gets the name of the variable.
        /// </summary>
        public string[] Names
        {
            get
            {
                return this.VariableDeclarationSyntaxNode.Variables.Select(
                    variableDeclarator => variableDeclarator.Identifier.ValueText).ToArray();
            }
        }

        /// <summary>
        /// Gets the type of the variable.
        /// </summary>
        /// <remarks>
        /// Might be null.
        /// </remarks>
        public string Type
        {
            get { return this.SemanticModel.GetSymbolInfo(this.VariableDeclarationSyntaxNode.Type).Symbol.Name; }
        }

        /// <summary>
        /// Gets the expression representing the assignment values.
        /// </summary>
        /// <remarks>
        /// Elements in the array might be null.
        /// </remarks>
        public ExpressionSyntax[] Expressions
        {
            get
            {
                return this.VariableDeclarationSyntaxNode.Variables.Select(
                    variableDeclarator => variableDeclarator.Initializer == null ? 
                    null : variableDeclarator.Initializer.Value).ToArray();
            }
        }

        private VariableDeclarationSyntax VariableDeclarationSyntaxNode
        {
            get { return this.syntaxNode as VariableDeclarationSyntax; }
        }
    }
}

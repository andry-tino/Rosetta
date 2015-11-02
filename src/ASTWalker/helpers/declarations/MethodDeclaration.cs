/// <summary>
/// MethodDeclaration.cs
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
    /// Helper for accessing class in AST
    /// </summary>
    internal class MethodDeclaration : Helper
    {
        // Cached values
        private IEnumerable<TypedIdentifier> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclaration"/> class.
        /// </summary>
        /// <param name="methodDeclarationNode"></param>
        public MethodDeclaration(MethodDeclarationSyntax methodDeclarationNode) 
            : this(methodDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclaration"/> class.
        /// </summary>
        /// <param name="methodDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public MethodDeclaration(MethodDeclarationSyntax methodDeclarationNode, SemanticModel semanticModel) 
            : base(methodDeclarationNode, semanticModel)
        {
            this.parameters = null;
        }

        /// <summary>
        /// Gets the visibility associated with the type.
        /// </summary>
        public VisibilityToken Visibility
        {
            get { return Modifiers.Get(this.MethodDeclarationSyntaxNode.Modifiers); }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name
        {
            get { return this.MethodDeclarationSyntaxNode.Identifier.ValueText; }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string ReturnType
        {
            get
            {
                var simpleNameSyntaxNode = this.MethodDeclarationSyntaxNode.ReturnType as SimpleNameSyntax;
                return simpleNameSyntaxNode != null ?
                    simpleNameSyntaxNode.Identifier.ValueText :
                    this.MethodDeclarationSyntaxNode.ReturnType.ToString();
            }
        }

        /// <summary>
        /// Gets the list of parameters.
        /// </summary>
        public IEnumerable<TypedIdentifier> Parameters
        {
            get
            {
                if (this.parameters == null)
                {
                    this.parameters = this.MethodDeclarationSyntaxNode.ParameterList.Parameters.Select(
                        (ParameterSyntax p) => new TypedIdentifier(p)).ToList();
                }

                return this.parameters;
            }
        }

        private MethodDeclarationSyntax MethodDeclarationSyntaxNode
        {
            get { return this.syntaxNode as MethodDeclarationSyntax; }
        }
    }
}

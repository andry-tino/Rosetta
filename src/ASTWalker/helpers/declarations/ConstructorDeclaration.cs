/// <summary>
/// ConstructorDeclaration.cs
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
    /// Helper for accessing constructors in AST.
    /// TODO: Evaluate possibility to inherit from <see cref="MetrhodDeclaration"/>.
    /// </summary>
    public class ConstructorDeclaration : Helper
    {
        // Cached values
        private IEnumerable<Parameter> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclaration"/> class.
        /// </summary>
        /// <param name="constructorDeclarationNode"></param>
        public ConstructorDeclaration(ConstructorDeclarationSyntax constructorDeclarationNode)
            : this(constructorDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclaration"/> class.
        /// </summary>
        /// <param name="constructorDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public ConstructorDeclaration(ConstructorDeclarationSyntax constructorDeclarationNode, SemanticModel semanticModel)
            : base(constructorDeclarationNode, semanticModel)
        {
            this.parameters = null;
        }

        /// <summary>
        /// Gets the visibility associated with the type.
        /// </summary>
        public VisibilityToken Visibility
        {
            get { return Modifiers.Get(this.ConstructorDeclarationSyntaxNode.Modifiers); }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name
        {
            get { return this.ConstructorDeclarationSyntaxNode.Identifier.ValueText; }
        }

        /// <summary>
        /// Gets the list of parameters.
        /// </summary>
        public IEnumerable<Parameter> Parameters
        {
            get
            {
                if (this.parameters == null)
                {
                    this.parameters = this.ConstructorDeclarationSyntaxNode.ParameterList.Parameters.Select(
                        (ParameterSyntax p) => new Parameter(p)).ToList();
                }

                return this.parameters;
            }
        }

        private ConstructorDeclarationSyntax ConstructorDeclarationSyntaxNode
        {
            get { return this.SyntaxNode as ConstructorDeclarationSyntax; }
        }
    }
}

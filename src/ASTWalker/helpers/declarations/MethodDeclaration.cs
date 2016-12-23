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
    /// Helper for accessing methods in AST
    /// </summary>
    public class MethodDeclaration : Helper
    {
        // Cached values
        private IEnumerable<Parameter> parameters;

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
        public virtual string Name => this.MethodDeclarationSyntaxNode.Identifier.ValueText;

        /// <summary>
        /// Gets the return type.
        /// </summary>
        public TypeReference ReturnType => this.CreateTypeReferenceHelper(this.MethodDeclarationSyntaxNode.ReturnType, this.SemanticModel);

        /// <summary>
        /// Gets the list of parameters.
        /// </summary>
        public IEnumerable<Parameter> Parameters
        {
            get
            {
                if (this.parameters == null)
                {
                    this.parameters = this.MethodDeclarationSyntaxNode.ParameterList.Parameters.Select(
                        p => this.CreateParameterHelper(p, this.SemanticModel)).ToList();
                }

                return this.parameters;
            }
        }

        protected MethodDeclarationSyntax MethodDeclarationSyntaxNode
        {
            get { return this.SyntaxNode as MethodDeclarationSyntax; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        protected virtual TypeReference CreateTypeReferenceHelper(TypeSyntax node, SemanticModel semanticModel)
        {
            return new TypeReference(node, semanticModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        protected virtual Parameter CreateParameterHelper(ParameterSyntax node, SemanticModel semanticModel)
        {
            return new Parameter(node, semanticModel);
        }
    }
}

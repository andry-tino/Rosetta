/// <summary>
/// MethodDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;

    /// <summary>
    /// Helper for methods.
    /// </summary>
    public class MethodDeclaration : Rosetta.AST.Helpers.MethodDeclaration
    {
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
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public override string Name => this.ShouldPreserveName ? base.Name : base.Name.ToScriptSharpName();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        protected override Rosetta.AST.Helpers.TypeReference CreateTypeReferenceHelper(TypeSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.AST.Helpers.TypeReference(node, semanticModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        protected override Rosetta.AST.Helpers.Parameter CreateParameterHelper(ParameterSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.AST.Helpers.Parameter(node, semanticModel);
        }

        private bool ShouldPreserveName
        {
            get
            {
                var attributes = new AttributeLists(this.MethodDeclarationSyntaxNode).Attributes;
                foreach (var attribute in attributes)
                {
                    if (PreserveNameAttributeDecoration.IsPreserveNameAttributeDecoration(attribute))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}

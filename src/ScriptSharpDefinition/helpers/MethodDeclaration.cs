/// <summary>
/// MethodDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        protected override Rosetta.AST.Helpers.TypeReference CreateTypeReferenceHelper(TypeSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.Definition.AST.Helpers.TypeReference(node, semanticModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        protected override Rosetta.AST.Helpers.Parameter CreateParameterHelper(ParameterSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.Definition.AST.Helpers.Parameter(node, semanticModel);
        }
    }
}

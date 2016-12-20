/// <summary>
/// ConstructorDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for constructor.
    /// </summary>
    public class ConstructorDeclaration : Rosetta.AST.Helpers.ConstructorDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclaration"/> class.
        /// </summary>
        /// <param name="methodDeclarationNode"></param>
        public ConstructorDeclaration(ConstructorDeclarationSyntax ctorDeclarationNode)
            : this(ctorDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclaration"/> class.
        /// </summary>
        /// <param name="methodDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public ConstructorDeclaration(ConstructorDeclarationSyntax ctorDeclarationNode, SemanticModel semanticModel)
            : base(ctorDeclarationNode, semanticModel)
        {
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

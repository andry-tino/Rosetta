/// <summary>
/// BaseTypeReference.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    ///Decorates <see cref="AttributeDecoration"/>.
    /// </summary>
    public class ClassDeclaration : Rosetta.AST.Helpers.ClassDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclaration"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        public ClassDeclaration(ClassDeclarationSyntax classDeclarationNode)
            : this(classDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclaration"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public ClassDeclaration(ClassDeclarationSyntax classDeclarationNode, SemanticModel semanticModel)
            : base(classDeclarationNode, semanticModel)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <param name="typeKind"></param>
        /// <returns></returns>
        protected override Rosetta.AST.Helpers.BaseTypeReference CreateBaseTypeReferenceHelper(BaseTypeSyntax node, SemanticModel semanticModel, TypeKind typeKind)
        {
            return new Rosetta.ScriptSharp.Definition.AST.Helpers.BaseTypeReference(node, semanticModel, typeKind);
        }
    }
}

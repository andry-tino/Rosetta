/// <summary>
/// ClassDefinitionASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;
    using Rosetta.AST.Factories;

    /// <summary>
    /// Walks a class AST node but only those areas relevant to definition.
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class ClassDefinitionASTWalker : ClassASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="classDefinition"></param>
        protected ClassDefinitionASTWalker(CSharpSyntaxNode node, ClassDefinitionTranslationUnit classDefinition) 
            : base(node, classDefinition)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ClassDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ClassDefinitionASTWalker(ClassDefinitionASTWalker other) 
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="ClassASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static ClassDefinitionASTWalker Create(CSharpSyntaxNode node)
        {
            return new ClassDefinitionASTWalker(node, 
                new ClassDefinitionTranslationUnitFactory(node).Create() as ClassDefinitionTranslationUnit);
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            var fieldDefinitionTranslationUnit = new FieldDefinitionTranslationUnitFactory(node).Create();
            this.classDeclaration.AddMemberDeclaration(fieldDefinitionTranslationUnit);

            this.InvokeFieldDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var propertyDefinitionTranslationUnit = new PropertyDeclarationTranslationUnitFactory(node).Create();
            this.classDeclaration.AddPropertyDeclaration(propertyDefinitionTranslationUnit);
            
            this.InvokePropertyDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// This will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var methodDefinitionTranslationUnit = new MethodDefinitionTranslationUnitFactory(node).Create();
            this.classDeclaration.AddMethodDeclaration(methodDefinitionTranslationUnit);

            this.InvokeMethodDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var constructorDefinitionTranslationUnit = new ConstructorDefinitionTranslationUnitFactory(node).Create();
            this.classDeclaration.AddConstructorDeclaration(constructorDefinitionTranslationUnit);

            this.InvokeConstructorDeclarationVisited(this, new WalkerEventArgs());
        }

        #endregion
    }
}

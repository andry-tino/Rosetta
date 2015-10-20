/// <summary>
/// ClassASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Walks a class AST node.
    /// </summary>
    public class ClassASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        private CSharpSyntaxNode node;

        private ClassDeclarationTranslationUnit classDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassASTWalker"/> class.
        /// </summary>
        private ClassASTWalker(CSharpSyntaxNode node)
        {
            var classDeclarationSyntaxNode = node as ClassDeclarationSyntax;
            if (classDeclarationSyntaxNode == null)
            {
                throw new ArgumentException("Specified node is not of type ClassDeclarationSyntax");
            }

            this.node = node;
            ClassDeclaration classHelper = new ClassDeclaration(classDeclarationSyntaxNode);

            this.classDeclaration = ClassDeclarationTranslationUnit.Create(
                classHelper.Visibility, 
                classHelper.Name,
                classHelper.BaseClass == null ? null : classHelper.BaseClass.Name);

            foreach (BaseTypeReference implementedInterface in classHelper.ImplementedInterfaces)
            {
                this.classDeclaration.AddImplementedInterfaceName(implementedInterface.Name);
            }
        }

        /// <summary>
        /// Factory method for class <see cref="ClassASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static ClassASTWalker Create(CSharpSyntaxNode node)
        {
            return new ClassASTWalker(node);
        }

        /// <summary>
        /// Walk the whole tree starting from specified <see cref="CSharpSyntaxNode"/> and 
        /// build the translation unit tree necessary for generating TypeScript output.
        /// </summary>
        /// <returns>The root of the translation unit tree.</returns>
        public ITranslationUnit Walk()
        {
            // Visiting
            this.Visit(node);

            // Returning root
            return this.classDeclaration;
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            base.VisitFieldDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            base.VisitPropertyDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            base.VisitVariableDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            base.VisitMethodDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            base.VisitConstructorDeclaration(node);
        }

        #endregion
    }
}

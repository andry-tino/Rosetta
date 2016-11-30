/// <summary>
/// EnumASTWalker.cs
/// Andrea Tino - 2016
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
    using Rosetta.AST.Factories;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Walks an interface AST node.
    /// </summary>
    public class EnumASTWalker : ASTWalker, IASTWalker
    {
        // Protected for testability
        protected EnumTranslationUnit enumDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected EnumASTWalker(CSharpSyntaxNode node, EnumTranslationUnit enumDeclaration, SemanticModel semanticModel)
            : base(node, semanticModel)
        {
            var enumSyntaxNode = node as EnumDeclarationSyntax;
            if (enumSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(EnumDeclarationSyntax).Name));
            }

            if (enumDeclaration == null)
            {
                throw new ArgumentNullException(nameof(enumDeclaration));
            }

            this.enumDeclaration = enumDeclaration;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="EnumASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public EnumASTWalker(EnumASTWalker other)
            : base(other)
        {
            this.enumDeclaration = other.enumDeclaration;
        }

        /// <summary>
        /// Factory method for class <see cref="EnumASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static EnumASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null, SemanticModel semanticModel = null)
        {
            return new EnumASTWalker(
                node,
                new EnumTranslationUnitFactory(node, semanticModel).Create() as EnumTranslationUnit,
                semanticModel)
            {
                Context = context
            };
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
            return this.enumDeclaration;
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            var enumMemberTranslationUnit = new EnumMemberTranslationUnitFactory(node, this.semanticModel).Create();
            this.enumDeclaration.AddMember(enumMemberTranslationUnit);

            this.InvokeEnumMemberVisited(this, new WalkerEventArgs());
        }

        #endregion

        #region Walk events

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent EnumMemberVisited;

        #endregion

        protected void InvokeEnumMemberVisited(object sender, WalkerEventArgs e)
        {
            if (this.EnumMemberVisited != null)
            {
                this.EnumMemberVisited(sender, e);
            }
        }
    }
}

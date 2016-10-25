/// <summary>
/// InterfaceASTWalker.cs
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
    /// Walks an interface AST node.
    /// </summary>
    public class InterfaceASTWalker : ASTWalker, IASTWalker
    {
        // Protected for testability
        protected InterfaceDeclarationTranslationUnit interfaceDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="interfaceDeclaration"></param>
        protected InterfaceASTWalker(CSharpSyntaxNode node, InterfaceDeclarationTranslationUnit interfaceDeclaration) 
            : base(node)
        {
            var interfaceDeclarationSyntaxNode = node as InterfaceDeclarationSyntax;
            if (interfaceDeclarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(InterfaceDeclarationSyntax).Name));
            }

            if (interfaceDeclaration == null)
            {
                throw new ArgumentNullException(nameof(interfaceDeclaration));
            }

            this.interfaceDeclaration = interfaceDeclaration;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="InterfaceASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public InterfaceASTWalker(InterfaceASTWalker other) 
            : base(other)
        {
            this.interfaceDeclaration = other.interfaceDeclaration;
        }

        /// <summary>
        /// Factory method for class <see cref="InterfaceASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <returns></returns>
        public static InterfaceASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null)
        {
            InterfaceDeclaration helper = new InterfaceDeclaration(node as InterfaceDeclarationSyntax);

            var interfaceDeclaration = InterfaceDeclarationTranslationUnit.Create(
                helper.Visibility,
                IdentifierTranslationUnit.Create(helper.Name));

            foreach (BaseTypeReference implementedInterface in helper.ExtendedInterfaces)
            {
                interfaceDeclaration.AddExtendedInterface(IdentifierTranslationUnit.Create(implementedInterface.Name));
            }

            return new InterfaceASTWalker(node, interfaceDeclaration)
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
            return this.interfaceDeclaration;
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// We don't need to go further deeper in the tree by visiting the node.
        /// </remarks>
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var helper = new PropertyDeclaration(node);

            // Properties in TypeScript will be translated as methods as 
            // TypeScript does not support properties in interfaces
            var translationUnit = MethodSignatureDeclarationTranslationUnit.Create(
                helper.Visibility, TypeIdentifierTranslationUnit.Create(helper.Type), IdentifierTranslationUnit.Create(helper.Name));
            
            this.interfaceDeclaration.AddSignature(translationUnit);

            this.InvokePropertyDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// We don't need to go further deeper in the tree by visiting the node.
        /// </remarks>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var helper = new MethodDeclaration(node);

            // Properties in TypeScript will be translated as methods as 
            // TypeScript does not support properties in interfaces
            var translationUnit = MethodSignatureDeclarationTranslationUnit.Create(
                helper.Visibility, TypeIdentifierTranslationUnit.Create(helper.ReturnType), IdentifierTranslationUnit.Create(helper.Name));

            this.interfaceDeclaration.AddSignature(translationUnit);

            this.InvokePropertyDeclarationVisited(this, new WalkerEventArgs());
        }

        #endregion

        #region Walk events
        
        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent PropertyDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent MethodDeclarationVisited;
        
        #endregion
        
        private void InvokePropertyDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.PropertyDeclarationVisited != null)
            {
                this.PropertyDeclarationVisited(sender, e);
            }
        }

        private void InvokeMethodDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.MethodDeclarationVisited != null)
            {
                this.MethodDeclarationVisited(sender, e);
            }
        }
    }
}

/// <summary>
/// InterfaceDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Factories
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Factory for generating a <see cref="InterfaceDeclarationSyntax"/>.
    /// </summary>
    public class InterfaceDeclarationSyntaxFactory : ISyntaxFactory
    {
        private readonly ITypeInfoProxy interfaceInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="classInfo"></param>
        public InterfaceDeclarationSyntaxFactory(ITypeInfoProxy interfaceInfo)
        {
            if (interfaceInfo == null)
            {
                throw new ArgumentNullException(nameof(interfaceInfo));
            }

            this.interfaceInfo = interfaceInfo;
        }

        /// <summary>
        /// Creates the <see cref="InterfaceDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public SyntaxNode Create()
        {
            var interfaceNode = SyntaxFactory.InterfaceDeclaration(this.InterfaceInfo.Name);

            // Defining accessibility
            interfaceNode = this.HandleAccessibility(interfaceNode);

            // Extended interfaces
            interfaceNode = this.HandleExtendedInterfaces(interfaceNode);

            // Methods
            interfaceNode = this.HandleMethods(interfaceNode);

            // Properties
            // TODO

            return interfaceNode;
        }

        protected ITypeInfoProxy InterfaceInfo => this.interfaceInfo;

        private InterfaceDeclarationSyntax HandleAccessibility(InterfaceDeclarationSyntax node)
        {
            var newNode = node;

            var visibility = new Visibility(this.InterfaceInfo).Token;
            if (visibility != SyntaxKind.None)
            {
                newNode = newNode.AddModifiers(SyntaxFactory.Token(visibility));
            }

            return newNode;
        }

        private InterfaceDeclarationSyntax HandleExtendedInterfaces(InterfaceDeclarationSyntax node)
        {
            var newNode = node;

            var interfaces = this.InterfaceInfo.ImplementedInterfaces;

            if (interfaces != null)
            {
                foreach (var @interface in interfaces)
                {
                    newNode = newNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(
                        SyntaxFactory.ParseTypeName(this.GetInterfaceFullName(@interface))));
                }
            }

            return newNode;
        }

        private InterfaceDeclarationSyntax HandleMethods(InterfaceDeclarationSyntax node)
        {
            var newNode = node;

            var methods = this.InterfaceInfo.DeclaredMethods;

            if (methods != null)
            {
                foreach (var method in methods)
                {
                    newNode = newNode.AddMembers(this.CreateMethodDeclarationSyntaxFactory(method).Create() as MethodDeclarationSyntax);
                }
            }

            return newNode;
        }

        protected virtual string GetInterfaceFullName(ITypeProxy type) => type.FullName;

        protected virtual ISyntaxFactory CreateMethodDeclarationSyntaxFactory(IMethodInfoProxy methodInfo)
            => new MethodDeclarationSyntaxFactory(methodInfo, false);
    }
}

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
            var interfaceNode = SyntaxFactory.InterfaceDeclaration(this.interfaceInfo.Name);

            // Defining accessibility
            var visibility = new Visibility(this.interfaceInfo).Token;
            if (visibility != SyntaxKind.None)
            {
                interfaceNode = interfaceNode.AddModifiers(SyntaxFactory.Token(visibility));
            }

            // Extended interfaces
            var interfaces = this.interfaceInfo.ImplementedInterfaces;

            if (interfaces != null)
            {
                foreach (var @interface in interfaces)
                {
                    interfaceNode = interfaceNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(@interface.FullName)));
                }
            }

            // Methods
            var methods = this.interfaceInfo.DeclaredMethods;

            if (methods != null)
            {
                foreach (var method in methods)
                {
                    interfaceNode = interfaceNode.AddMembers(new MethodDeclarationSyntaxFactory(method, false).Create() as MethodDeclarationSyntax);
                }
            }

            // Properties
            // TODO

            return interfaceNode;
        }
    }
}

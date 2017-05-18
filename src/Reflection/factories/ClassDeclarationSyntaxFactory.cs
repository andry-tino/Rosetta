/// <summary>
/// ClassDeclarationSyntaxFactory.cs
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
    /// Factory for generating a <see cref="ClassDeclarationSyntax"/>.
    /// </summary>
    public class ClassDeclarationSyntaxFactory : ISyntaxFactory
    {
        private readonly ITypeInfoProxy classInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="classInfo"></param>
        public ClassDeclarationSyntaxFactory(ITypeInfoProxy classInfo)
        {
            if (classInfo == null)
            {
                throw new ArgumentNullException(nameof(classInfo));
            }

            this.classInfo = classInfo;
        }

        /// <summary>
        /// Creates the <see cref="ClassDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public SyntaxNode Create()
        {
            var classNode = SyntaxFactory.ClassDeclaration(this.classInfo.Name);

            // Defining accessibility
            var visibility = new Visibility(this.classInfo).Token;
            if (visibility != SyntaxKind.None)
            {
                classNode = classNode.AddModifiers(SyntaxFactory.Token(visibility));
            }

            // Base type
            var baseType = this.classInfo.BaseType;
            
            if (baseType != null && !(new ObjectClass(baseType).Is)) // Filter out the System.Object class
            {
                classNode = classNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseType.FullName)));
            }

            // Interface implementations
            var interfaces = this.classInfo.ImplementedInterfaces;

            if (interfaces != null)
            {
                foreach (var @interface in interfaces)
                {
                    classNode = classNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(@interface.FullName)));
                }
            }

            // Constructors
            var ctors = this.classInfo.DeclaredConstructors;

            if (ctors != null)
            {
                foreach (var ctor in ctors)
                {
                    classNode = classNode.AddMembers(new ConstructorDeclarationSyntaxFactory(ctor, this.classInfo).Create() as ConstructorDeclarationSyntax);
                }
            }

            // Methods
            var methods = this.classInfo.DeclaredMethods;

            if (methods != null)
            {
                foreach (var method in methods)
                {
                    classNode = classNode.AddMembers(new MethodDeclarationSyntaxFactory(method).Create() as MethodDeclarationSyntax);
                }
            }

            // Properties
            // TODO

            return classNode;
        }
    }
}

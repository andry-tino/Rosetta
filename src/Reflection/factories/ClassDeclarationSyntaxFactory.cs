/// <summary>
/// Visibility.cs
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
    public class ClassDeclarationSyntaxFactory
    {
        private ITypeInfoProxy classInfo;

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
        public ClassDeclarationSyntax Create()
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

            if (baseType != null)
            {
                classNode = classNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseType.FullName)));
            }

            // Interfaces
            var interfaces = this.classInfo.ImplementedInterfaces;

            if (interfaces != null)
            {
                foreach (var @interface in interfaces)
                {
                    classNode = classNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(@interface.FullName)));
                }
            }

            // Constructors
            //var ctors = type.DeclaredMethods;

            //if (ctors != null)
            //{
            //    foreach (var ctor in ctors)
            //    {
            //        classNode.AddMembers(SyntaxFactory.ConstructorDeclaration());
            //    }
            //}

            // Methods
            var methods = this.classInfo.DeclaredMethods;

            if (methods != null)
            {
                foreach (var method in methods)
                {
                    classNode = classNode.AddMembers(new MethodDeclarationSyntaxFactory(method).Create());
                }
            }

            return classNode;
        }
    }
}

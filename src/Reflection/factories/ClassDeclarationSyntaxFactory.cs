/// <summary>
/// ClassDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Factories
{
    using System;
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Factory for generating a <see cref="ClassDeclarationSyntax"/>.
    /// </summary>
    public class ClassDeclarationSyntaxFactory : ISyntaxFactory, ISyntaxFilterOut
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
        /// Gets or sets the collection of types to filter out.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> FilteredOutTypes { get; set; }

        /// <summary>
        /// Creates the <see cref="ClassDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public SyntaxNode Create()
        {
            var classNode = SyntaxFactory.ClassDeclaration(this.ClassInfo.Name);

            // Defining accessibility
            classNode = this.HandleAccessibility(classNode);

            // Base type
            classNode = this.HandleBaseType(classNode);

            // Interface implementations
            classNode = this.HandleInterfaceImplementations(classNode);

            // Constructors
            classNode = this.HandleConstructors(classNode);

            // Methods
            classNode = this.HandleMethods(classNode);

            // Properties
            classNode = this.HandleProperties(classNode);

            // Handle fields
            classNode = this.HandleFields(classNode);

            return classNode;
        }

        protected ITypeInfoProxy ClassInfo => this.classInfo;

        private ClassDeclarationSyntax HandleAccessibility(ClassDeclarationSyntax node)
        {
            var newNode = node;

            var visibility = new Visibility(this.ClassInfo).Token;
            if (visibility != SyntaxKind.None)
            {
                newNode = newNode.AddModifiers(SyntaxFactory.Token(visibility));
            }

            return newNode;
        }

        private ClassDeclarationSyntax HandleBaseType(ClassDeclarationSyntax node)
        {
            var newNode = node;

            var baseType = this.ClassInfo.BaseType;

            if (baseType != null && !(new ObjectClass(baseType).Is)) // Filter out the System.Object class
            {
                newNode = newNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(this.GetBaseTypeFullName(baseType))));
            }

            return newNode;
        }

        private ClassDeclarationSyntax HandleInterfaceImplementations(ClassDeclarationSyntax node)
        {
            var newNode = node;

            var interfaces = this.ClassInfo.ImplementedInterfaces;

            if (interfaces != null)
            {
                foreach (var @interface in interfaces)
                {
                    newNode = newNode.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(this.GetInterfaceFullName(@interface))));
                }
            }

            return newNode;
        }

        private ClassDeclarationSyntax HandleConstructors(ClassDeclarationSyntax node)
        {
            var newNode = node;

            var ctors = this.ClassInfo.DeclaredConstructors;

            if (ctors != null)
            {
                foreach (var ctor in ctors)
                {
                    newNode = newNode.AddMembers(this.CreateConstructorDeclarationSyntaxFactory(ctor).Create() as ConstructorDeclarationSyntax);
                }
            }

            return newNode;
        }

        private ClassDeclarationSyntax HandleMethods(ClassDeclarationSyntax node)
        {
            var newNode = node;

            var methods = this.ClassInfo.DeclaredMethods;

            if (methods != null)
            {
                foreach (var method in methods)
                {
                    newNode = newNode.AddMembers(this.CreateMethodDeclarationSyntaxFactory(method).Create() as MethodDeclarationSyntax);
                }
            }

            return newNode;
        }

        private ClassDeclarationSyntax HandleProperties(ClassDeclarationSyntax node)
        {
            var newNode = node;

            var properties = this.ClassInfo.DeclaredProperties;

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    newNode = newNode.AddMembers(this.CreatePropertyDeclarationSyntaxFactory(property).Create() as PropertyDeclarationSyntax);
                }
            }

            return newNode;
        }

        private ClassDeclarationSyntax HandleFields(ClassDeclarationSyntax node)
        {
            var newNode = node;

            var fields = this.ClassInfo.DeclaredFields;

            if (fields != null)
            {
                foreach (var field in fields)
                {
                    newNode = newNode.AddMembers(this.CreateFieldDeclarationSyntaxFactory(field).Create() as FieldDeclarationSyntax);
                }
            }

            return newNode;
        }

        protected virtual string GetBaseTypeFullName(ITypeProxy type) => type.FullName;

        protected virtual string GetInterfaceFullName(ITypeProxy type) => type.FullName;

        protected virtual ISyntaxFactory CreateConstructorDeclarationSyntaxFactory(IConstructorInfoProxy ctorInfo) 
            => new ConstructorDeclarationSyntaxFactory(ctorInfo, this.ClassInfo);

        protected virtual ISyntaxFactory CreateMethodDeclarationSyntaxFactory(IMethodInfoProxy methodInfo)
            => new MethodDeclarationSyntaxFactory(methodInfo);

        protected virtual ISyntaxFactory CreatePropertyDeclarationSyntaxFactory(IPropertyInfoProxy propertyInfo)
            => new PropertyDeclarationSyntaxFactory(propertyInfo);

        protected virtual ISyntaxFactory CreateFieldDeclarationSyntaxFactory(IFieldInfoProxy fieldInfo)
            => new FieldDeclarationSyntaxFactory(fieldInfo);
    }
}

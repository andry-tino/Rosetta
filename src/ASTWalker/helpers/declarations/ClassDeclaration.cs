/// <summary>
/// ClassDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing class in AST
    /// </summary>
    internal class ClassDeclaration : InheritableDeclaration
    {
        // Cached values
        private IEnumerable<TypeReference> interfaces;
        private TypeReference baseClass;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationSyntax"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        public ClassDeclaration(ClassDeclarationSyntax classDeclarationNode) 
            : base(classDeclarationNode)
        {
            this.interfaces = null;
            this.baseClass = null;
        }

        /// <summary>
        /// Gets the base class.
        /// </summary>
        /// <remarks>
        /// Value is cached.
        /// </remarks>
        public TypeReference BaseClass
        {
            get
            {
                if (this.baseClass == null)
                {
                    this.baseClass = null;
                }

                return this.baseClass;
            }
        }

        /// <summary>
        /// Gets the collection of implemented interfaces.
        /// </summary>
        /// <remarks>
        /// Value is cached.
        /// </remarks>
        public IEnumerable<TypeReference> ImplementedInterfaces
        {
            get
            {
                if (this.interfaces == null)
                {
                    this.interfaces = null;
                }

                return this.interfaces;
            }
        }

        private ClassDeclarationSyntax ClassDeclarationNode
        {
            get { return (ClassDeclarationSyntax)this.syntaxNode; }
        }
    }
}

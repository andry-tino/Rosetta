/// <summary>
/// InterfaceDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing interface in AST
    /// </summary>
    internal class InterfaceDeclaration : InheritableDeclaration
    {
        // Cached values
        private IEnumerable<TypeReference> interfaces;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationSyntax"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        public InterfaceDeclaration(InterfaceDeclarationSyntax interfaceDeclarationNode)
            : base(interfaceDeclarationNode)
        {
            this.interfaces = null;
        }

        /// <summary>
        /// Gets the collection of extended interfaces. Same as <see cref="BaseTypes"/>.
        /// </summary>
        /// <remarks>
        /// Value is cached.
        /// </remarks>
        public IEnumerable<TypeReference> ExtendedInterfaces
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

        private InterfaceDeclarationSyntax InterfaceDeclarationNode
        {
            get { return (InterfaceDeclarationSyntax)this.syntaxNode; }
        }
    }
}

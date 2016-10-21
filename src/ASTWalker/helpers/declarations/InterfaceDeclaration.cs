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
    public class InterfaceDeclaration : InheritableDeclaration
    {
        // Cached values
        private IEnumerable<BaseTypeReference> interfaces;

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
        /// TODO: Duplicate code in <see cref="ClassDeclaration"/>! Extract!
        /// </summary>
        /// <remarks>
        /// Value is cached.
        /// </remarks>
        public IEnumerable<BaseTypeReference> ExtendedInterfaces
        {
            get
            {
                if (this.interfaces == null)
                {
                    this.interfaces = new List<BaseTypeReference>();
                    foreach (BaseTypeReference baseType in this.BaseTypes)
                    {
                        if (baseType.Kind == TypeKind.Interface)
                        {
                            ((List<BaseTypeReference>)this.interfaces).Add(baseType);
                        }
                    }
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

/// <summary>
/// PropertyDeclaration.cs
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
    /// Helper for accessing properties in AST
    /// </summary>
    internal class PropertyDeclaration : Helper
    {
        // Cached values
        private bool? hasGet;
        private bool? hasSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclaration"/> class.
        /// </summary>
        /// <param name="propertyDeclarationNode"></param>
        public PropertyDeclaration(PropertyDeclarationSyntax propertyDeclarationNode)
            : this(propertyDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclaration"/> class.
        /// </summary>
        /// <param name="propertyDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public PropertyDeclaration(PropertyDeclarationSyntax propertyDeclarationNode, SemanticModel semanticModel)
            : base(propertyDeclarationNode, semanticModel)
        {
            this.hasGet = null;
            this.hasSet = null;
        }

        /// <summary>
        /// Gets the visibility associated with the property.
        /// </summary>
        public VisibilityToken Visibility
        {
            get { return Modifiers.Get(this.PropertyDeclarationSyntaxNode.Modifiers); }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this.PropertyDeclarationSyntaxNode.Identifier.ValueText; }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Type
        {
            get
            {
                var simpleNameSyntaxNode = this.PropertyDeclarationSyntaxNode.Type as SimpleNameSyntax;
                return simpleNameSyntaxNode != null ?
                    simpleNameSyntaxNode.Identifier.ValueText :
                    this.PropertyDeclarationSyntaxNode.Type.ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the property supports a get.
        /// </summary>
        public bool HasGet
        {
            get
            {
                if (!this.hasGet.HasValue)
                {
                    this.hasGet = SearchForNode(this.PropertyDeclarationSyntaxNode.AccessorList.Accessors, 
                        SyntaxKind.GetAccessorDeclaration);
                }

                return this.hasGet.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the property supports a set.
        /// </summary>
        public bool HasSet
        {
            get
            {
                if (!this.hasSet.HasValue)
                {
                    this.hasSet = SearchForNode(this.PropertyDeclarationSyntaxNode.AccessorList.Accessors,
                        SyntaxKind.SetAccessorDeclaration);
                }

                return this.hasSet.Value;
            }
        }

        private static bool SearchForNode(SyntaxList<AccessorDeclarationSyntax> accessors, SyntaxKind kind)
        {
            foreach (var accessor in accessors)
            {
                if (accessor.Kind() == kind)
                {
                    return true;
                }
            }

            return false;
        }

        private PropertyDeclarationSyntax PropertyDeclarationSyntaxNode
        {
            get { return this.syntaxNode as PropertyDeclarationSyntax; }
        }
    }
}

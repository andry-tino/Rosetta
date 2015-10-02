/// <summary>
/// Inheritable.cs
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
    /// Helper for accessing classes and interfaces in AST.
    /// </summary>
    internal abstract class Inheritable
    {
        protected CSharpSyntaxNode syntaxNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Inheritable"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        public Inheritable(CSharpSyntaxNode syntaxNode)
        {
            this.syntaxNode = syntaxNode;
        }

        /// <summary>
        /// Gets the visibility associated with the type.
        /// </summary>
        public abstract VisibilityToken Visibility
        {
            get;
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract IEnumerable<Inheritable> BaseTypes
        {
            get;
        }
    }
}

/// <summary>
/// Class.cs
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
    /// Helper for accessing class in AST
    /// </summary>
    internal class Class : Inheritable
    {
        private ClassDeclarationSyntax ClassDeclarationNode
        {
            get { return (ClassDeclarationSyntax)this.syntaxNode; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationSyntax"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        public Class(ClassDeclarationSyntax classDeclarationNode) 
            : base(classDeclarationNode)
        {
        }

        /// <summary>
        /// Gets the visibility associated with the class.
        /// </summary>
        public override VisibilityToken Visibility
        {
            get { return Modifiers.Get(this.ClassDeclarationNode.Modifiers); }
        }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        public override string Name
        {
            get { return this.ClassDeclarationNode.Identifier.ValueText; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<Inheritable> BaseTypes
        {
            get { return null; }
        }
    }
}

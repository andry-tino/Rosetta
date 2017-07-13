/// <summary>
/// ConstructorDeclarationSyntaxNodeLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST.Diagnostics.Logging
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Diagnostics.Logging;

    /// <summary>
    /// Logs operations concerning <see cref="ConstructorDeclarationSyntax"/> nodes.
    /// </summary>
    public class ConstructorDeclarationSyntaxNodeLogger : SyntaxNodeLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclarationSyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public ConstructorDeclarationSyntaxNodeLogger(ClassDeclarationSyntax parent, ConstructorDeclarationSyntax node, ILogger logger)
            : base(parent, node, logger)
        {
        }

        protected override string NodeName => new ConstructorDeclaration(this.Node as ConstructorDeclarationSyntax).Name;

        protected override string ParentNodeName => new ClassDeclaration(this.Parent as ClassDeclarationSyntax).Name;

        protected override string NodeType => "Field declaration";
    }
}

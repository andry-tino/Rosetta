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
    /// Logs operations concerning <see cref="NamespaceDeclarationSyntax"/> nodes.
    /// </summary>
    public class NamespaceDeclarationSyntaxNodeLogger : SyntaxNodeLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceDeclarationSyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public NamespaceDeclarationSyntaxNodeLogger(CompilationUnitSyntax parent, NamespaceDeclarationSyntax node, ILogger logger)
            : base(parent, node, logger)
        {
        }

        protected override string NodeName => new NamespaceDeclaration(this.Node as NamespaceDeclarationSyntax).Name;

        protected override string ParentNodeName => "(Root level)";

        protected override string NodeType => "Namespace declaration";
    }
}

/// <summary>
/// InterfaceDeclarationSyntaxNodeLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST.Diagnostics.Logging
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Diagnostics.Logging;

    /// <summary>
    /// Logs operations concerning <see cref="InterfaceDeclarationSyntax"/> nodes.
    /// </summary>
    public class InterfaceDeclarationSyntaxNodeLogger : SyntaxNodeLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclarationSyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public InterfaceDeclarationSyntaxNodeLogger(NamespaceDeclarationSyntax parent, InterfaceDeclarationSyntax node, ILogger logger)
            : base(parent, node, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclarationSyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public InterfaceDeclarationSyntaxNodeLogger(CompilationUnitSyntax parent, InterfaceDeclarationSyntax node, ILogger logger)
            : base(parent, node, logger)
        {
        }

        protected override string NodeName => new InterfaceDeclaration(this.Node as InterfaceDeclarationSyntax).Name;

        protected override string ParentNodeName
        {
            get
            {
                if (this.Parent as NamespaceDeclarationSyntax != null)
                {
                    return new NamespaceDeclaration(this.Parent as NamespaceDeclarationSyntax).Name;
                }

                if (this.Parent as CompilationUnitSyntax != null)
                {
                    return "(Root level)";
                }

                return "Unidentified";
            }
        }

        protected override string NodeType => "Interface declaration";
    }
}

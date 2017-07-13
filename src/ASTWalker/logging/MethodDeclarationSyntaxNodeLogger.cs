/// <summary>
/// MethodDeclarationSyntaxNodeLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST.Diagnostics.Logging
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Diagnostics.Logging;

    /// <summary>
    /// Logs operations concerning <see cref="MethodDeclarationSyntax"/> nodes.
    /// </summary>
    public class MethodDeclarationSyntaxNodeLogger : SyntaxNodeLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclarationSyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public MethodDeclarationSyntaxNodeLogger(TypeDeclarationSyntax parent, MethodDeclarationSyntax node, ILogger logger)
            : base(parent, node, logger)
        {
        }

        protected override string NodeName => new MethodDeclaration(this.Node as MethodDeclarationSyntax).Name;

        protected override string ParentNodeName
        {
            get
            {
                if (this.Parent as ClassDeclarationSyntax != null)
                {
                    return new ClassDeclaration(this.Parent as ClassDeclarationSyntax).Name;
                }

                if (this.Parent as InterfaceDeclarationSyntax != null)
                {
                    return new InterfaceDeclaration(this.Parent as InterfaceDeclarationSyntax).Name;
                }

                return "Unidentified";
            }
        }

        protected override string NodeType => "Method declaration";
    }
}

/// <summary>
/// PropertyDeclarationSyntaxNodeLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST.Diagnostics.Logging
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Diagnostics.Logging;

    /// <summary>
    /// Logs operations concerning <see cref="PropertyDeclarationSyntax"/> nodes.
    /// </summary>
    public class PropertyDeclarationSyntaxNodeLogger : SyntaxNodeLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclarationSyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public PropertyDeclarationSyntaxNodeLogger(TypeDeclarationSyntax parent, PropertyDeclarationSyntax node, ILogger logger)
            : base(parent, node, logger)
        {
        }

        protected override string NodeName => new PropertyDeclaration(this.Node as PropertyDeclarationSyntax).Name;

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

        protected override string NodeType => "Property declaration";
    }
}

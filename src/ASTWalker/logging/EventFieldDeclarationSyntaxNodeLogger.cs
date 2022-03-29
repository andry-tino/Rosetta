/// <summary>
/// FieldDeclarationSyntaxNodeLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.AST.Diagnostics.Logging
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.Diagnostics.Logging;

    /// <summary>
    /// Logs operations concerning <see cref="EventFieldDeclarationSyntax"/> nodes.
    /// </summary>
    public class EventFieldDeclarationSyntaxNodeLogger : SyntaxNodeLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventFieldDeclarationSyntaxNodeLogger"/> class.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <param name="logger"></param>
        public EventFieldDeclarationSyntaxNodeLogger(ClassDeclarationSyntax parent, EventFieldDeclarationSyntax node, ILogger logger) 
            : base(parent, node, logger)
        {
        }

        protected override string NodeName => new EventFieldDeclaration(this.Node as EventFieldDeclarationSyntax).Name;

        protected override string ParentNodeName => new ClassDeclaration(this.Parent as ClassDeclarationSyntax).Name;

        protected override string NodeType => "Event field declaration";
    }
}

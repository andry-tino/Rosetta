/// <summary>
/// AttributeDecoration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing an attribute in AST.
    /// </summary>
    public class AttributeDecoration : Helper
    {
        // Cached quantities
        private IEnumerable<AttributeArgument> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeDecoration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public AttributeDecoration(AttributeSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Argument"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public AttributeDecoration(AttributeSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public string Name
        {
            get { return this.AttributeNode.Name.ToString(); }
        }

        /// <summary>
        /// Gets a collection of attribute arguments.
        /// </summary>
        public IEnumerable<AttributeArgument> Arguments
        {
            get
            {
                if (this.arguments == null)
                {
                    this.arguments = new List<AttributeArgument>();

                    foreach (var argument in this.AttributeNode.ArgumentList.Arguments)
                    {
                        (this.arguments as List<AttributeArgument>).Add(new AttributeArgument(argument));
                    }
                }

                return this.arguments;
            }
        }

        /// <summary>
        /// Gets the <see cref="AttributeListSyntax"/> where the <see cref="AttributeSyntax"/> belongs to.
        /// </summary>
        public AttributeListSyntax AttributeList
        {
            get { return this.AttributeNode.Parent as AttributeListSyntax; }
        }

        /// <summary>
        /// Gets the <see cref="AttributeSyntax"/> underlying the helper and representing the attribute in the AST.
        /// </summary>
        public AttributeSyntax AttributeNode
        {
            get { return (AttributeSyntax)this.SyntaxNode; }
        }

        public AttributeArgument GetArgument(string attributeName)
        {
            if (this.Arguments == null) { return null; }
            return this.Arguments.FirstOrDefault(x => x.AttributeName?.Equals(attributeName, StringComparison.Ordinal) ?? false);
        }
    }
}

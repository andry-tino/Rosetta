/// <summary>
/// UnsupportedSyntaxException.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Raised when a specific syntax construct is not supported.
    /// </summary>
    public class UnsupportedSyntaxException : Exception
    {
        protected SyntaxKind syntaxKind;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedSyntaxException"/> class.
        /// </summary>
        public UnsupportedSyntaxException() 
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedSyntaxException"/> class.
        /// </summary>
        /// <param name="syntaxType"></param>
        /// <param name="message"></param>
        public UnsupportedSyntaxException(CSharpSyntaxNode syntaxNode)
            : base()
        {
            this.syntaxKind = syntaxNode.Kind();
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("Syntax construct {0} is not supported!", this.syntaxKind.ToString());
            }
        }
    }
}

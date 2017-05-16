/// <summary>
/// Visibility.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Helpers
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Helper for <see cref="IMethodInfoProxy"/> in order to retrieve information about its visibility.
    /// </summary>
    public class Visibility
    {
        private readonly ITypeInfoProxy type;
        private readonly IMethodInfoProxy method;

        /// <summary>
        /// Initializes a new instance of the <see cref="Visibility"/> class.
        /// </summary>
        /// <param name="method">The <see cref="IMethodInfoProxy"/> to analyze.</param>
        public Visibility(IMethodInfoProxy method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            this.method = method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Visibility"/> class.
        /// </summary>
        /// <param name="type">The <see cref="ITypeInfoProxy"/> to analyze.</param>
        public Visibility(ITypeInfoProxy type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            this.type = type;
        }

        /// <summary>
        /// Gets the <see cref="SyntaxToken"/> best describing the accessibility of the entity.
        /// </summary>
        public SyntaxKind Token
        {
            get
            {
                if (this.method != null)
                {
                    if (this.method.IsPrivate) return SyntaxKind.PrivateKeyword;
                    if (this.method.IsFamily) return SyntaxKind.ProtectedKeyword;
                    if (this.method.IsPublic) return SyntaxKind.PublicKeyword;

                    // Methods in classes are by default private unless otherwise specify
                    return SyntaxKind.PrivateKeyword;
                }

                if (this.type != null)
                {
                    if (this.type.IsPublic) return SyntaxKind.PublicKeyword;
                    
                    return SyntaxKind.None;
                }

                throw new InvalidOperationException("The helper was constructed in a bad way");
            }
        }
    }
}

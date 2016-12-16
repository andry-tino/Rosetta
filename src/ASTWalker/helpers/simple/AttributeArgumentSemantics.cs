/// <summary>
/// AttributeArgumentSemantics.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Helper for <see cref="TypedConstant"/> representing an argument attribute.
    /// </summary>
    public class AttributeArgumentSemantics : SemanticHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeArgumentSemantics"/> class.
        /// </summary>
        /// <param name="semanticObject">The Roslyn semantic object.</param>
        public AttributeArgumentSemantics(TypedConstant semanticObject)
            : base(semanticObject)
        {
        }

        /// <summary>
        /// Gets the value of the default argument (no name specified).
        /// </summary>
        public string Value => this.AttributeArgument.Value.ToString();

        private TypedConstant AttributeArgument => (TypedConstant)this.SemanticObject;
    }
}

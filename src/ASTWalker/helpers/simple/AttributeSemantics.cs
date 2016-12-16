/// <summary>
/// AttributeSemantics.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Helper for <see cref="AttributeData"/>.
    /// </summary>
    public class AttributeSemantics : SemanticHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeSemantics"/> class.
        /// </summary>
        /// <param name="semanticObject">The Roslyn semantic object.</param>
        public AttributeSemantics(AttributeData semanticObject) 
            : base(semanticObject)
        {
        }

        /// <summary>
        /// Gets the class name of the attribute.
        /// </summary>
        public string AttributeClassName => this.Attribute.AttributeClass.Name;

        /// <summary>
        /// Gets the class metadata name of the attribute.
        /// </summary>
        public string AttributeClassMetadataName => this.Attribute.AttributeClass.MetadataName;

        /// <summary>
        /// Gets the class full name of the attribute.
        /// </summary>
        public string AttributeClassFullName => TypeReference.GetTypeSymbolFullName(this.Attribute.AttributeClass.OriginalDefinition);

        public IEnumerable<AttributeArgumentSemantics> ConstructorArguments => 
            this.Attribute.ConstructorArguments.Select(attribute => new AttributeArgumentSemantics(attribute));

        private AttributeData Attribute => this.SemanticObject as AttributeData;
    }
}

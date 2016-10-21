/// <summary>
/// ClassWithAttributeInDifferentNamespaceASTTransformer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Transformers
{
    using System;

    /// <summary>
    /// Base class for transformers acting on an attribute.
    /// </summary>
    public abstract class ClassWithAttributeInDifferentNamespaceASTTransformer : ClassInDifferentNamespaceASTTransformer
    {
        private readonly string attributeFullName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassWithAttributeInDifferentNamespaceASTTransformer"/> class.
        /// </summary>
        /// <param name="namespaceFullName">The new namespace to assign to the class.</param>
        /// <param name="attributeFullName">The attribute to look for.</param>
        public ClassWithAttributeInDifferentNamespaceASTTransformer(string namespaceFullName, string attributeFullName) 
            : base(namespaceFullName)
        {
            if (attributeFullName == null)
            {
                throw new ArgumentNullException(nameof(attributeFullName), "An attribute name must be provided!");
            }

            this.attributeFullName = attributeFullName;
        }

        /// <summary>
        /// Gets the namespace full name.
        /// </summary>
        protected string AttributeFullName
        {
            get { return this.attributeFullName; }
        }
    }
}

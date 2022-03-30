/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Helpers
{
    using System;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Abstraction for building an AST from an assembly.
    /// </summary>
    public class IgnoreNamespaceAttributeDecoration
    {
        public const string IgnoreNamespaceName = "IgnoreNamespaceAttribute";

        private readonly ICustomAttributeDataProxy attributeData;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="attributeData"></param>
        public IgnoreNamespaceAttributeDecoration(ICustomAttributeDataProxy attributeData)
        {
            if (attributeData == null)
            {
                throw new ArgumentNullException(nameof(attributeData));
            }

            this.attributeData = attributeData;
        }

        public static bool IsIgnoreNamespaceAttributeDecoration(ITypeProxy attribute)
        {
            return attribute.Name == IgnoreNamespaceName;
        }
    }
}

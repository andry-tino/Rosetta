/// <summary>
/// ScriptNamespaceAttributeOnType.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using System.Linq;

    using Rosetta.AST.Helpers;

    /// <summary>
    /// Decorates <see cref="AttributeSemantics"/>.
    /// </summary>
    /// <remarks>
    /// This is a syntax helper which is used to retrieve info on the ScriptNamespace
    /// attribute from a mere syntax point of view.
    /// </remarks>
    public class ScriptNamespaceAttributeOnType
    {
        private readonly AttributeSemantics attribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptNamespaceAttributeOnType"/> class.
        /// </summary>
        /// <param name="attribute"></param>
        public ScriptNamespaceAttributeOnType(AttributeSemantics attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            this.attribute = attribute;
        }

        /// <summary>
        /// Gets a value indicating whether the ScriptNamespace attribute decorates the type or not.
        /// </summary>
        /// <remarks>
        /// Limitation: We consider this usage of the attribute: <code>[ScriptNamespace("SomeName")]</code>.
        /// </remarks>
        public bool HasScriptNamespaceAttributeDecoration => 
            this.attribute.AttributeClassName.Contains(ScriptNamespaceAttributeDecoration.ScriptNamespaceName) &&
            this.attribute.ConstructorArguments.Count() > 0;

        /// <summary>
        /// Gets the value of the overriden namespace in the attribute.
        /// </summary>
        public string OverridenName => this.attribute.ConstructorArguments.First().Value.ToString();
    }
}

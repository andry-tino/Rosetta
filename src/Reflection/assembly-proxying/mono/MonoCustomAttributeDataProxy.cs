/// <summary>
/// MonoAssemblyProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Mono.Cecil;

    /// <summary>
    /// Proxy for Assembly incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("AttributeTypeName = {AttributeTypeFullName}", Name = "Attribute {AttributeTypeName,nq} (Mono proxy)")]
    public class MonoCustomAttributeDataProxy : ICustomAttributeDataProxy
    {
        private readonly CustomAttribute customAttribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoCustomAttributeDataProxy"/> class.
        /// </summary>
        /// <param name="monoModuleDefinition"></param>
        public MonoCustomAttributeDataProxy(CustomAttribute monoCustomAttribute)
        {
            if (monoCustomAttribute == null)
            {
                throw new ArgumentNullException(nameof(monoCustomAttribute));
            }

            this.customAttribute = monoCustomAttribute;
        }

        /// <summary>
        /// Gets the type of the attribute.
        /// </summary>
        public ITypeProxy AttributeType => new MonoTypeProxy(this.customAttribute.AttributeType);

        /// <summary>
        /// Gets the list of positional arguments specified for the attribute instance represented by the CustomAttributeData object.
        /// </summary>
        public IEnumerable<ICustomAttributeTypedArgumentProxy> ConstructorArguments => 
            this.customAttribute.ConstructorArguments.Select(arg => new MonoCustomAttributeTypedArgumentProxy(arg));

        private string AttributeTypeName => this.customAttribute.AttributeType.Name;

        private string AttributeTypeFullName => this.customAttribute.AttributeType.FullName;
    }
}

/// <summary>
/// MonoTypeProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Diagnostics;

    using Mono.Cecil;

    /// <summary>
    /// Proxy for type incapsulating Mono.
    /// </summary>
    /// <remarks>
    /// The difference with <see cref="MonoTypeInfoProxy"/> is that this one does not provide info about the type definition.
    /// </remarks>
    [DebuggerDisplay("TypeName = {Name}", Name = "Type {Name,nq} (Mono proxy)")]
    public class MonoTypeProxy : ITypeProxy
    {
        private readonly TypeReference typeReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoTypeProxy"/> class.
        /// </summary>
        /// <param name="monoTypeDefinition"></param>
        public MonoTypeProxy(TypeReference monoTypeReference)
        {
            if (monoTypeReference == null)
            {
                throw new ArgumentNullException(nameof(monoTypeReference));
            }

            this.typeReference = monoTypeReference;
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name => this.typeReference.Name;

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        public string FullName => this.typeReference.FullName;
    }
}

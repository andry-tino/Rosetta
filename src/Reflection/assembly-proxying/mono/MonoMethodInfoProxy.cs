/// <summary>
/// MonoMethodInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Diagnostics;

    using Mono.Cecil;

    /// <summary>
    /// Proxy for method incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("Method {Name,nq}", Name = "Method {Name,nq} (Mono proxy)")]
    public class MonoMethodInfoProxy : MonoMethodBaseProxy, IMethodInfoProxy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoMethodInfoProxy"/> class.
        /// </summary>
        /// <param name="monoMethodDefinition"></param>
        public MonoMethodInfoProxy(MethodDefinition monoMethodDefinition) 
            : base(monoMethodDefinition)
        {
        }
        /// <summary>
        /// Gets the return type of this method.
        /// </summary>
        public ITypeProxy ReturnType => new MonoTypeProxy(this.methodDefinition.ReturnType);
    }
}

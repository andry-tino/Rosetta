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
    /// Proxy for constructor incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("Constructor {Name,nq}", Name = "Constructor {Name,nq} (Mono proxy)")]
    public class MonoConstructorInfoProxy : MonoMethodBaseProxy, IConstructorInfoProxy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoConstructorInfoProxy"/> class.
        /// </summary>
        /// <param name="monoMethodDefinition"></param>
        public MonoConstructorInfoProxy(MethodDefinition monoMethodDefinition) 
            : base(monoMethodDefinition)
        {
        }
    }
}

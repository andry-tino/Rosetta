/// <summary>
/// MonoMethodInfoProxy.cs
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
    [DebuggerDisplay("Method {Name,nq}", Name = "Method {Name,nq} (Mono proxy)")]
    public class MonoMethodInfoProxy : IMethodInfoProxy
    {
        private readonly MethodDefinition methodDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoMethodInfoProxy"/> class.
        /// </summary>
        /// <param name="monoTypeDefinition"></param>
        public MonoMethodInfoProxy(MethodDefinition monoMethodDefinition)
        {
            if (monoMethodDefinition == null)
            {
                throw new ArgumentNullException(nameof(monoMethodDefinition));
            }

            this.methodDefinition = monoMethodDefinition;
        }

        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        public string Name => this.methodDefinition.Name;

        /// <summary>
        /// Gets the parameters of the specified method or constructor.
        /// </summary>
        public IEnumerable<IParameterInfoProxy> Parameters => this.methodDefinition.HasParameters 
            ? this.methodDefinition.Parameters.Select(parameter => new MonoParameterInfoProxy(parameter)) 
            : null;

        /// <summary>
        /// Gets the return type of this method.
        /// </summary>
        public ITypeProxy ReturnType => new MonoTypeProxy(this.methodDefinition.ReturnType);

        /// <summary>
        /// Gets a value indicating whether the method or constructor is visible only within its class 
        /// and derived classes.
        /// </summary>
        public bool IsFamily => this.methodDefinition.IsFamily;

        /// <summary>
        /// Gets a value indicating whether this member is private.
        /// </summary>
        public bool IsPrivate => this.methodDefinition.IsPrivate;

        /// <summary>
        /// Gets a value indicating whether this member is public.
        /// </summary>
        public bool IsPublic => this.methodDefinition.IsPublic;
    }
}

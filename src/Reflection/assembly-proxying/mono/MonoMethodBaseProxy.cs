/// <summary>
/// MonoMethodBaseProxy.cs
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
    /// Proxy for method incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("MethodBase {Name,nq}", Name = "MethodBase {Name,nq} (Mono proxy)")]
    public abstract class MonoMethodBaseProxy : IMethodBaseProxy
    {
        protected readonly MethodDefinition methodDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoMethodBaseProxy"/> class.
        /// </summary>
        /// <param name="monoTypeDefinition"></param>
        public MonoMethodBaseProxy(MethodDefinition monoMethodDefinition)
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

        /// <summary>
        /// Gets a value indicating whether the method is static.
        /// </summary>
        public bool IsStatic => this.methodDefinition.IsStatic;
    }
}

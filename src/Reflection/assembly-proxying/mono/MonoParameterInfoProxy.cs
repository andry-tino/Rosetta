/// <summary>
/// MonoParameterInfoProxy.cs
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
    /// Proxy for parameter info incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("Method {Name,nq}", Name = "Method {Name,nq} (Mono proxy)")]
    public class MonoParameterInfoProxy : IParameterInfoProxy
    {
        private readonly ParameterDefinition parameterDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoMethodInfoProxy"/> class.
        /// </summary>
        /// <param name="monoTypeDefinition"></param>
        public MonoParameterInfoProxy(ParameterDefinition monoParameterDefinition)
        {
            if (monoParameterDefinition == null)
            {
                throw new ArgumentNullException(nameof(monoParameterDefinition));
            }

            this.parameterDefinition = monoParameterDefinition;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name => this.parameterDefinition.Name;

        /// <summary>
        /// Gets the Type of this parameter.
        /// </summary>
        public ITypeProxy ParameterType => new MonoTypeProxy(this.parameterDefinition.ParameterType);
    }
}

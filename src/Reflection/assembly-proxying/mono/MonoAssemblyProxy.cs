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
    [DebuggerDisplay("Assembly = {AssemblyName}", Name = "Assembly {AssemblyName,nq} (Mono proxy)")]
    public class MonoAssemblyProxy : IAssemblyProxy
    {
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoAssemblyProxy"/> class.
        /// </summary>
        /// <param name="monoModuleDefinition"></param>
        public MonoAssemblyProxy(ModuleDefinition monoModuleDefinition)
        {
            if (monoModuleDefinition == null)
            {
                throw new ArgumentNullException(nameof(monoModuleDefinition));
            }

            this.moduleDefinition = monoModuleDefinition;
        }

        /// <summary>
        /// Gets a collection of the types defined in this assembly.
        /// </summary>
        public IEnumerable<ITypeInfoProxy> DefinedTypes => this.moduleDefinition.Types.Select(type => new MonoTypeInfoProxy(type));

        // Used by debugger
        private string AssemblyName => this.moduleDefinition.FullyQualifiedName;
    }
}

/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Utilities
{
    using System;

    using Rosetta.Reflection.ScriptSharp.Helpers;

    /// <summary>
    /// Helper for <see cref="ITypeInfoProxy"/> in order to retrieve information about its namespace.
    /// </summary>
    public static class ScriptNamespaceOverride
    {
        /// <summary>
        /// Looks up a type and returns the proper full name in case a ScriptNamespace override is applied on it.
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="typeLookup"></param>
        /// <returns></returns>
        public static string LookupTypeAndOverrideFullName(this string fullName, ITypeLookup typeLookup)
        {
            var typeDefinition = typeLookup.GetByFullName(fullName);

            // Could not find a definition for the type. Strange but we can go on
            // TODO: Find a reporting strategy
            if (typeDefinition == null)
            {
                return fullName;
            }

            // Check the presence of the ScriptNamespace attribute
            var namespaceHelper         = new Namespace(typeDefinition);                            // Gets the ScriptNamespace override
            var ordinaryNamespaceHelper = new Rosetta.Reflection.Helpers.Namespace(typeDefinition); // Gets the original namespace

            return Rosetta.Reflection.Helpers.Namespace.ReplaceNamespace(fullName, 
                ordinaryNamespaceHelper.FullName, namespaceHelper.FullName);
        }
    }
}

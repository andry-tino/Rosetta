/// <summary>
/// DataProvider.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Renderings
{
    using System;

    using Rosetta.ScriptSharp.Definition.AST.Renderings.Data;

    /// <summary>
    /// 
    /// </summary>
    internal static class DataProvider
    {
        public static Type ClassesDefinitionMethodsProvider => typeof(Classes);

        public static Type EnumsDefinitionMethodsProvider => typeof(Enums);

        public static Type InterfacesDefinitionMethodsProvider => typeof(Interfaces);

        public static Type ClassesWithScriptNamespaceDefinitionMethodsProvider => typeof(ClassesWithScriptNamespace);

        public static Type EnumsWithScriptNamespaceDefinitionMethodsProvider => typeof(EnumsWithScriptNamespace);

        public static Type InterfacesWithScriptNamespaceDefinitionMethodsProvider => typeof(InterfacesWithScriptNamespace);

        public static Type MethodsWithPreserveNameDefinitionMethodsProvider => typeof(MethodsWithPreserveName);

        public static Type PropertiesWithPreserveNameDefinitionMethodsProvider => typeof(PropertiesWithPreserveName);

        public static Type FieldsWithPreserveNameDefinitionMethodsProvider => typeof(FieldsWithPreserveName);
    }
}

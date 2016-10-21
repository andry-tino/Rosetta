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
        public static Type ClassesDefinitionMethodsProvider
        {
            get { return typeof(Classes); }
        }

        public static Type ClassesWithScriptNamespaceDefinitionMethodsProvider
        {
            get { return typeof(ClassesWithScriptNamespace); }
        }
    }
}

/// <summary>
/// DataProvider.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Renderings
{
    using System;

    using Rosetta.AST.Renderings.Data;

    /// <summary>
    /// 
    /// </summary>
    internal static class DataProvider
    {
        public static Type ClassesMethodsProvider
        {
            get { return typeof(Classes); }
        }
        
        public static Type InterfacesMethodstProvider
        {
            get { return typeof(Interfaces); }
        }

        public static Type MethodsMethodstProvider
        {
            get { return typeof(Methods); }
        }

        public static Type ConstructorsMethodstProvider
        {
            get { return typeof(Constructors); }
        }
    }
}

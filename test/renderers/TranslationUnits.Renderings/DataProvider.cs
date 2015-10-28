/// <summary>
/// DataProvider.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;

    using Rosetta.Translation.Renderings.Data;

    /// <summary>
    /// 
    /// </summary>
    internal static class DataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public static Type ClassesMethodsProvider
        {
            get
            {
                return typeof(Classes);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Type MethodsMethodsProvider
        {
            get
            {
                return typeof(Methods);
            }
        }
    }
}

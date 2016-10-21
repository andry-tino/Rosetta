/// <summary>
/// BaseTypeKind.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Roslyn = Microsoft.CodeAnalysis;

    /// <summary>
    /// Describes the type of base type.
    /// </summary>
    public enum TypeKind
    {
        /// <summary>
        /// Base type is a class.
        /// </summary>
        Class,

        /// <summary>
        /// Base type is an interface.
        /// </summary>
        Interface
    }

    /// <summary>
    /// 
    /// </summary>
    public static class TypeKindUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeKind"></param>
        /// <returns></returns>
        public static TypeKind ToTypeKind(this Roslyn.TypeKind typeKind)
        {
            switch (typeKind)
            {
                case Roslyn.TypeKind.Class: return TypeKind.Class;
                case Roslyn.TypeKind.Interface: return TypeKind.Interface;
            }

            throw new ArgumentOutOfRangeException(nameof(typeKind), "Unrecognized type kind!");
        }
    }
}

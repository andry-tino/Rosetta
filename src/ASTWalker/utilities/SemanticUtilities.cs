/// <summary>
/// SemanticUtilities.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Roslyn = Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;

    /// <summary>
    /// Utilities for handling semantics.
    /// </summary>
    public static class SemanticUtilities
    {
        /// <summary>
        /// Guesses the base type kind basing on the name of the base type.
        /// </summary>
        /// <param name="baseTypes"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        public static IEnumerable<BaseTypeInfo> SeparateClassAndInterfacesBasedOnNames(IEnumerable<BaseTypeSyntax> baseTypes)
        {
            if (!CanSeparateClassAndInterfacesBasedOnNames(baseTypes))
            {
                var names = baseTypes.Select(baseType => new BaseTypeReference(baseType).Name).Aggregate((i, j) => $"{i}; {j}");
                throw new ArgumentException(nameof(baseTypes), $"The provided collection of base types: [{names}] cannot be evaluated with this approach");
            }

            var infos = new List<BaseTypeInfo>();

            foreach (var baseType in baseTypes)
            {
                var helper = new BaseTypeReference(baseType);
                var name = helper.Name;
                var firstLetter = name[0];

                if (firstLetter == 'I')
                {
                    infos.Add(new BaseTypeInfo() { Node = baseType, Kind = Roslyn.TypeKind.Interface });
                }
                else
                {
                    infos.Add(new BaseTypeInfo() { Node = baseType, Kind = Roslyn.TypeKind.Class });
                }
            }

            return infos;
        }

        /// <summary>
        /// Used for assessing whether <see cref="SeparateClassAndInterfacesBasedOnNames"/> can be used for separating class and interfaces.
        /// </summary>
        /// <param name="baseTypes"></param>
        /// <returns></returns>
        public static bool CanSeparateClassAndInterfacesBasedOnNames(IEnumerable<BaseTypeSyntax> baseTypes)
        {
            int nameStartsWithIBaseTypes = 0;
            int nameDoesNotStartWithIBaseTypes = 0;

            foreach (var baseType in baseTypes)
            {
                var helper = new BaseTypeReference(baseType);
                var name = helper.Name;
                var firstLetter = name[0];

                if (firstLetter == 'I')
                {
                    nameStartsWithIBaseTypes++;
                }
                else
                {
                    nameDoesNotStartWithIBaseTypes++;
                }
            }

            return nameDoesNotStartWithIBaseTypes <= 1;
        }

        /// <summary>
        /// Guesses the base type kind basing on name.
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static Roslyn.TypeKind GuessBaseTypeKindFromName(BaseTypeSyntax baseType)
        {
            var helper = new BaseTypeReference(baseType);
            var name = helper.Name;
            var firstLetter = name[0];

            if (firstLetter == 'I')
            {
                return Roslyn.TypeKind.Interface;
            }

            return Roslyn.TypeKind.Class;
        }

        #region Types

        /// <summary>
        /// 
        /// </summary>
        public sealed class BaseTypeInfo
        {
            /// <summary>
            /// Gets or sets the node.
            /// </summary>
            public BaseTypeSyntax Node { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            public Roslyn.TypeKind Kind { get; set; }
        }

        #endregion
    }
}

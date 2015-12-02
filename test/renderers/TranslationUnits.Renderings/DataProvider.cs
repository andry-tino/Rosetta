/// <summary>
/// DataProvider.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;

    using Rosetta.Translation.Renderings.Data;

    using BinaryExpressions = Rosetta.Translation.Renderings.Data.BinaryExpressions;
    using UnaryExpressions = Rosetta.Translation.Renderings.Data.UnaryExpressions;
    using CastExpressions = Rosetta.Translation.Renderings.Data.CastExpressions;
    using ParenthesizedExpressions = Rosetta.Translation.Renderings.Data.ParenthesizedExpressions;

    /// <summary>
    /// 
    /// </summary>
    internal static class DataProvider
    {
        /// <summary>
        /// Gets render methods for <see cref="Modules"/>.
        /// </summary>
        public static Type ModulesMethodsProvider
        {
            get
            {
                return typeof(Modules);
            }
        }

        /// <summary>
        /// Gets render methods for <see cref="Classes"/>.
        /// </summary>
        public static Type ClassesMethodsProvider
        {
            get
            {
                return typeof(Classes);
            }
        }

        /// <summary>
        /// Gets render methods for <see cref="Methods"/>.
        /// </summary>
        public static Type MethodsMethodsProvider
        {
            get
            {
                return typeof(Methods);
            }
        }

        /// <summary>
        /// Gets render methods for <see cref="Members"/>.
        /// </summary>
        public static Type MembersMethodsProvider
        {
            get
            {
                return typeof(Members);
            }
        }

        /// <summary>
        /// Gets render methods for <see cref="BinaryExpressions.Expressions"/>.
        /// </summary>
        public static Type BinaryExpressionsMethodsProvider
        {
            get
            {
                return typeof(BinaryExpressions.Expressions);
            }
        }

        /// <summary>
        /// Gets render methods for <see cref="UnaryExpressions.Expressions"/>.
        /// </summary>
        public static Type UnaryExpressionsMethodsProvider
        {
            get
            {
                return typeof(UnaryExpressions.Expressions);
            }
        }

        /// <summary>
        /// Gets render methods for <see cref="CastExpressions.Expressions"/>.
        /// </summary>
        public static Type CastExpressionsMethodsProvider
        {
            get
            {
                return typeof(CastExpressions.Expressions);
            }
        }

        /// <summary>
        /// Gets render methods for <see cref="ParenthesizedExpressions.Expressions"/>.
        /// </summary>
        public static Type ParenthesizedExpressionsMethodsProvider
        {
            get
            {
                return typeof(ParenthesizedExpressions.Expressions);
            }
        }
    }
}

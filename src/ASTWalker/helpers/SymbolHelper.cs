/// <summary>
/// SymbolHelper.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Helper for symbols, relying on the <see cref="SemanticModel"/>.
    /// </summary>
    public static class SymbolHelper
    {
        /// <summary>
        /// Utility method for separating cases when <see cref="SemanticModel"/> is available and not.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="noSemanticModelAction"></param>
        /// <param name="semanticModelAction"></param>
        /// <returns></returns>
        public static T ChooseSymbolFrom<T>(this Helper helper, Func<T> noSemanticModelAction, Func<SemanticModel, T> semanticModelAction)
        {
            if (helper.SemanticModel != null)
            {
                if (semanticModelAction == null)
                {
                    throw new ArgumentNullException(nameof(semanticModelAction));
                }

                return semanticModelAction(helper.SemanticModel);
            }

            if (noSemanticModelAction == null)
            {
                throw new ArgumentNullException(nameof(noSemanticModelAction));
            }

            return noSemanticModelAction();
        }
    }
}

/// <summary>
/// TokenUtility.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Utility class for tokens.
    /// </summary>
    public static class TokenUtility
    {
        /// <summary>
        /// The public TypeScript keyword.
        /// </summary>
        public static string PublicVisibilityToken
        {
            get { return "public"; }
        }

        /// <summary>
        /// The private TypeScript keyword.
        /// </summary>
        public static string PrivateVisibilityToken
        {
            get { return "private"; }
        }

        /// <summary>
        /// The protected TypeScript keyword.
        /// </summary>
        public static string ProtectedVisibilityToken
        {
            get { return "protected"; }
        }

        /// <summary>
        /// Converts the visibility into the appropriate TypeScript token.
        /// </summary>
        /// <param name="visibilityToken">The visibility.</param>
        /// <returns></returns>
        public static string ToString(this VisibilityToken visibilityToken)
        {
            return visibilityToken != VisibilityToken.None ? visibilityToken.ToString("G").ToLower() : string.Empty;
        }

        /// <summary>
        /// Converts the operator into the appropriate TypeScript token.
        /// </summary>
        /// <param name="operatorToken">The visibility.</param>
        /// <returns></returns>
        public static string ToString(this OperatorToken operatorToken)
        {
            switch (operatorToken)
            {
                case OperatorToken.Addition:
                    return Lexems.Plus;
                case OperatorToken.Subtraction:
                    return Lexems.Minus;
                case OperatorToken.Multiplication:
                    return Lexems.Times;
                case OperatorToken.Divide:
                    return Lexems.Divide;
                case OperatorToken.Increment:
                    return Lexems.PlusPlus;
                case OperatorToken.Decrement:
                    return Lexems.MinusMinus;
                default:
                    return string.Empty;
            }
        }
    }
}

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
        /// 
        /// TODO: Move to Lexems.
        /// </summary>
        public static string PublicVisibilityToken
        {
            get { return "public"; }
        }

        /// <summary>
        /// The private TypeScript keyword.
        /// 
        /// TODO: Move to Lexems.
        /// </summary>
        public static string PrivateVisibilityToken
        {
            get { return "private"; }
        }

        /// <summary>
        /// The protected TypeScript keyword.
        /// 
        /// TODO: Move to Lexems.
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
        /// 
        /// </summary>
        /// <param name="visibilityToken"></param>
        /// <returns></returns>
        public static string EmitOptionalVisibility(this VisibilityToken visibilityToken)
        {
            return visibilityToken == VisibilityToken.None ? 
                string.Empty :
                string.Format("{0} ", ToString(visibilityToken));
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
                case OperatorToken.AdditionAssignment:
                    return Lexems.PlusAssign;
                case OperatorToken.Subtraction:
                    return Lexems.Minus;
                case OperatorToken.SubtractionAssignment:
                    return Lexems.MinusAssign;
                case OperatorToken.Multiplication:
                    return Lexems.Times;
                case OperatorToken.MultiplicationAssignment:
                    return Lexems.TimesAssign;
                case OperatorToken.Divide:
                    return Lexems.Divide;
                case OperatorToken.DivideAssignment:
                    return Lexems.DivideAssign;
                case OperatorToken.Increment:
                    return Lexems.PlusPlus;
                case OperatorToken.Decrement:
                    return Lexems.MinusMinus;
                case OperatorToken.Modulo:
                    return Lexems.Modulo;
                case OperatorToken.ModuloAssignment:
                    return Lexems.ModuloAssign;
                case OperatorToken.And:
                    return Lexems.And;
                case OperatorToken.AndAssignment:
                    return Lexems.AndAssign;
                case OperatorToken.Or:
                    return Lexems.Or;
                case OperatorToken.OrAssignment:
                    return Lexems.OrAssign;
                case OperatorToken.Xor:
                    return Lexems.Xor;
                case OperatorToken.XorAssignment:
                    return Lexems.XorAssign;
                case OperatorToken.LogicalEquals:
                    return Lexems.LogicalEquals;
                case OperatorToken.NotEquals:
                    return Lexems.LogicalNotEquals;
                case OperatorToken.LogicalNot:
                    return Lexems.LogicalNot;
                case OperatorToken.Equals:
                    return Lexems.EqualsSign;
                default:
                    return string.Empty;
            }
        }
    }
}

/// <summary>
/// TokenUtility.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

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
        /// <param name="modifierTokens">The modifiers.</param>
        /// <returns></returns>
        public static string ToString(this ModifierTokens modifierTokens)
        {
            string representation = string.Empty;

            if (modifierTokens != ModifierTokens.None)
            {
                var modifiers = new List<string>();

                foreach (ModifierTokens flag in Enum.GetValues(typeof(ModifierTokens)))
                {
                    if (flag == ModifierTokens.None)
                    {
                        continue;
                    }

                    if (modifierTokens.HasFlag(flag))
                    {
                        modifiers.Add(flag.ToString("G").ToLower());
                    }
                }

                representation = string.Join(" ", modifiers);
            }

            return representation;
        }

        /// <summary>
        /// Generates a <see cref="ModifierTokens"/> which is fully TypeScript compliant.
        /// </summary>
        /// <param name="modifierTokens">The modifiers.</param>
        /// <returns></returns>
        public static ModifierTokens ConvertToTypeScriptEquivalent(this ModifierTokens modifierTokens)
        {
            ModifierTokens modifiers = ModifierTokens.None;

            if (modifierTokens.HasFlag(ModifierTokens.Static))
            {
                modifiers |= ModifierTokens.Static;
            }

            if (modifierTokens.HasFlag(ModifierTokens.Private))
            {
                modifiers |= ModifierTokens.Private;

                return modifiers;
            }

            if (modifierTokens.HasFlag(ModifierTokens.Public))
            {
                modifiers |= ModifierTokens.Public;

                return modifiers;
            }

            if (modifierTokens.HasFlag(ModifierTokens.Protected))
            {
                modifiers |= ModifierTokens.Protected;

                if (modifierTokens.HasFlag(ModifierTokens.Internal))
                {
                    modifiers &= ~ModifierTokens.Protected; // Remove protected
                    modifiers |= ModifierTokens.Public; // Add public

                    return modifiers;
                }

                return modifiers;
            }

            if (modifierTokens.HasFlag(ModifierTokens.Internal))
            {
                modifiers |= ModifierTokens.Public;

                return modifiers;
            }

            return modifiers;
        }

        /// <summary>
        /// Removes the <see cref="ModifierTokens.Public"/> token if found among flags.
        /// </summary>
        /// <param name="modifierTokens">The modifiers.</param>
        /// <returns></returns>
        public static ModifierTokens StripPublic(this ModifierTokens modifierTokens)
        {
            ModifierTokens modifiers = modifierTokens;

            if (modifierTokens.HasFlag(ModifierTokens.Public))
            {
                modifiers &= ~ModifierTokens.Public;
            }

            return modifiers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifierTokens">The modifiers.</param>
        /// <returns></returns>
        public static string EmitOptionalVisibility(this ModifierTokens modifierTokens)
        {
            return modifierTokens == ModifierTokens.None ? 
                string.Empty :
                string.Format("{0} ", ToString(modifierTokens));
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

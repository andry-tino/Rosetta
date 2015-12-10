/// <summary>
/// LiteralTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for literals.
    /// </summary>
    public class LiteralTranslationUnit<T> : ITranslationUnit
    {
        private T literalValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralTranslationUnit(T)"/> class.
        /// </summary>
        /// <param name="name"></param>
        private LiteralTranslationUnit(T value)
        {
            this.literalValue = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static LiteralTranslationUnit<T> Create(T value)
        {
            return new LiteralTranslationUnit<T>(value);
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            string value = this.literalValue.ToString();

            if (typeof(T) == typeof(string))
            {
                value = string.Format("{1}{0}{1}", value, Lexems.SingleQuote);
            }

            return value;
        }
    }
}

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
    public class LiteralTranslationUnit<T> : ExpressionTranslationUnit
    {
        protected T literalValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralTranslationUnit(T)"/> class.
        /// </summary>
        /// <param name="name"></param>
        protected LiteralTranslationUnit(T value)
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
        public override string Translate()
        {
            string value = this.literalValue.ToString();

            // Case: string
            if (typeof(T) == typeof(string))
            {
                value = string.Format("{1}{0}{1}", value, Lexems.SingleQuote);
            }

            // Case: boolean
            if (typeof(T) == typeof(bool))
            {
                value = string.Format("{0}", bool.Parse(value) ? Lexems.TrueKeyword : Lexems.FalseKeyword);
            }

            return value;
        }
    }

    /// <summary>
    /// Translation unit for literals when we want to directly assign them.
    /// </summary>
    public class LiteralTranslationUnit : LiteralTranslationUnit<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        private LiteralTranslationUnit(string value) :
            base(value)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new static LiteralTranslationUnit Create(string value)
        {
            return new LiteralTranslationUnit(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static LiteralTranslationUnit Null
        {
            get { return Create(Lexems.NullKeyword); }
        }

        public override string Translate()
        {
            string value = string.Format("{0}", this.literalValue);

            return value;
        }
    }
}

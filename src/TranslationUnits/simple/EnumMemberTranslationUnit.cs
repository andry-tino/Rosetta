/// <summary>
/// EnumMemberTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for enum members.
    /// </summary>
    public class EnumMemberTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
    {
        private ITranslationUnit name;
        private ITranslationUnit value;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMemberTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private EnumMemberTranslationUnit(ITranslationUnit name, ITranslationUnit value) 
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnumMemberTranslationUnit Create(ITranslationUnit name, ITranslationUnit value = null)
        {
            return new EnumMemberTranslationUnit(name, value);
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            if (this.value != null)
            {
                writer.Write("{0} {1} {2}", 
                    this.name.Translate(), 
                    Lexems.EqualsSign, 
                    this.value.Translate());
            }
            else
            {
                writer.Write("{0}", this.name.Translate());
            }

            return writer.ToString();
        }
    }
}

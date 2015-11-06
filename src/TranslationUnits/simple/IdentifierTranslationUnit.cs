/// <summary>
/// IdentifierTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for identifiers.
    /// </summary>
    public class IdentifierTranslationUnit : ITranslationUnit
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        private IdentifierTranslationUnit(string name)
        {
            this.name = ValidateName(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IdentifierTranslationUnit Create(string name)
        {
            return new IdentifierTranslationUnit(name);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IdentifierTranslationUnit Empty
        {
            get { return Create(string.Empty); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static IdentifierTranslationUnit Void
        {
            get { return Create(Lexems.VoidReturnType); }
        }

        /// <summary>
        /// The nesting level.
        /// </summary>
        public int NestingLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            return this.name;
        }

        #region Validation

        private static string ValidateName(string name)
        {
            if (name.Contains(" "))
            {
                throw new ArgumentException("Name cannot contain spaces", nameof(name));
            }

            return name;
        }

        #endregion
    }
}
